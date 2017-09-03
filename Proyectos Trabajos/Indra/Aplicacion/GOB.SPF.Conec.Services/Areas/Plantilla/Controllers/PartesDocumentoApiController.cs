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
     public class PartesDocumentoController : ApiController
    {
 
         AdministraPartesDocumento mAdministraPartesDocumento;
 
         public PartesDocumentoController()
         {
             mAdministraPartesDocumento = new AdministraPartesDocumento();
         }
 
 
         // GET: api/PartesDocumento
         public IEnumerable<PartesDocumento> Get()
         {
             return mAdministraPartesDocumento.FindItemsStored();
         }
 
 
         [Route("api/PartesDocumento/Drop")]
         public IEnumerable<DropDto> GetDrop()
         {
             return mAdministraPartesDocumento.FindItemsDropStored();
         }
 
 
         [Route("api/PartesDocumento/Paged")]
         [HttpPost]
         public object GetPaged([FromBody]PartesDocumentoDtoBuscar find)
         {
             IEnumerable<PartesDocumento> items = mAdministraPartesDocumento.FindPagedStored(find);
             return new { items, find.PageTotalItems };
         }
 
         // GET: api/PartesDocumento/5
         [Route("api/PartesDocumento/GetById")]
         [ResponseType(typeof(PartesDocumento))]
         [HttpPost]
         public IHttpActionResult Get([FromBody]int id)
         {
             PartesDocumento partesdocumento = mAdministraPartesDocumento.FindById(id);
             if (partesdocumento == null)
             {
                 return NotFound();
             }
 
             return Ok(partesdocumento);
         }
 
         // PUT: api/PartesDocumento/5
         [ResponseType(typeof(void))]
         public IHttpActionResult Put(PartesDocumento partesdocumento)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             try
             {
                 mAdministraPartesDocumento.UpdateStored(partesdocumento);
             }
             catch (Exception ex)
             {
                 if (!Exists((int)partesdocumento.IdParteDocumento))
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
 
         // POST: api/PartesDocumento
         [ResponseType(typeof(PartesDocumento))]
         public IHttpActionResult Post(PartesDocumento partesdocumento)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             mAdministraPartesDocumento.InsertStored(partesdocumento);
 
             return CreatedAtRoute("DefaultApi", new { id = partesdocumento.IdParteDocumento }, partesdocumento);
         }
 
         // DELETE: api/PartesDocumento/5
         [ResponseType(typeof(PartesDocumento))]
         public IHttpActionResult Delete(int id)
         {
             PartesDocumento partesdocumento = mAdministraPartesDocumento.FindById(id);
             if (partesdocumento == null)
             {
                 return NotFound();
             }
 
             mAdministraPartesDocumento.DeleteStored(partesdocumento);
 
             return Ok(partesdocumento);
         }
 
         protected override void Dispose(bool disposing)
         {
             
             base.Dispose(disposing);
         }
 
         private bool Exists(int id)
         {
            PartesDocumento partesDocumento = mAdministraPartesDocumento.FindById(id);
            return partesDocumento.IdParteDocumento != 0;
         }
     }
 }
