﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Suncorp.ServiceController.UsuariosWcf {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UsuariosWcf.IUsuariosWcf")]
    public interface IUsuariosWcf {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuariosWcf/ObtenerTablaUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/ObtenerTablaUsuarioResponse")]
        System.Collections.Generic.List<Suncorp.Models.UsUsuarios> ObtenerTablaUsuario();
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IUsuariosWcf/ObtenerTablaUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/ObtenerTablaUsuarioResponse")]
        System.IAsyncResult BeginObtenerTablaUsuario(System.AsyncCallback callback, object asyncState);
        
        System.Collections.Generic.List<Suncorp.Models.UsUsuarios> EndObtenerTablaUsuario(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuariosWcf/InsertarUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/InsertarUsuarioResponse")]
        Suncorp.Models.UsUsuarios InsertarUsuario(Suncorp.Models.UsUsuarios model);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IUsuariosWcf/InsertarUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/InsertarUsuarioResponse")]
        System.IAsyncResult BeginInsertarUsuario(Suncorp.Models.UsUsuarios model, System.AsyncCallback callback, object asyncState);
        
        Suncorp.Models.UsUsuarios EndInsertarUsuario(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuariosWcf/ActualizarUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/ActualizarUsuarioResponse")]
        bool ActualizarUsuario(Suncorp.Models.UsUsuarios model);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IUsuariosWcf/ActualizarUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/ActualizarUsuarioResponse")]
        System.IAsyncResult BeginActualizarUsuario(Suncorp.Models.UsUsuarios model, System.AsyncCallback callback, object asyncState);
        
        bool EndActualizarUsuario(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuariosWcf/EliminarUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/EliminarUsuarioResponse")]
        bool EliminarUsuario(Suncorp.Models.UsUsuarios model);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IUsuariosWcf/EliminarUsuario", ReplyAction="http://tempuri.org/IUsuariosWcf/EliminarUsuarioResponse")]
        System.IAsyncResult BeginEliminarUsuario(Suncorp.Models.UsUsuarios model, System.AsyncCallback callback, object asyncState);
        
        bool EndEliminarUsuario(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuariosWcf/ObtenerUsuarioLogin", ReplyAction="http://tempuri.org/IUsuariosWcf/ObtenerUsuarioLoginResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<Suncorp.Models.UsUsuarios>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Suncorp.Models.UsUsuarios))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Suncorp.Models.EstatusProceso))]
        Suncorp.Models.WcfResponse ObtenerUsuarioLogin(string usuario, string contrasena);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IUsuariosWcf/ObtenerUsuarioLogin", ReplyAction="http://tempuri.org/IUsuariosWcf/ObtenerUsuarioLoginResponse")]
        System.IAsyncResult BeginObtenerUsuarioLogin(string usuario, string contrasena, System.AsyncCallback callback, object asyncState);
        
        Suncorp.Models.WcfResponse EndObtenerUsuarioLogin(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsuariosWcfChannel : Suncorp.ServiceController.UsuariosWcf.IUsuariosWcf, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenerTablaUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenerTablaUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.Generic.List<Suncorp.Models.UsUsuarios> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.Generic.List<Suncorp.Models.UsUsuarios>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InsertarUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public InsertarUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public Suncorp.Models.UsUsuarios Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((Suncorp.Models.UsUsuarios)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ActualizarUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ActualizarUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EliminarUsuarioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public EliminarUsuarioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObtenerUsuarioLoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenerUsuarioLoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public Suncorp.Models.WcfResponse Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((Suncorp.Models.WcfResponse)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsuariosWcfClient : System.ServiceModel.ClientBase<Suncorp.ServiceController.UsuariosWcf.IUsuariosWcf>, Suncorp.ServiceController.UsuariosWcf.IUsuariosWcf {
        
        private BeginOperationDelegate onBeginObtenerTablaUsuarioDelegate;
        
        private EndOperationDelegate onEndObtenerTablaUsuarioDelegate;
        
        private System.Threading.SendOrPostCallback onObtenerTablaUsuarioCompletedDelegate;
        
        private BeginOperationDelegate onBeginInsertarUsuarioDelegate;
        
        private EndOperationDelegate onEndInsertarUsuarioDelegate;
        
        private System.Threading.SendOrPostCallback onInsertarUsuarioCompletedDelegate;
        
        private BeginOperationDelegate onBeginActualizarUsuarioDelegate;
        
        private EndOperationDelegate onEndActualizarUsuarioDelegate;
        
        private System.Threading.SendOrPostCallback onActualizarUsuarioCompletedDelegate;
        
        private BeginOperationDelegate onBeginEliminarUsuarioDelegate;
        
        private EndOperationDelegate onEndEliminarUsuarioDelegate;
        
        private System.Threading.SendOrPostCallback onEliminarUsuarioCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenerUsuarioLoginDelegate;
        
        private EndOperationDelegate onEndObtenerUsuarioLoginDelegate;
        
        private System.Threading.SendOrPostCallback onObtenerUsuarioLoginCompletedDelegate;
        
        public UsuariosWcfClient() {
        }
        
        public UsuariosWcfClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsuariosWcfClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuariosWcfClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuariosWcfClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<ObtenerTablaUsuarioCompletedEventArgs> ObtenerTablaUsuarioCompleted;
        
        public event System.EventHandler<InsertarUsuarioCompletedEventArgs> InsertarUsuarioCompleted;
        
        public event System.EventHandler<ActualizarUsuarioCompletedEventArgs> ActualizarUsuarioCompleted;
        
        public event System.EventHandler<EliminarUsuarioCompletedEventArgs> EliminarUsuarioCompleted;
        
        public event System.EventHandler<ObtenerUsuarioLoginCompletedEventArgs> ObtenerUsuarioLoginCompleted;
        
        public System.Collections.Generic.List<Suncorp.Models.UsUsuarios> ObtenerTablaUsuario() {
            return base.Channel.ObtenerTablaUsuario();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginObtenerTablaUsuario(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenerTablaUsuario(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.Collections.Generic.List<Suncorp.Models.UsUsuarios> EndObtenerTablaUsuario(System.IAsyncResult result) {
            return base.Channel.EndObtenerTablaUsuario(result);
        }
        
        private System.IAsyncResult OnBeginObtenerTablaUsuario(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return this.BeginObtenerTablaUsuario(callback, asyncState);
        }
        
        private object[] OnEndObtenerTablaUsuario(System.IAsyncResult result) {
            System.Collections.Generic.List<Suncorp.Models.UsUsuarios> retVal = this.EndObtenerTablaUsuario(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenerTablaUsuarioCompleted(object state) {
            if ((this.ObtenerTablaUsuarioCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenerTablaUsuarioCompleted(this, new ObtenerTablaUsuarioCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenerTablaUsuarioAsync() {
            this.ObtenerTablaUsuarioAsync(null);
        }
        
        public void ObtenerTablaUsuarioAsync(object userState) {
            if ((this.onBeginObtenerTablaUsuarioDelegate == null)) {
                this.onBeginObtenerTablaUsuarioDelegate = new BeginOperationDelegate(this.OnBeginObtenerTablaUsuario);
            }
            if ((this.onEndObtenerTablaUsuarioDelegate == null)) {
                this.onEndObtenerTablaUsuarioDelegate = new EndOperationDelegate(this.OnEndObtenerTablaUsuario);
            }
            if ((this.onObtenerTablaUsuarioCompletedDelegate == null)) {
                this.onObtenerTablaUsuarioCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenerTablaUsuarioCompleted);
            }
            base.InvokeAsync(this.onBeginObtenerTablaUsuarioDelegate, null, this.onEndObtenerTablaUsuarioDelegate, this.onObtenerTablaUsuarioCompletedDelegate, userState);
        }
        
        public Suncorp.Models.UsUsuarios InsertarUsuario(Suncorp.Models.UsUsuarios model) {
            return base.Channel.InsertarUsuario(model);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginInsertarUsuario(Suncorp.Models.UsUsuarios model, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginInsertarUsuario(model, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public Suncorp.Models.UsUsuarios EndInsertarUsuario(System.IAsyncResult result) {
            return base.Channel.EndInsertarUsuario(result);
        }
        
        private System.IAsyncResult OnBeginInsertarUsuario(object[] inValues, System.AsyncCallback callback, object asyncState) {
            Suncorp.Models.UsUsuarios model = ((Suncorp.Models.UsUsuarios)(inValues[0]));
            return this.BeginInsertarUsuario(model, callback, asyncState);
        }
        
        private object[] OnEndInsertarUsuario(System.IAsyncResult result) {
            Suncorp.Models.UsUsuarios retVal = this.EndInsertarUsuario(result);
            return new object[] {
                    retVal};
        }
        
        private void OnInsertarUsuarioCompleted(object state) {
            if ((this.InsertarUsuarioCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.InsertarUsuarioCompleted(this, new InsertarUsuarioCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void InsertarUsuarioAsync(Suncorp.Models.UsUsuarios model) {
            this.InsertarUsuarioAsync(model, null);
        }
        
        public void InsertarUsuarioAsync(Suncorp.Models.UsUsuarios model, object userState) {
            if ((this.onBeginInsertarUsuarioDelegate == null)) {
                this.onBeginInsertarUsuarioDelegate = new BeginOperationDelegate(this.OnBeginInsertarUsuario);
            }
            if ((this.onEndInsertarUsuarioDelegate == null)) {
                this.onEndInsertarUsuarioDelegate = new EndOperationDelegate(this.OnEndInsertarUsuario);
            }
            if ((this.onInsertarUsuarioCompletedDelegate == null)) {
                this.onInsertarUsuarioCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnInsertarUsuarioCompleted);
            }
            base.InvokeAsync(this.onBeginInsertarUsuarioDelegate, new object[] {
                        model}, this.onEndInsertarUsuarioDelegate, this.onInsertarUsuarioCompletedDelegate, userState);
        }
        
        public bool ActualizarUsuario(Suncorp.Models.UsUsuarios model) {
            return base.Channel.ActualizarUsuario(model);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginActualizarUsuario(Suncorp.Models.UsUsuarios model, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginActualizarUsuario(model, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public bool EndActualizarUsuario(System.IAsyncResult result) {
            return base.Channel.EndActualizarUsuario(result);
        }
        
        private System.IAsyncResult OnBeginActualizarUsuario(object[] inValues, System.AsyncCallback callback, object asyncState) {
            Suncorp.Models.UsUsuarios model = ((Suncorp.Models.UsUsuarios)(inValues[0]));
            return this.BeginActualizarUsuario(model, callback, asyncState);
        }
        
        private object[] OnEndActualizarUsuario(System.IAsyncResult result) {
            bool retVal = this.EndActualizarUsuario(result);
            return new object[] {
                    retVal};
        }
        
        private void OnActualizarUsuarioCompleted(object state) {
            if ((this.ActualizarUsuarioCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ActualizarUsuarioCompleted(this, new ActualizarUsuarioCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ActualizarUsuarioAsync(Suncorp.Models.UsUsuarios model) {
            this.ActualizarUsuarioAsync(model, null);
        }
        
        public void ActualizarUsuarioAsync(Suncorp.Models.UsUsuarios model, object userState) {
            if ((this.onBeginActualizarUsuarioDelegate == null)) {
                this.onBeginActualizarUsuarioDelegate = new BeginOperationDelegate(this.OnBeginActualizarUsuario);
            }
            if ((this.onEndActualizarUsuarioDelegate == null)) {
                this.onEndActualizarUsuarioDelegate = new EndOperationDelegate(this.OnEndActualizarUsuario);
            }
            if ((this.onActualizarUsuarioCompletedDelegate == null)) {
                this.onActualizarUsuarioCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnActualizarUsuarioCompleted);
            }
            base.InvokeAsync(this.onBeginActualizarUsuarioDelegate, new object[] {
                        model}, this.onEndActualizarUsuarioDelegate, this.onActualizarUsuarioCompletedDelegate, userState);
        }
        
        public bool EliminarUsuario(Suncorp.Models.UsUsuarios model) {
            return base.Channel.EliminarUsuario(model);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginEliminarUsuario(Suncorp.Models.UsUsuarios model, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginEliminarUsuario(model, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public bool EndEliminarUsuario(System.IAsyncResult result) {
            return base.Channel.EndEliminarUsuario(result);
        }
        
        private System.IAsyncResult OnBeginEliminarUsuario(object[] inValues, System.AsyncCallback callback, object asyncState) {
            Suncorp.Models.UsUsuarios model = ((Suncorp.Models.UsUsuarios)(inValues[0]));
            return this.BeginEliminarUsuario(model, callback, asyncState);
        }
        
        private object[] OnEndEliminarUsuario(System.IAsyncResult result) {
            bool retVal = this.EndEliminarUsuario(result);
            return new object[] {
                    retVal};
        }
        
        private void OnEliminarUsuarioCompleted(object state) {
            if ((this.EliminarUsuarioCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.EliminarUsuarioCompleted(this, new EliminarUsuarioCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void EliminarUsuarioAsync(Suncorp.Models.UsUsuarios model) {
            this.EliminarUsuarioAsync(model, null);
        }
        
        public void EliminarUsuarioAsync(Suncorp.Models.UsUsuarios model, object userState) {
            if ((this.onBeginEliminarUsuarioDelegate == null)) {
                this.onBeginEliminarUsuarioDelegate = new BeginOperationDelegate(this.OnBeginEliminarUsuario);
            }
            if ((this.onEndEliminarUsuarioDelegate == null)) {
                this.onEndEliminarUsuarioDelegate = new EndOperationDelegate(this.OnEndEliminarUsuario);
            }
            if ((this.onEliminarUsuarioCompletedDelegate == null)) {
                this.onEliminarUsuarioCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnEliminarUsuarioCompleted);
            }
            base.InvokeAsync(this.onBeginEliminarUsuarioDelegate, new object[] {
                        model}, this.onEndEliminarUsuarioDelegate, this.onEliminarUsuarioCompletedDelegate, userState);
        }
        
        public Suncorp.Models.WcfResponse ObtenerUsuarioLogin(string usuario, string contrasena) {
            return base.Channel.ObtenerUsuarioLogin(usuario, contrasena);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginObtenerUsuarioLogin(string usuario, string contrasena, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenerUsuarioLogin(usuario, contrasena, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public Suncorp.Models.WcfResponse EndObtenerUsuarioLogin(System.IAsyncResult result) {
            return base.Channel.EndObtenerUsuarioLogin(result);
        }
        
        private System.IAsyncResult OnBeginObtenerUsuarioLogin(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string usuario = ((string)(inValues[0]));
            string contrasena = ((string)(inValues[1]));
            return this.BeginObtenerUsuarioLogin(usuario, contrasena, callback, asyncState);
        }
        
        private object[] OnEndObtenerUsuarioLogin(System.IAsyncResult result) {
            Suncorp.Models.WcfResponse retVal = this.EndObtenerUsuarioLogin(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenerUsuarioLoginCompleted(object state) {
            if ((this.ObtenerUsuarioLoginCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenerUsuarioLoginCompleted(this, new ObtenerUsuarioLoginCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenerUsuarioLoginAsync(string usuario, string contrasena) {
            this.ObtenerUsuarioLoginAsync(usuario, contrasena, null);
        }
        
        public void ObtenerUsuarioLoginAsync(string usuario, string contrasena, object userState) {
            if ((this.onBeginObtenerUsuarioLoginDelegate == null)) {
                this.onBeginObtenerUsuarioLoginDelegate = new BeginOperationDelegate(this.OnBeginObtenerUsuarioLogin);
            }
            if ((this.onEndObtenerUsuarioLoginDelegate == null)) {
                this.onEndObtenerUsuarioLoginDelegate = new EndOperationDelegate(this.OnEndObtenerUsuarioLogin);
            }
            if ((this.onObtenerUsuarioLoginCompletedDelegate == null)) {
                this.onObtenerUsuarioLoginCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenerUsuarioLoginCompleted);
            }
            base.InvokeAsync(this.onBeginObtenerUsuarioLoginDelegate, new object[] {
                        usuario,
                        contrasena}, this.onEndObtenerUsuarioLoginDelegate, this.onObtenerUsuarioLoginCompletedDelegate, userState);
        }
    }
}
