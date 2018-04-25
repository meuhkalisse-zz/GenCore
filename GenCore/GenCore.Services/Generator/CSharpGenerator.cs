using GenCore.Domain;
using GenCore.Services.TemplateGenerator.ClassGenerator;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace GenCore.Services.Generator
{
    public interface ICSharpGenerator
    {
        MemoryStream GenerateAll(string pCatalog);
        string GenerateClass(IClassGenerator pCg, List<Columns> pPropertyList);
        string GenerateProvider(string pClassName, bool pAddProperties = true);
    }
    internal class CSharpGenerator : ICSharpGenerator
    {
        private readonly ITablesService _tablesService;
        private readonly IColumnsService _columnsService;
        private readonly List<ClassTemplateType> _classTemplateType = new List<ClassTemplateType>()
        {
            ClassTemplateType.BASE,
            ClassTemplateType.EXTENDED,
        };

        public CSharpGenerator(ITablesService pTablesService, IColumnsService pColumnsService)
        {
            _tablesService = pTablesService;
            _columnsService = pColumnsService;
        }

        public MemoryStream GenerateAll(string pCatalog)
        {
            var fileList = new List<FileStream>();
            var tables = _tablesService.GetAll();

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var table in tables)
                {
                    var colList = _columnsService.GetAllByCatalogAndTableName(pCatalog, table.TABLE_NAME);
                    GenerateClasses(archive, table.TABLE_NAME, colList);
                    //GenerateProviders(archive, table.TABLE_NAME);
                }
            }
            return memoryStream;
        }

        private void GenerateClasses(ZipArchive pArchive, string pTableName, List<Columns> pColList)
        {
            foreach (var entry in _classTemplateType)
            {
                IClassGenerator cg = ClassGeneratorFactory.GetGenerator<ClassGenerator>(entry, pTableName);
                var demoFile = pArchive.CreateEntry(cg.FileName);
                string file = GenerateClass(cg, pColList);
                using (var entryStream = demoFile.Open())
                {
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write(file);
                    }
                }
            }
        }

        public string GenerateClass(IClassGenerator pCg, List<Columns> pPropertyList)
        {
            var path = GetTemplatePath(Templates.Class);
            string template = File.ReadAllText(path);
            template = template.Replace("{{ProjectName}}", pCg.ProjectName).Replace("{{ClassName}}", pCg.ClassName).Replace("{{ExtendedClassName}}", pCg.ExtendedClassName);
            StringBuilder sb = new StringBuilder();
            foreach (Columns c in pPropertyList)
            {
                sb.AppendLine(GenerateProperty(c));
            }
            return template.Replace("{{Properties}}", pCg.AddPropertyList ? sb.ToString() : "");
        }
        public string GenerateProvider(string pEntityName, bool pAddProperties = true)
        {
            var path = GetTemplatePath(Templates.Property);
            string template = File.ReadAllText(path);
            template = template.Replace("{{ProjectName}}", "VetReseau").Replace("{{EntityName}}", pEntityName);
            return template;
        }

        private string GetTemplatePath(Templates t)
        {
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = dirPath.Substring(0, dirPath.LastIndexOf('\\'));
            return Path.GetFullPath(Path.Combine(dirPath, "Content\\" + t.Value));
        }

        private string GenerateProperty(Columns c)
        {
            var path = GetTemplatePath(Templates.Property);
            string template = File.ReadAllText(path);
            return template.Replace("{{DATATYPE}}", GetPropertyType(c)).Replace("{{PROPERTYNAME}}", c.COLUMN_NAME);
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
