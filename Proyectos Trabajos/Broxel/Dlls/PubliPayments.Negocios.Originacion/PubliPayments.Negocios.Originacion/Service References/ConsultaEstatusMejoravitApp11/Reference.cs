﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.33440
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WsResConsultaEstatus", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class WsResConsultaEstatus : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSidMensajeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSMensajeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSvueltaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSUsuarioAltaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSFechaAltaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSUsuarioModificoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WSFechaModificoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string WSidMensaje {
            get {
                return this.WSidMensajeField;
            }
            set {
                if ((object.ReferenceEquals(this.WSidMensajeField, value) != true)) {
                    this.WSidMensajeField = value;
                    this.RaisePropertyChanged("WSidMensaje");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string WSMensaje {
            get {
                return this.WSMensajeField;
            }
            set {
                if ((object.ReferenceEquals(this.WSMensajeField, value) != true)) {
                    this.WSMensajeField = value;
                    this.RaisePropertyChanged("WSMensaje");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string WSvuelta {
            get {
                return this.WSvueltaField;
            }
            set {
                if ((object.ReferenceEquals(this.WSvueltaField, value) != true)) {
                    this.WSvueltaField = value;
                    this.RaisePropertyChanged("WSvuelta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string WSUsuarioAlta {
            get {
                return this.WSUsuarioAltaField;
            }
            set {
                if ((object.ReferenceEquals(this.WSUsuarioAltaField, value) != true)) {
                    this.WSUsuarioAltaField = value;
                    this.RaisePropertyChanged("WSUsuarioAlta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string WSFechaAlta {
            get {
                return this.WSFechaAltaField;
            }
            set {
                if ((object.ReferenceEquals(this.WSFechaAltaField, value) != true)) {
                    this.WSFechaAltaField = value;
                    this.RaisePropertyChanged("WSFechaAlta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string WSUsuarioModifico {
            get {
                return this.WSUsuarioModificoField;
            }
            set {
                if ((object.ReferenceEquals(this.WSUsuarioModificoField, value) != true)) {
                    this.WSUsuarioModificoField = value;
                    this.RaisePropertyChanged("WSUsuarioModifico");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string WSFechaModifico {
            get {
                return this.WSFechaModificoField;
            }
            set {
                if ((object.ReferenceEquals(this.WSFechaModificoField, value) != true)) {
                    this.WSFechaModificoField = value;
                    this.RaisePropertyChanged("WSFechaModifico");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ConsultaEstatusMejoravitApp11.WSConsultaEstatusMejoravitAppSoap")]
    public interface WSConsultaEstatusMejoravitAppSoap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento NSS del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ConsultaEstatus", ReplyAction="*")]
        PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusResponse ConsultaEstatus(PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ConsultaEstatusRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ConsultaEstatus", Namespace="http://tempuri.org/", Order=0)]
        public PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequestBody Body;
        
        public ConsultaEstatusRequest() {
        }
        
        public ConsultaEstatusRequest(PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ConsultaEstatusRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string NSS;
        
        public ConsultaEstatusRequestBody() {
        }
        
        public ConsultaEstatusRequestBody(string NSS) {
            this.NSS = NSS;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ConsultaEstatusResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ConsultaEstatusResponse", Namespace="http://tempuri.org/", Order=0)]
        public PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusResponseBody Body;
        
        public ConsultaEstatusResponse() {
        }
        
        public ConsultaEstatusResponse(PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ConsultaEstatusResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WsResConsultaEstatus ConsultaEstatusResult;
        
        public ConsultaEstatusResponseBody() {
        }
        
        public ConsultaEstatusResponseBody(PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WsResConsultaEstatus ConsultaEstatusResult) {
            this.ConsultaEstatusResult = ConsultaEstatusResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSConsultaEstatusMejoravitAppSoapChannel : PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WSConsultaEstatusMejoravitAppSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSConsultaEstatusMejoravitAppSoapClient : System.ServiceModel.ClientBase<PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WSConsultaEstatusMejoravitAppSoap>, PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WSConsultaEstatusMejoravitAppSoap {
        
        public WSConsultaEstatusMejoravitAppSoapClient() {
        }
        
        public WSConsultaEstatusMejoravitAppSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSConsultaEstatusMejoravitAppSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSConsultaEstatusMejoravitAppSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSConsultaEstatusMejoravitAppSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusResponse PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WSConsultaEstatusMejoravitAppSoap.ConsultaEstatus(PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequest request) {
            return base.Channel.ConsultaEstatus(request);
        }
        
        public PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WsResConsultaEstatus ConsultaEstatus(string NSS) {
            PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequest inValue = new PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequest();
            inValue.Body = new PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusRequestBody();
            inValue.Body.NSS = NSS;
            PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.ConsultaEstatusResponse retVal = ((PubliPayments.Negocios.Originacion.ConsultaEstatusMejoravitApp11.WSConsultaEstatusMejoravitAppSoap)(this)).ConsultaEstatus(inValue);
            return retVal.Body.ConsultaEstatusResult;
        }
    }
}
