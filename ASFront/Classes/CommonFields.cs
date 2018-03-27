using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;


namespace ASFront.Classes
{
    public static class CommonFields
    {

        public static CultureInfo[] Cultures = {
                    new CultureInfo("en-US"),
            new CultureInfo("hy-AM"),
            new CultureInfo("en-US"),
            new CultureInfo("ru-RU"),
            new CultureInfo("de-DE") };

        public static List<string> ButtonClassesList = new List<string>
    {       "btn-default",
            "btn-success",
            "btn-danger",
            "btn-warning"
    };

    }



}