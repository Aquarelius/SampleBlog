using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using SB.DAL.Entities;
using SB.DAL.Repositories;

namespace SB.BLL
{
    public class UsersService
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser GetUser(string id)
        {
            return _unitOfWork.UsersRepositry.FindById(id);
        }

        public List<ApplicationUser> GetList()
        {
            return _unitOfWork.UsersRepositry.List.ToList();
        }

        public List<IdentityRole> GetAllRoles()
        {
            return _unitOfWork.RolesRepository.List.ToList();
        }

        public void AddToRole(string userId, string role)
        {
            var r = _unitOfWork.RolesRepository.FindByName(role);
            var u = _unitOfWork.UsersRepositry.FindById(userId);
            if (u.Roles.All(z => z.RoleId != r.Id))
            {
                u.Roles.Add(new IdentityUserRole
                {
                    RoleId = r.Id,
                    UserId = r.Id
                });
            }
        }
        public void RemoveFromRole(string userId, string role)
        {
            var r = _unitOfWork.RolesRepository.FindByName(role);
            var u = _unitOfWork.UsersRepositry.FindById(userId);
            if (u.Roles.Any(z => z.RoleId == r.Id))
            {
                var ur = u.Roles.FirstOrDefault(z => z.RoleId == r.Id);
                u.Roles.Remove(ur);
            }
        }

        public void Delete(string id)
        {
            var user = _unitOfWork.UsersRepositry.FindById(id);
            _unitOfWork.CommentsRepository.Delete(user.Comments);
            while (user.Posts.Count > 0)
            {
                var post = user.Posts.First();
                _unitOfWork.CommentsRepository.Delete(post.Comments);
                _unitOfWork.PostsRepository.Delete(post);
            }
            _unitOfWork.UsersRepositry.Delete(user);
        }
    }
}
