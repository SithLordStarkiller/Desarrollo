using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntFuncionJavascript
    {
       
        public void CompementoFunciones(int idformulario)
        {

            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idFormulario", SqlDbType.Int) { Value = idformulario };
                instancia.EjecutarDataSet("SqlDefault", "FuncionesJsComplemento", parametros);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntFuncionJavascript", "CompementoFunciones : " + ex.Message);
            }
        }
    }
}
