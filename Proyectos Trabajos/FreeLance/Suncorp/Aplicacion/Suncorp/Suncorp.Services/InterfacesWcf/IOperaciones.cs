namespace Suncorp.Services.ServiciosWcf
{
    using Models;
    using System.Collections.Generic;
    using System.ServiceModel;

    /// <summary>
    /// Interface que se encarga de exponer los servicios
    /// </summary>
    [ServiceContract]
    public interface IOperaciones
    {
        #region OpeCatZonas

        #region Crud

        /// <summary>
        /// Obtiene una lista de la tabla de OpeCatZonas
        /// </summary>
        /// <returns>Retorna una lista con losel modelo OpeCatZonas</returns>
        [OperationContract]
        List<OpeCatZonas> ObtenerTablaOpeCatZonas();

        /// <summary>
        /// Metodo encargado de insertar un registro en DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere un modelo de OpeCatZonas para insertar el registro</param>
        /// <returns>Retorna un modelo de OpeCatZonas Actualizado</returns>
        [OperationContract]
        OpeCatZonas InsertarOpeCatZonas(OpeCatZonas model);

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatColonias
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        [OperationContract]
        bool ActualizarOpeCatZonas(OpeCatZonas model);

        /// <summary>
        /// Metodo encargado de eliminar una un registro de OpeCatZonas
        /// </summary>
        /// <param name="model">Se requiere el modelo de OpeCatZonas a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        [OperationContract]
        bool EliminarOpeCatZonas(OpeCatZonas model);

        #endregion

        #region Operaciones
        #endregion

        #endregion

        #region OpeZonasPorEstados

        #region Crud

        /// <summary>
        /// Obtiene una lista de la tabla de OpeZonasPorEstados
        /// </summary>
        /// <returns>Retorna una lista con losel modelo OpeZonasPorEstados</returns>
        [OperationContract]
        List<OpeZonasPorEstados> ObtenerTablaOpeZonasPorEstados();

        /// <summary>
        /// Metodo encargado de insertar un registro en OpeZonasPorEstados
        /// </summary>
        /// <param name="model">Se requiere un modelo de OpeZonasPorEstados para insertar el registro</param>
        /// <returns>Retorna un modelo de OpeCatZonas Actualizado</returns>
        [OperationContract]
        OpeZonasPorEstados InsertarOpeZonasPorEstados(OpeZonasPorEstados model);

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatColonias
        /// </summary>
        /// <param name="model">Un modelo de OpeZonasPorEstados Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        [OperationContract]
        bool ActualizarOpeZonasPorEstados(OpeZonasPorEstados model);

        /// <summary>
        /// Metodo encargado de eliminar una un registro de OpeCatZonas
        /// </summary>
        /// <param name="model">Se requiere el modelo de OpeCatZonas a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        [OperationContract]
        bool EliminarOpeZonasPorEstados(OpeZonasPorEstados model);

        #endregion

        #region Operaciones
        #endregion

        #endregion
    }
}
