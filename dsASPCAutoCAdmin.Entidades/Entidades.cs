﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace dsASPCAutoCAdmin.Entidades
{
    public class ElementoMenu
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class TipoVidrio
    {
        public int IDTipoVidrio { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }
    public class Carroceria
    {
        public int IDCarroceria { get; set; }
        public string Descripcion { get; set; }
        public List<Vidrio> Vidrios { get; set; }
        public string Imagen { get; set; }
    }
    public class ResultadoIM
    {
        public int Identidad { get; set; }
        public byte[] TS { get; set; }
        public string Resultado { get; set; }
    }
    public class CadenasBusqueda
    {
        public string Vista { get; set; }
        public string EntidadFuncion { get; set; }
        public string CampoClave { get; set; }
        public string Entidad { get; set; }
        public string CampoOrdenacion { get; set; }
    }
    [DataContract]
    public class BusquedaPaginada
    {
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string cadena { get; set; }
        [DataMember]
        public int? numPag { get; set; }
        [DataMember]
        public string LastValor { get; set; }
        [DataMember]
        public int LastIndice { get; set; }
        [DataMember]
        public string AccionPagina { get; set; }
        [DataMember]
        public string FirstValor { get; set; }
        [DataMember]
        public int FirstIndice { get; set; }
        [DataMember]
        public int? idSeccion { get; set; }
    }
    public class Vidrio
    {
        public int IDVidrio { get; set; }
        public int IDTipoVidrio { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int PosVer { get; set; }
        public int PosHor { get; set; }
        public int SpanVer { get; set; }
        public int SpanHor { get; set; }
        public string DescripcionTipoVidrio { get; set; }
        public int Modificador { get; set; }
    }
    public class TipoVehiculo
    {
        public int IDGenerico { get; set; }
        public string Imagen { get; set; }
        public string DescripcionGenerico { get; set; }
        public string CodigoGenerico { get; set; }
        //public int IdFamilia { get; set; }
    }
    /*public class MarcaModelo
    {
        public int IDMarcaModelo { get; set; }
        public int IDFamilia { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }*/
    public class Marca
    {
        public int IDSeccion { get; set; }
        public string DescripcionSeccion { get; set; }
        public string Imagen { get; set; }
        public string CodigoSeccion { get; set; }
    }
    public class Modelo
    {
        public int IDFamilia { get; set; }
        public string DescripcionFamilia { get; set; }
        public string Imagen { get; set; }
        public string CodigoFamilia { get; set; }
        public int IdSeccion { get; set; }
        public string descripcionSeccion { get; set; }
    }
    [DataContract]
    [Serializable]
    public class BuscaArticulo
    {
        [DataMember]
        public int IdArticulo { get; set; }

        [DataMember]
        public int? IdUnidadManipulacion { get; set; }

        [DataMember]
        public int? IdUnidadValoracion { get; set; }

        [DataMember]
        public int? IdMedidaUM { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string DescripcionUM { get; set; }

        [DataMember]
        public string GTINUC { get; set; }

        [DataMember]
        public string GTINUM { get; set; }

        [DataMember]
        public string ModoGestion { get; set; }

        [DataMember]
        public bool ContenidoVariable { get; set; }

        [DataMember]
        public decimal UnidadesContenido { get; set; }

        [DataMember]
        public decimal StockUM { get; set; }

        [DataMember]
        public decimal StockUV { get; set; }

        [DataMember]
        public string strStock { get; set; }

        [DataMember]
        public int? IdTipoPartida { get; set; }

        [DataMember]
        public int? IdTipoIva { get; set; }
        public int? IdFamilia { get; set; }
        public int? IdSeccion { get; set; }
        public int? IdGenerico { get; set; }
        public string DescripcionFamilia { get; set; }
        public string DescripcionSeccion { get; set; }
        public string DescripcionGenerico { get; set; }
    }
    public class MensajeRespuesta
    {
        public bool Existente { get; set; }
        public string Contenido { get; set; }
        public string Archivo { get; set; }
        public long Tamano { get; set; }
    }
    public class Tamano
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Res { get; set; }
    }
}
