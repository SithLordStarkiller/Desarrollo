
using System;
using System.Data;
using System.Data.SqlClient;


namespace PubliPayments.Entidades
{
   public  class EntMensajesServicios
    {
       public MensajesServiciosModel ObtenerMensajesServicios(string clave, int idAplicacion=0) 
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@Clave", SqlDbType.VarChar, 40) { Value = clave };
            parametros[1] = new SqlParameter("@idAplicacion", SqlDbType.Int) { Value = idAplicacion };
            MensajesServiciosModel result=null;
            var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerMensajesServicios", parametros);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result =
                    new MensajesServiciosModel(Convert.ToString(ds.Tables[0].Rows[0]["Titulo"]),
                        Convert.ToString(ds.Tables[0].Rows[0]["Mensaje"]),
                        Convert.ToString(ds.Tables[0].Rows[0]["Clave"]), Convert.ToString(ds.Tables[0].Rows[0]["Descripcion"]),
                        Convert.ToBoolean(ds.Tables[0].Rows[0]["EsHtml"]),
                        Convert.ToInt32(ds.Tables[0].Rows[0]["Tipo"]));
            }
            return result;

        }
    }
}
