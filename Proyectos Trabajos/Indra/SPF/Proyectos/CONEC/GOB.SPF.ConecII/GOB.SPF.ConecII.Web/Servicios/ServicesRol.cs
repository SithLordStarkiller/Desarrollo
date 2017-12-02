using GOB.SPF.ConecII.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Web.Models.Seguridad;
using GOB.SPF.ConecII.Library;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Web.Models;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesRol : IRoleStore<UiRol, int> 
    {
        /// <summary>
        /// Crea un rol
        /// </summary>
        /// <param name="role">Es el rol a crear</param>
        /// <returns></returns>
        public async Task CreateAsync(UiRol role)
        {
            await ServiceClient.PostObjectAsync("Roles", "GuardarRol", role);
        }

        public Task DeleteAsync(UiRol role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene un rol por el id
        /// </summary>
        /// <param name="roleId">Es el id del rol a obtener</param>
        /// <returns>Regresa un rol</returns>
        public async Task<UiRol> FindByIdAsync(int roleId)
        {
            return await ServiceClient.GetObjectAsync<UiRol>("Roles", "ObtenerPorId", new[] { new KeyValuePair<string, string>("id", roleId.ToString()) });
        }

        /// <summary>
        /// Obtiene un rol por su nombre
        /// </summary>
        /// <param name="roleName">Es el nombre del rol a obtener</param>
        /// <returns>Regresa un rol</returns>
        public async Task<UiRol> FindByNameAsync(string roleName)
        {
            return await ServiceClient.GetObjectAsync<UiRol>("Roles","ObtenerPorNombre", new[] { new KeyValuePair<string, string>("roleName", roleName) });
        }

        /// <summary>
        /// Actualiza un rol
        /// </summary>
        /// <param name="role">Es el rol a actualizar</param>
        /// <returns></returns>
        public async Task UpdateAsync(UiRol role)
        {
            await ServiceClient.PutObjectAsync<UiRol>("Roles", "ActualizarRol", "", "", role);
        }

        /// <summary>
        /// Regresa una lista de roles filtrando por el tipo de usuario
        /// </summary>
        /// <param name="usuarioExterno">Recibe un booleano indicando si se filtrara por usuario externos o internos</param>
        /// <returns>Regresa una lista de roles</returns>
        public async Task<IEnumerable<UiRol>> ObtenerPorTipoUsuario(UiRol rol, bool usuarioExterno)
        {
            return await ServiceClient.PostObjectAsync< List < UiRol > ,UiRol >("Roles", "ObtenerPorTipoUsuario","usuarioExterno", usuarioExterno.To<string>(), rol);
        }
    
        /// <summary>
        /// Guarda un rol en base de datos
        /// </summary>
        /// <param name="role">Es el rol a guardar, si el id=0 se crea un nuevo rol, sino se actualiza</param>
        /// <returns></returns>
        public async Task<int> GuardarRol(UiRol role)
        {
            return await ServiceClient.PostObjectAsync<UiRol, int>("Roles", "GuardarRol", role);
        }

        /// <summary>
        /// Cambia el estatus de un rol
        /// </summary>
        /// <param name="role">Recibe el objeto rol al cual se le cambiara el estatus</param>
        /// <returns>Regresa un 1=> si se actualizo correctamente, 0=> si el rol esta asignado a usuarios activos, -1=> si hubo error al actualizar</returns>
        public async Task<int> CambiarEstatusRol(UiRol role)
        {
            return await ServiceClient.PostObjectAsync<UiRol, int>("Roles", "CambiarEstatusRol", role);
        }
    }
}