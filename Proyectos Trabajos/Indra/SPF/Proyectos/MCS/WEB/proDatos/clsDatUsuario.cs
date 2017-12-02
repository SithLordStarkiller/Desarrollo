using System;
using System.Data;
using System.Data.Common;
using proUtilerias;
using SICOGUA.Datos;
using proEntidades;
using System.Collections.Generic;

namespace SICOGUA.Seguridad
{
    public class clsDatUsuario
    {
        public static DataTable consultarPermiso(clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DataTable dtResultado = new DataTable();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spConsultarUsuario";
                dbComando.Parameters.Clear();

                DbParameter dbpLogin = objConexion.dbProvider.CreateParameter();

                dbpLogin.DbType = DbType.String;
                dbpLogin.ParameterName = "@login";
                dbpLogin.Value = objSesion.usuario.UsuLogin;
                dbComando.Parameters.Add(dbpLogin);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

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

            return dtResultado;
        }

        public static Int16 crearUsuario(clsEntUsuario objUsuario, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spCrearUsuario";
                dbComando.Parameters.Clear();

                clsParametro objParametros = new clsParametro();

                objParametros.llenarParametros(ref dbComando, DbType.String, "@strLogin", objUsuario.UsuLogin);
                objParametros.llenarParametros(ref dbComando, DbType.Guid, "@idEmpleado", objUsuario.IdEmpleado);

                objParametros.llenarParametros(ref dbComando, DbType.String, "@strPassword", clsSegRijndaelSimple.Decrypt(objUsuario.UsuContrasenia));
                objParametros.llenarParametros(ref dbComando, DbType.Int16, "@idPerfil", objUsuario.Perfil.IdPerfil);
                objParametros.llenarParametros(ref dbComando, DbType.Int16, "@intAdministrador", objUsuario.Administrador);
                objParametros.llenarParametros(ref dbComando, DbType.Int16, ParameterDirection.Output, "@intRegresa", -1);

                dbComando.ExecuteNonQuery();

                Int16 a = (Int16)dbComando.Parameters["@intRegresa"].Value;

                return a;
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
        }

        public static bool eliminarUsuario(clsEntUsuario objUsuario, clsEntSesion objSesion)
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
                dbComando.CommandText = "seguridad.spEliminarUsuario";
                dbComando.Parameters.Clear();

                clsParametro objParametros = new clsParametro();

                objParametros.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", objUsuario.IdUsuario);

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

        public static bool actualizarUsuario(clsEntUsuario objUsuario, clsEntSesion objSesion)
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
                dbComando.CommandText = "seguridad.spActualizarUsuario";
                dbComando.Parameters.Clear();

                clsParametro objParametros = new clsParametro();

                objParametros.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", objUsuario.IdUsuario);
                objParametros.llenarParametros(ref dbComando, DbType.Int16, "@idPerfil", objUsuario.Perfil.IdPerfil);

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
                    //throw dbExRoll;
                }
                //throw dbEx;
            }
            catch (Exception Ex)
            {
                try
                {
                    dbTrans.Rollback();
                }
                catch (DbException dbExRoll)
                {
                    //throw dbExRoll;
                }
                //throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return false;
        }

        public static bool actualizarUsuarioContrasenia(clsEntUsuario objUsuario, clsEntSesion objSesion)
        {
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spActualizarUsuarioContrasenia";
                dbComando.Parameters.Clear();

                clsParametro objParametros = new clsParametro();

                objParametros.llenarParametros(ref dbComando, DbType.String, "@usuUsuario", objUsuario.UsuLogin);
                objParametros.llenarParametros(ref dbComando, DbType.String, "@empAnterior", objUsuario.UsuContrasenia);
                objParametros.llenarParametros(ref dbComando, DbType.String, "@empNueva", objUsuario.UsuConfirmacion);

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
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
        }

        public static clsEntUsuario consultarUsuario(Int32 idUsuario, clsEntSesion objSesion)
        {
            clsEntUsuario objUsuario = null;
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DataTable dtResultado = new DataTable();

            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            DbDataAdapter dbAdapter = objConexion.dbProvider.CreateDataAdapter();

            DbTransaction dbTrans = dbConexion.BeginTransaction();
            dbComando.Connection = dbConexion;
            dbComando.Transaction = dbTrans;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spConsultarUsuarioPorId";
                dbComando.Parameters.Clear();

                DbParameter dbpInput = objConexion.dbProvider.CreateParameter();

                dbpInput.DbType = DbType.String;
                dbpInput.ParameterName = "@IdUsuario";
                dbpInput.Value = idUsuario;
                dbComando.Parameters.Add(dbpInput);

                dbAdapter.SelectCommand = dbComando;

                dbAdapter.Fill(dtResultado);

                dbTrans.Commit();

                List<clsEntUsuario> usrs = CollectionUtil.ToCollection<clsEntUsuario>(dtResultado);
                if (usrs.Count > 0)
                {
                    objUsuario = usrs[0];
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
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return objUsuario;
        }

        public static bool actualizarUsuarioServicioInstalacion(int idUsuario, string idZona, string idServicio, string idInstalacion, bool usiConsultar, bool usiAsignar, bool esVigente, clsEntSesion objSesion)
        {
            bool result = false;
            clsDatConexion objConexion = new clsDatConexion();
            DbConnection dbConexion = objConexion.getConexion(objSesion);
            DbCommand dbComando = objConexion.dbProvider.CreateCommand();
            dbComando.Connection = dbConexion;

            try
            {
                dbComando.CommandType = CommandType.StoredProcedure;
                dbComando.CommandText = "seguridad.spAgregarUsuarioServicioInstalacion";
                dbComando.Parameters.Clear();

                clsParametro objParametros = new clsParametro();

                objParametros.llenarParametros(ref dbComando, DbType.Int32, "@idUsuario", idUsuario);
                objParametros.llenarParametros(ref dbComando, DbType.String, "@idZona", string.IsNullOrEmpty(idZona) ? (object)DBNull.Value : idZona);
                objParametros.llenarParametros(ref dbComando, DbType.String, "@idServicio", string.IsNullOrEmpty(idServicio) ? (object)DBNull.Value : idServicio);
                objParametros.llenarParametros(ref dbComando, DbType.String, "@idInstalacion", string.IsNullOrEmpty(idInstalacion) ? (object)DBNull.Value : idInstalacion);
                objParametros.llenarParametros(ref dbComando, DbType.Boolean, "@usiConsultar", usiConsultar);
                objParametros.llenarParametros(ref dbComando, DbType.Boolean, "@usiAsignar", usiAsignar);
                objParametros.llenarParametros(ref dbComando, DbType.Boolean, "@usuServInstVigente", esVigente);
                //objParametros.llenarParametros(ref dbComando, DbType.Boolean, "@result", result); // 1/agosto/quien le quito este parametro al método?

                dbComando.ExecuteNonQuery();

                result = true; // ((bool)(dbComando.Parameters["@result"].Value));
            }
            catch (DbException dbEx)
            {
                throw dbEx;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                clsDatConexion.cerrarTransaccion(dbConexion);
            }
            return result;
        }
    }
}
