﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunCorp.Controller.SvcServicesEntities {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SvcServicesEntities.IEntitiesServer")]
    public interface IEntitiesServer {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetUsUsuarios", ReplyAction="http://tempuri.org/IEntitiesServer/GetUsUsuariosResponse")]
        SunCorp.Entities.Entities.UsUsuarios GetUsUsuarios(string user, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetUsUsuarios", ReplyAction="http://tempuri.org/IEntitiesServer/GetUsUsuariosResponse")]
        System.Threading.Tasks.Task<SunCorp.Entities.Entities.UsUsuarios> GetUsUsuariosAsync(string user, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEntitiesServerChannel : SunCorp.Controller.SvcServicesEntities.IEntitiesServer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EntitiesServerClient : System.ServiceModel.ClientBase<SunCorp.Controller.SvcServicesEntities.IEntitiesServer>, SunCorp.Controller.SvcServicesEntities.IEntitiesServer {
        
        public EntitiesServerClient() {
        }
        
        public EntitiesServerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EntitiesServerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EntitiesServerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EntitiesServerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SunCorp.Entities.Entities.UsUsuarios GetUsUsuarios(string user, string password) {
            return base.Channel.GetUsUsuarios(user, password);
        }
        
        public System.Threading.Tasks.Task<SunCorp.Entities.Entities.UsUsuarios> GetUsUsuariosAsync(string user, string password) {
            return base.Channel.GetUsUsuariosAsync(user, password);
        }
    }
}
