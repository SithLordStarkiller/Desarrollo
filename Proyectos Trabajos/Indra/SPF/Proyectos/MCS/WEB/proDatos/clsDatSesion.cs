using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;

namespace SICOGUA.Seguridad
{
    public class clsDatSesion
    {
        public static int iniciarSesion(clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spIniciarSesion";
                dbComando.Parameters.Clear();

                DbParameter dbpSessionId = objConexion.dbProvider.CreateParameter();
                DbParameter dbpUsuario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIp = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIntentos = objConexion.dbProvider.CreateParameter();
                DbParameter dbpEstatus = objConexion.dbProvider.CreateParameter();
                DbParameter dbpValido = objConexion.dbProvider.CreateParameter();

                dbpSessionId.DbType = DbType.String;
                dbpSessionId.ParameterName = "@seSessionID";
                dbpSessionId.Value = objSesion.sessionId;
                dbComando.Parameters.Add(dbpSessionId);

                dbpUsuario.DbType = DbType.Int32;
                dbpUsuario.ParameterName = "@idUsuario";
                dbpUsuario.Value = objSesion.usuario.IdUsuario;
                dbComando.Parameters.Add(dbpUsuario);

                dbpIp.DbType = DbType.String;
                dbpIp.ParameterName = "@seIP";
                dbpIp.Value = objSesion.ip;
                dbComando.Parameters.Add(dbpIp);

                dbpIntentos.DbType = DbType.Int32;
                dbpIntentos.ParameterName = "@seIntentos";
                dbpIntentos.Value = objSesion.intentos;
                dbComando.Parameters.Add(dbpIntentos);

                dbpEstatus.DbType = DbType.Int32;
                dbpEstatus.ParameterName = "@seEstatus";
                dbpEstatus.Value = (int)objSesion.estatus;
                dbComando.Parameters.Add(dbpEstatus);

                dbpValido.DbType = DbType.Boolean;
                dbpValido.ParameterName = "@valido";
                dbpValido.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpValido);

                dbComando.ExecuteNonQuery();

                int valido = (int)dbComando.Parameters["@valido"].Value;

                dbTrans.Commit();

                return valido;
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

        public static bool finalizarSesion(clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spFinalizarSesion";
                dbComando.Parameters.Clear();

                DbParameter dbpSessionId = objConexion.dbProvider.CreateParameter();
                DbParameter dbpUsuario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpEstatus = objConexion.dbProvider.CreateParameter();

                dbpSessionId.DbType = DbType.String;
                dbpSessionId.ParameterName = "@seSessionID";
                dbpSessionId.Value = objSesion.sessionId;
                dbComando.Parameters.Add(dbpSessionId);

                dbpUsuario.DbType = DbType.Int32;
                dbpUsuario.ParameterName = "@idUsuario";
                dbpUsuario.Value = objSesion.usuario.IdUsuario;
                dbComando.Parameters.Add(dbpUsuario);

                dbpEstatus.DbType = DbType.Int32;
                dbpEstatus.ParameterName = "@seEstatus";
                dbpEstatus.Value = (int)objSesion.estatus;
                dbComando.Parameters.Add(dbpEstatus);

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
