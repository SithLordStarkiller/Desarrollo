using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;
using System.Collections.Generic;
using proEntidades;
using proUtilerias;
using SICOGUA.Entidades;

namespace SICOGUA.Datos
{
    public class clsDatCatalogos
    {
        #region Pase de Lista generica
        public static DataTable consultaHorarioReaLista(int idServicio, int idInstalacion, DateTime fecha, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();


            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spConsultarHorarioSerInsREALista";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", idInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fecha", fecha);
                dbComando.ExecuteNonQuery();

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        #endregion
        #region anexo tecnico
        public static DataTable consultaTurnoXTipoHorario(string procedimientoAlmacenado, string strTipoHorario, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.String;
                dbParamClave.ParameterName = "@thTipoHorario";
                dbParamClave.Value = strTipoHorario == "" ? (object)DBNull.Value : strTipoHorario;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
        public static DataTable consultaTipoHorario(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
        
        #endregion anexo tecnico
        #region tipoAsistencia
        public static DataTable consultarTipoAsignacion(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
        #endregion tipoAsistencia


        public static DataTable consultaHorarioRea(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spuConsultarHorarioREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", idInstalacion);
                dbComando.ExecuteNonQuery();

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
        public static DataTable consultaCatalogoZona(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoAgrupamiento(string procedimientoAlmacenado, int idZona, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idZona";
                dbParamClave.Value = idZona;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoCompania(string procedimientoAlmacenado, int idAgrupamiento, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idAgrupamiento";
                dbParamClave.Value = idAgrupamiento;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoSeccion(string procedimientoAlmacenado, int idCompania, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idCompania";
                dbParamClave.Value = idCompania;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoPeloton(string procedimientoAlmacenado, int idSeccion, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idSeccion";
                dbParamClave.Value = idSeccion;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoTipoServicio(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoServicio(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoServicioPorTipoServicio(string procedimientoAlmacenado, int idTipoServicio, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbIdTipoServicio = objConexion.dbProvider.CreateParameter();

                dbIdTipoServicio.DbType = DbType.Int32;
                dbIdTipoServicio.ParameterName = "@idTipoServicio";
                dbIdTipoServicio.Value = idTipoServicio == 0 ? (object)DBNull.Value : idTipoServicio;
                dbComando.Parameters.Add(dbIdTipoServicio);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoServicioPorUsuario(string procedimientoAlmacenado, int idUsuario, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;
                //
                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idUsuario";
                dbParamClave.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbParamClave);
                //
                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
        public static DataTable consultaCatalogoEstatusREA(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoServicioPorUsuarioyTipoServicio(string procedimientoAlmacenado, int idTipoServicio, int idUsuario, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbIdTipoServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdUsuario = objConexion.dbProvider.CreateParameter();

                dbIdTipoServicio.DbType = DbType.Int32;
                dbIdTipoServicio.ParameterName = "@idTipoServicio";
                dbIdTipoServicio.Value = idTipoServicio == 0 ? (object)DBNull.Value : idTipoServicio;
                dbComando.Parameters.Add(dbIdTipoServicio);

                dbIdUsuario.DbType = DbType.Int32;
                dbIdUsuario.ParameterName = "@idUsuario";
                dbIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbIdUsuario);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoInstalacion(string procedimientoAlmacenado, int idServicio, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idServicio";
                dbParamClave.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoInstalacionPorUsuario(string procedimientoAlmacenado, int idServicio, int idUsuario, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdUsuario = objConexion.dbProvider.CreateParameter();

                dbIdServicio.DbType = DbType.Int32;
                dbIdServicio.ParameterName = "@idServicio";
                dbIdServicio.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbIdServicio);

                dbIdUsuario.DbType = DbType.Int32;
                dbIdUsuario.ParameterName = "@idUsuario";
                dbIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbIdUsuario);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
        public static DataTable consultarHorarioSerIns(string procedimientoAlmacenado, int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdUsuario = objConexion.dbProvider.CreateParameter();

                dbIdServicio.DbType = DbType.Int32;
                dbIdServicio.ParameterName = "@idServicio";
                dbIdServicio.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbIdServicio);

                dbIdUsuario.DbType = DbType.Int32;
                dbIdUsuario.ParameterName = "@idInstalacion";
                dbIdUsuario.Value = idInstalacion == 0 ? (object)DBNull.Value : idInstalacion;
                dbComando.Parameters.Add(dbIdUsuario);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoInstalacionPorUsuarioyZona(string procedimientoAlmacenado, int idZona, int idServicio, int idUsuario, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbIdZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdUsuario = objConexion.dbProvider.CreateParameter();

                dbIdZona.DbType = DbType.Int16;
                dbIdZona.ParameterName = "@idZona";
                dbIdZona.Value = idZona == 0 ? (object)DBNull.Value : idZona;
                dbComando.Parameters.Add(dbIdZona);

                dbIdServicio.DbType = DbType.Int32;
                dbIdServicio.ParameterName = "@idServicio";
                dbIdServicio.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbIdServicio);

                dbIdUsuario.DbType = DbType.Int32;
                dbIdUsuario.ParameterName = "@idUsuario";
                dbIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbIdUsuario);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoInstalacionesPorServicioZona(string procedimientoAlmacenado, int idZona, int idServicio, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbIdZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdUsuario = objConexion.dbProvider.CreateParameter();

                dbIdZona.DbType = DbType.Int16;
                dbIdZona.ParameterName = "@idZona";
                dbIdZona.Value = idZona == 0 ? (object)DBNull.Value : idZona;
                dbComando.Parameters.Add(dbIdZona);

                dbIdServicio.DbType = DbType.Int32;
                dbIdServicio.ParameterName = "@idServicio";
                dbIdServicio.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbIdServicio);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogo(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogo(string procedimientoAlmacenado, Dictionary<string, clsEntParameter> parameters, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                foreach (var parameter in parameters)
                {
                    string parameterName = parameter.Key;
                    clsEntParameter parameterValue = parameter.Value;
                    DbParameter dbParameter = objConexion.dbProvider.CreateParameter();

                    dbParameter.DbType = parameterValue.Type;
                    dbParameter.ParameterName = parameterName;
                    dbParameter.Value = parameterValue.Value;
                    dbComando.Parameters.Add(dbParameter);
                }

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoEmpleadoJerarquiaZona(string procedimientoAlmacenado, Int16 idJerarquia, Int16 idZona, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                DbParameter dbIdJerarquia = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdZona = objConexion.dbProvider.CreateParameter();

                dbIdJerarquia.DbType = DbType.Int16;
                dbIdJerarquia.ParameterName = "@idJerarquia";
                dbIdJerarquia.Value = idJerarquia == 0 ? (object)DBNull.Value : idJerarquia;
                dbComando.Parameters.Add(dbIdJerarquia);

                dbIdZona.DbType = DbType.Int16;
                dbIdZona.ParameterName = "@idZona";
                dbIdZona.Value = idZona == 0 ? (object)DBNull.Value : idZona;
                dbComando.Parameters.Add(dbIdZona);

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoEmpleadoJerarquiaZonaServicio(string procedimientoAlmacenado, Int16 idJerarquia, Int16 idZona, Int32 idServicio, Int32 idInstalacion, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                DbParameter dbIdJerarquia = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdInstalacion = objConexion.dbProvider.CreateParameter();

                dbIdJerarquia.DbType = DbType.Int16;
                dbIdJerarquia.ParameterName = "@idJerarquia";
                dbIdJerarquia.Value = idJerarquia == 0 ? (object)DBNull.Value : idJerarquia;
                dbComando.Parameters.Add(dbIdJerarquia);

                dbIdZona.DbType = DbType.Int16;
                dbIdZona.ParameterName = "@idZona";
                dbIdZona.Value = idZona == 0 ? (object)DBNull.Value : idZona;
                dbComando.Parameters.Add(dbIdZona);

                dbIdServicio.DbType = DbType.Int32;
                dbIdServicio.ParameterName = "@idServicio";
                dbIdServicio.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbIdServicio);

                dbIdInstalacion.DbType = DbType.Int32;
                dbIdInstalacion.ParameterName = "@idInstalacion";
                dbIdInstalacion.Value = idInstalacion == 0 ? (object)DBNull.Value : idInstalacion;
                dbComando.Parameters.Add(dbIdInstalacion);

                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoJerarquia(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoPuesto(string procedimientoAlmacenado, int idJerarquia, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamClave = objConexion.dbProvider.CreateParameter();

                dbParamClave.DbType = DbType.Int32;
                dbParamClave.ParameterName = "@idJerarquia";
                dbParamClave.Value = idJerarquia;
                dbComando.Parameters.Add(dbParamClave);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }

        public static DataTable consultaCatalogoTipoHorario(string procedimientoAlmacenado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }




        public static DataTable consultaMunicipio(string procedimientoAlmacenado, int idEstado, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamIdEstado = objConexion.dbProvider.CreateParameter();

                dbParamIdEstado.DbType = DbType.Int32;
                dbParamIdEstado.ParameterName = "@idEstado";
                dbParamIdEstado.Value = idEstado;
                dbComando.Parameters.Add(dbParamIdEstado);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }




        public static DataTable consultaAsentamiento(string procedimientoAlmacenado, int idEstado, int idMunicipio, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                dbComando.CommandText = procedimientoAlmacenado;

                DbParameter dbParamIdEstado = objConexion.dbProvider.CreateParameter();
                DbParameter dbParamIdMunicipio = objConexion.dbProvider.CreateParameter();

                dbParamIdEstado.DbType = DbType.Int32;
                dbParamIdEstado.ParameterName = "@idEstado";
                dbParamIdEstado.Value = idEstado;
                dbComando.Parameters.Add(dbParamIdEstado);

                dbParamIdMunicipio.DbType = DbType.Int32;
                dbParamIdMunicipio.ParameterName = "@idMunicipio";
                dbParamIdMunicipio.Value = idMunicipio;
                dbComando.Parameters.Add(dbParamIdMunicipio);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }



        public static void consultarCodigoPostal(int idAsentamiento, ref DataSet aseCodigo, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spbuscarCodigoPostal";
                dbComando.Parameters.Clear();

                DbParameter dbpIdAsentamiento = objConexion.dbProvider.CreateParameter();

                dbpIdAsentamiento.DbType = DbType.Int32;
                dbpIdAsentamiento.Direction = ParameterDirection.Input;
                dbpIdAsentamiento.ParameterName = "@idAsentamiento";
                dbpIdAsentamiento.Value = idAsentamiento;
                dbComando.Parameters.Add(dbpIdAsentamiento);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(aseCodigo);

                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
        }

        public static string consultarFechaCorte(int idMes, int idAnio, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            string strRegresa = "";
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "historico.spuFechaCorteSustantivos";
                dbComando.Parameters.Clear();

                DbParameter pdbIdMes = objConexion.dbProvider.CreateParameter();
                DbParameter pdbIdAnio = objConexion.dbProvider.CreateParameter();
                pdbIdMes.DbType = DbType.Int32;
                pdbIdMes.Direction = ParameterDirection.Input;
                pdbIdMes.ParameterName = "@idMes";
                pdbIdMes.Value = idMes;
                dbComando.Parameters.Add(pdbIdMes);

                pdbIdAnio.DbType = DbType.Int32;
                pdbIdAnio.Direction = ParameterDirection.Input;
                pdbIdAnio.ParameterName = "@idAnio";
                pdbIdAnio.Value = idAnio;
                dbComando.Parameters.Add(pdbIdAnio);

                dbAdapter.SelectCommand = dbComando;
                DataTable dtResultado = new DataTable();
                dbAdapter.Fill(dtResultado);
                if (dtResultado != null && dtResultado.Rows.Count > 0)
                {
                    strRegresa = dtResultado.Rows[0][0].ToString();
                }

                dbTrans.Commit();

            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return strRegresa;
        }

        public static DataTable consultarAutoriza(clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spuBuscarPersonalAutoriza";

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.SelectCommand = dbComando;

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idActInacTod", 1);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@paterno", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@materno", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@nombre", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaNac", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAlta", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaBaja", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@cartilla", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Byte, "@loc", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Byte, "@cursoBasico", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@numEmpleado", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@cuip", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@rfc", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@curp", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idServicio", (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", (object)DBNull.Value);
                
                dbComando.ExecuteNonQuery();
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }


        public static DataTable consultaCatalogoFirmantesPorZona(int idServicio, int idInstalacion, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spConsultarFirmanteOficioAsignacion";
                dbComando.Parameters.Clear();

                DbParameter dbIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdInstalacion = objConexion.dbProvider.CreateParameter();

                dbIdServicio.DbType = DbType.Int16;
                dbIdServicio.ParameterName = "@idServicio";
                dbIdServicio.Value = idServicio == 0 ? (object)DBNull.Value : idServicio;
                dbComando.Parameters.Add(dbIdServicio);

                dbIdInstalacion.DbType = DbType.Int16;
                dbIdInstalacion.ParameterName = "@idInstalacion";
                dbIdInstalacion.Value = idInstalacion == 0 ? (object)DBNull.Value : idInstalacion;
                dbComando.Parameters.Add(dbIdInstalacion);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
                dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    throw dbExRoll;
                }
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return dTable;
        }
    }
}
