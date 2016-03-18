namespace SunCorp.Controller.Controller
{
    using System;
    using System.Xml;
    using SunCorp.Entities.Generic;

    public class ClientController
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

        public System.ServiceModel.EndpointAddress ObtenEndpointAddress(UserSession sesion, string directory, string service)
        {
            try
            {
                return sesion != null
                    ? new System.ServiceModel.EndpointAddress(sesion.UrlServer + directory + service)
                    : null;
            }
            catch (UriFormatException)
            {
                return null;
            }
        }
    }
}
