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
    public class PartesDocumentoUIController : ControllerCrud<PartesDocumento, PartesDocumentoDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public PartesDocumentoUIController()
        {
            ControllerName = "PartesDocumento";
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
        public override ActionResult Index([Bind(Include = "IdParteDocumento, IdTipoDocumento, RutaLogo, Folio, Asunto, LugarFecha, Paginado, Ccp, Puesto, Nombre, Direccion, FechaInicial, FechaFinal, Activo")]PartesDocumento item, PartesDocumentoDtoBuscar find)
        {
            TryUpdateModel(item, new[] { "IdParteDocumento", "IdTipoDocumento", "RutaLogo", "Folio", "Asunto", "LugarFecha", "Paginado", "Ccp", "Puesto", "Nombre", "Direccion", "FechaInicial", "FechaFinal", "Activo"});//regrese un bool
            if (ModelState.IsValid)
            {
                if (!item.FechaInicial.HasValue)
                    item.FechaInicial = DateTime.Today;
                if (!item.FechaFinal.HasValue)
                    item.FechaFinal = DateTime.Today;
                return base.Index(item, find);
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";
            return FindPartial(find);
        }

        public override PartialViewResult AddModifyItem(PartesDocumentoDtoBuscar find)
        {
            CargarDropTiposDocumento();
            return base.AddModifyItem(find);
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
