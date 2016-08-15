using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SB.DAL.Entities;

namespace SB.Models
{
    public class PostsListViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; set; }

        public List<BlogPost> Posts { get; set; } 
    }
}