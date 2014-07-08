using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Myware.Common
{
    public static class StaticUtility
    {
        private static object syncObject = new object();
        private static Dictionary<string, Type> typesByFullName = new Dictionary<string, Type>();
        private static Regex removeUnexpectedChars = new Regex(@"[\x00-\x08\x0b-\x0c\x0e-\x1f]", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex regexStripHTML = new Regex("<[^>]+>", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static HashSet<string> loadedAssemblies = new HashSet<string>();

        /// <summary>
        /// Get base url of current deployed web site.
        /// </summary>
        public static string WebSiteBaseUrl
        {
            get
            {
                //Getting the current context of HTTP request
                HttpContext context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    string serverHostAddress = string.Format("{0}://{1}{2}", context.Request.Url.Scheme, context.Request.Url.Host, context.Request.Url.Port == 80 ? string.Empty : ":" + context.Request.Url.Port);
                    //Formatting the fully qualified website url/name
                    return serverHostAddress + context.Request.ApplicationPath;
                }

                return null;
            }
        }

        /// <summary>
        /// Return true when argument is null or empty (after trimmed).
        /// </summary>
        /// <param name="arg">Input object</param>
        /// <returns>True when argument is null or empty (after trimmed).</returns>
        public static bool IsEmpty(object arg)
        {
            if (arg == null)
            {
                return true;
            }

            return arg.ToString().Trim().Length == 0;
        }

        /// <summary>
        /// Truncate characters more than specified length in the HTML string ignoring HTML tags.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="length">Number should be separated into</param>
        /// <returns>Separated string.</returns>
        public static string TruncateHTML(string input, int length)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            string text = regexStripHTML.Replace(input, "");

            if (text.Length > length)
            {
                text = text.Substring(0, length) + "...";
            }

            return text;
        }

        /// <summary>
        /// Truncate characters more than max length in the string.
        /// </summary>
        /// <param name="s">Input string</param>
        /// <param name="maxLength">The maximum lenght of output string.</param>
        /// <returns>Return string with truncating the characters more than specified max length.</returns>
        public static string TruncateText(string s, int maxLength)
        {
            if (StaticUtility.IsEmpty(s))
            {
                return "";
            }

            return s.Length > maxLength ? s.Substring(0, maxLength) + "..." : s;
        }

        /// <summary>
        /// If arguement is null, it will throw ArgumentNullException
        /// </summary>
        /// <param name="arg">Input argument for checking.</param>
        /// <param name="argumentName">Argument name in thrown exception.</param>
        public static void NotNull(object arg, string argumentName)
        {
            if (arg == null)
                throw new ArgumentNullException(argumentName);

            if (arg.ToString().Trim() == "")
            {
                throw new ArgumentNullException(argumentName);
            }
        }


        /// <summary>
        /// Encode url.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlEncode(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return System.Web.HttpUtility.UrlEncode(input);
        }

        /// <summary>
        /// Decode url.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlDecode(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return System.Web.HttpUtility.UrlDecode(input);
        }

        /// <summary>
        /// Cast the input object to bool value intelligently.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="defaultValue">Default value if convertion failed.</param>
        /// <returns></returns>
        public static bool ToBool(object arg, bool defaultValue)
        {
            if (arg == null) return false;

            if (arg is bool) return (bool)arg;

            string strBool = arg.ToString().ToLower();
            try
            {
                return bool.Parse(strBool);
            }
            catch (System.FormatException)
            {
            }

            if (string.Compare(strBool, "1") == 0)
                return true;
            else if (string.Compare(strBool, "0") == 0)
                return false;

            return defaultValue;
        }


        /// <summary>
        /// Convert the input string to target type using different ways.
        /// The attempts are 
        /// 1) convert explicitly. 
        /// 2) use Convert.ChangeType. 
        /// 3) Enumeration Parsing if the target type is an enumeration. 
        /// 4) Call Parse if the target type has the public static method.
        /// 5) Call constructor using the direct input value.
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">If all attempts of convertion are failed.</exception>
        public static object ConvertTypeInternal(string stringValue, Type targetType)
        {
            if (targetType == typeof(string))
                return stringValue;

            if (targetType == typeof(bool))
                return StaticUtility.ToBool(stringValue, false);

            try
            {
                return Convert.ChangeType(stringValue, targetType);
            }
            catch
            {
            }

            if (targetType.IsSubclassOf(typeof(Enum)))
            {
                try
                {
                    return Enum.Parse(targetType, stringValue);
                }
                catch
                {
                }
            }

            // If the target type has method "Parse"
            MethodInfo method = targetType.GetMethod("Parse", new Type[] { typeof(string) });
            if (method != null)
            {
                try
                {
                    return method.Invoke(null, new object[] { stringValue });
                }
                catch
                {
                }
            }

            // If the target type has a constructor with only parameter "string"
            ConstructorInfo constructor = targetType.GetConstructor(new Type[] { typeof(string) });
            if (constructor != null)
            {
                try
                {
                    return constructor.Invoke(new object[] { stringValue });
                }
                catch
                {
                }
            }

            throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, Resource.ConvertTypeFailed, stringValue, targetType));
        }

        /// <summary>
        /// Concat enumerable strings into a single string by separator "; ".
        /// </summary>
        /// <param name="inputStrings"></param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> inputStrings)
        {
            return inputStrings.Concat("; ");
        }

        /// <summary>
        /// Concat enumerable strings into a single string by the specified separator.
        /// </summary>
        /// <param name="inputStrings"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Concat(this IEnumerable<string> inputStrings, string separator)
        {
            StringBuilder output = new StringBuilder();
            foreach (string input in inputStrings)
            {
                if (output.Length > 0) output.Append(separator);
                output.Append(input);
            }

            return output.ToString();
        }

        /// <summary>
        /// Get System.Type by assembly qualified name as "[Namespace].[Type], [Assembly]". 
        /// This works around the exception thrown by System.Type.GetType() if an assembly is referenced and located in the executing application but not loaded at a moment.
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">when the specified type doesn't exist.</exception>
        public static Type GetType(string typeFullName)
        {
            Type resolvedTypeByDotNet = Type.GetType(typeFullName);
            if (resolvedTypeByDotNet != null) return resolvedTypeByDotNet;

            string[] typeSpliter = typeFullName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string typeName = typeSpliter[0].Trim();
            string assemblyName = null;

            if (typeSpliter.Length > 1)
                assemblyName = typeSpliter[1].Trim();

            if (assemblyName != null && !loadedAssemblies.Contains(assemblyName))
            {
                lock (syncObject)
                {
                    if (!loadedAssemblies.Contains(assemblyName))
                    {
                        if (AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.ManifestModule.Name.Equals(assemblyName + ".dll", StringComparison.InvariantCultureIgnoreCase)).Count() == 0)
                        {
                            AppDomain.CurrentDomain.Load(assemblyName);
                        }

                        loadedAssemblies.Add(assemblyName);
                    }
                }
            }

            if (!typesByFullName.ContainsKey(typeName))
            {
                lock (syncObject)
                {
                    if (!typesByFullName.ContainsKey(typeName))
                    {
                        Type targetType = null;
                        Assembly targetAssembly = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                                   where ((targetType = assembly.GetType(typeName)) != null)
                                                   select assembly).FirstOrDefault();

                        if (targetAssembly == null || targetType == null)
                        {
                            throw new NotImplementedException(string.Format(Resource.TypeNotImplementedInDomain, typeFullName));
                        }

                        typesByFullName.Add(typeName, targetType);
                    }
                }
            }

            return typesByFullName[typeName];
        }

        /// <summary>
        /// Convert specified path to local file system absolute path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToAbsolutePath(string path)
        {
            if (path.StartsWith("~/") || path.StartsWith("~\\"))
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server.MapPath(path);
                else
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path.Substring(2));
            }
            else if (path.StartsWith("/") || path.StartsWith("\\"))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Resolve absolute page url.
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static string ResolveAbsoluteUrl(string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl)) return WebSiteBaseUrl;

            string upperCasedPageUrl = pageUrl.ToUpperInvariant();

            if (upperCasedPageUrl.StartsWith("HTTP")) return pageUrl;
            else if (upperCasedPageUrl.StartsWith("FTP")) return pageUrl;

            if (pageUrl.StartsWith("~/"))
                return Path.Combine(WebSiteBaseUrl, pageUrl.Substring(2));
            else if (pageUrl.StartsWith("/"))
                return pageUrl;

            return Path.Combine(WebSiteBaseUrl, pageUrl);
        }
    }
}
