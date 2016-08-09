using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using SB.DAL.Entities;
using SB.DAL.Migrations;

namespace SB.DAL
{

    public class DataContextFactory : IDbContextFactory<DataContext>
    {
        private static DataContextFactory _instance;

        public static DataContextFactory Instance //Singletone
        {
            get
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
                if (_instance == null) _instance = new DataContextFactory();
                return _instance;
            }
        }

        private DataContextFactory() { }
        public DataContext Create()
        {
            return new DataContext();
        }
    }
    public class DataContext : IdentityDbContext<ApplicationUser>, IDataSource
    {
        public DataContext() : base("BlogCS") { }

        public IDbSet<BlogPost> Posts { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<Comment> Comments { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
               .HasMany<Tag>(s => s.Tags)
               .WithMany(c => c.Posts)
               .Map(m =>
               {
                   m.MapLeftKey("PostId");
                   m.MapRightKey("TagId");
                   m.ToTable("PostsTags");
               });

            modelBuilder.Entity<Comment>()
                .HasRequired<BlogPost>(c => c.Post)
                .WithMany(p => p.Comments)
                .Map(m => m.MapKey("PostId"));

            modelBuilder.Entity<BlogPost>()
                .HasRequired<ApplicationUser>(p => p.Author)
                .WithMany(u => u.Posts)
                .Map(m => m.MapKey("AuthorId"));

            modelBuilder.Entity<Comment>()
                .HasRequired<ApplicationUser>(c => c.Author)
                .WithMany(u => u.Comments)
                .Map(m => m.MapKey("AuthorId"));


            base.OnModelCreating(modelBuilder);
        }


        public void Save()
        {
            this.SaveChanges();
        }

        public void MarkForUpdate(object entity)
        {
            this.Entry(entity).State = EntityState.Modified;

        }



    }
}
