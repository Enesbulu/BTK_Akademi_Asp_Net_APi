using Microsoft.EntityFrameworkCore;
using nTier.Repositories.Contracts;
using nTier.Repositories.EFCore.Repositories;
using System.Linq.Expressions;

namespace nTier.Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryDbContext _context;

        public RepositoryBase(RepositoryDbContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trachChanges) => !trachChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trachChanges) => trachChanges ? _context.Set<T>().Where(expression) : _context.Set<T>().Where(expression).AsNoTracking<T>();

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
