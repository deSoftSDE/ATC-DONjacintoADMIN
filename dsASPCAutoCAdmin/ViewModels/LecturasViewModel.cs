using dsASPCAutoCAdmin.DataAccess;
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
        public LecturasViewModel(IConfiguration configuration, BusquedaPaginada ba)
        {
            var cadenas = ObtenerCadenasSegúnTipo(ba.tipo);
            cm = new BusquedaPaginada();
            cm.cadena = ba.cadena;
            cm.tipo = ba.tipo;
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
            var criterioAuxiliares = new CriterioBusqueda
            {
                IdISOLang = null,
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
                ValorFuncion = "'" + ba.cadena + "'",
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
