using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Data.SqlClient;

namespace SICOGUA.Datos
{
    public class clsDatPaseLista
    {
        public static bool desabilitarAsistenciaTiempo(DateTime fecha, int idHorario, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spDesabilitarAsistenciaTiempoREA";
                dbComando.Parameters.Clear();

                DbParameter dbpFecha = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMaximo = objConexion.dbProvider.CreateParameter();
                DbParameter dbpHorario = objConexion.dbProvider.CreateParameter();

                dbpHorario.DbType = DbType.Int32;
                dbpHorario.ParameterName = "@idhorario";
                dbpHorario.Value = idHorario;
                dbComando.Parameters.Add(dbpHorario);

                dbpFecha.DbType = DbType.DateTime;
                dbpFecha.ParameterName = "@fecha";
                dbpFecha.Value = fecha;
                dbComando.Parameters.Add(dbpFecha);

                dbpMaximo.DbType = DbType.Boolean;
                dbpMaximo.ParameterName = "@MCS";
                dbpMaximo.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpMaximo);

                dbComando.ExecuteNonQuery();

               return Convert.ToBoolean(dbComando.Parameters["@MCS"].Value);
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
        public static bool asistenciaBiometriocMCS(int idServicio, int idInstalacion,int idHorario,  clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spMCSoBiometricoREA";
                dbComando.Parameters.Clear();

                
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMaximo = objConexion.dbProvider.CreateParameter();
                DbParameter dbpHorario = objConexion.dbProvider.CreateParameter();

                dbpServicio.DbType = DbType.Int32;
                dbpServicio.ParameterName = "@idServicio";
                dbpServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpServicio);

                dbpInstalacion.DbType = DbType.Int32;
                dbpInstalacion.ParameterName = "@idInstalacion";
                dbpInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpInstalacion);

                dbpMaximo.DbType = DbType.Boolean;
                dbpMaximo.ParameterName = "@MCS";                
                dbpMaximo.Direction = ParameterDirection.ReturnValue;                
                dbComando.Parameters.Add(dbpMaximo);

                dbpHorario.DbType = DbType.Int32;
                dbpHorario.ParameterName = "@idhorario";
                dbpHorario.Value = idHorario;
                dbComando.Parameters.Add(dbpHorario);
               

                dbComando.ExecuteNonQuery();

                return Convert.ToBoolean(dbComando.Parameters["@MCS"].Value);
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

        /* ACTUALIZACION Febrero 2017 Pases de Lista Híbridos (MCS y Biométrico) */
        public static bool paseListaHibrido(int idServicio, int idZona, int idInstalacion, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spuConsultarPaseListaHibrido";
                dbComando.Parameters.Clear();


                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbpHibrido = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();

                dbpServicio.DbType = DbType.Int32;
                dbpServicio.ParameterName = "@idServicio";
                dbpServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpServicio);

                dbpZona.DbType = DbType.Int32;
                dbpZona.ParameterName = "@idZona";
                dbpZona.Value = idZona;
                dbComando.Parameters.Add(dbpZona);

                dbpHibrido.DbType = DbType.Boolean;
                dbpHibrido.ParameterName = "@hibrido";
                dbpHibrido.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpHibrido);

                dbpInstalacion.DbType = DbType.Int32;
                dbpInstalacion.ParameterName = "@idInstalacion";
                dbpInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpInstalacion);

                dbComando.ExecuteNonQuery();

                return Convert.ToBoolean(dbComando.Parameters["@hibrido"].Value);
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

        public static bool horarioAbiertoMCS(int idHorario, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spHorarioAbiertoMCSREA";
                dbComando.Parameters.Clear();


                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMaximo = objConexion.dbProvider.CreateParameter();
                DbParameter dbpHorario = objConexion.dbProvider.CreateParameter();

               

                dbpMaximo.DbType = DbType.Boolean;
                dbpMaximo.ParameterName = "@MCS";
                dbpMaximo.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpMaximo);

                dbpHorario.DbType = DbType.Int32;
                dbpHorario.ParameterName = "@idhorario";
                dbpHorario.Value = idHorario;
                dbComando.Parameters.Add(dbpHorario);


                dbComando.ExecuteNonQuery();

                return Convert.ToBoolean(dbComando.Parameters["@MCS"].Value);
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

        public static DataTable consultaListaAsistenciaSalida(clsEntEmpleadoAsignacion objAsignacion,int idHorario, DateTime dtInstante, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
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
                dbComando.CommandText = "operacionServicio.spGenerarListaAsistenciaSalidaREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objAsignacion.Servicio.idServicio != 0 ? (object)objAsignacion.Servicio.idServicio : DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objAsignacion.Instalacion.IdInstalacion != 0 ? (object)objAsignacion.Instalacion.IdInstalacion : DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idHorario", idHorario);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", objSesion.usuario.IdUsuario != 0 ? (object)objSesion.usuario.IdUsuario : DBNull.Value);


                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fecha", dtInstante);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;

                dbAdapter.Fill(dTable);
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
            return dTable;
        }
        public static DataTable consultaListaAsistencia(clsEntEmpleadoAsignacion objAsignacion, /*clsEntEmpleadoAsignacionOS objAsignacionOs,*/int idHorario,  DateTime dtInstante, clsEntSesion objSesion)
        {
            DataTable dTable = new DataTable();
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
                //dbComando.CommandText = "operacionServicio.spGenerarListaAsistencia";
                dbComando.CommandText = "operacionServicio.spGenerarListaAsistenciaREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idZona", objAsignacionOs.Zona.IdZona != 0 ? (object)objAsignacionOs.Zona.IdZona : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idAgrupamiento", objAsignacionOs.Agrupamiento.IdAgrupamiento != 0 ? (object)objAsignacionOs.Agrupamiento.IdAgrupamiento : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idCompania", objAsignacionOs.Compania.IdCompania != 0 ? (object)objAsignacionOs.Compania.IdCompania : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idSeccion", objAsignacionOs.Seccion.IdSeccion != 0 ? (object)objAsignacionOs.Seccion.IdSeccion : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idPeloton", objAsignacionOs.Peloton.IdPeloton != 0 ? (object)objAsignacionOs.Peloton.IdPeloton : DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objAsignacion.Servicio.idServicio != 0 ? (object)objAsignacion.Servicio.idServicio : DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objAsignacion.Instalacion.IdInstalacion != 0 ? (object)objAsignacion.Instalacion.IdInstalacion : DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idHorario", idHorario);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", objSesion.usuario.IdUsuario != 0 ? (object)objSesion.usuario.IdUsuario : DBNull.Value);
                
                
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fecha", dtInstante);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;

                dbAdapter.Fill(dTable);
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
            return dTable;
        }

        public static int consultarMaxIdPaseLista(DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spConsultarMaxIdPaseLista";
                dbComando.Parameters.Clear();

                DbParameter dbpMaximo = objConexion.dbProvider.CreateParameter();

                dbpMaximo.DbType = DbType.Int32;
                dbpMaximo.ParameterName = "@maximo";
                dbpMaximo.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpMaximo);

                dbComando.ExecuteNonQuery();

                return (int) dbComando.Parameters["@maximo"].Value;
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

        public static void insertarPaseListaCompleto( DataTable dtPaseLista,DateTime fecha,clsEntSesion objSesion)
        {
            int idUsuario = objSesion.usuario.IdUsuario;
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spInsertarPaseListaREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                /*Aquí paso toda la tabla al procedimiento almacenado*/
                SqlParameter tabla = new SqlParameter();
                tabla.ParameterName = "@paseListaTable";
                tabla.SqlDbType = System.Data.SqlDbType.Structured;
                tabla.Value = dtPaseLista;
                dbComando.Parameters.Add(tabla);

                SqlParameter fechActual = new SqlParameter();
                fechActual.ParameterName = "@fecha";
                fechActual.SqlDbType = System.Data.SqlDbType.DateTime;
                fechActual.Value = fecha;
                dbComando.Parameters.Add(fechActual);
                 
                SqlParameter idEmpleadoPL = new SqlParameter();
                idEmpleadoPL.ParameterName = "@idEmpleadoPL";
                idEmpleadoPL.SqlDbType = System.Data.SqlDbType.Int;
                idEmpleadoPL.Value = idUsuario;
                dbComando.Parameters.Add(idEmpleadoPL);
               
               
                dbComando.ExecuteNonQuery();
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
        public static void insertarAistenciaSalidaCompleto(DataTable dtPaseLista, DateTime fecha, clsEntSesion objSesion)
        {
            int idUsuario = objSesion.usuario.IdUsuario;
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spInsertarPaseListaSalidaREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                /*Aquí paso toda la tabla al procedimiento almacenado*/
                SqlParameter tabla = new SqlParameter();
                tabla.ParameterName = "@paseListaTable";
                tabla.SqlDbType = System.Data.SqlDbType.Structured;
                tabla.Value = dtPaseLista;
                dbComando.Parameters.Add(tabla);

                SqlParameter fechActual = new SqlParameter();
                fechActual.ParameterName = "@fecha";
                fechActual.SqlDbType = System.Data.SqlDbType.DateTime;
                fechActual.Value = fecha;
                dbComando.Parameters.Add(fechActual);

                SqlParameter idEmpleadoPL = new SqlParameter();
                idEmpleadoPL.ParameterName = "@idEmpleadoPL";
                idEmpleadoPL.SqlDbType = System.Data.SqlDbType.Int;
                idEmpleadoPL.Value = idUsuario;
                dbComando.Parameters.Add(idEmpleadoPL);


                dbComando.ExecuteNonQuery();
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
        public static bool insertarListaAsistencia(List<clsEntPaseLista> lstPaseLista, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();

            try
            {
                int idPaseLista = consultarMaxIdPaseLista(dbConexion, dbTrans, objSesion);

                foreach (clsEntPaseLista objPaseLista in lstPaseLista)
                {
                    objPaseLista.IdPaseLista = idPaseLista;
                    insertarPaseLista(objPaseLista, dbConexion, dbTrans, objSesion);
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

        public static void insertarPaseLista(clsEntPaseLista objPaseLista, DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "operacionServicio.spInsertarPaseLista";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objPaseLista.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoHorario", objPaseLista.IdEmpleadoHorario);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idPaseLista", objPaseLista.IdPaseLista);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idTipoAsistencia", objPaseLista.IdTipoAsistencia == 0 ? (object) DBNull.Value : objPaseLista.IdTipoAsistencia);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idIncidencia", objPaseLista.IdIncidencia == 0 ? (object) DBNull.Value : objPaseLista.IdIncidencia);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@plFecha", objPaseLista.PlFecha.ToShortDateString() == "01/01/0001" || objPaseLista.PlFecha.ToShortDateString() == "01/01/1900" ? (object) DBNull.Value : objPaseLista.PlFecha);

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

        public static DataTable buscarListaAsistencia(clsEntEmpleadoAsignacion objAsignacion, clsEntEmpleadoAsignacionOS objAsignacionOs, DateTime dtFecha, DateTime dtHora, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
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
                dbComando.CommandText = "operacionServicio.spConsultarPaseLista";
                dbComando.Parameters.Clear();

                DbParameter dbZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbAgrupamiento = objConexion.dbProvider.CreateParameter();
                DbParameter dbCompania = objConexion.dbProvider.CreateParameter();
                DbParameter dbSeccion = objConexion.dbProvider.CreateParameter();
                DbParameter dbPeloton = objConexion.dbProvider.CreateParameter();
                DbParameter dbServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbFecha = objConexion.dbProvider.CreateParameter();
                DbParameter dbHora = objConexion.dbProvider.CreateParameter();
                DbParameter dbUsario= objConexion.dbProvider.CreateParameter();

                int idUsuario = objSesion.usuario.IdUsuario;

                dbZona.DbType = DbType.Int16;
                dbZona.ParameterName = "@idZona";
                dbZona.Value = objAsignacionOs.Zona.IdZona != 0 ? (object)objAsignacionOs.Zona.IdZona : DBNull.Value;
                dbComando.Parameters.Add(dbZona);

                dbUsario.DbType = DbType.Int32;
                dbUsario.ParameterName = "@idUsuario";
                dbUsario.Value = idUsuario;
                dbComando.Parameters.Add(dbUsario);

                dbAgrupamiento.DbType = DbType.Int16;
                dbAgrupamiento.ParameterName = "@idAgrupamiento";
                dbAgrupamiento.Value = objAsignacionOs.Agrupamiento.IdAgrupamiento != 0 ? (object)objAsignacionOs.Agrupamiento.IdAgrupamiento : DBNull.Value;
                dbComando.Parameters.Add(dbAgrupamiento);

                dbCompania.DbType = DbType.Int16;
                dbCompania.ParameterName = "@idCompania";
                dbCompania.Value = objAsignacionOs.Compania.IdCompania != 0 ? (object)objAsignacionOs.Compania.IdCompania : DBNull.Value;
                dbComando.Parameters.Add(dbCompania);

                dbSeccion.DbType = DbType.Int16;
                dbSeccion.ParameterName = "@idSeccion";
                dbSeccion.Value = objAsignacionOs.Seccion.IdSeccion != 0 ? (object)objAsignacionOs.Seccion.IdSeccion : DBNull.Value;
                dbComando.Parameters.Add(dbSeccion);

                dbPeloton.DbType = DbType.Int16;
                dbPeloton.ParameterName = "@idPeloton";
                dbPeloton.Value = objAsignacionOs.Peloton.IdPeloton != 0 ? (object)objAsignacionOs.Peloton.IdPeloton : DBNull.Value;
                dbComando.Parameters.Add(dbPeloton);

                dbServicio.DbType = DbType.Int32;
                dbServicio.ParameterName = "@idServicio";
                dbServicio.Value = objAsignacion.Servicio.idServicio != 0 ? (object)objAsignacion.Servicio.idServicio : DBNull.Value;
                dbComando.Parameters.Add(dbServicio);

                dbInstalacion.DbType = DbType.Int32;
                dbInstalacion.ParameterName = "@idInstalacion";
                dbInstalacion.Value = objAsignacion.Instalacion.IdInstalacion != 0 ? (object)objAsignacion.Instalacion.IdInstalacion : DBNull.Value;
                dbComando.Parameters.Add(dbInstalacion);

                dbFecha.DbType = DbType.DateTime;
                dbFecha.ParameterName = "@fecha";
                dbFecha.Value = dtFecha.ToShortDateString() == "01/01/1900" ? DBNull.Value : (object)dtFecha;
                dbComando.Parameters.Add(dbFecha);

                dbHora.DbType = DbType.DateTime;
                dbHora.ParameterName = "@hora";
                dbHora.Value = dtHora.ToShortTimeString() == "12:00 a.m." ? DBNull.Value : (object)dtHora;
                dbComando.Parameters.Add(dbHora);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
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
            return dTable;
        }

        //AGREGUE
        public static DataTable consultarPaseListaAnterior(Guid idEmpleado, DateTime dtInstante, clsEntSesion objSesion)
        {
            DataTable dTable;
            DataSet ds = new DataSet();
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
                dbComando.CommandText = "operacionServicio.spConsultarPaseListaTurnoAnterior";
                dbComando.Parameters.Clear();

                DbParameter dbIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbFecha = objConexion.dbProvider.CreateParameter();
                //DbParameter dbFecha = objConexion.dbProvider.CreateParameter();
                //DbParameter dbHora = objConexion.dbProvider.CreateParameter();

                dbIdEmpleado.DbType = DbType.Guid;
                dbIdEmpleado.ParameterName = "@idEmpleado";
                dbIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbIdEmpleado);

                dbFecha.DbType = DbType.DateTime;
                dbFecha.ParameterName = "@fecha";
                dbFecha.Value = dtInstante;
                dbComando.Parameters.Add(dbFecha);

                //dbFecha.DbType = DbType.DateTime;
                //dbFecha.ParameterName = "@fecha";
                //dbFecha.Value = dtFecha.ToShortDateString() == "01/01/1900" ? DBNull.Value : (object)dtFecha;
                //dbComando.Parameters.Add(dbFecha);

                //dbHora.DbType = DbType.DateTime;
                //dbHora.ParameterName = "@hora";
                //dbHora.Value = dtHora.ToShortTimeString() == "12:00 a.m." ? DBNull.Value : (object)dtHora;
                //dbComando.Parameters.Add(dbHora);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.SelectCommand.Connection = dbConexion;

                dbAdapter.Fill(ds);
                dTable = ds.Tables[0];
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
            return dTable;
        }


        //AGREGUE
        //public static DataTable consultarPaseListaAnterior(Guid idEmpleado, DateTime dtInstante, clsEntSesion objSesion)
        //{
        //    DataTable dTable;
        //    DataSet ds = new DataSet();
        //    clsDatConexion objConexion = new clsDatConexion();
        //    DbConnection dbConexion = objConexion.getConexion(objSesion);

        //    DbCommand dbComando = objConexion.dbProvider.CreateCommand();
        //    DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

        //    DbTransaction dbTrans = dbConexion.BeginTransaction();
        //    dbComando.Transaction = dbTrans;
        //    dbComando.Connection = dbConexion;

        //    try
        //    {
        //        dbComando.CommandType = CommandType.StoredProcedure;
        //        dbComando.CommandText = "operacionServicio.spConsultarPaseListaTurnoAnterior";
        //        dbComando.Parameters.Clear();

        //        DbParameter dbIdEmpleado = objConexion.dbProvider.CreateParameter();
        //        DbParameter dbFecha = objConexion.dbProvider.CreateParameter();
        //        //DbParameter dbFecha = objConexion.dbProvider.CreateParameter();
        //        //DbParameter dbHora = objConexion.dbProvider.CreateParameter();

        //        dbIdEmpleado.DbType = DbType.Guid;
        //        dbIdEmpleado.ParameterName = "@idEmpleado";
        //        dbIdEmpleado.Value = idEmpleado;
        //        dbComando.Parameters.Add(dbIdEmpleado);

        //        dbFecha.DbType = DbType.DateTime;
        //        dbFecha.ParameterName = "@fecha";
        //        dbFecha.Value = dtInstante;
        //        dbComando.Parameters.Add(dbFecha);

        //        //dbFecha.DbType = DbType.DateTime;
        //        //dbFecha.ParameterName = "@fecha";
        //        //dbFecha.Value = dtFecha.ToShortDateString() == "01/01/1900" ? DBNull.Value : (object)dtFecha;
        //        //dbComando.Parameters.Add(dbFecha);

        //        //dbHora.DbType = DbType.DateTime;
        //        //dbHora.ParameterName = "@hora";
        //        //dbHora.Value = dtHora.ToShortTimeString() == "12:00 a.m." ? DBNull.Value : (object)dtHora;
        //        //dbComando.Parameters.Add(dbHora);

        //        dbAdapter.SelectCommand = dbComando;
        //        dbAdapter.SelectCommand.Connection = dbConexion;

        //        dbAdapter.Fill(ds);
        //        dTable = ds.Tables[0];
        //        dbTrans.Commit();
        //    }
        //    catch (DbException dbEx)
        //    {
        //        try
        //        {
        //            dbTrans.Rollback();
        //        }
        //        catch (DbException dbExRoll)
        //        {
        //            throw dbExRoll;
        //        }
        //        throw dbEx;
        //    }
        //    catch (Exception Ex)
        //    {
        //        try
        //        {
        //            dbTrans.Rollback();
        //        }
        //        catch (DbException dbExRoll)
        //        {
        //            throw dbExRoll;
        //        }
        //        throw Ex;
        //    }
        //    finally
        //    {
        //        clsDatConexion.cerrarTransaccion(dbConexion);
        //    }
        //    return dTable;
        //}

    }
}
