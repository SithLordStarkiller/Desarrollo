using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatTipoIncidencia
    {
        public static bool insertarTipoIncidencia(clsEntTipoIncidencia objIncidencia, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spInsertarTipoIncidencia";
                dbComando.Parameters.Clear();

                DbParameter dbpIncidencia = objConexion.dbProvider.CreateParameter();

                dbpIncidencia.DbType = DbType.String;
                dbpIncidencia.ParameterName = "@tiDescripcion";
                dbpIncidencia.Value = objIncidencia.TiDescripcion;
                dbComando.Parameters.Add(dbpIncidencia);

                dbComando.ExecuteNonQuery();

                dbTrans.Commit();

                return true;
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

        public static bool actualizarTipoIncidencia(clsEntTipoIncidencia objIncidencia, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spActualizarTipoIncidencia";
                dbComando.Parameters.Clear();

                DbParameter dbpIdIncidencia = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIncidencia = objConexion.dbProvider.CreateParameter();

                dbpIdIncidencia.DbType = DbType.Int16;
                dbpIdIncidencia.ParameterName = "@idTipoIncidencia";
                dbpIdIncidencia.Value = objIncidencia.IdTipoIncidencia;
                dbComando.Parameters.Add(dbpIdIncidencia);

                dbpIncidencia.DbType = DbType.String;
                dbpIncidencia.ParameterName = "@tiDescripcion";
                dbpIncidencia.Value = objIncidencia.TiDescripcion;
                dbComando.Parameters.Add(dbpIncidencia);

                dbComando.ExecuteNonQuery();

                dbTrans.Commit();

                return true;
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

        public static bool eliminarTipoIncidencia(clsEntTipoIncidencia objIncidencia, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "catalogo.spEliminarTipoIncidencia";
                dbComando.Parameters.Clear();

                DbParameter dbpIncidencia = objConexion.dbProvider.CreateParameter();

                dbpIncidencia.DbType = DbType.Int16;
                dbpIncidencia.ParameterName = "@idTipoIncidencia";
                dbpIncidencia.Value = objIncidencia.IdTipoIncidencia;
                dbComando.Parameters.Add(dbpIncidencia);

                dbComando.ExecuteNonQuery();

                dbTrans.Commit();

                return true;
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
    }
}
