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
    public class EtiquetasParrafoUIController : ControllerCrud<EtiquetasParrafo, EtiquetasParrafoDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public EtiquetasParrafoUIController()
        {
            ControllerName = "EtiquetasParrafo";
        }

        #endregion

        #region Actions

        public override ActionResult Index()
        {
            CargarDropParrafos();
            return base.Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Index([Bind(Include = "IdEtiquetaParrafo, IdParrafo, Etiqueta, Contenido, Negrita, Orden, FechaInicial, FechaFinal, Activo")]EtiquetasParrafo item, EtiquetasParrafoDtoBuscar find)
        {
            TryUpdateModel(item, new[] { "IdEtiquetaParrafo", "IdParrafo", "Etiqueta", "Contenido", "Negrita", "Orden", "FechaInicial", "FechaFinal", "Activo"});//regrese un bool
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

        public override PartialViewResult AddModifyItem(EtiquetasParrafoDtoBuscar find)
        {
            CargarDropParrafos();
            return base.AddModifyItem(find);
        }

        public virtual PartialViewResult ModifyItem(EtiquetasParrafoDtoBuscar find)
        {
            EtiquetasParrafo item = AddModifyGetItem(find);

            List<DropDto> lstParrafos = base.Drop("Parrafos");
            ViewBag.lstParrafos = lstParrafos;

            DropDto parrafos = lstParrafos.Where(t => t.Id == find.IdParrafoBuscar).FirstOrDefault();

            item.IdParrafo = parrafos.Id;

            item.Parrafos = new Parrafos();
            item.Parrafos.IdParrafo = parrafos.Id;
            item.Parrafos.Nombre = parrafos.Valor;

            return PartialView("_Edicion", item);
        }

        #endregion

        #region Drop

        protected void CargarDropParrafos()
        {
            List<DropDto> lstParrafos = base.Drop("Parrafos");
            ViewBag.lstParrafos = lstParrafos;
        }
        #endregion

    }
}
