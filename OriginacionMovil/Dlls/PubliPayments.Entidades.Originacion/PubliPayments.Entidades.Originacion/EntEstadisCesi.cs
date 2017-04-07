using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PubliPayments.Entidades.Originacion
{
    public class EntEstadisCesi
    {
        public Dictionary<string,string> ObtenerEstadisCesi()
        {
            var sql = "Select EstadoDescripcion,IdEstado From EstadoCesi ";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            if (ds.Tables.Count <= 0) return null;
             return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].AsEnumerable().ToDictionary(row => row.Field<string>(0),row => row.Field<string>(1)) : null;
        }
    }
}
