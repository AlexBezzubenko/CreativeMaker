using System.Web;
using System.Web.Optimization;
using Course.Helpers;

namespace Course
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
              "~/Content/themes/base/core.css",
              "~/Content/themes/base/resizable.css",
              "~/Content/themes/base/selectable.css",
              "~/Content/themes/base/accordion.css",
              "~/Content/themes/base/autocomplete.css",
              "~/Content/themes/base/button.css",
              "~/Content/themes/base/dialog.css",
              "~/Content/themes/base/slider.css",
              "~/Content/themes/base/jquery-ui.css",
              "~/Content/themes/base/tabs.css",
              "~/Content/themes/base/datepicker.css",
              "~/Content/themes/base/progressbar.css",
              "~/Content/themes/base/theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/rateyo").Include(
                    "~/Scripts/jquery.rateyo.js"));
            bundles.Add(new StyleBundle("~/Content/rateyo").Include(
                     "~/Content/Libs/jquery.rateyo.css"));

            bundles.Add(new StyleBundle("~/Content/hint").Include(
                     "~/Content/Libs/hint.min.css"));

            foreach (var theme in Bootstrap.Themes)
            {
                var stylePath = string.Format("~/Content/Themes/{0}/bootstrap.css", theme);
                var siteStylePath = string.Format("~/Content/Themes/{0}/Site.css", theme);

                bundles.Add(new StyleBundle(Bootstrap.Bundle(theme)).Include(
                        stylePath,
                        "~/Content/Libs/bootstrap-social.css",
                        "~/Content/Libs/font-awesome.css",
                        siteStylePath));
            }
        }
    }
}
