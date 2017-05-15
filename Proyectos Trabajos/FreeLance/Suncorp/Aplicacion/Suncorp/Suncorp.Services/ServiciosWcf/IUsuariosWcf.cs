namespace Suncorp.Services.ServiciosWcf
{
    using System.ServiceModel;
    using Models;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System;

    [ServiceContract]
    public interface IUsuariosWcf
    {
        #region Operaciones CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de Ususuarios
        /// </summary>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        [OperationContract]
        List<UsUsuarios> ObtenerTablaUsuario();

        /// <summary>
        /// Retorna un registro de UsUsuario por un criterio
        /// </summary>
        /// <param name="criterio">Criterio por el cual se va a filtrar el registro</param>
        /// <returns>Retorna un registro de UsUsuario</returns>
        [OperationContract]
        UsUsuarios ObteneRegistroUsuario(Expression<Func<UsUsuarios, bool>> criterio);

        /// <summary>
        /// Retorna una lista de registros de UsUsuario por un criterio
        /// </summary>
        /// <param name="criterio">Criterio por el cual se va a filtrar el registro</param>
        /// <returns>Retorna lista de registros de UsUsuario</returns>
        [OperationContract]
        List<UsUsuarios> ObteneRegistrosUsuarios(Expression<Func<UsUsuarios, bool>> criterio);

        /// <summary>
        /// Inserta en la tabla de usuarios
        /// </summary>
        /// <param name="model">Modelo UsUsuario con los campos que se van a modificar</param>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        [OperationContract]
        UsUsuarios InsertarUsuario(UsUsuarios model);

        /// <summary>
        /// Actualiza en la tabla de usuarios
        /// </summary>
        /// <param name="model">Campos que se van a modificar</param>
        /// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        [OperationContract]
        bool ActualizarUsuario(UsUsuarios model);

        /// <summary>
        /// Elimina en la tabla de usuarios
        /// </summary>
        /// <param name="model">Campos que se van a modificar</param>
        /// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        [OperationContract]
        bool EliminarUsuario(UsUsuarios model);

        #endregion

        #region Operaciones

        [OperationContract]
        UsUsuarios ObtenerUsuarioLogin(string usuario, string contrasena);
        
        #endregion
    }
}
