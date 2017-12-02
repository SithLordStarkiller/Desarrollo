using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GOB.SPF.ConecII.Interfaces;
using GOB.SPF.ConecII.Entities.Usuarios;
using GOB.SPF.Conec.Services.Filters;
using GOB.SPF.ConecII.Business;

namespace GOB.SPF.Conec.Services.Controllers
{
    [RoutePrefix("api/Usuarios")]
    public class UsuariosController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [AcceptVerbs("POST")]
        [Route("CrearUsuario")]
        public IHttpActionResult CrearUsuario([FromBody]NUsuario usuario)
        {
            try
            {
                var usuarioBusisness = new UsuarioBusiness();
                usuarioBusisness.Guardar(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("ObtenerUsuarioPorId")]
        public IHttpActionResult ObtenerUsuarioPorId(int id)
        {
            try
            {
                var usuarioBusisness = new UsuarioBusiness();
                var usuario = usuarioBusisness.ObtenerPorId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("ObtenerUsuarioPorLogin")]
        public IHttpActionResult ObtenerUsuarioPorLogin(string login)
        {
            try
            {
                var usuarioBusisness = new UsuarioBusiness();
                var usuario = usuarioBusisness.ObtenerPorLogin(login);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        [HttpPut]
        [ValidateModel]
        [AcceptVerbs("PUT")]
        [Route("ActualizarUsuario")]
        public IHttpActionResult ActualizarUsuario([FromBody]AUsuario usuario)
        {
            try
            {
                var usuarioBusisness = new UsuarioBusiness();
                usuarioBusisness.Guardar(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpDelete]
        public IHttpActionResult EliminarUsuario(int id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [ValidateModel]
        [AcceptVerbs("GET")]
        [Route("ObtenerClave")]
        public IHttpActionResult ObtenerClave(string login)
        {
            try
            {
                var repositoryClaves = new ClavesBusiness();
                var clave = repositoryClaves.ObtenerClave(login);
                return Ok(clave);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPut]
        [ValidateModel]
        [AcceptVerbs("PUT")]
        [Route("ActualizarClave")]
        public IHttpActionResult ActualizarClave([FromUri]string login, [FromBody]string clave)
        {
            try
            {
                var repositoryClaves = new ClavesBusiness();
                repositoryClaves.ActualizarClave(login, clave);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ValidateModel]
        [AcceptVerbs("POST")]
        [Route("ObtenerModulos")]
        public IHttpActionResult ObtenerModulos()
        {
            try
            {
                var moduloBusiness = new ModuloBusiness();
                var modulos = moduloBusiness.ObtenerTodos();
                return Ok(modulos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
