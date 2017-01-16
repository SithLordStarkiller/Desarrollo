using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using PubliPayments.Entidades;

namespace PubliPayments.Controllers
{
    public class ReversosController : Controller
    {
        //
        // GET: /Gestionadas/

        public ActionResult Index()
        {
            if (User.IsInRole("0") || User.IsInRole("3"))
            {
                ViewBag.idAplicacion = Config.AplicacionActual().idAplicacion;

                if (Session["Respuesta"] != null)
                {
                    string[] respuestas = Session["Respuesta"].ToString().Split('|');
                    Session.Remove("Respuesta");
                    ViewBag.MensajeResultado = respuestas[0];
                }
                Session["Refresh"] = true;
                ViewBag.idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)); 
                //ViewBag.Tipo = tipo.ToString();

                return View();
            }
            return Redirect("/unauthorized.aspx");
        }


        [ValidateInput(false)]
        public ActionResult GridReversosPartial()
        {
            //Para manejar los postbacks
            if (Session["Refresh"] != null)
            {
                Session.Remove("Refresh");
            }
            var idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));

            var sql = "exec ObtenerTablaReversos " +
                      "@TipoFormulario = N'" +CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta + "', " +
                      "@idUsuario = N'" +  idUsuarioPadre+"', " +
                      "@idRol = N'" + idRol + "' ";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            var model=ds.Tables[0];

            return PartialView("GridReversosPartial", model);
        }
    }

    
    }

