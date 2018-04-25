using dsASPCAutoCAdmin.Entidades;
using dsCore.Comun;
using dsCore2.DataAccess;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace dsASPCAutoCAdmin.DataAccess
{
    public class ServicioCorreo : BaseDataAccess
    {
        private readonly IConfiguration _configuration;
        public ServicioCorreo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResultadoInvitaciones ClientesInvitacionesProcesar(List<Cliente> Clientes)
        {
            var res = new ResultadoInvitaciones();
            var cr = "";
            try
            {
                cr = dsCore.Comun.Ayudas.SerializarACadenaXML(Clientes);
            }
            catch
            {
                //Si está vacía la lista no nos importa
            }
            res.Enviadas = new List<String>();
            res.UsuariosError = new List<UsuarioError>();
            var pp = new PropiedadesSitio();
            var UsuariosInvitados = new List<UsuarioDatosEmail>();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@clientes", cr),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesInvitacionesProcesar", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (_reader.Read())
                {
                    UsuarioDatosEmail us = RellenarUsuarioEmail();
                    UsuariosInvitados.Add(us);
                }
                _reader.NextResult();
                while (_reader.Read())
                {
                    var us = new UsuarioError
                    {
                        nombre = AsignaCadena("Nombre"),
                        motivo = AsignaCadena("Motivo"),
                    };
                    res.UsuariosError.Add(us);
                }
                _reader.NextResult();
                pp = RellenarPropiedadesSitio();
            }
            foreach (UsuarioDatosEmail us in UsuariosInvitados)
            {
                var email = RellenarEmail(pp.CorreoInvitacion, us, pp);
                res.Enviadas.Add(EnviarCorreo(us.EmaildsWin, "Invitación", email, pp));
            }
            return res;
        }
        public ResultadoRegistro ClientesProcesarRegistro(SolicitudRegistro sol)
        {
            var res = new ResultadoRegistro();
            var Usuario = new UsuarioDatosEmail();
            sol.empresawildcards = sol.empresa.Replace(" ", "%").ToLower();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@nombre", sol.nombre),
                    new SqlParameter("@password", sol.password),
                    new SqlParameter("@email", sol.email),
                    new SqlParameter("@telefono", sol.telefono),
                    new SqlParameter("@informacion", sol.informacion),
                    new SqlParameter("@cif", sol.cif),
                    new SqlParameter("@empresa", sol.empresa),
                    new SqlParameter("@empresawildcards", sol.empresawildcards),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesProcesarRegistro", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Resultado = AsignaEntero("Resultado");
                    res.Cadena = AsignaCadena("Cadena");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    UsuarioDatosEmail us = RellenarUsuarioEmail();
                    Usuario = us;
                }
                _reader.NextResult();
                res.Propiedades = RellenarPropiedadesSitio();
            }
            if (res.Resultado == 0)
            {
                var email = RellenarEmail(res.Propiedades.CorreoRegistro, Usuario, res.Propiedades);
                res.CadenaMail = EnviarCorreo(sol.email, "Registro", email, res.Propiedades);
            }
            else if (res.Resultado == 1)
            {
                var email = RellenarEmail(res.Propiedades.CorreoVerificacion, Usuario, res.Propiedades);
                res.CadenaMail = EnviarCorreo(Usuario.EmaildsWin, "Verificación", email, res.Propiedades);
            }
            return res;
        }
        public ResultadoAsignacion ClientesAsignarUsuarioWeb(int IDUsuarioWeb, int IDCliente, int? IDDomicilioCliente)
        {
            var res = new ResultadoAsignacion();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@IDUsuario", IDUsuarioWeb),
                    new SqlParameter("@IDCliente", IDCliente),
                    new SqlParameter("@IDDomicilioCliente", IDDomicilioCliente),
                    new SqlParameter("@iddelegacion", 0),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesAsignarUsuarioWeb", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Resultado = AsignaEntero("Resultado");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.Usuario = RellenarUsuarioEmail();
                }
                _reader.Read();
                if (_reader.Read())
                {
                    res.Propiedades = RellenarPropiedadesSitio();
                }
            }
            res.Propiedades = PropiedadesSitioLeer();
            if (res.Usuario.IdUsuarioWeb > 0)
            {
                var email = RellenarEmail(res.Propiedades.CorreoConfirmacion, res.Usuario, res.Propiedades);
                res.Cadena = EnviarCorreo(res.Usuario.Email, "Confirmación", email, res.Propiedades);
            }
            return res;
        }
        private string EnviarCorreo(string Destinatario, string Asunto, string Texto, PropiedadesSitio propis)
        {

            MailEnvio mail_envio = new MailEnvio();
            mail_envio.Mail = new Mail
            {
                Destinatario = Destinatario,
                Asunto = Asunto,
                Remitente = propis.Usuario,
                Texto = Texto
            };
            mail_envio.Cuenta = new Cuenta
            {
                NombreCuenta = propis.NombreCuenta,
                Usuario = propis.Usuario,
                Clave = propis.Clave,
                PuertoCorreoSal = propis.PuertoCorreoSal,
                ServCorreoSal = propis.ServCorreoSal
            };
            //var a = dsCore.Comun.dsMail.EnviarEmail(mail_envio);
            var a = Entidades.dsMail.EnviarEmail(mail_envio);
            return a;
            //return "Correo enviado, cuando descomente las lineas de arriba, claro";
        }
        private PropiedadesSitio PropiedadesSitioLeer()
        {
            var res = new PropiedadesSitio();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.PropiedadesWebLeer", null);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                res = RellenarPropiedadesSitio();
            }
            return res;
        }
        private PropiedadesSitio RellenarPropiedadesSitio()
        {
            var res = new PropiedadesSitio();
            if (_reader.Read())
            {
                res.NombreCuenta = AsignaCadena("NombreCuenta");
                res.Usuario = AsignaCadena("Usuario");
                res.Clave = AsignaCadena("Clave");
                res.ServCorreoSal = AsignaCadena("ServCorreoSal");
                res.PuertoCorreoSal = AsignaEntero("PuertoCorreoSal");
                res.CorreoRegistro = AsignaCadena("CorreoRegistro"); //Correo cuando un usuario se registra pero no está en nuestra BD
                res.CorreoVerificacion = AsignaCadena("CorreoVerificacion"); //Correo cuando un usuario se registra y soy capaz de validar su identidad
                res.CorreoInvitacion = AsignaCadena("CorreoInvitacion"); //Correo cuando decido invitar a un usuario.
                res.CorreoConfirmacion = AsignaCadena("CorreoConfirmacion"); //Correo cuando el administrador valida la identidad de un usuario
                res.CorreoRecuperacion = AsignaCadena("CorreoRecuperacion"); //Correo para recuperar la contraseña
                res.URLSitio = AsignaCadena("URLSitio");
                res.RutaValidacion = AsignaCadena("RutaValidacion");
                res.RutaRecuperacion = AsignaCadena("RutaRecuperacion");
                res.NombreSitio = AsignaCadena("NombreSitio");
                res.RutaLogo = AsignaCadena("RutaLogo");
            }
            return res;
        }
        private UsuarioDatosEmail RellenarUsuarioEmail()
        {
            return new UsuarioDatosEmail
            {
                IdUsuarioWeb = AsignaEntero("IDUsuarioWeb"),
                Nombre = AsignaCadena("Nombre"),
                Password = AsignaCadena("Password"),
                GuidValidacion = AsignaGuid("GuidValidacion"),
                Email = AsignaCadena("Email"),
                EmaildsWin = AsignaCadena("ListaEmails"),
                NombreCompleto = AsignaCadena("Cliente"),
                GuidRecuperacion = AsignaGuidNull("GuidRecuperacion"),
            };
        }
        private string RellenarEmail(string email, UsuarioDatosEmail us, PropiedadesSitio pr)
        {
            var newmail = email.Replace("#%guid%#", us.GuidValidacion.ToString());
            newmail = newmail.Replace("#%nombre%#", us.Nombre);
            newmail = newmail.Replace("#%guidrecuperacion%#", us.GuidRecuperacion.ToString());
            newmail = newmail.Replace("#%password%#", us.Password);
            newmail = newmail.Replace("#%nombrecompleto%#", us.NombreCompleto);
            newmail = newmail.Replace("#%urlSitio%#", pr.URLSitio);
            newmail = newmail.Replace("#%rutaValidacion%#", pr.RutaValidacion);
            newmail = newmail.Replace("#%rutaRecuperacion%#", pr.RutaRecuperacion);
            newmail = newmail.Replace("#%nombreSitio%#", pr.NombreSitio);
            newmail = newmail.Replace("#%rutaLogo%#", pr.RutaLogo);
            return newmail;
        }

        public ResultadoRecuperacionContrasena ClientesRecuperarContrasena(string email)
        {
            var res = new ResultadoRecuperacionContrasena();
            var us = new UsuarioDatosEmail();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@email", email),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesRecuperarContrasena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Resultado = AsignaEntero("Resultado");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    us = RellenarUsuarioEmail();
                }
            }
            res.Propiedades = PropiedadesSitioLeer();
            if (us.IdUsuarioWeb > 0)
            {
                var mailEnvio = RellenarEmail(res.Propiedades.CorreoRecuperacion, us, res.Propiedades);
                res.Cadena = EnviarCorreo(email, "Recuperación", mailEnvio, res.Propiedades);
            }
            return res;
        }
        public ResultadoIM ClientesValidarUsuarioWebPorGuid(Guid id)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@guid", id),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesValidarUsuarioWebPorGuid", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Resultado");
                    res.Resultado = AsignaCadena("ResultadoCadena");
                }
            }
            return res;
        }
        public ResultadoValidacionGuidRecuperacion ClientesValidarRecuperacionPassword(Guid guid)
        {
            var res = new ResultadoValidacionGuidRecuperacion();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@guid", guid),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesValidarRecuperacionPassword", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Resultado = AsignaEntero("Resultado");
                }
                _reader.NextResult();
                if (_reader.Read())
                {
                    res.Usuario = new UsuarioDatosEmail
                    {
                        IdUsuarioWeb = AsignaEntero("IDUsuarioWeb"),
                        Nombre = AsignaCadena("Nombre"),
                        NombreCompleto = AsignaCadena("Cliente"),
                    };
                }
            }
            return res;
        }
        public ResultadoIM ClientesCambiarContrasena(int idUsuarioWeb, string newPassword)
        {
            var res = new ResultadoIM();
            var cc = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(cc))
            {
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@idUsuarioWeb", idUsuarioWeb),
                    new SqlParameter("@newPassword", newPassword),
                };
                _cmd = SQLHelper.PrepareCommand(conn, null, CommandType.StoredProcedure, @"Web.ClientesCambiarContrasena", param);
                _reader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (_reader.Read())
                {
                    res.Identidad = AsignaEntero("Resultado");
                    res.Resultado = AsignaCadena("ResultadoCadena");
                }
            }
            return res;
        }
    }

}
