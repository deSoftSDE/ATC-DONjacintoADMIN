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
using dsASPCAutoCAdmin.Entidades;

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
            ViewData["Message"] = "";
            //ViewData["Company"] = SampleData.Companies;
            return View();
        }
        public IActionResult Carrocerias()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "";
            return View();
        }
        public IActionResult Usuarios()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "";
            return View();
        }

        public IActionResult Modelo(int id)
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            var ad = new AdaptadorAtcAdmin(_configuration);
            //var c = Int32.Parse(IDSeccion);
            var md = ad.ModelosLeerPorID(id);
            var vh = ad.TiposVehiculoLeer();
            ViewData["Modelo"] = md;
            ViewData["Message"] = "Carrocerias de " + md.DescripcionFamilia;
            ViewData["TiposVehiculo"] = vh;
            return View();
        }
        public IActionResult TiposVehiculo()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "";
            return View();
        }
        public IActionResult Articulos()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "Editar artículos";
            return View();
        }

        public IActionResult Marcas()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "";
            return View();
        }
        public IActionResult Web()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "";
            var ad = new AdaptadorAtcAdmin(_configuration);
            var b = ad.DatosEmpresaLeer();
            return View(b);
        }
        [HttpPost]
        public IActionResult Web([FromForm] EmpresaWeb a)
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            ViewData["Message"] = "";
            var ad = new AdaptadorAtcAdmin(_configuration);
            ad.DatosEmpresaProcesar(a);
            var b = ad.DatosEmpresaLeer();
            return View(b);
        }
        [HttpGet]
        public IActionResult Marca(int id)
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
        [HttpPost]
        public IActionResult Mensajes(MensajeWeb msj)
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            return View();
        }
        public IActionResult EliminarMensaje(int id)
        {
            //var em = new MenuViewModel();
            //ViewData["FilterMenu"] = em.menu;
            var ad = new AdaptadorAtcAdmin(_configuration);
            ad.MensajeEliminar(id);
            return RedirectToAction("Mensajes", "Home");
        }
        public IActionResult Mensajes()
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            var ad = new AdaptadorAtcAdmin(_configuration);
            var msj = ad.MensajeLeer(-1, 0);
            ViewData["Mensajes"] = msj;
            return View();
        }
        [HttpGet]
        public IActionResult Mensaje(int id)
        {
            var em = new MenuViewModel();
            ViewData["FilterMenu"] = em.menu;
            if (id > 0 )
            {
                var ad = new AdaptadorAtcAdmin(_configuration);
                var msj = ad.MensajeLeerPorId(-1, 0, id);
                if (msj.IdCliente < 1)
                {
                    msj.Cliente = "Todos";
                }
                ViewData["Mensaje"] = msj;
                return View(msj);
            } else
            {
               var  msj = new MensajeWeb();
                if (msj.IdCliente < 1)
                {
                    msj.Cliente = "Todos";
                }
                ViewData["Mensaje"] = msj;
                return View(msj);
            }
            
        }
        [HttpPost]
        public IActionResult ModificarMensaje([FromForm] MensajeWeb msj)
        {
            var em = new MenuViewModel();
            var ad = new AdaptadorAtcAdmin(_configuration);
            ad.MensajeModificar(msj);
            return RedirectToAction("Mensajes", "Home");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
