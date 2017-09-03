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
    public class TiposDocumentoUIController : ControllerCrud<TiposDocumento, TiposDocumentoDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public TiposDocumentoUIController()
        {
            ControllerName = "TiposDocumento";
        }

        #endregion

        #region Actions

        public override ActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Index([Bind(Include = "IdTipoDocumento, Nombre, Descripcion, FechaInicial, FechaFinal, Activo")]TiposDocumento item, TiposDocumentoDtoBuscar find)
        {            
            TryUpdateModel(item, new[] { "IdTipoDocumento", "Nombre", "Descripcion", "FechaInicial", "FechaFinal", "Activo"});//regrese un bool
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

        public override PartialViewResult AddModifyItem(TiposDocumentoDtoBuscar find)
        {
            return base.AddModifyItem(find);
        }

        #endregion

        #region Drop

        #endregion

    }
}
