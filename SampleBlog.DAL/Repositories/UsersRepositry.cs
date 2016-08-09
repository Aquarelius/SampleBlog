using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.DAL.Entities;

namespace SB.DAL.Repositories
{
    public class UsersRepositry:IRepository<ApplicationUser>
    {
        private readonly IDataSource _context;

        public UsersRepositry(IDataSource context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> List
        {
            get {return _context.Users; }
        }

        public void Add(ApplicationUser entity)
        {
            _context.Users.Add(entity);
        }

        public void Delete(ApplicationUser entity)
        {
            _context.Users.Remove(entity);
        }

        public void Update(ApplicationUser entity)
        {
            _context.Users.Attach(entity);
            _context.MarkForUpdate(entity);
        }

        public ApplicationUser FindById(object id)
        {
            var key = (string) id;
            return _context.Users.FirstOrDefault(u => u.Id == key);
        }
    }
}
