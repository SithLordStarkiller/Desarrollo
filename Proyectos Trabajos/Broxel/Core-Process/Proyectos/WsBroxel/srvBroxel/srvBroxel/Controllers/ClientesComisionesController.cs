using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using srvBroxel.DAL;

namespace srvBroxel.Controllers
{
    public class ClientesComisionesController : ApiController
    {
        

        // GET: api/ClientesComisiones
        public IQueryable<ClientesComisiones> GetClientesComisiones()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            return db.ClientesComisiones;
        }

        // GET: api/ClientesComisiones/5
        [ResponseType(typeof(ClientesComisiones))]
        public IHttpActionResult GetClientesComisiones(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            ClientesComisiones clientesComisiones = db.ClientesComisiones.Find(id);
            if (clientesComisiones == null)
            {
                return NotFound();
            }

            return Ok(clientesComisiones);
        }

        // PUT: api/ClientesComisiones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClientesComisiones(long id, ClientesComisiones clientesComisiones)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientesComisiones.Id)
            {
                return BadRequest();
            }

            db.Entry(clientesComisiones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesComisionesExists(id))
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

        // POST: api/ClientesComisiones
        [ResponseType(typeof(ClientesComisiones))]
        public IHttpActionResult PostClientesComisiones(ClientesComisiones clientesComisiones)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.ClientesComisiones.Add(clientesComisiones);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new {id = clientesComisiones.Id}, clientesComisiones);
            }
            catch (DbEntityValidationException entityValidationException)
            {
                foreach (var validationErrors in entityValidationException.EntityValidationErrors)
                {
                    var exceptionText = new StringBuilder();
                    exceptionText.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", validationErrors.Entry.Entity.GetType().Name, validationErrors.Entry.State);
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        exceptionText.AppendFormat("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                    ErrorHandling.EscribeError(exceptionText.ToString());
                }
                return null;
            }
        }

        // DELETE: api/ClientesComisiones/5
        [ResponseType(typeof(ClientesComisiones))]
        public IHttpActionResult DeleteClientesComisiones(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            ClientesComisiones clientesComisiones = db.ClientesComisiones.Find(id);
            if (clientesComisiones == null)
            {
                return NotFound();
            }

            db.ClientesComisiones.Remove(clientesComisiones);
            db.SaveChanges();

            return Ok(clientesComisiones);
        }

        protected override void Dispose(bool disposing)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientesComisionesExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            return db.ClientesComisiones.Count(e => e.Id == id) > 0;
        }
    }
}