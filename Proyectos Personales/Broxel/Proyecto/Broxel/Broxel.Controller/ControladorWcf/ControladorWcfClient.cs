namespace Broxel.Controller.ControladorWcf
{
    using System;
    using System.ServiceModel;
    using System.Xml;

    public class ControladorWcfClient
    {
        private readonly string _urlServe;

        public ControladorWcfClient(string urlServe)
        {
            _urlServe = urlServe;
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

        public EndpointAddress ObtenEndpointAddress(string directory, string service)
        {
            try
            {
                return new EndpointAddress(_urlServe + directory + service);
            }
            catch (UriFormatException)
            {
                return null;
            }
        }
    }
}
