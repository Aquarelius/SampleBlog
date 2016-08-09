using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SampleBlog
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "comments_for_post",
               routeTemplate: "api/post/{postId}/comments",
               defaults: new { controller = "Comments", action = "GetForPost" }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
