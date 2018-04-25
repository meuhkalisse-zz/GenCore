using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Services.TemplateGenerator.ClassGenerator
{
    public class ClassGenerator : IClassGenerator
    {
        public ClassTemplateType Type { get; set; }
        public string FileName { get; set; }
        public string ProjectName { get; set; }
        public string ClassName { get; set; }
        public string ExtendedClassName { get; set; }
        public bool AddPropertyList { get; set; }
        public string FolderName { get; set; }
    }
}
