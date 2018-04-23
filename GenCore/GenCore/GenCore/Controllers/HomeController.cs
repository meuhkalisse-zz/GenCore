using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GenCore.Models;
using GenCore.Services;

namespace GenCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IColumnsService _columnsService;
        public HomeController(IColumnsService pColumnsService)
        {
            _columnsService = pColumnsService;
        }
        public IActionResult Index()
        {
            var t = _columnsService.GetAll();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
