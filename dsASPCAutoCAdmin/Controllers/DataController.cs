﻿using dsASPCAutoCAdmin.DataAccess;
using dsASPCAutoCAdmin.Entidades;
using dsASPCAutoCAdmin.ViewModels;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AllowAllOrigins")]
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

        public IActionResult TiposVehiculoLeer()
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.TiposVehiculoLeer();
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
        public IActionResult ModelosLeerPorMarca(int IDSeccion)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ModelosLeerPorMarca(IDSeccion);
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
        public IActionResult ClientesLeer([FromBody] PaginacionClientes cl)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ClientesLeer(cl.pagina, cl.bloque, cl.nCliente);
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
        public IActionResult ImagenesCabWebLeer()
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ImagenesCabWebLeer();
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
        public IActionResult PermisosLeerPorIDUsuario(int idUsuarioWeb)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.PermisosLeerPorIDUsuario(idUsuarioWeb);
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
        public IActionResult DomiciliosLeerPorIDCliente(int idCliente)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.DomiciliosLeerPorIDCliente(idCliente);
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
        public IActionResult ImagenesCabWeb_Procesar([FromBody] ImagenCabWeb img)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ImagenCabWeb_Procesar(img);
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
        public IActionResult PermisosUsuarioModificar([FromBody] EmpresaWeb empr, int idUsuarioWeb)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                ad.PermisosUsuarioModificar(empr, idUsuarioWeb);
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
        [HttpPost]
        public IActionResult ArticulosModificar([FromBody] BuscaArticulo bs)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.ArticulosModificar(bs);
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
        public IActionResult ArticulosLeerPorID(int IDArticulo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.ArticulosLeerPorID(IDArticulo);
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
        public IActionResult CategoriasLeer()
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.CategoriasLeer();
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
        public IActionResult ArticulosLeerPorCategoria(int IDCategoria)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ArticulosLeerPorCategoria(IDCategoria);
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
        public IActionResult ArticulosLeerPorCategoriaYCadena(int IDCategoria, string cadena)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                var res = ad.ArticulosLeerPorCategoriaYCadena(IDCategoria, cadena);
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
        public IActionResult TiposVidrioLeer()
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.TiposVidrioLeer();
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
        public IActionResult CarroceriasLeerPorCadena(string cadena)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.CarroceriasLeerPorCadena(cadena);
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
        public IActionResult MarcasLeerPorCadena(string cadena)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                var res = ad.MarcasLeerPorCadena(cadena);
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
        public IActionResult TiposVehiculoEliminar(int IDTipoVehiculo)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                //var res = new LecturasViewModel(_configuration, bs);
                ad.TiposVehiculoEliminar(IDTipoVehiculo);
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

        [HttpPost]
        public IActionResult EsquemaDeCarroceria([FromBody] Carroceria carr)
        {
            ObjectResult result;
            //var ad = new AdaptadorAtc(_configuration);
            try
            {
                //var carr = ad.CarroceriasLeerEsquema(idmodelocarroceria, ano);
                var res = new EsquemaViewModel(carr);
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
        public IActionResult ClientesAsignarUsuarioWeb(int IDUsuarioWeb, int IDCliente)
        {
            ObjectResult result;
            var ad = new ServicioCorreo(_configuration);
            try
            {
                var res = ad.ClientesAsignarUsuarioWeb(IDUsuarioWeb, IDCliente, null);
                //var res = new EsquemaViewModel(carr);
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
        //[HttpGet]
        //public IActionResult ClientesAsignarUsuarioWeb(int IDUsuarioWeb, int IDCliente, int IDDomicilioCliente)
        //{
        //    ObjectResult result;
        //    var ad = new ServicioCorreo(_configuration);
        //    try
        //    {
        //        var res = ad.ClientesAsignarUsuarioWeb(IDUsuarioWeb, IDCliente, IDDomicilioCliente);
        //        //var res = new EsquemaViewModel(carr);
        //        result = new ObjectResult(res)
        //        {
        //            StatusCode = (int)HttpStatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ObjectResult(ex)
        //        {
        //            StatusCode = (int)HttpStatusCode.Conflict
        //        };
        //        Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
        //    }
        //    return result;
        //}
        [HttpGet]
        public IActionResult UsuariosWebEliminar(int IDUsuarioWeb)
        {
            ObjectResult result;
            var ad = new AdaptadorAtcAdmin(_configuration);
            try
            {
                ad.UsuariosWebEliminar(IDUsuarioWeb);
                //var res = new EsquemaViewModel(carr);
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
        public IActionResult ClientesInvitacionesProcesar(int IDCliente)
        {
            ObjectResult result;
            var ad = new ServicioCorreo(_configuration);
            try
            {
                var ob = new List<Cliente>();
                ob.Add(new Cliente { IDCliente = IDCliente });
                var res = ad.ClientesInvitacionesProcesar(ob);
                //var res = new EsquemaViewModel(carr);
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
