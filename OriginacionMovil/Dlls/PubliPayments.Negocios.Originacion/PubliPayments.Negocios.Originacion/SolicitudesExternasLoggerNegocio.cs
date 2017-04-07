using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class SolicitudesExternasLoggerNegocio
    {
        public static void AddLog(DateTime fecha, string descripcion, string Json, string url)
        {
            try
            {
                EntSolicitudesExternasLogger.AddLog(fecha, descripcion, Json, url);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, -1, "SolicitudesExternasLogger",ex.Message + "||"+ex.InnerException );                
            }            
        }
    }
}
