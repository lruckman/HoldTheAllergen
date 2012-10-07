using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class Repository<TObject> : IRepository<TObject>
        where TObject : class
    {
        protected DbContext Context = null;

        public Repository(DbContext context)
        {
            Context = context;
        }

        protected DbSet<TObject> DbSet
        {
            get { return Context.Set<TObject>(); }
        }

        #region IRepository<TObject> Members

        public virtual IQueryable<TObject> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<TObject>
            Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject TObject)
        {
            var newEntry = DbSet.Add(TObject);
            Context.SaveChanges();
            return newEntry;
        }

        public virtual int Count
        {
            get { return DbSet.Count(); }
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual int Update(TObject TObject)
        {
            var entry = Context.Entry(TObject);

            DbSet.Attach(TObject);
            entry.State = EntityState.Modified;

            return Context.SaveChanges();
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = Filter(predicate);

            foreach (var obj in objects)
            {
                DbSet.Remove(obj);
            }

            Context.SaveChanges();

            return 0;
        }

        public virtual int Delete(TObject TObject)
        {
            DbSet.Remove(TObject);
            return Context.SaveChanges();
        }


        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> filter, out int total, int index = 0,
                                                  int size = 50)
        {
            var skipCount = index*size;
            var resetSet = filter != null
                               ? DbSet.Where(filter).AsQueryable()
                               : DbSet.AsQueryable();
            resetSet = skipCount == 0
                           ? resetSet.Take(size)
                           : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        #endregion
    }
}