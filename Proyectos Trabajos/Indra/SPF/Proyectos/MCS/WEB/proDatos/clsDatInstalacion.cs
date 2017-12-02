using System;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Collections.Generic;

namespace SICOGUA.Datos
{
    public class clsDatInstalacion
    {
        public static bool insertarInstalacion(clsEntInstalacion objInstalacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spInsertarInstalacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpInstalacion.DbType = DbType.String;
                dbpInstalacion.ParameterName = "@insNombre";
                dbpInstalacion.Value = objInstalacion.InsNombre;
                dbComando.Parameters.Add(dbpInstalacion);

                dbComando.ExecuteNonQuery();

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

        public static bool actualizarInstalacion(clsEntInstalacion objInstalacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spActualizarInstalacion";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objInstalacion.IdInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicioAntes", objInstalacion.IdServicioAntes);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicioDespues", objInstalacion.IdServicio);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@insNombre", objInstalacion.InsNombre);

                dbComando.ExecuteNonQuery();

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

        public static bool eliminarInstalacion(clsEntInstalacion objInstalacion, clsEntServicio objServicio, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spEliminarInstalacion";
                dbComando.Parameters.Clear();
  
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
   
                dbpServicio.DbType = DbType.String;
                dbpServicio.ParameterName = "@serDescripcion";
                dbpServicio.Value = objServicio.serDescripcion;
                dbComando.Parameters.Add(dbpServicio);
                     
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = objInstalacion.IdInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbComando.ExecuteNonQuery();

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



        public static bool insertarInstalacionMod(clsEntInstalacion objInstalacion,clsEntZonaHoraria objZonaHoraria ,clsEntDomicilio objDomicilio,List<clsEntResponsable> lstResponsable, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spInsertaInstalacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpOperacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsFechaInicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsFechaFin = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsLongitud = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsLatitud = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsConvenio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsElementosTurno = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsElementosArmados = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsElementosMasculino = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsElementosFemenino= objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsColindancias = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsFunciones = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInsDescripcion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdClasificacion= objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdTipoInstalacion = objConexion.dbProvider.CreateParameter();

                dbpIdClasificacion.DbType = DbType.Int32;
                dbpIdClasificacion.ParameterName = "@idClasificacion";
                dbpIdClasificacion.Value = objInstalacion.idClasificacion;
                dbComando.Parameters.Add(dbpIdClasificacion);

                dbpOperacion.DbType = DbType.Int32;
                dbpOperacion.ParameterName = "@operacion";
                dbpOperacion.Value = objInstalacion.operacion;
                dbComando.Parameters.Add(dbpOperacion);

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.Direction = ParameterDirection.Output;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbpIdZona.DbType = DbType.Int16;
                dbpIdZona.ParameterName = "@idZona";
                dbpIdZona.Value = objInstalacion.idZona;
                dbComando.Parameters.Add(dbpIdZona);

                dbpInstalacion.DbType = DbType.String;
                dbpInstalacion.ParameterName = "@insNombre";
                dbpInstalacion.Value = objInstalacion.InsNombre ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpInstalacion);

                dbpInsFechaInicio.DbType = DbType.DateTime;
                dbpInsFechaInicio.ParameterName = "@insFechaInicio";
                dbpInsFechaInicio.Value = objInstalacion.insFechaInicio;
                dbComando.Parameters.Add(dbpInsFechaInicio);

                dbpInsFechaFin.DbType = DbType.DateTime;
                dbpInsFechaFin.ParameterName = "@insFechaFin";
               dbpInsFechaFin.Value = objInstalacion.insFechaFin.ToShortDateString() == "01/01/0001" || objInstalacion.insFechaFin.ToShortDateString() == "01/01/1900"  ? (object)DBNull.Value:objInstalacion.insFechaFin;
               dbComando.Parameters.Add(dbpInsFechaFin);

                dbpInsLongitud.DbType = DbType.Decimal;
                dbpInsLongitud.ParameterName = "@insLongitud";
                dbpInsLongitud.Value = objInstalacion.insLogitud == 0 ? (object)DBNull.Value : objInstalacion.insLogitud;
                dbComando.Parameters.Add(dbpInsLongitud);

                

                dbpInsLatitud.DbType = DbType.Decimal;
                dbpInsLatitud.ParameterName = "@insLatitud";
                dbpInsLatitud.Value = objInstalacion.insLatitud == 0 ? (object)DBNull.Value : objInstalacion.insLatitud;
                dbComando.Parameters.Add(dbpInsLatitud);


                dbpInsConvenio.DbType = DbType.String;
                dbpInsConvenio.ParameterName = "@insConvenio";
                dbpInsConvenio.Value = objInstalacion.insConvenio ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpInsConvenio);

                dbpInsElementosTurno.DbType = DbType.Int32;
                dbpInsElementosTurno.ParameterName = "@insElementosTurno";
                dbpInsElementosTurno.Value = objInstalacion.insElementosTurno == 0 ? (object)DBNull.Value : objInstalacion.insElementosTurno;
                dbComando.Parameters.Add(dbpInsElementosTurno);

                dbpInsElementosArmados.DbType = DbType.Int32;
                dbpInsElementosArmados.ParameterName = "@insElementosArmados";
                dbpInsElementosArmados.Value = objInstalacion.insElementosArmados == 0 ? (object)DBNull.Value : objInstalacion.insElementosArmados;
                dbComando.Parameters.Add(dbpInsElementosArmados);

                dbpInsElementosMasculino.DbType = DbType.Int32;
                dbpInsElementosMasculino.ParameterName = "@insElementosMasculino";
                dbpInsElementosMasculino.Value = objInstalacion.insElementosMasculino == 0 ? (object)DBNull.Value : objInstalacion.insElementosMasculino;
                dbComando.Parameters.Add(dbpInsElementosMasculino);

                dbpInsElementosFemenino.DbType = DbType.Int32;
                dbpInsElementosFemenino.ParameterName = "@insElementosFemenino";
                dbpInsElementosFemenino.Value = objInstalacion.insElementosFemenino == 0 ? (object)DBNull.Value : objInstalacion.insElementosFemenino;
                dbComando.Parameters.Add(dbpInsElementosFemenino);
                
                dbpInsColindancias.DbType = DbType.String;
                dbpInsColindancias.ParameterName = "@insColindancias";
                dbpInsColindancias.Value = objInstalacion.insColindancias ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpInsColindancias);

                dbpInsFunciones.DbType = DbType.String;
                dbpInsFunciones.ParameterName = "@insFunciones";
                dbpInsFunciones.Value = objInstalacion.insFunciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpInsFunciones);

                dbpInsDescripcion.DbType = DbType.String;
                dbpInsDescripcion.ParameterName = "@insDescripcion";
                dbpInsDescripcion.Value = objInstalacion.insDescripcion ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpInsDescripcion);

                dbpIdTipoInstalacion.DbType = DbType.Int32;
                dbpIdTipoInstalacion.ParameterName = "@idTipoInstalacion";
                dbpIdTipoInstalacion.Value = objInstalacion.idTipoInstalacion== 0 ? (object)DBNull.Value : objInstalacion.idTipoInstalacion;
                dbComando.Parameters.Add(dbpIdTipoInstalacion);


                dbComando.ExecuteNonQuery();

                int IdInstalacion = (int)dbComando.Parameters["@idInstalacion"].Value;
                clsDatInstalacion.insertarDomicilioInstalacion(IdInstalacion, objInstalacion, objDomicilio, dbConexion, dbTrans);

        
                clsDatInstalacion.insertarZonaHoraria(IdInstalacion, objInstalacion, objZonaHoraria, dbConexion, dbTrans);


                foreach (clsEntResponsable objResponsable in lstResponsable)
                {
                    clsDatInstalacion.insertarResponsableInstalacion(IdInstalacion, objInstalacion, objResponsable, dbConexion, dbTrans);
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

        public static bool insertarDomicilioInstalacion(int idInstalacion, clsEntInstalacion objInstalacion, clsEntDomicilio objDomicilio, DbConnection dbConexion, DbTransaction dbTrans)
        {


            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;


            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spInsertaDomicilioInstalacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdDomicilio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdAsentamiento = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomCalle = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomNumeroExterior = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomNumeroInterior = objConexion.dbProvider.CreateParameter();

                dbpIdDomicilio.DbType = DbType.Int32;
                dbpIdDomicilio.ParameterName = "@idDomicilio";
                dbpIdDomicilio.Value = objDomicilio.idDomicilio;
                dbComando.Parameters.Add(dbpIdDomicilio);

                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdAsentamiento.DbType = DbType.Int32;
                dbpIdAsentamiento.ParameterName = "@idAsentamiento";
                dbpIdAsentamiento.Value = objDomicilio.idAsentamiento;
                dbComando.Parameters.Add(dbpIdAsentamiento);

                dbpdomCalle.DbType = DbType.String;
                dbpdomCalle.ParameterName = "@domCalle";
                dbpdomCalle.Value = objDomicilio.domCalle ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpdomCalle);

                dbpdomNumeroExterior.DbType = DbType.String;
                dbpdomNumeroExterior.ParameterName = "@domNumeroExterior";
                dbpdomNumeroExterior.Value = objDomicilio.domNumeroExterior ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpdomNumeroExterior);

                dbpdomNumeroInterior.DbType = DbType.String;
                dbpdomNumeroInterior.ParameterName = "@domNumeroInterior";
                dbpdomNumeroInterior.Value = objDomicilio.domNumeroInterior ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpdomNumeroInterior);
              


                dbComando.ExecuteNonQuery();

   



                return true;
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

        //INSERTAR ZONA HORARIA
        public static bool insertarZonaHoraria(int idInstalacion, clsEntInstalacion objInstalacion, clsEntZonaHoraria objZonaHoraria, DbConnection dbConexion, DbTransaction dbTrans)
        {


            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

           
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spInsertaInstalacionZonaHoraria";
                dbComando.Parameters.Clear();

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdZonaHoraria = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidInstalacionZonaHoraria = objConexion.dbProvider.CreateParameter();


                dbpIdZonaHoraria.DbType = DbType.Int32;
                dbpIdZonaHoraria.ParameterName = "@idZonaHoraria";
                dbpIdZonaHoraria.Value = objZonaHoraria.idZonaHoraria;
                dbComando.Parameters.Add(dbpIdZonaHoraria);

                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);


                dbpidInstalacionZonaHoraria.DbType = DbType.String;
                dbpidInstalacionZonaHoraria.ParameterName = "@idInstalacionZonaHoraria";
                dbpidInstalacionZonaHoraria.Value = (object)DBNull.Value;
                dbComando.Parameters.Add(dbpidInstalacionZonaHoraria);


                dbComando.ExecuteNonQuery();





                return true;
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




        public static bool insertarResponsableInstalacion(int idInstalacion, clsEntInstalacion objInstalacion, clsEntResponsable objResponsable, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spInsertaResponsableInstalacion";
                dbComando.Parameters.Clear();


                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpriObservaciones = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = objResponsable.IdEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpriObservaciones.DbType = DbType.String;
                dbpriObservaciones.ParameterName = "@riObservaciones";
                dbpriObservaciones.Value = objResponsable.riObservaciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpriObservaciones);

            


                dbComando.ExecuteNonQuery();



       
                return true;
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






        public static void consultaInstalacionGeneral(clsEntInstalacion objInstalacion,clsEntDomicilio objDomicilio,clsEntResponsable objResponsable, ref DataTable dsInstalacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "servicio.spBuscaInstalacionGeneral";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsNombre = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsFechaInicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsFechaFin = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsLongitud = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsLatitud = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsConvenio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsElementosTurno = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsElementosArmados = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsElementosMasculino = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsElementosFemenino = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsColindancias = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsFunciones = objConexion.dbProvider.CreateParameter();
                DbParameter dbpinsDescripcion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidAsentamiento = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidEstado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidMunicipio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomCalle = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomNumeroExterior = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomNumeroInterior = objConexion.dbProvider.CreateParameter();
                DbParameter dbpriObservaciones = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdTipoInstalacion = objConexion.dbProvider.CreateParameter();	


                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio == 0 ? (object)DBNull.Value : objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpidZona.DbType = DbType.Int32;
                dbpidZona.Direction = ParameterDirection.Input;
                dbpidZona.ParameterName = "@idZona";
                dbpidZona.Value = objInstalacion.idZona == 0 ? (object)DBNull.Value : objInstalacion.idZona;
                dbComando.Parameters.Add(dbpidZona);

                dbpinsNombre.DbType = DbType.String;
                dbpinsNombre.Direction = ParameterDirection.Input;
                dbpinsNombre.ParameterName = "@insNombre";
                dbpinsNombre.Value = objInstalacion.InsNombre ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpinsNombre);

                dbpinsFechaInicio.DbType = DbType.DateTime;
                dbpinsFechaInicio.Direction = ParameterDirection.Input;
                dbpinsFechaInicio.ParameterName = "@insFechaInicio";
                dbpinsFechaInicio.Value = objInstalacion.insFechaInicio.ToShortDateString() == "01/01/0001" || objInstalacion.insFechaInicio.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objInstalacion.insFechaInicio;
                dbComando.Parameters.Add(dbpinsFechaInicio);

                dbpinsFechaFin.DbType = DbType.DateTime;
                dbpinsFechaFin.Direction = ParameterDirection.Input;
                dbpinsFechaFin.ParameterName = "@insFechaFin";
                dbpinsFechaFin.Value = objInstalacion.insFechaFin.ToShortDateString() == "01/01/0001" || objInstalacion.insFechaFin.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objInstalacion.insFechaFin;
                dbComando.Parameters.Add(dbpinsFechaFin);

                dbpinsLongitud.DbType = DbType.Decimal;
                dbpinsLongitud.Direction = ParameterDirection.Input;
                dbpinsLongitud.ParameterName = "@insLongitud";
                dbpinsLongitud.Value = objInstalacion.insLogitud == 0 ? (object)DBNull.Value : objInstalacion.insLogitud;
                dbComando.Parameters.Add(dbpinsLongitud);

                dbpinsLatitud.DbType = DbType.Decimal;
                dbpinsLatitud.Direction = ParameterDirection.Input;
                dbpinsLatitud.ParameterName = "@insLatitud";
                dbpinsLatitud.Value = objInstalacion.insLatitud == 0 ? (object)DBNull.Value : objInstalacion.insLatitud;
                dbComando.Parameters.Add(dbpinsLatitud);

                dbpinsConvenio.DbType = DbType.String;
                dbpinsConvenio.Direction = ParameterDirection.Input;
                dbpinsConvenio.ParameterName = "@insConvenio";
                dbpinsConvenio.Value = objInstalacion.insConvenio ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpinsConvenio);

                dbpinsElementosTurno.DbType = DbType.Int32;
                dbpinsElementosTurno.Direction = ParameterDirection.Input;
                dbpinsElementosTurno.ParameterName = "@insElementosTurno";
                dbpinsElementosTurno.Value = objInstalacion.insElementosTurno == 0 ? (object)DBNull.Value : objInstalacion.insElementosTurno;
                dbComando.Parameters.Add(dbpinsElementosTurno);

                dbpinsElementosArmados.DbType = DbType.Int32;
                dbpinsElementosArmados.Direction = ParameterDirection.Input;
                dbpinsElementosArmados.ParameterName = "@insElementosArmados";
                dbpinsElementosArmados.Value = objInstalacion.insElementosArmados == 0 ? (object)DBNull.Value : objInstalacion.insElementosArmados;
                dbComando.Parameters.Add(dbpinsElementosArmados);

                dbpinsElementosMasculino.DbType = DbType.Int32;
                dbpinsElementosMasculino.Direction = ParameterDirection.Input;
                dbpinsElementosMasculino.ParameterName = "@insElementosMasculino";
                dbpinsElementosMasculino.Value = objInstalacion.insElementosMasculino == 0 ? (object)DBNull.Value : objInstalacion.insElementosMasculino;
                dbComando.Parameters.Add(dbpinsElementosMasculino);

                dbpinsElementosFemenino.DbType = DbType.Int32;
                dbpinsElementosFemenino.Direction = ParameterDirection.Input;
                dbpinsElementosFemenino.ParameterName = "@insElementosFemenino";
                dbpinsElementosFemenino.Value = objInstalacion.insElementosFemenino == 0 ? (object)DBNull.Value : objInstalacion.insElementosFemenino;
                dbComando.Parameters.Add(dbpinsElementosFemenino);

                dbpinsColindancias.DbType = DbType.String;
                dbpinsColindancias.Direction = ParameterDirection.Input;
                dbpinsColindancias.ParameterName = "@insColindancias";
                dbpinsColindancias.Value = objInstalacion.insColindancias ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpinsColindancias);

                dbpinsFunciones.DbType = DbType.String;
                dbpinsFunciones.Direction = ParameterDirection.Input;
                dbpinsFunciones.ParameterName = "@insFunciones";
                dbpinsFunciones.Value = objInstalacion.insFunciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpinsFunciones);

                dbpinsDescripcion.DbType = DbType.String;
                dbpinsDescripcion.Direction = ParameterDirection.Input;
                dbpinsDescripcion.ParameterName = "@insDescripcion";
                dbpinsDescripcion.Value = objInstalacion.insDescripcion ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpinsDescripcion);

                dbpidAsentamiento.DbType = DbType.Int32;
                dbpidAsentamiento.Direction = ParameterDirection.Input;
                dbpidAsentamiento.ParameterName = "@idAsentamiento";
                dbpidAsentamiento.Value = objDomicilio.idAsentamiento == 0 ? (object)DBNull.Value : objDomicilio.idAsentamiento;
                dbComando.Parameters.Add(dbpidAsentamiento);

                dbpidEstado.DbType = DbType.Int32;
                dbpidEstado.Direction = ParameterDirection.Input;
                dbpidEstado.ParameterName = "@idEstado";
                dbpidEstado.Value = objDomicilio.idEstado == 0 ? (object)DBNull.Value : objDomicilio.idEstado;
                dbComando.Parameters.Add(dbpidEstado);

                dbpidMunicipio.DbType = DbType.Int32;
                dbpidMunicipio.Direction = ParameterDirection.Input;
                dbpidMunicipio.ParameterName = "@idMunicipio";
                dbpidMunicipio.Value = objDomicilio.idMunicipio == 0 ? (object)DBNull.Value : objDomicilio.idMunicipio;
                dbComando.Parameters.Add(dbpidMunicipio);

                dbpdomCalle.DbType = DbType.String;
                dbpdomCalle.Direction = ParameterDirection.Input;
                dbpdomCalle.ParameterName = "@domCalle";
                dbpdomCalle.Value = objDomicilio.domCalle ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpdomCalle);

                dbpdomNumeroExterior.DbType = DbType.String;
                dbpdomNumeroExterior.Direction = ParameterDirection.Input;
                dbpdomNumeroExterior.ParameterName = "@domNumeroExterior";
                dbpdomNumeroExterior.Value = objDomicilio.domNumeroExterior ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpdomNumeroExterior);

                dbpdomNumeroInterior.DbType = DbType.String;
                dbpdomNumeroInterior.Direction = ParameterDirection.Input;
                dbpdomNumeroInterior.ParameterName = "@domNumeroInterior";
                dbpdomNumeroInterior.Value = objDomicilio.domNumeroInterior ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpdomNumeroInterior);

                dbpriObservaciones.DbType = DbType.String;
                dbpriObservaciones.Direction = ParameterDirection.Input;
                dbpriObservaciones.ParameterName = "@riObservaciones";
                dbpriObservaciones.Value = objResponsable.riObservaciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpriObservaciones);

                dbpidEmpleado.DbType = DbType.Guid;
                dbpidEmpleado.Direction = ParameterDirection.Input;
                dbpidEmpleado.ParameterName = "@idEmpleado";

                dbpIdTipoInstalacion.DbType = DbType.Int32;
                dbpIdTipoInstalacion.ParameterName = "@idTipoInstalacion";
                dbpIdTipoInstalacion.Value = objInstalacion.idTipoInstalacion == 0 ? (object)DBNull.Value : objInstalacion.idTipoInstalacion;
                dbComando.Parameters.Add(dbpIdTipoInstalacion);
                                                                            
                dbpidEmpleado.Value = objResponsable.IdEmpleado == new Guid("00000000-0000-0000-0000-000000000000") ? (object)DBNull.Value : objResponsable.IdEmpleado;
                dbComando.Parameters.Add(dbpidEmpleado);

                
                                
                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(dsInstalacion);

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







         public static void consultaInstalacion(clsEntInstalacion objInstalacion, ref DataSet dsInstalacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "servicio.spBuscaInstalacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidZona = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objInstalacion.IdServicio == 0 ? (object)DBNull.Value : objInstalacion.IdServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpidInstalacion.DbType = DbType.Int32;
                dbpidInstalacion.Direction = ParameterDirection.Input;
                dbpidInstalacion.ParameterName = "@idInstalacion";
                dbpidInstalacion.Value = objInstalacion.IdInstalacion == 0 ? (object)DBNull.Value : objInstalacion.IdInstalacion;
                dbComando.Parameters.Add(dbpidInstalacion);

                dbpidZona.DbType = DbType.Int32;
                dbpidZona.Direction = ParameterDirection.Input;
                dbpidZona.ParameterName = "@idZona";
                dbpidZona.Value = objInstalacion.idZona == 0 ? (object)DBNull.Value : objInstalacion.idZona;
                dbComando.Parameters.Add(dbpidZona);

                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(dsInstalacion);
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



         public static void buscaResponsableInstalacion(clsEntInstalacion objInstalacion, ref DataSet dsResponsable, clsEntSesion objSesion)
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
                 dbComando.CommandText = "servicio.spBuscaResponsableInstalacion";
                 dbComando.Parameters.Clear();


                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();


                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = objInstalacion.IdServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpIdInstalacion.DbType = DbType.Int32;
                 dbpIdInstalacion.ParameterName = "@idInstalacion";
                 dbpIdInstalacion.Value = objInstalacion.IdInstalacion;
                 dbComando.Parameters.Add(dbpIdInstalacion);


                 dbAdapter.SelectCommand = dbComando;
                 dbAdapter.Fill(dsResponsable);
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








         public static void consultaCatalogoEquipo(clsEntInstalacion objInstalacion, ref DataSet dsEquipo, clsEntSesion objSesion)
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
                 dbComando.CommandText = "servicio.spBuscaEquipoInstalacion";
                 dbComando.Parameters.Clear();

                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();

                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = objInstalacion.IdServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpIdInstalacion.DbType = DbType.Int32;
                 dbpIdInstalacion.ParameterName = "@idInstalacion";
                 dbpIdInstalacion.Value = objInstalacion.IdInstalacion;
                 dbComando.Parameters.Add(dbpIdInstalacion);

                 dbAdapter.SelectCommand = dbComando;
                 dbAdapter.Fill(dsEquipo);
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





         public static void consultaInventario(clsEntInstalacion objInstalacion,ref DataSet dsEquipo, clsEntSesion objSesion)
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
                 dbComando.CommandText = "servicio.spConsultarInventario";
                 dbComando.Parameters.Clear();

                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();


                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.Direction = ParameterDirection.Input;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = objInstalacion.IdServicio == 0 ? (object)DBNull.Value : objInstalacion.IdServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpidInstalacion.DbType = DbType.Int32;
                 dbpidInstalacion.Direction = ParameterDirection.Input;
                 dbpidInstalacion.ParameterName = "@idInstalacion";
                 dbpidInstalacion.Value = objInstalacion.IdInstalacion == 0 ? (object)DBNull.Value : objInstalacion.IdInstalacion;
                 dbComando.Parameters.Add(dbpidInstalacion);


                 dbAdapter.SelectCommand = dbComando;
                 dbAdapter.Fill(dsEquipo);
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








         public static void consultaInventarioDetallado(clsEntInstalacion objInstalacion, ref DataSet dsEquipo, clsEntSesion objSesion)
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
                 dbComando.CommandText = "servicio.spConsultarInventarioDetallado";
                 dbComando.Parameters.Clear();

                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpidInstalacionEquipo = objConexion.dbProvider.CreateParameter();


                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.Direction = ParameterDirection.Input;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = objInstalacion.IdServicio == 0 ? (object)DBNull.Value : objInstalacion.IdServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpidInstalacion.DbType = DbType.Int32;
                 dbpidInstalacion.Direction = ParameterDirection.Input;
                 dbpidInstalacion.ParameterName = "@idInstalacion";
                 dbpidInstalacion.Value = objInstalacion.IdInstalacion == 0 ? (object)DBNull.Value : objInstalacion.IdInstalacion;
                 dbComando.Parameters.Add(dbpidInstalacion);

                 dbpidInstalacionEquipo.DbType = DbType.Int32;
                 dbpidInstalacionEquipo.Direction = ParameterDirection.Input;
                 dbpidInstalacionEquipo.ParameterName = "@idInstalacionEquipo";
                 dbpidInstalacionEquipo.Value = objInstalacion.IdInstalacion == 0 ? (object)DBNull.Value : objInstalacion.idInstalacionEquipo;
                 dbComando.Parameters.Add(dbpidInstalacionEquipo);


                 dbAdapter.SelectCommand = dbComando;
                 dbAdapter.Fill(dsEquipo);
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









         public static bool insertarInventario(ref int idInstalacionEquipo ,clsEntEquipoCatalogo obj, clsEntSesion objSesion)
         {
             clsDatConexion objConexion = new clsDatConexion();
             DbConnection dbConexion = objConexion.getConexion(objSesion);
             DbTransaction dbTrans = dbConexion.BeginTransaction();

             DbCommand dbComando = objConexion.dbProvider.CreateCommand();
             dbComando.Connection = dbConexion;
             dbComando.Transaction = dbTrans;
             int idUsuario = objSesion.usuario.IdUsuario;
             try
             {
                 dbComando.CommandType = CommandType.StoredProcedure;
                 dbComando.CommandText = "servicio.spInsertaEquipoInstalacion";
                 dbComando.Parameters.Clear();

                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIeFechaInicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIeFechaFin = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpBandera = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpidInstalacionEquipo = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpidInstalacionEquipoA = objConexion.dbProvider.CreateParameter();

                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = obj.idServicio==0 ?(object)DBNull.Value:obj.idServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpInstalacion.DbType = DbType.Int32;
                 dbpInstalacion.ParameterName = "@idInstalacion";
                 dbpInstalacion.Value = obj.idInstalacion == 0 ? (object)DBNull.Value : obj.idInstalacion;
                 dbComando.Parameters.Add(dbpInstalacion);

                 dbpIeFechaInicio.DbType = DbType.DateTime;
                 dbpIeFechaInicio.ParameterName = "@ieFechaInicio";
                 dbpIeFechaInicio.Value = Convert.ToDateTime(obj.ieFechaInicio);
                 dbComando.Parameters.Add(dbpIeFechaInicio);

                 dbpIeFechaFin.DbType = DbType.DateTime;
                 dbpIeFechaFin.ParameterName = "@ieFechaFin";
                 dbpIeFechaFin.Value = obj.ieFechaFin == string.Empty ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(obj.ieFechaFin);
                 dbComando.Parameters.Add(dbpIeFechaFin);

                 dbpBandera.DbType = DbType.Int32;
                 dbpBandera.ParameterName = "@bandera";
                 dbpBandera.Value = obj.semaforo;
                 dbComando.Parameters.Add(dbpBandera);


                 dbpidInstalacionEquipo.DbType = DbType.Int32;
                 dbpidInstalacionEquipo.Direction = ParameterDirection.Output;
                 dbpidInstalacionEquipo.ParameterName = "@idInstalacionEquipo";
                 dbComando.Parameters.Add(dbpidInstalacionEquipo);

                 dbpidInstalacionEquipoA.DbType = DbType.Int32;
                 dbpidInstalacionEquipoA.ParameterName = "@idInstalacionEquipoA";
                 dbpidInstalacionEquipoA.Value = obj.idInstalacionEquipo == null ? 0 : obj.idInstalacionEquipo;
                 dbComando.Parameters.Add(dbpidInstalacionEquipoA);




                 dbComando.ExecuteNonQuery();
                 idInstalacionEquipo = Convert.ToInt32(dbComando.Parameters["@idInstalacionEquipo"].Value);
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


         public static bool insertarEquipoInventario(int idInstalacionEquipo,clsEntEquipoCatalogo obj, clsEntSesion objSesion)
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
                 dbComando.CommandText = "servicio.spInsertaEquipoInstalacionInventario";
                 dbComando.Parameters.Clear();

                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdInstalacionEquipo = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdEquipo = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIecCantidad = objConexion.dbProvider.CreateParameter();

                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = obj.idServicio == 0 ? (object)DBNull.Value : obj.idServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpInstalacion.DbType = DbType.Int32;
                 dbpInstalacion.ParameterName = "@idInstalacion";
                 dbpInstalacion.Value = obj.idInstalacion == 0 ? (object)DBNull.Value : obj.idInstalacion;
                 dbComando.Parameters.Add(dbpInstalacion);

                 dbpIdInstalacionEquipo.DbType = DbType.Int32;
                 dbpIdInstalacionEquipo.ParameterName = "@idInstalacionEquipo";
                 dbpIdInstalacionEquipo.Value = idInstalacionEquipo == 0 ? (object)DBNull.Value : idInstalacionEquipo;
                 dbComando.Parameters.Add(dbpIdInstalacionEquipo);

                 dbpIdEquipo.DbType = DbType.Int32;
                 dbpIdEquipo.ParameterName = "@idEquipo";
                 dbpIdEquipo.Value = obj.idEquipo == 0 ? (object)DBNull.Value : obj.idEquipo;
                 dbComando.Parameters.Add(dbpIdEquipo);

                 dbpIecCantidad.DbType = DbType.Int32;
                 dbpIecCantidad.ParameterName = "@iecCantidad";
                 dbpIecCantidad.Value =  obj.ieCantidad;
                 dbComando.Parameters.Add(dbpIecCantidad);

                 dbComando.ExecuteNonQuery();

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

         public static bool consultarPermisos(clsEntEquipoCatalogo obj, clsEntSesion objSesion, ref DataSet dsEquipo)
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
                 dbComando.CommandText = "servicio.spBuscarPermisosInstalacion";
                 dbComando.Parameters.Clear();

                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
   
                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdServicio.Value = obj.idServicio == 0 ? (object)DBNull.Value : obj.idServicio;
                 dbComando.Parameters.Add(dbpIdServicio);

                 dbpInstalacion.DbType = DbType.Int32;
                 dbpInstalacion.ParameterName = "@idInstalacion";
                 dbpInstalacion.Value = obj.idInstalacion == 0 ? (object)DBNull.Value : obj.idInstalacion;
                 dbComando.Parameters.Add(dbpInstalacion);

                 dbpIdUsuario.DbType = DbType.Int32;
                 dbpIdUsuario.ParameterName = "@idUsuario";
                 dbpIdUsuario.Value =  idUsuario;
                 dbComando.Parameters.Add(dbpIdUsuario);

                 dbAdapter.SelectCommand = dbComando;
                 dbAdapter.Fill(dsEquipo);
        
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

         public static List<clsEntInstalacion> consultarInstalacionREA(int idServicio, int idInstalacion, char chVigente,clsEntSesion objSesion)
         {
             clsDatConexion objConexion = new clsDatConexion();
             DbConnection dbConexion = objConexion.getConexion(objSesion);
             DbCommand dbComando = objConexion.dbProvider.CreateCommand();
             DbTransaction dbTrans = dbConexion.BeginTransaction();

             int idUsuario = objSesion.usuario.IdUsuario;
             dbComando.Transaction = dbTrans;

             try
             {
                 dbComando.CommandType = CommandType.StoredProcedure;
                 dbComando.CommandText = "catalogo.spuBusquedaSerInst";
                 dbComando.Connection = dbConexion;
                 dbComando.Parameters.Clear();

                 DbParameter dbpVigente = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                 DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
                 

                 dbpVigente.DbType = DbType.String;
                 dbpIdServicio.DbType = DbType.Int32;
                 dbpIdInstalacion.DbType = DbType.Int32;
                 dbpIdUsuario.DbType = DbType.Int32;
                 

                 dbpVigente.ParameterName = "@insVigente";
                 dbpIdServicio.ParameterName = "@idServicio";
                 dbpIdInstalacion.ParameterName = "@idInstalacion";
                 dbpIdUsuario.ParameterName = "@idUsuario";

                 dbpVigente.Direction = ParameterDirection.Input;
                 dbpIdServicio.Direction = ParameterDirection.Input;
                 dbpIdInstalacion.Direction = ParameterDirection.Input;
                 dbpIdUsuario.Direction = ParameterDirection.Input;

                 dbpVigente.Value = chVigente;
                 dbpIdServicio.Value = idServicio;
                 dbpIdInstalacion.Value = idInstalacion;
                 dbpIdUsuario.Value = idUsuario;

                 dbComando.Parameters.Add(dbpVigente);
                 dbComando.Parameters.Add(dbpIdServicio);
                 dbComando.Parameters.Add(dbpIdInstalacion);
                 dbComando.Parameters.Add(dbpIdUsuario);

                 dbComando.ExecuteNonQuery();

                 IDataReader idrDatosInstalacion = dbComando.ExecuteReader();
                 List<clsEntInstalacion> lsInstalacion = new List<clsEntInstalacion>();

                 while (idrDatosInstalacion.Read())
                 {
                     clsEntInstalacion objInstalacion = new clsEntInstalacion();
                     clsEntZona objZona = new clsEntZona();
                     clsEntServicio objServicio = new clsEntServicio();

                     objInstalacion.Servicio = objServicio;
                     objInstalacion.Zona = objZona;

                     objInstalacion.Servicio.idServicio=conversiones.enteroNoNulo(idrDatosInstalacion["idServicio"]);
                     objInstalacion.IdInstalacion = conversiones.enteroNoNulo(idrDatosInstalacion["idInstalacion"]);
                     objInstalacion.Zona.ZonDescripcion = conversiones.cadena(idrDatosInstalacion["zonDescripcion"]);
                     objInstalacion.Servicio.serDescripcion = conversiones.cadena(idrDatosInstalacion["serDescripcion"]);
                     objInstalacion.InsNombre = conversiones.cadena(idrDatosInstalacion["insNombre"]);
                     objInstalacion.insVigente = conversiones.boleanoNoNulo(idrDatosInstalacion["insVigente"]);

                     lsInstalacion.Add(objInstalacion);

 
                 }
                 idrDatosInstalacion.Close();
                 return lsInstalacion;
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
             finally
             {
                 clsDatConexion.cerrarTransaccion(dbConexion);
             }

         }

    }
}
