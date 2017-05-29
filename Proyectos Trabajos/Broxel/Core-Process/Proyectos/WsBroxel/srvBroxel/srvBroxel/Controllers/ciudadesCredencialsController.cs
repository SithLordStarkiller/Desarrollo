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
    public class ciudadesCredencialsController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/ciudadesCredencials
        public IQueryable<ciudadesCredencial> GetciudadesCredencial()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.ciudadesCredencial;
        }

        // GET: api/ciudadesCredencials/5
        [ResponseType(typeof(ciudadesCredencial))]
        public IHttpActionResult GetciudadesCredencial(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            ciudadesCredencial ciudadesCredencial = db.ciudadesCredencial.Find(id);
            if (ciudadesCredencial == null)
            {
                return NotFound();
            }

            return Ok(ciudadesCredencial);
        }

        //// PUT: api/ciudadesCredencials/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutciudadesCredencial(long id, ciudadesCredencial ciudadesCredencial)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != ciudadesCredencial.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(ciudadesCredencial).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ciudadesCredencialExists(id))
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

        //// POST: api/ciudadesCredencials
        //[ResponseType(typeof(ciudadesCredencial))]
        //public IHttpActionResult PostciudadesCredencial(ciudadesCredencial ciudadesCredencial)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.ciudadesCredencial.Add(ciudadesCredencial);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = ciudadesCredencial.Id }, ciudadesCredencial);
        //}

        // DELETE: api/ciudadesCredencials/5
        //[ResponseType(typeof(ciudadesCredencial))]
        //public IHttpActionResult DeleteciudadesCredencial(long id)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    ciudadesCredencial ciudadesCredencial = db.ciudadesCredencial.Find(id);
        //    if (ciudadesCredencial == null)
        //    {
        //        return NotFound();
        //    }

        //    db.ciudadesCredencial.Remove(ciudadesCredencial);
        //    db.SaveChanges();

        //    return Ok(ciudadesCredencial);
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

        private bool ciudadesCredencialExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.ciudadesCredencial.Count(e => e.Id == id) > 0;
        }
    }
}