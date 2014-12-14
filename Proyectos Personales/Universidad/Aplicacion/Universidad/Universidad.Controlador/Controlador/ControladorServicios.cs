using System;
using System.Xml;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.Controlador.Controlador
{
    public class ControladorServicios
    {
        public System.ServiceModel.BasicHttpBinding ObtenBasicHttpBinding()
        {
            var result = new System.ServiceModel.BasicHttpBinding
            {
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferPoolSize = int.MaxValue
            };

            result.GetType().GetProperty("ReaderQuotas").SetValue(result, XmlDictionaryReaderQuotas.Max, null);
            result.ReceiveTimeout = new TimeSpan(0, 20, 0);
            result.CloseTimeout = new TimeSpan(0, 20, 0);
            result.OpenTimeout = new TimeSpan(0, 20, 0);
            result.SendTimeout = new TimeSpan(0, 20, 0);

            return result;
        }

        public System.ServiceModel.EndpointAddress ObtenEndpointAddress(Sesion sesion, string direcctorio, string servicio)
        {
            try
            {
                return new System.ServiceModel.EndpointAddress(sesion.Conexion + direcctorio + servicio);
            }
            catch (System.UriFormatException e)
            {
                return null;
            }
        }
    }
}
