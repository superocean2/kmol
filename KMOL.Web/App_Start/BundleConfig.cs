using System.Web;
using System.Web.Optimization;

namespace KMOL.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/contents/jquery-3.3.1.min.js"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                      "~/contents/jquery-3.1.1.min.js",
                      "~/contents/bootstrap/js/bootstrap.min.js",
                      "~/contents/jsrender.min.js",
                      "~/contents/jquery.lazyload.min.js",
                      "~/contents/script.js"
                      ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/contents/bootstrap/css/bootstrap.min.css",
                      "~/contents/style.css"));
        }
    }
}
