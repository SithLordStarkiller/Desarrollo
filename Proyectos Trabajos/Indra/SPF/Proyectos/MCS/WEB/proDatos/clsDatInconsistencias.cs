using System;
using System.Collections.Generic;
using System.Text;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Data;
using System.Data.Common;
using REA.Datos;
using REA.Entidades;

namespace SICOGUA.Datos
{

    public class clsDatInconsistencias
    {
        public static List<clsEntIncosistencia> listaInconsistencia(clsEntSesion objSesion, int idServicio, int idInstalacion, DateTime fecha)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            List<clsEntIncosistencia> lisInconsistencias = new List<clsEntIncosistencia>();
            try
            {


                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spuListaInconsistenciasAsistencia";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", objSesion.usuario.IdUsuario);
                clsParametro objParametroI = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idInstalacion", idInstalacion);
                clsParametro objParametroS = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idServicio", idServicio);
                clsParametro objParametroF = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fecha", fecha);
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
                        clsEntIncosistencia objInconsistencia = new clsEntIncosistencia
                        {
                            zonDescripcion = drEmpleado["zonDescripcion"].ToString(),
                            serDescripcion = drEmpleado["serDescripcion"].ToString(),
                            insNombre = drEmpleado["insNombre"].ToString(),
                            eaFechaIngreso = drEmpleado["eaFechaIngreso"].ToString(),
                            eaFechaBaja = drEmpleado["eaFechaBaja"].ToString(),
                            idEmpleado = Guid.Parse(drEmpleado["idEmpleado"].ToString()),
                            empleadoNombre = drEmpleado["empPaterno"].ToString() + ' ' + drEmpleado["empMaterno"].ToString() + ' ' + drEmpleado["empNombre"].ToString(),
                            capturaNombre = drEmpleado["usuPaterno"].ToString() + ' ' + drEmpleado["usuMaterno"].ToString() + ' ' + drEmpleado["usuNombre"].ToString(),
                            permisoCambiar = Convert.ToBoolean(drEmpleado["permisoCambiar"].ToString()),
                            idServicio = Convert.ToInt32(drEmpleado["idServicio"].ToString()),
                            idInstalacion = Convert.ToInt32(drEmpleado["idInstalacion"].ToString()),
                            idHorario = Convert.ToInt32(drEmpleado["idHorario"].ToString()),
                            idFuncionAsignacion = Convert.ToInt32(drEmpleado["idFuncionAsignacion"].ToString()),
                            cambiar = 0
                        };


                        lisInconsistencias.Add(objInconsistencia);

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
            return lisInconsistencias;
        }
        public static string cambiaInconsistencias(List<clsEntIncosistencia> lisInconsistencias, clsEntSesion objSesion)
        {
            string strRegresa = "";

            foreach (clsEntIncosistencia objInconsistencia in lisInconsistencias)
            {
                clsDatConexion objConexion = new clsDatConexion();
                DbConnection dbConexion = objConexion.getConexion(objSesion);

                DbCommand dbComando = objConexion.dbProvider.CreateCommand();
                DbTransaction dbTrans = dbConexion.BeginTransaction();
                dbComando.Connection = dbConexion;
                dbComando.Transaction = dbTrans;
                try
                {
                    clsEntEmpleado objEmpleado = new clsEntEmpleado { IdEmpleado = objInconsistencia.idEmpleado, IdUsuario= objSesion.usuario.IdEmpleado};
                    clsEntServicio objServicio = new clsEntServicio{idServicio = objInconsistencia.idServicio};
                    clsEntInstalacion objInstalacion = new clsEntInstalacion{IdInstalacion = objInconsistencia.idInstalacion};
                    clsEntEmpleadoHorarioREA objHorario = new clsEntEmpleadoHorarioREA { ahFechaInicio = DateTime.Today, idHorario = objInconsistencia.idHorario, intAccion=1 };
                    List<clsEntEmpleadoHorarioREA> lisHorarios = new List<clsEntEmpleadoHorarioREA>();
                    lisHorarios.Add(objHorario);
                    clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion { IdEmpleadoAsignacion= 0, Servicio = objServicio , Instalacion = objInstalacion, IdFuncionAsignacion = objInconsistencia.idFuncionAsignacion,
                    EaFechaIngreso =DateTime.Today, tipoOperacion= 1, horarios = lisHorarios};
                   

                    clsDatEmpleadoAsignacion.insertarEmpleadoAsignacion(objEmpleado, objAsignacion, dbConexion, dbTrans);
                 // clsDatEmpleadoPuesto.insertarHorariosREA(objInconsistencia.idEmpleado, objAsignacion, objSesion);
                    dbTrans.Commit();
                }
                catch (Exception Ex)
                {
                    strRegresa= Ex.Message;
                }
            
            }

            return strRegresa;
        }
    }
}