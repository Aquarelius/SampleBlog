using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SB.Helpers
{
    public class TextHelper
    {
        public static string TruncText(string data, int length = 300)
        {
            var sign = (data.Length > length) ? " ..." : "";
            return data.Substring(0, data.Length > length ? length : data.Length) + sign;
        }

        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}