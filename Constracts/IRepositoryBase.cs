using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
