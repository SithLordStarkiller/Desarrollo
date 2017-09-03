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
     public class ContratantesController : ApiController
    {
 
         AdministraContratantes mAdministraContratantes;
 
         public ContratantesController()
         {
             mAdministraContratantes = new AdministraContratantes();
         }
 
 
         // GET: api/Contratantes
         public IEnumerable<Contratantes> Get()
         {
             return mAdministraContratantes.FindItemsStored();
         }
 
 
         [Route("api/Contratantes/Drop")]
         public IEnumerable<DropDto> GetDrop()
         {
             return mAdministraContratantes.FindItemsDropStored();
         }
 
 
         [Route("api/Contratantes/Paged")]
         [HttpPost]
         public object GetPaged([FromBody]ContratantesDtoBuscar find)
         {
             IEnumerable<Contratantes> items = mAdministraContratantes.FindPagedStored(find);
             return new { items, find.PageTotalItems };
         }
 
         // GET: api/Contratantes/5
         [Route("api/Contratantes/GetById")]
         [ResponseType(typeof(Contratantes))]
         [HttpPost]
         public IHttpActionResult Get([FromBody]int id)
         {
             Contratantes contratantes = mAdministraContratantes.FindById(id);
             if (contratantes == null)
             {
                 return NotFound();
             }
 
             return Ok(contratantes);
         }
 
         // PUT: api/Contratantes/5
         [ResponseType(typeof(void))]
         public IHttpActionResult Put(Contratantes contratantes)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             try
             {
                 mAdministraContratantes.UpdateStored(contratantes);
             }
             catch (Exception ex)
             {
                 if (!Exists((int)contratantes.IdContratante))
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
 
         // POST: api/Contratantes
         [ResponseType(typeof(Contratantes))]
         public IHttpActionResult Post(Contratantes contratantes)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             mAdministraContratantes.InsertStored(contratantes);
 
             return CreatedAtRoute("DefaultApi", new { id = contratantes.IdContratante }, contratantes);
         }
 
         // DELETE: api/Contratantes/5
         [ResponseType(typeof(Contratantes))]
         public IHttpActionResult Delete(int id)
         {
             Contratantes contratantes = mAdministraContratantes.FindById(id);
             if (contratantes == null)
             {
                 return NotFound();
             }
 
             mAdministraContratantes.DeleteStored(contratantes);
 
             return Ok(contratantes);
         }
 
         protected override void Dispose(bool disposing)
         {
             
             base.Dispose(disposing);
         }
 
         private bool Exists(int id)
         {
            Contratantes contratante  = mAdministraContratantes.FindById(id);
             return contratante.IdContratante != 0;
         }
     }
 }
