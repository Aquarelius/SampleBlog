using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SB.DAL.Entities;

namespace SB.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        private readonly bool _pendingMigrations;
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            // Check if there are migrations pending to run, this can happen if database doesn't exists or if there was any
            //  change in the schema
            var migrator = new DbMigrator(this);
            _pendingMigrations = migrator.GetPendingMigrations().Any();

            // If there are pending migrations run migrator.Update() to create/update the database then run the Seed() method to populate
            //  the data if necessary
            if (_pendingMigrations)
            {
                migrator.Update();
                Seed(DataContextFactory.Instance.Create());
            }
        }

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            var adminRole = new IdentityRole("Admin") { Id = "1" };
            var writerRole = new IdentityRole("Writer") { Id = "2" };

            context.Roles.AddOrUpdate(r => r.Id, adminRole, writerRole);

            context.SaveChanges();
            var user = context.Users.FirstOrDefault(u => u.UserName == "Admin");
            if (user == null)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var r = manager.Create(new ApplicationUser
                {
                    UserName = "Admin"

                }, "admin123");
                user = context.Users.FirstOrDefault(u => u.UserName == "Admin");

                user.Roles.Add(new IdentityUserRole() { RoleId = "1", UserId = user.Id });
                user.Roles.Add(new IdentityUserRole() { RoleId = "2", UserId = user.Id });

                var tag1 = new Tag { Label = "Hello" };
                var tag2 = new Tag { Label = "Test" };
                context.Tags.AddOrUpdate(t => t.Label, tag1, tag2);
                context.SaveChanges();


                var text = "";
                using (var sr = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/lorem.txt")))
                {
                    text = sr.ReadToEnd();
                }


                context.Posts.AddOrUpdate(p=>p.Title, new BlogPost
                {
                    Author = user,
                    Title = "Hello",
                    Tags = new List<Tag>() { tag1, tag2 },
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsDraft = false,
                    Text = text,
                    Comments = new List<Comment>()
                });

                context.SaveChanges();
            }

        }
    }
}
