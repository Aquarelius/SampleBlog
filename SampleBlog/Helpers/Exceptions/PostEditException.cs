using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SB.Helpers.Exceptions
{
    public class PostEditException : Exception
    {

        public PostEditException(string message)
        {
            this.ErrorMessage = message;
        }

        public string ErrorMessage { get; set; }
        
    }
}