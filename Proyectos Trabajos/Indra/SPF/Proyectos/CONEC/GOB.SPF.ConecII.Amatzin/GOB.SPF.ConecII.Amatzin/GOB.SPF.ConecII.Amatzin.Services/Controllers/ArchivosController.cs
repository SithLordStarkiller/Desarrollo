using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GOB.SPF.ConecII.Amatzin.Entities;
using GOB.SPF.ConecII.Amatzin.Entities.Request;
using GOB.SPF.ConecII.Amatzin.Business;
namespace GOB.SPF.ConecII.Amatzin.Services.Controllers
{
    [RoutePrefix("api/Archivos")]
    public class ArchivosController : ApiController
    {
        [Route("RegistrarArchivo")]
        [HttpPost]
        [AllowAnonymous]
        public Result<Archivo> PostInsertaArchivo([FromBody] Archivo item)
        {
            return new Archivos().Insertar(item);
        }

        [Route("ConsultarHistorico")]
        [HttpGet]
        [AllowAnonymous]
        public Result<Archivo> PostConsultarHistoricoPorId(long id)
        {
            return new Archivos().ConsultarHistoricoPorId(id);
        }

        [Route("ConsultarArchivo")]
        [HttpGet]
        [AllowAnonymous]
        public Result<Archivo> PostObtenerPorId( long id)
        {
            return new Archivos().ObtenerPorId(id);
        }
    }
}
