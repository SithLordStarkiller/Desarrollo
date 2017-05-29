using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using srvBroxel.DAL;

namespace srvBroxel.Controllers
{
    public class maquilasController : ApiController
    {
        

        // GET: api/maquilas
        public IQueryable<maquila> Getmaquila()
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.maquila;
        }

        // GET: api/maquilas/5
        [ResponseType(typeof(maquila))]
        public IHttpActionResult Getmaquila(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            maquila maquila = db.maquila.Find(id);
            if (maquila == null)
            {
                return NotFound();
            }

            return Ok(maquila);
        }

        // PUT: api/maquilas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmaquila(int id, maquila maquila)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maquila.id)
            {
                return BadRequest();
            }

            db.Entry(maquila).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException entityValidationException)
            {
                foreach (var validationErrors in entityValidationException.EntityValidationErrors)
                {
                    var exceptionText = new StringBuilder();
                    exceptionText.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", validationErrors.Entry.Entity.GetType().Name, validationErrors.Entry.State);
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        exceptionText.AppendFormat("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                    ErrorHandling.EscribeError(exceptionText.ToString());
                }               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!maquilaExists(id))
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

        // POST: api/maquilas
        [ResponseType(typeof(maquila))]
        public IHttpActionResult Postmaquila(maquila maquila)
        {
             
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.maquila.Add(maquila);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = maquila.id }, maquila);
        }

        //// DELETE: api/maquilas/5
        //[ResponseType(typeof(maquila))]
        //public IHttpActionResult Deletemaquila(int id)
        //{
        //    broxelco_rdgEntities db = new broxelco_rdgEntities();

        //    maquila maquila = db.maquila.Find(id);
        //    if (maquila == null)
        //    {
        //        return NotFound();
        //    }

        //    db.maquila.Remove(maquila);
        //    db.SaveChanges();

        //    return Ok(maquila);
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

        private bool maquilaExists(int id)
        {
            broxelco_rdgEntities db = new broxelco_rdgEntities();

            return db.maquila.Count(e => e.id == id) > 0;
        }
    }
}