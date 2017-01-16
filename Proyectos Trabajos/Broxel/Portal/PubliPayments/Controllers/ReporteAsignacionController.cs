using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class ReporteAsignacionController : Controller
    {
        public ActionResult Index()
        {
            var idRol = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            var idDominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);
            ViewData["Rol"] = idRol;

            ViewData["Delegacion"] = new EntRankingIndicadores().ObtenerDelegacionUsuario(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0];

            if ((idDominio == "1" || idDominio == "2") && idRol=="0" || idRol=="1" ||  idRol=="5" )
            {
                return View();
            }

            return Redirect("~/unauthorized.aspx");
        }

        public ActionResult ComboFiltro(string idCombo, int tipoCombo, string delegacion, string despacho, string supervisor)
        {
            var lista = new List<OpcionesFiltroDashboard>();
            var elemento = new OpcionesFiltroDashboard();

            switch (idCombo)
            {
                case "supervisorCombo":
                    elemento.Value = "9999";
                    elemento.Description = "Seleccionar un supervisor";
                    break;
                case "despachoCombo":
                    elemento.Value = "9999";
                    elemento.Description = "Seleccionar un despacho";
                    break;

                case "delegacionCombo":
                    elemento.Value = "9999";
                    elemento.Description = "Seleccionar una delegación";
                    break;
                default:
                    throw new HttpException(500, "");
            }

            lista.Add(elemento);

            var sql = "exec ObtenerCombosFiltro " +
                      "@idCombo = N'" + idCombo + "', " +
                      "@tipoCombo = " + tipoCombo + ",  " +
                      "@delegacion = N'" + delegacion + "', " +
                      "@despacho = " + despacho + ", " +
                      "@supervisor = " + supervisor;
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            var listaAux = new List<OpcionesFiltroDashboard>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var elementoAux = new OpcionesFiltroDashboard
                {
                    Value = row[0].ToString(),
                    Description = row[1].ToString()
                };
                listaAux.Add(elementoAux);
            }
            lista.AddRange(listaAux);

            return PartialView(lista);
        }

        public void GenerarReporteAsignacion(string delegacion, string despacho, string idsupervisor)
        {
            var guidTiempos = Tiempos.Iniciar();
            delegacion = delegacion == "9999" ? "%" : delegacion;
            despacho = despacho == "9999" ? "%" : despacho;
            idsupervisor = idsupervisor == "9999" ? "%" : idsupervisor;
            string idUsuarioPadre = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            int idRol = Int32.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));

            try
            {
                if (idRol > 1)
                {
                    delegacion = new EntRankingIndicadores().ObtenerDelegacionUsuario(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0];
                }

                var reportes = new Negocios.Reportes();

                DataSet estatusDataSet = reportes.ObtenerEstatusReporte(Int32.Parse(idUsuarioPadre), null, 2);
                int estatus = -1;
                if (estatusDataSet != null && estatusDataSet.Tables.Count > 0)
                {
                    if (estatusDataSet.Tables[0].Rows.Count > 0)
                    {
                        estatus = Int32.Parse(estatusDataSet.Tables[0].Rows[0]["Estatus"].ToString());
                    }
                }
                if (estatus == 2)
                    return;
                Logger.WriteLine(Logger.TipoTraceLog.Trace, Int32.Parse(idUsuarioPadre), "ReporteAsignacionController", "GenerarReporteAsignacion.ActualizarEstatusReporte");
                var result = reportes.ActualizarEstatusReporte(Int32.Parse(idUsuarioPadre), 2);
                if (result.StartsWith("Error"))
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ReporteAsignacionController.GenerarReporteAsignacion", string.Format("idusuarioPadre:{0}, delegacion:{1},despacho:{2},idsupervisor:{3} Error:{4} ", idUsuarioPadre, delegacion, despacho, idsupervisor, result));
                }

                Logger.WriteLine(Logger.TipoTraceLog.Trace, Int32.Parse(idUsuarioPadre), "ReporteAsignacionController", "GenerarReporteAsignacion.ObtenerReporteAsignacion");
                var reporteDataSet = reportes.ObtenerReporteAsignacion(idUsuarioPadre, delegacion, despacho, idsupervisor);
                Logger.WriteLine(Logger.TipoTraceLog.Trace, Int32.Parse(idUsuarioPadre), "ReporteAsignacionController", "GenerarReporteAsignacion.ArmarReporteCSV");
                var reporteCsv = reportes.ArmarReporteCSVAsignacion(reporteDataSet);

                Logger.WriteLine(Logger.TipoTraceLog.Trace, Int32.Parse(idUsuarioPadre), "ReporteAsignacionController", "GenerarReporteAsignacion.InsertaReporteTexto");
                reportes.InsertaReporteTexto(Int32.Parse(result), reporteCsv, int.Parse(idUsuarioPadre), 2);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, Int32.Parse(idUsuarioPadre), "ReporteAsignacionController",
                    "GenerarReporteAsignacion - " + ex.Message);

            }
                Tiempos.Terminar(guidTiempos);
        }

        public ActionResult Descargar(String idReporte)
        {
            var guidTiempos = Tiempos.Iniciar();
            ConexionSql conn = ConexionSql.Instance;
            try
            {
                var resultado = new StringBuilder();
                String temporal;
                long contador = 0;
                Response.AddHeader("content-disposition", "attachment;filename=ReporteAsignacion.csv");
                Response.ContentType = "text/plain";
                do
                {
                    temporal = conn.DescargarReporteParticionado(idReporte, contador);
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
            var reportes =  new Negocios.Reportes();
            var guidTiempos = Tiempos.Iniciar();
            int idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            int idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            if (idRol != 0 && idRol != 1 && idRol != 5)
            {
                Tiempos.Terminar(guidTiempos);
                return Json(new { idReporte = "0", tipo = "1", Estatus = "1", Fecha = "", volverG = false },
                    JsonRequestBehavior.AllowGet);
            }
            DataSet estatusDataSet =reportes.ObtenerEstatusReporte(idUsuarioPadre, null, 2);
            int idReporte = -1;
            int tipo = -1;
            int estatus = -1;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            bool volverG = false,bloqueado=false;
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

            var bloqueo = reportes.VerificaBloqueoAsignaciones();

            var fechaAct = DateTime.Now.AddHours(-1);
            DateTime fechaDateime;
            if (DateTime.TryParseExact(fecha, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out fechaDateime))
            {
                int result = DateTime.Compare(fechaAct, fechaDateime);
                if (result > 0 && bloqueo == "0")
                {
                    volverG = true;
                }
            }

            
            if (bloqueo != "0")
                {
                    bloqueado = true;
                }
            
            Tiempos.Terminar(guidTiempos);
            return Json(new { idReporte, tipo, Estatus = estatus, Fecha = fecha, volverG, bloqueado }, JsonRequestBehavior.AllowGet);
        }
    }
}
