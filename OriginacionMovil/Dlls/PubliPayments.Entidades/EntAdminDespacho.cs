using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntAdminDespacho
    {
        public DataTable ObtenerDespachos(string nombre, string nCorto, int estatus)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntAdminDespacho", "ObtenerDespachos");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ObtenerDespachos", cnn);
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@nombre", SqlDbType.NVarChar) { Value = nombre };
                parametros[1] = new SqlParameter("@nCorto", SqlDbType.NVarChar) { Value = nCorto };
                parametros[2] = new SqlParameter("@estatus", SqlDbType.Int) { Value = estatus };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntAdminDespacho", "Error en ObtenerDespachos: " + ex.Message);
                return null;
            }
        }

        public DominioModel ObtenerDatosDominio(int dominio)
        {
            var dominioModelo = new DominioModel();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntAdminDespacho", "ObtenerDatosDominioXidDominio ");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ObtenerDatosDominioXidDominio", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = dominio };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                //return ds.Tables[0];
                if (ds.Tables.Count > 0)
                {
                    dominioModelo = new DominioModel(ds.Tables[0].Rows[0]["nombreDominio"].ToString()
                        , ds.Tables[0].Rows[0]["nom_corto"].ToString(),
                        int.Parse(ds.Tables[0].Rows[0]["estatus"].ToString()));
                }
                return dominioModelo;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntAdminDespacho", "Error en ObtenerDatosDominioXidDominio: " + ex.Message);
                return null;
            }
        }

        public bool ActualizarDominio(int idDominio, string nombreDominio, string nomCorto, int estatus)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntAdminDespacho", "ActualizarDatosDominioXidDominio ");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ActualizarDatosDominioXidDominio", cnn);
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
                parametros[1] = new SqlParameter("@nombreDominio", SqlDbType.NVarChar) { Value = nombreDominio };
                parametros[2] = new SqlParameter("@nom_corto", SqlDbType.NVarChar) { Value = nomCorto };
                parametros[3] = new SqlParameter("@estatus", SqlDbType.Int) { Value = estatus };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return int.Parse(ds.Tables[0].Rows[0]["r"].ToString()) == 1;

                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntAdminDespacho", "Error en ActualizarDatosDominioXidDominio: " + ex.Message);
                return false;
            }
        }

        public bool InsertaDominio(string dominio, string nomCorto, string usuario, string nombre, string password, string email)
        {

            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntAdminDespacho", "InsertaDominio");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("InsertaDominio", cnn);
                var parametros = new SqlParameter[6];

                parametros[0] = new SqlParameter("@Dominio", SqlDbType.NVarChar) { Value = dominio };
                parametros[1] = new SqlParameter("@nom_corto", SqlDbType.NVarChar) { Value = nomCorto };
                parametros[2] = new SqlParameter("@Usuario", SqlDbType.NVarChar) { Value = usuario };
                parametros[3] = new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre };
                parametros[4] = new SqlParameter("@Password", SqlDbType.NVarChar) { Value = password };
                parametros[5] = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntAdminDespacho", "Error en InsertaDominio: " + ex.Message);
                return false;
            }
        }

        public bool ValidaNomCorto(string nomCorto,int idDominio)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntAdminDespacho", "ValidarDominioNomCorto");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ValidarDominioNomCorto", cnn);
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@nomCorto", SqlDbType.NVarChar) { Value = nomCorto };
                parametros[1] = new SqlParameter("@idDominio", SqlDbType.Int) { Value = idDominio };
                
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return int.Parse(ds.Tables[0].Rows[0]["nomCorto"].ToString()) == 1;
                
                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntAdminDespacho", "Error de ValidarDominioNomCorto: " + ex.Message);
                return true;
            }
        }

        public bool ValidaUsuario(string usuario)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "EntAdminDespacho", "ValidaUsuario");

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ValidarNombreUsuario", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Usuario", SqlDbType.NVarChar) { Value = usuario };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return int.Parse(ds.Tables[0].Rows[0]["idUsuario"].ToString()) == -1;

                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntAdminDespacho", "Error de ValidarDominioNomCorto: " + ex.Message);
                return false;
            }
        }

    }
}
