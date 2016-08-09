using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SB.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Roles.AddOrUpdate(r=>r.Id,
                new IdentityRole("Admin"){Id = "1"},
                new IdentityRole("Writer"){Id = "2"},
                new IdentityRole("CommentsOnly") { Id = "3" });
        }
    }
}
