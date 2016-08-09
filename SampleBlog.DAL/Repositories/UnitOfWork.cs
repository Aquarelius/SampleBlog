
using System;

namespace SB.DAL.Repositories
{
    public class UnitOfWork: IDisposable
    {
        private readonly IDataSource _context;
        private PostsRepository _postRepo;
        private TagsRepository _tagsRepo;
        private CommentsRepository _commentsRepo;
        private UsersRepositry _usersRepositry;
        public UnitOfWork(IDataSource context) //DI
        {
            _context = context;
        }

        public PostsRepository PostsRepository
        {
            get { return _postRepo ?? (_postRepo = new PostsRepository(_context)); }
        }

        public TagsRepository TagsRepository
        {
            get { return _tagsRepo ?? (_tagsRepo = new TagsRepository(_context)); }
        }

        public CommentsRepository CommentsRepository
        {
            get { return _commentsRepo ?? (_commentsRepo = new CommentsRepository(_context)); }
        }


        public UsersRepositry UsersRepositry
        {
            get { return _usersRepositry ?? (_usersRepositry = new UsersRepositry(_context)); }
        }
        public void Save()
        {
            _context.Save();
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            GC.SuppressFinalize(this);
        }
    }
}
