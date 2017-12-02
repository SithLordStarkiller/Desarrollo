using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatEmpleadoHorario
    {
        public static DataTable consultarEmpleadoHorario(Guid idEmpleado, DateTime dtInstante, clsEntSesion objSesion)
        {
            DataTable dataTable = new DataTable();

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
                dbComando.CommandText = "recursoHumano.spConsultarEmpleadoHorario";
                dbComando.Parameters.Clear();

                DbParameter dbIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbFecha = objConexion.dbProvider.CreateParameter();

                dbIdEmpleado.DbType = DbType.Guid;
                dbIdEmpleado.ParameterName = "@idEmpleado";
                dbIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbIdEmpleado);

                dbFecha.DbType = DbType.DateTime;
                dbFecha.ParameterName = "@fecha";
                dbFecha.Value = dtInstante;
                dbComando.Parameters.Add(dbFecha);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;

                dbAdapter.Fill(dataTable);
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
            return dataTable;
        }

    }
}
