using System.Web.Mvc;

namespace SB.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
             "Admin_api_users",
             "Admin/api/users",
             new { action = "GetUsers", controller ="Users" },
              new[] { "SB.Areas.Admin.Controllers" }
         );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                 new[] { "SB.Areas.Admin.Controllers" }
            );
        }
    }
}