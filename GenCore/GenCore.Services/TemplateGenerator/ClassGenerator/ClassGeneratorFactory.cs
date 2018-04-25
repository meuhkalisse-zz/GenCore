using System;
using System.Collections.Generic;
using System.Text;

namespace GenCore.Services.TemplateGenerator.ClassGenerator
{
    public static class ClassGeneratorFactory
    {
        public static IClassGenerator GetGenerator<T>(ClassTemplateType type, string pClassName)
            where T : IClassGenerator, new()
        {
            switch (type)
            {
                case ClassTemplateType.EXTENDED:
                    return new T()
                    {
                        Type = ClassTemplateType.EXTENDED,
                        FileName = pClassName + "Extended.cs",
                        ClassName = pClassName + "Extended",
                        ExtendedClassName = pClassName,
                        ProjectName = "VetReseau",
                        AddPropertyList = false,
                        FolderName = "entity/extended"
                    };
                case ClassTemplateType.BASE:
                default:
                    return new T()
                    {
                        Type = ClassTemplateType.BASE,
                        FileName = pClassName + ".cs",
                        ClassName = pClassName,
                        ExtendedClassName = "IBaseEntity",
                        ProjectName = "VetReseau",
                        AddPropertyList = true,
                        FolderName = "entity/base"
                    };
            }
        }
    }
}
