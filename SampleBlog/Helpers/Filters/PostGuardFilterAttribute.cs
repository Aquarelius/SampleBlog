using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Ninject;
using SB.App_Start;
using SB.Areas.PrivateArea.Models;
using SB.BLL;
using SB.DAL;
using SB.DAL.Repositories;
using SB.Helpers.Exceptions;

namespace SB.Helpers.Filters
{
    public class PostGuardFilterAttribute : ActionFilterAttribute
    {
        private readonly UnitOfWork _unitOfWork;

        public PostGuardFilterAttribute()
        {
            var ds = NinjectWebCommon.bootstrapper.Kernel.Get<IDataSource>();
            _unitOfWork = new UnitOfWork(ds);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var message = "";
            var postService = new PostsService(_unitOfWork);
            var action = filterContext.ActionDescriptor.ActionName.ToLower();
            var id = -1;
            if (filterContext.ActionParameters.ContainsKey("id"))
                id = (int)filterContext.ActionParameters["id"];

            if (filterContext.ActionParameters.ContainsKey("model"))
                id = ((EditPostViewModel)filterContext.ActionParameters["model"]).Id;

            if (id == 0 && action == "edit") //New post
            {
                message = "";
            }
            else
            {
                var post = postService.GetPost(id);
                if (post == null) //check post exists
                {
                    message = "Unknown post";
                }
                else
                {

                    var isAdmin = HttpContext.Current.User.IsInRole("Admin");
                    if (!isAdmin)
                    {
                        var isAuthor = post.Author.Id == HttpContext.Current.User.Identity.GetUserId();
                        if (action == "delete" && !isAuthor)
                        {
                            message = "Only author or administrator can delete post";
                        }
                        if (action == "edit" && !isAuthor)
                        {
                            message = "Only author or administrator can edit post";
                        }
                    }
                }
            }

            if (message == "")
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                throw new PostEditException(message);
            }
        }
    }

}