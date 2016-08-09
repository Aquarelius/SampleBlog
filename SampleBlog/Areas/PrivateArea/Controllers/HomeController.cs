using System;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using SB.Areas.PrivateArea.Models;
using SB.BLL;
using SB.DAL.Entities;
using SB.DAL.Repositories;
using SB.Helpers;
using SB.Helpers.Exceptions;
using SB.Helpers.Filters;

namespace SB.Areas.PrivateArea.Controllers
{
    [Authorize(Roles = "Writer")]
    public class HomeController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        readonly UsersService _userService;
        readonly PostsService _postService;
        private readonly CommentsService _commentsService;
        public HomeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = new UsersService(_unitOfWork);
            _postService = new PostsService(_unitOfWork);
            _commentsService = new CommentsService(_unitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET: /PrivateArea/Home/
        public ActionResult Index()
        {
            var uId = User.Identity.GetUserId();

            var model = new DashboardViewModel
            {
                Posts = _postService.GetPostsForUserDashboard(uId),
                LatestComments = _commentsService.GetTopLatestComments(uId)
            };
            return View(model);
        }

        [HandleError(ExceptionType = typeof(PostEditException), View = "Error", Order = 1)]
        [PostGuardFilter(Order = 2)]
        public ActionResult Edit(int id)
        {

            var post = _postService.GetPost(id);

            var model = new EditPostViewModel();
            if (post != null)
            {
                model.Id = post.Id;
                model.Text = post.Text;
                model.Title = post.Title;
            }

            return View(model);
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(PostEditException), View = "Error", Order = 1)]
        [PostGuardFilter(Order = 2)]
        public ActionResult Edit(EditPostViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var itm = _postService.GetPost(model.Id) ?? new BlogPost();
            var user = _userService.GetUser(User.Identity.GetUserId());
            itm.Author = user;
            itm.Title = model.Title;
            itm.Text = model.Text;
            _postService.CreateOrUpdatePost(itm);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HandleError(ExceptionType = typeof(PostEditException), View = "Error", Order = 1)]
        [PostGuardFilter(Order = 2)]
        public ActionResult Delete(int id)
        {
            var post = _postService.GetPost(id);
            try
            {
                _postService.Delete(post);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new PostEditException("Error during deleting post");
            }

            return RedirectToAction("Index");
        }
    }
}