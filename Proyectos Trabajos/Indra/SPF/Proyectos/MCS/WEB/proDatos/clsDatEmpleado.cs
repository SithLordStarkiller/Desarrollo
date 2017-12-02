using System;
using SICOGUA.Entidades;
using System.Data;
using System.Data.Common;
using SICOGUA.Seguridad;
using proUtilerias;
using SICOGUA.Datos;

namespace SICOGUA.Datos
{
    public class clsDatEmpleado
    {
        public static void buscarEmpleado(clsEntEmpleado objEmpleado, string procedimientoAlmacenado, ref DataSet dsEmpleado, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.CommandTimeout = 20000;
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            int idUsuario = objSesion.usuario.IdUsuario;
            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaIngreso = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaBaja = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNombre = objConexion.dbProvider.CreateParameter();
                DbParameter dbpPaterno = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMaterno = objConexion.dbProvider.CreateParameter();
                DbParameter dbpRfc = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCurp = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCuip = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNumero = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaNacimiento = objConexion.dbProvider.CreateParameter();
                DbParameter dbpPais = objConexion.dbProvider.CreateParameter();
                DbParameter dbpEstado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMunicipio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCartilla = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCurso = objConexion.dbProvider.CreateParameter();
                DbParameter dbpLOC = objConexion.dbProvider.CreateParameter();
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpTipoServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaComision = objConexion.dbProvider.CreateParameter();  
                DbParameter dbpIdJerarquia = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdTipo = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaFinAsignacion = objConexion.dbProvider.CreateParameter();

                //

              
         
   

                dbpIdUsuario.DbType = DbType.Int32;
                dbpIdUsuario.Direction = ParameterDirection.Input;
                dbpIdUsuario.ParameterName = "@idUsuario";
                dbpIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbpIdUsuario);

                dbpTipoServicio.DbType = DbType.Int32;
                dbpTipoServicio.Direction = ParameterDirection.Input;
                dbpTipoServicio.ParameterName = "@idTipoServicio";
                dbpTipoServicio.Value = objEmpleado.EmpleadoAsignacion2.Servicio.IdTipoServicio == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoAsignacion2.Servicio.IdTipoServicio;
                dbComando.Parameters.Add(dbpTipoServicio);

                dbpServicio.DbType = DbType.Int32;
                dbpServicio.Direction = ParameterDirection.Input;
                dbpServicio.ParameterName = "@idServicio";
                dbpServicio.Value = objEmpleado.EmpleadoAsignacion2.Servicio.idServicio == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoAsignacion2.Servicio.idServicio;
                dbComando.Parameters.Add(dbpServicio);

                dbpInstalacion.DbType = DbType.Int32;
                dbpInstalacion.Direction = ParameterDirection.Input;
                dbpInstalacion.ParameterName = "@idInstalacion";
                dbpInstalacion.Value = objEmpleado.EmpleadoAsignacion2.Instalacion.IdInstalacion == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoAsignacion2.Instalacion.IdInstalacion;
                dbComando.Parameters.Add(dbpInstalacion);


                dbpFechaIngreso.DbType = DbType.DateTime;
                dbpFechaIngreso.Direction = ParameterDirection.Input;
                dbpFechaIngreso.ParameterName = "@empFechaIngreso";
                dbpFechaIngreso.Value = objEmpleado.EmpFechaIngreso.Year == 1 ? (object)DBNull.Value : objEmpleado.EmpFechaIngreso;
                dbComando.Parameters.Add(dbpFechaIngreso);

                dbpFechaBaja.DbType = DbType.DateTime;
                dbpFechaBaja.Direction = ParameterDirection.Input;
                dbpFechaBaja.ParameterName = "@empFechaBaja";
                dbpFechaBaja.Value = objEmpleado.EmpFechaBaja.Year == 1 ? (object)DBNull.Value : objEmpleado.EmpFechaBaja;
                dbComando.Parameters.Add(dbpFechaBaja);

                dbpNombre.DbType = DbType.String;
                dbpNombre.Direction = ParameterDirection.Input;
                dbpNombre.ParameterName = "@empNombre";
                dbpNombre.Value = objEmpleado.EmpNombre == "" ? (object)DBNull.Value : objEmpleado.EmpNombre;
                dbComando.Parameters.Add(dbpNombre);

                dbpPaterno.DbType = DbType.String;
                dbpPaterno.Direction = ParameterDirection.Input;
                dbpPaterno.ParameterName = "@empPaterno";
                dbpPaterno.Value = objEmpleado.EmpPaterno == "" ? (object)DBNull.Value : objEmpleado.EmpPaterno;
                dbComando.Parameters.Add(dbpPaterno);

                dbpMaterno.DbType = DbType.String;
                dbpMaterno.Direction = ParameterDirection.Input;
                dbpMaterno.ParameterName = "@empMaterno";
                dbpMaterno.Value = objEmpleado.EmpMaterno == "" ? (object)DBNull.Value : objEmpleado.EmpMaterno;
                dbComando.Parameters.Add(dbpMaterno);

                dbpRfc.DbType = DbType.String;
                dbpRfc.Direction = ParameterDirection.Input;
                dbpRfc.ParameterName = "@empRFC";
                dbpRfc.Value = objEmpleado.EmpRFC == "" ? (object)DBNull.Value : objEmpleado.EmpRFC;
                dbComando.Parameters.Add(dbpRfc);

                dbpCurp.DbType = DbType.String;
                dbpCurp.Direction = ParameterDirection.Input;
                dbpCurp.ParameterName = "@empCURP";
                dbpCurp.Value = objEmpleado.EmpCURP == "" ? (object)DBNull.Value : objEmpleado.EmpCURP;
                dbComando.Parameters.Add(dbpCurp);

                dbpCuip.DbType = DbType.String;
                dbpCuip.Direction = ParameterDirection.Input;
                dbpCuip.ParameterName = "@empCUIP";
                dbpCuip.Value = objEmpleado.EmpCUIP == "" ? (object)DBNull.Value : objEmpleado.EmpCUIP;
                dbComando.Parameters.Add(dbpCuip);

                //
                dbpNumero.DbType = DbType.Int32;
                dbpNumero.Direction = ParameterDirection.Input;
                dbpNumero.ParameterName = "@empNumero";
                dbpNumero.Value = objEmpleado.EmpNumero == 0 ? (object)DBNull.Value : objEmpleado.EmpNumero;
                dbComando.Parameters.Add(dbpNumero);

                dbpFechaNacimiento.DbType = DbType.DateTime;
                dbpFechaNacimiento.Direction = ParameterDirection.Input;
                dbpFechaNacimiento.ParameterName = "@empFechaNacimiento";
                dbpFechaNacimiento.Value = objEmpleado.EmpFechaNacimiento.Year == 1 ? (object)DBNull.Value : objEmpleado.EmpFechaNacimiento;
                dbComando.Parameters.Add(dbpFechaNacimiento);

                dbpPais.DbType = DbType.Int16;
                dbpPais.Direction = ParameterDirection.Input;
                dbpPais.ParameterName = "@idPais";
                dbpPais.Value = objEmpleado.IdPais == 0 ? (object)DBNull.Value : objEmpleado.IdPais;
                dbComando.Parameters.Add(dbpPais);

                dbpEstado.DbType = DbType.Int16;
                dbpEstado.Direction = ParameterDirection.Input;
                dbpEstado.ParameterName = "@idEstado";
                dbpEstado.Value = objEmpleado.IdEstado == 0 ? (object)DBNull.Value : objEmpleado.IdEstado;
                dbComando.Parameters.Add(dbpEstado);

                dbpMunicipio.DbType = DbType.Int16;
                dbpMunicipio.Direction = ParameterDirection.Input;
                dbpMunicipio.ParameterName = "@idMunicipio";
                dbpMunicipio.Value = objEmpleado.IdMunicipio == 0 ? (object)DBNull.Value : objEmpleado.IdMunicipio;
                dbComando.Parameters.Add(dbpMunicipio);


                dbpCartilla.DbType = DbType.String;
                dbpCartilla.Direction = ParameterDirection.Input;
                dbpCartilla.ParameterName = "@empCartilla";
                dbpCartilla.Value = objEmpleado.EmpCartilla == "" ? (object)DBNull.Value : objEmpleado.EmpCartilla;
                dbComando.Parameters.Add(dbpCartilla);

                dbpLOC.DbType = DbType.Int16;
                dbpLOC.Direction = ParameterDirection.Input;
                dbpLOC.ParameterName = "@empLOC";
                dbpLOC.Value = objEmpleado.EmpLOC == 0 ? (object)DBNull.Value : objEmpleado.EmpLOC;
                dbComando.Parameters.Add(dbpLOC);

                dbpCurso.DbType = DbType.Int16;
                dbpCurso.Direction = ParameterDirection.Input;
                dbpCurso.ParameterName = "@empCurso";
                dbpCurso.Value = objEmpleado.EmpCurso == 0 ? (object)DBNull.Value : objEmpleado.EmpCurso;
                dbComando.Parameters.Add(dbpCurso);

                if (procedimientoAlmacenado == "empleado.spBuscarEmpleadoPermisoConsulta" || procedimientoAlmacenado == "empleado.spBuscarEmpleadoMasivo")
                {
                dbpidEmpleado.DbType = DbType.Guid;
                dbpidEmpleado.Direction = ParameterDirection.Input;
                dbpidEmpleado.ParameterName = "@idEmpleado";

                dbpidEmpleado.Value = objEmpleado.IdEmpleado == new Guid("00000000-0000-0000-0000-000000000000") ? (object)DBNull.Value : objEmpleado.IdEmpleado;
                dbComando.Parameters.Add(dbpidEmpleado);
                }


                if (procedimientoAlmacenado == "empleado.spBuscarEmpleadoMasivo")
                {
                    dbpIdJerarquia.DbType = DbType.Int32;
                    dbpIdJerarquia.Direction = ParameterDirection.Input;
                    dbpIdJerarquia.ParameterName = "@idJerarquia";
                    dbpIdJerarquia.Value = objEmpleado.IdJerarquia == 0 ? (object)DBNull.Value : objEmpleado.IdJerarquia;
                    dbComando.Parameters.Add(dbpIdJerarquia);


                    dbpFechaComision.DbType = DbType.DateTime;
                    dbpFechaComision.Direction = ParameterDirection.Input;
                    dbpFechaComision.ParameterName = "@fechaComision";
                    dbpFechaComision.Value = objEmpleado.fechaIniCom.Year == 1 ? (object)DBNull.Value : objEmpleado.fechaIniCom;
                    dbComando.Parameters.Add(dbpFechaComision);

                    dbpIdTipo.DbType = DbType.Int32;
                    dbpIdTipo.Direction = ParameterDirection.Input;
                    dbpIdTipo.ParameterName = "@tipo";
                    dbpIdTipo.Value = objEmpleado.tipo == 0 ? (object)DBNull.Value : objEmpleado.tipo;
                    dbComando.Parameters.Add(dbpIdTipo);

                    dbpFechaFinAsignacion.DbType = DbType.DateTime;
                    dbpFechaFinAsignacion.Direction = ParameterDirection.Input;
                    dbpFechaFinAsignacion.ParameterName = "@fechaFinComision";
                    dbpFechaFinAsignacion.Value = objEmpleado.fechaFinAsignacion.Year == 1 ? (object)DBNull.Value : objEmpleado.fechaFinAsignacion;
                    dbComando.Parameters.Add(dbpFechaFinAsignacion);

              
                }


                //dbpPaiDescripcion.DbType = DbType.String;
                //dbpPaiDescripcion.Direction = ParameterDirection.Input;
                //dbpPaiDescripcion.ParameterName = "@paiDescripcion";
                //dbpPaiDescripcion.Value = "";
                //dbComando.Parameters.Add(dbpPaiDescripcion);

                //dbpEstDescripcion.DbType = DbType.String;
                //dbpEstDescripcion.Direction = ParameterDirection.Input;
                //dbpEstDescripcion.ParameterName = "@estDescripcion";
                //dbpEstDescripcion.Value = "";
                //dbComando.Parameters.Add(dbpEstDescripcion);

                //dbpMunDescripcion.DbType = DbType.String;
                //dbpMunDescripcion.Direction = ParameterDirection.Input;
                //dbpMunDescripcion.ParameterName = "@munDescripcion";
                //dbpMunDescripcion.Value = "";
                //dbComando.Parameters.Add(dbpMunDescripcion);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsEmpleado);

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

        public static bool insertarEmpleado(clsEntEmpleado objEmpleado, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spInsertarEmpleado";
                dbComando.Parameters.Clear();

                DbParameter dbpNombre = objConexion.dbProvider.CreateParameter();

                dbpNombre.DbType = DbType.String;
                dbpNombre.ParameterName = "@nombre";
                dbpNombre.Value = objEmpleado.EmpNombre;
                dbComando.Parameters.Add(dbpNombre);

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

        public static void consultarEmpleado(Guid idEmpleado, ref dsGuardas._empleado_guardaDataTable dsGuarda, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spConsultarEmpleado";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.Direction = ParameterDirection.Input;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsGuarda);

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
        
        public static void buscarEmpleadoMasivo(clsEntEmpleado objEmpleado, string procedimientoAlmacenado, ref DataSet dsEmpleado, clsEntSesion objSesion)
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
                dbComando.CommandText = procedimientoAlmacenado;
                dbComando.Parameters.Clear();

                DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaIngreso = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaBaja = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNombre = objConexion.dbProvider.CreateParameter();
                DbParameter dbpPaterno = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMaterno = objConexion.dbProvider.CreateParameter();
                DbParameter dbpRfc = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCurp = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCuip = objConexion.dbProvider.CreateParameter();
                DbParameter dbpNumero = objConexion.dbProvider.CreateParameter();
                DbParameter dbpFechaNacimiento = objConexion.dbProvider.CreateParameter();
                DbParameter dbpPais = objConexion.dbProvider.CreateParameter();
                DbParameter dbpEstado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpMunicipio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCartilla = objConexion.dbProvider.CreateParameter();
                DbParameter dbpCurso = objConexion.dbProvider.CreateParameter();
                DbParameter dbpLOC = objConexion.dbProvider.CreateParameter();
                DbParameter dbpServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpInstalacion = objConexion.dbProvider.CreateParameter();
                DbParameter dbpTipoServicio = objConexion.dbProvider.CreateParameter();
                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                //

                dbpIdUsuario.DbType = DbType.Int32;
                dbpIdUsuario.Direction = ParameterDirection.Input;
                dbpIdUsuario.ParameterName = "@idUsuario";
                dbpIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbpIdUsuario);

                dbpTipoServicio.DbType = DbType.Int32;
                dbpTipoServicio.Direction = ParameterDirection.Input;
                dbpTipoServicio.ParameterName = "@idTipoServicio";
                dbpTipoServicio.Value = objEmpleado.EmpleadoAsignacion2.Servicio.IdTipoServicio == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoAsignacion2.Servicio.IdTipoServicio;
                dbComando.Parameters.Add(dbpTipoServicio);

                dbpServicio.DbType = DbType.Int32;
                dbpServicio.Direction = ParameterDirection.Input;
                dbpServicio.ParameterName = "@idServicio";
                dbpServicio.Value = objEmpleado.EmpleadoAsignacion2.Servicio.idServicio == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoAsignacion2.Servicio.idServicio;
                dbComando.Parameters.Add(dbpServicio);

                dbpInstalacion.DbType = DbType.Int32;
                dbpInstalacion.Direction = ParameterDirection.Input;
                dbpInstalacion.ParameterName = "@idInstalacion";
                dbpInstalacion.Value = objEmpleado.EmpleadoAsignacion2.Instalacion.IdInstalacion == 0 ? (object)DBNull.Value : objEmpleado.EmpleadoAsignacion2.Instalacion.IdInstalacion;
                dbComando.Parameters.Add(dbpInstalacion);


                dbpFechaIngreso.DbType = DbType.DateTime;
                dbpFechaIngreso.Direction = ParameterDirection.Input;
                dbpFechaIngreso.ParameterName = "@empFechaIngreso";
                dbpFechaIngreso.Value = objEmpleado.EmpFechaIngreso.Year == 1 ? (object)DBNull.Value : objEmpleado.EmpFechaIngreso;
                dbComando.Parameters.Add(dbpFechaIngreso);

                dbpFechaBaja.DbType = DbType.DateTime;
                dbpFechaBaja.Direction = ParameterDirection.Input;
                dbpFechaBaja.ParameterName = "@empFechaBaja";
                dbpFechaBaja.Value = objEmpleado.EmpFechaBaja.Year == 1 ? (object)DBNull.Value : objEmpleado.EmpFechaBaja;
                dbComando.Parameters.Add(dbpFechaBaja);

                dbpNombre.DbType = DbType.String;
                dbpNombre.Direction = ParameterDirection.Input;
                dbpNombre.ParameterName = "@empNombre";
                dbpNombre.Value = objEmpleado.EmpNombre == "" ? (object)DBNull.Value : objEmpleado.EmpNombre;
                dbComando.Parameters.Add(dbpNombre);

                dbpPaterno.DbType = DbType.String;
                dbpPaterno.Direction = ParameterDirection.Input;
                dbpPaterno.ParameterName = "@empPaterno";
                dbpPaterno.Value = objEmpleado.EmpPaterno == "" ? (object)DBNull.Value : objEmpleado.EmpPaterno;
                dbComando.Parameters.Add(dbpPaterno);

                dbpMaterno.DbType = DbType.String;
                dbpMaterno.Direction = ParameterDirection.Input;
                dbpMaterno.ParameterName = "@empMaterno";
                dbpMaterno.Value = objEmpleado.EmpMaterno == "" ? (object)DBNull.Value : objEmpleado.EmpMaterno;
                dbComando.Parameters.Add(dbpMaterno);

                dbpRfc.DbType = DbType.String;
                dbpRfc.Direction = ParameterDirection.Input;
                dbpRfc.ParameterName = "@empRFC";
                dbpRfc.Value = objEmpleado.EmpRFC == "" ? (object)DBNull.Value : objEmpleado.EmpRFC;
                dbComando.Parameters.Add(dbpRfc);

                dbpCurp.DbType = DbType.String;
                dbpCurp.Direction = ParameterDirection.Input;
                dbpCurp.ParameterName = "@empCURP";
                dbpCurp.Value = objEmpleado.EmpCURP == "" ? (object)DBNull.Value : objEmpleado.EmpCURP;
                dbComando.Parameters.Add(dbpCurp);

                dbpCuip.DbType = DbType.String;
                dbpCuip.Direction = ParameterDirection.Input;
                dbpCuip.ParameterName = "@empCUIP";
                dbpCuip.Value = objEmpleado.EmpCUIP == "" ? (object)DBNull.Value : objEmpleado.EmpCUIP;
                dbComando.Parameters.Add(dbpCuip);

                //
                dbpNumero.DbType = DbType.Int32;
                dbpNumero.Direction = ParameterDirection.Input;
                dbpNumero.ParameterName = "@empNumero";
                dbpNumero.Value = objEmpleado.EmpNumero == 0 ? (object)DBNull.Value : objEmpleado.EmpNumero;
                dbComando.Parameters.Add(dbpNumero);

                dbpFechaNacimiento.DbType = DbType.DateTime;
                dbpFechaNacimiento.Direction = ParameterDirection.Input;
                dbpFechaNacimiento.ParameterName = "@empFechaNacimiento";
                dbpFechaNacimiento.Value = objEmpleado.EmpFechaNacimiento.Year == 1 ? (object)DBNull.Value : objEmpleado.EmpFechaNacimiento;
                dbComando.Parameters.Add(dbpFechaNacimiento);

                dbpPais.DbType = DbType.Int16;
                dbpPais.Direction = ParameterDirection.Input;
                dbpPais.ParameterName = "@idPais";
                dbpPais.Value = objEmpleado.IdPais == 0 ? (object)DBNull.Value : objEmpleado.IdPais;
                dbComando.Parameters.Add(dbpPais);

                dbpEstado.DbType = DbType.Int16;
                dbpEstado.Direction = ParameterDirection.Input;
                dbpEstado.ParameterName = "@idEstado";
                dbpEstado.Value = objEmpleado.IdEstado == 0 ? (object)DBNull.Value : objEmpleado.IdEstado;
                dbComando.Parameters.Add(dbpEstado);

                dbpMunicipio.DbType = DbType.Int16;
                dbpMunicipio.Direction = ParameterDirection.Input;
                dbpMunicipio.ParameterName = "@idMunicipio";
                dbpMunicipio.Value = objEmpleado.IdMunicipio == 0 ? (object)DBNull.Value : objEmpleado.IdMunicipio;
                dbComando.Parameters.Add(dbpMunicipio);


                dbpCartilla.DbType = DbType.String;
                dbpCartilla.Direction = ParameterDirection.Input;
                dbpCartilla.ParameterName = "@empCartilla";
                dbpCartilla.Value = objEmpleado.EmpCartilla == "" ? (object)DBNull.Value : objEmpleado.EmpCartilla;
                dbComando.Parameters.Add(dbpCartilla);

                dbpLOC.DbType = DbType.Int16;
                dbpLOC.Direction = ParameterDirection.Input;
                dbpLOC.ParameterName = "@empLOC";
                dbpLOC.Value = objEmpleado.EmpLOC == 0 ? (object)DBNull.Value : objEmpleado.EmpLOC;
                dbComando.Parameters.Add(dbpLOC);

                dbpCurso.DbType = DbType.Int16;
                dbpCurso.Direction = ParameterDirection.Input;
                dbpCurso.ParameterName = "@empCurso";
                dbpCurso.Value = objEmpleado.EmpCurso == 0 ? (object)DBNull.Value : objEmpleado.EmpCurso;
                dbComando.Parameters.Add(dbpCurso);

                if (procedimientoAlmacenado == "empleado.spBuscarEmpleadoPermisoConsulta")
                {
                    dbpidEmpleado.DbType = DbType.Guid;
                    dbpidEmpleado.Direction = ParameterDirection.Input;
                    dbpidEmpleado.ParameterName = "@idEmpleado";

                    dbpidEmpleado.Value = objEmpleado.IdEmpleado == new Guid("00000000-0000-0000-0000-000000000000") ? (object)DBNull.Value : objEmpleado.IdEmpleado;
                    dbComando.Parameters.Add(dbpidEmpleado);
                }
                //dbpPaiDescripcion.DbType = DbType.String;
                //dbpPaiDescripcion.Direction = ParameterDirection.Input;
                //dbpPaiDescripcion.ParameterName = "@paiDescripcion";
                //dbpPaiDescripcion.Value = "";
                //dbComando.Parameters.Add(dbpPaiDescripcion);

                //dbpEstDescripcion.DbType = DbType.String;
                //dbpEstDescripcion.Direction = ParameterDirection.Input;
                //dbpEstDescripcion.ParameterName = "@estDescripcion";
                //dbpEstDescripcion.Value = "";
                //dbComando.Parameters.Add(dbpEstDescripcion);

                //dbpMunDescripcion.DbType = DbType.String;
                //dbpMunDescripcion.Direction = ParameterDirection.Input;
                //dbpMunDescripcion.ParameterName = "@munDescripcion";
                //dbpMunDescripcion.Value = "";
                //dbComando.Parameters.Add(dbpMunDescripcion);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsEmpleado);

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
        
        public static void buscarUltimaAsignacion(clsEntEmpleado objEmpleado, ref DataSet dsEmpleado, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spBuscarUltimaAsignacion";
                dbComando.Parameters.Clear();

                DbParameter dbpIdEmpleado = objConexion.dbProvider.CreateParameter();

                dbpIdEmpleado.DbType = DbType.Guid;
                dbpIdEmpleado.ParameterName = "@idEmpleado";
                dbpIdEmpleado.Value = objEmpleado.IdEmpleado;
                dbComando.Parameters.Add(dbpIdEmpleado);

           
                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsEmpleado);

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
        
        public static void buscarAsignacionActual(Guid idEmpleado, ref DataSet dsAsignacion, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spBuscarAsignacionActual";
                dbComando.Parameters.Clear();

                DbParameter dbpidEmpleado = objConexion.dbProvider.CreateParameter();
                DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();


                dbpIdUsuario.DbType = DbType.Int32;
                dbpIdUsuario.Direction = ParameterDirection.Input;
                dbpIdUsuario.ParameterName = "@idUsuario";
                dbpIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbpIdUsuario);


                dbpidEmpleado.DbType = DbType.Guid;
                dbpidEmpleado.Direction = ParameterDirection.Input;
                dbpidEmpleado.ParameterName = "@idEmpleado";
                dbpidEmpleado.Value = idEmpleado;
                dbComando.Parameters.Add(dbpidEmpleado);
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
        public static DataTable consultarServicioInstalacionHorarioREA(Guid idEmpleado, DateTime fecha, clsEntSesion objSesion)
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
                dbComando.CommandText = "empleado.spConsultarServicioInstalacionHorarioREA";
                dbComando.Parameters.Clear();

                clsParametro objParametro = new clsParametro();

                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idZona", objAsignacionOs.Zona.IdZona != 0 ? (object)objAsignacionOs.Zona.IdZona : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idAgrupamiento", objAsignacionOs.Agrupamiento.IdAgrupamiento != 0 ? (object)objAsignacionOs.Agrupamiento.IdAgrupamiento : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idCompania", objAsignacionOs.Compania.IdCompania != 0 ? (object)objAsignacionOs.Compania.IdCompania : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idSeccion", objAsignacionOs.Seccion.IdSeccion != 0 ? (object)objAsignacionOs.Seccion.IdSeccion : DBNull.Value);
                //objParametro.llenarParametros(ref dbComando, DbType.Int16, "@idPeloton", objAsignacionOs.Peloton.IdPeloton != 0 ? (object)objAsignacionOs.Peloton.IdPeloton : DBNull.Value);
                objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado);
                objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@fecha", fecha);

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

        #region Pase de lista generica
            public static void buscarEmpleadoNumero(DateTime fechaAsistencia, string empNumero,int idHorario, ref DataSet dsEmpleado, clsEntSesion objSesion,ref byte existe, int idServicio, int idInstalacion,ref string excep)
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
                dbComando.CommandText = "empleado.spuBuscarEmpleadoPorNumero";
                dbComando.Parameters.Clear();

                DbParameter dbpIdUsuario = objConexion.dbProvider.CreateParameter();
                dbpIdUsuario.DbType = DbType.Int32;
                dbpIdUsuario.Direction = ParameterDirection.Input;
                dbpIdUsuario.ParameterName = "@idUsuario";
                dbpIdUsuario.Value = idUsuario == 0 ? (object)DBNull.Value : idUsuario;
                dbComando.Parameters.Add(dbpIdUsuario);


                DbParameter dbpEmpNumero = objConexion.dbProvider.CreateParameter();
                dbpEmpNumero.DbType = DbType.String;
                dbpEmpNumero.ParameterName = "@empNumero";
                dbpEmpNumero.Direction = ParameterDirection.Input;
                dbpEmpNumero.Value = empNumero;
                dbComando.Parameters.Add(dbpEmpNumero);


                DbParameter dbpIdhorario = objConexion.dbProvider.CreateParameter();
                dbpIdhorario.DbType = DbType.Int32;
                dbpIdhorario.ParameterName = "@idHorario";
                dbpIdhorario.Direction = ParameterDirection.Input;
                dbpIdhorario.Value = idHorario;
                dbComando.Parameters.Add(dbpIdhorario);

                DbParameter dbpIdServicio = objConexion.dbProvider.CreateParameter();
                dbpIdServicio.DbType = DbType.Int32;
                dbpIdServicio.ParameterName = "@idServicio";
                dbpIdServicio.Direction = ParameterDirection.Input;
                dbpIdServicio.Value = idServicio;
                dbComando.Parameters.Add(dbpIdServicio);

                DbParameter dbpIdInstalacion = objConexion.dbProvider.CreateParameter();
                dbpIdInstalacion.DbType = DbType.Int32;
                dbpIdInstalacion.ParameterName = "@idInstalacion";
                dbpIdInstalacion.Direction = ParameterDirection.Input;
                dbpIdInstalacion.Value = idInstalacion;
                dbComando.Parameters.Add(dbpIdInstalacion);

                DbParameter dbpExiste = objConexion.dbProvider.CreateParameter();
                dbpExiste.DbType = DbType.Byte;
                dbpExiste.ParameterName = "@existe";
                dbpExiste.Direction = ParameterDirection.Output;
                dbpExiste.Value = 0;
                dbComando.Parameters.Add(dbpExiste);


                DbParameter dbpIncidencia = objConexion.dbProvider.CreateParameter();
                dbpIncidencia.DbType = DbType.String;
                dbpIncidencia.Size = 1000;
                dbpIncidencia.ParameterName = "@incidencia";
                dbpIncidencia.Direction = ParameterDirection.Output;
                dbpIncidencia.Value = "";
                dbComando.Parameters.Add(dbpIncidencia);

                DbParameter dbpFechaAsistencia = objConexion.dbProvider.CreateParameter();
                dbpFechaAsistencia.DbType = DbType.DateTime;
                dbpFechaAsistencia.ParameterName = "@fechaAsistencia";
                dbpFechaAsistencia.Direction = ParameterDirection.Input;
                dbpFechaAsistencia.Value = fechaAsistencia;
                dbComando.Parameters.Add(dbpFechaAsistencia);


                

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dsEmpleado);
                existe = Convert.ToByte(dbComando.Parameters["@existe"].Value);
                excep = Convert.ToString(dbComando.Parameters["@incidencia"].Value);
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
        #endregion

        #region inmovilidad
            public static DataTable consultarInmovilidadPorEmpleado(Guid idEmpleado, clsEntSesion objSesion)
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
                    dbComando.CommandText = "empleado.spuConsultarEmpleadoInmovilidad";
                    dbComando.Parameters.Clear();

                    clsParametro objParametro = new clsParametro();
                    objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", idEmpleado);
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
            public static bool insertarEmpleadoInmovilidad(clsEntEmpleadoInmovilidad objEmpleadoInvomilidad, clsEntSesion objSesion)
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
                    dbComando.CommandText = "empleado.spuInsertarEmpleadoInmovilidad";
                    dbComando.Parameters.Clear();

                    clsParametro objParametro = new clsParametro();

                    objParametro.llenarParametros(ref dbComando, DbType.Int32, "@idEmpleadoInmovilidad", objEmpleadoInvomilidad.idEmpleadoInmovilidad);
                    objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objEmpleadoInvomilidad.idEmpleado);
                    objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idMotivoInmovilidad", objEmpleadoInvomilidad.idMotivoInmovilidad);
                    objParametro.llenarParametros(ref dbComando, DbType.Guid, "@idAutoriza", objEmpleadoInvomilidad.idAutoriza);
                    objParametro.llenarParametros(ref dbComando, DbType.Byte, "@idJerarquiaAutoriza", objEmpleadoInvomilidad.idJerarquiaAutoriza);
                    objParametro.llenarParametros(ref dbComando, DbType.String, "@eiDescripcion", objEmpleadoInvomilidad.eiDescripcion);
                    objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@eiFechaInicio", objEmpleadoInvomilidad.eiFechaInicio);
                    objParametro.llenarParametros(ref dbComando, DbType.DateTime, "@eiFechaFin", objEmpleadoInvomilidad.eiFechaFin);
                    objParametro.llenarParametros(ref dbComando, DbType.Binary, "@eiImagen", objEmpleadoInvomilidad.eiImagen ?? (object)DBNull.Value);

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
        #endregion
    }
}
