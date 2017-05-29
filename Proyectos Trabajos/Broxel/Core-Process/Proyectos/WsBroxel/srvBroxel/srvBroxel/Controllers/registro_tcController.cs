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
    public class registro_tcController : ApiController
    {
        

        // GET: api/registro_tc
        public IQueryable<registro_tc> Getregistro_tc()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.registro_tc;
        }

        // GET: api/registro_tc/5
        [ResponseType(typeof(registro_tc))]
        public IHttpActionResult Getregistro_tc(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            registro_tc registro_tc = db.registro_tc.Find(id);
            if (registro_tc == null)
            {
                return NotFound();
            }

            return Ok(registro_tc);
        }

        // PUT: api/registro_tc/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putregistro_tc(int id, registro_tc registro_tc)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registro_tc.id)
            {
                return BadRequest();
            }

            db.Entry(registro_tc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!registro_tcExists(id))
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

        // POST: api/registro_tc
        [ResponseType(typeof(registro_tc))]
        public IHttpActionResult Postregistro_tc(registro_tc registro_tc)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.registro_tc.Add(registro_tc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = registro_tc.id }, registro_tc);
        }

        //// DELETE: api/registro_tc/5
        //[ResponseType(typeof(registro_tc))]
        //public IHttpActionResult Deleteregistro_tc(int id)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    registro_tc registro_tc = db.registro_tc.Find(id);
        //    if (registro_tc == null)
        //    {
        //        return NotFound();
        //    }

        //    db.registro_tc.Remove(registro_tc);
        //    db.SaveChanges();

        //    return Ok(registro_tc);
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

        private bool registro_tcExists(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.registro_tc.Count(e => e.id == id) > 0;
        }
    }
}