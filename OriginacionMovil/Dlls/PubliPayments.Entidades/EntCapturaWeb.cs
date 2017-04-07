using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntCapturaWeb
    {
        public PorcentajePermitidoModel ObtenerPorcentajeCapturaWeb(int idUsuarioPadre, int idUsuario)
        {
            var guidTiempos = Tiempos.Iniciar();
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, "EntCapturaWeb",
                    "ObtenerPorcentajeCapturaWeb: " + idUsuarioPadre);

                var p = new PorcentajePermitidoModel();

                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand("ObtenerPorcentajeCapturaWeb", cnn);
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) {Value = idUsuarioPadre};
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    p.CantidadActual = Convert.ToInt32(ds.Tables[0].Rows[0]["CantidadActual"]);
                    p.PorcentajePermitido = Convert.ToSingle(ds.Tables[0].Rows[0]["PorcentajePermitido"]);
                }
                Tiempos.Terminar(guidTiempos);

                return p;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuario, "EntCapturaWeb",
                    "ObtenerPorcentajeCapturaWeb: " + idUsuarioPadre + " - Error " + ex.Message);
                Tiempos.Terminar(guidTiempos);
                return null;
            }
           
        }
    }
}
