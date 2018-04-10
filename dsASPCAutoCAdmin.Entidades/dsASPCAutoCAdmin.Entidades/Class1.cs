using dsCore.Comun;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [DataMember]
        public string Eurocode { get; set; }
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
        [DataMember]
        public string Eurocode { get; set; }
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
        public int BloquearCambioAuto { get; set; }
        [DataMember]
        public Boolean BloquearCambioAutoB { get; set; }
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
        public int? IdCategoria { get; set; }
        [DataMember]
        public List<ModificarAccesorio> accesoriosinsertar { get; set; }
        [DataMember]
        public int IdArticuloCategoria { get; set; }
        [DataMember]
        public string DescripcionCategoria { get; set; }
        [DataMember]
        public List<ArticuloCarroceria> Carrocerias { get; set; }
        [DataMember]
        public int UnClick { get; set; }
        [DataMember]
        public Boolean UnClickB { get; set; }
        [DataMember]
        public Boolean NovedadB { get; set; }
        [DataMember]
        public int Novedad { get; set; }
        [DataMember]
        public Boolean CambioAutoB { get; set; }
        [DataMember]
        public int CambioAuto { get; set; }
        
    }
    public class UsuarioDatosEmail
    {
        public int IdUsuarioWeb { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid GuidValidacion { get; set; }
        public string NombreCompleto { get; set; }
        public string EmaildsWin { get; set; }
        public Guid? GuidRecuperacion { get; set; }
        public bool Activo { get; set; }
        public int IdClienteDelegacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public string ipAddress { get; set; }
    }
    public class PaginacionClientes
    {
        public int pagina { get; set; }
        public int bloque { get; set; }
        public string nCliente { get; set; }
    }
    public class BuscaClientes
    {
        public int NumReg { get; set; }
        public List<Cliente> Clientes { get; set; }
        public PaginacionClientes Busqueda { get; set; }
    }
    public class Cliente
    {
        public int IDCliente { get; set; }
        public string Delegacion { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public string Cif { get; set; }
        public string NombreMunicipio { get; set; }
        public string NombreProvincia { get; set; }
        public int IDUsuarioWeb { get; set; }
    }
    public class BusquedaUsuarios
    {
        public int IdInicio { get; set; }
        public int tamPag { get; set; }
        public string cadena { get; set; }
        public int activo { get; set; }
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
        public int IDFamilia { get; set; }
        [DataMember]
        public string DescripcionFamilia { get; set; }
        [DataMember]
        public string DescripcionSeccion { get; set; }
        [DataMember]
        public Boolean Eliminar { get; set; }
        public int EliminarInt { get; set; }
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




    //NO ADMIN




    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
    }
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class PedidoWeb
    {
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdDelegacion { get; set; }
        public int IdDomiEnt { get; set; }
        public decimal ImporteBruto { get; set; }
        public decimal PoDtoCom { get; set; }
        public decimal PoDtoPP { get; set; }
        public decimal ImportePortes { get; set; }
        public int IdTipoIvaPortes { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalCuotaIva { get; set; }
        public decimal TotalCuotaRE { get; set; }
        public decimal ImporteLiquido { get; set; }
        public decimal PoRecFinanciero { get; set; }
        public decimal ImporteRecFin { get; set; }
        public decimal ImporteDocumento { get; set; }
        public string Observaciones { get; set; }
        public int IdRegimenIva { get; set; }
        public Boolean AplicarRe { get; set; }
        public Boolean AplicarIva { get; set; }
        public List<LineaPedidoVentas> LineasPedido { get; set; }
        public List<LineaIva> LineasIva { get; set; }
    }
    public class LineaPedidoVentas
    {
        public int IdArticulo { get; set; }
        public int IdLinDocumento { get; set; }
        public int? IdUnidadValoracion { get; set; }
        public int? IdUnidadVenta { get; set; }
        public int IdPoIva { get; set; }
        public decimal PoIva { get; set; }
        public int PoRE { get; set; }
        public int IdUnidadManipulacion { get; set; }
        public int CantidadPalets { get; set; }
        public int CantidadXPalet { get; set; }
        public int CantidadUM { get; set; }
        public int CantidadXUM { get; set; }
        public int CantidadUV { get; set; }
        public decimal Precio { get; set; }
        public int PoDto1 { get; set; }
        public int PoDto2 { get; set; }
        public decimal ImporteBruto { get; set; }
        public decimal ImporteDtosLinea { get; set; }
        public decimal ImporteBonificaciones { get; set; }
        public decimal BonifPrecio { get; set; }
        public decimal ImporteNeto { get; set; }
        public int ImporteDtoComercial { get; set; }
        public decimal ImporteDtoPP { get; set; }
        public int? IdCargo { get; set; }
        public decimal ValorOtrosCargos { get; set; }
        public decimal ImporteOtrosCargos { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal ImporteCuotaIva { get; set; }
        public decimal ImporteCuotaRE { get; set; }
        public decimal ImporteLiquido { get; set; }
        public int? IdPromocion { get; set; }
        public Boolean Bonificada { get; set; }
        public string TipoTrans { get; set; }
    }
    public class LineaIva
    {
        public int IdLineaIva { get; set; }
        public int IdDocumento { get; set; }
        public int IdTipoIva { get; set; }
        public decimal PoIva { get; set; }
        public decimal PoRE { get; set; }
        public decimal BaseBruta { get; set; }
        public decimal BaseImponible { get; set; }
        public decimal CuotaIva { get; set; }
        public decimal CuotaRE { get; set; }
        public Boolean Manual { get; set; }
        public string TipoTrans { get; set; }
    }
    [DataContract]
    public class CampoBusqueda
    {
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
    }
    public class BusquedaArticulos
    {
        public int idCliente { get; set; }
        public int? idDelegacion { get; set; }
        public int? idSeccion { get; set; }
        public int? idFamilia { get; set; }
        public int? idGenerico { get; set; }
        public int pagina { get; set; }
        public int tamanoPagina { get; set; }
        public string orderBy { get; set; }
        public string tipoOrden { get; set; }
        public string valorFuncion { get; set; }
        public int forzarImagenes { get; set; }
    }
    public class ListadoArticulos
    {
        public List<Articulo> Articulos { get; set; }
        public int NumReg { get; set; }
        public int NumPags { get; set; }
    }
    public class Articulo
    {
        public int idArticulo { get; set; }
        public string GTINUC { get; set; } //TAMPOCO SE SI ES UNA Cadena
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodUdValor { get; set; }
        public string DescUdValor { get; set; }
        public string Imagen { get; set; }
        public Guid? RowGuid { get; set; }
        public int idSeccion { get; set; }
        public int? idFamilia { get; set; }
        public int? idGenerico { get; set; }
        public int idTipoIva { get; set; }
        public int? idArtProm { get; set; }
        public List<UnidadManipulacion> UnidadesManipulacion { get; set; }
    }
    public class UnidadManipulacion
    {
        public int idUnidadManipulacion { get; set; }
        public int idArticulo { get; set; }

        public string GTIN { get; set; } //AUN NO SE SI ES CADENA O NO
        public string DescripcionUM { get; set; }
        public string CodUdVenta { get; set; }
        public string DescUdVenta { get; set; }
        public string ModoContenido { get; set; }
        public decimal UnidadesContenido { get; set; }
        public decimal PrecioTarifa { get; set; }
        public int idAcumuladoUdMan { get; set; }
        public decimal StockFinalUV { get; set; }
        public string NombreAlmacen { get; set; }
    }
    [DataContract]
    public class BusquedaRapida
    {
        [DataMember]
        public List<ArticuloBasico> Articulos { get; set; }
        [DataMember]
        public int NumReg { get; set; }
    }
    [DataContract]
    public class ArticuloBasico
    {
        [DataMember]
        public int IDArticulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
    }

    public class UsuarioWeb
    {
        public int IdUsuarioWeb { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public Cliente Cliente { get; set; }
        public List<Domicilio> Domicilios { get; set; }
        public List<Promocion> Promociones { get; set; }
    }
    public class RecuperacionPassword
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public string repeatpassword { get; set; }
        public string username { get; set; }
    }
    public class UsuarioError
    {
        public string nombre { get; set; }
        public string motivo { get; set; }
    }
    public class ResultadoInvitaciones
    {
        public List<string> Enviadas { get; set; }
        public List<UsuarioError> UsuariosError { get; set; }
    }
    public class ResultadoRegistro
    {
        public int? Resultado { get; set; }
        public string Cadena { get; set; }
        public PropiedadesSitio Propiedades { get; set; }
        public UsuarioDatosEmail Usuario { get; set; }
        public string CadenaMail { get; set; }
    }
    public class ResultadoAsignacion
    {
        public UsuarioDatosEmail Usuario { get; set; }
        public int Resultado { get; set; }
        public PropiedadesSitio Propiedades { get; set; }
        public string Cadena { get; set; }
    }
    public class ResultadoRecuperacionContrasena
    {
        public int Resultado { get; set; }
        public PropiedadesSitio Propiedades { get; set; }
        public string Cadena { get; set; }
    }
    public class SolicitudRegistro
    {
        public string nombre { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string informacion { get; set; }
        public string cif { get; set; }
        public string empresa { get; set; }
        public string empresawildcards { get; set; }

    }
    public class SolicitudRecuperacion
    {
        public string email { get; set; }
    }
    public class ResultadoValidacionGuidRecuperacion
    {
        public int Resultado { get; set; }
        public UsuarioDatosEmail Usuario { get; set; }
    }
    public class Domicilio
    {
        public int IDDomicilioCliente { get; set; }
        public int IdCliente { get; set; }
        public int IdDomicilioRelacion { get; set; }
        public int IdRelacion { get; set; }
        public int IdTipoIva { get; set; }
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string PisoPuerta { get; set; }
        public int IdLocalidad { get; set; }
        public string NombreMunicipio { get; set; }
        public int CodPostal { get; set; }
        public int IdProvincia { get; set; }
        public string NombreProvincia { get; set; }
        public int IdPais { get; set; }
        public string NombreDomicilio { get; set; }
        public string TipoDomicilio { get; set; }
        public int Venta { get; set; }
        public int Entrega { get; set; }
        public int Cobro { get; set; }
        public int IdTipoDomicilio { get; set; }
        public string NombrePais { get; set; }
        public string ApdoPostal { get; set; }
    }
    public class Promocion
    {
        public int IDPromocion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public Guid? RowGuid { get; set; }
    }
    [DataContract]
    public class Carrito
    {
        [DataMember]
        public List<ArticuloCarrito> Articulos { get; set; }
        [DataMember]
        public int? IDUsuario { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
    }
    [DataContract]
    public class ArticuloCarrito
    {
        [DataMember]
        public int IDArticulo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Cantidad { get; set; }
        [DataMember]
        public decimal Precio { get; set; }
        [DataMember]
        public decimal PrecioUd { get; set; }
    }

    public class UnClickYNovedades
    {
        public List<BuscaArticulo> UnClick { get; set; }
        public List<BuscaArticulo> Novedades { get; set; }
    }
    [DataContract]
    public class Ano
    {
        [DataMember]
        public int Valor { get; set; }
        [DataMember]
        public int CantidadArticulos { get; set; }
    }
    public class Parametros
    {
        public int? idModeloCarroceria;
        public int? ano;

        public int? idTipoVehiculo { get; set; }
        public int? idSeccion { get; set; }
        public int? idFamilia { get; set; }
        public string descripcionTipoVehiculo { get; set; }
        public string descripcionSeccion { get; set; }
        public string descripcionFamilia { get; set; }
        public int? idVidrio { get; set; }
        public string descripcionTipoVidrio { get; set; }
        public string descripcionCarroceria { get; set; }
        public int? idCarroceria { get; set; }
        public int? idTipoVidrio { get; set; }
    }
    [DataContract]
    public class BuscadorMarcas
    {
        [DataMember]
        public List<Marca> Marcas { get; set; }
        [DataMember]
        public List<Inicial> Iniciales { get; set; }
        public List<Modelo> Modelos { get; set; }
    }
    [DataContract]
    public class Inicial
    {
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public List<Marca> Marcas { get; set; }
        public List<Modelo> Modelos { get; set; }
    }
    public class ArticulosYCategorias
    {
        public List<BuscaArticulo> Articulos { get; set; }
        public List<Categoria> Accesorios { get; set; }
        public Parametros Parametros { get; set; }
    }


    public class dsMail
    {
        public static string EnviarEmail(MailEnvio mail_envio)
        {
            string res = "";
            //construye el mensaje
            MimeMessage message = BuildMessage(mail_envio);
            //realiza el envio
            res = SendBySMTP(message, mail_envio.Cuenta);
            return res;
            //return "holi";
        }
        private static MimeMessage BuildMessage(MailEnvio mail_envio)
        {
            Cuenta cuenta = mail_envio.Cuenta;
            Mail mail = mail_envio.Mail;

            //asigna remitente / destinatario
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(cuenta.NombreCuenta, cuenta.Usuario));

            string[] rcpts = mail.Destinatario.Split(';');
            if (rcpts.Length == 0)
                message.To.Add(new MailboxAddress(mail.Destinatario));
            else
            {
                foreach (string recipient in rcpts)
                    message.To.Add(new MailboxAddress(recipient));
            }

            if (!string.IsNullOrWhiteSpace(mail.CC))
            {
                string[] rcptsc = mail.CC.Split(';');
                if (rcptsc.Length == 0)
                    message.Cc.Add(new MailboxAddress(mail.CC));
                else
                {
                    foreach (string recipient in rcptsc)
                        message.Cc.Add(new MailboxAddress(recipient));
                }
                //message.Cc.Add(new MailboxAddress(mail.CC));
            }

            if (!string.IsNullOrWhiteSpace(mail.CCO))
            {
                string[] rcptsco = mail.CCO.Split(';');
                if (rcptsco.Length == 0)
                    message.Bcc.Add(new MailboxAddress(mail.CCO));
                else
                {
                    foreach (string recipient in rcptsco)
                        message.Bcc.Add(new MailboxAddress(recipient));
                }
                //message.Bcc.Add(new MailboxAddress(mail.CCO));
            }
            message.Subject = mail.Asunto;

            //construye el mensaje
            var builder = new BodyBuilder();
            builder.HtmlBody = mail.Texto;
            //if (html != "") builder.HtmlBody = html;
            //else builder.TextBody = mail.Texto;
            if (mail.ListaAdjuntos != null && mail.ListaAdjuntos.Count > 0)
            {
                foreach (AdjuntoMail adjunto in mail.ListaAdjuntos.OrderBy(o => o.Orden))
                    builder.Attachments.Add(adjunto.Archivo); // @"C:\Users\Joey\Documents\party.ics"); 
            }

            //string banner = System.IO.File.ReadAllText(@"C:\Temp\Firmagenerica.html");
            if (mail.Firmar && !string.IsNullOrWhiteSpace(cuenta.Firma))
                builder.HtmlBody += cuenta.Firma;
            //var image = builder.LinkedResources.Add(@"C:\Temp\Firmagenerica.html");
            //image.ContentId = MimeUtils.GenerateMessageId();

            message.Body = builder.ToMessageBody();

            return message;
        }
        private static string SendBySMTP(MimeMessage message, Cuenta cuenta)
        {
            string res = "error";

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(cuenta.ServCorreoSal, (int)cuenta.PuertoCorreoSal, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(cuenta.Usuario, cuenta.Clave);
                client.Send(message);
                client.Disconnect(true);

                res = "ok";
            }

            return res;
        }
        static string billede1 = string.Empty;
        static string billede2 = string.Empty;
    }
    public class PropiedadesSitio
    {
        public string NombreCuenta { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string ServCorreoSal { get; set; }
        public int PuertoCorreoSal { get; set; }
        public string CorreoRegistro { get; set; }
        public string URLSitio { get; set; }
        public string CorreoVerificacion { get; set; }
        public string CorreoInvitacion { get; set; }
        public string CorreoConfirmacion { get; set; }
        public string RutaValidacion { get; set; }
        public string NombreSitio { get; set; }
        public string RutaLogo { get; set; }
        public string CorreoRecuperacion { get; set; }
        public string RutaRecuperacion { get; set; }
    }
    [DataContract]
    public class EmpresaWeb
    {
        [DataMember]
        public int IdDatosWeb { get; set; }
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public Guid GuidImg { get; set; }
        [DataMember]
        public Guid GuidIcono { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string CodPostal { get; set; }
        [DataMember]
        public string Localidad { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Web { get; set; }
        [DataMember]
        public string PaginaFacebook { get; set; }
        [DataMember]
        public string PaginaTwitter { get; set; }
        [DataMember]
        public string PaginaGooglePlus { get; set; }
        [DataMember]
        public string PaginaPinterest { get; set; }
        [DataMember]
        public string PaginaLinkedIn { get; set; }
        [DataMember]
        public string PaginaInstagram { get; set; }
        [DataMember]
        [DataType(DataType.MultilineText)]
        public string AcercaDe { get; set; }
        [DataMember]
        public int IdClienteVentaDirecta { get; set; }
        [DataMember]
        public Boolean VisiblePedidos { get; set; }
        [DataMember]
        public Boolean VisibleFacturas { get; set; }
        [DataMember]
        public Boolean VisibleFinanzas { get; set; }
        [DataMember]
        public Boolean VisibleCatalogo { get; set; }
        [DataMember]
        public Boolean VisibleCuenta { get; set; }
        [DataMember]
        public Boolean VisibleIdiomas { get; set; }
        [DataMember]
        public Boolean VisibleMensajes { get; set; }
        [DataMember]
        public Boolean VisiblePlantillas { get; set; }
        [DataMember]
        public Boolean VisibleInvitado { get; set; }
        [DataMember]
        public Boolean VisibleVentaDirecta { get; set; }
        [DataMember]
        public string NombreCuenta { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string ServCorreoSal { get; set; }
        [DataMember]
        public int PuertoCorreoSal { get; set; }
        [DataMember]
        public string NombreSitio { get; set; }
        [DataMember]
        public string RutaLogo { get; set; }
        [DataMember]
        public string dirEmailContacto { get; set; }
        [DataMember]
        public Boolean VisibleCategorias { get; set; }
        [DataMember]
        public Boolean VisibleVehiculos { get; set; }
        [DataMember]
        public Boolean VisibleNovedades { get; set; }
        [DataMember]
        public Boolean VisibleExpress { get; set; }
        [DataMember]
        public Boolean VisibleUltimosPedidos { get; set; }
        [DataMember]
        public Boolean VisibleIP { get; set; }
        [DataMember]
        public Boolean VisibleUltimaConexion { get; set; }
        [DataMember]
        public Boolean VisibleEurocodeListado { get; set; }
        [DataMember]
        public Boolean VisibleEurocodeFicha { get; set; }
        [DataMember]
        public Boolean VisibleAlmacenesListado { get; set; }
        [DataMember]
        public Boolean VisibleAlmacenesFicha { get; set; }
        [DataMember]
        public string Copyright { get; set; }
    }
    [DataContract]
    public class ImagenCabWeb
    {
        [DataMember]
        public int IdImagen { get; set; }
        [DataMember]
        public int IdEmpresa { get; set; }
        [DataMember]
        public Guid RowGuid { get; set; }
        [DataMember]
        public string ImagenSt { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Subtitulo { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public int tipoTransaccion { get; set; }
    }
    [DataContract]
    public class MensajeWeb
    {
        [DataMember]
        public int IdMensaje { get; set; }
        [DataMember]
        public int IdCliente { get; set; }
        [DataMember]
        public int IdUsuarioWeb { get; set; }
        [DataMember]
        public int Prioridad { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        [DataType(DataType.MultilineText)]
        public string Mensaje { get; set; }
        [DataMember]
        public DateTime? FechaEnvio { get; set; }
        [DataMember]
        public DateTime? FechaLeido { get; set; }
        [DataMember]
        public Boolean Leido { get; set; }
        [DataMember]
        public string TipoTransaccion { get; set; }
        [DataMember]
        public string Cliente { get; set; }
    }
}
