using System;
using System.Collections.Generic;
using System.Linq;
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

       public Tag GetOrAdd(string label)
       {
           var tag = _unitOfWork.TagsRepository.List.FirstOrDefault(
               z => z.Label.Equals(label, StringComparison.InvariantCultureIgnoreCase));
           if (tag != null) return tag;

           tag = new Tag
           {
               Label = label
           };
           _unitOfWork.TagsRepository.Add(tag);
           return tag;
       }

       public string[] GetAutocompleteSuggetions(string pattern)
       {
           return _unitOfWork.TagsRepository.List
               .Where(t => t.Label.ToLowerInvariant().Contains(pattern.ToLowerInvariant()))
               .Select(t => t.Label)
               .ToArray();
       }

       public List<Tag> GetTopTags(int count = 10)
       {
           return _unitOfWork.TagsRepository.List
               .OrderByDescending(t => t.Posts.Count)
               .Take(count)
               .ToList();
       }
   }
}
