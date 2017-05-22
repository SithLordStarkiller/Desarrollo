namespace Suncorp.Services.ServiciosWcf
{
    using Models;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract]
    public interface IDireccionesWcf
    {
        #region DirCatEstados

        #region Crud

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

        #region Operaciones
        #endregion

        #endregion

        #region DirCatMunicipios

        #region Crud

        /// <summary>
        /// Obtiene una lista de la tabla de DirCatMunicipios
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        [OperationContract]
        List<DirCatMunicipios> ObtenerTablaDirCatMunicipios();

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatMunicipios
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un IdEstado)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        [OperationContract]
        DirCatMunicipios InsertarDirCatMunicipios(DirCatMunicipios model);

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatMunicipios
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        [OperationContract]
        bool ActualizarDirCatMunicipios(DirCatMunicipios model);

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatMunicipios
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        [OperationContract]
        bool EliminarDirCatMunicipios(DirCatMunicipios model);

        #endregion

        #region Operaciones
        #endregion

        #endregion

        #region DirCatColonias

        #region Crud

        /// <summary>
        /// Obtiene una lista de la tabla de DirCatColonias
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        [OperationContract]
        List<DirCatColonias> ObtenerTablaDirCatColonias();

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatMunicipios
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        [OperationContract]
        DirCatColonias InsertarDirCatColonias(DirCatColonias model);

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatColonias
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        [OperationContract]
        bool ActualizarDirCatColonias(DirCatColonias model);

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        [OperationContract]
        bool EliminarDirCatColonias(DirCatColonias model);

        #endregion

        #region Operaciones
        #endregion

        #endregion

        #region DirDirecciones

        #region Crud

        /// <summary>
        /// Obtiene una lista de la tabla de DirDirecciones
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        [OperationContract]
        List<DirDirecciones> ObtenerTablaDirDirecciones();

        /// <summary>
        /// Metodo encargado de insertar un registro en DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirDirecciones para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        [OperationContract]
        DirDirecciones InsertarDirDirecciones(DirDirecciones model);

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatColonias
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        [OperationContract]
        bool ActualizarDirDirecciones(DirDirecciones model);

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        [OperationContract]
        bool EliminarDirDirecciones(DirDirecciones model);

        #endregion

        #region Operaciones
        #endregion

        #endregion
    }
}
