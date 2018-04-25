using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using GenCore.Domain;
using GenCore.Services;
using GenCore.Services.Generator;
using GenCore.Services.TemplateGenerator.ClassGenerator;
using Microsoft.AspNetCore.Mvc;

namespace GenCore.Controllers
{
    public class GenerationController : Controller
    {
        private readonly ITablesService _tablesService;
        private readonly IColumnsService _columnsService;
        private readonly ICSharpGenerator _cSharpGenerator;

        private readonly Dictionary<string, bool> _providerFolderList = new Dictionary<string, bool>()
        {
            { "provider/base/", true },
            { "provider/extended/", false }
        };

        public GenerationController(ITablesService pTablesService, IColumnsService pColumnsService, ICSharpGenerator pCSharpGenerator)
        {
            _tablesService = pTablesService;
            _columnsService = pColumnsService;
            _cSharpGenerator = pCSharpGenerator;
        }

        public IActionResult Tables()
        {
            var model = _tablesService.GetAll();
            ViewBag.Catalog = "VetReseau_DEV";
            return View(model);
        }

        public IActionResult Columns(string pCatalog, string pName)
        {
            var colList = _columnsService.GetAllByCatalogAndTableName(pCatalog, pName);
            ViewBag.TableName = pName;
            ViewBag.Catalog = pCatalog;
            return View(colList);
        }

        public IActionResult Generate(string pCatalog, string pTableName)
        {
            var colList = _columnsService.GetAllByCatalogAndTableName(pCatalog, pTableName);
            //ViewBag.ClassGen = _cSharpGenerator.GenerateClass(pTableName, colList);
            return View();
        }

        public FileStreamResult DownloadClass(string pCatalog, string pTableName)
        {
            var colList = _columnsService.GetAllByCatalogAndTableName(pCatalog, pTableName);
            string name = pTableName + ".cs";
            string file = _cSharpGenerator.GenerateClass(ClassGeneratorFactory.GetGenerator<ClassGenerator>(ClassTemplateType.BASE, pTableName), colList);
            System.Text.Encoding enc = System.Text.Encoding.Unicode;
            MemoryStream str = new MemoryStream(enc.GetBytes(file));

            return File(str, "text/plain", name);
        }

        public ActionResult DownloadAllClass(string pCatalog)
        {
            return File(_cSharpGenerator.GenerateAll(pCatalog).ToArray(), "application/zip", "Generations.zip");
        }

        public void GenerateProviders(ZipArchive pArchive, string pTableName)
        {
            foreach (var entry in _providerFolderList)
            {
                var demoFile = pArchive.CreateEntry(entry.Key + pTableName + "Provider.cs");
                string file = _cSharpGenerator.GenerateProvider(pTableName, entry.Value);
                using (var entryStream = demoFile.Open())
                {
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write(file);
                    }
                }
            }
        }
    }
}