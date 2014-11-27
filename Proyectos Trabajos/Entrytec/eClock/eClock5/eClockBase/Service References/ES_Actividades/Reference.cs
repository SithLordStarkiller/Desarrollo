﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eClockBase.ES_Actividades {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ES_Actividades.S_Actividades")]
    public interface S_Actividades {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Actividades/DoWork", ReplyAction="urn:S_Actividades/DoWorkResponse")]
        System.IAsyncResult BeginDoWork(System.AsyncCallback callback, object asyncState);
        
        void EndDoWork(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Actividades/ObtenImagen", ReplyAction="urn:S_Actividades/ObtenImagenResponse")]
        System.IAsyncResult BeginObtenImagen(string SesionSeguridad, int ActividadID, System.DateTime FechaHoraMinima, System.AsyncCallback callback, object asyncState);
        
        string EndObtenImagen(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:S_Actividades/Incribirse", ReplyAction="urn:S_Actividades/IncribirseResponse")]
        System.IAsyncResult BeginIncribirse(string SesionSeguridad, int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion, System.AsyncCallback callback, object asyncState);
        
        int EndIncribirse(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface S_ActividadesChannel : eClockBase.ES_Actividades.S_Actividades, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenImagenCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenImagenCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class IncribirseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public IncribirseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class S_ActividadesClient : System.ServiceModel.ClientBase<eClockBase.ES_Actividades.S_Actividades>, eClockBase.ES_Actividades.S_Actividades {
        
        private BeginOperationDelegate onBeginDoWorkDelegate;
        
        private EndOperationDelegate onEndDoWorkDelegate;
        
        private System.Threading.SendOrPostCallback onDoWorkCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenImagenDelegate;
        
        private EndOperationDelegate onEndObtenImagenDelegate;
        
        private System.Threading.SendOrPostCallback onObtenImagenCompletedDelegate;
        
        private BeginOperationDelegate onBeginIncribirseDelegate;
        
        private EndOperationDelegate onEndIncribirseDelegate;
        
        private System.Threading.SendOrPostCallback onIncribirseCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public S_ActividadesClient() : 
                base(S_ActividadesClient.GetDefaultBinding(), S_ActividadesClient.GetDefaultEndpointAddress()) {
        }
        
        public S_ActividadesClient(EndpointConfiguration endpointConfiguration) : 
                base(S_ActividadesClient.GetBindingForEndpoint(endpointConfiguration), S_ActividadesClient.GetEndpointAddress(endpointConfiguration)) {
        }
        
        public S_ActividadesClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(S_ActividadesClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
        }
        
        public S_ActividadesClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(S_ActividadesClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
        }
        
        public S_ActividadesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public event System.EventHandler<ObtenImagenCompletedEventArgs> ObtenImagenCompleted;
        
        public event System.EventHandler<IncribirseCompletedEventArgs> IncribirseCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Actividades.S_Actividades.BeginDoWork(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDoWork(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void eClockBase.ES_Actividades.S_Actividades.EndDoWork(System.IAsyncResult result) {
            base.Channel.EndDoWork(result);
        }
        
        private System.IAsyncResult OnBeginDoWork(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((eClockBase.ES_Actividades.S_Actividades)(this)).BeginDoWork(callback, asyncState);
        }
        
        private object[] OnEndDoWork(System.IAsyncResult result) {
            ((eClockBase.ES_Actividades.S_Actividades)(this)).EndDoWork(result);
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
        System.IAsyncResult eClockBase.ES_Actividades.S_Actividades.BeginObtenImagen(string SesionSeguridad, int ActividadID, System.DateTime FechaHoraMinima, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenImagen(SesionSeguridad, ActividadID, FechaHoraMinima, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        string eClockBase.ES_Actividades.S_Actividades.EndObtenImagen(System.IAsyncResult result) {
            return base.Channel.EndObtenImagen(result);
        }
        
        private System.IAsyncResult OnBeginObtenImagen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            int ActividadID = ((int)(inValues[1]));
            System.DateTime FechaHoraMinima = ((System.DateTime)(inValues[2]));
            return ((eClockBase.ES_Actividades.S_Actividades)(this)).BeginObtenImagen(SesionSeguridad, ActividadID, FechaHoraMinima, callback, asyncState);
        }
        
        private object[] OnEndObtenImagen(System.IAsyncResult result) {
            string retVal = ((eClockBase.ES_Actividades.S_Actividades)(this)).EndObtenImagen(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenImagenCompleted(object state) {
            if ((this.ObtenImagenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenImagenCompleted(this, new ObtenImagenCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenImagenAsync(string SesionSeguridad, int ActividadID, System.DateTime FechaHoraMinima) {
            this.ObtenImagenAsync(SesionSeguridad, ActividadID, FechaHoraMinima, null);
        }
        
        public void ObtenImagenAsync(string SesionSeguridad, int ActividadID, System.DateTime FechaHoraMinima, object userState) {
            if ((this.onBeginObtenImagenDelegate == null)) {
                this.onBeginObtenImagenDelegate = new BeginOperationDelegate(this.OnBeginObtenImagen);
            }
            if ((this.onEndObtenImagenDelegate == null)) {
                this.onEndObtenImagenDelegate = new EndOperationDelegate(this.OnEndObtenImagen);
            }
            if ((this.onObtenImagenCompletedDelegate == null)) {
                this.onObtenImagenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenImagenCompleted);
            }
            base.InvokeAsync(this.onBeginObtenImagenDelegate, new object[] {
                        SesionSeguridad,
                        ActividadID,
                        FechaHoraMinima}, this.onEndObtenImagenDelegate, this.onObtenImagenCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult eClockBase.ES_Actividades.S_Actividades.BeginIncribirse(string SesionSeguridad, int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginIncribirse(SesionSeguridad, ActividadID, PersonaID, TipoInscripcionID, Descripcion, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        int eClockBase.ES_Actividades.S_Actividades.EndIncribirse(System.IAsyncResult result) {
            return base.Channel.EndIncribirse(result);
        }
        
        private System.IAsyncResult OnBeginIncribirse(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string SesionSeguridad = ((string)(inValues[0]));
            int ActividadID = ((int)(inValues[1]));
            int PersonaID = ((int)(inValues[2]));
            int TipoInscripcionID = ((int)(inValues[3]));
            string Descripcion = ((string)(inValues[4]));
            return ((eClockBase.ES_Actividades.S_Actividades)(this)).BeginIncribirse(SesionSeguridad, ActividadID, PersonaID, TipoInscripcionID, Descripcion, callback, asyncState);
        }
        
        private object[] OnEndIncribirse(System.IAsyncResult result) {
            int retVal = ((eClockBase.ES_Actividades.S_Actividades)(this)).EndIncribirse(result);
            return new object[] {
                    retVal};
        }
        
        private void OnIncribirseCompleted(object state) {
            if ((this.IncribirseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.IncribirseCompleted(this, new IncribirseCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void IncribirseAsync(string SesionSeguridad, int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion) {
            this.IncribirseAsync(SesionSeguridad, ActividadID, PersonaID, TipoInscripcionID, Descripcion, null);
        }
        
        public void IncribirseAsync(string SesionSeguridad, int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion, object userState) {
            if ((this.onBeginIncribirseDelegate == null)) {
                this.onBeginIncribirseDelegate = new BeginOperationDelegate(this.OnBeginIncribirse);
            }
            if ((this.onEndIncribirseDelegate == null)) {
                this.onEndIncribirseDelegate = new EndOperationDelegate(this.OnEndIncribirse);
            }
            if ((this.onIncribirseCompletedDelegate == null)) {
                this.onIncribirseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnIncribirseCompleted);
            }
            base.InvokeAsync(this.onBeginIncribirseDelegate, new object[] {
                        SesionSeguridad,
                        ActividadID,
                        PersonaID,
                        TipoInscripcionID,
                        Descripcion}, this.onEndIncribirseDelegate, this.onIncribirseCompletedDelegate, userState);
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
        
        protected override eClockBase.ES_Actividades.S_Actividades CreateChannel() {
            return new S_ActividadesClientChannel(this);
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Actividades)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.MaxReceivedMessageSize = int.MaxValue;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_S_Actividades)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:50723/S_Actividades.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return S_ActividadesClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_S_Actividades);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return S_ActividadesClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_S_Actividades);
        }
        
        private class S_ActividadesClientChannel : ChannelBase<eClockBase.ES_Actividades.S_Actividades>, eClockBase.ES_Actividades.S_Actividades {
            
            public S_ActividadesClientChannel(System.ServiceModel.ClientBase<eClockBase.ES_Actividades.S_Actividades> client) : 
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
            
            public System.IAsyncResult BeginObtenImagen(string SesionSeguridad, int ActividadID, System.DateTime FechaHoraMinima, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[3];
                _args[0] = SesionSeguridad;
                _args[1] = ActividadID;
                _args[2] = FechaHoraMinima;
                System.IAsyncResult _result = base.BeginInvoke("ObtenImagen", _args, callback, asyncState);
                return _result;
            }
            
            public string EndObtenImagen(System.IAsyncResult result) {
                object[] _args = new object[0];
                string _result = ((string)(base.EndInvoke("ObtenImagen", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginIncribirse(string SesionSeguridad, int ActividadID, int PersonaID, int TipoInscripcionID, string Descripcion, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[5];
                _args[0] = SesionSeguridad;
                _args[1] = ActividadID;
                _args[2] = PersonaID;
                _args[3] = TipoInscripcionID;
                _args[4] = Descripcion;
                System.IAsyncResult _result = base.BeginInvoke("Incribirse", _args, callback, asyncState);
                return _result;
            }
            
            public int EndIncribirse(System.IAsyncResult result) {
                object[] _args = new object[0];
                int _result = ((int)(base.EndInvoke("Incribirse", _args, result)));
                return _result;
            }
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_S_Actividades,
        }
    }
}
