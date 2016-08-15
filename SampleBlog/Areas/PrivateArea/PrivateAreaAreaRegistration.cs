using System.Web.Mvc;

namespace SB.Areas.PrivateArea
{
    public class PrivateAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PrivateArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "PrivateArea_edit",
               "PrivateArea/Edit/{id}",
               new { controller = "Home", action = "Edit" },
               new[] { "SB.Areas.PrivateArea.Controllers" }
           );

            context.MapRoute(
               "PrivateArea_publish",
               "PrivateArea/Edit/{id}/publish",
               new { controller = "Home", action = "Publish", isDraft = false },
               new[] { "SB.Areas.PrivateArea.Controllers" }
           );

            context.MapRoute(
               "PrivateArea_draft",
               "PrivateArea/Edit/{id}/draft",
               new { controller = "Home", action = "Publish", isDraft = true },
               new[] { "SB.Areas.PrivateArea.Controllers" }
           );

            context.MapRoute(
            "PrivateArea_create",
            "PrivateArea/Create",
            new { controller = "Home", action = "Edit", id = 0 },
               new[] { "SB.Areas.PrivateArea.Controllers" }
        );

            context.MapRoute(
            "PrivateArea_delete",
            "PrivateArea/Delete/{id}",
            new { controller = "Home", action = "Delete" },
            new[] { "SB.Areas.PrivateArea.Controllers" }

        );

            context.MapRoute(
            "PrivateArea_home",
            "PrivateArea/{page}",
            new { controller = "Home", action = "Index", page = UrlParameter.Optional },
           new[] { "SB.Areas.PrivateArea.Controllers" }
        );

            context.MapRoute(
                "PrivateArea_default",
                "PrivateArea/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "SB.Areas.PrivateArea.Controllers" }
            );
        }
    }
}