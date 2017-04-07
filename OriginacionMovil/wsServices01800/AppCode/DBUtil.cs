using System;               
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using IInfonavit.Infonavit;
using IInfonavit.PrecalificacionMejoravit2;
using PubliPayments.Utiles;

namespace wsServices01800.AppCode
{
    public class DBUtil
    {
        public long InsertRequestFormiik(string request, string externalId, string idWorkOrder, string idWorkFormType, string userName, string workOrderType, string action)
        {
            long ret = 0;
            try
            {
                using (var ctx = new SistemasOriginacionMovilEntities())
                {

                    var idInsert = new ObjectParameter("idInsert", typeof (long));
                    ctx.spInsFormikConsultas(request, externalId, idWorkOrder, idWorkFormType, userName,workOrderType,action, idInsert);
                    ret = (long)idInsert.Value;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "DBUtil: " + ex.Message + " ,Inner:" + ex.InnerException.Message);
                ret = 0;
            }
            return ret;
        }

        public void UpdateRequestFormiik(long id, string response)
        {
            try
            {
                using (var ctx = new SistemasOriginacionMovilEntities())
                {
                    var formiikConsulta = ctx.FormiikConsultas.Single(f => f.id == id);
                    if (formiikConsulta == null) return;
                    formiikConsulta.jResponse = response;
                    formiikConsulta.fechaResponse = DateTime.Now;
                    ctx.Entry(formiikConsulta).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "DBUtil: " + ex.Message + " ,Inner:" + ex.InnerException.Message);
                
            }
        }

        public long InsPrecalificaRequest(ProgramInterfaceWs_entrada entrada, long idO, string usuario, string nombre_oficina, string oficina)
        {
            
            long ret = 0;
            try
            {
                using (var ctx = new SistemasOriginacionMovilEntities())
                {
                    var id = new ObjectParameter("idInsert", typeof (long));
                    ctx.spInsPrecalifReq(idO, entrada.ws_nss, entrada.ws_pen_alim,usuario,nombre_oficina,oficina, id);
                    ret = (long)id.Value;
                }
            }
            catch(Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "DBUtil: "+ex.Message+" ,Inner:"+ex.InnerException.Message);
                ret = 0;
            }
            return ret;
        }

        public void InsPrecalificaResponse(WsRespuestaPreca salida, long idPre)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " +1);
            try
            {
                using (var ctx = new SistemasOriginacionMovilEntities())
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 2);
                    PrecalificacionResponse response = null;
                    
                    response = ctx.PrecalificacionResponse.SingleOrDefault(p => p.idPrecalificacion == idPre);

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 3);
                    if (response != null)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 4);
                        response.wsIdMensaje = salida.WSidMensaje;
                        response.wsCurp = String.IsNullOrEmpty(salida.WSCurp)? "" : salida.WSCurp.Trim();
                        response.wsGastosApertura = String.IsNullOrEmpty(salida.WSGastosApertura) ? 0 : Convert.ToDecimal(salida.WSGastosApertura.Trim(), CultureInfo.CreateSpecificCulture("es-MX"));
                        response.wsMensaje = String.IsNullOrEmpty(salida.WSMensaje) ? "" : salida.WSMensaje.Trim().Length > 200 ? salida.WSMensaje.Trim().Substring(0, 200) : salida.WSMensaje.Trim();
                        
                        response.wsNombreTitular = String.IsNullOrEmpty(salida.WSNombreTitular)? "" : salida.WSNombreTitular.Trim();
                        response.WSNombreEmpresa = String.IsNullOrEmpty(salida.WSNombreEmpresa)? "" : salida.WSNombreEmpresa.Trim();
                        response.wsNumRegistroPatronal = String.IsNullOrEmpty(salida.WSNumRegistroPatronal)? "" : salida.WSNumRegistroPatronal.Trim();
                        response.wsNumPlazos = String.IsNullOrEmpty(salida.WSNumPlazos) ? 0 : Convert.ToInt32(salida.WSNumPlazos, CultureInfo.CreateSpecificCulture("es-MX"));
                        response.wsPuntMin = String.IsNullOrEmpty(salida.WSPuntMin) ? 0 : Convert.ToDecimal(salida.WSPuntMin, CultureInfo.CreateSpecificCulture("es-MX"));
                        response.wsRFC = String.IsNullOrEmpty(salida.WSRFC)? "" : salida.WSRFC.Trim();
                        response.wsPuntTotal = String.IsNullOrEmpty(salida.WSPuntTotal) ? 0 : Convert.ToDecimal(salida.WSPuntTotal, CultureInfo.CreateSpecificCulture("es-MX"));
                        response.wsTasaInteres = String.IsNullOrEmpty(salida.WSTasaInteres) ? 0 : Convert.ToDecimal(salida.WSTasaInteres, CultureInfo.CreateSpecificCulture("es-MX"));
                        ctx.Entry(response).State = EntityState.Modified;
                        try
                        {
                            ctx.SaveChanges();
                            ctx.PrecalificacionOcur.RemoveRange(ctx.PrecalificacionOcur.Where(p => p.idPrecalificacion == idPre));
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            var exw = 1;
                        }

                    }
                    else
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 5);
                        
                        response = new PrecalificacionResponse
                        {
                            wsIdMensaje = String.IsNullOrEmpty(salida.WSidMensaje)? "" : salida.WSidMensaje.Trim(),
                            wsCurp = String.IsNullOrEmpty(salida.WSCurp)?"":salida.WSCurp.Trim(),
                            wsGastosApertura = String.IsNullOrEmpty(salida.WSGastosApertura) ? 0 : Convert.ToDecimal(salida.WSGastosApertura.Trim(), CultureInfo.CreateSpecificCulture("es-MX")),
                            idPrecalificacion = idPre,
                            wsMensaje = String.IsNullOrEmpty(salida.WSMensaje)? "" : salida.WSMensaje.Trim(),
                            wsNombreTitular = String.IsNullOrEmpty(salida.WSNombreTitular)? "" : salida.WSNombreTitular.Trim(),
                            WSNombreEmpresa = String.IsNullOrEmpty(salida.WSNombreEmpresa)? "" : salida.WSNombreEmpresa.Trim(),
                            wsNumRegistroPatronal = String.IsNullOrEmpty(salida.WSNumRegistroPatronal)? "" : salida.WSNumRegistroPatronal.Trim(),
                            wsNumPlazos = String.IsNullOrEmpty(salida.WSNumPlazos) ? 0 : Convert.ToInt32(salida.WSNumPlazos.Trim(), CultureInfo.CreateSpecificCulture("es-MX")),
                            wsPuntMin = String.IsNullOrEmpty(salida.WSPuntMin) ? 0 : Convert.ToDecimal(salida.WSPuntMin.Trim(), CultureInfo.CreateSpecificCulture("es-MX")),
                            wsRFC = String.IsNullOrEmpty(salida.WSRFC)? "" : salida.WSRFC.Trim(),
                            wsPuntTotal = String.IsNullOrEmpty(salida.WSPuntTotal) ? 0 : Convert.ToDecimal(salida.WSPuntTotal.Trim(), CultureInfo.CreateSpecificCulture("es-MX")),
                            wsTasaInteres = String.IsNullOrEmpty(salida.WSTasaInteres) ? 0 : Convert.ToDecimal(salida.WSTasaInteres.Trim(), CultureInfo.CreateSpecificCulture("es-MX"))
                        };
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 51);
                        ctx.Entry(response).State = EntityState.Added;
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 52);
                        ctx.SaveChanges();
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 53);
                    }

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 6);
                    var plazos = String.IsNullOrEmpty(salida.WSNumPlazos.Trim()) ? 0 : Convert.ToInt32(salida.WSNumPlazos, CultureInfo.CreateSpecificCulture("es-MX"));
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 7);
                    if (plazos <= 0) return;
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 8);
                    for (var i = 0; i < plazos; i++)
                    {
                        var contarias = String.IsNullOrEmpty(salida.WS_Ocur[i].Contarias.Trim())
                            ? 0
                            : Convert.ToDecimal(salida.WS_Ocur[i].Contarias,
                                CultureInfo.CreateSpecificCulture("es-MX"));
                        var mtoCredo = String.IsNullOrEmpty(salida.WS_Ocur[i].MontoCredito.Trim())
                            ? 0
                            : Convert.ToDecimal(salida.WS_Ocur[i].MontoCredito,
                                CultureInfo.CreateSpecificCulture("es-MX"));
                        var pago = String.IsNullOrEmpty(salida.WS_Ocur[i].PagoMensual.Trim())
                            ? 0
                            : Convert.ToDecimal(salida.WS_Ocur[i].PagoMensual,
                                CultureInfo.CreateSpecificCulture("es-MX"));
                        var plazo = String.IsNullOrEmpty(salida.WS_Ocur[i].Plazo.Trim())
                            ? 0
                            : Convert.ToInt32(salida.WS_Ocur[i].Plazo,
                                CultureInfo.CreateSpecificCulture("es-MX"));
                        var mano = String.IsNullOrEmpty(salida.WS_Ocur[i].MontoManoObra.Trim())
                            ? 0
                            : Convert.ToDecimal(salida.WS_Ocur[i].MontoManoObra,
                                CultureInfo.CreateSpecificCulture("es-MX"));
                        var pOcur = new PrecalificacionOcur
                        {
                            contarias = contarias,
                            idPrecalificacion = idPre,
                            monto_credito = mtoCredo,
                            pago_mensual = pago,
                            plazo = plazo,
                            mano_obra = mano
                        };
                        ctx.Entry(pOcur).State = EntityState.Added;
                        ctx.SaveChanges();
                    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                }
                Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "DBUtil: " + 9);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "DBUtil:  ,Inner:" + ex.InnerException.Message);
            }
        }

    }
    
}