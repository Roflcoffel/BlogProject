﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MovieStore.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/umd/popper.min.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap-journal.css",
                        "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/ckeditor").Include(
                        "~/Scripts/ckeditor/ckeditor.js"));


            bundles.Add(new ScriptBundle("~/bundles/Scripts/adapters").Include(
                       "~/Scripts/ckeditor/adapters/jquery.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/initialize").Include(
                      "~/Scripts/jquery.initialize.js"
           ));

        }

    }
}