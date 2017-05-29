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
    public class UsuariosOnlineBroxelsController : ApiController
    {
       

        // GET: api/UsuariosOnlineBroxels
        public IQueryable<UsuariosOnlineBroxel> GetUsuariosOnlineBroxel()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.UsuariosOnlineBroxel;
        }

        // GET: api/UsuariosOnlineBroxels/5
        [ResponseType(typeof(UsuariosOnlineBroxel))]
        public IHttpActionResult GetUsuariosOnlineBroxel(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            UsuariosOnlineBroxel usuariosOnlineBroxel = db.UsuariosOnlineBroxel.Find(id);
            if (usuariosOnlineBroxel == null)
            {
                return NotFound();
            }

            return Ok(usuariosOnlineBroxel);
        }

        //// PUT: api/UsuariosOnlineBroxels/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUsuariosOnlineBroxel(int id, UsuariosOnlineBroxel usuariosOnlineBroxel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != usuariosOnlineBroxel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(usuariosOnlineBroxel).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsuariosOnlineBroxelExists(id))
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

        //// POST: api/UsuariosOnlineBroxels
        //[ResponseType(typeof(UsuariosOnlineBroxel))]
        //public IHttpActionResult PostUsuariosOnlineBroxel(UsuariosOnlineBroxel usuariosOnlineBroxel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.UsuariosOnlineBroxel.Add(usuariosOnlineBroxel);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = usuariosOnlineBroxel.Id }, usuariosOnlineBroxel);
        //}

        //// DELETE: api/UsuariosOnlineBroxels/5
        //[ResponseType(typeof(UsuariosOnlineBroxel))]
        //public IHttpActionResult DeleteUsuariosOnlineBroxel(int id)
        //{
        //    UsuariosOnlineBroxel usuariosOnlineBroxel = db.UsuariosOnlineBroxel.Find(id);
        //    if (usuariosOnlineBroxel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.UsuariosOnlineBroxel.Remove(usuariosOnlineBroxel);
        //    db.SaveChanges();

        //    return Ok(usuariosOnlineBroxel);
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

        private bool UsuariosOnlineBroxelExists(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.UsuariosOnlineBroxel.Count(e => e.Id == id) > 0;
        }
    }
}