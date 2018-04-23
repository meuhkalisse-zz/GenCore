using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Domain.Base
{
    public interface IBaseCustomEntity
    {
        Guid Id { get; set; }
    }
}
