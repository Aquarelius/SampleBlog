using System.Web;
using System.Web.Optimization;

namespace SampleBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "http://ajax.googleapis.com/ajax/libs/angularjs/1.5.3/angular.min.js",
                "http://ajax.googleapis.com/ajax/libs/angularjs/1.5.3/angular-animate.min.js",
                "http://ajax.googleapis.com/ajax/libs/angularjs/1.5.3/angular-aria.min.js",
                "http://ajax.googleapis.com/ajax/libs/angularjs/1.5.3/angular-messages.min.js",
                "http://ajax.googleapis.com/ajax/libs/angular_material/1.1.0-rc2/angular-material.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/blog").Include(

                "~/Scripts/Blog/BlogApp.js",
                "~/Scripts/Blog/BlogApp.Comments.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/angular-material/angular-material.min.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
