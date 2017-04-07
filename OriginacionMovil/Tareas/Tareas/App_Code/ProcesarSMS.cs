using System;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public class ProcesarSMS
    {

        private static readonly string MensajeSMSAutorizacion = ConfigurationManager.AppSettings["MensajeSMSAutorizacion"];
        private static readonly string MensajeSMSError = ConfigurationManager.AppSettings["MensajeSMSError"];
        private static readonly string MensajeSMSNoEncontrado = ConfigurationManager.AppSettings["MensajeSMSNoEncontrado"];
        
        /// <summary>
        /// Procesa los registros de SMS
        /// </summary>
        public void Procesar()
        {
            //Obtener registros SMS
            var ent = new EntSMS();
            const string patron = @"[^\d]";
            var sms = ent.ObtenerSMSNoProcesados();
            foreach (DataRow row in sms.Tables[0].Rows)
            {
                var id = Convert.ToInt32(row["id"]);
                var telefono = row["NroTelefono"].ToString();
                var mensaje = row["Mensaje"].ToString();
                
                //limpio el mensaje
                mensaje = mensaje.Trim();
                var regex = new Regex(patron);
                //se eliminan todos los caracteres que no sean numeros
                var clave = regex.Replace(mensaje, "");
                
                if (clave.Length == 8)
                {
                    int numero;
                    if (int.TryParse(clave, out numero))
                    {
                        var resultado = ent.ValidaAutorizacionSMS(telefono, clave);
                        if (resultado.Tables.Count > 0 && resultado.Tables[0].Rows.Count > 0)
                        {
                            var result = Convert.ToInt32(resultado.Tables[0].Rows[0]["Resultado"]);
                            var texto = resultado.Tables[0].Rows[0]["Mensaje"];
                            var estatus = Convert.ToInt32(resultado.Tables[0].Rows[0]["Estatus"]);

                            switch (result)
                            {
                                case -1:
                                    var envio = new EnvioSMS();
                                    if (estatus != 4)
                                    {
                                        envio.EnviarSMS(telefono, MensajeSMSError);
                                    }
                                    ent.ActualizaTelefonosProcesados(id, "");
                                    break;
                                case -3:
                                    var envio2 = new EnvioSMS();
                                    ent.ActualizaTelefonosProcesados(-1, telefono);
                                    envio2.EnviarSMS(telefono, MensajeSMSNoEncontrado);
                                    break;
                                default:
                                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarSMS",
                                        string.Format("Error al validar SMS: {2} - Teléfono: {0} - Mensaje: {1}",
                                            telefono, mensaje, texto));
                                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                                        "Error al validar SMS",
                                        string.Format("Error al validar SMS: {2} - Teléfono: {0} - Mensaje: {1}",
                                            telefono, mensaje, texto));
                                    ent.ActualizaTelefonosProcesados(id, "");
                                    break;
                                case 1:
                                    var envio3 = new EnvioSMS();
                                    ent.ActualizaTelefonosProcesados(-1, telefono);
                                    envio3.EnviarSMS(telefono, MensajeSMSAutorizacion);
                                    break;
                            }
                        }
                        else
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarSMS",
                                string.Format("No se pudo validar el SMS - Teléfono: {0} - Mensaje: {1}", telefono,
                                    mensaje));
                        }
                    }
                    else
                    {
                        //Valida que la situacion de la orden del telefono se encuentre != 4
                        if (ent.ValidaEnvioDeSMS(telefono) == false)
                        {
                            ent.ActualizaTelefonosProcesados(id, "");
                            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarSMS",
                                string.Format(
                                    "El telefono {0} ya se encuentra con una orden autorizada, no se envio SMS",
                                    telefono));
                        }
                        else
                        {
                            ent.ActualizaTelefonosProcesados(id, "");
                            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarSMS",
                                string.Format("El mensaje no es numérico - Teléfono: {0} - Mensaje: {1}", telefono,
                                    mensaje));
                            var envio = new EnvioSMS();
                            envio.EnviarSMS(telefono, MensajeSMSError);
                        }
                    }
                }
                else
                {
                    //Valida que la situacion de la orden del telefono se encuentre != 4
                    if (ent.ValidaEnvioDeSMS(telefono) == false)
                    {
                        ent.ActualizaTelefonosProcesados(id, "");
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarSMS",
                            string.Format("El telefono {0} ya se encuentra con una orden autorizada, no se envio SMS",
                                telefono));
                    }
                    else
                    {
                        ent.ActualizaTelefonosProcesados(id, "");
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "ProcesarSMS",
                            string.Format("No se pudo procesar el mensaje - Teléfono: {0} - Mensaje: {1}", telefono,
                                mensaje));
                        var envio = new EnvioSMS();

                        if (telefono.Trim().Length == 10)
                            envio.EnviarSMS(telefono, MensajeSMSError);
                    }
                }
            }
        }
    }
}
