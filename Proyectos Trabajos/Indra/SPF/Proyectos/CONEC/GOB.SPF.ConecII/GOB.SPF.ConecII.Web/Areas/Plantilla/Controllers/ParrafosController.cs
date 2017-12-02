using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Web.Servicios;
using GOB.SPF.ConecII.Entities.DTO;

namespace GOB.SPF.ConecII.Web.Areas.Plantilla
{
    public class ParrafosController : BaseController<UiParrafos>
    {
        #region Members



        #endregion

        #region Constructor

        public ParrafosController() : base("Parrafos", "Parrafo", RutaPlantilla)
        {
        }

        #endregion

        #region Actions
        public override async Task<PartialViewResult> Item(UiParrafos model)
        {
            BaseServices<UiTiposSeccion> servicesTiposSeccion = new BaseServices<UiTiposSeccion>("TiposSeccion");
            
            List<DropDto> lstTiposSeccion = servicesTiposSeccion.ConsultaList();
            ViewBag.lstTiposSeccion = lstTiposSeccion;
            
            return AccionItem(model);

        }
        #endregion

        #region Drop

        #endregion

    }
}
