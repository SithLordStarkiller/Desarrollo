using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntOrdenes
    {
        public List<OrdenModel> ObtenerOrdenesPorEstatus(int estatus, int obtener, int idUsuarioLog)
        {
            try
            {
                var contexto = new SistemasCobranzaEntities();
                var lista = from o in contexto.Ordenes
                            join u in contexto.Usuario on o.idUsuario equals u.idUsuario
                            join ua in contexto.Usuario on o.idUsuarioAnterior equals ua.idUsuario
                            join c in contexto.Creditos on o.num_Cred equals c.CV_CREDITO
                            where o.Estatus == estatus
                            select new
                            {
                                o.idOrden,
                                o.num_Cred,
                                u.Usuario1,
                                UsuarioAnterior = ua.Usuario1,
                                c.CV_RUTA
                            };


                var listaObtener = lista.Take(obtener);

                return listaObtener.Select(item => new OrdenModel { IdOrden = item.idOrden, NumCred = item.num_Cred, Usuario = item.Usuario1, UsuarioAnterior = item.UsuarioAnterior, Ruta = item.CV_RUTA }).ToList();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                    "Error: al ejecutar ObtenerOrdenesPorEstatus: estatus = " + estatus + " obtener = " + obtener +
                    " - " +
                    ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                return new List<OrdenModel>();
            }
        }

        /// <summary>
        /// Método que recupera las ordenes con estatus 4 y Tipo null o vacio, para validar con el WS si ya estan dispersadas.
        /// </summary>
        /// <param name="estatus">Estatus a consultal (4)</param>
        /// <param name="idUsuarioLog">Identificador del usuario para el log</param>
        /// <returns>Listado de las ordenes listas para validar con el WS</returns>
        public List<ModeloOrdesConsultaEstatus> ObtenerOrdenesPorEstatusTipo(int estatus, int idUsuarioLog)
        {
            List<ModeloOrdesConsultaEstatus> mEstaus = new List<ModeloOrdesConsultaEstatus>();

            try
            {
                var instancia = ConnectionDB.Instancia;

                var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerOrdenesPorTipo");

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ModeloOrdesConsultaEstatus modeloTemp = new ModeloOrdesConsultaEstatus();
                        modeloTemp.Tipo = dr["Tipo"].ToString();
                        modeloTemp.Nss = dr["Nss"].ToString();
                        modeloTemp.IdOrden = Convert.ToInt32(dr["idOrden"].ToString());
                        mEstaus.Add(modeloTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                    "Error: al ejecutar ObtenerOrdenesPorEstatusTipo: estatus = " + estatus +
                    " - " +
                    ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                mEstaus = new List<ModeloOrdesConsultaEstatus>();
            }
            return mEstaus;
        }

        /// <summary>
        /// Obtiene los datos de una orden en relacion al Credito que tenga relacionado
        /// </summary>
        /// <param name="credito"> numero de credito a buscar</param>
        /// <returns>modelo de la orden resultado</returns>
        /// En caso de existir mas de una orden realcionada con el credito, regresa el primer registro de orden encontrada
        /// JARO
        public OrdenModel ObtenerOrdenxCredito(string credito)
        {

            var modelo = new OrdenModel();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@credito", SqlDbType.VarChar, 15) { Value = credito };
                var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerOrdenxCredito", parametros);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    modelo.IdOrden = Convert.ToInt32(ds.Tables[0].Rows[0]["IdOrden"]);
                    modelo.NumCred = Convert.ToString(ds.Tables[0].Rows[0]["NumCred"]);
                    modelo.IdUsuario = Convert.ToInt32(ds.Tables[0].Rows[0]["idusuario"]);
                    modelo.IdUsuarioPadre = Convert.ToInt32(ds.Tables[0].Rows[0]["idusuarioPadre"]);
                    modelo.IdUsuarioAlta = Convert.ToInt32(ds.Tables[0].Rows[0]["idusuarioAlta"]);
                    modelo.IdDominio = Convert.ToInt32(ds.Tables[0].Rows[0]["iddominio"]);
                    modelo.IdVisita = Convert.ToInt32(ds.Tables[0].Rows[0]["idvisita"]);
                    modelo.FechaAlta = Convert.ToString(ds.Tables[0].Rows[0]["fechaAlta"]);
                    modelo.Estatus = Convert.ToInt32(ds.Tables[0].Rows[0]["estatus"]);
                    modelo.FechaModificacion = Convert.ToString(ds.Tables[0].Rows[0]["fechaModificacion"]);
                    modelo.FechaEnvio = Convert.ToString(ds.Tables[0].Rows[0]["fechaEnvio"]);
                    modelo.FechaRecepcion = Convert.ToString(ds.Tables[0].Rows[0]["fechaRecepcion"]);
                    modelo.Auxiliar = Convert.ToString(ds.Tables[0].Rows[0]["auxiliar"]);
                    modelo.IdUsuarioAnterior = Convert.ToInt32(ds.Tables[0].Rows[0]["idusuarioAnterior"]);
                    modelo.Tipo = Convert.ToString(ds.Tables[0].Rows[0]["tipo"]);
                    modelo.Usuario = Convert.ToString(ds.Tables[0].Rows[0]["Usuario"]);
                    modelo.UsuarioAnterior = Convert.ToString(ds.Tables[0].Rows[0]["UsuarioAnterior"]);
                    modelo.Ruta = Convert.ToString(ds.Tables[0].Rows[0]["Ruta"]);
                    return modelo;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntOrdenes", "ObtenerOrdenxCredito: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Método que permite recuperar el correo de una orden
        /// </summary>
        /// <param name="idOrden">Identificador de la orden</param>
        /// <param name="idUsuarioLog">Usuario para le LOG</param>
        /// <returns>Objeto con la infformación del correo y el id de la orden</returns>
        public ModeloOrdesConsultaEstatus ObtenerOrdenCorreo(int idOrden, int idUsuarioLog)
        {

            ModeloOrdesConsultaEstatus modeloOrdenCorreo = new ModeloOrdesConsultaEstatus();
            try
            {

                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };

                var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerRespuestasCorreo", parametros);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    modeloOrdenCorreo.Tipo = ds.Tables[0].Rows[0]["Tipo"].ToString();
                    modeloOrdenCorreo.IdOrden = Convert.ToInt32(ds.Tables[0].Rows[0]["idOrden"].ToString());
                    modeloOrdenCorreo.NumeroCredito = ds.Tables[0].Rows[0]["num_Cred"].ToString();

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["NombreCampo"].Equals("CorreoElectronico"))
                        {
                            modeloOrdenCorreo.Correo = dr["Valor"].ToString();
                        }
                        else if (dr["NombreCampo"].Equals("Nombre"))
                        {
                            modeloOrdenCorreo.Nombre = dr["Valor"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                      "Error: al ejecutar ObtenerRespuestasCorreo: idOrden = " + idOrden + ex.Message +
                      (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));

                modeloOrdenCorreo = new ModeloOrdesConsultaEstatus();
            }

            return modeloOrdenCorreo;
        }

        public List<OrdenModel> ObtenerOrdenesPorId(string idOrdenes)
        {
            var sql =
                string.Format(
                    "SELECT DISTINCT  o.idOrden IdOrden, o.num_Cred NumCred, u.Usuario Usuario, " +
                    "CASE ua.idUsuario WHEN 0 THEN u.Usuario ELSE ua.Usuario END UsuarioAnterior, c.CV_RUTA Ruta,f.EnviarMovil " +
                    "FROM Ordenes o WITH(NOLOCK) INNER JOIN Usuario u WITH(NOLOCK) on o.idUsuario = u.idUsuario " +
                    "INNER JOIN Usuario ua WITH(NOLOCK) on o.idUsuarioAnterior = ua.idUsuario " +
                    "INNER JOIN creditos c WITH(NOLOCK) ON c.CV_CREDITO=o.num_Cred " +
                    "INNER JOIN Formulario f  WITH(NOLOCK) ON f.Ruta = c.CV_RUTA " +
                    "WHERE idOrden IN ({0})",
                    idOrdenes);
            var contexto = new SistemasCobranzaEntities();
            var ordenes = contexto.Database.SqlQuery<OrdenModel>(sql).ToList();
            return ordenes;
        }

        public List<Ordenes> ObtenerOrdenesPorIdPadre(int idUsuarioPadre)
        {
            List<Ordenes> ordenes = null;
            try
            {
                var sistemasCobranzaEntity = new SistemasCobranzaEntities();
                ordenes = (from o in sistemasCobranzaEntity.Ordenes where o.idUsuarioPadre == idUsuarioPadre select o).ToList();

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntOrdenes", string.Concat("ObtenerOrdenesPorIdPadre: ", ex.Message, (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
            }
            return ordenes;
        }

        /// <summary>
        /// Método que permite actualziar el campo Tipo de una orden con estatus 4
        /// </summary>
        /// <param name="idOrden">Identificador de la orden</param>
        /// <param name="tipo">Identificador para la marcar que la orden fue dispersada</param>
        /// /// <param name="estatus">Estatus de la orden al cual se realiza filtro 0=no aplica</param>
        /// <param name="idUsuarioLog">Isuario para el LOG</param>
        /// <returns>1 = se actulizo de manera correcta, 0 = no se actulizó</returns>
        public int ActualizarTipoOrden(int idOrden, string tipo, int estatus, int idUsuarioLog)
        {
            int retorno = 0;
            try
            {
                var contexto = new SistemasCobranzaEntities();

                Ordenes orden = estatus > 0 ? (from u in contexto.Ordenes
                                               where u.idOrden == idOrden && u.Estatus == estatus
                                               select u).FirstOrDefault()
                                 : (from u in contexto.Ordenes
                                    where u.idOrden == idOrden
                                    select u).FirstOrDefault();

                if (orden != null)
                {
                    orden.Tipo = tipo;
                    retorno = contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                    "Error: al ejecutar ActualizarTipoOrden: idOrden = " + idOrden +
                    " - " +
                    ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                retorno = 0;
            }

            return retorno;
        }

        public Ordenes ActualizarOrden(int idOrden, int? idUsuario, int? idUsuarioPadre, int? idUsuarioAlta, int? idVisita, int? estatus, DateTime? fechaModificacion, DateTime? fechaEnvio, DateTime? fechaRecepcion, string auxiliar, string tipo)
        {
            Ordenes orden = null;
            try
            {
                var sistemasCobranzaEntity = new SistemasCobranzaEntities();
                orden = (
                    from u in sistemasCobranzaEntity.Ordenes
                    where u.idOrden == idOrden
                    select u).FirstOrDefault();
                if (orden != null)
                {
                    orden.idUsuario = idUsuario ?? orden.idUsuario;
                    orden.idUsuarioPadre = idUsuarioPadre ?? orden.idUsuarioPadre;
                    orden.idUsuarioAlta = idUsuarioAlta ?? orden.idUsuarioAlta;
                    orden.idVisita = idVisita ?? orden.idVisita;
                    orden.Estatus = estatus ?? orden.Estatus;
                    orden.FechaModificacion = fechaModificacion ?? orden.FechaModificacion;
                    orden.FechaEnvio = fechaEnvio ?? orden.FechaEnvio;
                    orden.FechaRecepcion = fechaRecepcion ?? orden.FechaRecepcion;
                    orden.Auxiliar = auxiliar ?? orden.Auxiliar;
                    orden.Tipo = tipo ?? orden.Tipo;
                    sistemasCobranzaEntity.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntOrdenes",
                    string.Concat(
                        "ActualizarOrden: " + idUsuario + " " + idUsuarioPadre + " " + idUsuarioAlta + " " + idVisita + " "
                        + estatus + " " + fechaModificacion + " " + fechaEnvio + " " + fechaRecepcion + " " + auxiliar, ex.Message,
                        (ex.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.Message) : "")));
            }
            return orden;
        }

        public int ActualizarEstatusUsuarioOrdenes(string ordenes, int estatus, int actualizaUsuario, bool actualizaFecha, bool actualizaSiEstatusIgual, int idUsuarioLog)
        {
            try
            {
                var cnn = ConexionSql.Instance;
                return cnn.ActualizarEstatusUsuarioOrdenes(ordenes, estatus, actualizaUsuario, actualizaFecha,
                    actualizaSiEstatusIgual);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                    "Error: al ejecutar ActualizarEstatusOrdenes: estatus = " + estatus + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                return 0;
            }
        }

        public int ActualizarAuxiliar(string ordenes, string auxiliares, int idUsuarioLog)
        {
            try
            {
                var cnn = ConexionSql.Instance;
                return cnn.ActualizarAuxiliar(ordenes, auxiliares);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                    "Error: al ejecutar ActualizarAuxiliar: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                return 0;
            }
        }

        /// <summary>
        /// Actualiza el estatus de las ordenes sin modificar las respuestas
        /// </summary>
        /// <param name="ordenes">Listado de las órdenes separadas por comas</param>
        /// <param name="estatus">Estatus al que se quieren cambiar las ordenes</param>
        /// <param name="actualizaFecha">Si se establece en true cambia las ordenes</param>
        /// <param name="esReverso">Si es true, dispara actualizaciones de reversos</param>
        /// <returns>Regresa la cantidad de registros modificados</returns>
        public int ActualizaEstatusOrdenes(string ordenes, int estatus, bool actualizaFecha, bool esReverso)
        {
            var conexion = ConexionSql.Instance;
            SqlConnection cnn = null;
            try
            {
                cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ActualizaEstatusOrdenes", cnn);
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@TextoOrdenes", SqlDbType.VarChar, 1500) { Value = ordenes };
                parametros[1] = new SqlParameter("@Estatus", SqlDbType.Int) { Value = estatus };
                parametros[2] = new SqlParameter("@ActualizaFecha", SqlDbType.Bit) { Value = actualizaFecha };
                parametros[2] = new SqlParameter("@esReverso", SqlDbType.Bit) { Value = esReverso };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var resultado = sc.ExecuteNonQuery();
                conexion.CierraConexion(cnn);
                return resultado;
            }
            catch (Exception)
            {
                if (cnn != null)
                    conexion.CierraConexion(cnn);
                throw;
            }
        }

        /// <summary>
        /// Actualiza el estatus de las ordenes a Sincronizando
        /// </summary>
        /// <param name="orden">orden a modificar</param>        
        public void ActualizaOrdenesSincronizando(string orden)
        {
            var conexion = ConexionSql.Instance;
            SqlConnection cnn = null;
            try
            {
                cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ActualizaOrdenesSincronizando", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TextoOrden", SqlDbType.VarChar, 1500) { Value = orden };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.ExecuteNonQuery();
                conexion.CierraConexion(cnn);
            }
            catch (Exception)
            {
                if (cnn != null)
                    conexion.CierraConexion(cnn);
                throw;
            }

        }

        /// <summary>
        /// Autoriza las ordenes  de aplicacion Originacion
        /// </summary>
        /// <param name="ordenes">ordenes a modificar</param>        
        public int AutorizaOriginacion(string ordenes)
        {
            var conexion = ConexionSql.Instance;
            SqlConnection cnn = null;
            try
            {
                // var ord = ordenes.Split(',');

                //var ordenesAutorizar = ord.Aggregate("", (current, str) => current + ("''" + str + "'',"));
                //ordenesAutorizar = ordenesAutorizar.Substring(0, ordenesAutorizar.Length - 1);

                var sql = "exec AutorizaOriginacion " +
                          "@idOrden=" + ordenes + "";
                cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception)
            {
                if (cnn != null)
                    conexion.CierraConexion(cnn);
                throw;
            }
        }

        /// <summary>
        /// Autoriza las ordenes  de aplicacion Originacion
        /// </summary>
        /// <param name="ordenes">ordenes a modificar</param>        
        public int ReenviaIncompletosOriginacion(string ordenes)
        {
            var sql = "exec ReenviaIncompletosOriginacion " +
                      "@idOrden=" + ordenes + "";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        /// <summary>
        /// Registra estado que pueda pasar una orden
        /// </summary>
        /// <param name="textoOrdenes">Ordenes a registrar</param>
        /// <param name="estado">Estado que se encuentra</param>
        public string InsertaBitacoraEstadosOrdenes(string textoOrdenes)
        {
            try
            {
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("InsertaBitacoraEstadosOrdenes", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@TextoOrdenes", SqlDbType.VarChar) { Value = textoOrdenes };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public int CancelarOrdenes(string ordenes, int idUsuarioLog)
        {
            try
            {
                var cnn = ConexionSql.Instance;
                return cnn.CancelarOrdenes(ordenes);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "EntOrdenes",
                    "Error: al ejecutar CancelarOrdenes: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                return 0;
            }
        }

        /// <summary>
        /// Guarda temporalmente en una tabla lo relacionado a la gestión realizada por CC para que posterior mente sea procesada
        /// </summary>
        /// <param name="idOrden">Orden a procesar</param>
        /// <param name="idUsuario">id usuario que esta realizando la gestión</param>
        /// <param name="dicRespuesta"> Diccionario de las respuestas que obtiene de CW</param>
        /// <returns></returns>
        public string InsertarRespuestasPendientes(int idOrden, int idUsuario, StringBuilder dicRespuesta)
        {
            string resultado = "";
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@idorden", SqlDbType.Int) { Value = idOrden };
                parametros[1] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };
                parametros[2] = new SqlParameter("@ValoresRespuesta", SqlDbType.VarChar) { Value = dicRespuesta.ToString() };

                instancia.EjecutarDataSet("SqlDefault", "InsertaRespuestasPendientes", parametros);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntOrdenes - InsertarRespuestasPendientes", resultado);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista de idOrden las cuales están preparadas para poder procesar
        /// </summary>
        /// <returns>string idOrden,idorden </returns>
        public string ObtenerRespuestasPendientes()
        {
            var instancia = ConnectionDB.Instancia;
            var result = instancia.EjecutarEscalar("SqlDefault", "ObtenerRespuestasPendientes");
            return result;
        }

        /// <summary>
        /// Obtiene los datos de la tabla RespuestasPendientes
        /// </summary>
        /// <param name="idOrden">Orden a buscar</param>
        /// <returns>dataset con la información de la tabla</returns>
        public DataSet ObtenerDatosRespuestasPendientes(int idOrden)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idorden", SqlDbType.Int) { Value = idOrden };
            return instancia.EjecutarDataSet("SqlDefault", "ObtenerDatosRespuestasPendientes", parametros);
        }
        /// <summary>
        /// Borra de tabla las respuestas que estaban pendientes por procesar
        /// </summary>
        /// <param name="idOrden">idOrden a procesar</param>
        /// <returns>true si se ha realizado el borrado</returns>
        public bool BorrarRespuestasPendientes(int idOrden)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@idorden", SqlDbType.Int) { Value = idOrden };
            var result = instancia.EjecutarEscalar("SqlDefault", "BorrarRespuestasPendientes", parametros);
            return (!string.IsNullOrEmpty(result) ? Convert.ToInt32(result) : 0) < 1;
        }
        /// <summary>
        /// Filtra las ordenes que tengan como gestión un convenio
        /// </summary>
        /// <param name="ordenes">cadena de ordenes separadas por , para realizar la búsqueda</param>
        /// <returns>ordenes que no tienen un convenio</returns>
        public string FiltrarEstatusUsuarioOrdenes(string ordenes)
        {
            var instancia = ConnectionDB.Instancia;
            SqlConnection cnn = null;
            string resultado;
            try
            {
                cnn = instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("FiltrarEstatusUsuarioOrdenes", cnn);
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@Ordenes", SqlDbType.VarChar, 8000) { Value = ordenes };
                parametros[1] = new SqlParameter("@OrdenesFiltradas", SqlDbType.VarChar, 8000) { Direction = ParameterDirection.Output };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.ExecuteNonQuery();
                resultado = sc.Parameters["@OrdenesFiltradas"].Value.ToString();

                cnn.Close();
            }
            catch (Exception)
            {
                if (cnn != null)
                    cnn.Close();
                throw;
            }

            return resultado;
        }

        /// <summary>
        /// Filtra las ordenes que tuvieron como gestión un convenio, la búsqueda se realiza en la tabla de bitácora
        /// </summary>
        /// <param name="ordenes">cadena de ordenes separadas por , para realizar la búsqueda</param>
        /// <param name="formulario">Formulario en el cual se realiza la búsqueda</param>
        /// <returns>ordenes que no tuvieron un convenio</returns>
        public string FiltrarConveniosOrdenesBitacora(string ordenes, string formulario)
        {
            var instancia = ConnectionDB.Instancia;
            SqlConnection cnn = null;
            string resultado;
            try
            {
                cnn = instancia.IniciaConexion("SqlDefault");
                var sc = new SqlCommand("FiltrarConveniosOrdenesBitacora", cnn);
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@Formulario", SqlDbType.VarChar, 50) { Value = formulario };
                parametros[1] = new SqlParameter("@Ordenes", SqlDbType.VarChar, 8000) { Value = ordenes };
                parametros[2] = new SqlParameter("@OrdenesFiltradas", SqlDbType.VarChar, 8000) { Direction = ParameterDirection.Output };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                sc.ExecuteNonQuery();
                resultado = sc.Parameters["@OrdenesFiltradas"].Value.ToString();

                cnn.Close();
            }
            catch (Exception)
            {
                if (cnn != null)
                    cnn.Close();
                throw;
            }

            return resultado;
        }

        /// <summary>
        /// Genera una orden o actualiza de acuerdo a los parámetros enviados
        /// </summary>
        /// <param name="credito">Crédito al cual se va a relacionar la orden</param>
        /// <param name="idUsuario">usuario del gestor</param>
        /// <param name="idUsuarioPadre">usuario del supervisor</param>
        /// <param name="idUsuarioAlta">usuario del admin de despacho</param>
        /// <param name="idDominio">dominio del usuario</param>
        /// <param name="idOrden">orden a procesar , si se desea insertar el registro de la orden se tiene que enviar  un  -1</param>
        /// <returns>tablas necesarias para generar el xml de asignacion</returns>
        public DataSet GeneraOrdenXml(string credito, int idUsuario, int idUsuarioPadre, int idUsuarioAlta, int idDominio, int idOrden)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[7];
                parametros[0] = new SqlParameter("@idPool", SqlDbType.Int) { Value = 1 };
                parametros[1] = new SqlParameter("@Credito", SqlDbType.NVarChar, 50) { Value = credito };
                parametros[2] = new SqlParameter("@idUsuario", SqlDbType.Int) { Value = idUsuario };
                parametros[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) { Value = idUsuarioPadre };
                parametros[4] = new SqlParameter("@idUsuarioAlta", SqlDbType.Int) { Value = idUsuarioAlta };
                parametros[5] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
                parametros[6] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };
                return instancia.EjecutarDataSet("SqlDefault", "GeneraOrdenXML3", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "GeneraOrdenXml", ex.Message);
            }
            return null;
        }

        public bool ActualizarDictamenOrden(int idOrden)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idOrden", SqlDbType.Int) { Value = idOrden };
                instancia.EjecutarNonQuery("SqlDefault", "ActualizarDictamenOrden", parametros);
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ActualizarDictamenOrden", ex.Message);
            }
            return false;
        }
    }
}
