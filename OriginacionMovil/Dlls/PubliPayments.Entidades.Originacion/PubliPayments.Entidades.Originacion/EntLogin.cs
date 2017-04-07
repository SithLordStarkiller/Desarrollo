using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class EntLogin
    {
        public Dictionary<string, string> LoginUser(string usuario, string password)
        {
            try
            {
                var sql = "exec loginUsuarioAplicacion " +
                          " @usuario=N'" + usuario + "'," +
                          " @password=N'" + password + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                var result = new Dictionary<string, string>();
                result["Valido"] = ds.Tables[0].Rows.Count > 0 ? "true" : "false";
                result["Rol"] = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0].ToString() : "";

                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "EntLogin", "LoginUser Error : " + ex.Message);
                var dic = new Dictionary<string, string> {{"Valido", "false"}, {"Rol", ""}};
                return dic;
            }
        }

        public static string ObtenerProductId(string Aplicacion)
        {
            var sql = "exec ObtenerProductId " +
                         " @aplicacion=N'" + Aplicacion + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            var result = ds.Tables[0].Rows[0][0].ToString();

            return result;
        }

        public static string RegistraLoginUsuario(string usuario)
        {
            var sql = "exec RegistraLoginUsuario " +
                      " @usuario=N'" + usuario + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return ds.Tables[0].Rows[0][0].ToString();
        }

        public static void RegistraEstadoUsuario(string estado, string idUsario)
        {
            var sql = "exec RegistraEstadoUsuario " +
                      " @idUsuario=N'" + idUsario + "',"+
                      " @estado=N'" + estado + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
        }

        public Dictionary<string, string> ObtenerEstadosUsuario(string usuario)
        {
            try
            {
                var sql = "exec ObtenerEstadosUsuario " +
                          " @usuario =N'" + usuario + "'";
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                var result = new Dictionary<string, string>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(row[0].ToString(), row[1].ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "EntLogin", "ObtenerEstadosUsuario Error : " + ex.Message);
                return new Dictionary<string, string>();
            }

        }
    }
}
