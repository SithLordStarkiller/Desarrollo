using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.Web.Mvc.Internal;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class ReporteDescargaController : Controller
    {

        public ActionResult DescargaReporteInfoComercio(String idUsuarioPadre, String tipoFormulario)
        {
          
        return GenerarReporte(idUsuarioPadre, tipoFormulario, 1);

        }
        public ActionResult DescargaReporteResVisita(String idUsuarioPadre, String tipoFormulario)
        {   
            return GenerarReporte(idUsuarioPadre, tipoFormulario, 2);  
        }


        private ActionResult GenerarReporte(String idUsuarioPadre, String tipoFormulario, int tipo)
        {
            var guidTiempos = Tiempos.Iniciar();
            string usuario = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            if (usuario != idUsuarioPadre)
                return File(Encoding.Default.GetBytes("Error"),
                        "text/plain",
                         (tipo == 1 ? "Reporte total general" : "Reporte incremental") + ".csv");
            var sb = new StringBuilder();
            try
            {
                ConexionSql conn = ConexionSql.Instance;
                DataSet dSet = (tipo == 1) ? conn.ObtenerRespuestasBitacora(0, "0", 0, Int32.Parse(idUsuarioPadre), tipoFormulario) : conn.ResultadoVisita(0, "0", 0, Int32.Parse(idUsuarioPadre), tipoFormulario);
                sb = new Negocios.Reportes().ArmarReporteCSV(dSet);

            }
            
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(idUsuarioPadre), "DescargaReporteSira", "Error: " + ex.Message);
            }
            Tiempos.Terminar(guidTiempos);
            return File(Encoding.Default.GetBytes(sb.ToString()),
                       "text/plain",
                       (tipo == 1 ? "Reporte total general" : "Reporte incremental") + ".csv");
        }
    }
}
