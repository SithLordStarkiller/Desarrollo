﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eClockBase.ES_Periodos {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ES_Periodos.S_Periodos")]
    public interface S_Periodos {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Periodos/DoWork", ReplyAction="urn:S_Periodos/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Periodos/ObtenListado", ReplyAction="urn:S_Periodos/ObtenListadoResponse")]
        System.IAsyncResult BeginObtenListado(string SesionSeguridad, System.DateTime FechaDesde, System.DateTime FechaHasta, int EdoPeriodo, int SuscripcionID, System.AsyncCallback callback, object asyncState);
        
        string EndObtenListado(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface S_PeriodosChannel : eClockBase.ES_Periodos.S_Periodos, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenListadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenListadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class S_PeriodosClient : System.ServiceModel.ClientBase<eClockBase.ES_Periodos.S_Periodos>, eClockBase.ES_Periodos.S_Periodos {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenListadoDelegate;
        
        private EndOperationDelegate onEndObtenListadoDelegate;
        
        private System.Threading.SendOrPostCallback onObtenListadoCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public S_PeriodosClient() : 
                base(S_PeriodosClient.GetDefaultBinding(), S_PeriodosClient.GetDefaultEndpointAddress()) {
        }
        
        public S_PeriodosClient(EndpointConfiguration endpointConfiguration) : 
                base(S_PeriodosClient.GetBindingForEndpoint(endpointConfiguration), S_PeriodosClient.GetEndpointAddress(endpointConfiguration)) {
        }
        
        public S_PeriodosClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(S_PeriodosClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
        }
        
        public S_PeriodosClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(S_PeriodosClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
        }
        
        public S_PeriodosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> DoWorkCompleted;
        
        public event System.EventHandler<ObtenListadoCompletedEventArgs> ObtenListadoCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Periodos.S_Periodos.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void eClockBase.ES_Periodos.S_Periodos.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((eClockBase.ES_Periodos.S_Periodos)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((eClockBase.ES_Periodos.S_Periodos)(this)).EndDoWork(result);
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
        System.IAsyncResult eClockBase.ES_Periodos.S_Periodos.BeginObtenListado(string SesionSeguridad, System.DateTime FechaDesde, System.DateTime FechaHasta, int EdoPeriodo, int SuscripcionID, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenListado(SesionSeguridad, FechaDesde, FechaHasta, EdoPeriodo, SuscripcionID, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Periodos.S_Periodos.EndObtenListado(System.IAsyncResult result) {
            return base.Channel.EndObtenListado(result);
        }
        
        private System.IAsyncResult OnBeginObtenListado(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            System.DateTime FechaDesde = ((System.DateTime)(inValues[1]));
            System.DateTime FechaHasta = ((System.DateTime)(inValues[2]));
            int EdoPeriodo = ((int)(inValues[3]));
            int SuscripcionID = ((int)(inValues[4]));
            return ((eClockBase.ES_Periodos.S_Periodos)(this)).BeginObtenListado(SesionSeguridad, FechaDesde, FechaHasta, EdoPeriodo, SuscripcionID, callback, asyncState);
        }
        
        private object[] OnEndObtenListado(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Periodos.S_Periodos)(this)).EndObtenListado(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenListadoCompleted(object state) {
            if ((this.ObtenListadoCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenListadoCompleted(this, new ObtenListadoCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenListadoAsync(string SesionSeguridad, System.DateTime FechaDesde, System.DateTime FechaHasta, int EdoPeriodo, int SuscripcionID) {
            this.ObtenListadoAsync(SesionSeguridad, FechaDesde, FechaHasta, EdoPeriodo, SuscripcionID, null);
        }
        
        public void ObtenListadoAsync(string SesionSeguridad, System.DateTime FechaDesde, System.DateTime FechaHasta, int EdoPeriodo, int SuscripcionID, object userState) {
            if ((this.onBeginObtenListadoDelegate == null)) {
                this.onBeginObtenListadoDelegate = new BeginOperationDelegate(this.OnBeginObtenListado);
            }
            if ((this.onEndObtenListadoDelegate == null)) {
                this.onEndObtenListadoDelegate = new EndOperationDelegate(this.OnEndObtenListado);
            }
            if ((this.onObtenListadoCompletedDelegate == null)) {
                this.onObtenListadoCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenListadoCompleted);
            }
            base.InvokeAsync(this.onBeginObtenListadoDelegate, new object[] {
                        SesionSeguridad,
                        FechaDesde,
                        FechaHasta,
                        EdoPeriodo,
                        SuscripcionID}, this.onEndObtenListadoDelegate, this.onObtenListadoCompletedDelegate, userState);
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
        
        protected override eClockBase.ES_Periodos.S_Periodos CreateChannel() {
            return new S_PeriodosClientChannel(this);
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Periodos)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.MaxReceivedMessageSize = int.MaxValue;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Periodos)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:50723/S_Periodos.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return S_PeriodosClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_S_Periodos);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return S_PeriodosClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_S_Periodos);
        }
        
        private class S_PeriodosClientChannel : ChannelBase<eClockBase.ES_Periodos.S_Periodos>, eClockBase.ES_Periodos.S_Periodos {
            
            public S_PeriodosClientChannel(System.ServiceModel.ClientBase<eClockBase.ES_Periodos.S_Periodos> client) : 
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
            
            public System.IAsyncResult BeginObtenListado(string SesionSeguridad, System.DateTime FechaDesde, System.DateTime FechaHasta, int EdoPeriodo, int SuscripcionID, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[5];
                _args[0] = SesionSeguridad;
                _args[1] = FechaDesde;
                _args[2] = FechaHasta;
                _args[3] = EdoPeriodo;
                _args[4] = SuscripcionID;
                System.IAsyncResult _result = base.BeginInvoke("ObtenListado", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenListado(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenListado", _args, result)));
                return _result;
            }
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_S_Periodos,
        }
    }
}
