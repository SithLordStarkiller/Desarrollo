﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Controlador.SVR_Login {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SVR_Login.IS_Login")]
    public interface IS_Login {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_Login/LoginAdministrador", ReplyAction="http://tempuri.org/IS_Login/LoginAdministradorResponse")]
        string LoginAdministrador(string Usuario, string Contrasena);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IS_Login/LoginAdministrador", ReplyAction="http://tempuri.org/IS_Login/LoginAdministradorResponse")]
        System.IAsyncResult BeginLoginAdministrador(string Usuario, string Contrasena, System.AsyncCallback callback, object asyncState);
        
        string EndLoginAdministrador(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_Login/ObtenPersona", ReplyAction="http://tempuri.org/IS_Login/ObtenPersonaResponse")]
        string ObtenPersona(Universidad.Entidades.US_USUARIOS usuario);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IS_Login/ObtenPersona", ReplyAction="http://tempuri.org/IS_Login/ObtenPersonaResponse")]
        System.IAsyncResult BeginObtenPersona(Universidad.Entidades.US_USUARIOS usuario, System.AsyncCallback callback, object asyncState);
        
        string EndObtenPersona(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IS_LoginChannel : Universidad.Controlador.SVR_Login.IS_Login, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginAdministradorCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public LoginAdministradorCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class ObtenPersonaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ObtenPersonaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public partial class S_LoginClient : System.ServiceModel.ClientBase<Universidad.Controlador.SVR_Login.IS_Login>, Universidad.Controlador.SVR_Login.IS_Login {
        
        private BeginOperationDelegate onBeginLoginAdministradorDelegate;
        
        private EndOperationDelegate onEndLoginAdministradorDelegate;
        
        private System.Threading.SendOrPostCallback onLoginAdministradorCompletedDelegate;
        
        private BeginOperationDelegate onBeginObtenPersonaDelegate;
        
        private EndOperationDelegate onEndObtenPersonaDelegate;
        
        private System.Threading.SendOrPostCallback onObtenPersonaCompletedDelegate;
        
        public S_LoginClient() {
        }
        
        public S_LoginClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public S_LoginClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public S_LoginClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public S_LoginClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<LoginAdministradorCompletedEventArgs> LoginAdministradorCompleted;
        
        public event System.EventHandler<ObtenPersonaCompletedEventArgs> ObtenPersonaCompleted;
        
        public string LoginAdministrador(string Usuario, string Contrasena) {
            return base.Channel.LoginAdministrador(Usuario, Contrasena);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginLoginAdministrador(string Usuario, string Contrasena, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginLoginAdministrador(Usuario, Contrasena, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndLoginAdministrador(System.IAsyncResult result) {
            return base.Channel.EndLoginAdministrador(result);
        }
        
        private System.IAsyncResult OnBeginLoginAdministrador(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string Usuario = ((string)(inValues[0]));
            string Contrasena = ((string)(inValues[1]));
            return this.BeginLoginAdministrador(Usuario, Contrasena, callback, asyncState);
        }
        
        private object[] OnEndLoginAdministrador(System.IAsyncResult result) {
            string retVal = this.EndLoginAdministrador(result);
            return new object[] {
                    retVal};
        }
        
        private void OnLoginAdministradorCompleted(object state) {
            if ((this.LoginAdministradorCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.LoginAdministradorCompleted(this, new LoginAdministradorCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void LoginAdministradorAsync(string Usuario, string Contrasena) {
            this.LoginAdministradorAsync(Usuario, Contrasena, null);
        }
        
        public void LoginAdministradorAsync(string Usuario, string Contrasena, object userState) {
            if ((this.onBeginLoginAdministradorDelegate == null)) {
                this.onBeginLoginAdministradorDelegate = new BeginOperationDelegate(this.OnBeginLoginAdministrador);
            }
            if ((this.onEndLoginAdministradorDelegate == null)) {
                this.onEndLoginAdministradorDelegate = new EndOperationDelegate(this.OnEndLoginAdministrador);
            }
            if ((this.onLoginAdministradorCompletedDelegate == null)) {
                this.onLoginAdministradorCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnLoginAdministradorCompleted);
            }
            base.InvokeAsync(this.onBeginLoginAdministradorDelegate, new object[] {
                        Usuario,
                        Contrasena}, this.onEndLoginAdministradorDelegate, this.onLoginAdministradorCompletedDelegate, userState);
        }
        
        public string ObtenPersona(Universidad.Entidades.US_USUARIOS usuario) {
            return base.Channel.ObtenPersona(usuario);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginObtenPersona(Universidad.Entidades.US_USUARIOS usuario, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginObtenPersona(usuario, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndObtenPersona(System.IAsyncResult result) {
            return base.Channel.EndObtenPersona(result);
        }
        
        private System.IAsyncResult OnBeginObtenPersona(object[] inValues, System.AsyncCallback callback, object asyncState) {
            Universidad.Entidades.US_USUARIOS usuario = ((Universidad.Entidades.US_USUARIOS)(inValues[0]));
            return this.BeginObtenPersona(usuario, callback, asyncState);
        }
        
        private object[] OnEndObtenPersona(System.IAsyncResult result) {
            string retVal = this.EndObtenPersona(result);
            return new object[] {
                    retVal};
        }
        
        private void OnObtenPersonaCompleted(object state) {
            if ((this.ObtenPersonaCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ObtenPersonaCompleted(this, new ObtenPersonaCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ObtenPersonaAsync(Universidad.Entidades.US_USUARIOS usuario) {
            this.ObtenPersonaAsync(usuario, null);
        }
        
        public void ObtenPersonaAsync(Universidad.Entidades.US_USUARIOS usuario, object userState) {
            if ((this.onBeginObtenPersonaDelegate == null)) {
                this.onBeginObtenPersonaDelegate = new BeginOperationDelegate(this.OnBeginObtenPersona);
            }
            if ((this.onEndObtenPersonaDelegate == null)) {
                this.onEndObtenPersonaDelegate = new EndOperationDelegate(this.OnEndObtenPersona);
            }
            if ((this.onObtenPersonaCompletedDelegate == null)) {
                this.onObtenPersonaCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnObtenPersonaCompleted);
            }
            base.InvokeAsync(this.onBeginObtenPersonaDelegate, new object[] {
                        usuario}, this.onEndObtenPersonaDelegate, this.onObtenPersonaCompletedDelegate, userState);
        }
    }
}
