using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IRepository<T> 
    {
        List<T> List();

        T Get(Expression<Func<T, bool>> filter);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        List<T> List(Expression<Func<T, bool>> filter);
    }
}
