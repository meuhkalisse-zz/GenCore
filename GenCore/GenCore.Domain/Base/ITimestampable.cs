using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Domain.Base
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
