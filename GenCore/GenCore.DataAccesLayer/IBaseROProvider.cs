using GenCore.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.DataAccesLayer
{
    public interface IBaseROProvider<T>
        where T : IBaseEntity
    {
        IList<T> GetAll();
        T GetById(Guid pId);
    }
}
