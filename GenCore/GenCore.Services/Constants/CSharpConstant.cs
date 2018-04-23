using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Services.Constants
{
    public static class CSharpConstant
    {
        public const string EntityFileStart =
            @"using system;
              using VetReseau.Domain.Base;
                
              namespace VetReseau.Domain
              {{
                public class {0} : IBaseEntity
                {{";
        public const string EntityFileEnd = @"}
        }";
    }
}
