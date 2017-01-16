using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class Auditoria
    {
        /// <summary>
        /// Obtiene los combos para la auditoria de imagenes
        /// </summary>
        /// <param name="conexionBd">Conexion de Bd a la cual se conectara</param>
        /// <param name="idCombo"></param>
        /// <param name="tipoCombo"></param>
        /// <param name="delegacion"></param>
        /// <param name="despacho"></param>
        /// <param name="supervisor"></param>
        /// <returns></returns>
        /// 
        public DataSet ObtenerCombosAutorizaImagenes(string conexionBd, string idCombo, int tipoCombo,string delegacion, int despacho, string supervisor)
        {
            return new EntAuditoria().ObtenerCombosAutorizaImagenes(conexionBd, idCombo, tipoCombo, delegacion,despacho, supervisor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conexionBd"></param>
        /// <param name="idAuditoriaGestion"></param>
        /// <param name="idUsuario"></param>
        /// <param name="credito"></param>
        /// <param name="autorizado"></param>
        /// <returns></returns>
        public DataSet InsertaAuditoriaImagenes(string conexionBd, int idAuditoriaGestion, int idUsuario, String credito, String autorizado)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, "InsertaAuditoriaImagenes", string.Format("Auditando orden:{0} , crédito:{1} ,resultado: {2}", idAuditoriaGestion,credito,autorizado));
            return new EntAuditoria().InsertaAuditoriaImagenes(conexionBd, idAuditoriaGestion, idUsuario, credito, autorizado);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAuditoriaImagenes"></param>
        /// <param name="imagen"></param>
        /// <param name="resultado"></param>
        /// <param name="conexionBd"></param>
        public void InsertaAuditoriaCamposImagen(String idAuditoriaImagenes, String imagen, String resultado, string conexionBd)
        {
            new EntAuditoria().InsertaAuditoriaCamposImagen(idAuditoriaImagenes, imagen, resultado, conexionBd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuarioAuditoria">id usuario que esta solicitando la información</param>
        /// <param name="delegacion"></param>
        /// <param name="despacho"></param>
        /// <param name="supervisor"></param>
        /// <param name="gestor"></param>
        /// <param name="dictamen"></param>
        /// <param name="status"></param>
        /// <param name="autoriza"></param>
        /// <param name="tipoFormulario"></param>
        /// <param name="valorOcr"></param>
        /// <param name="conexionBd"></param>
        /// <returns></returns>
        public DataSet ObtenerTablaAuditoriaImagenes(string idUsuarioAuditoria, string delegacion, string despacho, string supervisor, string gestor, string dictamen, string status, string autoriza, string tipoFormulario, string valorOcr, string conexionBd)
        {
            return new EntAuditoria().ObtenerTablaAuditoriaImagenes(idUsuarioAuditoria,delegacion, despacho, supervisor, gestor, dictamen, status, autoriza, tipoFormulario,valorOcr, conexionBd);
        }


        /// <summary>
        /// Obtiene los datos que se tienen registrados de una auditoria de imágenes
        /// </summary>
        /// <param name="idAuditoriaImagenes">id de la auditoria</param>
        /// <returns></returns>
        public DataSet ObtenerDatosAuditoria(int idAuditoriaImagenes)
        {
            return new EntAuditoria().ObtenerDatosAuditoria(idAuditoriaImagenes);
        }


        /// <summary>
        /// Rechaza una gestión que no ha sido autorizada por medio de la auditoria de imágenes
        /// </summary>
        /// <param name="credito">Crédito relacionado</param>
        /// <param name="idAuditoriaImagenes">Id del registro de auditoria</param>
        /// <param name="idUsuarioAuditoria">usuario para registrar en bitácora</param>
        /// <param name="produccion">aplica si es ambiente productivo</param>
        /// <param name="aplicacion">nombre de la aplicación a ejecutar</param>
        public void CancelacionAutomaticaXRechazo(string credito,int idAuditoriaImagenes, int idUsuarioAuditoria,bool produccion,string aplicacion)
        {
            var ordenNegocio = new Orden(3, produccion, null, null, null, null, aplicacion);
         
            try
            {
                var ordenModel = ordenNegocio.ObtenerOrdenxCredito(credito);
                var datosAuditoria = ObtenerDatosAuditoria(idAuditoriaImagenes);

                string resultadoAuditoria = null;

                if (datosAuditoria.Tables.Count > 0 && datosAuditoria.Tables[0].Rows.Count > 0)
                {
                    resultadoAuditoria = Convert.ToString(datosAuditoria.Tables[0].Rows[0]["resultado"]);
                }
                if (resultadoAuditoria != null)
                {
                    if (resultadoAuditoria == "0")
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAuditoria, "CancelacionAutomaticaXRechazo", string.Format("Rechazo del crédito:{0}", credito));
                        var reversos = ordenNegocio.CambiarEstatusOrdenes(Convert.ToString(ordenModel.IdOrden), 3, true, true, idUsuarioAuditoria);
                        if (reversos > 0)
                        {

                            if (produccion && aplicacion.ToUpper() == "LONDON") //Solo London
                            {
                                var objectList = new List<Object>(ordenModel.IdOrden);
                                Gestiones.EnviarGestionBatch(objectList, "R", "3");
                            }

                            int totalCanceladas = ordenNegocio.Cancelar(new List<int> { ordenModel.IdOrden }, idUsuarioAuditoria);
                            new EntOrdenes().ActualizarOrden(ordenModel.IdOrden, null, null, null, null, null, null,null, null, null, " ");
                            if (totalCanceladas < 1)
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAuditoria, "CancelacionAutomaticaXRechazo", "cancelar  orden  " + ordenModel.IdOrden + " no se realizó");
                            }
                        }
                        else
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAuditoria, "CancelacionAutomaticaXRechazo", "Reverso de orden  " + ordenModel.IdOrden + " no se realizó");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAuditoria, "CancelacionAutomaticaXRechazo", ex.Message);
            }
            
        }
    }
}
