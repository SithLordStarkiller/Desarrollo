using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatAsistencia
    {
        public static int consultarOpcion(DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion, clsEntPaseLista objPaseLista )
        {
            clsDatConexion objConexion = new clsDatConexion();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spValidarAsistencia";
                dbComando.Parameters.Clear();

                DbParameter dbpOpcion = objConexion.dbProvider.CreateParameter();

                dbpOpcion.DbType = DbType.Int32;
                dbpOpcion.ParameterName = "@intRegresa";
                dbpOpcion.Direction = ParameterDirection.Output;
                dbpOpcion.Value = 0;
                dbComando.Parameters.Add(dbpOpcion);
                DbParameter dbpTipo = objConexion.dbProvider.CreateParameter();
                dbpTipo.DbType = DbType.String;
                dbpTipo.Size = 60;
                dbpTipo.ParameterName = "@strTipoAsistencia";
                dbpTipo.Direction = ParameterDirection.Output;
                dbpTipo.Value = "";
                dbComando.Parameters.Add(dbpTipo);

                DbParameter dbpHorario = objConexion.dbProvider.CreateParameter();

                dbpHorario.DbType = DbType.Int32;
                dbpHorario.ParameterName = "@idEmpleadoHorario";
                dbpHorario.Direction = ParameterDirection.Input;
                dbpHorario.Value = objPaseLista.IdEmpleadoHorario;
                dbComando.Parameters.Add(dbpHorario);

                DbParameter dbpFecha = objConexion.dbProvider.CreateParameter();

                dbpFecha.DbType = DbType.Date;
                dbpFecha.ParameterName = "@plFecha";
                dbpFecha.Direction = ParameterDirection.Input;
                dbpFecha.Value = objPaseLista.PlFecha;
                dbComando.Parameters.Add(dbpFecha);


                DbParameter dbpEmpleado = objConexion.dbProvider.CreateParameter();

                dbpEmpleado.DbType = DbType.Guid;
                dbpEmpleado.ParameterName = "@idEmpleado";
                dbpEmpleado.Direction = ParameterDirection.Input;
                dbpEmpleado.Value = objPaseLista.IdEmpleado;
                dbComando.Parameters.Add(dbpEmpleado);

                dbComando.ExecuteNonQuery();
                objPaseLista.strTipoAsistencia = dbComando.Parameters["@strTipoAsistencia"].Value.ToString();
                return (int)dbComando.Parameters["@intRegresa"].Value;
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
        public static int consultarOpcionREA(DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion, clsEntPaseLista objPaseLista)
        {
            clsDatConexion objConexion = new clsDatConexion();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spValidarAsistenciaREA";
                dbComando.Parameters.Clear();

                DbParameter dbpOpcion = objConexion.dbProvider.CreateParameter();

                dbpOpcion.DbType = DbType.Int32;
                dbpOpcion.ParameterName = "@intRegresa";
                dbpOpcion.Direction = ParameterDirection.Output;
                dbpOpcion.Value = 0;
                dbComando.Parameters.Add(dbpOpcion);
                DbParameter dbpTipo = objConexion.dbProvider.CreateParameter();
                dbpTipo.DbType = DbType.String;
                dbpTipo.Size = 60;
                dbpTipo.ParameterName = "@strTipoAsistencia";
                dbpTipo.Direction = ParameterDirection.Output;
                dbpTipo.Value = "";
                dbComando.Parameters.Add(dbpTipo);

                DbParameter dbpFecha = objConexion.dbProvider.CreateParameter();

                dbpFecha.DbType = DbType.Date;
                dbpFecha.ParameterName = "@plFecha";
                dbpFecha.Direction = ParameterDirection.Input;
                dbpFecha.Value = objPaseLista.PlFecha;
                dbComando.Parameters.Add(dbpFecha);


                DbParameter dbpEmpleado = objConexion.dbProvider.CreateParameter();

                dbpEmpleado.DbType = DbType.Guid;
                dbpEmpleado.ParameterName = "@idEmpleado";
                dbpEmpleado.Direction = ParameterDirection.Input;
                dbpEmpleado.Value = objPaseLista.IdEmpleado;
                dbComando.Parameters.Add(dbpEmpleado);

                dbComando.ExecuteNonQuery();
                objPaseLista.strTipoAsistencia = dbComando.Parameters["@strTipoAsistencia"].Value.ToString();
                return (int)dbComando.Parameters["@intRegresa"].Value;
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
        public static void insertarPaseAsistencia(clsEntPaseLista objPaseLista, DbConnection dbConexion, DbTransaction dbTrans, clsEntSesion objSesion, int intTipo)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spInsertarAsistencia";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objPaseLista.IdEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoHorario", objPaseLista.IdEmpleadoHorario);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idAsistencia", 0);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idTipoAsistencia", objPaseLista.IdTipoAsistencia == 0 ? (object)DBNull.Value : objPaseLista.IdTipoAsistencia);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idIncidencia", objPaseLista.IdIncidencia == 0 ? (object)DBNull.Value : objPaseLista.IdIncidencia);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@plFecha", objPaseLista.PlFecha.ToShortDateString() == "01/01/0001" || objPaseLista.PlFecha.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objPaseLista.PlFecha);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@strTipoAsistencia", objPaseLista.strTipoAsistencia == null ? "" : objPaseLista.strTipoAsistencia);

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
        public static int insertar(List<clsEntPaseLista> lstPaseLista, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbTransaction dbTrans = dbConexion.BeginTransaction();

            try
            {
                int idOpcion =consultarOpcion(dbConexion, dbTrans, objSesion, lstPaseLista[0]);
                if (idOpcion == 2)
                    return 2;
                foreach (clsEntPaseLista objPaseLista in lstPaseLista)
                {
                    insertarPaseAsistencia(objPaseLista, dbConexion, dbTrans, objSesion, idOpcion);
                }

                dbTrans.Commit();

                return idOpcion;
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


        public static void buscarAsistencias(string texto, int valor, ref int contador, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            int idUsuario = objSesion.usuario.IdUsuario;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spBuscarTipoAsistencia";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.String, "@taDescripcion", texto == null ? (object)DBNull.Value : texto);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idTipoAsistencia", valor == -1 ? (object)DBNull.Value : valor);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, ParameterDirection.Output, "@contador", -1);


                dbComando.ExecuteNonQuery();
                contador = Convert.ToInt32(dbComando.Parameters["@contador"].Value);
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

        public static void validarSancion(Guid idEmpleado, ref bool booSuspension, ref bool booInhabilitacion, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();
            booInhabilitacion = booSuspension = false;
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            int idUsuario = objSesion.usuario.IdUsuario;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spuValidarSancion";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado ==Guid.Empty ? (object)DBNull.Value : idEmpleado);

                DataAdapter daResultado = objConexion.dbProvider.CreateDataAdapter();
                DataTable dtResultado = new DataTable();
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);
               
             
                dbTrans.Commit();

                if (dtResultado.Rows.Count > 0)
                {
                    if (dtResultado.Rows[0][0].ToString() != null)
                    {
                        booSuspension = true;
                    }
                    if (dtResultado.Rows[0][1].ToString() != null)
                    {
                        booInhabilitacion = true;
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
            

            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
        }

        public static bool validaInstalacionCONEC(int idServicio, int idInstalacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "servicio.spValidaInstalacionCONEC";
                dbComando.Parameters.Clear();

                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpRespuesta = objConexion.dbProvider.CreateParameter();
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();

                dbpServicio.DbType = DbType.Int32;
                dbpServicio.ParameterName = "@idServicio";
                dbpServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpServicio);

                dbpInstalacion.DbType = DbType.Int32;
                dbpInstalacion.ParameterName = "@idInstalacion";
                dbpInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpInstalacion);

                dbpRespuesta.DbType = DbType.Boolean;
                dbpRespuesta.ParameterName = "@respuesta";
                dbpRespuesta.Direction = ParameterDirection.ReturnValue;
                dbComando.Parameters.Add(dbpRespuesta);

                dbComando.ExecuteNonQuery();

                return Convert.ToBoolean(dbComando.Parameters["@respuesta"].Value);
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


        #region Pase Lista Generica

        /* ACTUALIZACIÓN MARZO 2017 PARA QUITAR INCONSISTENCIAS */

        public static string insertarListaAsistencia(List<clsEntEmpleadosListaGenerica> lstHorario, clsEntSesion objSesion, ref int registro, ref int error, ref int inconsistencia, ref int cambioIncosistencia)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {

                foreach (var item in lstHorario)
                {
                    dbComando.CommandType = CommandType.StoredProcedure;
                    dbComando.CommandText = "MCS.spuRegistroPaseDeLista";
                    dbComando.Parameters.Clear();

                    DbParameter dbpHorario = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpEmpleado = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpFechaAsis = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpFechaAsisSal = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpEstatus = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpFuncionDia = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpEmpleadoCaptura = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpUsuario = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpAsignacion = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpRegistro = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpError = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpInconsistencia = objConexion.dbProvider.CreateParameter();
                    DbParameter dbpCambioInconsistencia = objConexion.dbProvider.CreateParameter();

                    dbpHorario.DbType = DbType.Int32;
                    dbpHorario.ParameterName = "@idHorario";
                    dbpHorario.Value = item.idHorario;
                    dbComando.Parameters.Add(dbpHorario);

                    dbpEmpleado.DbType = DbType.Guid;
                    dbpEmpleado.ParameterName = "@idEmpleado";
                    dbpEmpleado.Value = item.idEmpleado;
                    dbComando.Parameters.Add(dbpEmpleado);

                    dbpFechaAsis.DbType = DbType.DateTime;
                    dbpFechaAsis.ParameterName = "@fechaAsistencia";
                    dbpFechaAsis.Value = item.fechaAsistencia.ToShortDateString() + " " + item.asiHora;
                    dbComando.Parameters.Add(dbpFechaAsis);

                    dbpFechaAsisSal.DbType = DbType.DateTime;
                    dbpFechaAsisSal.ParameterName = "@fechaAsistenciaSal";
                    dbpFechaAsisSal.Value = Convert.ToDateTime(item.fechaAsistencia.ToShortDateString() + " " + item.asiHoraSalida).AddDays(item.diasAgregados);
                    dbComando.Parameters.Add(dbpFechaAsisSal);

                    dbpEstatus.DbType = DbType.Byte;
                    dbpEstatus.ParameterName = "@idEstatus";
                    dbpEstatus.Value = item.idEstatus;
                    dbComando.Parameters.Add(dbpEstatus);

                    dbpFuncionDia.DbType = DbType.Byte;
                    dbpFuncionDia.ParameterName = "@idFuncionDia";
                    dbpFuncionDia.Value = item.idMonta;
                    dbComando.Parameters.Add(dbpFuncionDia);

                    dbpEmpleadoCaptura.DbType = DbType.Guid;
                    dbpEmpleadoCaptura.ParameterName = "@idEmpleadoCaptura";
                    dbpEmpleadoCaptura.Value = objSesion.usuario.IdEmpleado;
                    dbComando.Parameters.Add(dbpEmpleadoCaptura);

                    dbpServicio.DbType = DbType.Int32;
                    dbpServicio.ParameterName = "@idServicioSICOGUA";
                    dbpServicio.Value = item.idServicio;
                    dbComando.Parameters.Add(dbpServicio);

                    dbpInstalacion.DbType = DbType.Int32;
                    dbpInstalacion.ParameterName = "@idInstalacionSICOGUA";
                    dbpInstalacion.Value = item.idInstalacion;
                    dbComando.Parameters.Add(dbpInstalacion);

                    dbpUsuario.DbType = DbType.Int32;
                    dbpUsuario.ParameterName = "@idUsuarioSICOGUA";
                    dbpUsuario.Value = objSesion.usuario.IdUsuario;
                    dbComando.Parameters.Add(dbpUsuario);

                    dbpAsignacion.DbType = DbType.Int32;
                    dbpAsignacion.ParameterName = "@idFuncionAsignacion";
                    dbpAsignacion.Value = item.idFuncionAsignacion;
                    dbComando.Parameters.Add(dbpAsignacion);

                    dbpRegistro.DbType = DbType.Boolean;
                    dbpRegistro.ParameterName = "@registro";
                    dbpRegistro.Direction = ParameterDirection.Output;
                    dbComando.Parameters.Add(dbpRegistro);

                    dbpError.DbType = DbType.Boolean;
                    dbpError.ParameterName = "@yaHayAsistencia";
                    dbpError.Direction = ParameterDirection.Output;
                    dbComando.Parameters.Add(dbpError);

                    dbpInconsistencia.DbType = DbType.Boolean;
                    dbpInconsistencia.ParameterName = "@incosistencia";
                    dbpInconsistencia.Direction = ParameterDirection.Output;
                    dbComando.Parameters.Add(dbpInconsistencia);

                    dbpCambioInconsistencia.DbType = DbType.Boolean;
                    dbpCambioInconsistencia.ParameterName = "@cambioIncons";
                    dbpCambioInconsistencia.Direction = ParameterDirection.Output;
                    dbComando.Parameters.Add(dbpCambioInconsistencia);

                    dbComando.ExecuteNonQuery();


                    registro = Convert.ToInt32(dbComando.Parameters["@registro"].Value);
                    error = Convert.ToInt32(dbComando.Parameters["@yaHayAsistencia"].Value);
                    inconsistencia = Convert.ToInt32(dbComando.Parameters["@incosistencia"].Value);
                    cambioIncosistencia = Convert.ToInt32(dbComando.Parameters["@cambioIncons"].Value);


                    //clsParametro objParametro = new clsParametro();
                    //objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idHorario", item.idHorario);
                    //objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", item.idEmpleado);
                    //objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsistencia", Convert.ToDateTime(item.fechaAsistencia.ToShortDateString() + " " + item.asiHora));
                    //objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsistenciaSal", (Convert.ToDateTime(item.fechaAsistencia.ToShortDateString() + " " + item.asiHoraSalida)).AddDays(item.diasAgregados));
                    //objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idEstatus", item.idEstatus);
                    //objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idFuncionDia", item.idMonta);
                    //objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoCaptura", objSesion.usuario.IdEmpleado);
                    //objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicioSICOGUA", item.idServicio);
                    //objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacionSICOGUA", item.idInstalacion);
                    //objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idUsuarioSICOGUA", objSesion.usuario.IdUsuario);
                    //objParametro.llenarParametros(ref dbComando, DbType.Byte, ParameterDirection.Output, "@registro", -1);
                    //objParametro.llenarParametros(ref dbComando, DbType.Byte, ParameterDirection.Output, "@yaHayAsistencia", -1);
                    //objParametro.llenarParametros(ref dbComando, DbType.Byte, ParameterDirection.Output, "@incosistencia", -1);
                    //objParametro.llenarParametros(ref dbComando, DbType.Byte, ParameterDirection.Output, "@cambioIncons", -1);
                    ////dbComando.ExecuteNonQuery();
                    //registro = Convert.ToInt32(dbComando.Parameters["@registro"].Value);
                    //error = Convert.ToInt32(dbComando.Parameters["@yaHayAsistencia"].Value);
                    //inconsistencia = Convert.ToInt32(dbComando.Parameters["@incosistencia"].Value);
                    //cambioIncosistencia = Convert.ToInt32(dbComando.Parameters["@cambioIncons"].Value);
                }

                dbTrans.Commit();
                return string.Empty;

            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        //public static string insertarListaAsistencia(List<clsEntEmpleadosListaGenerica> lstHorario, clsEntSesion objSesion, ref int registro, ref int error )
        //{

        //    clsDatConexion objConexion = new clsDatConexion();
        //    DbConnection dbConexion = objConexion.getConexion(objSesion);

        //    DbTransaction dbTrans = dbConexion.BeginTransaction();

        //    DbCommand dbComando = objConexion.dbProvider.CreateCommand();

        //    dbComando.Connection = dbConexion;
        //    dbComando.Transaction = dbTrans;

        //    try
        //    {

        //   foreach (var item in lstHorario)
        //        {
        //        dbComando.CommandType = CommandType.StoredProcedure;
        //        dbComando.CommandText = "rea.spuRegistroPaseDeLista";
        //        dbComando.Parameters.Clear();




        //            clsParametro objParametro = new clsParametro();
        //            objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idHorarioREA", item.idHorario);
        //            objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoREA", item.idEmpleado);
        //            objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsistenciaREA", Convert.ToDateTime(item.fechaAsistencia.ToShortDateString() + " "+item.asiHora));
        //            objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fechaAsistenciaSalREA", (Convert.ToDateTime(item.fechaAsistencia.ToShortDateString() + " " + item.asiHoraSalida)).AddDays(item.diasAgregados));
        //            objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idEstatusREA", item.idEstatus);
        //            objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idFuncionDiaREA", item.idMonta);
        //            objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoCaptura", objSesion.usuario.IdEmpleado);
        //            objParametro.llenarParametros(ref dbComando, DbType.Byte, ParameterDirection.Output, "@registro",-1);
        //            objParametro.llenarParametros(ref dbComando, DbType.Byte, ParameterDirection.Output, "@yaHayAsistencia", -1);
        //            dbComando.ExecuteNonQuery();
        //            registro=Convert.ToInt32(dbComando.Parameters["@registro"].Value);
        //            error = Convert.ToInt32(dbComando.Parameters["@yaHayAsistencia"].Value);
        //        }

        //        dbTrans.Commit();
        //        return string.Empty;

        //    }
        //    catch (Exception Ex)
        //    {
        //        return Ex.Message;
        //    }
        //}

        #endregion

        public static clsEntEmpleadosListaGenerica consultaAsistenciaCompleta(Guid idEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbTransaction dbTrans = dbConexion.BeginTransaction();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();

            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            clsEntEmpleadosListaGenerica objAsistencia = new clsEntEmpleadosListaGenerica();

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "recursoHumano.spuValidarAsistenciaCompletaREA";
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
                        objAsistencia = new clsEntEmpleadosListaGenerica
                        {
                            idEmpleado = Guid.Parse(drEmpleado["idEmpleado"].ToString()),
                            idHorario = Convert.ToInt32(drEmpleado["idHorario"].ToString()),
                            fechaAsistencia = Convert.ToDateTime(drEmpleado["asiEntrada"].ToString()),
                            fechaAsistenciaSal = drEmpleado["asiSalida"].ToString() == "" ? Convert.ToDateTime(null) : Convert.ToDateTime(drEmpleado["asiSalida"].ToString()),
                            tipoAsistencia = drEmpleado["estDescripcion"].ToString()
                        };

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
            return objAsistencia;
        }

        /* FIN ACTUALIZACIÓN */
    }
}
