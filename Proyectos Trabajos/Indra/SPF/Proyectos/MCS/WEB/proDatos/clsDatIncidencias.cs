using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatIncidencias
    {
        public static bool insertarRecursoHumanoIncidencias(clsEntEmpleado objEmpleado, List<clsEntIncidencia> eliminarIncidencias, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();

            try
            {
                foreach (clsEntIncidencia incidencia in eliminarIncidencias)
                {
                    eliminarIncidencia(objEmpleado.IdEmpleado, incidencia, dbConexion, dbTrans, objSesion);
                }
                foreach (clsEntIncidencia incidencia in objEmpleado.Incidencias)
                {
                    insertarIncidencia(objEmpleado.IdEmpleado, incidencia, dbConexion, dbTrans, objSesion);
                }

                dbTrans.Commit();

                return true;
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

        public static void insertarIncidencia(Guid idEmpleado, clsEntIncidencia objIncidencia, DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spInsertarIncidencia";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdIncidencia = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdPersonaAutoriza = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdTipoIncidencia = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaIncial = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaFinal = objConexion.dbProvider.CreateParameter();
                DbParameter dbpDescripcion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNoOficio = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpIdIncidencia.DbType = DbType.Int32;
                dbpIdIncidencia.ParameterName = "@idIncidencia";
                dbpIdIncidencia.Value = objIncidencia.IdIncidencia;
                dbComando.Parameters.Add(dbpIdIncidencia);

                dbpIdPersonaAutoriza.DbType = DbType.Guid;
                dbpIdPersonaAutoriza.ParameterName = "@idEmpleadoAutoriza";
                dbpIdPersonaAutoriza.Value =
                    objIncidencia.IdEmpleadoAutoriza.CompareTo(new Guid("00000000-0000-0000-0000-000000000000")) == 0
                        ? (object)DBNull.Value
                        : objIncidencia.IdEmpleadoAutoriza;
                dbComando.Parameters.Add(dbpIdPersonaAutoriza);

                dbpIdTipoIncidencia.DbType = DbType.Int16;
                dbpIdTipoIncidencia.ParameterName = "@idTipoIncidencia";
                dbpIdTipoIncidencia.Value = objIncidencia.IdTipoIncidencia == 0 ? (object)DBNull.Value : objIncidencia.IdTipoIncidencia;
                dbComando.Parameters.Add(dbpIdTipoIncidencia);

                dbpFechaIncial.DbType = DbType.DateTime;
                dbpFechaIncial.ParameterName = "@incFechaInicial";
                dbpFechaIncial.Value = objIncidencia.IncFechaInicial.ToShortDateString() == "01/01/0001"
                                           ? (object)DBNull.Value
                                           : objIncidencia.IncFechaInicial;
                dbComando.Parameters.Add(dbpFechaIncial);

                dbpFechaFinal.DbType = DbType.DateTime;
                dbpFechaFinal.ParameterName = "@incFechaFinal";
                dbpFechaFinal.Value = objIncidencia.IncFechaFinal.ToShortDateString() == "01/01/0001"
                                           ? (object)DBNull.Value
                                           : objIncidencia.IncFechaFinal;
                dbComando.Parameters.Add(dbpFechaFinal);

                dbpDescripcion.DbType = DbType.String;
                dbpDescripcion.ParameterName = "@incDescripcion";
                dbpDescripcion.Value = objIncidencia.IncDescripcion ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpDescripcion);

                dbpNoOficio.DbType = DbType.String;
                dbpNoOficio.ParameterName = "@incNoOficio";
                dbpNoOficio.Value = objIncidencia.IncNoOficio ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpNoOficio);

                dbComando.ExecuteNonQuery();
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static void eliminarIncidencia(Guid idEmpleado, clsEntIncidencia objIncidencia, DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spEliminarIncidencia";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdIncidencia = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpIdIncidencia.DbType = DbType.Int32;
                dbpIdIncidencia.ParameterName = "@idIncidencia";
                dbpIdIncidencia.Value = objIncidencia.IdIncidencia;
                dbComando.Parameters.Add(dbpIdIncidencia);

                dbComando.ExecuteNonQuery();
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static void consultarIncidencias(Guid idEmpleado, ref List<clsEntIncidencia> lisIncidencias, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();
            DataSet dsREsultado = new DataSet();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spConsultarIncidenciasREA";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                
                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsREsultado);

                if (dsREsultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow renglon in dsREsultado.Tables[0].Rows)
                    {
                       clsEntTipoIncidencia objTipo = new clsEntTipoIncidencia{ IdTipoIncidencia = Convert.ToInt16(renglon[4].ToString()), TiDescripcion =renglon[5].ToString()};

                        clsEntIncidencia obj = new clsEntIncidencia
                        {
                            IdEmpleado = Guid.Parse(renglon[0].ToString()),
                            IdEmpleadoAutoriza = Guid.Parse(renglon[2].ToString()),
                            IdIncidencia = Convert.ToInt32(renglon[1].ToString()),
                            IdTipoIncidencia = Convert.ToInt16(renglon[4].ToString()),
                            IncDescripcion = renglon[8].ToString(),
                            
                            IncFechaInicial = DateTime.Parse(renglon[6].ToString()),
                            IncNoOficio = renglon[9].ToString(),
                            sEmpleadoAutoriza = renglon[3].ToString(),
                            sFechaFinal = renglon[7].ToString(),
                            sFechaInicial = renglon[6].ToString(),
                            tipoIncidencia =objTipo
                        };
                        if (obj.sFechaFinal.Length > 0)
                        {
                            obj.IncFechaFinal = DateTime.Parse(renglon[7].ToString());
                        }
                        lisIncidencias.Add(obj);

                    }
                }

                //dbTrans.Commit();
            }
            catch (DbException dbEx)
            {
                try
                {
                    //dbTrans.Rollback();
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
                    //dbTrans.Rollback();
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
