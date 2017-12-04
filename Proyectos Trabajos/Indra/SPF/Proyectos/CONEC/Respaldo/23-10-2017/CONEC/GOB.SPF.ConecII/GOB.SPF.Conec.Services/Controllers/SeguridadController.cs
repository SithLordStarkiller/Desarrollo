using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GOB.SPF.Conec.Services.Filters;
using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Genericos;
using GOB.SPF.ConecII.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GOB.SPF.Conec.Services.Controllers
{
    [RoutePrefix("api/Seguridad")]
    public class SeguridadController : ApiController
    {
        private const string privateKey = "sw6p!eg?3uwr2jufr*6eth@qE";
        private const string issuer = "GOB.SPF";
        private const string audience = "GOB.SPF.CONECII";
        private static SymmetricSecurityKey sign = new SymmetricSecurityKey(Encoding.Default.GetBytes(privateKey));
        private static SigningCredentials credentials = new SigningCredentials(sign, SecurityAlgorithms.HmacSha256);
        private static JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
        private static JwtSecurityToken _token;
        private static string _apiUrl;
        private static int _apiPort;

        private static JwtSecurityToken Token
        {
            get
            {
                if (_token == null || DateTime.UtcNow.AddSeconds(60) >= _token.ValidTo)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Issuer = issuer,
                        Audience = audience,
                        IssuedAt = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddMinutes(120),
                        SigningCredentials = credentials,
                    };
                    _token = securityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                }
                return _token;
            }
        }
        private static string _tokenString;
        private static string TokenString
        {
            get
            {
                if (string.IsNullOrEmpty(_tokenString) || DateTime.UtcNow.AddSeconds(60) >= _token.ValidTo)
                    _tokenString = securityTokenHandler.WriteToken(Token);
                return _tokenString;
            }
        }

        #region Modulos
        /// <summary>
        /// Obtiene un lista de modulos y submodulos con sus respectivo roles autorizados.
        /// </summary>
        /// <returns>IEnumerable de IModulo</returns>
        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("ModulosObtenerRolesAutorizados")]
        public IHttpActionResult ModulosObtenerRolesAutorizados()
        {
            try
            {
                var businessModulos = new ModuloBusiness();
                var modulos = businessModulos.ObtenerTodos();
                return Ok(modulos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtiene todos los modulos padres que existen en base de datos
        /// </summary>
        /// <returns>Regresa una lista de Modulos</returns>
        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("ObtenerModulosTodos")]
        public IHttpActionResult ModulosObtener()
        {
            try
            {
                return Ok(new ModuloBusiness().ObtenerTodos(new Paging() { All = true, Pages = 0, CurrentPage = 0, Rows = 0 }));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtiene todos los submodulos de primer nivel de un modulo
        /// </summary>
        /// <param name="idPadre">Es el id del modulo padre del que se quieren obtener su submodulos</param>
        /// <returns>Regresa una lista de Modulos</returns>
        [AcceptVerbs("GET")]
        [Route("ObtenerSubModulosPorIdPadre")]
        public IHttpActionResult ObtenerSubModulosPorIdPadre(int idPadre)
        {
            try
            {
                return Ok(new ModuloBusiness().ObtenerSubModulosPorIdPadre(idPadre));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Obtiene todos los modulos padre que cumplen con los filtros ingresados
        /// </summary>
        /// <param name="modulo">Aqui van los parametros a filtrar
        /// modulo.Solicitud.Id => Es el id del menu padre
        /// modulo.Solicitud.Submodulos.First().Id => es el id del submenu hijo (opcional)
        /// modulo.Solicitud.Activo => Es el estatus del menu padre
        /// modulo.Paginado.All => Indica si quieres todos o paginado los modulos
        /// modulo.Paginado.Pages => Indica la pagina que quieres obtener
        /// modulo.Paginado.Rows => Indica la cantidad de elemtos por pagina
        /// </param>
        /// <returns>Regresa una lista de Modulos</returns>
        [HttpPost]
        [AcceptVerbs("POST")]
        [Route("ObtenerModulosPorCriterio")]
        public IHttpActionResult ModulosObtenerPorCriterio([FromBody]ConecII.Entities.Genericos.Peticion<ConecII.Entities.Modulos.Modulo> modulo)
        {
            Respuesta<IEnumerable<IModulo>> result = new Respuesta<IEnumerable<IModulo>>();
            try
            {
                result.Resultado = new ModuloBusiness().ObtenerPorCriterio(modulo.Paginado, modulo.Solicitud);
                result.Paginado =
                    new Paging() { CurrentPage = modulo.Paginado.CurrentPage, Pages = modulo.Paginado.Pages };
                result.Exitoso = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un modulo especifico
        /// </summary>
        /// <param name="id">Es el id del modulo a obtener</param>
        /// <returns>Regresa un modulo</returns>
        [AcceptVerbs("GET")]
        [AllowAnonymous]
        [Route("ObtenerModulosPorId")]
        public IHttpActionResult ModuloObtenerPorId(int id)
        {
            try
            {
                return Ok(new ModuloBusiness().ObtenerPorId(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Inserta un nuevo modulo o actualiza uno existente
        /// </summary>
        /// <param name="modulo">Es el modulo a insertar o actualizar
        /// Si modulo.Id = 0, inserta el modulo
        /// Si modulo.Id != 0, actualiza el modulo
        /// </param>
        /// <returns>Regresa un valor entero, -1 hubo error, 1 exitoso</returns>
        [HttpPost]
        [Route("GuardarModulo")]
        [AcceptVerbs("POST")]
        [AllowAnonymous]
        public IHttpActionResult ModuloGuardar([FromBody]ConecII.Entities.Modulos.Modulo modulo)
        {
            try
            {
                return Ok(new ModuloBusiness().Guardar((modulo)));
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                return InternalServerError(e);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Cambia el estatus de un modulo
        /// </summary>
        /// <param name="modulo">Recibe el modulo a actualizar
        /// modulo.Id => es el id del modulo a actualizar
        /// modulo.Activo => tiene el estatus a actualizar
        /// </param>
        /// <returns>Regresa un entero, -1 => error, 1 => exito </returns>
        [HttpPost]
        [Route("CambiarEstatusModulo")]
        [AcceptVerbs("POST")]
        [AllowAnonymous]
        public IHttpActionResult ModuloCambiarEstatus([FromBody]ConecII.Entities.Modulos.Modulo modulo)
        {
            try
            {
                return Ok(new ModuloBusiness().CambiarEstatus(modulo));
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                return InternalServerError(e);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                return InternalServerError(e);
            }
        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        [AcceptVerbs("GET")]
        [Route("ObtenerToken")]
        public IHttpActionResult ObtenerToken()
        {
            return Ok(TokenString);
        }
    }
}