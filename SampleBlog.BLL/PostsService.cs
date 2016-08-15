using System;
using System.Collections.Generic;
using System.Linq;
using SB.DAL.Entities;
using SB.DAL.Repositories;

namespace SB.BLL
{
    public class PostsService
    {
        private readonly UnitOfWork _unitOfWork;

        public PostsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BlogPost> GetPostsForPage(int pageNum, int pageSize)
        {
            return _unitOfWork.PostsRepository.List
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalPostsCount()
        {
            return _unitOfWork.PostsRepository.List.Count(p => !p.IsDraft);
        }

        public List<BlogPost> GetPostsForUserDashboard(string userId, int page, int pageSize, out int total)
        {

            total = _unitOfWork.PostsRepository.List.Count(p => p.Author.Id == userId);
            return _unitOfWork.PostsRepository.GetPostsForAuthor(userId, true)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public BlogPost GetPost(int id)
        {
            return _unitOfWork.PostsRepository.FindById(id);
        }

        public bool CreateOrUpdatePost(BlogPost entity)
        {
            bool res = false;
            try
            {
                entity.Updated = DateTime.UtcNow;
                if (entity.Id == 0)
                {
                    entity.IsDraft = true;
                    entity.Created = DateTime.UtcNow;
                    _unitOfWork.PostsRepository.Add(entity);
                }
                else
                {
                    _unitOfWork.PostsRepository.Update(entity);
                }
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return res;
        }

        public bool Delete(int id)
        {
            var post = GetPost(id);
            if (post == null) return false;
            return Delete(post);
        }
        public bool Delete(BlogPost enity)
        {
            var res = false;
            try
            {
                _unitOfWork.CommentsRepository.Delete(enity.Comments);
                _unitOfWork.PostsRepository.Delete(enity);
                res = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return res;
        }
    }
}
