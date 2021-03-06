﻿using dsASPCAutoCAdmin.DataAccess;
using dsASPCAutoCAdmin.Entidades;
using dsCore.Buscador;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAutoCAdmin.ViewModels
{
    public class LecturasViewModel
    {
        public BusquedaPaginada cm;
        public List<object> Articulos;
        public int NumReg;
        public int NumPags;
        public Boolean SinResultados;
        public int? idSeccion;
        public LecturasViewModel(IConfiguration configuration, BusquedaPaginada ba)
        {
            var cadenas = ObtenerCadenasSegúnTipo(ba.tipo);
            cm = new BusquedaPaginada();
            cm.cadena = ba.cadena;
            cm.tipo = ba.tipo;
            idSeccion = ba.idSeccion;
            var v = "";
            var vc = "";
            if (ba.AccionPagina == null)
            {
                ba.AccionPagina = "F";
            }
            switch (ba.AccionPagina)
            {
                case "P":
                    v = ba.FirstValor;
                    vc = ba.FirstIndice.ToString();
                    break;
                case "N":
                    v = ba.LastValor;
                    vc = ba.LastIndice.ToString();
                    break;
            }
            string lcb = null;
            if (ba.idSeccion.HasValue)
            {
                lcb = "IDSeccion=" + ba.idSeccion;
                if (ba.cadena != null && ba.cadena.Length > 0)
                {
                    lcb = lcb + " AND " + cadenas.CampoOrdenacion + " LIKE '%" + ba.cadena + "%' ";
                }
            } else if (ba.cadena != null && ba.cadena.Length > 0)
            {
                lcb = cadenas.CampoOrdenacion + " LIKE '%" + ba.cadena.Replace(" ", "%") + "%' "; 
                if (cadenas.EntidadFuncion == "WebArticulos")
                {
                    lcb = lcb + " OR Codigo LIKE '%" + ba.cadena.Replace(" ", "%") + "%' ";
                }
            }
            var criterioAuxiliares = new CriterioBusqueda
            {
                IdISOLang = null,
                SqlWhere = lcb,
                CampoOrdenacion = cadenas.CampoOrdenacion, //"Descripcion",
                TipoOrden = "ASC",
                NumPagina = 1,
                TamanoPagina = 6,
                CamposBusqueda = null,
                Entidad = cadenas.Entidad, //"BuscaArticulo",
                Paginacion = false,
                Operacion = ba.AccionPagina,
                Valor = v,
                ValorClave = vc,
                CampoClave = cadenas.CampoClave, //"IdArticulo",
                EntidadFuncion = cadenas.EntidadFuncion, //"BuscaArticulo",
                ValorFuncion = "'" + ba.cadena.Replace(" ", "%") + "'",
                EntidadVista = cadenas.Vista, // "VBuscaArticulo",
                idAlmacen = 1,
                idDelegacion = 0
            };
            var ls = new LecturasDA(configuration);
            var res = ls.LeerLista(criterioAuxiliares);
            Articulos = res.ListaResultados;
            NumReg = res.ContadorResultados;
            NumPags = res.NumeroPaginas;
            ColocarIndicesSegunTipo(ba.tipo, ba.AccionPagina);
        }
        private CadenasBusqueda ObtenerCadenasSegúnTipo(string tipo)
        {
            var res = new CadenasBusqueda();
            res.CampoOrdenacion = "Descripcion";
            switch (tipo)
            {
                case "Carroceria":
                case "Carrocerias":
                    res.Vista = "VCarrocerias";
                    res.EntidadFuncion = "VCarrocerias";
                    res.CampoClave = "IdCarroceria";
                    res.Entidad = "Carroceria";
                    break;
                case "TiposVidrio":
                    res.Vista = "VTiposVidrio";
                    res.EntidadFuncion = "VTiposVidrio";
                    res.CampoClave = "IdTipoVidrio";
                    res.Entidad = "TipoVidrio";
                    break;
                case "Vidrios":
                    res.Vista = "VVidrios";
                    res.EntidadFuncion = "VVidrios";
                    res.CampoClave = "IdVidrio";
                    res.Entidad = "Vidrio";
                    break;
                case "MarcaModelo":
                    res.Vista = "VMarcaModelo";
                    res.EntidadFuncion = "MarcaModelo";
                    res.CampoClave = "IdMarcaModelo";
                    res.Entidad = "MarcaModelo";
                    break;
                case "TipoVehiculo":
                case "TiposVehiculo":
                    res.Vista = "VTiposVehiculo";
                    res.EntidadFuncion = "VTiposVehiculo";
                    res.CampoClave = "IdTipoVehiculo";
                    res.Entidad = "TipoVehiculo";
                    res.CampoOrdenacion = "Descripcion";
                    break;
                case "Marcas":
                    res.Vista = "Seccion";
                    res.EntidadFuncion = "Seccion";
                    res.CampoClave = "IdSeccion";
                    res.Entidad = "Marca";
                    res.CampoOrdenacion = "DescripcionSeccion";
                    break;
                case "Modelos":
                case "Modelo":
                    res.Vista = "Familia";
                    res.EntidadFuncion = "Familia";
                    res.CampoClave = "IdFamilia";
                    res.Entidad = "Modelo";
                    res.CampoOrdenacion = "DescripcionFamilia";
                    break;
                case "Articulo":
                case "Articulos":
                    res.Vista = "WebArticulos";
                    res.EntidadFuncion = "WebArticulos";
                    res.CampoClave = "IdArticulo";
                    res.Entidad = "Articulo";
                    res.CampoOrdenacion = "Descripcion";
                    break;
                case "UsuariosWeb":
                case "UsuarioWeb":
                    res.Vista = "VUsuariosWeb";
                    res.EntidadFuncion = "VUsuariosWeb";
                    res.CampoClave = "IdUsuarioWeb";
                    res.Entidad = "UsuarioWeb";
                    res.CampoOrdenacion = "Nombre";
                    break;
            }
            return res;
        }
        private void ColocarIndicesSegunTipo(string tipo, string action)
        {
            try
            {
                switch (tipo)
                {
                    case "Carroceria":
                    case "Carrocerias":
                        RellenoIndiceCarroceria(action);
                        break;
                    case "Vidrio":
                        RellenoIndiceVidrio(action);
                        break;
                    case "TipoVidrio":
                    case "TiposVidrio":
                        RellenoIndiceTipoVidrio(action);
                        break;
                    case "BuscaArticulo":
                        RellenoIndiceBuscaObjeto(action);
                        break;
                    case "TipoVehiculo":
                        RellenoIndiceTipoVehiculo(action);
                        break;
                    case "Marca":
                    case "Marcas":
                        RellenoIndiceMarca(action);
                        break;
                    case "Modelos":
                    case "Modelo":
                        RellenoIndiceModelo(action);
                        break;
                    case "Articulo":
                    case "Articulos":
                        RellenoIndiceArticulo(action);
                        break;
                    case "UsuarioWeb":
                    case "UsuariosWeb":
                        RellenoIndiceUsuarios(action);
                        break;
                }
            }
            catch
            {
                SinResultados = true;
            }
            
        }
        private void RellenoIndiceCarroceria(string action)
        {
            var c = (Carroceria)Articulos[Articulos.Count - 1];
            var d = (Carroceria)Articulos[0];
            cm.LastValor = c.Descripcion;
            cm.LastIndice = c.IDCarroceria;
            cm.FirstValor = d.Descripcion;
            cm.FirstIndice = d.IDCarroceria;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceUsuarios(string action)
        {
            var c = (UsuarioDatosEmail)Articulos[Articulos.Count - 1];
            var d = (UsuarioDatosEmail)Articulos[0];
            cm.LastValor = c.Nombre;
            cm.LastIndice = c.IdUsuarioWeb;
            cm.FirstValor = d.Nombre;
            cm.FirstIndice = d.IdUsuarioWeb;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceTipoVidrio(string action)
        {
            var c = (TipoVidrio)Articulos[Articulos.Count - 1];
            var d = (TipoVidrio)Articulos[0];
            cm.LastValor = c.Descripcion;
            cm.LastIndice = c.IDTipoVidrio;
            cm.FirstValor = d.Descripcion;
            cm.FirstIndice = d.IDTipoVidrio;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceArticulo(string action)
        {
            var c = (BuscaArticulo)Articulos[Articulos.Count - 1];
            var d = (BuscaArticulo)Articulos[0];
            cm.LastValor = c.Descripcion;
            cm.LastIndice = c.IdArticulo;
            cm.FirstValor = d.Descripcion;
            cm.FirstIndice = d.IdArticulo;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceMarca(string action)
        {
            var c = (Marca)Articulos[Articulos.Count - 1];
            var d = (Marca)Articulos[0];
            cm.LastValor = c.DescripcionSeccion;
            cm.LastIndice = c.IDSeccion;
            cm.FirstValor = d.DescripcionSeccion;
            cm.FirstIndice = d.IDSeccion;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceModelo(string action)
        {
            var c = (Modelo)Articulos[Articulos.Count - 1];
            var d = (Modelo)Articulos[0];
            cm.LastValor = c.DescripcionFamilia;
            cm.LastIndice = c.IDFamilia;
            cm.FirstValor = d.DescripcionFamilia;
            cm.FirstIndice = d.IDFamilia;
            cm.AccionPagina = action;
            cm.idSeccion = idSeccion;
        }
        private void RellenoIndiceTipoVehiculo(string action)
        {
            var c = (TipoVehiculo)Articulos[Articulos.Count - 1];
            var d = (TipoVehiculo)Articulos[0];
            cm.LastValor = c.Descripcion;
            cm.LastIndice = c.IDTipoVehiculo;
            cm.FirstValor = d.Descripcion;
            cm.FirstIndice = d.IDTipoVehiculo;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceVidrio(string action)
        {
            var c = (Vidrio)Articulos[Articulos.Count - 1];
            var d = (Vidrio)Articulos[0];
            cm.LastValor = c.Descripcion;
            cm.LastIndice = c.IDVidrio;
            cm.FirstValor = d.Descripcion;
            cm.FirstIndice = d.IDVidrio;
            cm.AccionPagina = action;
        }
        private void RellenoIndiceBuscaObjeto(string action)
        {
            var c = (BuscaArticulo)Articulos[Articulos.Count - 1];
            var d = (BuscaArticulo)Articulos[0];
            cm.LastValor = c.Descripcion;
            cm.LastIndice = c.IdArticulo;
            cm.FirstValor = d.Descripcion;
            cm.FirstIndice = d.IdArticulo;
            cm.AccionPagina = action;
        }
    }
    
}
