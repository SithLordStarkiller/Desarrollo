using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Business.Plantilla;
using GOB.SPF.ConecII.Entities.DTO;

namespace Cns.Sies.Rest.Mvc.Areas.Plantilla.Controllers
{
    public class InstitucionesController : ApiController
    {

        AdministraInstituciones mAdministraInstituciones;

        public InstitucionesController()
        {
            mAdministraInstituciones = new AdministraInstituciones();
        }


        // GET: api/Instituciones
        public IEnumerable<Instituciones> Get()
        {
            return mAdministraInstituciones.FindItemsStored();
        }


        [Route("api/Instituciones/Drop")]
        public IEnumerable<DropDto> GetDrop()
        {
            return mAdministraInstituciones.FindItemsDropStored();
        }


        [Route("api/Instituciones/Paged")]
        [HttpPost]
        public object GetPaged([FromBody]InstitucionesDtoBuscar find)
        {
            IEnumerable<Instituciones> items = mAdministraInstituciones.FindPagedStored(find);
            return new { items, find.PageTotalItems };
        }

        // GET: api/Instituciones/5
        [Route("api/Instituciones/GetById")]
        [ResponseType(typeof(Instituciones))]
        [HttpPost]
        public IHttpActionResult Get([FromBody]int id)
        {
            Instituciones instituciones = mAdministraInstituciones.FindById(id);
            if (instituciones == null)
            {
                return NotFound();
            }

            return Ok(instituciones);
        }

        // PUT: api/Instituciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Instituciones instituciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                mAdministraInstituciones.UpdateStored(instituciones);
            }
            catch (Exception ex)
            {
                if (!Exists((int)instituciones.IdInstitucion))
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

        // POST: api/Instituciones
        [ResponseType(typeof(Instituciones))]
        public IHttpActionResult Post(Instituciones instituciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mAdministraInstituciones.InsertStored(instituciones);

            return CreatedAtRoute("DefaultApi", new { id = instituciones.IdInstitucion }, instituciones);
        }

        // DELETE: api/Instituciones/5
        [ResponseType(typeof(Instituciones))]
        public IHttpActionResult Delete(int id)
        {
            Instituciones instituciones = mAdministraInstituciones.FindById(id);
            if (instituciones == null)
            {
                return NotFound();
            }

            mAdministraInstituciones.DeleteStored(instituciones);

            return Ok(instituciones);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        private bool Exists(int id)
        {
            Instituciones instituciones = mAdministraInstituciones.FindById(id);
            return instituciones.IdInstitucion != 0;
        }
    }
}