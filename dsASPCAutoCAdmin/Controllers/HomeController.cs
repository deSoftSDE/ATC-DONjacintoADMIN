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
using dsASPCAutoCAdmin.DataAccess;
using Microsoft.Extensions.Configuration;

namespace dsASPCAutoCAdmin.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
            ViewData["Message"] = "Editar-crear tipos de Vidrio";
            //ViewData["Company"] = SampleData.Companies;
            return View();
        }
        public IActionResult Carrocerias()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Editar-crear Carrocerías";
            return View();
        }
        public IActionResult ModelosCarrocerias(int id)
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            var ad = new AdaptadorAtcAdmin(_configuration);
            //var c = Int32.Parse(IDSeccion);
            var md = ad.ModelosLeerPorID(id);
            ViewData["Modelo"] = md;
            ViewData["Message"] = "Carrocerias de " + md.DescripcionFamilia;
            return View();
        }
        public IActionResult TiposVehiculo()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Editar-crear Tipos de vehiculo";
            return View();
        }

        public IActionResult Marcas()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Editar-crear Marcas";
            return View();
        }
        [HttpGet]
        public IActionResult Modelos(int id)
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            var ad = new AdaptadorAtcAdmin(_configuration);
            //var c = Int32.Parse(IDSeccion);
            var mc = ad.MarcasLeerPorID(id);
            ViewData["Marca"] = mc;
            ViewData["Message"] = "Modelos de " + mc.DescripcionSeccion;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
