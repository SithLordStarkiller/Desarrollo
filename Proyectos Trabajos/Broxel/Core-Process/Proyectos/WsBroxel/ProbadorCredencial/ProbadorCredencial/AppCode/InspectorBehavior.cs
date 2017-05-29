using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ProbadorCredencial.AppCode
{
    class InspectorBehavior : IEndpointBehavior
    {
        private readonly MyMessageInspector _myMessageInspector = new MyMessageInspector();

        public string LastRequestXml
        {
            get { return _myMessageInspector.LastRequestXml; }
        }

        public string LastResponseXml
        {
            get { return _myMessageInspector.LastResponseXml; }
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // No implementation necessary
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(_myMessageInspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // No implementation necessary
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // No implementation necessary
        }
    }
}
