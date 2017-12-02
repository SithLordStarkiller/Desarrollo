using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatAsignacionLunes
    {
        public enum regresaValidacionLunes
        {
            Lunes,
            Festivo,
            LunesFestivo,
            Normal

        };
        public static regresaValidacionLunes validaDiaFestivo(DateTime datFecha, clsEntSesion objSesion)
        {
            regresaValidacionLunes regresa = regresaValidacionLunes.Normal;
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
                dbComando.CommandText = "MCS.spuValidaDiaFestivo";
                dbComando.Parameters.Clear();

                DbParameter dbpFecha = objConexion.dbProvider.CreateParameter();


                dbpFecha.DbType = DbType.Date;
                dbpFecha.ParameterName = "@datFecha";
                dbpFecha.Value = datFecha;
                dbComando.Parameters.Add(dbpFecha);



                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

                dbTrans.Commit();

                if (dtResultado.Rows.Count == 1)
                {
                    if (dtResultado.Rows[0][0].ToString() == "False")
                    {
                        regresa = regresaValidacionLunes.Normal;

                    }
                    else
                    {
                        regresa = regresaValidacionLunes.Festivo;
                    }
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
            return regresa;



        }
    }
}
