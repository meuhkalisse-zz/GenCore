using GenCore.Domain;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            var path = GetClassTemplatePath();
            string template = File.ReadAllText(path);
            template = template.Replace("{{ProjectName}}", "VetReseau").Replace("{{ClassName}}", pClassName);
            StringBuilder sb = new StringBuilder();
            foreach (Columns c in pPropertyList)
            {
                sb.AppendLine(GenerateProperty(c));
            }
            return template.Replace("{{Properties}}", sb.ToString());
        }

        public string GetClassTemplatePath()
        {
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = dirPath.Substring(0, dirPath.LastIndexOf('\\'));
            return Path.GetFullPath(Path.Combine(dirPath, "Content\\ClassTemplate.txt"));
        }

        private string GenerateProperty(Columns c)
        {
            return string.Format("public virtual {0} {1} {{ get; set; }}", GetPropertyType(c), c.COLUMN_NAME);
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
