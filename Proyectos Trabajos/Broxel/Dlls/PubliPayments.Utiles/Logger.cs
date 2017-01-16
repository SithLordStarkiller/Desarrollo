using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PubliPayments.Utiles
{
    public static class Logger
    {
        public static object Lock = new object();
        private static readonly List<RegistroLog> ListaLog = new List<RegistroLog>();

        public static void WriteLine(TipoTraceLog tipo, int idUsuario, string origen,  string linea)
        {
            var l = new RegistroLog
            {
                TipoTraceLog = tipo,
                IdUsuario = idUsuario,
                Origen = origen.Length > 250 ? origen.Substring(0, 250) : origen,
                Linea = linea.Length > 500 ? linea.Substring(0, 500) : linea
            };
            var t = new Task(() => AddLog(l));
            t.Start();

            //Guardo una segunda linea si es muy grande el texto
            if (linea.Length > 500)
            {
                linea = linea.Substring(500);
                var ll = new RegistroLog
                {
                    TipoTraceLog = tipo,
                    IdUsuario = idUsuario,
                    Origen = origen.Length > 250 ? origen.Substring(0, 250) : origen,
                    Linea = linea.Length > 500 ? linea.Substring(0, 500) : linea
                };
                var tl = new Task(() => AddLog(ll));
                tl.Start();
            }
        }

        private static void AddLog(RegistroLog l)
        {
            lock (Lock)
            {
                ListaLog.Add(l);
            }

            var t = new Task(WriteLog);
            t.Start();
        }

        private static void WriteLog()
        {
            lock (Lock)
            {
                if (ListaLog.Count <= 0) return;
                var intento = 0;

                var ent = new EntTraceLog();

                while (ListaLog.Count > 0 && intento < 3)
                {
                    var log = ListaLog[0];
                    bool result;
                    if (log.TipoTraceLog != TipoTraceLog.TraceDisco)
                    {
                        result = ent.GuardaTraceLog(log.IdUsuario, Convert.ToInt32(log.TipoTraceLog), log.Origen,
                            log.Linea);
                    }
                    else
                    {
                        Trace.WriteLine(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + " -" + log.Origen + "-" +
                                        log.IdUsuario + "- " + log.Linea); //Usando TraceLinstener del config
                        result = true;
                    }

                    if (result)
                        ListaLog.RemoveAt(0);
                    else
                        intento++;
                }
            }
        }

        public enum TipoTraceLog
        {
            TraceDisco = 0,
            Trace = 1,
            Log = 2,
            Error = 3
        }
    }

    internal class RegistroLog
    {
        public Logger.TipoTraceLog TipoTraceLog { get; set; }
        public int IdUsuario { get; set; }
        public string Origen { get; set; }
        public string Linea { get; set; }
    }
}