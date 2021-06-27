using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity

    {
        Context context = new Context();
        DbSet<T> _object;

        public GenericRepository()
        {
            _object = context.Set<T>();
        }

        public void Delete(T entity)
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.Where(x=>!x.IsDeleted).SingleOrDefault(filter);
        }

        public void Insert(T entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public List<T> List()
        {
            return _object.Where(x=>!x.IsDeleted).ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
