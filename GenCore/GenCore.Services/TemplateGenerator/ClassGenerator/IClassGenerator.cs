using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Services.TemplateGenerator.ClassGenerator
{
    public interface IClassGenerator
    {
        ClassTemplateType Type { get; set; }
        string FileName { get; set; }
        string ProjectName { get; set; }
        string ClassName { get; set; }
        string ExtendedClassName { get; set; }
        bool AddPropertyList { get; set; }
        string FolderName { get; set; }
    }
}
