using dsASPCAutoCAdmin.DataAccess;
using dsASPCAutoCAdmin.Entidades;
using dsASPCAutoCAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace dsASPCAutoCAdmin.Controllers
{
    [Produces("application/json")]
    public class DataController : Controller
    {
        private IConfiguration _configuration;

        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Companies()
        {
            ObjectResult result;
            //var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = ad.TiposVehiculoLeerPorID(IDTipoVehiculo);
                result = new ObjectResult(1)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        public IActionResult TiposVehiculoLeerPorID(int IDTipoVehiculo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.TiposVehiculoLeerPorID(IDTipoVehiculo);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }

        public IActionResult MarcasModelosLeerPorID(int IDMarcaModelo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.MarcasModelosLeerPorID(IDMarcaModelo);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        public IActionResult TiposVidrioLeerPorID(int IDTipoVidrio)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.TiposVidrioLeerPorID(IDTipoVidrio);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        public IActionResult CarroceriasLeerPorID(int IDCarroceria)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.CarroceriasLeerPorID(IDCarroceria);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        public IActionResult VidriosLeerPorID(int IDVidrio)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.VidriosLeerPorID(IDVidrio);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        [HttpPost]
        public IActionResult LecturasGenericasPaginadas([FromBody] BusquedaPaginada bs)
        {
            ObjectResult result;
            //var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = new LecturasViewModel(_configuration, bs);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        [HttpPost]
        public IActionResult EjemploTipoVidrio(BusquedaPaginada bs)
        {
            ObjectResult result;
            //var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = SampleData.TiposVidrio;
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }

    }
}
