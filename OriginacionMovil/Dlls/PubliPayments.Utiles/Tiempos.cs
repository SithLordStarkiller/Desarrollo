using System;
using System.Diagnostics;

namespace PubliPayments.Utiles
{
    public static class Tiempos
    {
        public static string Iniciar()
        {
            var guid = Guid.NewGuid().ToString();
            var frame = new StackFrame(1);
            var metodo = frame.GetMethod();
            string claseDesde = metodo.DeclaringType != null ? metodo.DeclaringType.Name : "Tiempos";

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, claseDesde,
                  string.Format("Tiempos,{0},{1},{2},{3}", metodo.Name, guid,
                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), "Inicio")); 
            return guid;
        }

        public static void Terminar(string guid)
        {
            var frame = new StackFrame(1);
            var metodo  = frame.GetMethod();
            string claseDesde = metodo.DeclaringType != null ? metodo.DeclaringType.Name : "Tiempos";

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, claseDesde,
                string.Format("Tiempos,{0},{1},{2},{3}", metodo.Name, guid,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), "Fin")); 
        }
    }
}
