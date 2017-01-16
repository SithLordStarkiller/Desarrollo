
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using PubliPayments.Utiles;
using PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11;


namespace PubliPayments.Negocios.Originacion
{
    /// <summary>
    /// Clase que nos permite consultar el esattus de una orden con el NSS del titular del crédito
    /// </summary>
    public class ConsultaEstatusMejoravit
    {
       
        /// <summary>
        /// Método para consultar el estatus de un NSS mediante un WS
        /// </summary>
        /// <param name="nSs">NSS a consultar</param>
        /// <returns>Objeto con la información que regresó el servicio</returns>
        public RespuestaEstatus ConsultaEstatus(string nSs)
        {
           
            RespuestaEstatus respEstatNss = new RespuestaEstatus();

            try
            {


                WSConsultaEstatusMejoravitAppSoapClient conEstatus =
                    new WSConsultaEstatusMejoravitAppSoapClient();
                
               
          
                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "ConsultaEstatus: parametro", nSs);


                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                WsResConsultaEstatus respuestaServicio = conEstatus.ConsultaEstatus(nSs);

                respEstatNss.FechaAlta = respuestaServicio.WSFechaAlta;
                respEstatNss.IdMensaje = respuestaServicio.WSidMensaje;
                respEstatNss.Mensaje = respuestaServicio.WSMensaje;
                respEstatNss.UsuarioAlta = respuestaServicio.WSUsuarioAlta;
                respEstatNss.Vuelta = respuestaServicio.WSvuelta.Trim() == String.Empty ? 0 : Convert.ToInt32(respuestaServicio.WSvuelta);
            }
            catch(Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ConsultaEstatus", ex.Message);
                respEstatNss.IdMensaje = "0020";
                respEstatNss.Mensaje = "Error al comunicarse con el servicio: " + ex.Message;
                respEstatNss.Vuelta = 0;
            }
            return respEstatNss;
        }

       
    }


    /// <summary>
    /// Clase  para regresar la respuesta del servicio 
    /// </summary>
    public class RespuestaEstatus
    {

        /// <summary>
        /// Identificador del mensaje
        /// </summary>
        public string IdMensaje { get; set; }
        public string Mensaje { get; set; }

        public int Vuelta { get; set; }

        public string UsuarioAlta { get; set; }

        public string FechaAlta { get; set; }


    }
}
