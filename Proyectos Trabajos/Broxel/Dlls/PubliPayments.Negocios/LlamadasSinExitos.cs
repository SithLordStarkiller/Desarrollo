using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Math.EC;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class LlamadasSinExitos
    {
        /// <summary>
        /// Método que lee el archivo  de llamadas no exitosas  que carga el administrador
        /// </summary>
        /// <param name="idUsuario">Id del usuario que carga el archivo</param>
        /// <param name="archivo">Nombre del archivo</param>
        /// <param name="sr">Archivo</param>
        /// <returns>Mensaje del proceso de lectura del archivo</returns>
        public string LeeArchivoLLamadas(int idUsuario, string archivo, StreamReader sr)
        {
            string mensaje = string.Empty;
            var datos = new DataSet();
            var context = new EntArchivos();
            var idArchivo = -1;

            try
            {
                datos = context.InsUpsArchivos(idArchivo, archivo, "Procesando", 0, "txt", 100, idUsuario);

                if (datos.Tables.Count > 0 && datos.Tables[0].Rows.Count > 0)
                {
                    string error = "Error: ";
                    if (Convert.ToInt16(datos.Tables[0].Rows[0]["Codigo"]) != 0)
                    {
                        error += datos.Tables[0].Rows[0]["Descripcion"];
                    }
                    else
                    {
                        idArchivo = Convert.ToInt16(datos.Tables[1].Rows[0]["id"]);
                    }

                    if (error != "Error: ")
                    {
                        return "Error: " + datos + ". " + error;
                    }
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "LeeArchivoLLamadas:No se pudo registrar el archivo en la base de datos");
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                    "LeeArchivoLLamadas:No se pudo registrar el archivo en la base de datos");

                    mensaje = "No se pudo leer el archivo, validar";
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "LeeArchivoLLamadas:No se pudo registrar el archivo en la base de datos: " + ex.Message);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                    "LeeArchivoLLamadas:No se pudo registrar el archivo en la base de datos: " + ex.Message);

                mensaje = "No se pudo leer el archivo, validar";

                return mensaje;
            }

            try
            {
                int totalRegistros = 0;
                var lineasConError = new List<string>();
                var catalogoResultados = new DataSet();

                catalogoResultados = context.ObtieneResultadosLlamadas();

                if (catalogoResultados != null)
                {
                    if (catalogoResultados.Tables.Count > 0 && catalogoResultados.Tables[0].Rows.Count > 0)
                    {
                        using (sr)
                        {
                            while (sr.Peek() > -1)
                            {
                                string line = sr.ReadLine();
                                if (string.IsNullOrEmpty(line))
                                    break;

                                totalRegistros++;
                                string line1 = line;

                                string error = ProcesarLinea(idArchivo, line1, catalogoResultados);

                                if (error != string.Empty)
                                    lineasConError.Add(error);
                            }
                        }
                    }
                }

                if (lineasConError.Count > 0)
                {
                    var sbErrores = new StringBuilder();
                    foreach (var l in lineasConError)
                    {
                        sbErrores.Append(l);
                        sbErrores.Append(Environment.NewLine);
                    }

                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoProcesado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivo);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, totalRegistros, lineasConError.Count,
                        archivo, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    mensaje = mensajeServ.Mensaje;
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);

                    try
                    {
                        context.InsArchivosError(idArchivo, sbErrores.ToString());
                    }
                    catch (Exception errEx)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                            "LeeArchivoLLamadas:No se pudo guardar el registro de errores." + errEx.Message);
                        Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                            "LeeArchivoLLamadas:No se pudo guardar el registro de errores." + errEx.Message);
                    }
                }
                else
                {
                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ArchivoProcesado");
                    mensajeServ.Titulo = string.Format(mensajeServ.Titulo, archivo);
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, totalRegistros, lineasConError.Count,
                        archivo, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    mensaje = mensajeServ.Mensaje;
                    NotificacionesEmail(mensajeServ.Titulo, mensajeServ.Mensaje, false, mensajeServ.EsHtml);
                }

                try
                {
                    context.InsUpsArchivos(idArchivo, archivo, "Procesado", totalRegistros, "txt", 100, idUsuario);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                        "LeeArchivoLLamadas:No se pudo actualizar la información del archivo en la base de datos" +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                        "LeeArchivoLLamadas:No se pudo actualizar la información del archivo en la base de datos" +
                        ex.Message);
                }
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Archivos",
                    archivo + " Se procesaron " + totalRegistros.ToString(CultureInfo.InvariantCulture) +
                    " registros.");
                Console.WriteLine(archivo + " Se procesaron " + totalRegistros.ToString(CultureInfo.InvariantCulture) +
                                  " registros.");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Archivos",
                    "LeeArchivoLLamadas:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                    ex.Message);
                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "Archivos",
                    "LeeArchivoLLamadas:No se pudo actualizar la información del archivo en la base de datos, se continua procesando el archivo..." +
                    ex.Message);
            }

            return mensaje;
        }

        /// <summary>
        /// Metodo que procesa línea por línea el archivo recibido
        /// </summary>
        /// <param name="idArchivo">Id del archivo</param>
        /// <param name="line">Contenido de la linea</param>
        /// <returns>Cadena con error del procesamiento de la línea</returns>
        private static string ProcesarLinea(int idArchivo, string line, DataSet catalogoResultados)
        {
            //100  LLamadas sin éxito
            var errorSalida = new StringBuilder();
            string[] linea = line.Split('|');
            var credito = string.Empty;
            var telefono = string.Empty;
            var fechaAlta = new DateTime(1900, 12, 01);
            var fechaLlamada = string.Empty;
            var Val = new Regex(@"^\d*$");
            var valTel = new Regex(@"^\d{10}");
            var insertaDatos = new EntArchivos();
            int idResultado = 0;

            try
            {

                if (Val.IsMatch(linea[0]))
                {
                    credito = linea[0];
                }
                else
                {
                    errorSalida.Append("Error: " + linea[0] + ", el formato del credito es incorrecto --> " + linea[0] + " <--. ");
                }

                if (DateTime.TryParse(linea[1], out fechaAlta))
                {
                    IFormatProvider cultura = new CultureInfo("es-MX");
                    DateTime fecha = DateTime.Parse(linea[1], cultura);

                    if (fecha <= DateTime.Now)
                    {
                        fechaLlamada = fecha.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    else
                    {
                        errorSalida.Append("Error: " + credito + ", la fecha ingresada no es valida --> " + linea[1] + " <--. ");
                    }
                }
                else
                {
                    errorSalida.Append("Error: " + credito + ", la fecha ingresada no es valida --> " + linea[1] + " <--. ");
                }

                if (valTel.IsMatch(linea[2]))
                {
                    telefono = linea[2];
                }
                else
                {
                    errorSalida.Append("Error: " + credito + ", el formato del telefono es incorrecto --> " + linea[2] + " <--. ");
                }


                foreach (DataRow resultado in catalogoResultados.Tables[0].Rows)
                {
                    try
                    {
                        var Resultado = resultado["RESULTADO"].ToString();

                        if (Resultado.Equals(linea[3].ToUpper()))
                        {
                            int.TryParse(resultado["ID_RESULTADO"].ToString(), out idResultado);
                        }
                    }
                    catch
                        (Exception err)
                    {
                        errorSalida.Append(err.Message);
                    }
                }

                if (idResultado == 0)
                {
                    errorSalida.Append("Error: " + credito + ", el resultado de la gestion es incorrecto --> " + linea[3] + " <--. ");
                }
            }
            catch
                (Exception err)
            {
                errorSalida.Append(err.Message);
            }

            try
            {
                if (credito != string.Empty && fechaLlamada != string.Empty && telefono != string.Empty && idResultado != 0)
                {
                    var resultado = insertaDatos.InsUpdLLamadasSinExito(idArchivo, credito, fechaLlamada, telefono, idResultado);

                    if (resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                    {
                        string error = "Error: ";
                        if (Convert.ToInt16(resultado.Tables[0].Rows[0]["Codigo"]) != 0)
                        {
                            error += resultado.Tables[0].Rows[0]["Descripcion"];
                        }

                        if (error != "Error: ")
                        {
                            return "Error: " + credito + ". " + error;
                        }
                    }
                    else
                    {
                        return "Error: " + credito + ", la insersion no regreso ningun resultado. ";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error: " + credito + ". " + ex.Message + " -- Inner " + ex.InnerException;
            }

            return errorSalida.ToString();
        }

        #region NotificacionesEmail
        /// <summary>
        /// Método que envía notificación via correo electrónico 
        /// </summary>
        /// <param name="subject">Asunto</param>
        /// <param name="body">Cuerpo del mensaje</param>
        /// <param name="privado"></param>
        /// <param name="esHtml"></param>
        private static void NotificacionesEmail(string subject, string body, bool privado, bool esHtml = false)
        {
            try
            {
                var entUsuario = new EntUsuario();
                var usuarios = (privado) ? entUsuario.ObtenerUsuariosPorDominio("1") : entUsuario.ObtenerUsuariosPorDominio("1,2").Where(x => x.IdRol == 1 || x.IdRol == 0).ToList();
                var direccion = usuarios.GroupBy(usr => usr.Email).Select(usuario => usuario.Key).ToList();
                Email.EnviarEmail(direccion, subject, body, esHtml);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Tareas", "NotificacionesEmail - " + ex.Message);
            }
        }

        #endregion NotificacionesEmail

        /// <summary>
        /// Borra el registro de una llamada sin éxito y pasa el registro a bitácora
        /// </summary>
        /// <param name="credito">Numero de crédito</param>
        public void BorrarLlamadaSinExito(string credito)
        {
            try
            {
                var retultado= new  EntLlamadasSinExito().BorrarLlamadaSinExito(credito);
                if (Convert.ToInt32(retultado)>0)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "LlamadasSinExitos", string.Format("BorrarLlamadaSinExito_se borro el registro de llamada  Credito:{0}", credito));    
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "LlamadasSinExitos", string.Format("BorrarLlamadaSinExito_Error:{0}, Credito:{1}", ex.Message, credito));
            }
        }
    }
}
