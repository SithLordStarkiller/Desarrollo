using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using proUtilerias;

namespace SICOGUA.Datos
{
    public class clsDatRevision
    {
        public static bool insertarRevision(ref clsEntRevision objRevision, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spInsertarRevision";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objRevision.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, ParameterDirection.Output, "@idRevision",-1);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idEmpleadoAsignacion", objRevision.idEmpleadoAsignacion == 0 ? (object)DBNull.Value : objRevision.idEmpleadoAsignacion);
                
                dbComando.ExecuteNonQuery();
                objRevision.idRevision =Convert.ToInt32(dbComando.Parameters["@idRevision"].Value);
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



        public static bool insertarRevisionDocumentos(clsEntRevisionDocumentos objRevisionDocumentos, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spInsertarRevisionDocumentos";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objRevisionDocumentos.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idRevision", objRevisionDocumentos.idRevision == 0 ? (object)DBNull.Value : objRevisionDocumentos.idRevision);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idRevisionDocumentos", objRevisionDocumentos.idRevisionDocumento == -1 ? (object)DBNull.Value : objRevisionDocumentos.idRevisionDocumento);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@rdFechaActa", objRevisionDocumentos.rdFechaActa.ToShortDateString() == "01/01/0001" || objRevisionDocumentos.rdFechaActa.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objRevisionDocumentos.rdFechaActa);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@rdNoOficio", objRevisionDocumentos.rdNoOficio == null ? (object)DBNull.Value : objRevisionDocumentos.rdNoOficio);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@rdFechaOficio", objRevisionDocumentos.rdFechaOficio.ToShortDateString() == "01/01/0001" || objRevisionDocumentos.rdFechaOficio.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objRevisionDocumentos.rdFechaOficio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idRevisionTipoDocumento", objRevisionDocumentos.idRevisionTipoDocumento == 0 ? (object)DBNull.Value : objRevisionDocumentos.idRevisionTipoDocumento);
                objParametro.llenarParametros(ref dbComando, DbType.Binary, "@rdOficio",objRevisionDocumentos.rdOficio  ?? (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@rdObservaciones", objRevisionDocumentos.rdObservaciones ?? (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", objRevisionDocumentos.idEmpleadoAsignacion == 0 ? (object)DBNull.Value : objRevisionDocumentos.idEmpleadoAsignacion);

                
                dbComando.ExecuteNonQuery();
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



        public static bool insertarCancelacion(clsEntRevisionCancelacion objCancelacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spInsertarRevisionCancelacion";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objCancelacion.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idRevisionDocumentos", objCancelacion.idRevisionDocumento == -1 ? (object)DBNull.Value : objCancelacion.idRevisionDocumento);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@rcFechaCancelacion", objCancelacion.rcFechaCancelacion.ToShortDateString() == "01/01/0001" || objCancelacion.rcFechaCancelacion.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objCancelacion.rcFechaCancelacion);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@rcObservaciones", objCancelacion.rcObservaciones == null ? (object)DBNull.Value : objCancelacion.rcObservaciones);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idRevision", objCancelacion.idRevision == -1 ? (object)DBNull.Value : objCancelacion.idRevision);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", objCancelacion.idEmpleadoAsignacion == 0 ? (object)DBNull.Value : objCancelacion.idEmpleadoAsignacion);

                dbComando.ExecuteNonQuery();
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



        public static bool insertarRenuncia(clsEntRenuncia objRenuncia, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spInsertarRevisionRenuncia";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objRenuncia.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@renFechaRenuncia", objRenuncia.fechaRenuncia.ToShortDateString() == "01/01/0001" || objRenuncia.fechaRenuncia.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objRenuncia.fechaRenuncia);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@renNoOficio", objRenuncia.noOficio == null ? (object)DBNull.Value : objRenuncia.noOficio);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@renFechaOficio", objRenuncia.fechaOficio.ToShortDateString() == "01/01/0001" || objRenuncia.fechaOficio.ToShortDateString() == "01/01/1900" ? (object)DBNull.Value : objRenuncia.fechaOficio);
                objParametro.llenarParametros(ref dbComando, DbType.Binary, "@renOficio", objRenuncia.oficioAdjunto ?? (object)DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@renObservaciones", objRenuncia.observaciones == null ? (object)DBNull.Value : objRenuncia.observaciones);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idEmpleadoAsigancion", objRenuncia.idEmpleadoAsignacion == 0 ? (object)DBNull.Value : objRenuncia.idEmpleadoAsignacion);
               
                

                dbComando.ExecuteNonQuery();
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

        public static void buscarRevision(Guid idEmpleado, ref DataSet dsRevision, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spBuscarRevision";
                dbComando.Parameters.Clear();

                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                dbpidEmpleado.DbType = DbType.Guid;
                dbpidEmpleado.Direction = ParameterDirection.Input;
                dbpidEmpleado.ParameterName = "@idEmpleado";
                dbpidEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpidEmpleado);
                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(dsRevision);

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





        public static void buscarRenuncia(Guid idEmpleado, ref DataSet dsRenuncia, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spBuscarRenuncia";
                dbComando.Parameters.Clear();

                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                dbpidEmpleado.DbType = DbType.Guid;
                dbpidEmpleado.Direction = ParameterDirection.Input;
                dbpidEmpleado.ParameterName = "@idEmpleado";
                dbpidEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpidEmpleado);
                dbAdapter.SelectCommand = dbComando;
                dbAdapter.Fill(dsRenuncia);

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
