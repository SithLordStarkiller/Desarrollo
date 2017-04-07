using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    internal class EnviarCancelados : Tarea
    {
        public override int Ejecutar()
        {
            int ejecucionOk = 0;

            //if (Estatus == 0)
            //    Estatus = 1;
            //else
            //{
            //    return 0;
            //}

            ////Selecciona Ordenes
            //var entOrdenes = new EntOrdenes();

            //Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "EnviarCancelados", "Consultando ordenes en estatus 12");
            ////obtengo las ordenes por asignar a dispositivo
            //var ordenes = entOrdenes.ObtenerOrdenesPorEstatus(12, 100, 1); //Uduario ADMIN

            //if (ordenes.Count > 0)
            //{
            //    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarCancelados",
            //        "Preparandose para cancelar " + ordenes.Count + " órdenes");
            //    //Para el rol de asignación a dispositivo siempre va a ser 3
            //    var orden = new Orden(3, Config.IsProduction, Config.IdClient, Config.IdProduct);

            //    //Envia cancelacion Ordenes
            //    var resultado = orden.EnviarCancelacionWs(ordenes, 1); //Usuario ADMIN

            //    //Si falla, envia correo y Continua
            //    if (ordenes.Count != resultado)
            //    {
            //        Email.EnviarEmail(
            //            new List<string> {"m.silva@publipayments.com", "a.rojas@publipayments.com"},
            //            "Tareas - Error al enviar ordenes al web service",
            //            "La cantidad de órdenes canceladas al ws no corresponde con el resultado");
            //        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarAsignados",
            //            "Error al enviar ordenes al web service");
            //        //ejecucionOk = false;
            //    }
            //    else
            //    {
            //        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarCancelados", "Marcando ordenes como asignadas");
            //        //Si se envió marca con estatus 1
            //        var listaTextoOrdenes = String.Join(",", ordenes.Select(x => x.IdOrden).ToArray());
            //        ejecucionOk = entOrdenes.ActualizarEstatusUsuarioOrdenes(listaTextoOrdenes, 1, -1, false, false, 1); //Usuario ADMIN
            //        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarCancelados",
            //            "Se marcaron " + ordenes.Count + " órdenes");
            //    }
            //}
            //else
            //{
            //    Logger.WriteLine(Logger.TipoTraceLog.TraceDisco, 1, "EnviarCancelados", "No se econtraron órdenes");
            //}

            //Estatus = 0;

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


