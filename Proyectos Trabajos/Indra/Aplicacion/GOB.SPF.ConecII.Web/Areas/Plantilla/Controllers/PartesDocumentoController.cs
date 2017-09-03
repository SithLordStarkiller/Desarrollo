using GOB.SPF.ConecII.Entities.DTO;
using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Areas.Plantilla
{
    public class PartesDocumentoController : ControllerCrud<PartesDocumento, PartesDocumentoDtoBuscar>
    {
        #region Members



        #endregion

        #region Constructor

        public PartesDocumentoController()
        {
            ControllerName = "PartesDocumento";
        }

        #endregion

        #region Actions

        public override ActionResult Index()
        {
            FormatoPlantilla formatoPlantilla = new FormatoPlantilla();
            PartesDocumentoDtoBuscar find = new PartesDocumentoDtoBuscar();
            TryUpdateModel(find, new[] { "IdTipoDocumentoBuscar" });//regrese un bool
            if (ModelState.IsValid)
            {
                if (!find.IdTipoDocumentoBuscar.HasValue)
                { 
                    find.IdTipoDocumentoBuscar = 3;// -1;
                }

                ViewBag.IdTipoDocumento = find.IdTipoDocumentoBuscar;

                List<PartesDocumento> items = FindPaged(find);
                formatoPlantilla.PartesDocumento = items.FirstOrDefault();

                
                InstitucionesUIController institucionCrud = new InstitucionesUIController();
                institucionCrud.ControllerName = "Instituciones";
                InstitucionesDtoBuscar findInstituciones = new InstitucionesDtoBuscar() { IdTipoDocumentoBuscar = find.IdTipoDocumentoBuscar };
                formatoPlantilla.ListaInstituciones = institucionCrud.FindPaged(findInstituciones);

                ParrafosUIController parrafoCrud = new ParrafosUIController();
                parrafoCrud.ControllerName = "Parrafos";
                ParrafosDtoBuscar findParrafos = new ParrafosDtoBuscar() { IdTipoDocumentoBuscar = find.IdTipoDocumentoBuscar };
                formatoPlantilla.ListaParrafos = parrafoCrud.FindPaged(findParrafos);


                List<EtiquetasParrafo> etiquetasParrafos= ObtenerEtiquetasParrafoPorIdTipoDocumento((int)find.IdTipoDocumentoBuscar);
                foreach (Parrafos parrafo in formatoPlantilla.ListaParrafos)
                    parrafo.ListaEtiquetasParrafo = etiquetasParrafos.Where(e => e.IdParrafo == parrafo.IdParrafo).ToList();

                return View(formatoPlantilla);
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";
            
            return FindPartial(find);
        }

        private List<EtiquetasParrafo> ObtenerEtiquetasParrafoPorIdTipoDocumento(int IdTipoDocumento)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri((String)System.Configuration.ConfigurationManager.AppSettings["UrlServerApi"]);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("EtiquetasParrafo/GetPorIdTipoDocumento", IdTipoDocumento).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadAsAsync<List<EtiquetasParrafo>>().Result;
                return respuesta;
            }
            return null;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Index([Bind(Include = "IdParteDocumento, IdTipoDocumento, RutaLogo, Folio, Asunto, LugarFecha, Paginado, Ccp, Puesto, Nombre, Direccion, FechaInicial, FechaFinal, Activo")]PartesDocumento item, PartesDocumentoDtoBuscar find)
        {
            TryUpdateModel(item, new[] { "IdParteDocumento", "IdTipoDocumento", "RutaLogo", "Folio", "Asunto", "LugarFecha", "Paginado", "Ccp", "Puesto", "Nombre", "Direccion", "FechaInicial", "FechaFinal", "Activo" });//regrese un bool
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInstituciones(InstitucionesDtoBuscar find)
        {
            Instituciones item = new Instituciones();
            InstitucionesUIController institucionCrud = new InstitucionesUIController();

            ViewBag.IdTipoDocumento = find.IdTipoDocumentoBuscar;

            TryUpdateModel(item, new[] { "IdInstitucion", "IdTipoDocumento", "Nombre", "Negrita", "Orden", "FechaInicial", "FechaFinal", "Activo" });//regrese un bool
            if (ModelState.IsValid)
            {
                if (!item.FechaInicial.HasValue)
                    item.FechaInicial = DateTime.Today;
                if (!item.FechaFinal.HasValue)
                    item.FechaFinal = DateTime.Today;
                bool resultado = institucionCrud.Save(item, find);

                if (resultado == true)
                    ViewBag.msj = "Se registró la información exitosamente";
                else
                    ViewBag.msj = "No se registró la información, favor de intentar nuevamente...";            
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";

            
            return PartialView("ListaInstituciones", institucionCrud.FindPaged(find));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveParrafos(ParrafosDtoBuscar find)
        {
            Parrafos item = new Parrafos();
            ParrafosUIController parrafoCrud = new ParrafosUIController();

            ViewBag.IdTipoDocumento = find.IdTipoDocumentoBuscar;

            TryUpdateModel(item, new[] { "IdParrafo", "IdTipoDocumento", "Nombre", "Texto", "FechaInicial", "FechaFinal", "Activo" });//regrese un bool
            if (ModelState.IsValid)
            {
                if (!item.FechaInicial.HasValue)
                    item.FechaInicial = DateTime.Today;
                if (!item.FechaFinal.HasValue)
                    item.FechaFinal = DateTime.Today;
                bool resultado = parrafoCrud.Save(item, find);

                if (resultado == true)
                    ViewBag.msj = "Se registró la información exitosamente";
                else
                    ViewBag.msj = "No se registró la información, favor de intentar nuevamente...";
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";

            List<Parrafos> lstParrafos = parrafoCrud.FindPaged(find);
            List<EtiquetasParrafo> etiquetasParrafos = ObtenerEtiquetasParrafoPorIdTipoDocumento((int)find.IdTipoDocumentoBuscar);
            foreach (Parrafos parrafo in lstParrafos)
                parrafo.ListaEtiquetasParrafo = etiquetasParrafos.Where(e => e.IdParrafo == parrafo.IdParrafo).ToList();

            return PartialView("ListaParrafos", lstParrafos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEtiquetasParrafo(EtiquetasParrafoDtoBuscar find , ParrafosDtoBuscar findParrafos)
        {
            EtiquetasParrafo item = new EtiquetasParrafo();
            EtiquetasParrafoUIController parrafoEtiquetaCrud = new EtiquetasParrafoUIController();
            ParrafosUIController parrafoCrud = new ParrafosUIController();

            ViewBag.IdTipoDocumento = findParrafos.IdTipoDocumentoBuscar;

            TryUpdateModel(item, new[] { "IdEtiquetaParrafo", "IdParrafo", "Etiqueta", "Contenido", "Negrita", "Orden", "FechaInicial", "FechaFinal", "Activo" });//regrese un bool
            if (ModelState.IsValid)
            {
                if (!item.FechaInicial.HasValue)
                    item.FechaInicial = DateTime.Today;
                if (!item.FechaFinal.HasValue)
                    item.FechaFinal = DateTime.Today;
                bool resultado = parrafoEtiquetaCrud.Save(item, find);

                if (resultado == true)
                    ViewBag.msj = "Se registró la información exitosamente";
                else
                    ViewBag.msj = "No se registró la información, favor de intentar nuevamente...";
            }
            else
                ViewBag.msj = "Datos no validos, no se guardo el registró";

            findParrafos.IdParrafoBuscar = 0;
            List<Parrafos> lstParrafos = parrafoCrud.FindPaged(findParrafos);
            List<EtiquetasParrafo> etiquetasParrafos = ObtenerEtiquetasParrafoPorIdTipoDocumento((int)findParrafos.IdTipoDocumentoBuscar);
            foreach (Parrafos parrafo in lstParrafos)
                parrafo.ListaEtiquetasParrafo = etiquetasParrafos.Where(e => e.IdParrafo == parrafo.IdParrafo).ToList();

            return PartialView("ListaParrafos", lstParrafos);
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
