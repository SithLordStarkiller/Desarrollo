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
    public class DetalleClientesBroxelsController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/DetalleClientesBroxels
        public IQueryable<DetalleClientesBroxel> GetDetalleClientesBroxel()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.DetalleClientesBroxel;
        }

        // GET: api/DetalleClientesBroxels/5
        [ResponseType(typeof(DetalleClientesBroxel))]
        public IHttpActionResult GetDetalleClientesBroxel(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            DetalleClientesBroxel detalleClientesBroxel = db.DetalleClientesBroxel.Find(id);
            if (detalleClientesBroxel == null)
            {
                return NotFound();
            }

            return Ok(detalleClientesBroxel);
        }

        // PUT: api/DetalleClientesBroxels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleClientesBroxel(long id, DetalleClientesBroxel detalleClientesBroxel)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleClientesBroxel.Id)
            {
                return BadRequest();
            }

            db.Entry(detalleClientesBroxel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleClientesBroxelExists(id))
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

        // POST: api/DetalleClientesBroxels
        [ResponseType(typeof(DetalleClientesBroxel))]
        public IHttpActionResult PostDetalleClientesBroxel(DetalleClientesBroxel detalleClientesBroxel)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleClientesBroxel.Add(detalleClientesBroxel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleClientesBroxel.Id }, detalleClientesBroxel);
        }

        // DELETE: api/DetalleClientesBroxels/5
        [ResponseType(typeof(DetalleClientesBroxel))]
        public IHttpActionResult DeleteDetalleClientesBroxel(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            DetalleClientesBroxel detalleClientesBroxel = db.DetalleClientesBroxel.Find(id);
            if (detalleClientesBroxel == null)
            {
                return NotFound();
            }

            db.DetalleClientesBroxel.Remove(detalleClientesBroxel);
            db.SaveChanges();

            return Ok(detalleClientesBroxel);
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

        private bool DetalleClientesBroxelExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.DetalleClientesBroxel.Count(e => e.Id == id) > 0;
        }
    }
}