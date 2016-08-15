using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SB.DAL.Entities;

namespace SB.Areas.PrivateArea.Models
{
    public class DashboardViewModel
    {
        public List<BlogPost> Posts { get; set; }

        public List<Comment> LatestComments { get; set; }

        public int PagesCount { get; set; }

        public int Page { get; set; }
    }
}