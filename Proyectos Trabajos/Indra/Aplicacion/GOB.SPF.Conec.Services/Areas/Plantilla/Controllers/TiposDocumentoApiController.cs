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
     public class TiposDocumentoController : ApiController
    {
 
         AdministraTiposDocumento mAdministraTiposDocumento;
 
         public TiposDocumentoController()
         {
             mAdministraTiposDocumento = new AdministraTiposDocumento();
         }
 
 
         // GET: api/TiposDocumento
         public IEnumerable<TiposDocumento> Get()
         {
             return mAdministraTiposDocumento.FindItemsStored();
         }
 
 
         [Route("api/TiposDocumento/Drop")]
         public IEnumerable<DropDto> GetDrop()
         {
             return mAdministraTiposDocumento.FindItemsDropStored();
         }
 
 
         [Route("api/TiposDocumento/Paged")]
         [HttpPost]
         public object GetPaged([FromBody]TiposDocumentoDtoBuscar find)
         {
             IEnumerable<TiposDocumento> items = mAdministraTiposDocumento.FindPagedStored(find);
             return new { items, find.PageTotalItems };
         }
 
         // GET: api/TiposDocumento/5
         [Route("api/TiposDocumento/GetById")]
         [ResponseType(typeof(TiposDocumento))]
         [HttpPost]
         public IHttpActionResult Get([FromBody]int id)
         {
             TiposDocumento tiposdocumento = mAdministraTiposDocumento.FindById(id);
             if (tiposdocumento == null)
             {
                 return NotFound();
             }
 
             return Ok(tiposdocumento);
         }
 
         // PUT: api/TiposDocumento/5
         [ResponseType(typeof(void))]
         public IHttpActionResult Put(TiposDocumento tiposdocumento)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             try
             {
                 mAdministraTiposDocumento.UpdateStored(tiposdocumento);
             }
             catch (Exception ex)
             {
                 if (!Exists((int)tiposdocumento.IdTipoDocumento))
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
 
         // POST: api/TiposDocumento
         [ResponseType(typeof(TiposDocumento))]
         public IHttpActionResult Post(TiposDocumento tiposdocumento)
         {   
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             mAdministraTiposDocumento.InsertStored(tiposdocumento);
 
             return CreatedAtRoute("DefaultApi", new { id = tiposdocumento.IdTipoDocumento }, tiposdocumento);
         }
 
         // DELETE: api/TiposDocumento/5
         [ResponseType(typeof(TiposDocumento))]
         public IHttpActionResult Delete(int id)
         {
             TiposDocumento tiposdocumento = mAdministraTiposDocumento.FindById(id);
             if (tiposdocumento == null)
             {
                 return NotFound();
             }
 
             mAdministraTiposDocumento.DeleteStored(tiposdocumento);
 
             return Ok(tiposdocumento);
         }
 
         protected override void Dispose(bool disposing)
         {
             
             base.Dispose(disposing);
         }
 
         private bool Exists(int id)
         {
            TiposDocumento tiposDocumento = mAdministraTiposDocumento.FindById(id);
             return tiposDocumento.IdTipoDocumento != 0;
         }
     }
 }
