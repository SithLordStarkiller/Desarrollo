using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GOB.SPF.Conec.Services.Controllers
{
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        /// <summary>
        /// Guarda un rol en base de datos
        /// </summary>
        /// <param name="rol">Es el rol a guardar, debe tener id=0 para que se inserte como nuevo</param>
        /// <returns>Regresa 0=> si ya existe un rol con ese nombre, 1=>exitoso, -1=> error</returns>
        [HttpPost]
        [Route("GuardarRol")]
        public IHttpActionResult GuardarRol([FromBody]Rol rol)
        {
            try
            {
                return Ok(new RolBusiness().Guardar(rol));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtiene un rol por el id
        /// </summary>
        /// <param name="id">Es el id del rol a obtener</param>
        /// <returns>Regresa un rol</returns>
        [AcceptVerbs("GET")]
        [Route("ObtenerPorId")]
        public IHttpActionResult ObtenerPorId(long id)
        {
            try
            {
                return Ok(new RolBusiness().ObtenerPorId(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Guarda un rol en base de datos
        /// </summary>
        /// <param name="rol">Es el rol a actualizar debe tener el id!=0 si no no se actualiza</param>
        /// <returns>Regresa 0=> si ya existe un rol con ese nombre, 1=>exitoso, -1=> error</returns>
        [HttpPut]
        [AcceptVerbs("PUT")]
        [Route("ActualizarRol")]
        public IHttpActionResult ActualizarRol([FromBody]Rol rol)
        {
            try
            {
                return Ok(new RolBusiness().Guardar(rol));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtiene un rol por su nombre
        /// </summary>
        /// <param name="roleName">Es el nombre del rol a obtener</param>
        /// <returns>Regresa un rol</returns>
        [AcceptVerbs("GET")]
        [Route("ObtenerPorNombre")]
        public IHttpActionResult ObtenerPorNombre(string roleName)
        {
            try
            {
                return Ok(new RolBusiness().ObtenerPorNombre(roleName));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Regresa una lista de roles filtrando por el tipo de usuario
        /// </summary>
        /// <param name="usuarioExterno">Recibe un booleano indicando si se filtrara por usuario externos o internos</param>
        /// <param name="rol">Recibe un rol con la informacion a filtrar</param>
        /// <returns>Regresa una lista de roles</returns>
        [HttpPost]
        [AcceptVerbs("POST")]
        [Route("ObtenerPorTipoUsuario")]
        public IHttpActionResult ObtenerPorTipoUsuario([FromBody]Rol rol, [FromUri]bool usuarioExterno)
        {
            try
            {
                return Ok(new RolBusiness().ObtenerRolesPorTipoUsuario(rol, usuarioExterno));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Cambia el estatus de un rol
        /// </summary>
        /// <param name="rol">Recibe el objeto rol al cual se le cambiara el estatus</param>
        /// <returns>Regresa un 1=> si se actualizo correctamente, 0=> si el rol esta asignado a usuarios activos, -1=> si hubo error al actualizar</returns>
        [HttpPost]
        [AcceptVerbs("POST")]
        [Route("CambiarEstatusRol")]
        public IHttpActionResult CambiarEstatusRol([FromBody]Rol rol)
        {
            try
            {
                return Ok(new RolBusiness().CambiarEstatus(rol));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}