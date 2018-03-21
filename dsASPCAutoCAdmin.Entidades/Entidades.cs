using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace dsASPCAutoCAdmin.Entidades
{
    public class ElementoMenu
    {

        public string Key { get; set; }
        public string Value { get; set; }
        public string Img { get; set; }
    }
    [DataContract]
    public class TipoVidrio
    {
        [DataMember]
        public int IDTipoVidrio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
    }
    [DataContract]
    public class Carroceria
    {
        [DataMember]
        public int IDCarroceria { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<Vidrio> Vidrios { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public int IDModeloCarroceria { get; set; }
    }
    [DataContract]
    public class ResultadoIM
    {
        [DataMember]
        public int Identidad { get; set; }
        [DataMember]
        public byte[] TS { get; set; }
        [DataMember]
        public string Resultado { get; set; }
    }
    [DataContract]
    public class CadenasBusqueda
    {
        [DataMember]
        public string Vista { get; set; }
        [DataMember]
        public string EntidadFuncion { get; set; }
        [DataMember]
        public string CampoClave { get; set; }
        [DataMember]
        public string Entidad { get; set; }
        [DataMember]
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
    [DataContract]
    public class Vidrio
    {
        [DataMember]
        public int IDVidrio { get; set; }
        [DataMember]
        public int IDTipoVidrio { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public int PosVer { get; set; }
        [DataMember]
        public int PosHor { get; set; }
        [DataMember]
        public int SpanVer { get; set; }
        [DataMember]
        public int SpanHor { get; set; }
        [DataMember]
        public string DescripcionTipoVidrio { get; set; }
        [DataMember]
        public int Modificador { get; set; }
    }
    [DataContract]
    public class TipoVehiculo
    {
        [DataMember]
        public int IDTipoVehiculo { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        //public int IdFamilia { get; set; }
    }
    /*public class MarcaModelo
    {
        public int IDMarcaModelo { get; set; }
        public int IDFamilia { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }*/
    [DataContract]
    public class Marca
    {
        [DataMember]
        public int IDSeccion { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string CodigoSeccion { get; set; }
    }
    [DataContract]
    public class Modelo
    {
        [DataMember]
        public int IDFamilia { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string CodigoFamilia { get; set; }
        [DataMember]
        public int? IdSeccion { get; set; }
        [DataMember]
        public string descripcionSeccion { get; set; }
        [DataMember]
        public int idTipoVehiculo { get; set; }
        [DataMember]
        public string descripcionTipoVehiculo { get; set; }
        [DataMember]
        public List<Carroceria> Carrocerias { get; set; }
        [DataMember]
        public List<Carroceria> CarroceriasEliminadas { get; set; }
        [DataMember]
        public List<ImagenFamilia> Imagenes { get; set; }
        [DataMember]
        public List<ImagenFamilia> ImagenesEliminadas { get; set; }
    }
    [DataContract]
    public class ImagenFamilia
    {
        [DataMember]
        public int IDImagenFamilia { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public int IDFamilia { get; set; }
        public string holi { get; set; }
    }
    public class Categoria
    {
        public int IDCategoria { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public List<BuscaArticulo> Articulos { get; set; }
        public int IdArticuloCategoria { get; set; }
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
        [DataMember]
        public int? IdFamilia { get; set; }
        [DataMember]
        public int? IdSeccion { get; set; }
        [DataMember]
        public int? IdTipoVidrio { get; set; }
        [DataMember]
        public int? IdGenerico { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
        [DataMember]
        public string DescripcionCorta { get; set; }
        [DataMember]
        public string DescripcionTipoVidrio { get; set; }
        [DataMember]
        public string DescripcionDetallada { get; set; }
        [DataMember]
        public string DescripcionWeb1 { get; set; }
        [DataMember]
        public string DescripcionWeb2 { get; set; }
        [DataMember]
        public int AnoInicial { get; set; }
        [DataMember]
        public int AnoFinal { get; set; }
        [DataMember]
        public List<Categoria> Accesorios { get; set; }
        [DataMember]
        public int IdCategoria { get; set; }
        [DataMember]
        public List<ModificarAccesorio> accesoriosinsertar { get; set; }
        [DataMember]
        public int IdArticuloCategoria { get; set; }
        [DataMember]
        public string DescripcionCategoria { get; set; }
        [DataMember]
        public List<ArticuloCarroceria> Carrocerias { get; set; }
        //[DataMember]
        //public List<ModificarAccesorio> accesorioseliminar { get; set; }
    }
    [DataContract]
    public class ArticuloCarroceria
    {
        [DataMember]
        public int IDModeloCarroceria { get; set; }
        [DataMember]
        public string DescripcionCarroceria { get; set; }
        [DataMember]
        public string Anos { get; set; }
        [DataMember]
        public string DescripcionArticuloModelo { get; set; }
        [DataMember]
        public int IDArticuloModelo { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
    }
    [DataContract]
    public class MensajeRespuesta
    {
        [DataMember]
        public bool Existente { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public string Archivo { get; set; }
        [DataMember]
        public long Tamano { get; set; }
    }
    [DataContract]
    public class Tamano
    {
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Res { get; set; }
    }
    [DataContract]
    public class ModificarAccesorio
    {
        [DataMember]
        public int? IDArticuloCategoria { get; set; }
        [DataMember]
        public int? IDCategoria { get; set; }
        [DataMember]
        public int? IDArticuloRel { get; set; }
        [DataMember]
        public int TipoOperacion { get; set; }
    }
}
