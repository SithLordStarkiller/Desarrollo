namespace Conexion_sql
{
    using System.Data.SqlClient;

    public class Conexion
    {

        public static SqlConnection Conectar()
        {
            var conn = new SqlConnection(@"Data Source=STARKILLER1\MSSQLSERVER2012;Initial Catalog=Registro;Integrated Security=True");
            conn.Open();
            return conn;
        }

    }
}
