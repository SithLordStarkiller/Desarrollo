﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenCsi.ControllerServer.UsuariosServer {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UsuariosServer.IUsuarios")]
    public interface IUsuarios {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarios/InsertarUsuario", ReplyAction="http://tempuri.org/IUsuarios/InsertarUsuarioResponse")]
        int InsertarUsuario(ExamenCsi.Entities.UsUsuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarios/InsertarUsuario", ReplyAction="http://tempuri.org/IUsuarios/InsertarUsuarioResponse")]
        System.Threading.Tasks.Task<int> InsertarUsuarioAsync(ExamenCsi.Entities.UsUsuario usuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsuariosChannel : ExamenCsi.ControllerServer.UsuariosServer.IUsuarios, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsuariosClient : System.ServiceModel.ClientBase<ExamenCsi.ControllerServer.UsuariosServer.IUsuarios>, ExamenCsi.ControllerServer.UsuariosServer.IUsuarios {
        
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
        
        public int InsertarUsuario(ExamenCsi.Entities.UsUsuario usuario) {
            return base.Channel.InsertarUsuario(usuario);
        }
        
        public System.Threading.Tasks.Task<int> InsertarUsuarioAsync(ExamenCsi.Entities.UsUsuario usuario) {
            return base.Channel.InsertarUsuarioAsync(usuario);
        }
    }
}
