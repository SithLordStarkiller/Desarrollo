using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Areas.Plantilla
{
    public class InstitucionesUIController : ControllerCrud<Instituciones, InstitucionesDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public InstitucionesUIController()
        {
            ControllerName = "Instituciones";
        }

        #endregion

        #region Actions

        public override ActionResult Index()
        {
            CargarDropTiposDocumento();
            return base.Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Index([Bind(Include = "IdInstitucion, IdTipoDocumento, Nombre, Negrita, Orden, FechaInicial, FechaFinal, Activo")]Instituciones item, InstitucionesDtoBuscar find)
        {
            TryUpdateModel(item, new[] { "IdInstitucion", "IdTipoDocumento", "Nombre", "Negrita", "Orden", "FechaInicial", "FechaFinal", "Activo"});//regrese un bool
            if (ModelState.IsValid)
            {
                return SaveItem(item, find);
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";
            return FindPartial(find);
        }

        public ActionResult SaveItem(Instituciones item, InstitucionesDtoBuscar find)
        {
            
                if (!item.FechaInicial.HasValue)
                    item.FechaInicial = DateTime.Today;
                if (!item.FechaFinal.HasValue)
                    item.FechaFinal = DateTime.Today;
                if (!item.Orden.HasValue)
                    item.Orden = 0;
                return base.Index(item, find);         
        }

        public override PartialViewResult AddModifyItem(InstitucionesDtoBuscar find)
        {
            CargarDropTiposDocumento();
            return base.AddModifyItem(find);
        }

        public virtual PartialViewResult ModifyItem(InstitucionesDtoBuscar find)
        {
            Instituciones item = AddModifyGetItem(find);

            List<DropDto> lstTiposDocumento = base.Drop("TiposDocumento");
            ViewBag.lstTiposDocumento = lstTiposDocumento;

            DropDto tiposDocumento = lstTiposDocumento.Where(t => t.Id == find.IdTipoDocumentoBuscar).FirstOrDefault();

            item.IdTipoDocumento = tiposDocumento.Id;

            item.TiposDocumento = new TiposDocumento();
            item.TiposDocumento.IdTipoDocumento = tiposDocumento.Id;
            item.TiposDocumento.Nombre = tiposDocumento.Valor;

            return PartialView("_Edicion", item);
        }

        #endregion

        #region Drop

        protected void CargarDropTiposDocumento()
        {
            List<DropDto> lstTiposDocumento = base.Drop("TiposDocumento");
            ViewBag.lstTiposDocumento = lstTiposDocumento;
        }
        #endregion

    }
}
