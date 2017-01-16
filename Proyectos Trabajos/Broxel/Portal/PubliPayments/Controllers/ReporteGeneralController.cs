using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class ReporteGeneralController : Controller
    {
        public ActionResult Index()
        {
            var guidTiempos = Tiempos.Iniciar();
            int idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            ViewData["AplicacionLogos"] = "/imagenes/Logos" + Config.AplicacionActual().Nombre + "/";
            ViewData["Aplicacion"] = Config.AplicacionActual().idAplicacion;

            var fechas = new EntReporteGeneral().ObtenerFechasReporteGeneral(1, idUsuarioPadre);
            Tiempos.Terminar(guidTiempos);
            return View(fechas);
        }

        public void Generar()
        {
            string idUsuarioPadre = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            ConexionSql conn = ConexionSql.Instance;
            DataSet estatusDataSet = conn.ObtenerEstatusReporte(Int32.Parse(idUsuarioPadre), null, 1);
            int estatus = -1;
            if (estatusDataSet != null && estatusDataSet.Tables.Count > 0)
            {
                if (estatusDataSet.Tables[0].Rows.Count > 0)
                {
                    estatus = Convert.ToInt32(estatusDataSet.Tables[0].Rows[0]["Estatus"]);
                }
            }
            if (estatus==2)
            {
                return;
            }
            switch (idRol)
            {
                case 3:
                    new Negocios.ReporteGeneral().GeneraReporteGeneral(idUsuarioPadre, idRol);
                    break;

                case 5:
                    new Negocios.ReporteGeneral().GeneraReporteSupervisoresDelegacion(Convert.ToInt32(idUsuarioPadre));
                    break;
            }
            
        }

        public ActionResult Descargar(String idReporte, string fecha)
        {
            var guidTiempos = Tiempos.Iniciar();
            ConexionSql conn = ConexionSql.Instance;
            try
            {
                int idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
                if (idRol != 3 && idRol != 5)
                {
                    Tiempos.Terminar(guidTiempos);
                    return File(Encoding.UTF8.GetBytes("null"), "test/plain", "ReporteGeneral.csv");
                }
                var resultado = new StringBuilder();
                String temporal;
                long contador = 0;
                if (string.IsNullOrEmpty(fecha))
                    Response.AddHeader("content-disposition", "attachment;filename=ReporteGeneral.csv");
                else
                {
                    fecha = fecha.Trim();
                    int n;
                    bool isNumeric = int.TryParse(fecha, out n);
                    if (isNumeric && !n.Equals(999999))
                        Response.AddHeader("content-disposition", "attachment;filename=ReporteGeneral" + fecha + ".csv");
                    else
                    {

                        Response.Flush();
                        Tiempos.Terminar(guidTiempos);
                        return null;
                    }
                }
                Response.ContentType = "text/plain";
                do
                {
                    temporal = !string.IsNullOrEmpty(fecha)
                        ? conn.DescargarReporteParticionado(idUsuarioPadre.ToString(CultureInfo.InvariantCulture),
                            contador, fecha)
                        : conn.DescargarReporteParticionado(idReporte, contador);
                    resultado.Append(temporal);
                    contador += 32768;
                    var buffer = Encoding.ASCII.GetBytes(temporal);
                    if (Response.IsClientConnected)
                    {
                        Response.OutputStream.Write(buffer, 0, buffer.Length);
                        Response.Flush();
                    }
                    else
                    {
                        Tiempos.Terminar(guidTiempos);
                        return null;
                    }

                } while (temporal.Length >= 32767);
                Response.Flush();
                Tiempos.Terminar(guidTiempos);
                return null;
            }
            catch (Exception)
            {

                Response.Flush();
                Tiempos.Terminar(guidTiempos);
                return null;
            }
        }

        public ActionResult VerificaEstatus()
        {
            var guidTiempos = Tiempos.Iniciar();
            int idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            if (idRol != 3 && idRol != 5)
            {
                Tiempos.Terminar(guidTiempos);
                return Json(new {idReporte = "0", tipo = "1", Estatus = "1", Fecha = "", volverG = false},
                    JsonRequestBehavior.AllowGet);
            }
            ConexionSql conn = ConexionSql.Instance;

            DataSet estatusDataSet = conn.ObtenerEstatusReporte(idUsuarioPadre, null, 1);
            int idReporte = -1;
            int tipo = -1;
            int estatus = -1;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            bool volverG = false;
            if (estatusDataSet != null && estatusDataSet.Tables.Count > 0)
            {
                if (estatusDataSet.Tables[0].Rows.Count > 0)
                {
                    idReporte = Convert.ToInt32(estatusDataSet.Tables[0].Rows[0]["idReporte"]);
                    tipo = Convert.ToInt32(estatusDataSet.Tables[0].Rows[0]["tipo"]);
                    estatus = Convert.ToInt32(estatusDataSet.Tables[0].Rows[0]["Estatus"]);
                    fecha = Convert.ToDateTime(estatusDataSet.Tables[0].Rows[0]["Fecha"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
            }
            var fechaAct = DateTime.Now.AddHours(-2);
            DateTime fechaDateime;
            if (DateTime.TryParseExact(fecha, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out fechaDateime))
            {
                int result = DateTime.Compare(fechaAct, fechaDateime);
                if (result > 0)
                {
                    volverG = true;
                }
            }
            Tiempos.Terminar(guidTiempos);
            return Json(new { idReporte, tipo, Estatus = estatus, Fecha = fecha, volverG }, JsonRequestBehavior.AllowGet);
        }

    }
}
