using System.Web.Mvc;
using System.Web.Routing;

namespace SampleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                        "PublicList",
                        "{page}",
                        new { controller = "Home", action = "List" },
                        new { page = @"\d+" },
                         new[] { "SB.Controllers" }
                   );

            routes.MapRoute(
                      "PostView",
                      "Post/{id}",
                      new { controller = "Home", action = "PostView" },
                    new[] { "SB.Controllers" }
                 );

            routes.MapRoute(
                    "AuthorList",
                    "Author/{authorId}/{page}",
                    new { controller = "Home", action = "AuthorList", page = UrlParameter.Optional },
                  new[] { "SB.Controllers" }
               );

            routes.MapRoute(
                   "TagList",
                   "Tag/{id}/{page}",
                   new { controller = "Home", action = "TagList", page = UrlParameter.Optional },
                 new[] { "SB.Controllers" }
              );

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "SB.Controllers" }
            );

        }
    }
}
