using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using proUtilerias;

namespace SICOGUA.Datos
{
    public class clsDatEmpleadoPuesto
    {
        public static bool insertarEmpleadoPuesto(clsEntEmpleado objEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                //dbComando.CommandType = CommandType.StoredProcedure;
                //dbComando.CommandText = "empleado.spInsertarEmpleadoPuesto";
                //dbComando.Parameters.Clear();

                //clsParametro objParametro = new clsParametro();

                //objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objEmpleado.IdEmpleado);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoPuesto", objEmpleado.EmpleadoPuesto.IdEmpleadoPuesto);
                //objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idPuesto", objEmpleado.EmpleadoPuesto.Puesto.IdPuesto);
                ////AGregue
                //objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@ehFechaIngreso", objEmpleado.EmpleadoHorario.EhFechaingreso);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idHorario", objEmpleado.EmpleadoHorario.horario.IdHorario == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoHorario.horario.IdHorario);
                ////objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idHorario", objEmpleado.EmpleadoHorario.IdHorario == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoHorario.IdHorario);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idTipoHorario", objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoHorario.horario.tipoHorario.IdTipoHorario);

                //dbComando.ExecuteNonQuery();

                foreach (clsEntEmpleadoAsignacion asignacion in objEmpleado.EmpleadoAsignacion)
                {
                    clsDatEmpleadoAsignacion.insertarEmpleadoAsignacion(objEmpleado, asignacion, dbConexion, dbTrans);
                    clsDatEmpleadoPuesto.insertarHorariosREA(objEmpleado.IdEmpleado,  asignacion, objSesion);
                }


                //Cambio junio 2017 - Para evitar que se asignen Integrantes al servicio de despliegue que actualmente está dado de baja
                //clsDatEmpleadoAsignacion.cerrarAsignacionPosterior(objEmpleado.IdEmpleado, objEmpleado.EmpleadoAsignacion[(objEmpleado.EmpleadoAsignacion.Count()) - 1].EaFechaIngreso, dbConexion, dbTrans);
                

                dbTrans.Commit();

                return true;
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

        public static void consultarEmpleadoPuesto(Guid idEmpleado, ref dsGuardas._empleado_empleadoPuestoDataTable dsPuesto, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spConsultarEmpleadoPuesto";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsPuesto);

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

        public static bool insertarEmpleadoPuestoMasivo(clsEntAsignacionMasiva objAsigMasiva, clsEntLaboralMasivo objLab, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                clsDatEmpleadoAsignacion.cerrarAsignacionMasivo(objAsigMasiva, objLab, dbConexion, dbTrans);
                clsDatEmpleadoAsignacion.insertarEmpleadoAsignacionMasivo(objAsigMasiva, dbConexion, dbTrans);
                clsDatEmpleadoAsignacion.cerrarAsignacionMasivoPosterior(objAsigMasiva, objLab, dbConexion, dbTrans);



                dbTrans.Commit();

                return true;
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

        public static bool insertarHorariosREA(Guid idEmpleado, clsEntEmpleadoAsignacion objEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spInsertarHorarioREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                if (objEmpleado.horarios != null && objEmpleado.horarios.Count > 0)
                {
                    foreach (clsEntEmpleadoHorarioREA objhorario in objEmpleado.horarios)
                    {
                        if (objhorario.intAccion == 1 || objhorario.intAccion == 2 || objhorario.intAccion == 3)
                        {
                            dbComando.Parameters.Clear();
                            objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado);
                            objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idHorario", objhorario.idHorario);
                            objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@ahFechaInicio", objhorario.ahFechaInicio);


                            objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objEmpleado.Servicio.idServicio);

                            objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objEmpleado.Instalacion.IdInstalacion);


                            if (objhorario.strFechaFin != null && objhorario.strFechaFin.Length > 0)
                            {
                                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@ahFechaFin", objhorario.ahFechaFin);
                            }
                        
                            objParametro.llenarParametros(ref dbComando, DbType.Int32, "@intAccion", objhorario.intAccion);
                            dbComando.ExecuteNonQuery();
                        }
                    }
                }

               

                dbTrans.Commit();

                return true;
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
