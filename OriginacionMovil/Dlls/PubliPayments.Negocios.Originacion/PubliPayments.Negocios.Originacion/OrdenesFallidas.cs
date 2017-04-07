using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class OrdenesFallidas
    {
        private const string sStoredProcedure = "ObtenerOrdenesFallidas";
        private DataTable GetOrdenesFallidas()
        {
            var conexion   = ConexionSql.Instance;
            var cnn        = conexion.IniciaConexion();
            var sc         = new SqlCommand(sStoredProcedure, cnn) {CommandType = CommandType.StoredProcedure};
            var sda        = new SqlDataAdapter(sc);
            var ds         = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }


        private List<string> GetCorreos()
        {
            return new List<string>( ConfigurationManager.AppSettings["Correos"].Trim().Split(',') );
        }

        private string UnirOrdenes()
        {
            var sOrdenesUnidas          = new StringBuilder();
            DataTable dtOrdenesFallidas = null;

            try
            {
                dtOrdenesFallidas = GetOrdenesFallidas();
            }
            catch (Exception ex )
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "OrdenesFallidas.GetOrdenesFallidas", "Error: " + ex.Message);
            }


            if (dtOrdenesFallidas.Rows.Count > 0)
            {
                sOrdenesUnidas.AppendLine("Incidencia con creditos ");
                sOrdenesUnidas.AppendLine("Credito \t\tNSS   \t\tIdOrden\tMensaje");
                foreach (DataRow drOrdenFallida in dtOrdenesFallidas.Rows)
                {
                    sOrdenesUnidas.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}", drOrdenFallida["Num_cred"].ToString(), drOrdenFallida["cv_nss"].ToString(), drOrdenFallida["idOrden"].ToString(), drOrdenFallida["Texto"].ToString()));
                }

            }
            return sOrdenesUnidas.ToString();
        }


        public void EnviarOrdenesCorreo()
        {
            var lista     = GetCorreos();
            var sbOrdenes = UnirOrdenes();

            if ( !string.IsNullOrEmpty(sbOrdenes) )
            {
                try
                {
                    Email.EnviarEmail(lista,
                           "Incidencia Con Creditos Día " + DateTime.Now,
                           sbOrdenes);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "OrdenesFallidas", "Error: " + ex.Message);
                }
                
            }
           
        }

    }
}
