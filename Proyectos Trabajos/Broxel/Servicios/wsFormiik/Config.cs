using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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