﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18051
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eClockBase.ES_Localizaciones {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ES_Localizaciones.S_Localizaciones")]
    public interface S_Localizaciones {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Localizaciones/DoWork", ReplyAction="urn:S_Localizaciones/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Localizaciones/ObtenEtiqueta", ReplyAction="urn:S_Localizaciones/ObtenEtiquetaResponse")]
        System.IAsyncResult BeginObtenEtiqueta(string LocalizacionIdioma, string LocalizacionLlave, System.AsyncCallback callback, object asyncState);
        
        string EndObtenEtiqueta(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Localizaciones/ObtenEtiquetaDebug", ReplyAction="urn:S_Localizaciones/ObtenEtiquetaDebugResponse")]
        System.IAsyncResult BeginObtenEtiquetaDebug(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado, System.AsyncCallback callback, object asyncState);
        
        string EndObtenEtiquetaDebug(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Localizaciones/ObtenAyuda", ReplyAction="urn:S_Localizaciones/ObtenAyudaResponse")]
        System.IAsyncResult BeginObtenAyuda(string LocalizacionIdioma, string LocalizacionLlave, System.AsyncCallback callback, object asyncState);
        
        string EndObtenAyuda(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Localizaciones/ObtenEtiquetasAyuda", ReplyAction="urn:S_Localizaciones/ObtenEtiquetasAyudaResponse")]
        System.IAsyncResult BeginObtenEtiquetasAyuda(string SesionSeguridad, string LocalizacionIdioma, string Hash, System.AsyncCallback callback, object asyncState);
        
        string EndObtenEtiquetasAyuda(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface S_LocalizacionesChannel : eClockBase.ES_Localizaciones.S_Localizaciones, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenEtiquetaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenEtiquetaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class ObtenEtiquetaDebugCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenEtiquetaDebugCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class ObtenAyudaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenAyudaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class ObtenEtiquetasAyudaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenEtiquetasAyudaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class S_LocalizacionesClient : System.ServiceModel.ClientBase<eClockBase.ES_Localizaciones.S_Localizaciones>, eClockBase.ES_Localizaciones.S_Localizaciones {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenEtiquetaDelegate;
        
        private EndOperationDelegate onEndObtenEtiquetaDelegate;
        
        private System.Threading.SendOrPostCallback onObtenEtiquetaCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenEtiquetaDebugDelegate;
        
        private EndOperationDelegate onEndObtenEtiquetaDebugDelegate;
        
        private System.Threading.SendOrPostCallback onObtenEtiquetaDebugCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenAyudaDelegate;
        
        private EndOperationDelegate onEndObtenAyudaDelegate;
        
        private System.Threading.SendOrPostCallback onObtenAyudaCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenEtiquetasAyudaDelegate;
        
        private EndOperationDelegate onEndObtenEtiquetasAyudaDelegate;
        
        private System.Threading.SendOrPostCallback onObtenEtiquetasAyudaCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public S_LocalizacionesClient() : 
                base(S_LocalizacionesClient.GetDefaultBinding(), S_LocalizacionesClient.GetDefaultEndpointAddress()) {
        }
        
        public S_LocalizacionesClient(EndpointConfiguration endpointConfiguration) : 
                base(S_LocalizacionesClient.GetBindingForEndpoint(endpointConfiguration), S_LocalizacionesClient.GetEndpointAddress(endpointConfiguration)) {
        }
        
        public S_LocalizacionesClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(S_LocalizacionesClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
        }
        
        public S_LocalizacionesClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(S_LocalizacionesClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
        }
        
        public S_LocalizacionesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public event System.EventHandler<ObtenEtiquetaCompletedEventArgs> ObtenEtiquetaCompleted;
        
        public event System.EventHandler<ObtenEtiquetaDebugCompletedEventArgs> ObtenEtiquetaDebugCompleted;
        
        public event System.EventHandler<ObtenAyudaCompletedEventArgs> ObtenAyudaCompleted;
        
        public event System.EventHandler<ObtenEtiquetasAyudaCompletedEventArgs> ObtenEtiquetasAyudaCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Localizaciones.S_Localizaciones.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void eClockBase.ES_Localizaciones.S_Localizaciones.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).EndDoWork(result);
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
        System.IAsyncResult eClockBase.ES_Localizaciones.S_Localizaciones.BeginObtenEtiqueta(string LocalizacionIdioma, string LocalizacionLlave, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenEtiqueta(LocalizacionIdioma, LocalizacionLlave, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Localizaciones.S_Localizaciones.EndObtenEtiqueta(System.IAsyncResult result) {
            return base.Channel.EndObtenEtiqueta(result);
        }
        
        private System.IAsyncResult OnBeginObtenEtiqueta(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string LocalizacionIdioma = ((string)(inValues[0]));
            string LocalizacionLlave = ((string)(inValues[1]));
            return ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).BeginObtenEtiqueta(LocalizacionIdioma, LocalizacionLlave, callback, asyncState);
        }
        
        private object[] OnEndObtenEtiqueta(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).EndObtenEtiqueta(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenEtiquetaCompleted(object state) {
            if ((this.ObtenEtiquetaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenEtiquetaCompleted(this, new ObtenEtiquetaCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenEtiquetaAsync(string LocalizacionIdioma, string LocalizacionLlave) {
            this.ObtenEtiquetaAsync(LocalizacionIdioma, LocalizacionLlave, null);
        }
        
        public void ObtenEtiquetaAsync(string LocalizacionIdioma, string LocalizacionLlave, object userState) {
            if ((this.onBeginObtenEtiquetaDelegate == null)) {
                this.onBeginObtenEtiquetaDelegate = new BeginOperationDelegate(this.OnBeginObtenEtiqueta);
            }
            if ((this.onEndObtenEtiquetaDelegate == null)) {
                this.onEndObtenEtiquetaDelegate = new EndOperationDelegate(this.OnEndObtenEtiqueta);
            }
            if ((this.onObtenEtiquetaCompletedDelegate == null)) {
                this.onObtenEtiquetaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenEtiquetaCompleted);
            }
            base.InvokeAsync(this.onBeginObtenEtiquetaDelegate, new object[] {
                        LocalizacionIdioma,
                        LocalizacionLlave}, this.onEndObtenEtiquetaDelegate, this.onObtenEtiquetaCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Localizaciones.S_Localizaciones.BeginObtenEtiquetaDebug(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenEtiquetaDebug(LocalizacionIdioma, LocalizacionLlave, TextoPredeterminado, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Localizaciones.S_Localizaciones.EndObtenEtiquetaDebug(System.IAsyncResult result) {
            return base.Channel.EndObtenEtiquetaDebug(result);
        }
        
        private System.IAsyncResult OnBeginObtenEtiquetaDebug(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string LocalizacionIdioma = ((string)(inValues[0]));
            string LocalizacionLlave = ((string)(inValues[1]));
            string TextoPredeterminado = ((string)(inValues[2]));
            return ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).BeginObtenEtiquetaDebug(LocalizacionIdioma, LocalizacionLlave, TextoPredeterminado, callback, asyncState);
        }
        
        private object[] OnEndObtenEtiquetaDebug(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).EndObtenEtiquetaDebug(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenEtiquetaDebugCompleted(object state) {
            if ((this.ObtenEtiquetaDebugCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenEtiquetaDebugCompleted(this, new ObtenEtiquetaDebugCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenEtiquetaDebugAsync(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado) {
            this.ObtenEtiquetaDebugAsync(LocalizacionIdioma, LocalizacionLlave, TextoPredeterminado, null);
        }
        
        public void ObtenEtiquetaDebugAsync(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado, object userState) {
            if ((this.onBeginObtenEtiquetaDebugDelegate == null)) {
                this.onBeginObtenEtiquetaDebugDelegate = new BeginOperationDelegate(this.OnBeginObtenEtiquetaDebug);
            }
            if ((this.onEndObtenEtiquetaDebugDelegate == null)) {
                this.onEndObtenEtiquetaDebugDelegate = new EndOperationDelegate(this.OnEndObtenEtiquetaDebug);
            }
            if ((this.onObtenEtiquetaDebugCompletedDelegate == null)) {
                this.onObtenEtiquetaDebugCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenEtiquetaDebugCompleted);
            }
            base.InvokeAsync(this.onBeginObtenEtiquetaDebugDelegate, new object[] {
                        LocalizacionIdioma,
                        LocalizacionLlave,
                        TextoPredeterminado}, this.onEndObtenEtiquetaDebugDelegate, this.onObtenEtiquetaDebugCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Localizaciones.S_Localizaciones.BeginObtenAyuda(string LocalizacionIdioma, string LocalizacionLlave, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenAyuda(LocalizacionIdioma, LocalizacionLlave, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Localizaciones.S_Localizaciones.EndObtenAyuda(System.IAsyncResult result) {
            return base.Channel.EndObtenAyuda(result);
        }
        
        private System.IAsyncResult OnBeginObtenAyuda(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string LocalizacionIdioma = ((string)(inValues[0]));
            string LocalizacionLlave = ((string)(inValues[1]));
            return ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).BeginObtenAyuda(LocalizacionIdioma, LocalizacionLlave, callback, asyncState);
        }
        
        private object[] OnEndObtenAyuda(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).EndObtenAyuda(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenAyudaCompleted(object state) {
            if ((this.ObtenAyudaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenAyudaCompleted(this, new ObtenAyudaCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenAyudaAsync(string LocalizacionIdioma, string LocalizacionLlave) {
            this.ObtenAyudaAsync(LocalizacionIdioma, LocalizacionLlave, null);
        }
        
        public void ObtenAyudaAsync(string LocalizacionIdioma, string LocalizacionLlave, object userState) {
            if ((this.onBeginObtenAyudaDelegate == null)) {
                this.onBeginObtenAyudaDelegate = new BeginOperationDelegate(this.OnBeginObtenAyuda);
            }
            if ((this.onEndObtenAyudaDelegate == null)) {
                this.onEndObtenAyudaDelegate = new EndOperationDelegate(this.OnEndObtenAyuda);
            }
            if ((this.onObtenAyudaCompletedDelegate == null)) {
                this.onObtenAyudaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenAyudaCompleted);
            }
            base.InvokeAsync(this.onBeginObtenAyudaDelegate, new object[] {
                        LocalizacionIdioma,
                        LocalizacionLlave}, this.onEndObtenAyudaDelegate, this.onObtenAyudaCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Localizaciones.S_Localizaciones.BeginObtenEtiquetasAyuda(string SesionSeguridad, string LocalizacionIdioma, string Hash, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenEtiquetasAyuda(SesionSeguridad, LocalizacionIdioma, Hash, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Localizaciones.S_Localizaciones.EndObtenEtiquetasAyuda(System.IAsyncResult result) {
            return base.Channel.EndObtenEtiquetasAyuda(result);
        }
        
        private System.IAsyncResult OnBeginObtenEtiquetasAyuda(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            string LocalizacionIdioma = ((string)(inValues[1]));
            string Hash = ((string)(inValues[2]));
            return ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).BeginObtenEtiquetasAyuda(SesionSeguridad, LocalizacionIdioma, Hash, callback, asyncState);
        }
        
        private object[] OnEndObtenEtiquetasAyuda(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Localizaciones.S_Localizaciones)(this)).EndObtenEtiquetasAyuda(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenEtiquetasAyudaCompleted(object state) {
            if ((this.ObtenEtiquetasAyudaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenEtiquetasAyudaCompleted(this, new ObtenEtiquetasAyudaCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenEtiquetasAyudaAsync(string SesionSeguridad, string LocalizacionIdioma, string Hash) {
            this.ObtenEtiquetasAyudaAsync(SesionSeguridad, LocalizacionIdioma, Hash, null);
        }
        
        public void ObtenEtiquetasAyudaAsync(string SesionSeguridad, string LocalizacionIdioma, string Hash, object userState) {
            if ((this.onBeginObtenEtiquetasAyudaDelegate == null)) {
                this.onBeginObtenEtiquetasAyudaDelegate = new BeginOperationDelegate(this.OnBeginObtenEtiquetasAyuda);
            }
            if ((this.onEndObtenEtiquetasAyudaDelegate == null)) {
                this.onEndObtenEtiquetasAyudaDelegate = new EndOperationDelegate(this.OnEndObtenEtiquetasAyuda);
            }
            if ((this.onObtenEtiquetasAyudaCompletedDelegate == null)) {
                this.onObtenEtiquetasAyudaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenEtiquetasAyudaCompleted);
            }
            base.InvokeAsync(this.onBeginObtenEtiquetasAyudaDelegate, new object[] {
                        SesionSeguridad,
                        LocalizacionIdioma,
                        Hash}, this.onEndObtenEtiquetasAyudaDelegate, this.onObtenEtiquetasAyudaCompletedDelegate, userState);
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
        
        protected override eClockBase.ES_Localizaciones.S_Localizaciones CreateChannel() {
            return new S_LocalizacionesClientChannel(this);
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Localizaciones)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.MaxReceivedMessageSize = int.MaxValue;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Localizaciones)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:50723/S_Localizaciones.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return S_LocalizacionesClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_S_Localizaciones);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return S_LocalizacionesClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_S_Localizaciones);
        }
        
        private class S_LocalizacionesClientChannel : ChannelBase<eClockBase.ES_Localizaciones.S_Localizaciones>, eClockBase.ES_Localizaciones.S_Localizaciones {
            
            public S_LocalizacionesClientChannel(System.ServiceModel.ClientBase<eClockBase.ES_Localizaciones.S_Localizaciones> client) : 
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
            
            public System.IAsyncResult BeginObtenEtiqueta(string LocalizacionIdioma, string LocalizacionLlave, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = LocalizacionIdioma;
                _args[1] = LocalizacionLlave;
                System.IAsyncResult _result = base.BeginInvoke("ObtenEtiqueta", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenEtiqueta(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenEtiqueta", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginObtenEtiquetaDebug(string LocalizacionIdioma, string LocalizacionLlave, string TextoPredeterminado, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[3];
                _args[0] = LocalizacionIdioma;
                _args[1] = LocalizacionLlave;
                _args[2] = TextoPredeterminado;
                System.IAsyncResult _result = base.BeginInvoke("ObtenEtiquetaDebug", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenEtiquetaDebug(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenEtiquetaDebug", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginObtenAyuda(string LocalizacionIdioma, string LocalizacionLlave, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = LocalizacionIdioma;
                _args[1] = LocalizacionLlave;
                System.IAsyncResult _result = base.BeginInvoke("ObtenAyuda", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenAyuda(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenAyuda", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginObtenEtiquetasAyuda(string SesionSeguridad, string LocalizacionIdioma, string Hash, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[3];
                _args[0] = SesionSeguridad;
                _args[1] = LocalizacionIdioma;
                _args[2] = Hash;
                System.IAsyncResult _result = base.BeginInvoke("ObtenEtiquetasAyuda", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenEtiquetasAyuda(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenEtiquetasAyuda", _args, result)));
                return _result;
            }
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_S_Localizaciones,
        }
    }
}