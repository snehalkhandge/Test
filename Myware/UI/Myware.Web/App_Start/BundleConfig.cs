using System.Web;
using System.Web.Optimization;

namespace Myware.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/js")
                       .Include("~/Scripts/angular/*.js")
                       .Include("~/Scripts/angular/extra/*.js")
                       .Include("~/Scripts/angular-ui/*.js")
                       .Include("~/Scripts/jaydata/extra/*.js")
                       .Include("~/Scripts/jaydata/*.js")
                       .Include("~/Scripts/jaydatamodules/*.js")
                       .Include("~/Scripts/jaydataproviders/*.js")
                       .Include("~/Scripts/app/*.js")
                       );
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-theme.css")
                .Include("~/Content/ng-grid.css")
                .Include("~/Content/Site.css")
            );
            
            

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
