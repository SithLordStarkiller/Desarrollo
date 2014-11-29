﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18051
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eClockBase.ES_Empleados {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ES_Empleados.S_Empleados")]
    public interface S_Empleados {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Empleados/DoWork", ReplyAction="urn:S_Empleados/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Empleados/ObtenPersonaID", ReplyAction="urn:S_Empleados/ObtenPersonaIDResponse")]
        System.IAsyncResult BeginObtenPersonaID(string SesionSeguridad, int NumeroDeEmpleado, System.AsyncCallback callback, object asyncState);
        
        int EndObtenPersonaID(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Empleados/ClaveEmpleado2PersonaID", ReplyAction="urn:S_Empleados/ClaveEmpleado2PersonaIDResponse")]
        System.IAsyncResult BeginClaveEmpleado2PersonaID(string SesionSeguridad, string NumeroDeEmpleado, System.AsyncCallback callback, object asyncState);
        
        int EndClaveEmpleado2PersonaID(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface S_EmpleadosChannel : eClockBase.ES_Empleados.S_Empleados, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenPersonaIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenPersonaIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ClaveEmpleado2PersonaIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ClaveEmpleado2PersonaIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class S_EmpleadosClient : System.ServiceModel.ClientBase<eClockBase.ES_Empleados.S_Empleados>, eClockBase.ES_Empleados.S_Empleados {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenPersonaIDDelegate;
        
        private EndOperationDelegate onEndObtenPersonaIDDelegate;
        
        private System.Threading.SendOrPostCallback onObtenPersonaIDCompletedDelegate;
        
        private BeginOperationDelegate onBeginClaveEmpleado2PersonaIDDelegate;
        
        private EndOperationDelegate onEndClaveEmpleado2PersonaIDDelegate;
        
        private System.Threading.SendOrPostCallback onClaveEmpleado2PersonaIDCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public S_EmpleadosClient() : 
                base(S_EmpleadosClient.GetDefaultBinding(), S_EmpleadosClient.GetDefaultEndpointAddress()) {
        }
        
        public S_EmpleadosClient(EndpointConfiguration endpointConfiguration) : 
                base(S_EmpleadosClient.GetBindingForEndpoint(endpointConfiguration), S_EmpleadosClient.GetEndpointAddress(endpointConfiguration)) {
        }
        
        public S_EmpleadosClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(S_EmpleadosClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
        }
        
        public S_EmpleadosClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(S_EmpleadosClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
        }
        
        public S_EmpleadosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("No se puede establecer el objeto CookieContainer. Asegúrese de que el enlace cont" +
                            "iene un objeto HttpCookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> DoWorkCompleted;
        
        public event System.EventHandler<ObtenPersonaIDCompletedEventArgs> ObtenPersonaIDCompleted;
        
        public event System.EventHandler<ClaveEmpleado2PersonaIDCompletedEventArgs> ClaveEmpleado2PersonaIDCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Empleados.S_Empleados.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void eClockBase.ES_Empleados.S_Empleados.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((eClockBase.ES_Empleados.S_Empleados)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((eClockBase.ES_Empleados.S_Empleados)(this)).EndDoWork(result);
            return null;
        }
        
        private void OnDoWorkCompleted(object state) {
            if ((this.DoWorkCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DoWorkCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DoWorkAsync() {
            this.DoWorkAsync(null);
        }
        
        public void DoWorkAsync(object userState) {
            if ((this.onBeginDoWorkDelegate == null)) {
                this.onBeginDoWorkDelegate = new BeginOperationDelegate(this.OnBeginDoWork);
            }
            if ((this.onEndDoWorkDelegate == null)) {
                this.onEndDoWorkDelegate = new EndOperationDelegate(this.OnEndDoWork);
            }
            if ((this.onDoWorkCompletedDelegate == null)) {
                this.onDoWorkCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDoWorkCompleted);
            }
            base.InvokeAsync(this.onBeginDoWorkDelegate, null, this.onEndDoWorkDelegate, this.onDoWorkCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Empleados.S_Empleados.BeginObtenPersonaID(string SesionSeguridad, int NumeroDeEmpleado, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenPersonaID(SesionSeguridad, NumeroDeEmpleado, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        int eClockBase.ES_Empleados.S_Empleados.EndObtenPersonaID(System.IAsyncResult result) {
            return base.Channel.EndObtenPersonaID(result);
        }
        
        private System.IAsyncResult OnBeginObtenPersonaID(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            int NumeroDeEmpleado = ((int)(inValues[1]));
            return ((eClockBase.ES_Empleados.S_Empleados)(this)).BeginObtenPersonaID(SesionSeguridad, NumeroDeEmpleado, callback, asyncState);
        }
        
        private object[] OnEndObtenPersonaID(System.IAsyncResult result) {
            int retVal = ((eClockBase.ES_Empleados.S_Empleados)(this)).EndObtenPersonaID(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenPersonaIDCompleted(object state) {
            if ((this.ObtenPersonaIDCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenPersonaIDCompleted(this, new ObtenPersonaIDCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenPersonaIDAsync(string SesionSeguridad, int NumeroDeEmpleado) {
            this.ObtenPersonaIDAsync(SesionSeguridad, NumeroDeEmpleado, null);
        }
        
        public void ObtenPersonaIDAsync(string SesionSeguridad, int NumeroDeEmpleado, object userState) {
            if ((this.onBeginObtenPersonaIDDelegate == null)) {
                this.onBeginObtenPersonaIDDelegate = new BeginOperationDelegate(this.OnBeginObtenPersonaID);
            }
            if ((this.onEndObtenPersonaIDDelegate == null)) {
                this.onEndObtenPersonaIDDelegate = new EndOperationDelegate(this.OnEndObtenPersonaID);
            }
            if ((this.onObtenPersonaIDCompletedDelegate == null)) {
                this.onObtenPersonaIDCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenPersonaIDCompleted);
            }
            base.InvokeAsync(this.onBeginObtenPersonaIDDelegate, new object[] {
                        SesionSeguridad,
                        NumeroDeEmpleado}, this.onEndObtenPersonaIDDelegate, this.onObtenPersonaIDCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Empleados.S_Empleados.BeginClaveEmpleado2PersonaID(string SesionSeguridad, string NumeroDeEmpleado, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginClaveEmpleado2PersonaID(SesionSeguridad, NumeroDeEmpleado, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        int eClockBase.ES_Empleados.S_Empleados.EndClaveEmpleado2PersonaID(System.IAsyncResult result) {
            return base.Channel.EndClaveEmpleado2PersonaID(result);
        }
        
        private System.IAsyncResult OnBeginClaveEmpleado2PersonaID(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            string NumeroDeEmpleado = ((string)(inValues[1]));
            return ((eClockBase.ES_Empleados.S_Empleados)(this)).BeginClaveEmpleado2PersonaID(SesionSeguridad, NumeroDeEmpleado, callback, asyncState);
        }
        
        private object[] OnEndClaveEmpleado2PersonaID(System.IAsyncResult result) {
            int retVal = ((eClockBase.ES_Empleados.S_Empleados)(this)).EndClaveEmpleado2PersonaID(result);
            return new object[] {
                    retVal};
        }
        
        private void OnClaveEmpleado2PersonaIDCompleted(object state) {
            if ((this.ClaveEmpleado2PersonaIDCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ClaveEmpleado2PersonaIDCompleted(this, new ClaveEmpleado2PersonaIDCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ClaveEmpleado2PersonaIDAsync(string SesionSeguridad, string NumeroDeEmpleado) {
            this.ClaveEmpleado2PersonaIDAsync(SesionSeguridad, NumeroDeEmpleado, null);
        }
        
        public void ClaveEmpleado2PersonaIDAsync(string SesionSeguridad, string NumeroDeEmpleado, object userState) {
            if ((this.onBeginClaveEmpleado2PersonaIDDelegate == null)) {
                this.onBeginClaveEmpleado2PersonaIDDelegate = new BeginOperationDelegate(this.OnBeginClaveEmpleado2PersonaID);
            }
            if ((this.onEndClaveEmpleado2PersonaIDDelegate == null)) {
                this.onEndClaveEmpleado2PersonaIDDelegate = new EndOperationDelegate(this.OnEndClaveEmpleado2PersonaID);
            }
            if ((this.onClaveEmpleado2PersonaIDCompletedDelegate == null)) {
                this.onClaveEmpleado2PersonaIDCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnClaveEmpleado2PersonaIDCompleted);
            }
            base.InvokeAsync(this.onBeginClaveEmpleado2PersonaIDDelegate, new object[] {
                        SesionSeguridad,
                        NumeroDeEmpleado}, this.onEndClaveEmpleado2PersonaIDDelegate, this.onClaveEmpleado2PersonaIDCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override eClockBase.ES_Empleados.S_Empleados CreateChannel() {
            return new S_EmpleadosClientChannel(this);
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Empleados)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.MaxReceivedMessageSize = int.MaxValue;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Empleados)) {
                return new System.ServiceModel.EndpointAddress("http://bishop.entrytec.com.mx/S_Empleados.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return S_EmpleadosClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_S_Empleados);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return S_EmpleadosClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_S_Empleados);
        }
        
        private class S_EmpleadosClientChannel : ChannelBase<eClockBase.ES_Empleados.S_Empleados>, eClockBase.ES_Empleados.S_Empleados {
            
            public S_EmpleadosClientChannel(System.ServiceModel.ClientBase<eClockBase.ES_Empleados.S_Empleados> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("DoWork", _args, callback, asyncState);
                return _result;
            }
            
            public void EndDoWork(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("DoWork", _args, result);
            }
            
            public System.IAsyncResult BeginObtenPersonaID(string SesionSeguridad, int NumeroDeEmpleado, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = SesionSeguridad;
                _args[1] = NumeroDeEmpleado;
                System.IAsyncResult _result = base.BeginInvoke("ObtenPersonaID", _args, callback, asyncState);
                return _result;
            }
            
            public int EndObtenPersonaID(System.IAsyncResult result) {
                object[] _args = new object[0];
                int _result = ((int)(base.EndInvoke("ObtenPersonaID", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginClaveEmpleado2PersonaID(string SesionSeguridad, string NumeroDeEmpleado, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = SesionSeguridad;
                _args[1] = NumeroDeEmpleado;
                System.IAsyncResult _result = base.BeginInvoke("ClaveEmpleado2PersonaID", _args, callback, asyncState);
                return _result;
            }
            
            public int EndClaveEmpleado2PersonaID(System.IAsyncResult result) {
                object[] _args = new object[0];
                int _result = ((int)(base.EndInvoke("ClaveEmpleado2PersonaID", _args, result)));
                return _result;
            }
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_S_Empleados,
        }
    }
}
