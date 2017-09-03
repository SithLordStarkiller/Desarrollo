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
    public class EstiquetasUIController : ControllerCrud<Estiquetas, EstiquetasDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public EstiquetasUIController()
        {
            ControllerName = "Estiquetas";
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
        public override ActionResult Index([Bind(Include = "IdEtiqueta, IdTipoDocumento, Etiqueta, Contenido, Negrita, Orden, FechaInicial, FechaFinal, Activo")]Estiquetas item, EstiquetasDtoBuscar find)
        {
            TryUpdateModel(item, new[] { "IdEtiqueta", "IdTipoDocumento", "Etiqueta", "Contenido", "Negrita", "Orden", "FechaInicial", "FechaFinal", "Activo"});//regrese un bool
            if (ModelState.IsValid)
            {
                if (!item.FechaInicial.HasValue)
                    item.FechaInicial = DateTime.Today;
                if (!item.FechaFinal.HasValue)
                    item.FechaFinal = DateTime.Today;
                if (!item.Orden.HasValue)
                    item.Orden = 0;
                return base.Index(item, find);
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";
            return FindPartial(find);
        }

        public override PartialViewResult AddModifyItem(EstiquetasDtoBuscar find)
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
