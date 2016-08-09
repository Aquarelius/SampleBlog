using SB.DAL.Entities;
using SB.DAL.Repositories;

namespace SB.BLL
{
   public class TagsService
   {
       private readonly UnitOfWork _unitOfWork;

       public TagsService(UnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork;
       }

       public Tag GetTag(int id)
       {
           return _unitOfWork.TagsRepository.FindById(id);
       }
   }
}
