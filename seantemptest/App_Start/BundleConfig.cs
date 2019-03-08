using System.Web;
using System.Web.Optimization;

namespace seantemptest
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/home.js"));
         
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                       "~/Content/css/all.min.css",
                      "~/Content/css/site.css"));
        }
    }
}
