using System.Web;
using System.Web.Optimization;

namespace GOB.SPF.ConecII.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js", "~/Scripts/jquery/jquery.json-1.0-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/respond.js",
                      "~/Scripts/geass/geass.js",
                      "~/Scripts/conec/common/main.js", 
                      "~/Scripts/controls/gobControl.js",
                      "~/Scripts/controls/gobGrid.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/bootstrap/site.css",
                      "~/Content/conec/common/header.css",
                      "~/Content/conec/common/skinConec.css",
                      "~/Content/controls/gobgrid.css"));
        }
    }
}
