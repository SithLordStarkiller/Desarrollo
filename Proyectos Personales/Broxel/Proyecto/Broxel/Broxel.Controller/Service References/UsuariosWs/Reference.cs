﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Broxel.Controller.UsuariosWs {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UsUsuarios", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class UsUsuarios : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IdUsuarioField;
        
        private System.Nullable<int> IdEstatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsuarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContrasenaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int IdUsuario {
            get {
                return this.IdUsuarioField;
            }
            set {
                if ((this.IdUsuarioField.Equals(value) != true)) {
                    this.IdUsuarioField = value;
                    this.RaisePropertyChanged("IdUsuario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public System.Nullable<int> IdEstatus {
            get {
                return this.IdEstatusField;
            }
            set {
                if ((this.IdEstatusField.Equals(value) != true)) {
                    this.IdEstatusField = value;
                    this.RaisePropertyChanged("IdEstatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Usuario {
            get {
                return this.UsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.UsuarioField, value) != true)) {
                    this.UsuarioField = value;
                    this.RaisePropertyChanged("Usuario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Contrasena {
            get {
                return this.ContrasenaField;
            }
            set {
                if ((object.ReferenceEquals(this.ContrasenaField, value) != true)) {
                    this.ContrasenaField = value;
                    this.RaisePropertyChanged("Contrasena");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UsuariosWs.UsuariosWsSoap")]
    public interface UsuariosWsSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento usuario del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenUsUsuarionPorLogin", ReplyAction="*")]
        Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponse ObtenUsUsuarionPorLogin(Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ObtenUsUsuarionPorLogin", ReplyAction="*")]
        System.Threading.Tasks.Task<Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponse> ObtenUsUsuarionPorLoginAsync(Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenUsUsuarionPorLoginRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenUsUsuarionPorLogin", Namespace="http://tempuri.org/", Order=0)]
        public Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequestBody Body;
        
        public ObtenUsUsuarionPorLoginRequest() {
        }
        
        public ObtenUsUsuarionPorLoginRequest(Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenUsUsuarionPorLoginRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string usuario;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string contrasena;
        
        public ObtenUsUsuarionPorLoginRequestBody() {
        }
        
        public ObtenUsUsuarionPorLoginRequestBody(string usuario, string contrasena) {
            this.usuario = usuario;
            this.contrasena = contrasena;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ObtenUsUsuarionPorLoginResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ObtenUsUsuarionPorLoginResponse", Namespace="http://tempuri.org/", Order=0)]
        public Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponseBody Body;
        
        public ObtenUsUsuarionPorLoginResponse() {
        }
        
        public ObtenUsUsuarionPorLoginResponse(Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ObtenUsUsuarionPorLoginResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Broxel.Controller.UsuariosWs.UsUsuarios ObtenUsUsuarionPorLoginResult;
        
        public ObtenUsUsuarionPorLoginResponseBody() {
        }
        
        public ObtenUsUsuarionPorLoginResponseBody(Broxel.Controller.UsuariosWs.UsUsuarios ObtenUsUsuarionPorLoginResult) {
            this.ObtenUsUsuarionPorLoginResult = ObtenUsUsuarionPorLoginResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface UsuariosWsSoapChannel : Broxel.Controller.UsuariosWs.UsuariosWsSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsuariosWsSoapClient : System.ServiceModel.ClientBase<Broxel.Controller.UsuariosWs.UsuariosWsSoap>, Broxel.Controller.UsuariosWs.UsuariosWsSoap {
        
        public UsuariosWsSoapClient() {
        }
        
        public UsuariosWsSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsuariosWsSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuariosWsSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuariosWsSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponse Broxel.Controller.UsuariosWs.UsuariosWsSoap.ObtenUsUsuarionPorLogin(Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest request) {
            return base.Channel.ObtenUsUsuarionPorLogin(request);
        }
        
        public Broxel.Controller.UsuariosWs.UsUsuarios ObtenUsUsuarionPorLogin(string usuario, string contrasena) {
            Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest inValue = new Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest();
            inValue.Body = new Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequestBody();
            inValue.Body.usuario = usuario;
            inValue.Body.contrasena = contrasena;
            Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponse retVal = ((Broxel.Controller.UsuariosWs.UsuariosWsSoap)(this)).ObtenUsUsuarionPorLogin(inValue);
            return retVal.Body.ObtenUsUsuarionPorLoginResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponse> Broxel.Controller.UsuariosWs.UsuariosWsSoap.ObtenUsUsuarionPorLoginAsync(Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest request) {
            return base.Channel.ObtenUsUsuarionPorLoginAsync(request);
        }
        
        public System.Threading.Tasks.Task<Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginResponse> ObtenUsUsuarionPorLoginAsync(string usuario, string contrasena) {
            Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest inValue = new Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequest();
            inValue.Body = new Broxel.Controller.UsuariosWs.ObtenUsUsuarionPorLoginRequestBody();
            inValue.Body.usuario = usuario;
            inValue.Body.contrasena = contrasena;
            return ((Broxel.Controller.UsuariosWs.UsuariosWsSoap)(this)).ObtenUsUsuarionPorLoginAsync(inValue);
        }
    }
}