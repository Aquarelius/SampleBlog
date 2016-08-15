using System;
using System.Linq;
using System.Web.Mvc;
using SB.BLL;
using SB.DAL;
using SB.DAL.Repositories;
using SB.Helpers;
using SB.Models;

namespace SB.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostsService _postService;
        private readonly UsersService _usersService;
        private readonly TagsService _tagsService;
        private readonly UnitOfWork _unitOfWork;
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public HomeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _postService = new PostsService(_unitOfWork);
            _usersService = new UsersService(_unitOfWork);
            _tagsService = new TagsService(_unitOfWork);
        }
        public ActionResult Index()
        {

            var model = new PostsListViewModel
            {
                Page = 1,
                PageSize = SettingsHelper.PageSize,
                Posts = _postService.GetPostsForPage(1, SettingsHelper.PageSize),
                PagesCount = (int)Math.Ceiling((double)_postService.GetTotalPostsCount() / SettingsHelper.PageSize)
            };

            return View(model);
        }

        public ActionResult List(int page)
        {

            var model = new PostsListViewModel
            {
                Page = page,
                PageSize = SettingsHelper.PageSize,
                Posts = _postService.GetPostsForPage(page, SettingsHelper.PageSize),
                PagesCount = (int)Math.Ceiling((double)_postService.GetTotalPostsCount() / SettingsHelper.PageSize)
            };

            return View("Index", model);
        }

        public ActionResult AuthorList(string authorId, int page = 1)
        {
            var user = _usersService.GetUser(authorId);
            if (user == null)
            {
                Response.StatusCode = 404;
                Response.StatusDescription = "Author not found";
                Response.End();
                return null;
            }
            var model = new PostsListViewModel
            {
                Page = page,
                PageSize = SettingsHelper.PageSize,
                PagesCount = (int)Math.Ceiling((double)user.Posts.Count / SettingsHelper.PageSize),
                Posts = user.Posts
                .Skip((page - 1) * SettingsHelper.PageSize)
                .Take(SettingsHelper.PageSize)
                .ToList()
            };
            ViewData["userName"] = user.UserName;
            ViewData["userId"] = authorId;
            return View(model);
        }

        public ActionResult TagList(int id, int page = 1)
        {
            var tag = _tagsService.GetTag(id);
            if (tag == null)
            {
                Response.StatusCode = 404;
                Response.StatusDescription = "Tag not found";
                Response.End();
                return null;
            }
            var model = new PostsListViewModel
            {
                Page = 1,
                PageSize = SettingsHelper.PageSize,
                PagesCount = (int)Math.Ceiling((double)tag.Posts.Count / SettingsHelper.PageSize),
                Posts = tag.Posts
                .Skip((page - 1) * SettingsHelper.PageSize)
                .Take(SettingsHelper.PageSize)
                .ToList()
            };
            ViewData["Tag"] = tag.Label;
            ViewData["TagId"] = id;
            return View(model);
        }

        public ActionResult PostView(int id)
        {
            var post = _postService.GetPost(id);
            //TODO: Add 404 check
            return View(post);
        }

    }
}