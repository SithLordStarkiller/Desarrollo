﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenEdenred.Controller.SvcUsuarios {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SvcUsuarios.IUsuarios")]
    public interface IUsuarios {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarios/ExisteUsuario", ReplyAction="http://tempuri.org/IUsuarios/ExisteUsuarioResponse")]
        ExamenEdenred.Entities.Entities.UsUsuarios ExisteUsuario(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarios/ExisteUsuario", ReplyAction="http://tempuri.org/IUsuarios/ExisteUsuarioResponse")]
        System.Threading.Tasks.Task<ExamenEdenred.Entities.Entities.UsUsuarios> ExisteUsuarioAsync(int idUsuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsuariosChannel : ExamenEdenred.Controller.SvcUsuarios.IUsuarios, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsuariosClient : System.ServiceModel.ClientBase<ExamenEdenred.Controller.SvcUsuarios.IUsuarios>, ExamenEdenred.Controller.SvcUsuarios.IUsuarios {
        
        public UsuariosClient() {
        }
        
        public UsuariosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsuariosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuariosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuariosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ExamenEdenred.Entities.Entities.UsUsuarios ExisteUsuario(int idUsuario) {
            return base.Channel.ExisteUsuario(idUsuario);
        }
        
        public System.Threading.Tasks.Task<ExamenEdenred.Entities.Entities.UsUsuarios> ExisteUsuarioAsync(int idUsuario) {
            return base.Channel.ExisteUsuarioAsync(idUsuario);
        }
    }
}
