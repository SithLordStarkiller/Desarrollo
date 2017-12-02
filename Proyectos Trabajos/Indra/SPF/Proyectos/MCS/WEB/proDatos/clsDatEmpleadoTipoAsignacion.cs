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

    public class clsDatEmpleadoTipoAsignacion
    {
        public static List<clsEntEmpleadoTipoAsignacion> listaEmpleadoTipoAsignacion(clsEntSesion objSesion, int idTipoAsignacion, int idServicio, int idInstalacion, int empNumero, string empPaterno, string empMaterno, string empNombre, string empRFC, int intFiltroAsignacion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Transaction = dbTrans;
            dbComando.Connection = dbConexion;
            List<clsEntEmpleadoTipoAsignacion> lisEmpleadoTipoAsignacion = new List<clsEntEmpleadoTipoAsignacion>();
            try
            {


                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "empleado.spuConsultarIntegrantesTipoAsignacion";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idTipoAsignacion", idTipoAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@intFiltroAsignacion", intFiltroAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idServicio", idServicio);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idInstalacion", idInstalacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@empNumero", empNumero);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@empPaterno", empPaterno);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@empMaterno", empMaterno);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@empNombre", empNombre);
                objParametro.llenarParametros(ref dbComando, DbType.String, "@empRFC", empRFC);
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

                        clsEntEmpleadoTipoAsignacion objEmpleadoTipoAsignacion = new clsEntEmpleadoTipoAsignacion
                        {
                            empCompleto = drEmpleado["empCompleto"].ToString(),
                            empPaterno = drEmpleado["empPaterno"].ToString(),
                            empMaterno = drEmpleado["empMaterno"].ToString(),
                            empNombre = drEmpleado["empNombre"].ToString(),
                            empNumero = drEmpleado["empNumero"].ToString().Length == 0 ? 0 : (Int32)drEmpleado["empNumero"],
                            empRFC = drEmpleado["empRFC"].ToString(),
                            idEmpleadoAsignacion = drEmpleado["idEmpleadoAsignacion"].ToString().Length == 0 ? 0 : Convert.ToInt32(drEmpleado["idEmpleadoAsignacion"]),
                            idInstalacion = drEmpleado["idInstalacion"].ToString().Length == 0 ? 0 : (Int32)drEmpleado["idInstalacion"],
                            idServicio = drEmpleado["idServicio"].ToString().Length == 0 ? 0 : (Int32)drEmpleado["idServicio"],
                            idTipoAsignacion = drEmpleado["idTipoAsignacion"].ToString().Length == 0 ? 0 : (Int32)drEmpleado["idTipoAsignacion"],
                            jerDescripcion = drEmpleado["jerDescripcion"].ToString(),
                            tiaDescripcion = drEmpleado["tiaDescripcion"].ToString(),
                            idEmpleado = Guid.Parse(drEmpleado["idEmpleado"].ToString()),
                            empLoc = drEmpleado["LOC"].ToString(),
                            empSexo = drEmpleado["Sexo"].ToString(),
                            zonDescripcion = drEmpleado["zonDescripcion"].ToString(),
                            serDescripcion = drEmpleado["serDescripcion"].ToString(),
                            insNombre = drEmpleado["insNombre"].ToString(),
                            estDescripcion = drEmpleado["estNombre"].ToString(),
                            faDescripcion = drEmpleado["faDescripcion"].ToString(),
                            inicioAsignacion = (drEmpleado["asignacionInicio"].ToString()),
                            finAsignacion = (drEmpleado["asignacionFin"].ToString()),
                            incidencia = drEmpleado["incidencia"].ToString(),
                            inicioIncidencia = (drEmpleado["incidenciaInicio"].ToString()),
                            finIncidencia = (drEmpleado["incidenciaFin"].ToString()),
                            incidenciaRevisada = drEmpleado["incidenciaRevisada"].ToString(),
                            usuario = drEmpleado["usuario"].ToString(),
                            fecha = drEmpleado["fecha"].ToString(),
                            estacion = drEmpleado["estacion"].ToString()

                        };


                        lisEmpleadoTipoAsignacion.Add(objEmpleadoTipoAsignacion);

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
            return lisEmpleadoTipoAsignacion;
        }
        public static bool insertaempleadoTipoAsignacion(clsEntEmpleadoTipoAsignacion objEmpleadoTipoASignacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spuInsertaEmpleadoAsignacionTipo";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objEmpleadoTipoASignacion.idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idEmpleadoAsignacion", objEmpleadoTipoASignacion.idEmpleadoAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idTipoAsignacion", objEmpleadoTipoASignacion.idTipoAsignacion);
                objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idUsuario", objSesion.usuario.IdUsuario);

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