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
     public class ParrafosController : ApiController
     {
 
         AdministraParrafos mAdministraParrafos;
 
         public ParrafosController()
         {
             mAdministraParrafos = new AdministraParrafos();
         }
 
 
         // GET: api/Parrafos
         public IEnumerable<Parrafos> Get()
         {
             return mAdministraParrafos.FindItemsStored();
         }
 
 
         [Route("api/Parrafos/Drop")]
         public IEnumerable<DropDto> GetDrop()
         {
             return mAdministraParrafos.FindItemsDropStored();
         }
 
 
         [Route("api/Parrafos/Paged")]
         [HttpPost]
         public object GetPaged([FromBody]ParrafosDtoBuscar find)
         {
             IEnumerable<Parrafos> items = mAdministraParrafos.FindPagedStored(find);
             return new { items, find.PageTotalItems };
         }
 
         // GET: api/Parrafos/5
         [Route("api/Parrafos/GetById")]
         [ResponseType(typeof(Parrafos))]
         [HttpPost]
         public IHttpActionResult Get([FromBody]int id)
         {
             Parrafos parrafos = mAdministraParrafos.FindById(id);
             if (parrafos == null)
             {
                 return NotFound();
             }
 
             return Ok(parrafos);
         }
 
         // PUT: api/Parrafos/5
         [ResponseType(typeof(void))]
         public IHttpActionResult Put(Parrafos parrafos)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             try
             {
                 mAdministraParrafos.UpdateStored(parrafos);
             }
            catch (Exception ex)
            {
                 if (!Exists((int)parrafos.IdParrafo))
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
 
         // POST: api/Parrafos
         [ResponseType(typeof(Parrafos))]
         public IHttpActionResult Post(Parrafos parrafos)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
 
             mAdministraParrafos.InsertStored(parrafos);
 
             return CreatedAtRoute("DefaultApi", new { id = parrafos.IdParrafo }, parrafos);
         }
 
         // DELETE: api/Parrafos/5
         [ResponseType(typeof(Parrafos))]
         public IHttpActionResult Delete(int id)
         {
             Parrafos parrafos = mAdministraParrafos.FindById(id);
             if (parrafos == null)
             {
                 return NotFound();
             }
 
             mAdministraParrafos.DeleteStored(parrafos);
 
             return Ok(parrafos);
         }
 
         protected override void Dispose(bool disposing)
         {
             
             base.Dispose(disposing);
         }
 
         private bool Exists(int id)
         {
            Parrafos parrafo =  mAdministraParrafos.FindById(id);
            return parrafo.IdParrafo != 0;
         }
     }
 }
