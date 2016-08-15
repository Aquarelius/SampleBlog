using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SB.DAL.Entities;

namespace SB.DAL.Repositories
{
    public class PostsRepository : IRepository<BlogPost>
    {
        private readonly IDataSource _context;

        public PostsRepository(IDataSource context) //DI
        {
            _context = context;
        }

        public IEnumerable<BlogPost> List
        {
            get
            {
                return _context.Posts
                    .Where(p => !p.IsDraft)
                    .Include(p=>p.Author)
                    .OrderByDescending(p => p.Updated);
            }
        }

        public IEnumerable<BlogPost> GetPostsForAuthor(string authorId, bool includeDrafts = false)
        {
            return _context.Posts
                .Where(p => p.Author.Id == authorId && (includeDrafts || !p.IsDraft))
                .OrderByDescending(p => p.Updated);

        }

        public void Add(BlogPost entity)
        {
            _context.Posts.Add(entity);
        }

        public void Delete(BlogPost entity)
        {
            _context.Posts.Remove(entity);
        }

        public void Update(BlogPost entity)
        {
            _context.Posts.Attach(entity);
            _context.MarkForUpdate(entity);
        }

        public BlogPost FindById(object id)
        {
            var key = (int)id;
            return _context.Posts.FirstOrDefault(p => p.Id == key);
        }


        public void Delete(ICollection<BlogPost> entities)
        {
            foreach (var post in entities)
            {
                _context.Posts.Remove(post);
            }
        }
    }
}
