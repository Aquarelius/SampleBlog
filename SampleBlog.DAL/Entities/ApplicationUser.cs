using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SB.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<BlogPost> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } 
    }
}
