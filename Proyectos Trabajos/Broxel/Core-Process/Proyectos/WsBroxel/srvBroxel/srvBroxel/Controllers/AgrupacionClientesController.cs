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
    public class AgrupacionClientesController : ApiController
    {
        //private broxelco_rdgEntities db = new broxelco_rdgEntities();

        // GET: api/AgrupacionClientes
        public IQueryable<AgrupacionClientes> GetAgrupacionClientes()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.AgrupacionClientes;
        }

        // GET: api/AgrupacionClientes/5
        [ResponseType(typeof(AgrupacionClientes))]
        public IHttpActionResult GetAgrupacionClientes(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            AgrupacionClientes agrupacionClientes = db.AgrupacionClientes.Find(id);
            if (agrupacionClientes == null)
            {
                return NotFound();
            }

            return Ok(agrupacionClientes);
        }

        // PUT: api/AgrupacionClientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgrupacionClientes(long id, AgrupacionClientes agrupacionClientes)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agrupacionClientes.id)
            {
                return BadRequest();
            }

            db.Entry(agrupacionClientes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgrupacionClientesExists(id))
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

        // POST: api/AgrupacionClientes
        [ResponseType(typeof(AgrupacionClientes))]
        public IHttpActionResult PostAgrupacionClientes(AgrupacionClientes agrupacionClientes)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AgrupacionClientes.Add(agrupacionClientes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agrupacionClientes.id }, agrupacionClientes);
        }

        // DELETE: api/AgrupacionClientes/5
        [ResponseType(typeof(AgrupacionClientes))]
        public IHttpActionResult DeleteAgrupacionClientes(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            AgrupacionClientes agrupacionClientes = db.AgrupacionClientes.Find(id);
            if (agrupacionClientes == null)
            {
                return NotFound();
            }

            db.AgrupacionClientes.Remove(agrupacionClientes);
            db.SaveChanges();

            return Ok(agrupacionClientes);
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

        private bool AgrupacionClientesExists(long id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.AgrupacionClientes.Count(e => e.id == id) > 0;
        }
    }
}