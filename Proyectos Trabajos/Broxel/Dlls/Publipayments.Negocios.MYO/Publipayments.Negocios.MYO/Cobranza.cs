using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Entidades.MYO;
using PubliPayments.Negocios;
using Publipayments.Negocios.MYO.NotificacionesWebServices;
using Publipayments.Negocios.MYO.WSCreditosMyo;
using PubliPayments.Utiles;

namespace Publipayments.Negocios.MYO
{
    public class Cobranza
    {

        public string Empresa = ConfigurationManager.AppSettings["MYOEmpresa"];
        public string Producto = ConfigurationManager.AppSettings["MYOProducto"];
        private static readonly string MyoMensajeSMS = ConfigurationManager.AppSettings["MYOMensajeSMS"];

        /// <summary>
        /// Método que se conecta a WS para obtener los registros de los créditos que no han presentado pago y tienen uno o mas periodos de atraso
        /// </summary>
        public void ObtenerSAFCarteraVencida()
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Negocios.MYO.Cobranza", "Iniciando... ObtenerCarteraVencida");
            var dia = DateTime.Now.ToString("yyyyMMdd");

            var myoCreditos = new CreditosClient();
            var adeudos = myoCreditos.ObtenResumenAdeudos(Empresa, Producto, dia);

            var creditos = String.Join("','", adeudos.Select(x => x.Num_Credito));
             creditos = "'" + creditos + "'";
            
            new EntCobranza().DesactivarCreditosPagados(creditos);

            foreach (var result in adeudos)
            {
                InsCarteraVencida(Convert.ToInt64(result.Num_Credito), result.COD_CLIENTE, result.Cod_Agencia,
                    result.Cod_Empresa, result.NOM_CLIENTE, result.NumPagosAtrasados, result.Mon_Deduccion,
                    result.DIA_ULT_PAGO, result.CASA, result.CELULAR, result.OFICINA);
            }

        }

        /// <summary>
        /// Se encarga de llenar la tabla CarteraVencida con datos nuevos
        /// </summary>
        /// <param name="numCredito"> Crédito de cliente</param>
        /// <param name="codCliente"> Código del cliente</param>
        /// <param name="codAgencia">Código de la agencia</param>
        /// <param name="codEmpresa">Código de la empresa</param>
        /// <param name="nomCliente"> Nombre del cliente</param>
        /// <param name="numPagosAtrasados">Numero de pagos atrasados</param>
        /// <param name="monDeduccion">Cantidad que tiene que pagar</param>
        /// <param name="diaUltPago">Día en que se debió de realizar ultimo pago</param>
        /// <param name="casa">Número de teléfono de casa</param>
        /// <param name="celular">Número de Celular</param>
        /// <param name="oficina">Número de teléfono de oficina</param>
        private void InsCarteraVencida(long numCredito
            , string codCliente
            , string codAgencia
            , string codEmpresa
            , string nomCliente
            , int? numPagosAtrasados
            , decimal? monDeduccion
            , DateTime? diaUltPago
            , string casa
            , string celular
            , string oficina)
        {
            //var ds=ObtenerProductoXCredito(numCredito);
            var entCobranza = new EntCobranza();
            //if (ds!=null && ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1)
            //{
            ////    var frecuencia = ds.Tables[0].Rows[0]["Frecuencia"];
            //    var loanDocumentationRequestId ="AC"+ ds.Tables[0].Rows[0]["loanDocumentationRequestId"].ToString().PadLeft(10, '0');
            //    var email = Convert.ToString(ds.Tables[0].Rows[0]["email"]);

                //if (frecuencia!=null)
                //{
            entCobranza.InsCarteraVencida("", numCredito, codCliente, codAgencia, codEmpresa, nomCliente, numPagosAtrasados, monDeduccion, diaUltPago, casa, celular, oficina, "", "semanal");
                //}
            //}
            //else
            //{
            //    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Negocios.MYO.Cobranza", string.Format("Error: InsCarteraVencida: El crédito {0} no tiene un producto relacionado", numCredito));
            //}

            
        }


        /// <summary>
        /// Obtiene el producto que se tiene relacionado a este crédito  (el producto que se contrato)
        /// </summary>
        /// <param name="numCredito">numero de crédito de SAF</param>
        /// <returns>información del producto</returns>
        public DataSet ObtenerProductoXCredito(long numCredito)
        {
            var entCobranza = new EntCobranza();
            return entCobranza.ObtenerProductoXCredito(numCredito);
        }

        /// <summary>
        /// Se encarga de procesar los créditos que se encuentran en cartera vencida e inserta la notificación correspondiente
        /// </summary>
        public void ProcesarCarteraVencida()
        {

            var entCobranza = new EntCobranza();
            var carteraVencida=entCobranza.ObtenerCarteraVencida();

            if (carteraVencida!=null&& carteraVencida.Tables.Count>0 && carteraVencida.Tables[0].Rows.Count>0)
            {

                foreach (DataRow row in carteraVencida.Tables[0].Rows)
                {
                    entCobranza.ProcesarAlertasXId(Convert.ToInt32(row["id"]));
                }    
            }
        }

        /// <summary>
        /// Se encarga de mandar las alertar que están pendientes
        /// </summary>
        public void EnviarAlertas()
        {
            var entCobranza = new EntCobranza();
            try
            {
                var alertas = entCobranza.ObtenerRegistroAlertas();
                if (alertas != null && alertas.Tables.Count > 0 && alertas.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in alertas.Tables[0].Rows)
                    {
                        var idRegistroAlerta = Convert.ToInt32(row["id"]);
                        try
                        {
                            var tipoAlerta = Convert.ToInt32(row["TipoAlerta"]);
                            var receptor = Convert.ToString(row["Receptor"]);
                            var textoUnicoArr = Convert.ToString(row["TextoUnico"]);
                            var textoUnico = textoUnicoArr.Split('|');
                            var fecha = Convert.ToDateTime(textoUnico[3]);
                            var mensaje = string.Format(MyoMensajeSMS, textoUnico[0], textoUnico[1], textoUnico[2], fecha.ToString("dd/MM/yyyy"));

                            switch (tipoAlerta)
                            {
                                case 1: //EMAIL
                                    if (!string.IsNullOrEmpty(receptor))
                                    {
                                        if (Email.EnviarEmail(receptor, "Información de su crédito MYO", mensaje, true))
                                        {
                                            entCobranza.ActualizaAlertaEnviada(idRegistroAlerta, "Enviado");
                                        }
                                    }


                                    break;
                                case 2: //SMS
                                    //Buen día, su crédito MYO No {0} tiene {1} semanas adeudo por la cantidad {2} . Favor de pagarlo antes del día {3}
                                    var envioSMS = new EnvioSMS().EnviarSMSC3ntro(receptor, mensaje);
                                    var resultado = Convert.ToString(envioSMS.LogId);
                                    entCobranza.ActualizaAlertaEnviada(idRegistroAlerta, resultado);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {

                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Negocios.MYO.Cobranza", "Error: EnviarAlertas_ idRegistroAlerta  " + idRegistroAlerta + ex.Message);
                        }

                       
                    }

                }
            }
            catch (Exception ex )
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Negocios.MYO.Cobranza", "Error: EnviarAlertas: "+ ex.Message);
               
            }

        }


        #region CobranzaPreventiva

        public void ObtenerDatosCobranzaPreventiva()
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Negocios.MYO.Cobranza", "Iniciando... ObtenerDatosCobranzaPreventiva");
            Trace.WriteLine(String.Format("{0} - Negocios.MYO.Cobranza: Iniciando... ObtenerDatosCobranzaPreventiva", DateTime.Now));
            var notificacionesWS = new NotificacionesWebServicesClient();

                var notificaciones = notificacionesWS.ObtenerNotificaciones(Convert.ToInt32(Empresa), Producto);
                var entCobranza = new EntCobranza();
                foreach (var datos in notificaciones)
                {
                    entCobranza.InsCarteraPreventiva(datos.Cuenta, datos.ClaveCliente, datos.Producto, datos.NomCliente, datos.Monto1, datos.Monto2, datos.DiaPago, datos.Casa, datos.Celular, datos.Oficina, datos.Email, datos.Estado, datos.Direccion);

                }

            Trace.WriteLine(String.Format("{0} - Negocios.MYO.Cobranza: Fin... ObtenerDatosCobranzaPreventiva", DateTime.Now));

        }

        /// <summary>
        /// Se encarga de enviar todas aquellas alertas que se tienen en la tabla de RegistroAlertas
        /// </summary>
        public void EnviarAlertasPreventivas()
        {
            var entCobranza = new EntCobranza();
            try
            {
                var alertas = entCobranza.ObtenerRegistroAlertas();
                if (alertas != null && alertas.Tables.Count > 0 && alertas.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in alertas.Tables[0].Rows)
                    {
                        var idRegistroAlerta = Convert.ToInt32(row["id"]);
                        try
                        {
                            var tipoAlerta = Convert.ToInt32(row["TipoAlerta"]);
                            var receptor = Convert.ToString(row["Receptor"]);
                            var textoUnicoArr = Convert.ToString(row["TextoUnico"]);
                            var textoUnico = textoUnicoArr.Split('|');
                            var fechaActual = DateTime.Now.ToString("d MMMM yyy", CultureInfo.CreateSpecificCulture("es-MX"));
                            var cuenta = Convert.ToString(row["Cuenta"]);
                            var mensajeServ = new MensajesServiciosModel(null, null, null, null, false, 0);
                            switch (tipoAlerta)
                            {
                                case 1: //EMAIL
                                    if (!string.IsNullOrEmpty(receptor))
                                    {
                                        mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("NotificacionPreventivaEmail", 1);
                                        mensajeServ.Mensaje =
                                            mensajeServ.Mensaje.Replace("{{FechaActual}}", fechaActual)
                                                .Replace("{{NumCuenta}}", cuenta)
                                                .Replace("{{NombreCliente}}", textoUnico[0]);
                                        if (Email.EnviarEmail(receptor, mensajeServ.Titulo, mensajeServ.Mensaje, true))
                                        {
                                            entCobranza.ActualizaAlertaEnviada(idRegistroAlerta, "Enviado");
                                        }
                                    }


                                    break;
                                case 2: //SMS
                                    var fecha2 = Convert.ToDateTime(textoUnico[0]);
                                    mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("NotificacionPreventivaSMS", 1);
                                    mensajeServ.Mensaje =
                                        mensajeServ.Mensaje.Replace("{{FechaLimite}}", fecha2.ToString("dd/MM/yyyy"));

                                    var envioSMS = new EnvioSMS().EnviarSMSC3ntro(receptor, mensajeServ.Mensaje);
                                    var resultado = Convert.ToString(envioSMS.LogId);
                                    entCobranza.ActualizaAlertaEnviada(idRegistroAlerta, resultado);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {

                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Negocios.MYO.Cobranza", "Error: EnviarAlertas_ idRegistroAlerta  " + idRegistroAlerta + ex.Message);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Negocios.MYO.Cobranza", "Error: EnviarAlertas: " + ex.Message);
            }

        }

        public void ProcesarCarteraPreventiva()
        {

            var entCobranza = new EntCobranza();
            var carteraVencida = entCobranza.ObtenerCarteraPreventiva();
            
            if (carteraVencida != null && carteraVencida.Tables.Count > 0 && carteraVencida.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow row in carteraVencida.Tables[0].Rows)
                {
                    entCobranza.ProcesaAlertasPreventivas(Convert.ToInt32(row["id"]));
                }
            }
            
        }

        #endregion


        public void InsCarteraPreventiva(string cuenta
            , string claveCliente
            , string producto
            , string nomCliente
            , float monto1
            , float monto2
            , DateTime diaPago
            , string casa
            , string celular
            , string oficina
            , string email
            , string estado
            , string direccion
            )
        {
            var entCobranza = new EntCobranza();
            entCobranza.InsCarteraPreventiva(cuenta, claveCliente, producto, nomCliente, monto1, monto2, diaPago, casa, celular, oficina, email, estado, direccion);
          
        }
    }
}
