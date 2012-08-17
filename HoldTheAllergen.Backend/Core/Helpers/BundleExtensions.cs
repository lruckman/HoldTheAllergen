using System.Web.Optimization;

namespace HoldTheAllergen.Backend.Core.Helpers
{
    public static class BundleExtensions
    {
        public static void EnableBootstrapBundle(this BundleCollection bundles)
        {
            var bootstrapCss = new Bundle("~/content/css", new CssMinify());
            bootstrapCss.AddFile("~/content/bootstrap.css");
            bootstrapCss.AddFile("~/content/bootstrap-responsive.css");
            bootstrapCss.AddFile("~/content/site.css");

            bundles.Add(bootstrapCss);

            var bootstrapJs = new Bundle("~/js", new JsMinify());
            bootstrapJs.AddFile("~/scripts/jquery-1.7.1.min.js");
            bootstrapJs.AddFile("~/scripts/jquery.validate.min.js");
            bootstrapJs.AddFile("~/scripts/jquery.validate.unobtrusive.min.js");
            bootstrapJs.AddFile("~/scripts/bootstrap.js");

            bundles.Add(bootstrapJs);
        }
    }
}