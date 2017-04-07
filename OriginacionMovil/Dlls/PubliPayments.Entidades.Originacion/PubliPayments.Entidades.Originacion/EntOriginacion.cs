using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliPayments.Entidades.Originacion
{
    public class EntOriginacion
    {
        private static object objLock = new object();
        private static Aplicacion _aplicacionActual;
        public static Aplicacion ObtenerDatosAplicacion()
        {
            lock (objLock)
            {
                if (_aplicacionActual != null)
                    return _aplicacionActual;
                string aplicacion = ConfigurationManager.AppSettings["Aplicacion"];
                try
                {
                    var app = new SistemasCobranzaEntities().Aplicacion.FirstOrDefault(
                        x => x.Nombre == aplicacion);
                    _aplicacionActual = app;
                    return app;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " Config.AplicacionActual - Error:" +
                                    ex.Message);
                    return null;
                }
            }
        }
    }
}
