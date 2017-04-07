using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntSubFormulario
    {
        public SubFormularioModel InsertaSubFormulario(SubFormularioModel modelo)
        {
            try
            {
                var instancia = ConnectionDB.Instancia;

                var parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@idformulario", SqlDbType.Int) { Value = modelo.IdFormulario };
                parametros[1] = new SqlParameter("@Subformulario", SqlDbType.NVarChar, 50) { Value = modelo.SubFormulario };
                parametros[2] = new SqlParameter("@Clase", SqlDbType.NVarChar,50 ) { Value = modelo.Clase};

                var result = instancia.EjecutarEscalar("SqlDefault", "InsertaSubFormulario", parametros);
                if (!string.IsNullOrEmpty(result))
                {
                    var id = Convert.ToInt32(result);
                    modelo.IdSubFormulario = id;
                }

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntSubFormulario", string.Concat("InsertaSubFormulario: " + 0 + " ", ex.Message, (ex.InnerException.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.InnerException.Message) : "")));
                modelo.Error = ex.Message + " " + (ex.InnerException.InnerException != null ? string.Concat(" - Inner: ", ex.InnerException.InnerException.Message) : "");
            }
            return modelo;
        }
    }
}
