using Constracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext repositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }


        public IQueryable<T> FindAll(bool trackChanges)
            => !trackChanges
            ? repositoryContext.Set<T>().AsNoTracking()
            : repositoryContext.Set<T>();

        public IQueryable<T> FindByConition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges)
            => !trackChanges
            ? repositoryContext.Set<T>().Where(expression).AsNoTracking()
            : repositoryContext.Set<T>().Where(expression);

        public void Create(T entity)
            => repositoryContext.Add<T>(entity);

        public void Update(T entity)
            => repositoryContext.Update<T>(entity);

        public void Delete(T entity)
            => repositoryContext.Remove<T>(entity);
    }
}
