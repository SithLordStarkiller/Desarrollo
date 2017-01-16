using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntTelefonos
    {
        public List<TelefonoModel> ObtenerTelefonos(string telefono)
        {
            var db = ConnectionDB.Instancia;
            string nir, serie, numeracion;
            nir = telefono.Substring(0, 2);
            serie = telefono.Substring(2, 4);
            numeracion = telefono.Substring(6, 4);
            var existe = db.EjecutarDataSet("SqlDefault", "ValidarTelefono", new SqlParameter[3]{
                new SqlParameter("@NIR",SqlDbType.NChar){Value=nir},
                new SqlParameter("@SERIE",SqlDbType.NChar){Value=serie},
                new SqlParameter("@NUMERACION",SqlDbType.NChar){Value=numeracion}
            }).Tables[0];
            return existe.ToList<TelefonoModel>();
        }


        public List<TelefonoModel> ValidarTipoTelefono(string telefono)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@Telefono", SqlDbType.VarChar,10) { Value = telefono };
            var ds = instancia.EjecutarDataSet("SqlDefault", "ValidarTipoTelefono", parametros);

            if (ds!=null && ds.Tables.Count>1 && ds.Tables[0].Rows.Count>1)
            {
               return ds.Tables[0].ToList<TelefonoModel>();
            }
            return null;
        }

        public void InsertarTipoTelefono( int idorden)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idorden", SqlDbType.Int) { Value = idorden };
                instancia.EjecutarDataSet("SqlDefault", "InsertarTipoTelefono", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "InsertarTipoTelefono","Orden: "+idorden + " error:" + ex.Message);
                
            }
            
        }
    }
}
