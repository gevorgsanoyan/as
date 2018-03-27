using System.Web;
using System.Web.Optimization;

namespace ASFront
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
          "~/Scripts/jquery.validate.min.js"
          ,
          "~/Scripts/jquery.validate.unobtrusive.min.js"
          //,
          //  "~/Scripts/jquery.validate.globalize.min.js"



          ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropdownhover").Include(
          "~/Scripts/bootstrap-dropdownhover.min.js"));

            bundles.Add(new StyleBundle("~/Content/icons").Include(
                      "~/Content/material-icons.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                   "~/Content/Gridmvc.css"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/kendocss").Include(
            //    "~/Content/kendo/2017.3.1026/kendo.common.core.min.css",
            //    "~/Content/kendo/2017.3.1026/kendo.common.min.css",
            //    "~/Content/kendo/2017.3.1026/kendo.common-bootstrap.core.min.css",
            //    "~/Content/kendo/2017.3.1026/kendo.bootstrap.min.css",
            //    "~/Content/kendo/2017.3.1026/kendo.bootstrap.mobile.min.css"

            //     ));


            //            bundles.Add(new ScriptBundle("~/bundles/globalisation").Include(
            //"~/Scripts/globalizeNew.js",
            //"~/Scripts/globalize-cultures/globalize.culture.hy-AM.js"

            //));

            BundleTable.EnableOptimizations = true;
        }
    }
}
