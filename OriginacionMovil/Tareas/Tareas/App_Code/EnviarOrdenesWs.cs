using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    class EnviarOrdenesWs : Tarea
    {
        public override int Ejecutar()
        {
            int cantidadOrdenesProcesadas = 0;

            //Obtengo las ordenes a enviar 
            var ent = new EntBitacoraEnvio();
            var listaOrdenesDup = new List<OrdenEstatusModel>();
            var listaOrdenesEnviar = new Dictionary<int, int>();
            var listaOrdenesCancelar = new List<OrdenEstatusModel>();
            var listaOrdenesReasignar = new List<OrdenEstatusModel>();
            var entOrdenes = new EntOrdenes();

            var dsPorEnviar = ent.ObtenerBitacoraEnvio(1);
            if (dsPorEnviar != null && dsPorEnviar.Tables.Count > 0 && dsPorEnviar.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsPorEnviar.Tables[0].Rows)
                {
                    var idOrden = Convert.ToInt32(row["idOrden"]);
                    var id = Convert.ToInt32(row["id"]);
                    var estatusAnterior = Convert.ToInt32(row["EstatusAnterior"]);
                    var estatus = Convert.ToInt32(row["EstatusEnvio"]);

                    bool encontrado = (from DataRow rowDup in dsPorEnviar.Tables[0].Rows
                        let idOrdenDup = Convert.ToInt32(rowDup["idOrden"])
                        let idDup = Convert.ToInt32(rowDup["id"])
                        where idOrden == idOrdenDup && id != idDup
                        select idOrdenDup).Any();

                    if (encontrado)
                        listaOrdenesDup.Add(new OrdenEstatusModel { Id = id, Orden = idOrden, EstatusAnterior = estatusAnterior, EstatusActual = estatus});
                    else
                    {
                        switch (estatus)
                        {
                            case 11:
                                listaOrdenesEnviar.Add(id, idOrden);
                                break;
                            case 12:
                                listaOrdenesCancelar.Add(new OrdenEstatusModel { Id = id, Orden = idOrden, EstatusAnterior = estatusAnterior, EstatusActual = estatus });
                                break;
                            case 15:                                
                                listaOrdenesReasignar.Add(new OrdenEstatusModel { Id = id, Orden = idOrden, EstatusAnterior = estatusAnterior, EstatusActual = estatus });
                                break;
                        }
                    }
                }
            }

            //Reviso que no se hayan enviado recientemente
            var dsEnviadoReciente = ent.ObtenerBitacoraEnvio(2);

            var ordenes = new Dictionary<int,int>();
 
            foreach (var ordenEnviar in listaOrdenesEnviar)
            {
                var idOrden = ordenEnviar.Value;
                bool encontrado =
                    (from DataRow row in dsEnviadoReciente.Tables[0].Rows select Convert.ToInt32(row["idOrden"])).Any(
                        idOrdenReciente => idOrden == idOrdenReciente);

                // tiene que esperar
                if (!encontrado)
                {
                    //Envia ordenes
                    ordenes.Add(ordenEnviar.Key,ordenEnviar.Value);

                    if (ordenes.Count > 100)
                    {
                       cantidadOrdenesProcesadas +=  EnviarMarcarOrdenesWs(ordenes, false, ent);
                        ordenes.Clear();
                    }
                }
            }
            cantidadOrdenesProcesadas += EnviarMarcarOrdenesWs(ordenes, false, ent);
            ordenes.Clear();


            //Procesar envios por cancelar
            var ordenesActualizar = new List<int>();
            
            foreach (var ordenCancelar in listaOrdenesCancelar)
            {
                if (ordenCancelar.EstatusAnterior == 3) //Si el estatus anterior de la orden es 3 solo actualizo 
                {
                    var resultadoFiltro= entOrdenes.FiltrarConveniosOrdenesBitacora(Convert.ToString(ordenCancelar.Orden), "%CallCenter%");   /// se busca si anteriormente fue gestionada con convenio de CallCenter
                    if (string.IsNullOrEmpty(resultadoFiltro))
                    {
                        ordenesActualizar.Add(ordenCancelar.Id);

                        if (ordenesActualizar.Count > 100)
                        {
                            cantidadOrdenesProcesadas += ActualizarBitacoraEnvio(string.Join(",", ordenesActualizar),
                                "No enviado a WS por estatus anterior = 3", ent);
                            ordenesActualizar.Clear();
                        }
                        continue;
                    }
                }

                var idOrden = ordenCancelar.Orden;

                bool encontrado =
                    (from DataRow row in dsEnviadoReciente.Tables[0].Rows select Convert.ToInt32(row["idOrden"])).Any(
                        idOrdenReciente => idOrden == idOrdenReciente);

                // tiene que esperar
                if (!encontrado)
                {
                    //Envia ordenes
                    ordenes.Add(ordenCancelar.Id, ordenCancelar.Orden);

                    if (ordenes.Count > 100)
                    {
                        cantidadOrdenesProcesadas += CancelarMarcarOrdenesWs(ordenes, ent);
                        ordenes.Clear();
                    }
                }
            }
            cantidadOrdenesProcesadas += CancelarMarcarOrdenesWs(ordenes, ent);
            ordenes.Clear();
            if (ordenesActualizar.Count > 0)
            {
                cantidadOrdenesProcesadas += ActualizarBitacoraEnvio(string.Join(",", ordenesActualizar),
                    "No enviado a WS por estatus anterior = 3", ent);
                ordenesActualizar.Clear();
            }


            //Procesar reasignaciones a enviar
            foreach (var ordenReasignar in listaOrdenesReasignar)
            {
                var idOrden = ordenReasignar.Orden;
                bool encontrado =
                    (from DataRow row in dsEnviadoReciente.Tables[0].Rows select Convert.ToInt32(row["idOrden"])).Any(
                        idOrdenReciente => idOrden == idOrdenReciente);

                // tiene que esperar
                if (!encontrado)
                {
                    //Envia ordenes
                    ordenes.Add(ordenReasignar.Id, ordenReasignar.Orden);
                    cantidadOrdenesProcesadas += EnviarMarcarOrdenesWs(ordenes, true, ent);
                    ordenes.Clear();
                }
            }

            //Reviso casos duplicados
            //Ordeno registros y reviso que no esten en los recientemente enviados
            var listaOrdenesDupFinal = (from o in listaOrdenesDup
                let idOrden = o.Orden
                let encontradoDup =
                    (from DataRow row in dsEnviadoReciente.Tables[0].Rows select Convert.ToInt32(row["idOrden"])).Any(
                        idOrdenReciente => idOrden == idOrdenReciente)
                where !encontradoDup
                select o).ToList().OrderBy(x => x.Orden);

            cantidadOrdenesProcesadas += ProcesarDuplicados(listaOrdenesDupFinal.ToList(), ent);

            return cantidadOrdenesProcesadas;
        }

        private int ProcesarDuplicados(List<OrdenEstatusModel> ordenesDup, EntBitacoraEnvio ent)
        {
            var cantidadOrdenesProcesadas = 0;
            if (ordenesDup.Count > 1)
            {
                int ordenAnterior = -1;
            
                for (int i = 0; i < ordenesDup.Count(); i++)
                {
                    if (ordenAnterior != ordenesDup[i].Orden)
                    {
                        ordenAnterior = ordenesDup[i].Orden;

                        int anterior = ordenAnterior;
                        var ordenesProcesar = (from o in ordenesDup
                            where o.Orden == anterior
                            orderby o.Id descending
                            select o).ToList();

                        if (ordenesProcesar.Count() == 2)
                        {
                            if (ordenesProcesar[0].EstatusActual == 12 && ordenesProcesar[1].EstatusActual == 11)
                            {
                                //Actualizo a ignorar
                                var ordenIgnorar = (from o in ordenesProcesar
                                    select o.Id).ToList();

                                cantidadOrdenesProcesadas += ActualizarBitacoraEnvio(string.Join(",", ordenIgnorar),
                                    "Se ignora por duplicado asignado y cancelado", ent);
                                continue;
                            }

                            if (ordenesProcesar[0].EstatusActual == 15 && ordenesProcesar[1].EstatusActual == 11)
                            {
                                //Envio Asignación y actualizo el que queda
                                var ordenAsignar = new Dictionary<int, int> { { ordenesProcesar[0].Id, ordenesProcesar[0].Orden } };
                                cantidadOrdenesProcesadas += EnviarMarcarOrdenesWs(ordenAsignar, false, ent);

                                cantidadOrdenesProcesadas += ActualizarBitacoraEnvio(ordenesProcesar[1].Id.ToString(CultureInfo.InvariantCulture),
                                    "Procesada asignación por duplicado para id:" + ordenesProcesar[1].Id, ent);
                                continue;
                            }
                        }

                        if (ordenesProcesar[0].EstatusActual == 12)
                        {
                            //Envio cancelación

                            var ordenCancelar = new Dictionary<int, int> { { ordenesProcesar[0].Id, ordenesProcesar[0].Orden } };
                            cantidadOrdenesProcesadas += CancelarMarcarOrdenesWs(ordenCancelar, ent);

                            var ordenesActCancelar = (from o in ordenesProcesar
                                                     where o.Id != ordenesProcesar[0].Id
                                                     select o.Id).ToList();

                            cantidadOrdenesProcesadas += ActualizarBitacoraEnvio(string.Join(",", ordenesActCancelar),
                                "Procesada cancelación por duplicado para id:" + ordenesProcesar[0].Id, ent);

                            continue;
                        }

                        //Si el ultimo es 11 o 15 envio una reasignación
                        var orden = new Dictionary<int, int> {{ordenesProcesar[0].Id, ordenesProcesar[0].Orden}};
                        var cantidadOrdenesProcesadasAntes = cantidadOrdenesProcesadas;
                        cantidadOrdenesProcesadas += EnviarMarcarOrdenesWs(orden, true, ent);

                        if (cantidadOrdenesProcesadasAntes != cantidadOrdenesProcesadas)
                        {
                            var ordenesActualizar = (from o in ordenesProcesar
                                where o.Id != ordenesProcesar[0].Id
                                select o.Id).ToList();

                            cantidadOrdenesProcesadas += ActualizarBitacoraEnvio(string.Join(",", ordenesActualizar),
                                "Procesada reasignación por duplicado para id:" + ordenesProcesar[0].Id, ent);
                        }
                    }
                }
            }

            return cantidadOrdenesProcesadas;
        }

        private int EnviarMarcarOrdenesWs(Dictionary<int, int> ordenes, bool esReasignacion, EntBitacoraEnvio ent)
        {
            if (ordenes.Count > 0)
            {
                try
                {
                   
                    var entOrdenes = new EntOrdenes();

                    var idOrdenes = string.Join(",", ordenes.Select(x => x.Value).ToList());
                    var modelOrdenes = entOrdenes.ObtenerOrdenesPorId(idOrdenes);
                    var firstOrDefault = modelOrdenes.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        var entFormulario = new EntFormulario().ObtenerListaFormularios(firstOrDefault.Ruta).FirstOrDefault(x=> x.Captura==1);
                        if (entFormulario != null)
                        {
                            var negocioOrden = new Orden(3, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario.Nombre, entFormulario.Version, Config.AplicacionActual().Nombre);
                            var resultado = negocioOrden.EnviarOrdenesWs(modelOrdenes, esReasignacion, 1);
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarOrdenesWs",
                                string.Format(
                                    "{3} {0} ordenes al WS con resultado {1}, No se pudo generar XML para {2} ordenes",
                                    ordenes.Count,
                                    resultado.Resultado, resultado.Lista.Count, esReasignacion ? "Reasignadas" : "Enviadas"));
                            var idBitacoraOrdenes = string.Join(",", ordenes.Select(x => x.Key).ToList());
                            if (!resultado.Resultado.ToLower().StartsWith("error"))
                            {
                                return ActualizarBitacoraEnvio(idBitacoraOrdenes, resultado.Resultado, ent);
                            }
                            if (Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                            {
                                var ordenesErr = resultado.Resultado.ToLower().Split(',');
                                foreach (var ord in ordenesErr)
                                {
                                    try
                                    {
                                        var item = ordenes.First(ordn => ordn.Value.ToString() == ord);
                                        ordenes.Remove(item.Key);
                                    }
                                    catch (Exception){                                        }
                                    
                                }
                                if (ordenes.Count>0)
                                {
                                    var idBitacoraOrdenestemp = string.Join(",", ordenes.Select(x => x.Key).ToList());
                                    return ActualizarBitacoraEnvio(idBitacoraOrdenestemp, resultado.Resultado, ent);    
                                }
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EnviarOrdenesWs",
                        "Error: EnviarMarcarOrdenesWS: " + ex.Message +
                        (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                }
            }
            return 0;
        }

        private int ActualizarBitacoraEnvio(string idBitacoraOrdenes, string resultado, EntBitacoraEnvio ent)
        {
            try
            {
                var res = ent.ActualizarResultado(idBitacoraOrdenes, resultado);
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarOrdenesWs",
                    string.Format("Se actualizaron {0} registros de BitacoraEnvio", res));
                return res;
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EnviarOrdenesWs",
                    "Error: ActualizarBitacoraEnvio: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                return 0;
            }
        }

        private int CancelarMarcarOrdenesWs(Dictionary<int, int> ordenes, EntBitacoraEnvio ent)
        {
            if (ordenes.Count > 0)
            {
                try
                {
                    var entOrdenes = new EntOrdenes();

                    var idOrdenes = string.Join(",", ordenes.Select(x => x.Value).ToList());
                    var modelOrdenes = entOrdenes.ObtenerOrdenesPorId(idOrdenes);
                    var firstOrDefault = modelOrdenes.FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        var entFormulario = new EntFormulario().ObtenerListaFormularios(firstOrDefault.Ruta);
                        var negocioOrden = new Orden(3, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario[0].Nombre, entFormulario[0].Version,Config.AplicacionActual().Nombre);
                        var resultado = negocioOrden.EnviarCancelacionWs(modelOrdenes, 1);
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EnviarOrdenesWs",
                            string.Format(
                                "Canceladas {0} ordenes al WS con resultado {1}",
                                ordenes.Count,
                                resultado));
                        var idBitacoraOrdenes = string.Join(",", ordenes.Select(x => x.Key).ToList());
                        if (!resultado.ToLower().StartsWith("error:"))
                        {
                            return ActualizarBitacoraEnvio(idBitacoraOrdenes, resultado, ent);
                        }

                        if (Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                        {
                            var ordenesErr = resultado.ToLower().Split(',');
                            foreach (var ord in ordenesErr)
                            {
                                try
                                {
                                    var item = ordenes.First(ordn => ordn.Value.ToString() == ord);
                                    ordenes.Remove(item.Key);
                                }
                                catch (Exception) { }

                            }
                            if (ordenes.Count > 0)
                            {
                                var idBitacoraOrdenestemp = string.Join(",", ordenes.Select(x => x.Key).ToList());
                                return ActualizarBitacoraEnvio(idBitacoraOrdenestemp, "ok", ent);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EnviarOrdenesWs",
                        "Error: EnviarMarcarOrdenesWS: " + ex.Message +
                        (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                }
            }
            return 0;
        }

        public override int Terminar()
        {
            return 0;
        }
    }
}
