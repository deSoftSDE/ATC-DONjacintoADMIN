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
        public IActionResult TiposVidrioEliminar(int IDTipoVidrio)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                ad.TiposVidrioEliminar(IDTipoVidrio);
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
        public IActionResult MarcasEliminar(int IDSeccion)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                ad.MarcasEliminar(IDSeccion);
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
        public IActionResult MarcasLeerPorID(int IDSeccion)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.MarcasLeerPorID(IDSeccion);
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
        public IActionResult TiposVidrioCrearModificar([FromBody] TipoVidrio vidrio)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.TiposVidrioCrearModificar(vidrio);
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
        public IActionResult MarcasCrearModificar([FromBody] Marca marca)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.MarcasCrearModificar(marca);
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
        public IActionResult TiposVehiculoCrearModificar([FromBody] TipoVehiculo vehiculo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.TiposVehiculoCrearModificar(vehiculo);
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
        [HttpPost]
        public IActionResult CarroceriasCrearModificar([FromBody] Carroceria carroceria)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.CarroceriasCrearModificar(carroceria);
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
        public IActionResult ModelosCrearModificar([FromBody] Modelo modelo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ModelosCrearModificar(modelo);
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
        public IActionResult ModelosLeerPorID(int IDFamilia)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ModelosLeerPorID(IDFamilia);
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
        [HttpGet]
        public IActionResult TiposVidrioLeerPorCadena(string cadena)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.TiposVidrioLeerPorCadena(cadena);
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
        [HttpGet]
        public IActionResult CarroceriasEliminar(int IDCarroceria)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                ad.CarroceriasEliminar(IDCarroceria);
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
        [HttpGet]
        public IActionResult TiposVehiculoEliminar(int idGenerico)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                ad.TiposVehiculoEliminar(idGenerico);
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
