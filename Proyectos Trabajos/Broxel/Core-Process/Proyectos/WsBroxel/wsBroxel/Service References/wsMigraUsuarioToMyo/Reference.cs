﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsBroxel.wsMigraUsuarioToMyo {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/WcfMigraUsuariosToMyo")]
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseMigracion", Namespace="http://schemas.datacontract.org/2004/07/MigraUsuariosBroxelNeg")]
    [System.SerializableAttribute()]
    public partial class ResponseMigracion : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ErrorClienteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ErrorCuentasField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ErrorTarjetaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalUsuariosField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UsuariosMigradasField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UsuariosYaRegistradasField;
        
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
        public int ErrorCliente {
            get {
                return this.ErrorClienteField;
            }
            set {
                if ((this.ErrorClienteField.Equals(value) != true)) {
                    this.ErrorClienteField = value;
                    this.RaisePropertyChanged("ErrorCliente");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ErrorCuentas {
            get {
                return this.ErrorCuentasField;
            }
            set {
                if ((this.ErrorCuentasField.Equals(value) != true)) {
                    this.ErrorCuentasField = value;
                    this.RaisePropertyChanged("ErrorCuentas");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ErrorTarjeta {
            get {
                return this.ErrorTarjetaField;
            }
            set {
                if ((this.ErrorTarjetaField.Equals(value) != true)) {
                    this.ErrorTarjetaField = value;
                    this.RaisePropertyChanged("ErrorTarjeta");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalUsuarios {
            get {
                return this.TotalUsuariosField;
            }
            set {
                if ((this.TotalUsuariosField.Equals(value) != true)) {
                    this.TotalUsuariosField = value;
                    this.RaisePropertyChanged("TotalUsuarios");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserMessage {
            get {
                return this.UserMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.UserMessageField, value) != true)) {
                    this.UserMessageField = value;
                    this.RaisePropertyChanged("UserMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UsuariosMigradas {
            get {
                return this.UsuariosMigradasField;
            }
            set {
                if ((this.UsuariosMigradasField.Equals(value) != true)) {
                    this.UsuariosMigradasField = value;
                    this.RaisePropertyChanged("UsuariosMigradas");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UsuariosYaRegistradas {
            get {
                return this.UsuariosYaRegistradasField;
            }
            set {
                if ((this.UsuariosYaRegistradasField.Equals(value) != true)) {
                    this.UsuariosYaRegistradasField = value;
                    this.RaisePropertyChanged("UsuariosYaRegistradas");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="wsMigraUsuarioToMyo.IwsMigraUsuarioToMyo")]
    public interface IwsMigraUsuarioToMyo {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwsMigraUsuarioToMyo/GetData", ReplyAction="http://tempuri.org/IwsMigraUsuarioToMyo/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwsMigraUsuarioToMyo/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IwsMigraUsuarioToMyo/GetDataUsingDataContractResponse")]
        wsBroxel.wsMigraUsuarioToMyo.CompositeType GetDataUsingDataContract(wsBroxel.wsMigraUsuarioToMyo.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IwsMigraUsuarioToMyo/RealizaMigracionToMyo", ReplyAction="http://tempuri.org/IwsMigraUsuarioToMyo/RealizaMigracionToMyoResponse")]
        wsBroxel.wsMigraUsuarioToMyo.ResponseMigracion RealizaMigracionToMyo(int pUsuarioBroxelId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IwsMigraUsuarioToMyoChannel : wsBroxel.wsMigraUsuarioToMyo.IwsMigraUsuarioToMyo, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IwsMigraUsuarioToMyoClient : System.ServiceModel.ClientBase<wsBroxel.wsMigraUsuarioToMyo.IwsMigraUsuarioToMyo>, wsBroxel.wsMigraUsuarioToMyo.IwsMigraUsuarioToMyo {
        
        public IwsMigraUsuarioToMyoClient() {
        }
        
        public IwsMigraUsuarioToMyoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IwsMigraUsuarioToMyoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IwsMigraUsuarioToMyoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IwsMigraUsuarioToMyoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public wsBroxel.wsMigraUsuarioToMyo.CompositeType GetDataUsingDataContract(wsBroxel.wsMigraUsuarioToMyo.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public wsBroxel.wsMigraUsuarioToMyo.ResponseMigracion RealizaMigracionToMyo(int pUsuarioBroxelId) {
            return base.Channel.RealizaMigracionToMyo(pUsuarioBroxelId);
        }
    }
}