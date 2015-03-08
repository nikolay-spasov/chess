using System.Web;
using System.Web.Optimization;

namespace Chess.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/lib").Include(
                "~/Scripts/jquery-1.10.2.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/loading-bar.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/spa").Include(
                "~/App/app.js",
                "~/App/directives/*.js",
                "~/App/services/*.js",
                "~/App/interceptors/*.js",
                "~/App/controllers/*.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/loading-bar.css",
                "~/Content/Site.css"
            ));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
