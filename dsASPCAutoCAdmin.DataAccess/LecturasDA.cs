using dsASPCAutoCAdmin.Entidades;
using dsCore2.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace dsASPCAutoCAdmin.DataAccess
{
    public class LecturasDA : DataList
    {
        public LecturasDA(IConfiguration configuration) : base(configuration)
        {
            var a = _configuration.GetConnectionString("DefaultConnection");
        }
        protected override void AsignarMetodoRelleno()
        {
            switch (Criterio.Entidad)
            {
                case "BuscaArticulo":
                case "VBuscaArticulo":
                case "BuscaArticuloUM":
                    MetodoRellenarLista = metodoRellenoBuscaArticulos;
                    TipoDato = typeof(BuscaArticulo);
                    break;
                case "Vidrio":
                    MetodoRellenarLista = metodoRellenoVidrio;
                    TipoDato = typeof(Vidrio);
                    break;
                case "TipoVidrio":
                case "TiposVidrio":
                    MetodoRellenarLista = metodoRellenoTipoVidrio;
                    TipoDato = typeof(TipoVidrio);
                    break;
                case "TipoVehiculo":
                    MetodoRellenarLista = metodoRellenoTipoVehiculo;
                    TipoDato = typeof(TipoVehiculo);
                    break;
                case "MarcaModelo":
                    MetodoRellenarLista = metodoRellenoMarcaModelo;
                    TipoDato = typeof(MarcaModelo);
                    break;
                case "Carroceria":
                    MetodoRellenarLista = metodoRellenoCarroceria;
                    TipoDato = typeof(Carroceria);
                    break;


            }
        }



        private void metodoRellenoBuscaArticulos(object entidadLista)
        {
            ((BuscaArticulo)entidadLista).IdArticulo = AsignaEntero("IdArticulo");
            ((BuscaArticulo)entidadLista).IdUnidadManipulacion = AsignaEnteroNull("IdUnidadManipulacion");
            ((BuscaArticulo)entidadLista).IdUnidadValoracion = AsignaEnteroNull("IdUnidadValoracion");
            ((BuscaArticulo)entidadLista).IdMedidaUM = AsignaEnteroNull("IdUnidadVenta");
            ((BuscaArticulo)entidadLista).Codigo = AsignaCadena("Codigo");
            ((BuscaArticulo)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((BuscaArticulo)entidadLista).DescripcionUM = AsignaCadena("DescripcionUM");
            ((BuscaArticulo)entidadLista).GTINUC = AsignaCadena("GTINUC");
            ((BuscaArticulo)entidadLista).GTINUM = AsignaCadena("GTINUM");
            ((BuscaArticulo)entidadLista).ModoGestion = AsignaCadena("ModoGestion");
            ((BuscaArticulo)entidadLista).ContenidoVariable = AsignaBool("ContenidoVariable");
            ((BuscaArticulo)entidadLista).UnidadesContenido = AsignaDecimal("UnidadesContenido");
            ((BuscaArticulo)entidadLista).IdTipoPartida = AsignaEnteroNull("IdTipoPartida");
            ((BuscaArticulo)entidadLista).StockUM = AsignaDecimal("StockUM");
            ((BuscaArticulo)entidadLista).StockUV = AsignaDecimal("StockUV");
            ((BuscaArticulo)entidadLista).IdTipoIva = AsignaEnteroNull("IdTipoIva");
        }
        private void metodoRellenoVidrio(object entidadLista)
        {
            ((Vidrio)entidadLista).IDVidrio = AsignaEntero("IdArticulo");
            ((Vidrio)entidadLista).IDTipoVidrio = AsignaEntero("IDTipoVidrio");
            ((Vidrio)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((Vidrio)entidadLista).Imagen = AsignaCadena("Imagen");
            ((Vidrio)entidadLista).PosVer = AsignaEntero("PosVer");
            ((Vidrio)entidadLista).PosHor = AsignaEntero("PosHor");
            ((Vidrio)entidadLista).SpanVer = AsignaEntero("SpanVer");
            ((Vidrio)entidadLista).SpanHor = AsignaEntero("SpanHor");
        }
        private void metodoRellenoTipoVidrio(object entidadLista)
        {
            ((TipoVidrio)entidadLista).IDTipoVidrio = AsignaEntero("IDTipoVidrio");
            ((TipoVidrio)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((TipoVidrio)entidadLista).Imagen = AsignaCadena("Imagen");
        }
        private void metodoRellenoCarroceria(object entidadLista)
        {
            ((Carroceria)entidadLista).IDCarroceria = AsignaEntero("IDCarroceria");
            ((Carroceria)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((Carroceria)entidadLista).Imagen = AsignaCadena("Imagen");
        }
        private void metodoRellenoTipoVehiculo(object entidadLista)
        {
            ((TipoVehiculo)entidadLista).IDTipoVehiculo = AsignaEntero("IDTipoVehiculo");
            ((TipoVehiculo)entidadLista).IDGenerico = AsignaEntero("IDGenerico");
            ((TipoVehiculo)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((TipoVehiculo)entidadLista).Imagen = AsignaCadena("Imagen");
            ((TipoVehiculo)entidadLista).NombreTipo = AsignaCadena("NombreTipo");

        }
        private void metodoRellenoMarcaModelo(object entidadLista)
        {
            ((MarcaModelo)entidadLista).IDMarcaModelo = AsignaEntero("IDMarcaModelo");
            ((MarcaModelo)entidadLista).IDFamilia = AsignaEntero("IDFamilia");
            ((MarcaModelo)entidadLista).Descripcion = AsignaCadena("Descripcion");
            ((MarcaModelo)entidadLista).Imagen = AsignaCadena("Imagen");
        }
    }
}
