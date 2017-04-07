using System;
using System.Configuration;
using System.Linq;
using PubliPayments.Entidades;

namespace wsServices01800
{
    public static class Config
    {
        private static object objLock = new object();
        private static Aplicacion _aplicacionActual;
        public static Aplicacion AplicacionActual()
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
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}