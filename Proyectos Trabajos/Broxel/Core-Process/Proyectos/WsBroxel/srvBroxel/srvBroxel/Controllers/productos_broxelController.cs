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
    public class productos_broxelController : ApiController
    {
        

        // GET: api/productos_broxel
        public IQueryable<productos_broxel> Getproductos_broxel()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.productos_broxel;
        }

        // GET: api/productos_broxel/5
        [ResponseType(typeof(productos_broxel))]
        public IHttpActionResult Getproductos_broxel(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            productos_broxel productos_broxel = db.productos_broxel.Find(id);
            if (productos_broxel == null)
            {
                return NotFound();
            }

            return Ok(productos_broxel);
        }

        //// PUT: api/productos_broxel/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putproductos_broxel(int id, productos_broxel productos_broxel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != productos_broxel.ID0)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(productos_broxel).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!productos_broxelExists(id))
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

        //// POST: api/productos_broxel
        //[ResponseType(typeof(productos_broxel))]
        //public IHttpActionResult Postproductos_broxel(productos_broxel productos_broxel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.productos_broxel.Add(productos_broxel);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = productos_broxel.ID0 }, productos_broxel);
        //}

        //// DELETE: api/productos_broxel/5
        //[ResponseType(typeof(productos_broxel))]
        //public IHttpActionResult Deleteproductos_broxel(int id)
        //{
        //    productos_broxel productos_broxel = db.productos_broxel.Find(id);
        //    if (productos_broxel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.productos_broxel.Remove(productos_broxel);
        //    db.SaveChanges();

        //    return Ok(productos_broxel);
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

        private bool productos_broxelExists(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.productos_broxel.Count(e => e.ID0 == id) > 0;
        }
    }
}