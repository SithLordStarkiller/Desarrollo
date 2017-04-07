using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    class EnviarReasignados : Tarea
    {
        public override int Ejecutar()
        {
            int ejecucionOk = 0;

            try
            {
                if (Estatus == 0)
                    Estatus = 1;
                else
                {
                    return 0;
                }

                //Selecciona Ordenes
                var entOrdenes = new EntOrdenes();

                Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "EnviarReasignados", "Consultando ordenes en estatus 15");
                //obtengo las ordenes por asignar a dispositivo
                var ordenes = entOrdenes.ObtenerOrdenesPorEstatus(15, 100, 1); //Uduario ADMIN

                if (ordenes.Count > 0)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarReasignados",
                        "Preparandose para reasignar " + ordenes.Count + " órdenes");
                    //Para el rol de asignación a dispositivo siempre va a ser 3
                    var entFormulario = new EntFormulario().ObtenerListaFormularios(ordenes.FirstOrDefault().Ruta);
                    var orden = new Orden(3, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario[0].Nombre, entFormulario[0].Version,Config.AplicacionActual().Nombre);

                    //Envia cancelacion Ordenes
                    //var resultado = orden.EnviarCancelacionWs(ordenes, 1); //Usuario ADMIN

                    ////Si falla, envia correo y Continua
                    //if (ordenes.Count != resultado)
                    //{
                    //    Email.EnviarEmail(
                    //        new List<string> { "m.silva@publipayments.com", "a.rojas@publipayments.com" },
                    //        "Tareas - Error al reasignar ordenes al web service",
                    //        "La cantidad de órdenes reasignadas al ws no corresponde con el resultado");
                    //    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarAsignados",
                    //        "Error al enviar ordenes al web service");
                    //    //ejecucionOk = false;
                    //}
                    //else
                    //{
                    //Si fue correcto envio las nuevas asignaciones 
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarReasignados",
                        "Preparandose para enviar " + ordenes.Count + " órdenes");
                    //Envia Ordenes
                    var result = orden.EnviarOrdenesWs(ordenes, true, 1); //Usuario ADMIN

                    ////Si falla, envia correo y Continua
                    //if (ordenes.Count != result)
                    //{
                    //    Email.EnviarEmail(
                    //        new List<string> { "m.silva@publipayments.com", "a.rojas@publipayments.com" },
                    //        "Tareas - Error al reasignar ordenes al web service",
                    //        "La cantidad de órdenes reasignadas al ws no corresponde con el resultado");
                    //    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarAsignados",
                    //        "Error al enviar ordenes al web service");
                    //}
                    //else
                    //{
                    //    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarReasignados",
                    //        "Marcando ordenes como asignadas");
                    //    //Si se envió marca con estatus 1
                    //    var listaTextoOrdenes = String.Join(",", ordenes.Select(x => x.IdOrden).ToArray());
                    //    ejecucionOk = entOrdenes.ActualizarEstatusUsuarioOrdenes(listaTextoOrdenes, 1, -1, false, false, 1);
                    //    //Usuario ADMIN
                    //    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarReasignados",
                    //        "Se marcaron " + ordenes.Count + " órdenes");
                    //}
                    //}
                }
                else
                {
                    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "EnviarReasignados", "No se econtraron órdenes");
                }

                Estatus = 0;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EnviarReasignados",
                 "Error: Ejecutar reasignación " + ex.Message +
                 (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                
            }
     
            return ejecucionOk;
        }

        public override int Terminar()
        {
            while (Estatus == 1)
            {
                Thread.Sleep(1000);
            }
            Estatus = -1;
            return 0;
        }
    }
}
