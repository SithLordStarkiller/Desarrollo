﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace srvBroxel.wsBitacora {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Response", Namespace="http://schemas.datacontract.org/2004/07/wsBitacora")]
    [System.SerializableAttribute()]
    public partial class Response : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MensajeRespuestaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MensajeRespuesta {
            get {
                return this.MensajeRespuestaField;
            }
            set {
                if ((object.ReferenceEquals(this.MensajeRespuestaField, value) != true)) {
                    this.MensajeRespuestaField = value;
                    this.RaisePropertyChanged("MensajeRespuesta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/wsBitacoras")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wsBitacora.IWsBitacoras")]
    public interface IWsBitacoras {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsBitacoras/InsertaBitacora", ReplyAction="http://tempuri.org/IWsBitacoras/InsertaBitacoraResponse")]
        srvBroxel.wsBitacora.Response InsertaBitacora(string pSistema, string pBaseDatos, string pTabla, string pTransaccion, string pDescripcion, string pIpUsuario, string pUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsBitacoras/InsertaBitacora", ReplyAction="http://tempuri.org/IWsBitacoras/InsertaBitacoraResponse")]
        System.Threading.Tasks.Task<srvBroxel.wsBitacora.Response> InsertaBitacoraAsync(string pSistema, string pBaseDatos, string pTabla, string pTransaccion, string pDescripcion, string pIpUsuario, string pUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsBitacoras/GetData", ReplyAction="http://tempuri.org/IWsBitacoras/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsBitacoras/GetData", ReplyAction="http://tempuri.org/IWsBitacoras/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsBitacoras/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IWsBitacoras/GetDataUsingDataContractResponse")]
        srvBroxel.wsBitacora.CompositeType GetDataUsingDataContract(srvBroxel.wsBitacora.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsBitacoras/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IWsBitacoras/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<srvBroxel.wsBitacora.CompositeType> GetDataUsingDataContractAsync(srvBroxel.wsBitacora.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWsBitacorasChannel : srvBroxel.wsBitacora.IWsBitacoras, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsBitacorasClient : System.ServiceModel.ClientBase<srvBroxel.wsBitacora.IWsBitacoras>, srvBroxel.wsBitacora.IWsBitacoras {
        
        public WsBitacorasClient() {
        }
        
        public WsBitacorasClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsBitacorasClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsBitacorasClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsBitacorasClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public srvBroxel.wsBitacora.Response InsertaBitacora(string pSistema, string pBaseDatos, string pTabla, string pTransaccion, string pDescripcion, string pIpUsuario, string pUsuario) {
            return base.Channel.InsertaBitacora(pSistema, pBaseDatos, pTabla, pTransaccion, pDescripcion, pIpUsuario, pUsuario);
        }
        
        public System.Threading.Tasks.Task<srvBroxel.wsBitacora.Response> InsertaBitacoraAsync(string pSistema, string pBaseDatos, string pTabla, string pTransaccion, string pDescripcion, string pIpUsuario, string pUsuario) {
            return base.Channel.InsertaBitacoraAsync(pSistema, pBaseDatos, pTabla, pTransaccion, pDescripcion, pIpUsuario, pUsuario);
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public srvBroxel.wsBitacora.CompositeType GetDataUsingDataContract(srvBroxel.wsBitacora.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<srvBroxel.wsBitacora.CompositeType> GetDataUsingDataContractAsync(srvBroxel.wsBitacora.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
    }
}
