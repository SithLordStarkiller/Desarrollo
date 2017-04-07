using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class ExpedienteServicios
    {  
        public static void GuardarExpediente(string credito)
        {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //var cliente = new ExpedienteDigitalClient();
                //cliente.Open();
                //var model = new ModeloGuardarExpediente
                //{
                //    Credito = credito                    
                //};
                //Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "NegocioOriginacion-GeneraPdfOriginacion-ConsumiendoExpedienteDigital.svc",
                //      "Tareas-GeneraPdfOriginacion-ConsumiendoExpedienteDigital.svc - Terminado");


                //Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "NegocioOriginacion-GeneraPdfOriginacion-EvaluandoRespuestaExpedienteDigital.svc",
                //      "Tareas-GeneraPdfOriginacion-EvaluandoRespuestaExpedienteDigital.svc...");

                //var response = cliente.GuardarExpediente(model);

                //Logger.WriteLine(Logger.TipoTraceLog.Error, 1,
                //    "ExpedientesServicio-GeneraPdfOriginacion-Respuesta ExpedienteDigital.svc.GuardarExpediente",
                //    "Error en GeneraPdfOriginacion--Respuesta Recibida por ExpedienteDigital.svc.GuardarExpediente: " +
                //    response.Codigo + ", Mensaje - " + response.Mensaje);

                //if (response.Codigo == 1)
                //{
                //    var ordenesActualizadas = new EntOrdenes().ActualizaEstatusOrdenes(orden, 9, false, false);
                //    if (ordenesActualizadas < 1)
                //    {
                //        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "NegocioOriginacion-GeneraPdfOriginacion-ActualizarEstatusOrden",
                //       "Error en GeneraPdfOriginacion-ActualizarEstatusOrden, se guardo el documento pero no se pudo actualizar la orden a su nuevo estatus, Orden: " + orden);
                //    }
                //}

                //Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "NegocioOriginacion-GeneraPdfOriginacion-EvaluandoRespuestaExpedienteDigital.svc",
                //      "Tareas-GeneraPdfOriginacion-EvaluandoRespuestaExpedienteDigital.svc - Terminado");

                //cliente.Close();
        }
    }
}
