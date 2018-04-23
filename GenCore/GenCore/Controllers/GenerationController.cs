using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using GenCore.Services;
using GenCore.Services.Generator;
using Microsoft.AspNetCore.Mvc;

namespace GenCore.Controllers
{
    public class GenerationController : Controller
    {
        private readonly ITablesService _tablesService;
        private readonly IColumnsService _columnsService;
        private readonly ICSharpGenerator _cSharpGenerator;

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
            ViewBag.ClassGen = _cSharpGenerator.GenerateClass(pTableName, colList);
            return View();
        }

        public FileStreamResult DownloadClass(string pCatalog, string pTableName)
        {
            var colList = _columnsService.GetAllByCatalogAndTableName(pCatalog, pTableName);
            string name = pTableName + ".cs";
            string file = _cSharpGenerator.GenerateClass(pTableName, colList);
            System.Text.Encoding enc = System.Text.Encoding.Unicode;
            MemoryStream str = new MemoryStream(enc.GetBytes(file));

            return File(str, "text/plain", name);
        }

        public FileStreamResult DownloadAllClass(string pCatalog)
        {
            var fileList = new List<FileStream>();
            var tables = _tablesService.GetAll();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var table in tables)
                    {
                        var colList = _columnsService.GetAllByCatalogAndTableName(pCatalog, table.TABLE_NAME);
                        var demoFile = archive.CreateEntry(table.TABLE_NAME + ".cs");
                        string file = _cSharpGenerator.GenerateClass(table.TABLE_NAME, colList);
                        using (var entryStream = demoFile.Open())
                        {
                            using (var streamWriter = new StreamWriter(entryStream))
                            {
                                streamWriter.Write(file);
                            }
                        }
                    }

                }

                return File(memoryStream, "application/zip", "Generations.zip");
            }



        }
    }
}