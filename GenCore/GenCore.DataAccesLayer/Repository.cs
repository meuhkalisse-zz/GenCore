using GenCore.Domain.Base;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenCore.DataAccesLayer
{
    internal class Repository<T> : IRepository<T>
        where T : class, IBaseEntity
    {
        private ISession _session { get; set; }

        public Repository(ISession pSession) => _session = pSession;

        public void Create(T entity) => _session.Save(entity);

        public void Delete(Guid id) => _session.Delete(_session.Load<T>(id));

        public IQueryable<T> GetAll() => _session.Query<T>();

        public IQueryOver<T, T> GetAllToFilter() => _session.QueryOver<T>();

        public T GetById(Guid id)
        {
            var entity = _session.Get<T>(id);
            if (entity == null)
                throw new Exception("dang yo");
            return entity;
        }

        public void Update(T entity) => _session.Update(entity);
    }
}
