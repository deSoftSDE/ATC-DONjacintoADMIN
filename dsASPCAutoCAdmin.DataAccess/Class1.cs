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
        public Modelo ModelosLeerPorID(int IDFamilia)
        {
            var res = new Modelo();
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
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDSeccion", bs.IdSeccion),
                    new SqlParameter("@IDArticulo", bs.IdArticulo),
                    new SqlParameter("@IDFamilia", bs.IdFamilia),
                    new SqlParameter("@IDTipoVidrio", bs.IdTipoVidrio),
                    new SqlParameter("@Codigo", bs.Codigo),
                    new SqlParameter("@Descripcion", bs.Descripcion),
                    new SqlParameter("@DescripcionCorta", bs.DescripcionCorta),
                    new SqlParameter("@DescripcionDetallada", bs.DescripcionDetallada),

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
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                var cr = "";
                var crel = "";
                try
                {
                    cr = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.Carrocerias);
                }
                catch (Exception ex)
                {
                    //Si está vacía la lista no nos importa
                }
                try
                {
                    crel = dsCore.Comun.Ayudas.SerializarACadenaXML(tiv.CarroceriasEliminadas);
                }
                catch (Exception ex)
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
                    new SqlParameter("@idTipoVehiculo", tiv.idTipoVehiculo),
                    
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
    }
}
