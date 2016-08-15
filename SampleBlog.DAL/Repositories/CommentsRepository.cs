using System;
using System.Collections.Generic;
using System.Linq;
using SB.DAL.Entities;

namespace SB.DAL.Repositories
{
    public class CommentsRepository : IRepository<Comment>
    {
        private readonly IDataSource _context;

        public CommentsRepository(IDataSource context)
        {
            _context = context;
        }
        public IEnumerable<Comment> List
        {
            get { return _context.Comments; }
        }

        public IEnumerable<Comment> GetCommentsForPost(int postId)
        {
            return _context.Comments.Where(c => c.Post.Id == postId).OrderBy(c => c.Added);
        }

        public IEnumerable<Comment> GetCommentsForAutor(string autorId)
        {
            return _context.Comments
                .Where(c => c.Author.Id.Equals(autorId, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(c => c.Added);
        }

        public void Add(Comment entity)
        {
            _context.Comments.Add(entity);
        }

        public void Delete(Comment entity)
        {
            _context.Comments.Remove(entity);

        }

        public void Delete(ICollection<Comment> list)
        {
            foreach (var comment in list)
            {
                Delete(comment);
            }
        }


        public void Update(Comment entity)
        {
            _context.Comments.Attach(entity);
            _context.MarkForUpdate(entity);
        }

        public Comment FindById(object id)
        {
            var key = (int)id;
            return _context.Comments.FirstOrDefault(c => c.Id == key);
        }


    }
}
