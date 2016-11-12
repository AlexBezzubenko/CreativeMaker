using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course.Helpers
{
    public class Bootstrap
    {
        public const string BundleBase = "~/Content/css/";

        public class Theme
        {
            public const string Default = "Default";
            public const string Readable = "Readable";
        }

        public static HashSet<string> Themes = new HashSet<string>
    {
        Theme.Default,
        Theme.Readable
    };

        public static string Bundle(string themename)
        {
            return BundleBase + themename;
        }
    }
}