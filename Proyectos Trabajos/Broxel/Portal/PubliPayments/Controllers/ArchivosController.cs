using System.Data;
using PubliPayments.Entidades;
using Publipayments.Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class ArchivosController : Controller
    {
        public ActionResult Index()
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) == "0" || "234".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                if (!Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                    Response.Redirect("unauthorized.aspx");
            }            return View();
        }

        public ActionResult NextPage(string tipo)
        {
            var mod = GridBind().Where(f => f.Tipo.Equals(tipo)).ToList();
            return PartialView("_TablaArchivos", mod);
        }

        private List<ArchivosModel> GridBind()
        {
            int idUsuario = Int32.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var dsArchivos = new DataSet();
            string tipo = string.Empty;
            var archivosNegocio = new Negocios.ArchivosN();
            var archivos = new List<ArchivosModel>();

            dsArchivos = archivosNegocio.ObtenerArchivos(idUsuario, 1);

            archivos = dsArchivos.Tables[0].ToList<ArchivosModel>();

            return archivos;
        }
    }
}
