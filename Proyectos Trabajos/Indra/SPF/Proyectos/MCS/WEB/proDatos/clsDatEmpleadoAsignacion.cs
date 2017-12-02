using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using proUtilerias;
using System.Web;
using System.Collections.Generic;

namespace SICOGUA.Datos
{
    public class clsDatEmpleadoAsignacion
    {

        public static void insertarEmpleadoHorarioREA(clsEntHorario objHorario, clsEntSesion objSesion)
        {

            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spuInsertarHorarioREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objHorario.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idHorario", objHorario.idHorario);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@ahFechaInicio", objHorario.ahFechaInicio.ToShortDateString()=="01/01/1900"?(object)DBNull.Value:objHorario.ahFechaInicio);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@ahFechaFin", objHorario.ahFechaFin.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objHorario.ahFechaInicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objHorario.idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaCierre", objHorario.fechaCierreAsignacion);

                dbComando.ExecuteNonQuery();
                dbTrans.Commit();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static void insertarEmpleadoAsignacion(clsEntEmpleado objEmpleado,clsEntEmpleadoAsignacion objAsignacion, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;
          
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spInsertarEmpleadoAsignacion";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objEmpleado.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario", objEmpleado.IdUsuario);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", objAsignacion.IdEmpleadoAsignacion);

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objAsignacion.Servicio.idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idFuncionAsignacion", objAsignacion.IdFuncionAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objAsignacion.Instalacion.IdInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@eaFechaIngreso", objAsignacion.EaFechaIngreso);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@eaFechaBaja", objAsignacion.EaFechaBaja.ToShortDateString() == "01/01/0001" || objAsignacion.EaFechaBaja.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objAsignacion.EaFechaBaja);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@operacion", objAsignacion.tipoOperacion == 3 ? (object)DBNull.Value : objAsignacion.tipoOperacion);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@oficio", objAsignacion.oficio );

        
                dbComando.ExecuteNonQuery();
          
              
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static void cerrarAsignacionMasivo(clsEntAsignacionMasiva objAsignacion, clsEntLaboralMasivo objLab, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spCerrarAsignacion";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objLab.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsignacion", objAsignacion.fechaIngreso);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaModificacion", objAsignacion.fechaModificacion);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario", objAsignacion.idUsuario);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objAsignacion.idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objAsignacion.idInstalacion);

                dbComando.ExecuteNonQuery();
                 }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public static void cerrarAsignacionMasivoPosterior(clsEntAsignacionMasiva objAsignacion, clsEntLaboralMasivo objLab, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spCerrarAsignacionPosterior";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objLab.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsignacion", objAsignacion.fechaIngreso);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaModificacion", objAsignacion.fechaModificacion);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario", objAsignacion.idUsuario);
                dbComando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static void cerrarAsignacionPosterior(Guid idEmpleado, DateTime fechaIngreso, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spCerrarAsignacionPosterior";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                clsEntSesion objSesion = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
          
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsignacion", fechaIngreso);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaModificacion", DateTime.Now.ToLocalTime());
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario",objSesion.usuario.IdEmpleado);

                
                dbComando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

  


        public static void insertarEmpleadoAsignacionMasivo(clsEntAsignacionMasiva objAsignacion, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spInsertarEmpleadoAsignacionMasivo";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado",objAsignacion.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idUsuario", objAsignacion.idUsuario);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", objAsignacion.idEmpleadoAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objAsignacion.idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idFuncionAsignacion", objAsignacion.idFuncionAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objAsignacion.idInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@eaFechaIngreso", objAsignacion.fechaIngreso);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@eaFechaBaja", objAsignacion.fechaBaja.ToShortDateString() == "01/01/0001" || objAsignacion.fechaBaja.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objAsignacion.fechaBaja);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@operacion", objAsignacion.operacion == 3 ? (object)DBNull.Value : objAsignacion.operacion);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaModificacion", objAsignacion.fechaModificacion);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@eaOficio", objAsignacion.eaOficio);
                
                dbComando.ExecuteNonQuery();
                objAsignacion.fechaModificacionMasiva = Convert.ToDateTime(dbComando.Parameters["@fechaModificacion"].Value);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /* ACTUALIZACIÓN MARZO 2017 */
        public static List<clsEntEmpleadosListaGenerica> consultaAsignacion(Guid idEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            List<clsEntEmpleadosListaGenerica> lisAsignaciones = new List<clsEntEmpleadosListaGenerica>();

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spConsultarEmpleadoAsignacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

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
                        clsEntEmpleadosListaGenerica objAsignacion = new clsEntEmpleadosListaGenerica
                        {
                            idEmpleado = Guid.Parse(drEmpleado["idEmpleado"].ToString()),
                            idEmpleadoAsignacion = Convert.ToInt32(drEmpleado["idEmpleadoAsignacion"].ToString()),
                            idServicio = Convert.ToInt32(drEmpleado["idServicio"].ToString()),
                            idInstalacion = Convert.ToInt32(drEmpleado["idInstalacion"].ToString()),
                            serDescripcion = drEmpleado["serDescripcion"].ToString(),
                            insNombre = drEmpleado["insNombre"].ToString(),
                            eaFechaIngreso = Convert.ToDateTime(drEmpleado["eaFechaIngreso"].ToString()),
                            eaFechaBaja = Convert.ToDateTime(drEmpleado["eaFechaBaja"].ToString()),
                            idFuncionAsignacion = Convert.ToInt32(drEmpleado["idFuncionAsignacion"].ToString()),
                            funcionAsignacion = drEmpleado["funcionAsignacion"].ToString(),
                        };


                        lisAsignaciones.Add(objAsignacion);

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
            return lisAsignaciones;
        }
        /* FIN ACTUALIZACIÓN */

        public static void consultarEmpleadoAsignacion(Guid idEmpleado, ref dsGuardas._empleado_empleadoAsignacionDataTable dsAsignacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spConsultarEmpleadoAsignacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsAsignacion);

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
        public static string consultarOficioAsignacion(Guid idEmpleado, int idEmpleadoAsignacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            string strOficio = "";
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spuConsultarOficioAsignacion";
                dbComando.Parameters.Clear();
                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", idEmpleadoAsignacion);

                DbParameter dbpOficio = objConexion.dbProvider.CreateParameter();

                dbpOficio.DbType = DbType.String;
                dbpOficio.ParameterName = "@eaOficio";
                dbpOficio.Value = "";
                dbpOficio.Size = 60;
                dbpOficio.Direction = ParameterDirection.Output;
                dbComando.Parameters.Add(dbpOficio);

                dbAdapter.SelectCommand = dbComando;
                dbComando.ExecuteNonQuery();
                strOficio = dbpOficio.Value.ToString();

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
            return strOficio;
        }
    }
}
