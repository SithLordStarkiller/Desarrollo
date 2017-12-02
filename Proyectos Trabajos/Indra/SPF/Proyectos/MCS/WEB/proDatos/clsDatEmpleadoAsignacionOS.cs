using System;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using proDatos;

namespace SICOGUA.Datos
{
    public class clsDatEmpleadoAsignacionOS
    {
        public static void insertarEmpleadoAsignacionOS(clsEntEmpleado objEmpleado, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spInsertarEmpleadoAsignacionOS";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objEmpleado.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacionOS", objEmpleado.EmpleadoAsignacionOS.IdEmpleadoAsignacionOS);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idZona", objEmpleado.EmpleadoAsignacionOS.Zona.IdZona);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idAgrupamiento", objEmpleado.EmpleadoAsignacionOS.Agrupamiento.IdAgrupamiento);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idCompania", objEmpleado.EmpleadoAsignacionOS.Compania.IdCompania);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idSeccion", objEmpleado.EmpleadoAsignacionOS.Seccion.IdSeccion);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idPeloton", objEmpleado.EmpleadoAsignacionOS.Peloton.IdPeloton);

                dbComando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static void consultarEmpleadoAsignacionOS(Guid idEmpleado, ref dsGuardas._operacionServicio_empleadoAsignacionOSDataTable dsAsignacionOS, clsEntSesion objSesion)
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
                dbComando.CommandText = "operacionServicio.spConsultarEmpleadoAsignacionOS";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsAsignacionOS);

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
    }
}
