using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using PubliPayments.Entidades;

namespace PubliPayments.Controllers
{
    public class TelSMSRepetidosController : Controller
    {
        //
        // GET: /TelSMSRepetidos/

        public ActionResult Index()
        {
            ViewBag.delegacion = "";
            ViewBag.despacho = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);
            ViewBag.supervisor = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            ViewData["Rol"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            ViewData["Delegacion"] = "";
            ViewData["Despacho"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);
            ViewData["Supervisor"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            return View();
        }

        public ActionResult TablaTelefonos(string delegacion, string despacho, string supervisor)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            if (string.IsNullOrEmpty(tipoFormulario))
            {
                //Response.Redirect("/Login.aspx");
                Response.Redirect("~/Home");
            }

            var sql = "exec ObtenerReporteTelefonosDuplicados " +
                      "@idDominio = "+despacho+","+
                      "@idUsuarioPadre = " + supervisor + "," +
                      "@delegacion = " + delegacion + "," +
                      "@TipoFormulario = N'''" + tipoFormulario + "'''";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            ViewData["delegacion"]=delegacion;
            ViewData["supervisor"] = supervisor;
            ViewData["despacho"] = despacho;

            return PartialView(ds.Tables[0]);
        }

    }
}
