namespace ExamenEdenred.Controller.ServiceController
{
    using System;
    using System.Xml;
    using Entities.Models;

    public class ServiceController
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

        public System.ServiceModel.EndpointAddress ObtenEndpointAddress(Session session, string direcctorio, string servicio)
        {
            try
            {
                return session != null ? new System.ServiceModel.EndpointAddress(session.Conexion + direcctorio + servicio) : null;
            }
            catch (UriFormatException)
            {
                return null;
            }
        }
    }
}
