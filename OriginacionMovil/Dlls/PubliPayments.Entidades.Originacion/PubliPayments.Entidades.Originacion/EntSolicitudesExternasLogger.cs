using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliPayments.Entidades.Originacion
{
    public class EntSolicitudesExternasLogger
    {
        public static void AddLog(DateTime fecha, string descripcion, string Json, string url)
        {
            using (var ctx = new SistemasOriginacionMovilEntities())
            {
                ctx.SolicitudesExternas.Add(new SolicitudesExternas()
                {
                    Fecha = fecha,
                    JSON = Json,
                    URL = url,
                    Descripcion = descripcion
                });
                ctx.SaveChanges();
            }
        }
    }
}
