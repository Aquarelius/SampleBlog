using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SB.Areas.Admin.Models
{
    public class ChangeUserInRoleModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }

        public bool userInRole { get; set; }
    }
}