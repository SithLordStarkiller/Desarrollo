﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eClockBase.ES_Reportes {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ES_Reportes.S_Reportes")]
    public interface S_Reportes {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Reportes/DoWork", ReplyAction="urn:S_Reportes/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Reportes/ObtenListado", ReplyAction="urn:S_Reportes/ObtenListadoResponse")]
        System.IAsyncResult BeginObtenListado(string SesionSeguridad, string Hash, System.AsyncCallback callback, object asyncState);
        
        string EndObtenListado(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Reportes/ObtenReporte", ReplyAction="urn:S_Reportes/ObtenReporteResponse")]
        System.IAsyncResult BeginObtenReporte(string SesionSeguridad, int ReporteID, string Parametros, int FormatoRepID, string Lang, System.AsyncCallback callback, object asyncState);
        
        string EndObtenReporte(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Reportes/ObtenReporteMail", ReplyAction="urn:S_Reportes/ObtenReporteMailResponse")]
        System.IAsyncResult BeginObtenReporteMail(string SesionSeguridad, string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID, string Lang, System.AsyncCallback callback, object asyncState);
        
        string EndObtenReporteMail(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface S_ReportesChannel : eClockBase.ES_Reportes.S_Reportes, System.ServiceModel.IClientChannel {
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
    public partial class ObtenReporteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenReporteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class ObtenReporteMailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenReporteMailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class S_ReportesClient : System.ServiceModel.ClientBase<eClockBase.ES_Reportes.S_Reportes>, eClockBase.ES_Reportes.S_Reportes {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenListadoDelegate;
        
        private EndOperationDelegate onEndObtenListadoDelegate;
        
        private System.Threading.SendOrPostCallback onObtenListadoCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenReporteDelegate;
        
        private EndOperationDelegate onEndObtenReporteDelegate;
        
        private System.Threading.SendOrPostCallback onObtenReporteCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenReporteMailDelegate;
        
        private EndOperationDelegate onEndObtenReporteMailDelegate;
        
        private System.Threading.SendOrPostCallback onObtenReporteMailCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public S_ReportesClient() : 
                base(S_ReportesClient.GetDefaultBinding(), S_ReportesClient.GetDefaultEndpointAddress()) {
        }
        
        public S_ReportesClient(EndpointConfiguration endpointConfiguration) : 
                base(S_ReportesClient.GetBindingForEndpoint(endpointConfiguration), S_ReportesClient.GetEndpointAddress(endpointConfiguration)) {
        }
        
        public S_ReportesClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(S_ReportesClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
        }
        
        public S_ReportesClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(S_ReportesClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
        }
        
        public S_ReportesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public event System.EventHandler<ObtenReporteCompletedEventArgs> ObtenReporteCompleted;
        
        public event System.EventHandler<ObtenReporteMailCompletedEventArgs> ObtenReporteMailCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Reportes.S_Reportes.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void eClockBase.ES_Reportes.S_Reportes.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((eClockBase.ES_Reportes.S_Reportes)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((eClockBase.ES_Reportes.S_Reportes)(this)).EndDoWork(result);
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
        System.IAsyncResult eClockBase.ES_Reportes.S_Reportes.BeginObtenListado(string SesionSeguridad, string Hash, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenListado(SesionSeguridad, Hash, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Reportes.S_Reportes.EndObtenListado(System.IAsyncResult result) {
            return base.Channel.EndObtenListado(result);
        }
        
        private System.IAsyncResult OnBeginObtenListado(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            string Hash = ((string)(inValues[1]));
            return ((eClockBase.ES_Reportes.S_Reportes)(this)).BeginObtenListado(SesionSeguridad, Hash, callback, asyncState);
        }
        
        private object[] OnEndObtenListado(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Reportes.S_Reportes)(this)).EndObtenListado(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenListadoCompleted(object state) {
            if ((this.ObtenListadoCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenListadoCompleted(this, new ObtenListadoCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenListadoAsync(string SesionSeguridad, string Hash) {
            this.ObtenListadoAsync(SesionSeguridad, Hash, null);
        }
        
        public void ObtenListadoAsync(string SesionSeguridad, string Hash, object userState) {
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
                        Hash}, this.onEndObtenListadoDelegate, this.onObtenListadoCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Reportes.S_Reportes.BeginObtenReporte(string SesionSeguridad, int ReporteID, string Parametros, int FormatoRepID, string Lang, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenReporte(SesionSeguridad, ReporteID, Parametros, FormatoRepID, Lang, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Reportes.S_Reportes.EndObtenReporte(System.IAsyncResult result) {
            return base.Channel.EndObtenReporte(result);
        }
        
        private System.IAsyncResult OnBeginObtenReporte(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            int ReporteID = ((int)(inValues[1]));
            string Parametros = ((string)(inValues[2]));
            int FormatoRepID = ((int)(inValues[3]));
            string Lang = ((string)(inValues[4]));
            return ((eClockBase.ES_Reportes.S_Reportes)(this)).BeginObtenReporte(SesionSeguridad, ReporteID, Parametros, FormatoRepID, Lang, callback, asyncState);
        }
        
        private object[] OnEndObtenReporte(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Reportes.S_Reportes)(this)).EndObtenReporte(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenReporteCompleted(object state) {
            if ((this.ObtenReporteCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenReporteCompleted(this, new ObtenReporteCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenReporteAsync(string SesionSeguridad, int ReporteID, string Parametros, int FormatoRepID, string Lang) {
            this.ObtenReporteAsync(SesionSeguridad, ReporteID, Parametros, FormatoRepID, Lang, null);
        }
        
        public void ObtenReporteAsync(string SesionSeguridad, int ReporteID, string Parametros, int FormatoRepID, string Lang, object userState) {
            if ((this.onBeginObtenReporteDelegate == null)) {
                this.onBeginObtenReporteDelegate = new BeginOperationDelegate(this.OnBeginObtenReporte);
            }
            if ((this.onEndObtenReporteDelegate == null)) {
                this.onEndObtenReporteDelegate = new EndOperationDelegate(this.OnEndObtenReporte);
            }
            if ((this.onObtenReporteCompletedDelegate == null)) {
                this.onObtenReporteCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenReporteCompleted);
            }
            base.InvokeAsync(this.onBeginObtenReporteDelegate, new object[] {
                        SesionSeguridad,
                        ReporteID,
                        Parametros,
                        FormatoRepID,
                        Lang}, this.onEndObtenReporteDelegate, this.onObtenReporteCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Reportes.S_Reportes.BeginObtenReporteMail(string SesionSeguridad, string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID, string Lang, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenReporteMail(SesionSeguridad, eMails, Titulo, Cuerpo, ReporteID, Parametros, FormatoRepID, Lang, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Reportes.S_Reportes.EndObtenReporteMail(System.IAsyncResult result) {
            return base.Channel.EndObtenReporteMail(result);
        }
        
        private System.IAsyncResult OnBeginObtenReporteMail(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            string eMails = ((string)(inValues[1]));
            string Titulo = ((string)(inValues[2]));
            string Cuerpo = ((string)(inValues[3]));
            int ReporteID = ((int)(inValues[4]));
            string Parametros = ((string)(inValues[5]));
            int FormatoRepID = ((int)(inValues[6]));
            string Lang = ((string)(inValues[7]));
            return ((eClockBase.ES_Reportes.S_Reportes)(this)).BeginObtenReporteMail(SesionSeguridad, eMails, Titulo, Cuerpo, ReporteID, Parametros, FormatoRepID, Lang, callback, asyncState);
        }
        
        private object[] OnEndObtenReporteMail(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Reportes.S_Reportes)(this)).EndObtenReporteMail(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenReporteMailCompleted(object state) {
            if ((this.ObtenReporteMailCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenReporteMailCompleted(this, new ObtenReporteMailCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenReporteMailAsync(string SesionSeguridad, string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID, string Lang) {
            this.ObtenReporteMailAsync(SesionSeguridad, eMails, Titulo, Cuerpo, ReporteID, Parametros, FormatoRepID, Lang, null);
        }
        
        public void ObtenReporteMailAsync(string SesionSeguridad, string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID, string Lang, object userState) {
            if ((this.onBeginObtenReporteMailDelegate == null)) {
                this.onBeginObtenReporteMailDelegate = new BeginOperationDelegate(this.OnBeginObtenReporteMail);
            }
            if ((this.onEndObtenReporteMailDelegate == null)) {
                this.onEndObtenReporteMailDelegate = new EndOperationDelegate(this.OnEndObtenReporteMail);
            }
            if ((this.onObtenReporteMailCompletedDelegate == null)) {
                this.onObtenReporteMailCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenReporteMailCompleted);
            }
            base.InvokeAsync(this.onBeginObtenReporteMailDelegate, new object[] {
                        SesionSeguridad,
                        eMails,
                        Titulo,
                        Cuerpo,
                        ReporteID,
                        Parametros,
                        FormatoRepID,
                        Lang}, this.onEndObtenReporteMailDelegate, this.onObtenReporteMailCompletedDelegate, userState);
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
        
        protected override eClockBase.ES_Reportes.S_Reportes CreateChannel() {
            return new S_ReportesClientChannel(this);
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Reportes)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.MaxReceivedMessageSize = int.MaxValue;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Reportes)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:50723/S_Reportes.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return S_ReportesClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_S_Reportes);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return S_ReportesClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_S_Reportes);
        }
        
        private class S_ReportesClientChannel : ChannelBase<eClockBase.ES_Reportes.S_Reportes>, eClockBase.ES_Reportes.S_Reportes {
            
            public S_ReportesClientChannel(System.ServiceModel.ClientBase<eClockBase.ES_Reportes.S_Reportes> client) : 
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
            
            public System.IAsyncResult BeginObtenListado(string SesionSeguridad, string Hash, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = SesionSeguridad;
                _args[1] = Hash;
                System.IAsyncResult _result = base.BeginInvoke("ObtenListado", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenListado(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenListado", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginObtenReporte(string SesionSeguridad, int ReporteID, string Parametros, int FormatoRepID, string Lang, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[5];
                _args[0] = SesionSeguridad;
                _args[1] = ReporteID;
                _args[2] = Parametros;
                _args[3] = FormatoRepID;
                _args[4] = Lang;
                System.IAsyncResult _result = base.BeginInvoke("ObtenReporte", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenReporte(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenReporte", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginObtenReporteMail(string SesionSeguridad, string eMails, string Titulo, string Cuerpo, int ReporteID, string Parametros, int FormatoRepID, string Lang, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[8];
                _args[0] = SesionSeguridad;
                _args[1] = eMails;
                _args[2] = Titulo;
                _args[3] = Cuerpo;
                _args[4] = ReporteID;
                _args[5] = Parametros;
                _args[6] = FormatoRepID;
                _args[7] = Lang;
                System.IAsyncResult _result = base.BeginInvoke("ObtenReporteMail", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenReporteMail(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenReporteMail", _args, result)));
                return _result;
            }
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_S_Reportes,
        }
    }
}
