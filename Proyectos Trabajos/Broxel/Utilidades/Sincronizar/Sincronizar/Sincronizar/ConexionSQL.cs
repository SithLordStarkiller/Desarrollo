using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sincronizar
{
    public class ConexionSql
    {
        private readonly object _lockObject = new object();
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString;
        public List<OrdenSincronizada> ObtenerOrdenes()
        {
            var cnn = new SqlConnection(_connectionString);
            cnn.Open();
            var command = new SqlCommand("SELECT o.idOrden,o.Estatus, u.Usuario, o.FechaEnvio, o.FechaRecepcion FROM Ordenes o INNER JOIN Usuario u ON o.idUsuario = u.idUsuario WHERE (o.idUsuario != 0 OR idVisita > 1)", cnn) { CommandType = CommandType.Text };
            var da = new SqlDataAdapter(command);
            var ds = new DataSet();
            da.Fill(ds);
            cnn.Close();

            var ordenes = new List<OrdenSincronizada>();

            Parallel.ForEach(ds.Tables[0].AsEnumerable() , dr =>
            {
                var item = new OrdenSincronizada
                {
                    IdOrden = dr["idOrden"].ToString(),
                    Estatus = Convert.ToInt32(dr["Estatus"]),
                    Usuario = dr["Usuario"].ToString(),
                    FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]),
                   
                };
                if (dr["FechaRecepcion"] != DBNull.Value)
                    item.FechaRecepcion = Convert.ToDateTime(dr["FechaRecepcion"]);
                lock (_lockObject)
                {
                    ordenes.Add(item);
                }
            });


            return ordenes;
        }

        internal void GuardarHistoria(OrderHistory oh)
        {
            
        }

    }
}
