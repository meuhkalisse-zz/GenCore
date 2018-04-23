using GenCore.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.DataAccesLayer
{
    public interface IBaseProvider<T> : IBaseROProvider<T>
        where T : IBaseEntity
    {
        void Create(T pEntity);
        void Delete(Guid pId);
        void Update(T entity);
    }
}
