//using System;
//using System.Diagnostics;

//namespace PubliPayments.Entidades
//{
//    public sealed class EntTraceLog
//    {
//        public bool GuardaTraceLog(int idUsuario, int tipoTraceLog, string origen, string texto)
//        {
//            try
//            {
//                //Se utiliza EntityFramework
//                var context = new SistemasCobranzaEntities();

//                context.TraceLog.Add(new TraceLog
//                {
//                    idUsuario = idUsuario,
//                    Fecha = DateTime.Now,
//                    idTipoLog = Convert.ToInt32(tipoTraceLog),
//                    Pagina = origen,
//                    Texto = texto
//                });

//                context.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Trace.WriteLine(texto + " - " + ex.Message);
//                return false;
//            }
//            return true;
//        }
//    }
//}

