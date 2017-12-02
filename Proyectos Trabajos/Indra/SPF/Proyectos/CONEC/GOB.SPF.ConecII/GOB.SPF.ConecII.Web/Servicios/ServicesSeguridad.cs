using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GOB.SPF.ConecII.Interfaces.Genericos;
using GOB.SPF.ConecII.Web.Models.Generico;
using GOB.SPF.ConecII.Web.Models.Seguridad;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesSeguridad
    {
        #region RolModulos
        /// <summary>
        /// Obtiene todas la relaciones de roles con los modulos padre
        /// </summary>
        /// <returns>Regresa lista de roles modulos</returns>
        public async Task<IEnumerable<UiRolModulo>> RolesModulosObtenerTodos()
        {
            return await ServiceClient.GetObjectAsync<IEnumerable<UiRolModulo>>("Seguridad", "RolesModulosObtenerTodos");
        }

        /// <summary>
        /// Obtiene todos las relaciones de modulos con los roles que cumplen con los filtros ingresados
        /// </summary>
        /// <param name="peticion">Aqui van los parametros a filtrar
        /// peticion.Solicitud.Rol.Id => Es el id del rol (opcional)
        /// peticion.Solicitud.Rol.IdArea =>Es el id del area (opcional)
        /// peticion.Solicitud.Modulo.Id =>Es el id del módulo (opcional)
        /// peticion.Solicitud.Modulo.SubModulos.Id => Es el id del submodulo (opcional)
        /// peticion.Solicitud.Activo => Es el estatus del rol modulo
        /// peticion.Paginado.All => Indica si quieres todos o paginado los modulos
        /// peticion.Paginado.Pages => Indica la pagina que quieres obtener
        /// peticion.Paginado.Rows => Indica la cantidad de elementos por pagina
        /// </param>
        /// <returns>Regresa una lista de Roles Modulos</returns>
        public async Task<IEnumerable<UiRolModulo>> ObtenerModulosPorCriterio(IPeticion<UiRolModulo> peticion)
        {
            var result = await ServiceClient.PostObjectAsync<IPeticion<UiRolModulo>, UiRespuesta<List<UiRolModulo>>>("Seguridad",
                "ObtenerModulosPorCriterio",
                peticion);

            return result.Resultado;
        }
        #endregion
    }
}