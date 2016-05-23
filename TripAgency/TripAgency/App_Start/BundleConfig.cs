using System.Web;
using System.Web.Optimization;

namespace TripAgency
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/clientcss").Include(
                      "~/Content/ctt041ca/bootstrap.css",
                      "~/Content/ctt041ca/font-awesome.css",
                      "~/Content/ctt041ca/icomoon.css",
                      "~/Content/ctt041ca/styles.css",
                      "~/Content/ctt041ca/mystyles.css",
                      "~/Content/ctt041ca/switcher.css"));

            bundles.Add(new ScriptBundle("~/bundles/clientjs").Include(
                      "~/Scripts/js/jquery.js",
                      "~/Scripts/js/bootstrap.js",
                      "~/Scripts/js/slimmenu.js",
                      "~/Scripts/js/bootstrap-datepicker.js",
                      "~/Scripts/js/bootstrap-timepicker.js",
                      "~/Scripts/js/nicescroll.js",
                      "~/Scripts/js/dropit.js",
                      "~/Scripts/js/ionrangeslider.js",
                      "~/Scripts/js/icheck.js",
                      "~/Scripts/js/fotorama.js",
                      "~/Scripts/js/typeahead.js",
                      "~/Scripts/js/card-payment.js",
                      "~/Scripts/js/magnific.js",
                      "~/Scripts/js/owl-carousel.js",
                      "~/Scripts/js/fitvids.js",
                      "~/Scripts/js/tweet.js",
                      "~/Scripts/js/countdown.js",
                      "~/Scripts/js/gridrotator.js",
                      "~/Scripts/js/custom.js",
                      "~/Scripts/js/switcher.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryCost").Include(
                     "~/Scripts/jquery-1.10.2.js",
                     "~/Scripts/metronic.js",
                     "~/Scripts/jquery-1.11.0.min.js",
                     "~/Scripts/jquery.blockui.min.js"));

            bundles.Add(new StyleBundle("~/Content/pagedlist").Include(
                      "~/Content/PagedList.css"));
            bundles.Add(new ScriptBundle("~/bundles/new").Include(
                     "~/Scripts/jquery-1.js",
                     "~/Scripts/valocd.js"));
            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                     "~/Scripts/angular.min.js"));
        }
    }
}
