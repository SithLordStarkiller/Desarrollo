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
    public class estadosCredencialsController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/estadosCredencials
        public IQueryable<estadosCredencial> GetestadosCredencial()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.estadosCredencial;
        }

        // GET: api/estadosCredencials/5
        [ResponseType(typeof(estadosCredencial))]
        public IHttpActionResult GetestadosCredencial(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            estadosCredencial estadosCredencial = db.estadosCredencial.Find(id);
            if (estadosCredencial == null)
            {
                return NotFound();
            }

            return Ok(estadosCredencial);
        }

        //// PUT: api/estadosCredencials/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutestadosCredencial(long id, estadosCredencial estadosCredencial)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != estadosCredencial.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(estadosCredencial).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!estadosCredencialExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/estadosCredencials
        //[ResponseType(typeof(estadosCredencial))]
        //public IHttpActionResult PostestadosCredencial(estadosCredencial estadosCredencial)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.estadosCredencial.Add(estadosCredencial);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = estadosCredencial.id }, estadosCredencial);
        //}

        //// DELETE: api/estadosCredencials/5
        //[ResponseType(typeof(estadosCredencial))]
        //public IHttpActionResult DeleteestadosCredencial(long id)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    estadosCredencial estadosCredencial = db.estadosCredencial.Find(id);
        //    if (estadosCredencial == null)
        //    {
        //        return NotFound();
        //    }

        //    db.estadosCredencial.Remove(estadosCredencial);
        //    db.SaveChanges();

        //    return Ok(estadosCredencial);
        //}

        protected override void Dispose(bool disposing)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool estadosCredencialExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.estadosCredencial.Count(e => e.id == id) > 0;
        }
    }
}