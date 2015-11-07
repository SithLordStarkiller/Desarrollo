namespace ExamenEdenred.SitioWeb.Controllers
{
    using Entities.Entities;
    using System.Web.Mvc;
    using System.Net;

    public class CatalogosController : AsyncController
    {
        // GET: Catalogos
        public ActionResult EditarTipoUsuario(int? idTipoPersona)
        {
            if (idTipoPersona == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var registro = new Entities.Entities.PerPersonas();//db.Students.Find(id);
            
            UsUsuarios registro = null;
            
            if (registro == null)
            {
                return HttpNotFound();
            }

            return View(registro);
        }
    }
}