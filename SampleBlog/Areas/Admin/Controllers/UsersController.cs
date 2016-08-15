using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SB.Areas.Admin.Models;
using SB.BLL;
using SB.DAL.Repositories;

namespace SB.Areas.Admin.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class UsersController : ApiController
    {
        private readonly UsersService _usersService;

        private readonly UnitOfWork _unitOfWork;


        public UsersController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _usersService = new UsersService(_unitOfWork);
        }

        public List<UserRoleModel> GetUsers()
        {
            var res = new List<UserRoleModel>();
            var lst = _usersService.GetList();
            var roles = _usersService.GetAllRoles();
            var adminRole = roles.FirstOrDefault(r => r.Name == "Admin");
            var writerRole = roles.FirstOrDefault(r => r.Name == "Writer");
            var adminRoleId = adminRole != null ? adminRole.Id : "";
            var writerRoleId = writerRole != null ? writerRole.Id : "";
            foreach (var usr in lst)
            {
                var userRolesIds = usr.Roles.Select(z => z.RoleId).ToList();
                var model = new UserRoleModel
                {
                    Id = usr.Id,
                    Name = usr.UserName,
                    Admin = userRolesIds.Contains(adminRoleId),
                    Writer = userRolesIds.Contains(writerRoleId)
                };
                res.Add(model);
            }
            return res;
        }


        [HttpPost]
        public void ChangeRole(ChangeUserInRoleModel model)
        {
            if (model.userInRole)
            {
                _usersService.AddToRole(model.UserId, model.Role);
            }
            else
            {
                _usersService.RemoveFromRole(model.UserId, model.Role);
            }
            _unitOfWork.Save();
        }

        [HttpDelete]
        public string Delete(string id)
        {
            _usersService.Delete(id);
            _unitOfWork.Save();
            return id;
        }
    }
}
