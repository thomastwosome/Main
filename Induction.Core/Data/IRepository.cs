using System.Linq;

namespace Induction.Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void AddOrUpdate(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
    }
}
