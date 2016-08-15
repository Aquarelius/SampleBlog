using System.Collections.Generic;
using System.Linq;
using SB.DAL.Entities;
using SB.DAL.Repositories;

namespace SB.BLL
{
    public class CommentsService
    {
        private UnitOfWork _unitOfWork;

        public CommentsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Comment> GetTopLatestComments(string userId, int count = 10)
        {
            return _unitOfWork.CommentsRepository.List
                .Where(c => c.Post.Author.Id == userId)
                .OrderByDescending(c => c.Added)
                .Take(count)
                .ToList();
        }

        public List<Comment> GetCommentsForPost(int postId)
        {
            return _unitOfWork.CommentsRepository.GetCommentsForPost(postId).ToList();
        }

        public Comment GetComment(int id)
        {
            return _unitOfWork.CommentsRepository.FindById(id);
        }

        public void AddComment(Comment data)
        {
            _unitOfWork.CommentsRepository.Add(data);
        }

        /// <summary>
        /// Delete comment with all childs
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <returns>List of Ids of all deleted comments</returns>
        public List<int> Delete(int id)
        {
            var res = new List<int>();
            var comment = _unitOfWork.CommentsRepository.FindById(id);
            if (comment != null)
            {
                var lst = new List<Comment> {comment};
                lst.AddRange(GetChilds(comment));
                _unitOfWork.CommentsRepository.Delete(lst);
                res = lst.Select(c => c.Id).ToList();
            }
            return res;
        }

        private List<Comment> GetChilds(Comment data)
        {
            var res = new List<Comment>();
            var lst = _unitOfWork.CommentsRepository.List.Where(c => c.ParentCommentId == data.Id).ToList();
            foreach (var comment in lst)
            {
                res.Add(comment);
                res.AddRange(GetChilds(comment));
            }
            return res;
        } 
    }
}
