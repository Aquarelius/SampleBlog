using System.Linq;
using System.Web.Mvc;
using SB.BLL;
using SB.DAL;
using SB.DAL.Repositories;
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
            _postService= new PostsService(_unitOfWork);
            _usersService = new UsersService(_unitOfWork);
            _tagsService = new TagsService(_unitOfWork);
        }
        public ActionResult Index()
        {
           
            var model = new PostsListViewModel
            {
                Page = 1,
                PageSize = 10,
                Posts = _postService.GetPostsForPage(1,10)
            };

            return View(model);
        }

        public ActionResult List(int page)
        {
            
            var model = new PostsListViewModel
            {
                Page = page,
                PageSize = 10,
                Posts = _postService.GetPostsForPage(1, 10)
            };

            return View("Index",model);
        }

        public ActionResult AuthorList(string authorId)
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
                Page = 1,
                PageSize = 10,
                Posts = user.Posts.ToList()
            };
            ViewData["userName"] = user.UserName;
            return View(model);
        }

        public ActionResult TagList(int id)
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
                PageSize = 10,
                Posts = tag.Posts.ToList()
            };
            ViewData["Tag"] = tag.Label;
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