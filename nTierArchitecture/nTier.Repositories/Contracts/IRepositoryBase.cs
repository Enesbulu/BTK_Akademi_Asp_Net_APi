using System.Linq.Expressions;

namespace nTier.Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        //CRUD
        IQueryable<T> FindAll(bool trachChanges);
        IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression, bool trachChanges);
        void Create (T entity);
        void Update (T entity);
        void Delete (T entity);
    }
}
