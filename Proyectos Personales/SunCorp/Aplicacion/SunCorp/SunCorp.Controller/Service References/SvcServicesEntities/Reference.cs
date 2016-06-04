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
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SunCorp.Entities.UsUsuarios))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<SunCorp.Entities.UsZona>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SunCorp.Entities.UsZona))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SunCorp.Entities.UsTipoUsuario))]
        SunCorp.Entities.UsUsuarios GetUsUsuarios(SunCorp.Entities.Generic.UserSession session);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetUsUsuarios", ReplyAction="http://tempuri.org/IEntitiesServer/GetUsUsuariosResponse")]
        System.Threading.Tasks.Task<SunCorp.Entities.UsUsuarios> GetUsUsuariosAsync(SunCorp.Entities.Generic.UserSession session);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetListUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/GetListUsZonaResponse")]
        System.Collections.Generic.List<SunCorp.Entities.UsZona> GetListUsZona();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetListUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/GetListUsZonaResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.UsZona>> GetListUsZonaAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetListUsZonasUser", ReplyAction="http://tempuri.org/IEntitiesServer/GetListUsZonasUserResponse")]
        System.Collections.Generic.List<SunCorp.Entities.UsZona> GetListUsZonasUser(SunCorp.Entities.UsUsuarios user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetListUsZonasUser", ReplyAction="http://tempuri.org/IEntitiesServer/GetListUsZonasUserResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.UsZona>> GetListUsZonasUserAsync(SunCorp.Entities.UsUsuarios user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/NewRegUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/NewRegUsZonaResponse")]
        SunCorp.Entities.UsZona NewRegUsZona(SunCorp.Entities.UsZona zona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/NewRegUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/NewRegUsZonaResponse")]
        System.Threading.Tasks.Task<SunCorp.Entities.UsZona> NewRegUsZonaAsync(SunCorp.Entities.UsZona zona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/UpdateRegUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/UpdateRegUsZonaResponse")]
        bool UpdateRegUsZona(SunCorp.Entities.UsZona zona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/UpdateRegUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/UpdateRegUsZonaResponse")]
        System.Threading.Tasks.Task<bool> UpdateRegUsZonaAsync(SunCorp.Entities.UsZona zona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/DeleteRegUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/DeleteRegUsZonaResponse")]
        bool DeleteRegUsZona(SunCorp.Entities.UsZona zona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/DeleteRegUsZona", ReplyAction="http://tempuri.org/IEntitiesServer/DeleteRegUsZonaResponse")]
        System.Threading.Tasks.Task<bool> DeleteRegUsZonaAsync(SunCorp.Entities.UsZona zona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetTypeUser", ReplyAction="http://tempuri.org/IEntitiesServer/GetTypeUserResponse")]
        SunCorp.Entities.UsTipoUsuario GetTypeUser(SunCorp.Entities.UsUsuarios user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEntitiesServer/GetTypeUser", ReplyAction="http://tempuri.org/IEntitiesServer/GetTypeUserResponse")]
        System.Threading.Tasks.Task<SunCorp.Entities.UsTipoUsuario> GetTypeUserAsync(SunCorp.Entities.UsUsuarios user);
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
        
        public SunCorp.Entities.UsUsuarios GetUsUsuarios(SunCorp.Entities.Generic.UserSession session) {
            return base.Channel.GetUsUsuarios(session);
        }
        
        public System.Threading.Tasks.Task<SunCorp.Entities.UsUsuarios> GetUsUsuariosAsync(SunCorp.Entities.Generic.UserSession session) {
            return base.Channel.GetUsUsuariosAsync(session);
        }
        
        public System.Collections.Generic.List<SunCorp.Entities.UsZona> GetListUsZona() {
            return base.Channel.GetListUsZona();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.UsZona>> GetListUsZonaAsync() {
            return base.Channel.GetListUsZonaAsync();
        }
        
        public System.Collections.Generic.List<SunCorp.Entities.UsZona> GetListUsZonasUser(SunCorp.Entities.UsUsuarios user) {
            return base.Channel.GetListUsZonasUser(user);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.UsZona>> GetListUsZonasUserAsync(SunCorp.Entities.UsUsuarios user) {
            return base.Channel.GetListUsZonasUserAsync(user);
        }
        
        public SunCorp.Entities.UsZona NewRegUsZona(SunCorp.Entities.UsZona zona) {
            return base.Channel.NewRegUsZona(zona);
        }
        
        public System.Threading.Tasks.Task<SunCorp.Entities.UsZona> NewRegUsZonaAsync(SunCorp.Entities.UsZona zona) {
            return base.Channel.NewRegUsZonaAsync(zona);
        }
        
        public bool UpdateRegUsZona(SunCorp.Entities.UsZona zona) {
            return base.Channel.UpdateRegUsZona(zona);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateRegUsZonaAsync(SunCorp.Entities.UsZona zona) {
            return base.Channel.UpdateRegUsZonaAsync(zona);
        }
        
        public bool DeleteRegUsZona(SunCorp.Entities.UsZona zona) {
            return base.Channel.DeleteRegUsZona(zona);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteRegUsZonaAsync(SunCorp.Entities.UsZona zona) {
            return base.Channel.DeleteRegUsZonaAsync(zona);
        }
        
        public SunCorp.Entities.UsTipoUsuario GetTypeUser(SunCorp.Entities.UsUsuarios user) {
            return base.Channel.GetTypeUser(user);
        }
        
        public System.Threading.Tasks.Task<SunCorp.Entities.UsTipoUsuario> GetTypeUserAsync(SunCorp.Entities.UsUsuarios user) {
            return base.Channel.GetTypeUserAsync(user);
        }
    }
}
