namespace CrudMySql.Entidades
{
    using AccesoDatos;
    using MySql.Data.MySqlClient;
    using System.Data;

    public class EntClientesBroxel
    {
        public DataTable ObtenerTabla()
        {
            var instancia = new MySqlDbController();

            var param = new MySqlParameter[] { new MySqlParameter("@id", MySqlDbType.Int32) { Value = 2 } };

            var table = instancia.EjecutarDataSet("SELECT * FROM clientesbroxel WHERE id = @id", param, CommandType.Text);

            return table;
        }
    }
}
