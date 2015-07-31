﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Controlador.SVRUsuarios {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SVRUsuarios.ISUsuarios")]
    public interface ISUsuarios {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISUsuarios/ObtenUsuario", ReplyAction="http://tempuri.org/ISUsuarios/ObtenUsuarioResponse")]
        Universidad.Entidades.US_USUARIOS ObtenUsuario(string usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISUsuarios/ObtenUsuario", ReplyAction="http://tempuri.org/ISUsuarios/ObtenUsuarioResponse")]
        System.Threading.Tasks.Task<Universidad.Entidades.US_USUARIOS> ObtenUsuarioAsync(string usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISUsuarios/ObtenUsuarioPorId", ReplyAction="http://tempuri.org/ISUsuarios/ObtenUsuarioPorIdResponse")]
        Universidad.Entidades.US_USUARIOS ObtenUsuarioPorId(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISUsuarios/ObtenUsuarioPorId", ReplyAction="http://tempuri.org/ISUsuarios/ObtenUsuarioPorIdResponse")]
        System.Threading.Tasks.Task<Universidad.Entidades.US_USUARIOS> ObtenUsuarioPorIdAsync(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISUsuarios/CrearCuantaUsuario", ReplyAction="http://tempuri.org/ISUsuarios/CrearCuantaUsuarioResponse")]
        Universidad.Entidades.US_USUARIOS CrearCuantaUsuario(Universidad.Entidades.US_USUARIOS usuario, string personaId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISUsuarios/CrearCuantaUsuario", ReplyAction="http://tempuri.org/ISUsuarios/CrearCuantaUsuarioResponse")]
        System.Threading.Tasks.Task<Universidad.Entidades.US_USUARIOS> CrearCuantaUsuarioAsync(Universidad.Entidades.US_USUARIOS usuario, string personaId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISUsuariosChannel : Universidad.Controlador.SVRUsuarios.ISUsuarios, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SUsuariosClient : System.ServiceModel.ClientBase<Universidad.Controlador.SVRUsuarios.ISUsuarios>, Universidad.Controlador.SVRUsuarios.ISUsuarios {
        
        public SUsuariosClient() {
        }
        
        public SUsuariosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SUsuariosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SUsuariosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SUsuariosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Universidad.Entidades.US_USUARIOS ObtenUsuario(string usuario) {
            return base.Channel.ObtenUsuario(usuario);
        }
        
        public System.Threading.Tasks.Task<Universidad.Entidades.US_USUARIOS> ObtenUsuarioAsync(string usuario) {
            return base.Channel.ObtenUsuarioAsync(usuario);
        }
        
        public Universidad.Entidades.US_USUARIOS ObtenUsuarioPorId(int idUsuario) {
            return base.Channel.ObtenUsuarioPorId(idUsuario);
        }
        
        public System.Threading.Tasks.Task<Universidad.Entidades.US_USUARIOS> ObtenUsuarioPorIdAsync(int idUsuario) {
            return base.Channel.ObtenUsuarioPorIdAsync(idUsuario);
        }
        
        public Universidad.Entidades.US_USUARIOS CrearCuantaUsuario(Universidad.Entidades.US_USUARIOS usuario, string personaId) {
            return base.Channel.CrearCuantaUsuario(usuario, personaId);
        }
        
        public System.Threading.Tasks.Task<Universidad.Entidades.US_USUARIOS> CrearCuantaUsuarioAsync(Universidad.Entidades.US_USUARIOS usuario, string personaId) {
            return base.Channel.CrearCuantaUsuarioAsync(usuario, personaId);
        }
    }
}