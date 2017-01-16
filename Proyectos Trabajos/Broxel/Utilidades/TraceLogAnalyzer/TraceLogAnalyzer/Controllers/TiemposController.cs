using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TraceLogAnalyzer.Models;

namespace TraceLogAnalyzer.Controllers
{
    public class TiemposController : Controller
    {
        // GET: Tiempos
        public ActionResult Index()
        {
            var swTotal = new Stopwatch();
            swTotal.Start();

            var tiempoModels = new List<TiempoModel>();
            List<IdTexto> tl;

            var swBase = new Stopwatch();
            swBase.Start();
            using (var dataContext = new SistemasCobranzaEntities())
            {
                dataContext.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

                var trace = from traceLog in dataContext.TraceLog
                    where traceLog.Texto.StartsWith("Tiempos,")
                    select new IdTexto
                    {
                        Id = traceLog.id,
                        Texto = traceLog.Texto
                    };
                trace = trace.OrderByDescending(x => x.Id);

                trace = trace.Take(20000);

                tl = trace.ToList();
            }
            swBase.Stop();
            ViewBag.TiempoBase = swBase.ElapsedMilliseconds;

            foreach (var t in tl)
            {
                var tiempo = t.Texto.Split(',');
              
                if (tiempo[4] == "Inicio")
                {
                    var tm = new TiempoModel();
                    tm.GUID = tiempo[2];
                    tm.Origen = tiempo[1];
                    tm.Inicio = tiempo[3];

                    var fin =
                        tl.FirstOrDefault(
                            x =>
                                x.Texto.IndexOf(tm.GUID, StringComparison.Ordinal) > 0 &&
                                x.Texto.IndexOf("Fin", StringComparison.Ordinal) > 0);

                    if (fin != null)
                    {
                        var tiempoFin = fin.Texto.Split(',');
                        tm.Fin = tiempoFin[3];
                        if (tm.Inicio != tm.Fin)
                        {
                            var diff = DateTime.Parse(tm.Fin) - DateTime.Parse(tm.Inicio);
                            tm.Diferencia = diff.Milliseconds + diff.Seconds*1000 + diff.Minutes*60*1000;
                        }
                    }

                    if (tm.Diferencia > 1000) //Solo se muestran los que se tardan mas de 1 segundo
                        tiempoModels.Add(tm);
                }
            }

            tiempoModels = tiempoModels.OrderByDescending(x => x.Diferencia).ToList();

            swTotal.Stop();
            ViewBag.TiempoTotal = swTotal.ElapsedMilliseconds;
            return View(tiempoModels);
        }
    }
}
