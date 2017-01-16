using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades.Originacion
{
    public static class  EntDatosProspecto
    {

        public static DataSet obtenerDatosRegistro(string nss, string plazo,string centralObrera)
        {
            string sql = "exec obtenerDatosRegistro " +
                         " @nss=N'"+nss+"',"+
                         " @plazo=N'" + plazo + "'," +
                         " @centObrera=N'"+centralObrera+"'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return ds;
        }

        public static void ReenviarAMovil(string idOrden,string nss,string tipo)
        {
            var ds = new DataSet();

            string sql = "exec ReenviarAMovil " +
                         " @idOrden = " + idOrden+","+
                         " @nss = N'"+nss+"',"+
                         " @tipo = N'" + tipo + "'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds1 = new DataSet();
            sda.Fill(ds1);
            conexion.CierraConexion(cnn);
        }

        public static DataSet ObtenerDocumentos(string idOrden)
        {
            string sql = "exec ObtenerDocumentosOrden " +
                         " @idOrden=" + idOrden ;
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return ds;
        }

        public static void RegistrarEntidadFinanciera(string entidad,string idOrden)
        {
            string sql = "exec RegistrarEntidadFinanciera " +
                         " @idOrden='" + idOrden + "'," +
                         " @entidad=N'" + entidad+"'";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
        }

        public static void InsertarRespuesta(string campo,string valor, string idOrden)
        {
            string sql = "exec InsertarRespuesta " +
                         "@idOrden =" + idOrden + ", " +
                         "@NombreCampo ='" + campo + "'," +
                         "@Valor ='" + valor + "',  " +
                         "@idFormulario=-1";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);
        }

    }
}
