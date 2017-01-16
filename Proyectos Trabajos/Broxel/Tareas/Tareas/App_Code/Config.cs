using System;
using System.Configuration;
using System.Linq;
using PubliPayments.Entidades;

namespace PubliPayments
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
                var app = new SistemasCobranzaEntities().Aplicacion.FirstOrDefault(
                    x => x.Nombre == aplicacion);
                _aplicacionActual = app;
                return app;
            }
        }

        public static int LimiteOrdenGestor = (ConfigurationManager.AppSettings["LimiteOrdenGestor"] != null)
            ? Convert.ToInt32(ConfigurationManager.AppSettings["LimiteOrdenGestor"])
            : 120;
    }
}