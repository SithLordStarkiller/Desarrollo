using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using srvBroxel.DAL;

namespace srvBroxel.Controllers
{
    public class clientesBroxelsController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/clientesBroxels
        public IQueryable<clientesBroxel> GetclientesBroxel()
        {
            var db = new broxelco_rdgEntities();

            return db.clientesBroxel;
        }

        // GET: api/clientesBroxels/5
        [ResponseType(typeof(clientesBroxel))]
        public IHttpActionResult GetclientesBroxel(long id)
        {
            var db = new broxelco_rdgEntities();

            clientesBroxel clientesBroxel = db.clientesBroxel.Find(id);
            if (clientesBroxel == null)
            {
                return NotFound();
            }

            return Ok(clientesBroxel);
        }

        // PUT: api/clientesBroxels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutclientesBroxel(long id, clientesBroxel clientesBroxel)
        {
            var db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientesBroxel.id)
            {
                return BadRequest();
            }

            db.Entry(clientesBroxel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clientesBroxelExists(id))
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

        // POST: api/clientesBroxels
        [ResponseType(typeof(clientesBroxel))]
        public IHttpActionResult PostclientesBroxel(clientesBroxel clientesBroxel)
        {
            var db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.clientesBroxel.Add(clientesBroxel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clientesBroxel.id }, clientesBroxel);
        }

        // DELETE: api/clientesBroxels/5
        [ResponseType(typeof(clientesBroxel))]
        public IHttpActionResult DeleteclientesBroxel(long id)
        {
            var db = new broxelco_rdgEntities();

            clientesBroxel clientesBroxel = db.clientesBroxel.Find(id);
            if (clientesBroxel == null)
            {
                return NotFound();
            }

            db.clientesBroxel.Remove(clientesBroxel);
            db.SaveChanges();

            return Ok(clientesBroxel);
        }

        protected override void Dispose(bool disposing)
        {
            var db = new broxelco_rdgEntities();

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clientesBroxelExists(long id)
        {
            var db = new broxelco_rdgEntities();

            return db.clientesBroxel.Count(e => e.id == id) > 0;
        }
    }
}