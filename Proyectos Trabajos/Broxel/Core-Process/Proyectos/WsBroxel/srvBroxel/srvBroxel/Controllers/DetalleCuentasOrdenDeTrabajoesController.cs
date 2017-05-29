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
    public class DetalleCuentasOrdenDeTrabajoesController : ApiController
    {
        

        // GET: api/DetalleCuentasOrdenDeTrabajoes
        public IQueryable<DetalleCuentasOrdenDeTrabajo> GetDetalleCuentasOrdenDeTrabajo()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            return db.DetalleCuentasOrdenDeTrabajo;
        }

        // GET: api/DetalleCuentasOrdenDeTrabajoes/5
        [ResponseType(typeof(DetalleCuentasOrdenDeTrabajo))]
        public IHttpActionResult GetDetalleCuentasOrdenDeTrabajo(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            DetalleCuentasOrdenDeTrabajo detalleCuentasOrdenDeTrabajo = db.DetalleCuentasOrdenDeTrabajo.Find(id);
            if (detalleCuentasOrdenDeTrabajo == null)
            {
                return NotFound();
            }

            return Ok(detalleCuentasOrdenDeTrabajo);
        }

        // PUT: api/DetalleCuentasOrdenDeTrabajoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleCuentasOrdenDeTrabajo(long id, DetalleCuentasOrdenDeTrabajo detalleCuentasOrdenDeTrabajo)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleCuentasOrdenDeTrabajo.Id)
            {
                return BadRequest();
            }

            db.Entry(detalleCuentasOrdenDeTrabajo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleCuentasOrdenDeTrabajoExists(id))
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

        // POST: api/DetalleCuentasOrdenDeTrabajoes
        [ResponseType(typeof(DetalleCuentasOrdenDeTrabajo))]
        public IHttpActionResult PostDetalleCuentasOrdenDeTrabajo(DetalleCuentasOrdenDeTrabajo detalleCuentasOrdenDeTrabajo)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleCuentasOrdenDeTrabajo.Add(detalleCuentasOrdenDeTrabajo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleCuentasOrdenDeTrabajo.Id }, detalleCuentasOrdenDeTrabajo);
        }

        // DELETE: api/DetalleCuentasOrdenDeTrabajoes/5
        [ResponseType(typeof(DetalleCuentasOrdenDeTrabajo))]
        public IHttpActionResult DeleteDetalleCuentasOrdenDeTrabajo(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            DetalleCuentasOrdenDeTrabajo detalleCuentasOrdenDeTrabajo = db.DetalleCuentasOrdenDeTrabajo.Find(id);
            if (detalleCuentasOrdenDeTrabajo == null)
            {
                return NotFound();
            }

            db.DetalleCuentasOrdenDeTrabajo.Remove(detalleCuentasOrdenDeTrabajo);
            db.SaveChanges();

            return Ok(detalleCuentasOrdenDeTrabajo);
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

        private bool DetalleCuentasOrdenDeTrabajoExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            return db.DetalleCuentasOrdenDeTrabajo.Count(e => e.Id == id) > 0;
        }
    }
}