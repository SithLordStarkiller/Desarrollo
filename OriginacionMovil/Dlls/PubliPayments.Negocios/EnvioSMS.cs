using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class EnvioSMS
    {
        private static readonly string MensajeSMS = ConfigurationManager.AppSettings["MensajeSMS"];
        private static readonly string MensajeSMSCC = ConfigurationManager.AppSettings["MensajeSMSCC"];
        private static readonly string UsernameSMS = ConfigurationManager.AppSettings["UsernameSMS"];
        private static readonly string PasswordSMS = ConfigurationManager.AppSettings["PasswordSMS"];
        private static readonly string HostSMS = ConfigurationManager.AppSettings["HostSMS"];
        private static readonly bool IsProduction = ConfigurationManager.AppSettings["Produccion"] != null && ConfigurationManager.AppSettings["Produccion"].ToLower() == "true";

        public void EnviarSMSGestion()
        {
            try
            {
                var ds = new EntSMS().ObtenerTelefonosEnvioSMS();

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dsTelefonosSMS = ds.Tables[0];
                    for (int i = 0; i < dsTelefonosSMS.Rows.Count; i++)
                    {
                        var idOrden = Convert.ToInt32(dsTelefonosSMS.Rows[i]["idOrden"].ToString());
                        var telefono = dsTelefonosSMS.Rows[i]["Telefono"].ToString();
                        var clave = dsTelefonosSMS.Rows[i]["Clave"].ToString();
                        var dictamen = dsTelefonosSMS.Rows[i]["Dictamen"].ToString();
                        var mensajeSMS = dsTelefonosSMS.Rows[i]["esCallCenter"].ToString()=="0" ? MensajeSMS.Replace("{0}", dictamen).Replace("{1}", clave) : MensajeSMSCC.Replace("{0}", dictamen).Replace("{1}", clave);
                        if (EnviarMensaje(telefono, mensajeSMS, idOrden))
                        {
                            if (dsTelefonosSMS.Rows[i]["esCallCenter"].ToString() == "0")
                            {
                                new Orden().ActualizarEstatusUsuarioOrdenes(Convert.ToString(idOrden), 15, -1, true, true, 1); // se manda la reasignacion automática a los móviles        
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnviarSMSGestion", ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                
            }
           
        }

        private bool EnviarMensaje(string celular, string smsMensaje, int idOrden)
        {

            smsMensaje = NoEspeciales(smsMensaje);
            var sms = new wsSms.ServicioSMSClient();
            var entAutorizacionSMS = new EntSMS();

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "EnviarMensaje", "Enviando SMS: Orden " + idOrden.ToString(CultureInfo.InvariantCulture) + " Celular " + celular);
            try
            {
                if (IsProduction)
                {
                    var respuesta = sms.EnviarSMS(new wsSms.SMS
                    {
                        Mensaje = smsMensaje,
                        Telefono = celular

                    }, new wsSms.Credenciales
                    {
                        Username = UsernameSMS,
                        Password = PasswordSMS,
                        Host = HostSMS
                    });
                    if (respuesta == null || !respuesta.Enviado)
                    {
                        var error = "No se envió el SMS, error de comunicación con quiubas- Orden " + idOrden + " celular: " + celular;
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnviarMensaje", error);
                    }
                    else
                    {
                        var logId = respuesta.LogId;
                        entAutorizacionSMS.ActualizacionEnvioSMS(idOrden, logId);
                        return true;
                    }
                }
                else
                {
                    entAutorizacionSMS.ActualizacionEnvioSMS(idOrden, 336699);
                    return true;
                }
               
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnviarMensaje", ex.Message+
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
            }
            return false;

        }

        public void EnviarSMS(string telefono, string mensaje)
        {
            try
            {
                var sms = new wsSms.ServicioSMSClient();
                var respuesta = sms.EnviarSMS(new wsSms.SMS
                {
                    Mensaje = mensaje,
                    Telefono = telefono

                }, new wsSms.Credenciales
                {
                    Username = UsernameSMS,
                    Password = PasswordSMS,
                    Host = HostSMS
                });
                if (respuesta == null || !respuesta.Enviado)
                {
                    var error =
                        string.Format(
                            "EnviarSMS - No se envió el SMS, error de comunicación con quiubas, Telefono:{0} - Mensaje:{1}",
                            telefono, mensaje);
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnvioSMS", error);
                }
                else
                {
                    var resp =
                        string.Format(
                            "EnviarSMS - Se envio SMS Respuesta servicio: {2} - Telefono:{0} - Mensaje:{1}",
                            telefono, mensaje, respuesta.LogId);
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "EnvioSMS", resp);
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnvioSMS", "EnviarSMS - " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                
                Email.EnviarEmail(
                    "sistemasdesarrollo@publipayments.com",
                    "Error al enviar SMS",
                    string.Format("Error al enviar SMS: {2} - Teléfono: {0} - Mensaje: {1}",
                        telefono, mensaje, ex.Message));
            }
        }

        public wsSms.Respuesta EnviarSMSC3ntro(string telefono, string mensaje)
        {
            try
            {
                var sms = new wsSms.ServicioSMSClient();
                var respuesta = sms.EnviarSMSC3ntro(new wsSms.SMS
                {
                    Mensaje = mensaje,
                    Telefono = telefono

                }, new wsSms.Credenciales
                {
                    Username = UsernameSMS,
                    Password = PasswordSMS,
                    Host = HostSMS
                });
                if (respuesta == null || !respuesta.Enviado)
                {
                    var error =
                        string.Format(
                            "EnviarSMS - No se envió el SMS, error de comunicación con C3ntro, Telefono:{0} - Mensaje:{1}",
                            telefono, mensaje);
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnviarSMSC3ntro", error);
                }
                else
                {
                    var resp =
                        string.Format(
                            "EnviarSMS - Se envio SMS Respuesta servicio: {2} - Telefono:{0} - Mensaje:{1}",
                            telefono, mensaje, respuesta.LogId);
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "EnviarSMSC3ntro", resp);
                }
                return respuesta;
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EnvioSMS", "EnviarSMS - " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));

                Email.EnviarEmail(
                    "sistemasdesarrollo@publipayments.com",
                    "Error al enviar SMS",
                    string.Format("Error al enviar SMS: {2} - Teléfono: {0} - Mensaje: {1}",
                        telefono, mensaje, ex.Message));
            }
            return null;
        }



        private static string NoEspeciales(string texto)
        {
            string respuesta = texto.Replace("ñ", "?")
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("Á", "A")
                .Replace("É", "E")
                .Replace("Í", "I")
                .Replace("Ó", "O")
                .Replace("Ú", "U");
            return respuesta;
        }
    }
}
