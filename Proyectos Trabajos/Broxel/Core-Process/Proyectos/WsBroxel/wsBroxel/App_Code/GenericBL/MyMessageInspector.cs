using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace wsBroxel.App_Code.GenericBL
{
    class MyMessageInspector : IClientMessageInspector
    {
        public string LastRequestXml { get; private set; }
        public string LastResponseXml { get; private set; }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            LastResponseXml = reply.ToString();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            LastRequestXml = request.ToString();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(LastRequestXml);
            writer.Flush();
            ms.Position = 0;
            var reader = XmlReader.Create(ms);
            request = Message.CreateMessage(reader, int.MaxValue, request.Version);
            return request;
        }
    }
}