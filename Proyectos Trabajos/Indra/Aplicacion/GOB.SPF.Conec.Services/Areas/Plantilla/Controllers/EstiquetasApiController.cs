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
     public class EstiquetasController : ApiController
    {
 
         AdministraEstiquetas mAdministraEstiquetas;
 
         public EstiquetasController()
         {
             mAdministraEstiquetas = new AdministraEstiquetas();
         }
 
 
         // GET: api/Estiquetas
         public IEnumerable<Estiquetas> Get()
         {
             return mAdministraEstiquetas.FindItemsStored();
         }
 
 
         [Route("api/Estiquetas/Drop")]
         public IEnumerable<DropDto> GetDrop()
         {
             return mAdministraEstiquetas.FindItemsDropStored();
         }
 
 
         [Route("api/Estiquetas/Paged")]
         [HttpPost]
         public object GetPaged([FromBody]EstiquetasDtoBuscar find)
         {
             IEnumerable<Estiquetas> items = mAdministraEstiquetas.FindPagedStored(find);
             return new { items, find.PageTotalItems };
         }
 
         // GET: api/Estiquetas/5
         [Route("api/Estiquetas/GetById")]
         [ResponseType(typeof(Estiquetas))]
         [HttpPost]
         public IHttpActionResult Get([FromBody]int id)
         {
             Estiquetas estiquetas = mAdministraEstiquetas.FindById(id);
             if (estiquetas == null)
             {
                 return NotFound();
             }
 
             return Ok(estiquetas);
         }
 
         // PUT: api/Estiquetas/5
         [ResponseType(typeof(void))]
         public IHttpActionResult Put(Estiquetas estiquetas)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             try
             {
                 mAdministraEstiquetas.UpdateStored(estiquetas);
             }
             catch (Exception ex)
             {
                 if (!Exists((int)estiquetas.IdEtiqueta))
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
 
         // POST: api/Estiquetas
         [ResponseType(typeof(Estiquetas))]
         public IHttpActionResult Post(Estiquetas estiquetas)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             mAdministraEstiquetas.InsertStored(estiquetas);
 
             return CreatedAtRoute("DefaultApi", new { id = estiquetas.IdEtiqueta }, estiquetas);
         }
 
         // DELETE: api/Estiquetas/5
         [ResponseType(typeof(Estiquetas))]
         public IHttpActionResult Delete(int id)
         {
             Estiquetas estiquetas = mAdministraEstiquetas.FindById(id);
             if (estiquetas == null)
             {
                 return NotFound();
             }
 
             mAdministraEstiquetas.DeleteStored(estiquetas);
 
             return Ok(estiquetas);
         }
 
         protected override void Dispose(bool disposing)
         {
             
             base.Dispose(disposing);
         }
 
         private bool Exists(int id)
         {
            Estiquetas estiqueta  = mAdministraEstiquetas.FindById(id);
             return estiqueta.IdEtiqueta != 0;
         }
     }
 }
