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
    public class ParrafosUIController : ControllerCrud<Parrafos, ParrafosDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public ParrafosUIController()
        {
            ControllerName = "Parrafos";
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
        public override ActionResult Index([Bind(Include = "IdParrafo, IdTipoDocumento, Nombre, Texto, FechaInicial, FechaFinal, Activo")]Parrafos item, ParrafosDtoBuscar find)
        {
            TryUpdateModel(item, new[] { "IdParrafo", "IdTipoDocumento", "Nombre", "Texto", "FechaInicial", "FechaFinal", "Activo"});//regrese un bool
            if (ModelState.IsValid)
            {                
                return SaveItem(item, find);;
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";
            return FindPartial(find);
        }

        public ActionResult SaveItem(Parrafos item, ParrafosDtoBuscar find)
        {

            if (!item.FechaInicial.HasValue)
                item.FechaInicial = DateTime.Today;
            if (!item.FechaFinal.HasValue)
                item.FechaFinal = DateTime.Today;

            return base.Index(item, find);
        }

        public override PartialViewResult AddModifyItem(ParrafosDtoBuscar find)
        {
            CargarDropTiposDocumento();
            return base.AddModifyItem(find);
        }

        public virtual PartialViewResult ModifyItem(ParrafosDtoBuscar find)
        {
            Parrafos item = AddModifyGetItem(find);

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
