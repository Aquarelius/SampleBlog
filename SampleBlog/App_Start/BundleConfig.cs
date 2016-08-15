using System.Web;
using System.Web.Optimization;

namespace SampleBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular/angular.min.js",
                "~/Scripts/angular-animate/angular-animate.min.js",
                "~/Scripts/angular-aria/angular-aria.min.js",
                "~/Scripts/angular-messages/angular-messages.min.js",
                "~/Scripts/angular-material/angular-material.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/blog").Include(
                "~/Scripts/Blog/BlogApp.js",
                "~/Scripts/Blog/BlogApp.Comments.js",
                "~/Scripts/Blog/BlogApp.Admin.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                "~/Scripts/tinymce/tinymce.min.js"));

        

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Scripts/angular-material/angular-material.min.css",
                      "~/Scripts/angular-material/layouts/angular-material.layouts.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
