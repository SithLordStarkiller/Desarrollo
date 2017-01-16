using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntUsuario
    {
        public VUsuarios ObtenerUsuarioPorId(int idUsuario)
        {
            VUsuarios vUsuario = null;
            try
            {
                var sistemasCobranzaEntity = new SistemasCobranzaEntities();
                vUsuario = (
                    from r in sistemasCobranzaEntity.VUsuarios
                    where r.idUsuario == idUsuario
                    select r).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntUsuario",
                    string.Concat("ObtenerUsuarioPorId: " + idUsuario + " ", ex.Message,
                        (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
            }
            return vUsuario;
        }

        public UsuarioModel ObtenerUsuarioPorId(string idUsuario)
        {
            var usuariomodel = new UsuarioModel();

            try
            {
                var idusuario = Convert.ToInt32(idUsuario);
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idusuario", SqlDbType.Int) { Value = idusuario };
                var result = instancia.EjecutarDataSet("SqlDefault", "ObtenerUsuarioPorId", parametros);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    usuariomodel = SetModelUSuario(result.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntUsuario", "ObtenerUsuarioXUsuario_" + ex.Message);

            }

            return usuariomodel;
        }
        public List<UsuarioModel> ObtenerUsuariosPorDominio(string idDominio)
        {
            
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("ObtenerUsuariosPorDominio", cnn);
            var result = new List<UsuarioModel>();
            try
            {
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idsDominio", SqlDbType.VarChar,100) { Value = idDominio };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result.AddRange(from DataRow row in ds.Tables[0].Rows select SetModelUSuario(row));
                }
                
            }
            catch (Exception)
            {
                if (cnn != null)
                    instancia.CierraConexion(cnn);
                throw;
                
            }
            return result;
        }

        public VUsuarios ObtenerUsuarioPorEmail(string email)
        {
            VUsuarios vUsuario = null;
            try
            {
                var sistemasCobranzaEntity = new SistemasCobranzaEntities();
                vUsuario = (
                    from u in sistemasCobranzaEntity.VUsuarios
                    where u.Email.ToUpper() == email.ToUpper()
                    select u).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntUsuario",
                    string.Concat("ObtenerUsuarioPorEmail: " + email + " ", ex.Message,
                        (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
            }
            return vUsuario;
        }
       
        public DataSet ObtenerUsuarioPorDelegacion(int idUsuarioPadre, int idRol)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("ObtenerUsuarioPorDelegacion", cnn);
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) { Value = idUsuarioPadre };
            parametros[1] = new SqlParameter("@idRol", SqlDbType.Int) { Value = idRol };
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandTimeout = 300;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);
            return ds;
        }

        public DataSet ObtenerUsuariosPorRol(int idRol)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("ObtenerUsuariosPorRol", cnn);
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idRol", SqlDbType.Int) { Value = idRol };
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);
            return ds;
        }

        /// <summary>
        /// Obtiene los datos de un usuario
        /// </summary>
        /// <param name="usuario">Usuario registrado en la plataforma</param>
        /// <returns>Modelo con la informacion</returns>
        public UsuarioModel ObtenerUsuarioXUsuario(string usuario)
        {
            var usuariomodel = new UsuarioModel();

            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@usuario", SqlDbType.NVarChar, 50) { Value = usuario };
                var result = instancia.EjecutarDataSet("SqlDefault", "ObtenerUsuarioXUsuario", parametros);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    usuariomodel = SetModelUSuario(result.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntUsuario", "ObtenerUsuarioXUsuario_"+ex.Message);
                
            }
            
            return usuariomodel;
        }

        public List<UsuarioModel> ObtenerUsuarios(int idDominio, int idPadre, int idRol, int estatus, string delegacion)
        {
            var usuarioModelList = new List<UsuarioModel>();
            try   {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
                parametros[1] = new SqlParameter("@idPadre", SqlDbType.Int) { Value = idPadre };
                parametros[2] = new SqlParameter("@idRol", SqlDbType.Int) { Value = idRol };
                parametros[3] = new SqlParameter("@Estatus", SqlDbType.Int) { Value = estatus };
                parametros[4] = new SqlParameter("@Delegacion", SqlDbType.VarChar, 2) { Value = delegacion };
                var result = instancia.EjecutarDataSet("SqlDefault", "ObtenerUsuarios", parametros);

                if (result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    usuarioModelList.AddRange(from DataRow row in result.Tables[0].Rows select SetModelUSuario(row));
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntUsuario", "ObtenerUsuarios_" + ex.Message);
                
            }
            return usuarioModelList;

        }

        public Usuario ActualizarUsuario(int idUsuario, string nombre, string email, string password, int? estatus, DateTime? bloqueo)
        {
            Usuario usuario = null;
            try
            {
                var sistemasCobranzaEntity = new SistemasCobranzaEntities();
                usuario = (
                    from u in sistemasCobranzaEntity.Usuario
                    where u.idUsuario == idUsuario
                    select u).FirstOrDefault();
                if (usuario != null)
                {
                    usuario.Nombre = nombre ?? usuario.Nombre;
                    usuario.Email = email ?? usuario.Email;
                    usuario.Password = password ?? usuario.Password;
                    usuario.Estatus = estatus ?? usuario.Estatus;

                    sistemasCobranzaEntity.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuario, "EntUsuario",
                    string.Concat(
                        "ActualizarUsuario: " + nombre + " " + email + " " + password + " " + estatus + " " + bloqueo,
                        ex.Message,
                        (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
            }
            return usuario;
        }

        public bool ValidarPassBitacora(int idUsuario, string password)
        {
            bool valido = true;
            try
            {
                var sistemasCobranzaEntity = new SistemasCobranzaEntities();
                var usuario = (from bP in sistemasCobranzaEntity.BitacoraPasswords
                    where bP.idUsuario == idUsuario && bP.PassWord == password
                    select bP).FirstOrDefault();
                if (usuario != null)
                {
                    valido = false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuario, "EntUsuario",
                    string.Concat("ValidarPassBitacora: " + idUsuario + " " + password + " ", ex.Message,
                        (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
            }
            return valido;
        }

        public UsuarioModel ValidaUsuario(String dominio, String usuario, String password)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("ValidarUsuario", cnn);
            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@Dominio", SqlDbType.NVarChar, 50);
            parametros[0].Value = dominio;
            parametros[1] = new SqlParameter("@Usuario", SqlDbType.NVarChar, 50);
            parametros[1].Value = usuario;
            parametros[2] = new SqlParameter("@Password", SqlDbType.NVarChar, 130);
            parametros[2].Value = password;
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return SetModelUSuario(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        public UsuarioModel BloqueoLogin(string dominio, string usuario)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("BloqueoLogin", cnn);
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@Dominio", SqlDbType.NVarChar, 50) {Value = dominio};
            parametros[1] = new SqlParameter("@Usuario", SqlDbType.NVarChar, 50) {Value = usuario};
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return SetModelUSuario(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        public UsuarioModel CambiarContraseniaUsuario(int idUsuario, String contrasenia, String llave)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();

            var sc = new SqlCommand("CambiaContraseniaUsuario", cnn);
            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int);
            parametros[0].Value = idUsuario;
            parametros[1] = new SqlParameter("@NuevaContrasenia", SqlDbType.NVarChar, 130);
            parametros[1].Value = contrasenia;
            parametros[2] = new SqlParameter("@Llave", SqlDbType.NVarChar, 15);
            parametros[2].Value = llave;
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);

            instancia.CierraConexion(cnn);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return SetModelUSuario(ds.Tables[0].Rows[0]);    
            }
            return null;
        }

        public UsuarioModel SetModelUSuario(DataRow dataRow)
        {
            var modelUsuario = new UsuarioModel();
            modelUsuario.Extra = new string[20];
            if (dataRow!=null)
            {
                foreach (var colum in dataRow.Table.Columns)
                {
                    string columnName = colum.ToString();
                    switch (columnName.ToLower())
                    {
                        case "idusuario":
                            modelUsuario.IdUsuario = dataSetInt(dataRow, columnName);
                            break;

                        case "iddominio":
                            modelUsuario.IdDominio = dataSetInt(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "idrol":
                            modelUsuario.IdRol = dataSetInt(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "usuario":
                            modelUsuario.Usuario = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "nombre":
                        case "nombreusuario":
                            modelUsuario.Nombre = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "email":
                            modelUsuario.Email = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "password":
                            modelUsuario.Password = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "alta":
                            modelUsuario.Alta = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "ultimologin":
                            modelUsuario.UltimoLogin = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "estatus":
                            modelUsuario.Estatus = dataSetInt(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "intentos":
                            modelUsuario.Intentos = dataSetInt(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "bloqueo":
                            modelUsuario.Bloqueo = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "nombrerol":
                            modelUsuario.NombreRol = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "dominio":
                            modelUsuario.NomCorto = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "nombredominio":
                            modelUsuario.NombreDominio = dataSetString(dataRow, columnName, modelUsuario.IdUsuario);
                            break;
                        case "idpadre":
                            modelUsuario.IdPadre = dataSetInt(dataRow, columnName, modelUsuario.IdPadre);
                            break;
                        case "escallcenterout":
                            modelUsuario.EsCallCenterOut = dataSetString(dataRow, columnName, modelUsuario.IdUsuario).ToLower()=="true";
                            break;
                        default:
                            modelUsuario.Extra[modelUsuario.Extra.Length-1] = columnName;
                            break;
                    }
                }
            }
            return modelUsuario;
        }

        private int dataSetInt(DataRow dataRow, string columnName,int idusuario=-1)
        {
            try
            {
                return dataRow[columnName] != null
                ? Convert.ToInt32(dataRow[columnName].ToString())
                : -1;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "EntUsuario",
              string.Concat("dataSetInt-idusuario:" + idusuario + " columnName: " + columnName + " ", ex.Message,
                  (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
                return -1;
            }
            
        }

        private string dataSetString(DataRow dataRow, string columnName,int idusuario=-1)
        {
            try
            {
                return dataRow[columnName] != null
               ? dataRow[columnName].ToString()
               : null;
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "EntUsuario",
              string.Concat("dataSetString-idusuario:" + idusuario + " columnName: " + columnName + " ", ex.Message,
                  (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
                return null;
            }
           
        }

        /// <summary>
        /// Se encarga de cambiar las dependencias de un usuario padre-hijo
        /// </summary>
        /// <param name="idPadreViejo">idUsuario padre que se tiene actualmente</param>
        /// <param name="idPadreNuevo">idUsuario padre que tendrá asignado </param>
        /// <param name="idHijo">Usuario a mover</param>
        /// <returns>Mensaje con el resultado de la acción</returns>
        public string ReasignarUsuarios(string idPadreViejo, string idPadreNuevo, string idHijo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@idPadreViejo", SqlDbType.Int) { Value = Convert.ToInt32(idPadreViejo) };
                parametros[1] = new SqlParameter("@idPadreNuevo", SqlDbType.Int) { Value = Convert.ToInt32(idPadreNuevo) };
                parametros[2] = new SqlParameter("@idHijo", SqlDbType.Int) { Value = Convert.ToInt32(idHijo) };

                return instancia.EjecutarEscalar("SqlDefault", "ReasignarUsuario", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntUsuario", "ReasignarUsuarios: " + ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// Inserta nuevo usuario.
        /// </summary>
        /// <param name="idPadre">Id el padre.</param>
        /// <param name="idDominio">I del dominio.</param>
        /// <param name="idRol">Rol del nuevo usuario.</param>
        /// <param name="usuario">Usuario.</param>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="password">Password del usuario.</param>
        /// <param name="email">Email del usuario.</param>
        /// <param name="esCallCenter">Define si es CallCenter.</param>
        /// <returns>DataSet con el dominio y el id del nuevo usuario</returns>
        public DataSet InsertaUSuario(String idPadre, String idDominio, String idRol, String usuario, String nombre,
            String password, String email, int esCallCenter)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[8];
                parametros[0] = new SqlParameter("@idPadre", SqlDbType.Int) { Value = idPadre };
                parametros[1] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
                parametros[2] = new SqlParameter("@idRol", SqlDbType.Int) { Value = idRol };
                parametros[3] = new SqlParameter("@Usuario", SqlDbType.NVarChar, 50) { Value = usuario };
                parametros[4] = new SqlParameter("@Nombre", SqlDbType.NVarChar, 30) { Value = nombre };
                parametros[5] = new SqlParameter("@Password", SqlDbType.NVarChar, 130) { Value = password };
                parametros[6] = new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = email };
                parametros[7] = new SqlParameter("@EsCallCenter", SqlDbType.Int) { Value = esCallCenter };

                return instancia.EjecutarDataSet("SqlDefault", "InsertaUSuario", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntUsuario", "InsertaUSuario: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Actualizar datos usuario por idUsuario.
        /// </summary>
        /// <param name="idUsuario">Id del usuario.</param>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="email">Email del usuario.</param>
        /// <param name="password">Password del usuario.</param>
        /// <param name="estatus">Estatus del usuario.</param>
        /// <param name="esCallCenter">Define si es CallCenter.</param>
        /// <returns>Falso o verdadero</returns>
        public bool CambiarDatosUsuarioPoridUsuario(int idUsuario, string nombre, string email, string password, int estatus, int esCallCenter)
        { 
            try
            {
                var result = false;
                var ds = new DataSet();
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };
                parametros[1] = new SqlParameter("@nombre", SqlDbType.NVarChar) { Value = nombre };
                parametros[2] = new SqlParameter("@email", SqlDbType.NVarChar) { Value = email };
                parametros[3] = new SqlParameter("@password", SqlDbType.NVarChar) { Value = password };
                parametros[4] = new SqlParameter("@Estatus", SqlDbType.Int) { Value = estatus };
                parametros[5] = new SqlParameter("@EsCallCenter", SqlDbType.Int) { Value = esCallCenter };

                ds = instancia.EjecutarDataSet("SqlDefault", "ActualizarDatosUsuario", parametros);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntUsuario", "CambiarDatosUsuarioPoridUsuario: " + ex.Message);
                return false;
            }
        }
    }
}
