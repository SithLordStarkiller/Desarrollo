using System;
using System.Data;
using System.Data.SqlClient;

namespace PubliPayments.Entidades
{
    public class EntBloqueoConcurrencia
    {
        public BloqueoConcurrenciaModel BloquearConcurrencia(BloqueoConcurrenciaModel modelo)
        {
            var instancia = ConnectionDB.Instancia;
          
            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@Llave", SqlDbType.VarChar, 50) { Value = modelo.Llave };
            parametros[1] = new SqlParameter("@Aplicacion", SqlDbType.VarChar, 50) {Value = modelo.Aplicacion};
            parametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar, 50) { Value = modelo.Origen };
            parametros[3] = new SqlParameter("@Estatus", SqlDbType.Int) { Value = modelo.Estatus };

            var ds = instancia.EjecutarDataSet("SqlDefault", "BloquearConcurrencia", parametros);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelo.Error = Convert.ToInt32(ds.Tables[0].Rows[0]["ErrorNumber"]);
                modelo.ErrorMensaje = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return modelo;
        }

        public BloqueoConcurrenciaModel ObtenerConcurrencia(BloqueoConcurrenciaModel modelo)
        {
            modelo.Estatus = -1;

            var instancia = ConnectionDB.Instancia;

            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@Llave", SqlDbType.VarChar, 50) { Value = modelo.Llave };
            parametros[1] = new SqlParameter("@Aplicacion", SqlDbType.VarChar, 50) { Value = modelo.Aplicacion };
            parametros[2] = new SqlParameter("@Origen", SqlDbType.VarChar, 50) { Value = modelo.Origen };
            parametros[3] = new SqlParameter("@id", SqlDbType.Int) {Value = modelo.Id};

            var ds = instancia.EjecutarDataSet("SqlDefault", "ObtenerConcurrencia", parametros);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                modelo.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                modelo.Estatus = Convert.ToInt32(ds.Tables[0].Rows[0]["Estatus"]);
                modelo.Resultado = ds.Tables[0].Rows[0]["Resultado"].ToString();
            }
            return modelo;
        }

        public int ActualizarConcurrencia(BloqueoConcurrenciaModel modelo)
        {
            var instancia = ConnectionDB.Instancia;

            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@id", SqlDbType.Int) { Value = modelo.Id };
            parametros[1] = new SqlParameter("@Estatus", SqlDbType.Int) { Value = modelo.Estatus };
            parametros[2] = new SqlParameter("@Resultado", SqlDbType.VarChar, 100) { Value = modelo.Resultado };

            var resultado = instancia.EjecutarNonQuery("SqlDefault", "ActualizarConcurrencia", parametros);

            return resultado;
        }
    }
}
