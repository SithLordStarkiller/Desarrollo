using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;


namespace PubliPayments.Entidades.MYO
{
    public class EntOrdenesMyo
    {
        public static string AutorizaMyo(int idOrden, int estatus, int visita, string tipo, string idUsuario)
        {
            var sql = "exec ActualizaOrdenesMyo " +
                      "@idUsuario =" + idUsuario + "," +
                      "@idOrden = " + idOrden + "," +
                      "@Estatus = " + estatus + "," +
                      "@idVisita = " + visita + "," +
                      "@Tipo = N'" + tipo + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
            
	
            return "Autorizado";
        }
        public static string RechazaMyo(int idOrden, string idUsuario)
        {
            var sql = "exec ActualizaOrdenesMyo " +
                      "@idUsuario =" + idUsuario + "," +
                      "@idOrden = " + idOrden + "," +
                      "@Estatus = 2," +
                      "@idVisita = '-1'" + "," +
                      "@Tipo = N' '";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return "Rechazado";
        }
        public static string ReasignaMyo(int idOrden, string idUsuario)
        {
            var sql = "exec ActualizaOrdenesMyo " +
                      "@idUsuario =" + idUsuario + "," +
                      "@idOrden = " + idOrden + "," +
                      "@Estatus = 1," +
                      "@idVisita = '-1'" + "," +
                      "@Tipo = N'I'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return "Reasignado";
        }

        public static InfoOrdenMYOModel ObtenerInfoOrdenMyo(int idOrden)
        {
            var sql = "exec ObtenerInfoOrdenMyo " +
                      "@idOrden = " + idOrden;
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            var result = new InfoOrdenMYOModel
            {
                idVisita = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()),
                Etiqueta = ds.Tables[0].Rows[0][1].ToString(),
                mayorAlLimite = ds.Tables[0].Rows[0][2].ToString(),
                nombreUsuario = ds.Tables[0].Rows[0][3].ToString(),
                idUsuario = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString()),
                idFlock= Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString())
            };

            return result;
        }

        public List<FormularioModel> ObtenerFormularioXOrden(int idOrden, int idusuario, int captura)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[3];
                parametros[0] = new SqlParameter("@idorden", SqlDbType.Int) { Value = idOrden };
                parametros[1] = new SqlParameter("@idusuario", SqlDbType.Int) { Value = idusuario };
                parametros[2] = new SqlParameter("@Captura", SqlDbType.Int) { Value = captura };
                var result = instancia.EjecutarDataSet("SqlDefault", "ObtenerFormularioXOrden", parametros);
                return (from DataRow row in result.Tables[0].Rows select new FormularioModel(row["idFormulario"].ToString(), row["idAplicacion"].ToString(), row["Nombre"].ToString(), row["Descripcion"].ToString(), row["Version"].ToString(), row["Estatus"].ToString(), row["FechaAlta"].ToString(), row["Captura"].ToString(), row["Ruta"].ToString())).ToList();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFormulario", "ObtenerFormularioXOrden : " + ex.Message);
                return null;
            }
        }



        public static bool InsertarBitacoreRegistros(int idUsuario, int idVisita, int estatus, int idOrden, string tipo)
        {
            try
            {
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();
                var ds = new DataSet();
                var sc = new SqlCommand("InsertarBitacoraRegistros", cnn);
                var parametros = new SqlParameter[5];
                parametros[0] = new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = idUsuario };
                parametros[1] = new SqlParameter("@IdVisita", SqlDbType.Int) { Value = idVisita };
                parametros[2] = new SqlParameter("@Estatus", SqlDbType.Int) { Value = estatus };
                parametros[3] = new SqlParameter("@IdOrden", SqlDbType.Int) { Value = idOrden };
                parametros[4] = new SqlParameter("@Tipo", SqlDbType.VarChar) { Value = tipo };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);

                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "InsertarBitacoreRegistros - Error: " + ex.Message);
                return false;
            }
        }

    }
}
