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

    public class clsDatReemplazo
    {
        public static List<clsEntReemplazo> listaReemplazos(clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            List<clsEntReemplazo> lisReemplazos = new List<clsEntReemplazo>();
            try
            {


                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spConsultarAReemplazar";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", objSesion.usuario.IdUsuario);
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
                        clsEntJerarquia objJerarquia = new clsEntJerarquia
                        {
                            IdJerarquia = (byte)drEmpleado["idJerarquia"],
                            JerDescripcion = drEmpleado["jerDescripcion"].ToString()
                        };
                        clsEntPuesto objPuesto = new clsEntPuesto
                        {
                            Jerarquia = objJerarquia,
                            PueDescripcion = drEmpleado["pueDescripcion"].ToString()
                        };

                        clsEntTipoServicio objTipoServicio = new clsEntTipoServicio { tsDescripcion = drEmpleado["tsDescripcion"].ToString() };
                        clsEntServicio objServicio = new clsEntServicio
                        {
                            idServicio = (Int32)drEmpleado["idServicio"],
                            serDescripcion = drEmpleado["serDescripcion"].ToString(),
                            objTipoServicio = objTipoServicio
                        };
                        clsEntInstalacion objInstalacion = new clsEntInstalacion();

                        objInstalacion.IdInstalacion = (Int32)drEmpleado["idInstalacion"];
                        objInstalacion.InsNombre = drEmpleado["insNombre"].ToString();
                        objInstalacion.idZona = conversiones.enteroNoNulo(drEmpleado["idZona"]);


                        clsEntEmpleadoAsignacion objAsignacion = new clsEntEmpleadoAsignacion
                        {
                            IdFuncionAsignacion = (int)drEmpleado["idFuncionAsignacion"],
                            funcionAsignacion = drEmpleado["faDescripcion"].ToString(),
                            EaFechaIngreso = (DateTime)drEmpleado["eaFechaIngreso"],
                            EaFechaBaja = (DateTime)drEmpleado["eaFechaBaja"],
                            IdEmpleadoAsignacion = (short)drEmpleado["idEmpleadoAsignacion"],
                            Instalacion = objInstalacion,
                            Servicio = objServicio
                        };

                        List<clsEntEmpleadoAsignacion> lisAsignaciones = new List<clsEntEmpleadoAsignacion>();
                        lisAsignaciones.Add(objAsignacion);


                        clsEntAsignacionHorarioREA objEmpleadoHorario = new clsEntAsignacionHorarioREA
                        {
                            idAsignacionHorario =conversiones.enteroNoNulo(drEmpleado["idEmpleadoHorario"]),
                            ahFechaInicio = conversiones.fechaNoNulo(drEmpleado["ehFechaIngreso"]),
                            ahFechaFin = conversiones.fechaNoNulo(drEmpleado["ehFechaBaja"]),
                            horNombre = conversiones.cadena(drEmpleado["thDescripcion"]),
                            idHorario =conversiones.enteroCortoNoNulo(drEmpleado["idHorario"]),


                        };

                        List<clsEntAsignacionHorarioREA> lisEmpleadoHorario = new List<clsEntAsignacionHorarioREA>();
                        lisEmpleadoHorario.Add(objEmpleadoHorario);

                        clsEntIncidenciaREA objIncidenciaREA = new clsEntIncidenciaREA
                        {
                            tiDescripcion = conversiones.cadena(drEmpleado["tiDescripcion"]),
                            idTipoIncidencia = conversiones.enteroCortoNoNulo(drEmpleado["idTipoIncidencia"]),
                            incFechaInicio = conversiones.fechaNoNulo(drEmpleado["incFechaInicial"]),
                            incFechaFin = conversiones.fechaNoNulo(drEmpleado["incFechaFinal"]),
                            tipDescripcion = conversiones.cadena(drEmpleado["incDescripcion"]),

                        };

                                             
                       
                        List<clsEntIncidenciaREA> lisIncidencia = new List<clsEntIncidenciaREA>();
                        lisIncidencia.Add(objIncidenciaREA);
                       
                        clsEntEmpleadoPuesto objEmpleadoPuesto = new clsEntEmpleadoPuesto { Puesto = objPuesto };
                        clsEntEmpleado objEmpleado = new clsEntEmpleado
                        {
                            EmpPaterno = drEmpleado["empPaterno"].ToString(),
                            EmpMaterno = drEmpleado["empMaterno"].ToString(),
                            EmpNombre = drEmpleado["empNombre"].ToString(),
                            EmpNumero = (Int32)drEmpleado["empNumero"],
                            EmpleadoPuesto = objEmpleadoPuesto,
                            EmpLOC = (bool)drEmpleado["empLoc"] == true ? 1 : 0,
                            IdEmpleado = (Guid)drEmpleado["idEmpleado"],
                            EmpleadoAsignacion = lisAsignaciones,
                            incidenciaREA = lisIncidencia,
                            horarioREA = objEmpleadoHorario,
                            // EmpleadoHorario = objHorario,
                        };
                        clsEntReemplazo objReemplazo = new clsEntReemplazo { integranteReemplazar = objEmpleado };
                        lisReemplazos.Add(objReemplazo);

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
            return lisReemplazos;
        }

        public static bool insertaReemplazo(clsEntReemplazo objReemplazo, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spGeneraReemplazo";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoReemplazo", objReemplazo.integranteReemplazo.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@datFechaInicio", objReemplazo.integranteReemplazo.fechaIniCom);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@datFechaFin", objReemplazo.integranteReemplazo.fechaFinAsignacion);

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].Servicio.idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].Instalacion.IdInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario", objSesion.usuario.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idHorario", objReemplazo.integranteReemplazar.horarioREA.idHorario);
               
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoReemplazar", objReemplazo.integranteReemplazar.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idAsignacionEmpleadoReemplazar", objReemplazo.integranteReemplazar.EmpleadoAsignacion[0].IdEmpleadoAsignacion);


                dbComando.ExecuteNonQuery();
                dbComando.Transaction.Commit();
                return true;

            }
            catch (Exception Ex)
            {
                dbComando.Transaction.Rollback();
                return false;
                throw Ex;

            }
        }
    }
}
