using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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
        private readonly TagsService _tagsService;
        private readonly CommentsService _commentsService;
        public HomeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userService = new UsersService(_unitOfWork);
            _postService = new PostsService(_unitOfWork);
            _commentsService = new CommentsService(_unitOfWork);
            _tagsService = new TagsService(_unitOfWork);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET: /PrivateArea/Home/
        public ActionResult Index(int page = 1)
        {
            var uId = User.Identity.GetUserId();

            int total;
            var posts = _postService.GetPostsForUserDashboard(uId, page, SettingsHelper.PageSize, out total);
            var model = new DashboardViewModel
            {
                Posts = posts,
                Page = page,
                PagesCount = (int)Math.Ceiling((double)total / SettingsHelper.PageSize),
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
                model.Tags = JsonConvert.SerializeObject(post.Tags.Select(t => t.Label));
            }

            return View(model);
        }

        [HttpPost]
        [HandleError(ExceptionType = typeof(PostEditException), View = "Error", Order = 1)]
        [PostGuardFilter(Order = 2)]
        public ActionResult Edit(EditPostViewModel model)
        {
            if (!ModelState.IsValid) return View();

            var itm = _postService.GetPost(model.Id) ?? new BlogPost { Tags = new List<Tag>() };
            var user = _userService.GetUser(User.Identity.GetUserId());
            itm.Author = user;
            itm.Title = model.Title;
            itm.Text = model.Text;

            itm.Tags.Clear();
            foreach (var tag in JsonConvert.DeserializeObject<string[]>(model.Tags))
            {
                if (tag.Trim().Length > 0) itm.Tags.Add(_tagsService.GetOrAdd(tag.Trim()));
            }

            _postService.CreateOrUpdatePost(itm);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HandleError(ExceptionType = typeof(PostEditException), View = "Error", Order = 1)]
        [PostGuardFilter(Order = 2)]
        public ActionResult Publish(int id, bool isDraft)
        {

            var post = _postService.GetPost(id);

            post.IsDraft = isDraft;
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
            catch (Exception ex)
            {
                throw new PostEditException("Error during deleting post");
            }

            return RedirectToAction("Index");
        }
    }
}