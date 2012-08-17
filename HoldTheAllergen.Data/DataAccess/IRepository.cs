using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HoldTheAllergen.Data.DataAccess
{
    public interface IRepository<T> where T : class
    {
        T CreateNew();
        T GetById(int id);
        IQueryable<T> Query(Expression<Func<T, bool>> where);
        IQueryable<T> All();

        IRepository InsertOnSubmit(T entity);
        IRepository DeleteOnSubmit(T entity);
        IRepository DeleteAllOnSubmit(IEnumerable<T> entities);

        void SaveChanges();
    }

    public interface IRepository
    {
        object CreateNew();
        object GetById(int id);
        object All();

        IRepository InsertOnSubmit(object entity);
        IRepository DeleteOnSubmit(object entity);

        void SaveChanges();
    }
}