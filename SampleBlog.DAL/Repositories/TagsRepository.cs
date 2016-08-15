using System.Collections.Generic;
using System.Linq;
using SB.DAL.Entities;

namespace SB.DAL.Repositories
{
    public class TagsRepository : IRepository<Tag>
    {

        private readonly IDataSource _context;

        public TagsRepository(IDataSource context)
        {
            _context = context;
        }

        public IEnumerable<Tag> List
        {
            get { return _context.Tags; }
        }

        public IEnumerable<Tag> TagsForPost(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            return _context.Tags.Where(t => t.Posts.Contains(post));
        }


        public void Add(Tag entity)
        {
            _context.Tags.Add(entity);
        }

        public void Delete(Tag entity)
        {
            _context.Tags.Remove(entity);
        }

        public void Update(Tag entity)
        {
            _context.Tags.Attach(entity);
            _context.MarkForUpdate(entity);
        }

        public Tag FindById(object id)
        {
            var key = (int) id;
            return _context.Tags.FirstOrDefault(t => t.Id == key);
        }


        public void Delete(ICollection<Tag> entities)
        {
            foreach (var tag in entities)
            {
                _context.Tags.Remove(tag);
            }
        }
    }
}
