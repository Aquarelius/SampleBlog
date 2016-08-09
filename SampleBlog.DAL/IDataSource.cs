using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SB.DAL.Entities;

namespace SB.DAL
{
    public interface IDataSource
    {
        IDbSet<BlogPost> Posts { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<ApplicationUser> Users { get; }

        IDbSet<IdentityRole> Roles { get; }

        void Save();

        void MarkForUpdate(object entity);

        void Dispose();
    }
}
