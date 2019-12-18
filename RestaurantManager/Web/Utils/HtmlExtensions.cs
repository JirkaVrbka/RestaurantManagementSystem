using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Utils
{
    public static class HtmlExtensions
    {
        public static object DisabledIf(this HtmlHelper html, bool condition, String classes)
        {
            return condition ? (object) new {@class = classes, @disabled = "disabled"} : new {@class = classes};
        }
    }
}