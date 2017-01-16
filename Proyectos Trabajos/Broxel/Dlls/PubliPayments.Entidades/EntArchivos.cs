using PubliPayments.Utiles;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades
{
    public class EntArchivos
    {
        /// <summary>
        /// Obtiene lista de archivos cargados
        /// </summary>
        /// <param name="idUsuario">id de usuario</param>
        /// <param name="idProceso">identificador que define el proceso en el sp</param>
        /// <returns>DataSet con los archivos</returns>
        public DataSet ObtieneArchivos(int idUsuario, int idProceso)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@PAUSUARIO", SqlDbType.Int) { Value = idUsuario };
                parametros[1] = new SqlParameter("@PAPROCESO", SqlDbType.Int) { Value = idProceso };
                return instancia.EjecutarDataSet("SqlDefault", "ObtieneArchivos", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en ObtenerArchivosConError: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Inserta o actualiza el registro del archivo que se esá cargando
        /// </summary>
        /// <param name="idArchivo">Id del archivo</param>
        /// <param name="nombreArchivo">Nombre del archivo</param>
        /// <param name="estatusArchivo">Estatus del archivo</param>
        /// <param name="registros">Numero de registros que contiene el archivo</param>
        /// <param name="extensionArchivo">Extención del archivo</param>
        /// <param name="tipoArchivo">ID del tipo de archivo</param>
        /// <param name="idUsuario">Ud del usuario que está cargando el archivo</param>
        /// <returns>DataSet con el resultado de insercion</returns>
        public DataSet InsUpsArchivos(int idArchivo, string nombreArchivo, string estatusArchivo, int registros, string extensionArchivo, int tipoArchivo, int idUsuario)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[7];
                parametros[0] = new SqlParameter("@PANOMBREARCHIVO", SqlDbType.VarChar) { Value = nombreArchivo };
                parametros[1] = new SqlParameter("@PAESTATUSARCHIVO", SqlDbType.VarChar) { Value = estatusArchivo };
                parametros[2] = new SqlParameter("@PAREGISTROS", SqlDbType.Int) { Value = registros };
                parametros[3] = new SqlParameter("@PAEXTARCHIVO", SqlDbType.VarChar) { Value = extensionArchivo };
                parametros[4] = new SqlParameter("@PATIPOARCHIVO", SqlDbType.Int) { Value = tipoArchivo };
                parametros[5] = new SqlParameter("@PAIDUSUARIO", SqlDbType.Int) { Value = idUsuario };
                parametros[6] = new SqlParameter("@PAIDARCHIVO", SqlDbType.Int) { Value = idArchivo };
                
                return instancia.EjecutarDataSet("SqlDefault", "InsUpsArchivos", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en InsUpsArchivos: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Inserta error del archivo
        /// </summary>
        /// <param name="idArchivo">Id del archivo</param>
        /// <param name="error">Cadena con el error</param>
        /// <returns></returns>
        public DataSet InsArchivosError(int idArchivo, string error)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@PAIDARCHIVO", SqlDbType.Int) { Value = idArchivo };
                parametros[1] = new SqlParameter("@PAERROR", SqlDbType.VarChar) { Value = error };
                return instancia.EjecutarDataSet("SqlDefault", "InsArchivosError", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en InsArchivosError: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Inserta creditos del archivo de asignación
        /// </summary>
        /// <param name="idArchivo">Id del archivo de pagos</param>
        /// <param name="credito">Número de crédito</param>
        /// <param name="campos">Nombres de las columnas de la tabla de pagos</param>
        /// <param name="valores">Valores de los campos que se van a insertar</param>
        /// <param name="tipo">Tipo de archivo 5 = Pagos </param>
        /// <returns>Dataset con respuesta de inserción</returns>
        public DataSet InsUpdLondon(int idArchivo, string credito, string campos, string valores, int tipo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@idArchivo", SqlDbType.Int) { Value = idArchivo };
                parametros[1] = new SqlParameter("@Credito", SqlDbType.VarChar) { Value = credito };
                parametros[2] = new SqlParameter("@Campos", SqlDbType.VarChar) { Value = campos };
                parametros[3] = new SqlParameter("@Valores", SqlDbType.VarChar) { Value = valores };
                parametros[4] = new SqlParameter("@Tipo", SqlDbType.Int) { Value = tipo };
                return instancia.EjecutarDataSet("SqlDefault", "InsUpdLondon", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en InsUpdLondon: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Inserta pagos a los creditos de London
        /// </summary>
        /// <param name="idArchivo">Id del archivo de pagos</param>
        /// <param name="credito">The tx_pagocredito.</param>
        /// <param name="estatusPago">The estatus pago.</param>
        /// <param name="noPago">The no pago.</param>
        /// <param name="montoPago">The monto pago.</param>
        /// <param name="txPago">The tx pago.</param>
        /// <returns>
        /// Dataset con respuesta de inserción
        /// </returns>
        public DataSet InsUpdPagosLondon(int idArchivo, string credito, int estatusPago, int noPago, decimal montoPago, string txPago)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@PAIDARCHIVO", SqlDbType.Int) {Value = idArchivo};
                parametros[1] = new SqlParameter("@PACREDITO", SqlDbType.VarChar) {Value = credito};
                parametros[2] = new SqlParameter("@PAIESTATUSPAGO", SqlDbType.Int) { Value = estatusPago };
                parametros[3] = new SqlParameter("@PAINOPAGO", SqlDbType.Int) { Value = noPago };
                parametros[4] = new SqlParameter("@PADMONTOPAGO", SqlDbType.Decimal) { Value = montoPago };
                parametros[5] = new SqlParameter("@PACTXPAGO", SqlDbType.VarChar) { Value = txPago };
                return instancia.EjecutarDataSet("SqlDefault", "InsUpdPagosLondon", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en InsUpdPagosLondon: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Inserta credito de cc de los cuales no se tuvo exito
        /// </summary>
        /// <param name="idArchivo">Id del archivo que se carga</param>
        /// <param name="credito">Número de crédito</param>
        /// <param name="fechaLlamada">Fecha de cuando se realizó la llamada sin exito</param>
        /// <param name="telefono">Número de crédito</param>
        /// <param name="resultado">Fecha de cuando se realizó la llamada sin exito</param>
        /// <returns>DataSet con el resultado de inserción</returns>
        public DataSet InsUpdLLamadasSinExito(int idArchivo, string credito, string fechaLlamada, string telefono, int resultado)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@PAIDARCHIVO", SqlDbType.Int) {Value = idArchivo};
                parametros[1] = new SqlParameter("@PACREDITO", SqlDbType.VarChar, 15) {Value = credito};
                parametros[2] = new SqlParameter("@PAFECHALLAMADA", SqlDbType.DateTime) {Value = fechaLlamada};
                parametros[3] = new SqlParameter("@PATELEFONO", SqlDbType.VarChar, 12) { Value = telefono };
                parametros[4] = new SqlParameter("@PARESULTADO", SqlDbType.Int) { Value = resultado };
                return instancia.EjecutarDataSet("SqlDefault", "InsUpdLLamadasSinExito", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos","Error en InsUpdLLamadasSinExito: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Obtiene el catalogo de resultados de Llamadas sin exito
        /// </summary>
        /// <returns>DataSet con el catalogo</returns>
        public DataSet ObtieneResultadosLlamadas()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[0];
                return instancia.EjecutarDataSet("SqlDefault", "ObtieneResultadosLlamadas", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en ObtieneResultadosLlamadas: " + ex.Message);
                return new DataSet();
            }
        }

        /// <summary>
        /// Obtiene el catalogo de los estatus de los pagos London
        /// </summary>
        /// <returns>DataSet con el catalogo</returns>
        public DataSet ObtieneEstatusPago()
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[0];
                return instancia.EjecutarDataSet("SqlDefault", "ObtieneEstatusPago", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntArchivos", "Error en ObtieneEstatusPago: " + ex.Message);
                return new DataSet();
            }
        }
    }
}
