using System.Collections.Generic;

namespace PubliPayments.Utiles
{
    public class Inicializa
    {
        public static void Inicializar(string connectionString)
        {
            //Inicializa todas las clases de utilidades que requieran inicializarse
            EntTraceLog.ConnectionString = connectionString;
        }
        public static void InicializarEmail(Dictionary<string,string> credenciales)
        {
            Email.Credenciales = credenciales;
        }
        
    }
}
