using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;

namespace SICOGUA.Seguridad
{
    public class clsDatPermiso
    {
        public static bool consultarPermiso(Int16 idPerfil, Int16 idOperacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DataTable dtResultado = new DataTable();
            
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spConsultarPermiso";
                dbComando.Parameters.Clear();

                DbParameter dbpIdPerfil = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdOperacion = objConexion.dbProvider.CreateParameter();

                dbpIdPerfil.DbType = DbType.Int16;
                dbpIdPerfil.ParameterName = "@idPerfil";
                dbpIdPerfil.Value = idPerfil;
                dbComando.Parameters.Add(dbpIdPerfil);

                dbpIdOperacion.DbType = DbType.Int16;
                dbpIdOperacion.ParameterName = "@idOperacion";
                dbpIdOperacion.Value = idOperacion;
                dbComando.Parameters.Add(dbpIdOperacion);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

                dbTrans.Commit();

                if(dtResultado.Rows.Count == 1)
                {
                    return true;
                }
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

            return false;
        }

        public static bool consultarPermiso(Int16 idPerfil, string pagina, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DataTable dtResultado = new DataTable();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spConsultarPermisoPagina";
                dbComando.Parameters.Clear();

                DbParameter dbpPagina = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdPerfil = objConexion.dbProvider.CreateParameter();

                dbpPagina.DbType = DbType.String;
                dbpPagina.ParameterName = "@pagina";
                dbpPagina.Value = pagina;
                dbComando.Parameters.Add(dbpPagina);

                dbpIdPerfil.DbType = DbType.Int16;
                dbpIdPerfil.ParameterName = "@idPerfil";
                dbpIdPerfil.Value = idPerfil;
                dbComando.Parameters.Add(dbpIdPerfil);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

                dbTrans.Commit();

                if (dtResultado.Rows.Count == 1)
                {
                    return true;
                }
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

            return false;
        }

        //Agregue sp
        public static bool consultarPermisoPorServicioAsignados(Int32 idUsuario, Int32 idServicio, Int32 idInstalacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DataTable dtResultado = new DataTable();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spConsultarPermisoServInst";
                dbComando.Parameters.Clear();

                DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdServicio= objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();

                dbpIdUsuario.DbType = DbType.Int32;
                dbpIdUsuario.ParameterName = "@idUsuario";
                dbpIdUsuario.Value = idUsuario;
                dbComando.Parameters.Add(dbpIdUsuario);

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

                dbTrans.Commit();

                if (dtResultado.Rows.Count == 1)
                {
                    return true;
                }
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

            return false;
        }

        public static int consultarPermisoAsignaciones(Guid idEmpleado ,Int32 idUsuario, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DataTable dtResultado = new DataTable();
            
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spConsultarEmpleadoAsignacionActualPorUsuario";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpConsulta = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpIdUsuario.DbType = DbType.Int32;
                dbpIdUsuario.ParameterName = "@idUsuario";
                dbpIdUsuario.Value = idUsuario;
                dbComando.Parameters.Add(dbpIdUsuario);

                dbpConsulta.DbType = DbType.Int32;
                dbpConsulta.ParameterName = "@consulta";
                dbpConsulta.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpConsulta);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

                dbTrans.Commit();

                return (int)dbComando.Parameters["@consulta"].Value;
               
 
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

            //return false;
        }

    }
}
