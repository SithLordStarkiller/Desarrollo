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
    public class clsDatEmpleadoHorarioREA
    {
        public static List<clsEntEmpleadoHorarioREA> consultaHorarios(clsEntSesion objSesion, Guid idEmpleado, DateTime datFechaInicio, DateTime datFechaFin, int idServicio, int idInstalacion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            List<clsEntEmpleadoHorarioREA> lisHorarios = new List<clsEntEmpleadoHorarioREA>();
            try
            {


                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spConsultarHorarioAsignacionREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado);


                clsParametro objParametroFechaInicio = new clsParametro();
                objParametroFechaInicio.llenarParametros(ref dbComando, DbType.DateTime, "@datFechaInicio", datFechaInicio);


                clsParametro objParametroFechaFin = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@datFechaFin", datFechaFin);


                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", idInstalacion);


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
                    foreach (DataRow drEmpleado in dTable.Rows)
                    {
                        clsEntEmpleadoHorarioREA obj = new clsEntEmpleadoHorarioREA
                        {
                            idAsignacionHorario = Convert.ToInt32(drEmpleado["idAsignacionHorario"].ToString()),
                            idEmpleado = Guid.Parse(drEmpleado["idEmpleado"].ToString()),
                            idHorario = Convert.ToInt32(drEmpleado["idHorario"]),
                            ahFechaInicio = DateTime.Parse(drEmpleado["ahFechaInicio"].ToString()),
                            ahVigente = Convert.ToBoolean(drEmpleado["ahVigente"].ToString()),
                            horNombre = drEmpleado["horNombre"].ToString()
                        };
                        if (drEmpleado["ahFechaFin"].ToString().Length > 0 )
                        {
                            obj.ahFechaFin = DateTime.Parse(drEmpleado["ahFechaFin"].ToString());
                           
                        }
                        if(obj.ahFechaFin.ToShortDateString() != "01/01/1900")
                        {
                            obj.strFechaFin = obj.ahFechaFin.ToShortDateString();
                        }
                        obj.strFechaInicio = obj.ahFechaInicio.ToShortDateString();
                        obj.intAccion = 0;
                        lisHorarios.Add(obj);
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
            return lisHorarios;
        }
    }
}
