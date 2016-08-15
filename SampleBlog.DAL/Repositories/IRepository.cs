using System.Collections.Generic;

namespace SB.DAL.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> List { get; }
        void Add(T entity);
        void Delete(T entity);

        void Delete(ICollection<T> entities);
        void Update(T entity);
        T FindById(object id);
    }
}
