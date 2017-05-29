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
using System.Diagnostics;

namespace srvBroxel.Controllers
{
    public class OrdenDeTrabajoesController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/OrdenDeTrabajoes
        public IQueryable<OrdenDeTrabajo> GetOrdenDeTrabajo()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.OrdenDeTrabajo;
        }

        // GET: api/OrdenDeTrabajoes/5
        [ResponseType(typeof(OrdenDeTrabajo))]
        public IHttpActionResult GetOrdenDeTrabajo(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            OrdenDeTrabajo ordenDeTrabajo = db.OrdenDeTrabajo.Find(id);
            if (ordenDeTrabajo == null)
            {
                return NotFound();
            }

            return Ok(ordenDeTrabajo);
        }

        // PUT: api/OrdenDeTrabajoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrdenDeTrabajo(long id, OrdenDeTrabajo ordenDeTrabajo)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ordenDeTrabajo.Id)
            {
                return BadRequest();
            }

            db.Entry(ordenDeTrabajo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenDeTrabajoExists(id))
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

        // POST: api/OrdenDeTrabajoes
        [ResponseType(typeof(OrdenDeTrabajo))]
        public IHttpActionResult PostOrdenDeTrabajo(OrdenDeTrabajo ordenDeTrabajo)
        {
            try
            {
                broxelco_rdgEntities db = new broxelco_rdgEntities();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.OrdenDeTrabajo.Add(ordenDeTrabajo);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = ordenDeTrabajo.Id }, ordenDeTrabajo);
            }
            catch(Exception ex)
            {
                Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffff") + " ==== " +
                                          "Insercion a la base: " + ex.Message);
                return null;
            }
            
        }

        // DELETE: api/OrdenDeTrabajoes/5
        [ResponseType(typeof(OrdenDeTrabajo))]
        public IHttpActionResult DeleteOrdenDeTrabajo(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            OrdenDeTrabajo ordenDeTrabajo = db.OrdenDeTrabajo.Find(id);
            if (ordenDeTrabajo == null)
            {
                return NotFound();
            }

            db.OrdenDeTrabajo.Remove(ordenDeTrabajo);
            db.SaveChanges();

            return Ok(ordenDeTrabajo);
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

        private bool OrdenDeTrabajoExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.OrdenDeTrabajo.Count(e => e.Id == id) > 0;
        }
    }
}