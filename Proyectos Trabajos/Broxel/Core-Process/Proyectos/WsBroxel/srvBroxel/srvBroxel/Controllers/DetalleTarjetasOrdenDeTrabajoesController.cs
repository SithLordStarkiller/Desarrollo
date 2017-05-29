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
    public class DetalleTarjetasOrdenDeTrabajoesController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/DetalleTarjetasOrdenDeTrabajoes
        public IQueryable<DetalleTarjetasOrdenDeTrabajo> GetDetalleTarjetasOrdenDeTrabajo()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.DetalleTarjetasOrdenDeTrabajo;
        }

        // GET: api/DetalleTarjetasOrdenDeTrabajoes/5
        [ResponseType(typeof(DetalleTarjetasOrdenDeTrabajo))]
        public IHttpActionResult GetDetalleTarjetasOrdenDeTrabajo(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            DetalleTarjetasOrdenDeTrabajo detalleTarjetasOrdenDeTrabajo = db.DetalleTarjetasOrdenDeTrabajo.Find(id);
            if (detalleTarjetasOrdenDeTrabajo == null)
            {
                return NotFound();
            }

            return Ok(detalleTarjetasOrdenDeTrabajo);
        }

        // PUT: api/DetalleTarjetasOrdenDeTrabajoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetalleTarjetasOrdenDeTrabajo(long id, DetalleTarjetasOrdenDeTrabajo detalleTarjetasOrdenDeTrabajo)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleTarjetasOrdenDeTrabajo.Id)
            {
                return BadRequest();
            }

            db.Entry(detalleTarjetasOrdenDeTrabajo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleTarjetasOrdenDeTrabajoExists(id))
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

        // POST: api/DetalleTarjetasOrdenDeTrabajoes
        [ResponseType(typeof(DetalleTarjetasOrdenDeTrabajo))]
        public IHttpActionResult PostDetalleTarjetasOrdenDeTrabajo(DetalleTarjetasOrdenDeTrabajo detalleTarjetasOrdenDeTrabajo)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetalleTarjetasOrdenDeTrabajo.Add(detalleTarjetasOrdenDeTrabajo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detalleTarjetasOrdenDeTrabajo.Id }, detalleTarjetasOrdenDeTrabajo);
        }

        // DELETE: api/DetalleTarjetasOrdenDeTrabajoes/5
        [ResponseType(typeof(DetalleTarjetasOrdenDeTrabajo))]
        public IHttpActionResult DeleteDetalleTarjetasOrdenDeTrabajo(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            DetalleTarjetasOrdenDeTrabajo detalleTarjetasOrdenDeTrabajo = db.DetalleTarjetasOrdenDeTrabajo.Find(id);
            if (detalleTarjetasOrdenDeTrabajo == null)
            {
                return NotFound();
            }

            db.DetalleTarjetasOrdenDeTrabajo.Remove(detalleTarjetasOrdenDeTrabajo);
            db.SaveChanges();

            return Ok(detalleTarjetasOrdenDeTrabajo);
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

        private bool DetalleTarjetasOrdenDeTrabajoExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.DetalleTarjetasOrdenDeTrabajo.Count(e => e.Id == id) > 0;
        }
    }
}