using dsASPCAutoCAdmin.Entidades;
using dsCore2.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace dsASPCAutoCAdmin.DataAccess
{
    public class AdaptadorAtcAdmin : BaseDataAccess
    {
        private readonly IConfiguration _configuration;
        public AdaptadorAtcAdmin(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TipoVehiculo TiposVehiculoLeerPorID(int IDTipoVehiculo)
        {
            var res = new TipoVehiculo();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", IDTipoVehiculo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVehiculoLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDTipoVehiculo = AsignaEntero("IDTipoVehiculo");
                    res.Imagen = AsignaCadena("Imagen");
                    res.Descripcion = AsignaCadena("Descripcion");

                }
            }
            return res;
        }
        public List<TipoVehiculo> TiposVehiculoLeer()
        {
            var res = new List<TipoVehiculo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVehiculoLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vh = new TipoVehiculo
                    {
                        IDTipoVehiculo = AsignaEntero("IDTipoVehiculo"),
                        Imagen = AsignaCadena("Imagen"),
                        Descripcion = AsignaCadena("Descripcion")
                    };
                    res.Add(vh);
                }
            }
            return res;
        }
        public BuscaArticulo ArticulosLeerPorID(int IDArticulo)
        {
            var res = new BuscaArticulo();
            var Accesorios = new List<BuscaArticulo>();
            res.Carrocerias = new List<ArticuloCarroceria>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                    new SqlParameter("@Admin", 1),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IdArticulo = AsignaEntero("IdArticulo");
                    res.Codigo = AsignaCadena("Codigo");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.IdFamilia = AsignaEnteroNull("IdFamilia");
                    res.IdSeccion = AsignaEnteroNull("IdSeccion");
                    res.IdGenerico = AsignaEnteroNull("IdGenerico");
                    res.DescripcionFamilia = AsignaCadena("DescripcionFamilia");
                    res.DescripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.DescripcionCorta = AsignaCadena("DescripcionCorta");
                    res.DescripcionDetallada = AsignaCadena("DescripcionDetallada");
                    res.IdTipoVidrio = AsignaEntero("IdTipoVidrio");
                    res.DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio");
                    res.DescripcionWeb1 = AsignaCadena("DescripcionWeb1");
                    res.DescripcionWeb2 = AsignaCadena("DescripcionWeb2");
                    res.AnoInicial = AsignaEntero("AnoInicial");
                    res.AnoFinal = AsignaEntero("AnoFinal");
                    res.IdCategoria = AsignaEntero("IDCategoria");
                    res.DescripcionCategoria = AsignaCadena("DescripcionCategoria");
                    res.UnClick = AsignaEntero("UnClick");
                    res.Novedad = AsignaEntero("Novedad");
                    res.CambioAuto = AsignaEntero("BloquearCambioAuto");
                }
                res.Accesorios = new List<Categoria>();
                _reader.NextResult();
                while (_reader.Read())
                {
                    var cat = new Categoria
                    {
                        IDCategoria = AsignaEntero("IDCategoria"),
                        Descripcion = AsignaCadena("Descripcion"),
                        IdArticuloCategoria = AsignaEntero("IDArticuloCategoria"),
                        Articulos = new List<BuscaArticulo>(),
                    };
                    res.Accesorios.Add(cat);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo();
                    ar.IdArticulo = AsignaEntero("IdArticuloRel");
                    ar.Descripcion = AsignaCadena("DescripcionArticuloRel");
                    ar.IdCategoria = AsignaEntero("IdCategoria");
                    ar.IdArticuloCategoria = AsignaEntero("IDArticuloCategoria");
                    Accesorios.Add(ar);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var carr = new ArticuloCarroceria
                    {
                        IDModeloCarroceria = AsignaEntero("IDModeloCarroceria"),
                        DescripcionCarroceria = AsignaCadena("DescripcionCarroceria"),
                        Anos = AsignaCadena("Anos"),
                        DescripcionArticuloModelo = AsignaCadena("DescripcionArticuloModelo"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        IDArticuloModelo = AsignaEntero("IDArticuloModelo"),
                        IDFamilia = AsignaEntero("IDFamilia"),
                        EliminarInt = AsignaEntero("Eliminado"),
                    };
                    if (carr.EliminarInt > 0)
                    {
                        carr.Eliminar = true;
                    }
                    res.Carrocerias.Add(carr);
                }
            }
            foreach (Categoria cat in res.Accesorios)
            {
                foreach (BuscaArticulo ar in Accesorios)
                {
                    if (ar.IdCategoria == cat.IDCategoria)
                    {
                        cat.Articulos.Add(ar);
                    }
                }
            }
            if (res.UnClick > 0)
            {
                res.UnClickB = true;
            }
            if (res.Novedad > 0)
            {
                res.NovedadB = true;
            }
            if (res.CambioAuto > 0)
            {
                res.CambioAutoB = true;
            }
            return res;
        }
        public List<BuscaArticulo> ArticulosLeerPorCategoria(int IDCategoria)
        {
            var res = new List<BuscaArticulo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDCategoria", IDCategoria),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerPorCategoria", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticulo"),
                        Codigo = AsignaCadena("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),

                    };
                    res.Add(ar);
                }

            }
            return res;
        }
        public List<BuscaArticulo> ArticulosLeerPorCategoriaYCadena(int IDCategoria, string cadena)
        {
            var res = new List<BuscaArticulo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDCategoria", IDCategoria),
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosLeerPorCategoriaYCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var ar = new BuscaArticulo
                    {
                        IdArticulo = AsignaEntero("IdArticulo"),
                        Codigo = AsignaCadena("Codigo"),
                        Descripcion = AsignaCadena("Descripcion"),

                    };
                    res.Add(ar);
                }

            }
            return res;
        }
        public List<Categoria> CategoriasLeer()
        {
            var res = new List<Categoria>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CategoriasLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var ct = new Categoria
                    {
                        IDCategoria = AsignaEntero("IDCategoria"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Codigo = AsignaCadena("Codigo"),
                    };
                    res.Add(ct);
                }

            }
            return res;
        }
        public Modelo ModelosLeerPorID(int IDFamilia)
        {
            var res = new Modelo();
            res.Imagenes = new List<ImagenFamilia>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDFamilia", IDFamilia),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ModelosLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDFamilia = AsignaEntero("IDFamilia");
                    res.Imagen = AsignaCadena("Imagen");
                    res.IdSeccion = AsignaEntero("IDSeccion");
                    res.CodigoFamilia = AsignaCadena("CodigoFamilia");
                    res.descripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.DescripcionFamilia = AsignaCadena("DescripcionFamilia");
                    res.descripcionTipoVehiculo = AsignaCadena("DescripcionTipoVehiculo");
                    res.idTipoVehiculo = AsignaEntero("IdTipoVehiculo");
                    res.BloquearCambioAuto = AsignaEntero("BloquearCambioAuto");

                }
                res.Carrocerias = new List<Carroceria>();
                _reader.NextResult();
                while (_reader.Read())
                {
                    var cr = new Carroceria
                    {
                        Descripcion = AsignaCadena("Descripcion"),
                        IDCarroceria = AsignaEntero("IDCarroceria"),
                        IDModeloCarroceria = AsignaEntero("IDModeloCarroceria"),
                    };
                    res.Carrocerias.Add(cr);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var im = new ImagenFamilia
                    {
                        IDImagenFamilia = AsignaEntero("IDImagenFamilia"),
                        IDFamilia = AsignaEntero("IDFamilia"),
                        Valor = AsignaCadena("Valor"),
                    };
                    res.Imagenes.Add(im);
                }
            }
            if (res.BloquearCambioAuto > 0)
            {
                res.BloquearCambioAutoB = true;
            }
            return res;
        }
        public List<Modelo> ModelosLeerPorMarca(int IDSeccion)
        {
            var res = new List<Modelo>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", IDSeccion),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ModelosLeerPorMarca", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var md = new Modelo
                    {
                        IDFamilia = AsignaEntero("IDFamilia"),
                        Imagen = AsignaCadena("Imagen"),
                        IdSeccion = AsignaEntero("IDSeccion"),
                        CodigoFamilia = AsignaCadena("CodigoFamilia"),
                        descripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        DescripcionFamilia = AsignaCadena("DescripcionFamilia")
                    };
                   res.Add(md);
                }
                
            }
            return res;
        }
        public void CarroceriasEliminar(int IDCarroceria)
        {
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDCarroceria", IDCarroceria),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasEliminar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        public byte[] ImagenLeerPorIDArticulo(int IDArticulo)
        {
            byte[] res = null;
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDArticulo", IDArticulo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ImagenLeerPorIDArticulo", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res = AsignaArrayByte("Imagen");
                };
            }
            return res;
        }

        public Marca MarcasLeerPorID(int IDSeccion)
        {
            var res = new Marca();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", IDSeccion),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDSeccion = AsignaEntero("IDSeccion");
                    res.DescripcionSeccion = AsignaCadena("DescripcionSeccion");
                    res.Imagen = AsignaCadena("Imagen");
                    res.CodigoSeccion = AsignaCadena("CodigoSeccion");
                };
            }
            return res;
        }
        public List<Marca> MarcasLeerPorCadena(string cadena)
        {
            var res = new List<Marca>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var mc = new Marca
                    {
                        IDSeccion = AsignaEntero("IDSeccion"),
                        DescripcionSeccion = AsignaCadena("DescripcionSeccion"),
                        Imagen = AsignaCadena("Imagen"),
                        CodigoSeccion = AsignaCadena("CodigoSeccion")
                    };
                    res.Add(mc);
                };
            }
            return res;
        }

        public Carroceria CarroceriasLeerPorID(int IDCarroceria)
        {
            var res = new Carroceria();
            res.Vidrios = new List<Vidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDCarroceria", IDCarroceria),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDCarroceria = AsignaEntero("IDCarroceria");
                    res.Imagen = AsignaCadena("Imagen");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.Eurocode = AsignaCadena("Eurocode");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var vd = new Vidrio
                    {
                        IDVidrio = AsignaEntero("IDVidrio"),
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        PosVer = AsignaEntero("PosVer"),
                        PosHor = AsignaEntero("PosHor"),
                        SpanVer = AsignaEntero("SpanVer"),
                        SpanHor = AsignaEntero("SpanHor"),
                        Imagen = AsignaCadena("Imagen"),
                    };
                    res.Vidrios.Add(vd);
                }
            }
            return res;
        }

        public Vidrio VidriosLeerPorID(int IDVidrio)
        {
            var res = new Vidrio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDVidrio", IDVidrio),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.VidriosLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDVidrio = AsignaEntero("IDVidrio");
                    res.IDTipoVidrio = AsignaEntero("IDTipoVidrio");
                    res.DescripcionTipoVidrio = AsignaCadena("DescripcionTipoVidrio");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.Imagen = AsignaCadena("Imagen");
                }
            }
            return res;
        }

        public TipoVidrio TiposVidrioLeerPorID(int IDTipoVidrio)
        {
            var res = new TipoVidrio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVidrio", IDTipoVidrio),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioLeerPorID", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IDTipoVidrio = AsignaEntero("IDTipoVidrio");
                    res.Descripcion = AsignaCadena("Descripcion");
                    res.Imagen = AsignaCadena("Imagen");
                }
            }
            return res;
        }
        public void TiposVehiculoEliminar(int IDTipoVehiculo)
        {
            var res = new TipoVidrio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", IDTipoVehiculo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVehiculoEliminar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

        }
        public void TiposVidrioEliminar(int IDTipoVidrio)
        {
            var res = new TipoVidrio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVidrio", IDTipoVidrio),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioEliminar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

        }
        public void MarcasEliminar(int IDSeccion)
        {
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", IDSeccion),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasEliminar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

        }
        public List<TipoVidrio> TiposVidrioLeerPorCadena(string cadena)
        {
            var res = new List<TipoVidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vid = new TipoVidrio
                    {
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Imagen = AsignaCadena("Imagen"),
                    };
                    res.Add(vid);
                }
            }
            return res;
        }
        public List<TipoVidrio> TiposVidrioLeer()
        {
            var res = new List<TipoVidrio>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    //new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var vid = new TipoVidrio
                    {
                        IDTipoVidrio = AsignaEntero("IDTipoVidrio"),
                        Descripcion = AsignaCadena("Descripcion"),
                        Imagen = AsignaCadena("Imagen"),
                    };
                    res.Add(vid);
                }
            }
            return res;
        }
        public List<Carroceria> CarroceriasLeerPorCadena(string cadena)
        {
            var res = new List<Carroceria>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@cadena", cadena),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasLeerPorCadena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    var cr = new Carroceria
                    {
                        Descripcion = AsignaCadena("Descripcion"),
                        IDCarroceria = AsignaEntero("IDCarroceria"),
                        IDModeloCarroceria = AsignaEntero("IDModeloCarroceria"),
                    };
                    res.Add(cr);
                }
            }
            return res;
        }
        public ResultadoIM ArticulosModificar(BuscaArticulo bs)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            var cr = "";
            var carr = "";
            if (bs.UnClickB == true)
            {
                bs.UnClick = 1;
            }
            else
            {
                bs.UnClick = 0;
            }
            if (bs.NovedadB == true)
            {
                bs.Novedad = 1;
            }
            else
            {
                bs.Novedad = 0;
            }
            if (bs.CambioAutoB == true)
            {
                bs.CambioAuto = 1;
            }
            else
            {
                bs.CambioAuto = 0;
            }
            foreach (ArticuloCarroceria ac in bs.Carrocerias)
            {
                if (ac.Eliminar)
                {
                    ac.EliminarInt = 1;
                }
            }
            try
            {
                cr = dsCore.Comun.Ayudas.SerializarACadenaXML(bs.accesoriosinsertar);
            }
            catch
            {
                //Si está vacía la lista no nos importa
            }
            try
            {
                carr = dsCore.Comun.Ayudas.SerializarACadenaXML(bs.Carrocerias);
            }
            catch
            {
                //Si está vacía la lista no nos importa
            }
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", bs.IdSeccion),
                    new SqlParameter("@IDArticulo", bs.IdArticulo),
                    new SqlParameter("@IDFamilia", bs.IdFamilia),
                    new SqlParameter("@IDTipoVidrio", bs.IdTipoVidrio),
                    new SqlParameter("@IDCategoriaA", bs.IdCategoria),
                    new SqlParameter("@Codigo", bs.Codigo),
                    new SqlParameter("@Descripcion", bs.Descripcion),
                    new SqlParameter("@DescripcionCorta", bs.DescripcionCorta),
                    new SqlParameter("@DescripcionDetallada", bs.DescripcionDetallada),
                    new SqlParameter("@DescripcionWeb1", bs.DescripcionWeb1),
                    new SqlParameter("@DescripcionWeb2", bs.DescripcionWeb2),
                    new SqlParameter("@AnoInicial", bs.AnoInicial),
                    new SqlParameter("@AnoFinal", bs.AnoFinal),
                    new SqlParameter("@Accesorios", cr),
                    new SqlParameter("@Carrocerias", carr),
                    new SqlParameter("@UnClick", bs.UnClick),
                    new SqlParameter("@Novedad", bs.Novedad),
                    new SqlParameter("@BloquearCambioAuto", bs.CambioAuto),

                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ArticulosModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.TS = AsignaArrayByte("TS");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
        public ResultadoIM MarcasCrearModificar(Marca tiv)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", tiv.IDSeccion),
                    new SqlParameter("@DescripcionSeccion", tiv.DescripcionSeccion),
                    new SqlParameter("@CodigoSeccion", tiv.CodigoSeccion),
                    new SqlParameter("@Imagen", tiv.Imagen),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.MarcasCrearModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.TS = AsignaArrayByte("TS");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
        public ResultadoIM ModelosCrearModificar(Modelo tiv)
        {
            var res = new ResultadoIM();
            if (tiv.BloquearCambioAutoB)
            {
                tiv.BloquearCambioAuto = 1;
            }
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                var cr = "";
                var crel = "";
                var img = "";
                var imgel = "";
                try
                {
                    cr = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.Carrocerias);
                }
                catch
                {
                    //Si está vacía la lista no nos importa
                }
                try
                {
                    crel = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.CarroceriasEliminadas);
                }
                catch
                {
                    //Si está vacía la lista no nos importa
                }
                try
                {
                    img = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.Imagenes);
                }
                catch
                {
                    //Si está vacía la lista no nos importa
                }
                try
                {
                    imgel = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.ImagenesEliminadas);
                }
                catch
                {
                    //Si está vacía la lista no nos importa
                }
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDFamilia", tiv.IDFamilia),
                    new SqlParameter("@IDSeccion", tiv.IdSeccion),
                    new SqlParameter("@CodigoFamilia", tiv.CodigoFamilia),
                    new SqlParameter("@DescripcionFamilia", tiv.DescripcionFamilia),
                    new SqlParameter("@Imagen", tiv.Imagen),
                    new SqlParameter("@Carrocerias", cr),
                    new SqlParameter("@CarroceriasElim", crel),
                    new SqlParameter("@Imagenes", img),
                    new SqlParameter("@ImagenesElim", imgel),
                    new SqlParameter("@idTipoVehiculo", tiv.idTipoVehiculo),
                    new SqlParameter("@BloquearCambioAuto", tiv.BloquearCambioAuto),

                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ModelosCrearModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.TS = AsignaArrayByte("TS");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
        public ResultadoIM TiposVidrioCrearModificar(TipoVidrio tiv)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                //SIN HACER
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVidrio", tiv.IDTipoVidrio),
                    new SqlParameter("@Descripcion", tiv.Descripcion),
                    new SqlParameter("@Imagen", tiv.Imagen),
                    new SqlParameter("@Eurocode", tiv.Eurocode),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVidrioCrearModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.TS = AsignaArrayByte("TS");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
        public ResultadoIM TiposVehiculoCrearModificar(TipoVehiculo tiv)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                //SIN HACER
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDTipoVehiculo", tiv.IDTipoVehiculo),
                    new SqlParameter("@Descripcion", tiv.Descripcion),
                    new SqlParameter("@Imagen", tiv.Imagen)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.TiposVehiculoCrearModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.TS = AsignaArrayByte("TS");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
        public List<UsuarioDatosEmail> UsuariosWebLeer(BusquedaUsuarios us)
        {
            var res = new List<UsuarioDatosEmail>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                //SIN HACER
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idInicio", us.IdInicio),
                    new SqlParameter("@tamPag", us.tamPag),
                    new SqlParameter("@cadena", us.cadena),
                    new SqlParameter("@activo", us.activo),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.UsuariosWebLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    res.Add(new UsuarioDatosEmail
                    {
                        IdUsuarioWeb = AsignaEntero("IDUsuarioWeb"),
                        Nombre = AsignaCadena("Nombre"),
                        Email = AsignaCadena("Email"),
                        EmaildsWin = AsignaCadena("ListaEmails"),
                        NombreCompleto = AsignaCadena("Cliente"),
                        Activo = AsignaBool("Activo"),
                        IdClienteDelegacion = AsignaEntero("IdClienteDelegacion"),
                    });
                }
            }
            return res;
        }
        public void UsuariosWebEliminar(int idUsuarioWeb)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idUsuarioWeb", idUsuarioWeb),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.UsuariosWebEliminar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        public BuscaClientes ClientesLeer(int pagina, int bloque, string ncliente)
        {
            var res = new BuscaClientes();
            res.Clientes = new List<Cliente>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@pagina", pagina),
                    new SqlParameter("@bloque", bloque),
                    new SqlParameter("@nCliente", ncliente)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesLeer", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.NumReg = AsignaEntero("Registros");
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var cl = new Cliente
                    {
                        IDCliente = AsignaEntero("IDCliente"),
                        Delegacion = AsignaCadena("Delegacion"),
                        Nombre = AsignaCadena("Cliente"),
                        NombreComercial = AsignaCadena("NombreComercial"),
                        RazonSocial = AsignaCadena("RazonSocial"),
                        Cif = AsignaCadena("Cif"),
                        NombreMunicipio = AsignaCadena("NombreMunicipio"),
                        NombreProvincia = AsignaCadena("NombreProvincia"),
                        IDUsuarioWeb = AsignaEntero("IDUsuarioWeb"),
                    };
                    res.Clientes.Add(cl);
                }
            }
            res.Busqueda = new PaginacionClientes();
            res.Busqueda.bloque = bloque;
            res.Busqueda.pagina = pagina;
            res.Busqueda.nCliente = ncliente;
            return res;
        }
        public ResultadoIM CarroceriasCrearModificar(Carroceria tiv)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                var vid = "";
                try
                {
                    vid = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.Vidrios);
                } catch (Exception ex)
                {
                    //Significa que la lista está vacía, pero me da igual que esté vacío)
                }
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDCarroceria", tiv.IDCarroceria),
                    new SqlParameter("@Descripcion", tiv.Descripcion),
                    new SqlParameter("@Vidrios", vid),
                    new SqlParameter("@ImagenCarr", tiv.Imagen),
                    new SqlParameter("@Eurocode", tiv.Eurocode),

                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.CarroceriasCrearModificar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Identidad");
                    res.TS = AsignaArrayByte("TS");
                    res.Resultado = AsignaCadena("Resultado");
                }
            }
            return res;
        }
        public EmpresaWeb DatosEmpresaProcesar(EmpresaWeb em)
        {
            var cc = _configuration.GetConnectionString("DefaultConnection");
            var de = dsCore.Comun.Ayudas.SerializarACadenaXML(em);
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@empresa", de)
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.DatosEmpresa_Procesar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            return DatosEmpresaLeer();
        }
        public EmpresaWeb DatosEmpresaLeer()
        {
            var res = new EmpresaWeb();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.DatosEmpresaLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.IdDatosWeb = AsignaEntero("IdDatosWeb");
                    res.IdEmpresa = AsignaEntero("IdEmpresa");
                    res.GuidImg = AsignaGuid("GuidImg");
                    res.GuidIcono = AsignaGuid("GuidIcono");
                    res.Direccion = AsignaCadena("Direccion");
                    res.CodPostal = AsignaCadena("CodPostal");
                    res.Localidad = AsignaCadena("Localidad");
                    res.Telefono = AsignaCadena("Telefono");
                    res.Email = AsignaCadena("Email");
                    res.Web = AsignaCadena("Web");
                    res.PaginaFacebook = AsignaCadena("PaginaFacebook");
                    res.PaginaTwitter = AsignaCadena("PaginaTwitter");
                    res.PaginaGooglePlus = AsignaCadena("PaginaGooglePlus");
                    res.PaginaPinterest = AsignaCadena("PaginaPinterest");
                    res.PaginaLinkedIn = AsignaCadena("PaginaLinkedIn");
                    res.AcercaDe = AsignaCadena("AcercaDe");
                    res.IdClienteVentaDirecta = AsignaEntero("IdClienteVentaDirecta");
                    res.VisiblePedidos = AsignaBool("VisiblePedidos");
                    res.VisibleFacturas = AsignaBool("VisibleFacturas");
                    res.VisibleFinanzas = AsignaBool("VisibleFinanzas");
                    res.VisibleCatalogo = AsignaBool("VisibleCatalogo");
                    res.VisibleCuenta = AsignaBool("VisibleCuenta");
                    res.VisibleIdiomas = AsignaBool("VisibleIdiomas");
                    res.VisibleMensajes = AsignaBool("VisibleMensajes");
                    res.VisiblePlantillas = AsignaBool("VisiblePlantillas");
                    res.VisibleInvitado = AsignaBool("VisibleInvitado");
                    res.VisibleVentaDirecta = AsignaBool("VisibleVentaDirecta");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.NombreCuenta = AsignaCadena("NombreCuenta");
                    res.Usuario = AsignaCadena("Usuario");
                    res.Clave = AsignaCadena("Clave");
                    res.ServCorreoSal = AsignaCadena("ServCorreoSal");
                    res.PuertoCorreoSal = AsignaEntero("PuertoCorreoSal");
                    
                    res.NombreSitio = AsignaCadena("NombreSitio");
                    res.RutaLogo = AsignaCadena("RutaLogo");
                   
                    res.dirEmailContacto = AsignaCadena("dirEmailContacto");
                }
            }
            return res;
        }
        public void PruebaCreacionPedidoWeb()
        {
            var pw = new PedidoWeb();
            pw.Fecha = DateTime.Now;
        }
    }
}
