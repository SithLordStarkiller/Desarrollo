﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunCorp.Controller.SvcServicesCatalogs {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SvcServicesCatalogs.ICatalogsServer")]
    public interface ICatalogsServer {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICatalogsServer/GetListCatalogsSystem", ReplyAction="http://tempuri.org/ICatalogsServer/GetListCatalogsSystemResponse")]
        System.Collections.Generic.List<SunCorp.Entities.Generic.GenericTable> GetListCatalogsSystem();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICatalogsServer/GetListCatalogsSystem", ReplyAction="http://tempuri.org/ICatalogsServer/GetListCatalogsSystemResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.Generic.GenericTable>> GetListCatalogsSystemAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICatalogsServer/GetListMenuForUserType", ReplyAction="http://tempuri.org/ICatalogsServer/GetListMenuForUserTypeResponse")]
        System.Collections.Generic.List<SunCorp.Entities.SisArbolMenu> GetListMenuForUserType(SunCorp.Entities.UsUsuarios user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICatalogsServer/GetListMenuForUserType", ReplyAction="http://tempuri.org/ICatalogsServer/GetListMenuForUserTypeResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.SisArbolMenu>> GetListMenuForUserTypeAsync(SunCorp.Entities.UsUsuarios user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICatalogsServerChannel : SunCorp.Controller.SvcServicesCatalogs.ICatalogsServer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CatalogsServerClient : System.ServiceModel.ClientBase<SunCorp.Controller.SvcServicesCatalogs.ICatalogsServer>, SunCorp.Controller.SvcServicesCatalogs.ICatalogsServer {
        
        public CatalogsServerClient() {
        }
        
        public CatalogsServerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CatalogsServerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CatalogsServerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CatalogsServerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<SunCorp.Entities.Generic.GenericTable> GetListCatalogsSystem() {
            return base.Channel.GetListCatalogsSystem();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.Generic.GenericTable>> GetListCatalogsSystemAsync() {
            return base.Channel.GetListCatalogsSystemAsync();
        }
        
        public System.Collections.Generic.List<SunCorp.Entities.SisArbolMenu> GetListMenuForUserType(SunCorp.Entities.UsUsuarios user) {
            return base.Channel.GetListMenuForUserType(user);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<SunCorp.Entities.SisArbolMenu>> GetListMenuForUserTypeAsync(SunCorp.Entities.UsUsuarios user) {
            return base.Channel.GetListMenuForUserTypeAsync(user);
        }
    }
}
