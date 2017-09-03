using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Business.Plantilla;
using GOB.SPF.ConecII.Entities.DTO;

namespace Cns.Sies.Rest.Mvc.Areas.Plantilla.Controllers
 {
     public class EtiquetasParrafoController : ApiController
    {
 
         AdministraEtiquetasParrafo mAdministraEtiquetasParrafo;
 
         public EtiquetasParrafoController()
         {
             mAdministraEtiquetasParrafo = new AdministraEtiquetasParrafo();
         }
 
 
         // GET: api/EtiquetasParrafo
         public IEnumerable<EtiquetasParrafo> Get()
         {
             return mAdministraEtiquetasParrafo.FindItemsStored();
         }
 
 
         [Route("api/EtiquetasParrafo/Drop")]
         public IEnumerable<DropDto> GetDrop()
         {
             return mAdministraEtiquetasParrafo.FindItemsDropStored();
         }
 
 
         [Route("api/EtiquetasParrafo/Paged")]
         [HttpPost]
         public object GetPaged([FromBody]EtiquetasParrafoDtoBuscar find)
         {
             IEnumerable<EtiquetasParrafo> items = mAdministraEtiquetasParrafo.FindPagedStored(find);
             return new { items, find.PageTotalItems };
         }


         [Route("api/EtiquetasParrafo/GetPorIdTipoDocumento")]
         [ResponseType(typeof(List<EtiquetasParrafo>))]
         [HttpPost]
         public IHttpActionResult GetPorIdTipoDocumento([FromBody]int IdTipoDocumento)
         {
             List<EtiquetasParrafo> etiquetasparrafo = mAdministraEtiquetasParrafo.GetPorIdTipoDocumento(IdTipoDocumento);
             if (etiquetasparrafo == null)
             {
                 return NotFound();
             }

             return Ok(etiquetasparrafo);
         }
         
 
         // GET: api/EtiquetasParrafo/5
         [Route("api/EtiquetasParrafo/GetById")]
         [ResponseType(typeof(EtiquetasParrafo))]
         [HttpPost]
         public IHttpActionResult Get([FromBody]int id)
         {
             EtiquetasParrafo etiquetasparrafo = mAdministraEtiquetasParrafo.FindById(id);
             if (etiquetasparrafo == null)
             {
                 return NotFound();
             }
 
             return Ok(etiquetasparrafo);
         }
 
         // PUT: api/EtiquetasParrafo/5
         [ResponseType(typeof(void))]
         public IHttpActionResult Put(EtiquetasParrafo etiquetasparrafo)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             try
             {
                 mAdministraEtiquetasParrafo.UpdateStored(etiquetasparrafo);
             }
             catch (Exception ex)
             {
                 if (!Exists((int)etiquetasparrafo.IdEtiquetaParrafo))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }
 
             return StatusCode(HttpStatusCode.NoContent);
         }
 
         // POST: api/EtiquetasParrafo
         [ResponseType(typeof(EtiquetasParrafo))]
         public IHttpActionResult Post(EtiquetasParrafo etiquetasparrafo)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             mAdministraEtiquetasParrafo.InsertStored(etiquetasparrafo);
 
             return CreatedAtRoute("DefaultApi", new { id = etiquetasparrafo.IdEtiquetaParrafo }, etiquetasparrafo);
         }
 
         // DELETE: api/EtiquetasParrafo/5
         [ResponseType(typeof(EtiquetasParrafo))]
         public IHttpActionResult Delete(int id)
         {
             EtiquetasParrafo etiquetasparrafo = mAdministraEtiquetasParrafo.FindById(id);
             if (etiquetasparrafo == null)
             {
                 return NotFound();
             }
 
             mAdministraEtiquetasParrafo.DeleteStored(etiquetasparrafo);
 
             return Ok(etiquetasparrafo);
         }
 
         protected override void Dispose(bool disposing)
         {
             
             base.Dispose(disposing);
         }
 
         private bool Exists(int id)
         {
            EtiquetasParrafo etiquetasParrafo = mAdministraEtiquetasParrafo.FindById(id);
             return etiquetasParrafo.IdEtiquetaParrafo != 0;
         }
     }
 }
