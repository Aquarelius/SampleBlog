using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SB.DAL.Repositories
{
    public class RolesRepository:IRepository<IdentityRole>
    {
        private IDataSource _context;

        public RolesRepository(IDataSource context)
        {
            _context = context;
        }

        public IEnumerable<IdentityRole> List
        {
            get { return _context.Roles; }
        }

        public void Add(IdentityRole entity)
        {
            _context.Roles.Add(entity);
        }

        public void Delete(IdentityRole entity)
        {
            _context.Roles.Remove(entity);
        }

        public void Update(IdentityRole entity)
        {
            _context.Roles.Attach(entity);
            _context.MarkForUpdate(entity);
        }

        public IdentityRole FindById(object id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }
        public IdentityRole FindByName(string name)
        {
            return _context.Roles.FirstOrDefault(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) );
        }


        public void Delete(ICollection<IdentityRole> entities)
        {
            foreach (var role in entities)
            {
                _context.Roles.Remove(role);
            }
        }
    }
}
