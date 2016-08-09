using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SB.BLL;
using SB.DAL.Entities;
using SB.DAL.Repositories;
using SB.Models;

namespace SB.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly CommentsService _service;
        private readonly UsersService _usersService;
        private readonly PostsService _postsService;
        public CommentsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _service = new CommentsService(_unitOfWork);
            _usersService = new UsersService(_unitOfWork);
            _postsService = new PostsService(_unitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET api/post/5/comments
        public IEnumerable<CommentViewModel> GetForPost(int postId)
        {

            return _service.GetCommentsForPost(postId).Select(c => new CommentViewModel(c));
        }

        // GET api/comments/5
        public CommentViewModel Get(int id)
        {
            return new CommentViewModel(_service.GetComment(id));
        }

        // POST api/comments
        [Authorize]
        public CommentViewModel Post([FromBody]CommentViewModel model)
        {
            var user = _usersService.GetUser(User.Identity.GetUserId());
            var post = _postsService.GetPost(model.PostId);
            var c = new Comment
            {
                Added = DateTime.UtcNow,
                Author = user,
                Post = post,
                Text = model.Text,
                ParentCommentId = model.ParentCommentId
            };
            _service.AddComment(c);
            _unitOfWork.Save();
            return new CommentViewModel(c);
        }


        // DELETE api/comments/5
        [Authorize]
        public ICollection<int> Delete(int id)
        {
            var ids = _service.Delete(id);
            _unitOfWork.Save();
            return ids;
        }
    }
}
