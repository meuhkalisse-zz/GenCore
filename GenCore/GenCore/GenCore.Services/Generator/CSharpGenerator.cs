using GenCore.Domain;
using GenCore.Services.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Services.Generator
{
    public interface ICSharpGenerator
    {
        string GenerateClass(string pClassName, List<Columns> pPropertyList);
    }
    internal class CSharpGenerator : ICSharpGenerator
    {
        public string GenerateClass(string pClassName, List<Columns> pPropertyList)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(CSharpConstant.EntityFileStart, pClassName));
            foreach (Columns c in pPropertyList)
            {
                sb.AppendLine(GenerateProperty(c));
            }
            sb.AppendLine(CSharpConstant.EntityFileEnd);
            return sb.ToString();
        }

        private string GenerateProperty(Columns c)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("public virtual {0} {1} {{ get; set; }}", GetPropertyType(c), c.COLUMN_NAME));
            return sb.ToString();
        }

        private string GetPropertyType(Columns column)
        {
            string cSharpType = "";
            switch (column.DATA_TYPE)
            {
                case "uniqueidentifier":
                    cSharpType = "Guid";
                    break;
                case "datetime":
                    cSharpType = "DateTime";
                    break;
                case "int":
                    cSharpType = "int";
                    break;
                case "nvarchar":
                case "varchar":
                    cSharpType = "string";
                    break;
                case "bit":
                    cSharpType = "bool";
                    break;
                case "binary":
                    cSharpType = "byte[]";
                    break;
                default:
                    cSharpType = "string";
                    break;
            }

            return column.IS_NULLABLE == "YES" ? cSharpType + "?" : cSharpType;
        }
    }
}
