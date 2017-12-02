using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Data.Common;
using System.Data;


namespace SICOGUA.Datos
{
    public class clsDatFotografia
    {
     
        public static void consultarPersonaFoto(Guid idEmpleado, ref clsEntEmpleado objEmpleado, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spuConsultarFotografia";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.Direction = ParameterDirection.Input;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbAdapter.SelectCommand = dbComando;
                DataTable dsGuarda= new DataTable();
                dbAdapter.Fill(dsGuarda);
                if (dsGuarda != null && dsGuarda.Rows.Count > 0)
                {
                    objEmpleado.empFoto = (byte[]) dsGuarda.Rows[0][0];
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
        }

    }
}
