using System;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;

namespace SICOGUA.Datos
{
    public class clsDatReporteIncidente
    {
        public static bool insertarActualizarReporteIncidente(clsEntReporteIncidente objIncidente, clsEntSesion objSesion)
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
                dbComando.CommandText = "operacionServicio.spInsertarReporteIncidente";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idIncidente", objIncidente.IdIncidente);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", objIncidente.Servicio.idServicio == 0 ? (object) DBNull.Value : objIncidente.Servicio.idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", objIncidente.Instalacion.IdInstalacion == 0 ? (object) DBNull.Value : objIncidente.Instalacion.IdInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@riFechaHora", objIncidente.RiFechaHora);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoInvolucrado", objIncidente.IdEmpleadoInvolucrado.CompareTo(new Guid("00000000-0000-0000-0000-000000000000")) == 0 ? (object) DBNull.Value : objIncidente.IdEmpleadoInvolucrado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoPuestoInvolucrado", objIncidente.IdEmpleadoPuestoInvolucrado == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoPuestoInvolucrado);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riLugar", objIncidente.RiLugar);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riActividad", objIncidente.RiActividad);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riUniforme", objIncidente.RiUniforme);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riDesarrolloConsecuencia", objIncidente.RiDesarrolloConsecuencia);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riLesion", objIncidente.RiLesion);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riUbicacionCadaverLesionado", objIncidente.RiUbicacionCadaverLesionado);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riAccionVSAgresor", objIncidente.RiAccionVsAgresor);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoTomaNota", objIncidente.IdEmpleadoTomaNota.CompareTo(new Guid("00000000-0000-0000-0000-000000000000")) == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoTomaNota);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoPuestoTomaNota", objIncidente.IdEmpleadoPuestoTomaNota == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoPuestoTomaNota);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riAccionMando", objIncidente.RiAccionMando);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoAutor", objIncidente.IdEmpleadoAutor.CompareTo(new Guid("00000000-0000-0000-0000-000000000000")) == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoAutor);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoPuestoAutor", objIncidente.IdEmpleadoPuestoAutor == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoPuestoAutor);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleadoSuperior", objIncidente.IdEmpleadoSuperior.CompareTo(new Guid("00000000-0000-0000-0000-000000000000")) == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoSuperior);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoPuestoSuperior", objIncidente.IdEmpleadoPuestoSuperior == 0 ? (object)DBNull.Value : objIncidente.IdEmpleadoPuestoSuperior);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@riDanioMaterial", objIncidente.RiDanioMaterial);
                objParametro.llenarParametros(ref dbComando, DbType.Double, "@riMonto", objIncidente.RiMonto);

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

        public static DataTable buscarReporteIncidente(clsEntReporteIncidente objIncidente, clsEntSesion objSesion)
        {
            DataTable dtReporte = new DataTable();

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
                dbComando.CommandText = "operacionServicio.spBuscarReporteIncidente";
                dbComando.Parameters.Clear();

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFecha = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdZona = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNumero = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNombre = objConexion.dbProvider.CreateParameter();

                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Value = objIncidente.Servicio.idServicio == 0
                                          ? (object) DBNull.Value
                                          : objIncidente.Servicio.idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Value = objIncidente.Instalacion.IdInstalacion == 0
                                             ? (object) DBNull.Value
                                             : objIncidente.Instalacion.IdInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                dbpFecha.DbType = DbType.DateTime;
                dbpFecha.ParameterName = "@riFechaHora";
                dbpFecha.Value = objIncidente.RiFechaHora.ToShortDateString() == "01/01/0001" ||
                                 objIncidente.RiFechaHora.ToShortDateString() == "01/01/1900"
                                     ? (object) DBNull.Value
                                     : objIncidente.RiFechaHora;
                dbComando.Parameters.Add(dbpFecha);

                dbpIdZona.DbType = DbType.Int16;
                dbpIdZona.ParameterName = "@idZona";
                dbpIdZona.Value = objIncidente.ZonaEmpleadoInvolucrado.IdZona == 0
                                      ? (object) DBNull.Value
                                      : objIncidente.ZonaEmpleadoInvolucrado.IdZona;
                dbComando.Parameters.Add(dbpIdZona);

                dbpNumero.DbType = DbType.Int32;
                dbpNumero.ParameterName = "@empNumero";
                dbpNumero.Value = objIncidente.NoEmpleadoInvolucrado == 0
                                      ? (object) DBNull.Value
                                      : objIncidente.NoEmpleadoInvolucrado;
                dbComando.Parameters.Add(dbpNumero);

                dbpNombre.DbType = DbType.String;
                dbpNombre.ParameterName = "@empNombre";
                dbpNombre.Value = objIncidente.NombreEmpleadoInvolucrado.Trim() == ""
                                      ? (object) DBNull.Value
                                      : objIncidente.NombreEmpleadoInvolucrado.Trim();
                dbComando.Parameters.Add(dbpNombre);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtReporte);

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
            return dtReporte;
        }

        public static dsGuardas._operacionServicio_ReporteIncidenteDataTable consultarReporteIncidente(clsEntReporteIncidente objIncidente, clsEntSesion objSesion)
        {
            dsGuardas._operacionServicio_ReporteIncidenteDataTable dtReporte = new dsGuardas._operacionServicio_ReporteIncidenteDataTable();

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
                dbComando.CommandText = "operacionServicio.spConsultarReporteIncidente";
                dbComando.Parameters.Clear();

                DbParameter dbpIdIncidente = objConexion.dbProvider.CreateParameter();

                dbpIdIncidente.DbType = DbType.Int32;
                dbpIdIncidente.ParameterName = "@idIncidente";
                dbpIdIncidente.Value = objIncidente.IdIncidente;
                dbComando.Parameters.Add(dbpIdIncidente);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtReporte);

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

            return dtReporte;
        }
    }
}
