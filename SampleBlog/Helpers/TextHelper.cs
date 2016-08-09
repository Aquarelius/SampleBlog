using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SB.Helpers
{
    public class TextHelper
    {
        public static string TruncText(string data)
        {
            var sign = (data.Length > 100) ? " ..." : "";
            return data.Substring(0, data.Length > 100 ? 100 : data.Length) + sign;
        }
    }
}