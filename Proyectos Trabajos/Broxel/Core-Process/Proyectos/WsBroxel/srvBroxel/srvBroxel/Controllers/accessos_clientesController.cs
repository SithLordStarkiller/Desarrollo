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
    public class AccessosClientesController : ApiController
    {
        

        // GET: api/accessos_clientes
        public IQueryable<accessos_clientes> Getaccessos_clientes()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.accessos_clientes;
        }

        // GET: api/accessos_clientes/5
        [ResponseType(typeof(accessos_clientes))]
        public IHttpActionResult Getaccessos_clientes(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            accessos_clientes accessos_clientes = db.accessos_clientes.Find(id);
            if (accessos_clientes == null)
            {
                return NotFound();
            }

            return Ok(accessos_clientes);
        }

        //// PUT: api/accessos_clientes/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putaccessos_clientes(long id, accessos_clientes accessos_clientes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != accessos_clientes.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(accessos_clientes).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!accessos_clientesExists(id))
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

        //// POST: api/accessos_clientes
        //[ResponseType(typeof(accessos_clientes))]
        //public IHttpActionResult Postaccessos_clientes(accessos_clientes accessos_clientes)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.accessos_clientes.Add(accessos_clientes);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = accessos_clientes.id }, accessos_clientes);
        //}

        //// DELETE: api/accessos_clientes/5
        //[ResponseType(typeof(accessos_clientes))]
        //public IHttpActionResult Deleteaccessos_clientes(long id)
        //{
        //    accessos_clientes accessos_clientes = db.accessos_clientes.Find(id);
        //    if (accessos_clientes == null)
        //    {
        //        return NotFound();
        //    }

        //    db.accessos_clientes.Remove(accessos_clientes);
        //    db.SaveChanges();

        //    return Ok(accessos_clientes);
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

        private bool accessos_clientesExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.accessos_clientes.Count(e => e.id == id) > 0;
        }
    }
}