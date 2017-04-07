using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class EReferenciaNumeroCredito
    {
        public string CV_CREDITO  { get; set; }
        public string CV_TELEFONO { get; set; }
       //public string CV_STATUS   { get; set; }
    }

    public class DReferenciaNumeroCredito
    {
        const string sSQL = @"exec ActualizaRefNumCredito @CV_CREDITO , @ACCION, @CODIGO";

        public static int EjecutaRefNumCredito(string sCV_CREDITO , string sACCION,string codigo = "")
        {
            var iRowAfectadas = 0;
            try
            {
                var conexion = ConexionSql.Instance;
                var cnn      = conexion.IniciaConexion();
                var cmd      = new SqlCommand(sSQL, cnn);
                cmd.Parameters.AddWithValue("@CV_CREDITO", sCV_CREDITO);
                cmd.Parameters.AddWithValue("@ACCION"    , sACCION);
                cmd.Parameters.AddWithValue("@CODIGO"    , codigo);
                iRowAfectadas = cmd.ExecuteNonQuery();
                conexion.CierraConexion(cnn);
                if (iRowAfectadas == 0) Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ActualizaRefNumCredito - Originacion", "NumCredito:" + sCV_CREDITO + " Error: No Se Actualizo o No Existe El Crédito");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ActualizaRefNumCredito - Originacion", "NumCredito:" + sCV_CREDITO + " Error:" + ex.ToString() );
                return 0;
            }

            return iRowAfectadas;
        }


        public static List<EReferenciaNumeroCredito> ObtenerRefNumCredito(string sCV_CREDITO, string sACCION)
        { 
            var ds    = new DataSet();
            var oList = new List<EReferenciaNumeroCredito>();

            try
            {
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var cmd = new SqlCommand(sSQL, cnn);
                cmd.Parameters.AddWithValue("@CV_CREDITO", sCV_CREDITO);
                cmd.Parameters.AddWithValue("@ACCION"    , sACCION);
                cmd.Parameters.AddWithValue("@CODIGO", "");
                var sda = new SqlDataAdapter(cmd);     
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
            }
            catch ( Exception ex )
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerRefNumCredito", "NumCredito:" + sCV_CREDITO + " Error:" + ex.ToString());
                return null;
            }

            if (ds.Tables.Count > 0)
            {
                if ( ds.Tables[0].Rows.Count > 0)
                {
                    oList = (from row in ds.Tables[0].AsEnumerable()
                            select new EReferenciaNumeroCredito
                            {
                                CV_CREDITO  = row.Field<string>("CV_CREDITO"),
                                CV_TELEFONO = row.Field<string>("CV_TELEFONO")
                            }).ToList();
                }               
            }
            else
            {
                return null;
            }

            return oList;

        }

    }

}
