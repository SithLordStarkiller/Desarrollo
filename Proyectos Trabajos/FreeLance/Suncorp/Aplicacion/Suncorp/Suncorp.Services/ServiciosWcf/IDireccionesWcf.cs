namespace Suncorp.Services.ServiciosWcf
{
    using Models;
    using System.Collections.Generic;
    using System.ServiceModel;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDireccionesWcf" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDireccionesWcf
    {
        #region Operaciones CRUD DirCatEstados

        /// <summary>
        /// Obtiene una lista de la tabla de DirDirecciones
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        [OperationContract]
        List<DirCatEstados> ObtenerTablaDirCatEstado();

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatEstados
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un IdEstado)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        [OperationContract]
        DirCatEstados InsertarDirCatEstado(DirCatEstados model);

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatEstados
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        [OperationContract]
        bool ActualizarDirCatEstado(DirCatEstados model);

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatEstados
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        [OperationContract]
        bool EliminarDirCatEstado(DirCatEstados model);

        #endregion

        #region Operaciones DirCatEstados
        #endregion
    }
}
