using System.Web.Optimization;

namespace GoldenCityShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/admin/js").Include(
                "~/Scripts/admin.js",
                "~/Scripts/site.js",
                "~/Scripts/lazysizes.min.js",
                "~/Scripts/jquery-MVC-RemoveRow.js",
                 "~/Scripts/sweet-alert.min.js",
                "~/Scripts/json2.js",
                "~/Scripts/plugins/metisMenu/metisMenu.min.js",
               "~/Scripts/sb-admin-2.js",
                "~/Scripts/site.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/slideshowMini.js",
                "~/Scripts/star-rating.min.js",
                "~/Scripts/jquery.sliderPro.min.js",
                "~/Scripts/bootstrap-select.min.js",
                "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
                "~/Scripts/noty/jquery.noty.js",
                "~/Scripts/lazysizes.min.js",
                "~/Scripts/respond.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/masterjs").Include(
                "~/Scripts/sweet-alert.min.js",
                "~/Scripts/site.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/slideshowMini.js",
                "~/Scripts/jquery-MVC-RemoveRow.js",
                 "~/Scripts/sweet-alert.min.js",
                "~/Scripts/jquery.autocomplete.min.js",
                "~/Scripts/cloud-zoom.js",
                "~/Scripts/star-rating.min.js",
                "~/Scripts/turbolinks.min.js",
                "~/Scripts/jquery.sliderPro.min.js",
                "~/Scripts/bootstrap-select.min.js",
                "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
                "~/Scripts/lazysizes.min.js",
                "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/fileinp/css").Include(
               "~/Content/fileinput.min.css"
               ));
            bundles.Add(new ScriptBundle("~/fileinp/js").Include(
               "~/Scripts/fileinput.min.js"));

            bundles.Add(new ScriptBundle("~/customerBundle/js").Include(
                "~/Scripts/starRating-plugin.js",
                "~/Scripts/AddToCart-plugin.js",
                "~/Scripts/AddToWishList-plugin.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/AddToCompareList-plugin.js",
                "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
                "~/Scripts/customer-actions.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/master.css",
                "~/Content/slideshow.css",
                "~/Content/mycss.css",
                "~/Content/cloud-zoom.css",
                "~/Content/slider-pro.min.css",
                "~/Content/star-rating.min.css",
                "~/Content/jquery.autocomplete.css",
                "~/Content/bootstrap-select.min.css",
                 "~/Content/font-awesome.min.css",
                 "~/Content/product.css",
                 "~/Content/animate.min.css",
                "~/Content/sweet-alert.css"));

            bundles.Add(new StyleBundle("~/Search/css").Include(
                "~/Content/search.css"));

            bundles.Add(new StyleBundle("~/adminContent/css").Include(
               "~/Content/bootstrap.min.css",
               "~/Content/mycss.css",
               "~/Content/bootstrap-select.min.css",
                "~/Content/sweet-alert.css",
                "~/Content/animate.min.css",
                "~/Content/plugins/metisMenu/metisMenu.min.css",
                "~/Content/plugins/timeline.css",
                "~/Content/sb-admin-2.css",
                "~/Content/plugins/morris.css",
                "~/Content/font-awesome.min.css"
                ));


            bundles.Add(new StyleBundle("~/editor/css").Include(
             "~/Scripts/ckeditor/contents.css"
             ));
            bundles.Add(new ScriptBundle("~/editor/js").Include(
               "~/Scripts/ckeditor/ckeditor.js"));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862



            BundleTable.EnableOptimizations = true;
        }
    }
}
