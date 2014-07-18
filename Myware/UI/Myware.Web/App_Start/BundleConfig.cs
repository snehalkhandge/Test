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
                    .Include("~/Scripts/jquery/*.js")
                    .Include("~/Scripts/jquery/extra/*.js")
                    .Include("~/Scripts/angular/*.js")
                    .Include("~/Scripts/angular/extra/*.js")
                    .Include("~/Scripts/angular-ui/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app/*.js")
                .Include("~/app/blocks/*.js")
                .Include("~/app/blocks/exception/*.js")
                .Include("~/app/blocks/logger/*.js")
                .Include("~/app/blocks/router/*.js")
                .Include("~/app/core/*.js")
                .Include("~/app/core/components/*.js")
                .Include("~/app/account/*.js")
                .Include("~/app/account/components/*.js")
                .Include("~/app/layout/*.js")
                .Include("~/app/layout/components/*.js")
                .Include("~/app/widgets/*.js")
                .Include("~/app/widgets/sidebar/*.js")
                .Include("~/app/widgets/spinner/*.js")
                .Include("~/app/widgets/widget/*.js")
                .Include("~/app/avengers/*.js")
                .Include("~/app/dashboard/*.js")
                .Include("~/app/dashboard/components/*.js")
                .Include("~/app/usermanagement/*.js")
                .Include("~/app/usermanagement/users/*.js")
                .Include("~/app/usermanagement/roles/*.js")
                .Include("~/app/usermanagement/permissions/*.js")
                .Include("~/app/presalesunit/*.js")
                .Include("~/app/presalesunit/customertypes/*.js")
                .Include("~/app/presalesunit/contactstatustypes/*.js")
                .Include("~/app/presalesunit/facingtypes/*.js")
                .Include("~/app/presalesunit/unittypes/*.js")
                .Include("~/app/presalesunit/locations/*.js")
                .Include("~/app/presalesunit/locality/*.js")
                .Include("~/app/presalesunit/sources/*.js")
                .Include("~/app/presalesunit/company/*.js")
                .Include("~/app/presalesunit/developers/*.js")
                .Include("~/app/presalesunit/brokers/*.js")
                
                );


            bundles.Add(new StyleBundle("~/Content/bootstrap")
                        .Include("~/Content/bootstrap/*.css")
                        .Include("~/Content/bootstrap-theme/*.css")
                        .Include("~/Content/styles.css")
                        .Include("~/Content/toastr.css")
                        .Include("~/Content/Site.css")
                    );


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
