using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dsASPCAutoCAdmin.Models;
using dsASPCAutoCAdmin.ViewModels;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;

namespace dsASPCAutoCAdmin.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            return View();
            
        }

        public IActionResult About()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Productos()
        {
            
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult TiposVidrio()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Página de tipos de Vidrio";
            //ViewData["Company"] = SampleData.Companies;
            return View();
        }
        public IActionResult Carrocerias()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Página de Carrocerías";
            return View();
        }
        public IActionResult TiposVehiculo()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Página de Tipos de vehiculo";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
