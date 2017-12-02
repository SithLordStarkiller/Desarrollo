using System;
using System.Collections.Generic;
using System.Text;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Data;
using System.Data.Common;
using SICOGUA.Datos;


namespace SICOGUA.Datos
{

    public class clsDatCredencial
    {
        public static string[] consultarCredenciales(clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            string[] strRegresa = { "", "", "" };
            try
            {


                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spuConsultarCredenciales";
                dbComando.Parameters.Clear();

                dbComando.ExecuteNonQuery();
                DataTable dTable = new DataTable();
                DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();
                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;
                dbAdapter.Fill(dTable);
                dbTrans.Commit();
                int intRenglones = dTable.Rows.Count;
                if (intRenglones != 0)
                {
                    foreach (DataRow drCredencial in dTable.Rows)
                    {
                        strRegresa[0] = drCredencial[0].ToString();
                        strRegresa[1] = drCredencial[1].ToString();
                        strRegresa[2] = drCredencial[2].ToString();

                    }

                }

            }

            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return strRegresa;
        }
    }
}