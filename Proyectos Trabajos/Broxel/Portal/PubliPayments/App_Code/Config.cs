using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using PubliPayments.Entidades;

namespace PubliPayments
{
    public static class Config
    {
        private static object objLock = new object();
        private static Aplicacion _aplicacionActual;
        private static bool? _esCallCenter;
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
                catch (Exception ex)
                {
                    Trace.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " Config.AplicacionActual - Error:" +
                                    ex.Message);
                    return null;
                }
            }
        }

        public static int LimiteOrdenGestor = (ConfigurationManager.AppSettings["LimiteOrdenGestor"] != null)
            ? Convert.ToInt32(ConfigurationManager.AppSettings["LimiteOrdenGestor"])
            : 120;

        public static bool EsCallCenter
        {
            get
            {
                if (_esCallCenter == null)
                {
                    _esCallCenter = (ConfigurationManager.AppSettings["EsCallCenter"] != null) &&
                                    Convert.ToBoolean(ConfigurationManager.AppSettings["EsCallCenter"]);
                }

                return _esCallCenter.Value;
            }
        }
    }
}