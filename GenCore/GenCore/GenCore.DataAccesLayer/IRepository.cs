using GenCore.Domain.Base;
using NHibernate;
using System;
using System.Linq;

namespace GenCore.DataAccesLayer
{
    public interface IRepository<T>
        where T : IBaseEntity
    {
        T GetById(Guid id);
        IQueryable<T> GetAll();
        IQueryOver<T, T> GetAllToFilter();
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
