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
    public class LogDetalleClientesBroxelsController : ApiController
    {
        

        // GET: api/LogDetalleClientesBroxels
        public IQueryable<LogDetalleClientesBroxel> GetLogDetalleClientesBroxel()
        {
            var db = new broxelco_rdgEntities();
            return db.LogDetalleClientesBroxel;
        }

        // GET: api/LogDetalleClientesBroxels/5
        [ResponseType(typeof(LogDetalleClientesBroxel))]
        public IHttpActionResult GetLogDetalleClientesBroxel(long id)
        {
            var db = new broxelco_rdgEntities();
            LogDetalleClientesBroxel logDetalleClientesBroxel = db.LogDetalleClientesBroxel.Find(id);
            if (logDetalleClientesBroxel == null)
            {
                return NotFound();
            }

            return Ok(logDetalleClientesBroxel);
        }

        // PUT: api/LogDetalleClientesBroxels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLogDetalleClientesBroxel(long id, LogDetalleClientesBroxel logDetalleClientesBroxel)
        {
            var db = new broxelco_rdgEntities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != logDetalleClientesBroxel.Id)
            {
                return BadRequest();
            }

            db.Entry(logDetalleClientesBroxel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogDetalleClientesBroxelExists(id))
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

        // POST: api/LogDetalleClientesBroxels
        [ResponseType(typeof(LogDetalleClientesBroxel))]
        public IHttpActionResult PostLogDetalleClientesBroxel(LogDetalleClientesBroxel logDetalleClientesBroxel)
        {
            var db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LogDetalleClientesBroxel.Add(logDetalleClientesBroxel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = logDetalleClientesBroxel.Id }, logDetalleClientesBroxel);
        }

        //// DELETE: api/LogDetalleClientesBroxels/5
        //[ResponseType(typeof(LogDetalleClientesBroxel))]
        //public IHttpActionResult DeleteLogDetalleClientesBroxel(long id)
        //{
        //    LogDetalleClientesBroxel logDetalleClientesBroxel = db.LogDetalleClientesBroxel.Find(id);
        //    if (logDetalleClientesBroxel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.LogDetalleClientesBroxel.Remove(logDetalleClientesBroxel);
        //    db.SaveChanges();

        //    return Ok(logDetalleClientesBroxel);
        //}

        protected override void Dispose(bool disposing)
        {
            var db = new broxelco_rdgEntities();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LogDetalleClientesBroxelExists(long id)
        {
            var db = new broxelco_rdgEntities();
            return db.LogDetalleClientesBroxel.Count(e => e.Id == id) > 0;
        }
    }
}