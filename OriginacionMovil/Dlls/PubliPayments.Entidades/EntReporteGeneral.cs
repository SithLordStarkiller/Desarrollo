using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades
{
    public class EntReporteGeneral
    {
        public List<FechasReporteGeneralModel> ObtenerFechasReporteGeneral(int tipo, int idPadre)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var sc = new SqlCommand("ObtenerFechasReportes", cnn);
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@TipoReporte", SqlDbType.Int) {Value = tipo};
            parametros[1] = new SqlParameter("@idPadre", SqlDbType.Int) {Value = idPadre};
            sc.Parameters.AddRange(parametros);
            sc.CommandType = CommandType.StoredProcedure;
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            instancia.CierraConexion(cnn);

            var fechas = new List<FechasReporteGeneralModel>();
            fechas.Add(new FechasReporteGeneralModel {Fecha = "Seleccione una fecha", FechaReporte = "999999"});
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    fechas.Add(new FechasReporteGeneralModel
                    {
                        Fecha = row["Fecha"].ToString(),
                        FechaReporte = row["FechaReporte"].ToString()
                    });
                }
            }
            return fechas;
        }
    }
}
