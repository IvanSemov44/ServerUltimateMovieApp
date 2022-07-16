using System.Linq.Expressions;

namespace Constracts
{
    public  interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChenges);
        IQueryable<T> FindByConition(Expression<Func<T, bool>> expression,bool trackChange);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
