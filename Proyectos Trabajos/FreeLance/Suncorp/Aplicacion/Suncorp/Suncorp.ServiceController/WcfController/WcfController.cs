namespace Suncorp.ServiceController
{
    using Models;
    using System;
    using System.ServiceModel;
    using System.Xml;

    public class WcfController
    {
        private SessionSecurityWcf _sesion;

        public WcfController(SessionSecurityWcf session)
        {
            _sesion = session;
        }

        public BasicHttpBinding ObtenBasicHttpBinding()
        {
            var result = new BasicHttpBinding
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

        public EndpointAddress ObtenEndpointAddress()
        {
            try
            {
                return _sesion != null ? new EndpointAddress(_sesion.UrlServer + _sesion.Service) : null;
            }
            catch (UriFormatException)
            {
                return null;
            }
        }
    }
}
