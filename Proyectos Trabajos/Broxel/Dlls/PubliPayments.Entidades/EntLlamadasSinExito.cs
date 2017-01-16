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
    public class EntLlamadasSinExito
    {
        /// <summary>
        /// Borra el registro de una llamada sin éxito y pasa el registro a bitácora
        /// </summary>
        /// <param name="credito">Numero de crédito</param>
        public string BorrarLlamadaSinExito(string credito)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@cv_credito", SqlDbType.VarChar,15) { Value = credito };
            return instancia.EjecutarEscalar("SqlDefault", "BorrarLlamadaSinExito", parametros);
        }
    }
}
