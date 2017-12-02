using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatHorarioComplemento
    {
        public static DataTable consultarHorarioComplemento(Int16 idHorario, Int16 idTipoHorario, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spConsultarHorarioComplemento";
                dbComando.Parameters.Clear();

                DbParameter dbIdHorario = objConexion.dbProvider.CreateParameter();
                DbParameter dbIdTipoHorario = objConexion.dbProvider.CreateParameter();

                dbIdHorario.DbType = DbType.Int16;
                dbIdHorario.ParameterName = "@idHorario";
                dbIdHorario.Value = idHorario;
                dbComando.Parameters.Add(dbIdHorario);

                dbIdTipoHorario.DbType = DbType.Int16;
                dbIdTipoHorario.ParameterName = "@idTipoHorario";
                dbIdTipoHorario.Value = idTipoHorario;
                dbComando.Parameters.Add(dbIdTipoHorario);

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
