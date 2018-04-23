using System;
using System.Collections.Generic;
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
    }
}