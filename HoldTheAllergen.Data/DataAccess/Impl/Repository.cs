using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class Repository<T> : IRepository<T>, IRepository where T : EntityObject
    {
        private readonly ObjectContext _context;

        public Repository(ObjectContext context)
        {
            _context = context;
        }

        #region IRepository Members

        object IRepository.All()
        {
            return All();
        }

        IRepository IRepository.InsertOnSubmit(object entity)
        {
            InsertOnSubmit((T) entity);
            return this;
        }

        IRepository IRepository.DeleteOnSubmit(object entity)
        {
            DeleteOnSubmit((T) entity);
            return this;
        }

        object IRepository.CreateNew()
        {
            return CreateNew();
        }

        object IRepository.GetById(int id)
        {
            return GetById(id);
        }

        #endregion

        #region IRepository<T> Members

        public virtual IQueryable<T> All()
        {
            return _context.CreateQuery<T>(GetEntitySetName());
        }

        public virtual T CreateNew()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T GetById(int id)
        {
            string entitySetKeyName = string.Format("{0}.{1}", _context.DefaultContainerName, GetEntitySetName());
            var key = new EntityKey(entitySetKeyName, "Id", id);

            return (T) _context.GetObjectByKey(key);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> where)
        {
            return _context.CreateQuery<T>(GetEntitySetName()).Where(where);
        }

        public virtual IRepository InsertOnSubmit(T entity)
        {
            _context.AddObject(GetEntitySetName(), entity);
            return this;
        }

        public virtual IRepository DeleteOnSubmit(T entity)
        {
            _context.DeleteObject(entity);
            return this;
        }

        public virtual IRepository DeleteAllOnSubmit(IEnumerable<T> entities)
        {
            foreach (T entity in entities) DeleteOnSubmit(entity);
            return this;
        }

        #endregion

        private string GetEntitySetName()
        {
            string entitySetName = _context.MetadataWorkspace.GetEntityContainer(
                _context.DefaultContainerName, DataSpace.CSpace)
                .BaseEntitySets.FirstOrDefault(x => x.ElementType.Name == typeof (T).Name).Name;

            return entitySetName;
        }
    }
}