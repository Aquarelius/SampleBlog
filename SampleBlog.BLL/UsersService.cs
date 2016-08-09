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

        
    }
}
