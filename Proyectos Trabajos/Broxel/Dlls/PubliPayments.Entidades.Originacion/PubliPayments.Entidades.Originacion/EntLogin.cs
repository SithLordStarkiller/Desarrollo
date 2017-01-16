using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades.Originacion
{
    public class EntLogin
    {
        public static Dictionary<string, string> LoginUser(string usuario, string password)
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

            var result= new Dictionary<string, string>();
            result["Valido"] = ds.Tables[0].Rows.Count > 0 ? "true" : "false";
            result["Rol"] = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0].ToString() : "";

            return result;
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

            return "OK";
        }
    }
}
