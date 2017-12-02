using System;
using System.Data;
using System.Data.Common;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Collections.Generic;

namespace SICOGUA.Datos
{
    public class clsDatServicio
    {
        public static bool insertarServicio(clsEntServicio objServicio, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spInsertarServicio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = objServicio.idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpServicio.DbType = DbType.String;
                dbpServicio.ParameterName = "@serDescripcion";
                dbpServicio.Value = objServicio.serDescripcion;
                dbComando.Parameters.Add(dbpServicio);

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

        public static bool actualizarServicio(clsEntServicio objServicio, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spActualizarServicio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objServicio.idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = objServicio.idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpServicio.DbType = DbType.String;
                dbpServicio.ParameterName = "@serDescripcion";
                dbpServicio.Value = objServicio.serDescripcion;
                dbComando.Parameters.Add(dbpServicio);

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

        public static bool eliminarServicio(clsEntServicio objServicio, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spEliminarServicio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objServicio.idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

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

        public static bool insertarServicioMod(clsEntServicio objServicio, clsEntDomicilio objDomicilio, List<clsEntResponsable> lstResponsable, clsEntSesion objSesion)
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
                dbComando.CommandText = "catalogo.spInsertaServicio";
                dbComando.Parameters.Clear();

                DbParameter dbpOperacion= objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdTipoServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerDescripcion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerRazonSocial = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerRfc = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerFechaInicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerFechaFin = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerObservaciones = objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerLogotipo= objConexion.dbProvider.CreateParameter();
                DbParameter dbpSerPaginaWeb = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdCategoriaServicio= objConexion.dbProvider.CreateParameter();


                dbpOperacion.DbType = DbType.Int32;
                dbpOperacion.ParameterName = "@operacion";
                dbpOperacion.Value = objServicio.operacion;
                dbComando.Parameters.Add(dbpOperacion);

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Output;
                dbpIdServicio.ParameterName = "@idServicio";
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdTipoServicio.DbType = DbType.Int32;
                dbpIdTipoServicio.ParameterName = "@idTipoServicio";
                dbpIdTipoServicio.Value = objServicio.IdTipoServicio;
                dbComando.Parameters.Add(dbpIdTipoServicio);

                dbpSerDescripcion.DbType = DbType.String;
                dbpSerDescripcion.ParameterName = "@serDescripcion";
                dbpSerDescripcion.Value = objServicio.serDescripcion ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpSerDescripcion);

                dbpSerRazonSocial.DbType = DbType.String;
                dbpSerRazonSocial.ParameterName = "@serRazonSocial";
                dbpSerRazonSocial.Value = objServicio.serRazonSocial ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpSerRazonSocial);

                dbpSerRfc.DbType = DbType.String;
                dbpSerRfc.ParameterName = "@serRfc";
                dbpSerRfc.Value = objServicio.serRfc ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpSerRfc);

                dbpSerFechaInicio.DbType = DbType.DateTime;
                dbpSerFechaInicio.ParameterName = "@serFechaInicio";
                dbpSerFechaInicio.Value = objServicio.serFechaInicio;
                dbComando.Parameters.Add(dbpSerFechaInicio);

                dbpSerFechaFin.DbType = DbType.DateTime;
                dbpSerFechaFin.ParameterName = "@serFechaFin";
                dbpSerFechaFin.Value = objServicio.serFechaFin.ToShortDateString() == "01/01/0001" || objServicio.serFechaFin.ToShortDateString() == "01/01/1900"  ? (object)DBNull.Value:objServicio.serFechaFin;
                dbComando.Parameters.Add(dbpSerFechaFin);

                dbpSerObservaciones.DbType = DbType.String;
                dbpSerObservaciones.ParameterName = "@serObservaciones";
                dbpSerObservaciones.Value = objServicio.serObservaciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpSerObservaciones);

                dbpSerLogotipo.DbType = DbType.Binary;
                dbpSerLogotipo.ParameterName = "@serLogotipo";
                dbpSerLogotipo.Value = objServicio.serLogotipo ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpSerLogotipo);

                dbpSerPaginaWeb.DbType = DbType.String;
                dbpSerPaginaWeb.ParameterName = "@serPaginaWeb";
                dbpSerPaginaWeb.Value = objServicio.serPaginaWeb ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpSerPaginaWeb);

                dbpIdCategoriaServicio.DbType = DbType.Int32;
                dbpIdCategoriaServicio.ParameterName = "@idCategoriaServicio";
                dbpIdCategoriaServicio.Value = objServicio.idCategoriaServicio == 0 ? (object)DBNull.Value : objServicio.idCategoriaServicio;
                dbComando.Parameters.Add(dbpIdCategoriaServicio);

        


                dbComando.ExecuteNonQuery();
                int IdServicio = (int)dbComando.Parameters["@idServicio"].Value;
                clsDatServicio.insertarDomicilioServicio(IdServicio, objServicio ,objDomicilio, dbConexion, dbTrans);

                foreach (clsEntResponsable objResponsable in lstResponsable)
                {
                    clsDatServicio.insertarResponsableServicio(IdServicio, objServicio, objResponsable, dbConexion, dbTrans);
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






        public static bool insertarDomicilioServicio(int idServicio, clsEntServicio objServicio, clsEntDomicilio objDomicilio, DbConnection dbConexion, DbTransaction dbTrans)
        {


            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;


            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spInsertaDomicilioServicio";
                dbComando.Parameters.Clear();

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

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = idServicio;
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






        public static bool insertarResponsableServicio(int idServicio, clsEntServicio objServicio, clsEntResponsable objResponsable, DbConnection dbConexion, DbTransaction dbTrans)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "servicio.spInsertaResponsableServicio";
                dbComando.Parameters.Clear();


                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpriObservaciones = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = objResponsable.IdEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbpriObservaciones.DbType = DbType.String;
                dbpriObservaciones.ParameterName = "@rsObservaciones";
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






        public static void consultaServicioGeneral(clsEntServicio objServicio, clsEntDomicilio objDomicilio, clsEntResponsable objResponsable, ref DataTable dsServicio, clsEntSesion objSesion)
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
                dbComando.CommandText = "servicio.spBuscaServicioGeneral";
                dbComando.Parameters.Clear();

                DbParameter dbpidTipoServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserDescripcion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserRazonSocial = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserRfc = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserFechaInicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserFechaFin = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserObservaciones = objConexion.dbProvider.CreateParameter();
                DbParameter dbpserPaginaWeb = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidAsentamiento = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidEstado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidMunicipio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomCalle = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomNumeroExterior = objConexion.dbProvider.CreateParameter();
                DbParameter dbpdomNumeroInterior = objConexion.dbProvider.CreateParameter();
                DbParameter dbprsObservaciones = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdCategoriaServicio = objConexion.dbProvider.CreateParameter();

                dbpidTipoServicio.DbType = DbType.Int32;
                dbpidTipoServicio.Direction = ParameterDirection.Input;
                dbpidTipoServicio.ParameterName = "@idTipoServicio";
                dbpidTipoServicio.Value = objServicio.IdTipoServicio == 0 ? (object)DBNull.Value : objServicio.IdTipoServicio;
                dbComando.Parameters.Add(dbpidTipoServicio);

                dbpserDescripcion.DbType = DbType.String;
                dbpserDescripcion.Direction = ParameterDirection.Input;
                dbpserDescripcion.ParameterName = "@serDescripcion";
                dbpserDescripcion.Value = objServicio.serDescripcion?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpserDescripcion);

                dbpserRazonSocial.DbType = DbType.String;
                dbpserRazonSocial.Direction = ParameterDirection.Input;
                dbpserRazonSocial.ParameterName = "@serRazonSocial";
                dbpserRazonSocial.Value = objServicio.serRazonSocial ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpserRazonSocial);

                dbpserRfc.DbType = DbType.String;
                dbpserRfc.Direction = ParameterDirection.Input;
                dbpserRfc.ParameterName = "@serRfc";
                dbpserRfc.Value = objServicio.serRfc ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpserRfc);

                dbpserFechaInicio.DbType = DbType.DateTime;
                dbpserFechaInicio.Direction = ParameterDirection.Input;
                dbpserFechaInicio.ParameterName = "@serFechaInicio";
                dbpserFechaInicio.Value = objServicio.serFechaInicio.ToShortDateString() == "01/01/0001" || objServicio.serFechaInicio.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objServicio.serFechaInicio;
                dbComando.Parameters.Add(dbpserFechaInicio);

                dbpserFechaFin.DbType = DbType.DateTime;
                dbpserFechaFin.Direction = ParameterDirection.Input;
                dbpserFechaFin.ParameterName = "@serFechaFin";
                dbpserFechaFin.Value = objServicio.serFechaFin.ToShortDateString() == "01/01/0001" || objServicio.serFechaFin.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objServicio.serFechaFin;
                dbComando.Parameters.Add(dbpserFechaFin);

                dbpserObservaciones.DbType = DbType.String;
                dbpserObservaciones.Direction = ParameterDirection.Input;
                dbpserObservaciones.ParameterName = "@serObservaciones";
                dbpserObservaciones.Value = objServicio.serObservaciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpserObservaciones);


                dbpserPaginaWeb.DbType = DbType.String;
                dbpserPaginaWeb.Direction = ParameterDirection.Input;
                dbpserPaginaWeb.ParameterName = "@serPaginaWeb";
                dbpserPaginaWeb.Value = objServicio.serPaginaWeb ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbpserPaginaWeb);

 

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

                dbprsObservaciones.DbType = DbType.String;
                dbprsObservaciones.Direction = ParameterDirection.Input;
                dbprsObservaciones.ParameterName = "@rsObservaciones";
                dbprsObservaciones.Value = objResponsable.riObservaciones ?? (object)DBNull.Value;
                dbComando.Parameters.Add(dbprsObservaciones);

                dbpidEmpleado.DbType = DbType.Guid;
                dbpidEmpleado.Direction = ParameterDirection.Input;
                dbpidEmpleado.ParameterName = "@idEmpleado";

                dbpIdCategoriaServicio.DbType = DbType.Int32;
                dbpIdCategoriaServicio.Direction = ParameterDirection.Input;
                dbpIdCategoriaServicio.ParameterName = "@idCategoriaServicio";
                dbpIdCategoriaServicio.Value = objServicio.idCategoriaServicio == 0 ? (object)DBNull.Value : objServicio.idCategoriaServicio;
                dbComando.Parameters.Add(dbpIdCategoriaServicio);



                dbpidEmpleado.Value = objResponsable.IdEmpleado == new Guid("00000000-0000-0000-0000-000000000000") ? (object)DBNull.Value : objResponsable.IdEmpleado;
                dbComando.Parameters.Add(dbpidEmpleado);



                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(dsServicio);

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






        public static void consultaServicio(clsEntServicio objServicio, ref DataSet dsInstalacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "servicio.spBuscaServicio";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidZona = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objServicio.idServicio == 0 ? (object)DBNull.Value : objServicio.idServicio;
                dbComando.Parameters.Add(dbpIdServicio);


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




        public static void buscaResponsableServicio(clsEntServicio objServicio, ref DataSet dsResponsable, clsEntSesion objSesion)
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
                dbComando.CommandText = "servicio.spBuscaResponsableServicio";
                dbComando.Parameters.Clear();


                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();


                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objServicio.idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

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



    }
}
