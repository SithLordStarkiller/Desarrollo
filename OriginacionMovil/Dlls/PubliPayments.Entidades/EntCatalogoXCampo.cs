using System.Data.SqlClient;
using PubliPayments.Utiles;
using System;
using System.Data;

namespace PubliPayments.Entidades
{
    public class EntCatalogoXCampo
    {
        public CatalogoXCampoModel InsertaCatalogoXCampo(CatalogoXCampoModel modelo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[5];

                parametros[0] = new SqlParameter("@idCampoFormulario", SqlDbType.Int) { Value = modelo.IdCampoFormulario };
                parametros[1] = new SqlParameter("@Texto", SqlDbType.NVarChar, 250) { Value = modelo.Texto };
                parametros[2] = new SqlParameter("@Valor", SqlDbType.NVarChar, 250) { Value = modelo.Valor };
                parametros[3] = new SqlParameter("@Auxiliar", SqlDbType.NVarChar, 250) { Value = modelo.Auxiliar };
                parametros[4] = new SqlParameter("@Ayuda", SqlDbType.NVarChar, 250) { Value = modelo.Ayuda };

                var result = instancia.EjecutarEscalar("SqlDefault", "InsertaCatalogoXCampo", parametros);
                if (!string.IsNullOrEmpty(result))
                {
                    modelo.IdCatalogoCampo = Convert.ToInt32(result);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntCatalogoXCampo", "InsertaCatalogoXCampo: "+ex.Message);
                modelo.Error = ex.Message ;
            }
            return modelo;
        }
    }
}
