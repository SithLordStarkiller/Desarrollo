﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Controlador.SvcGestionCatalogos {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SvcGestionCatalogos.IS_GestionCatalogos")]
    public interface IS_GestionCatalogos {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatTipoUsuarios", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatTipoUsuariosResponse")]
        System.Collections.Generic.List<Universidad.Entidades.US_CAT_TIPO_USUARIO> ObtenTablaUsCatTipoUsuarios();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatTipoUsuarios", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatTipoUsuariosResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.US_CAT_TIPO_USUARIO>> ObtenTablaUsCatTipoUsuariosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatNivelUsuario", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatNivelUsuarioResponse")]
        System.Collections.Generic.List<Universidad.Entidades.US_CAT_NIVEL_USUARIO> ObtenTablaUsCatNivelUsuario();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatNivelUsuario", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatNivelUsuarioResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.US_CAT_NIVEL_USUARIO>> ObtenTablaUsCatNivelUsuarioAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatEstatusUsuario", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatEstatusUsuarioResponse")]
        System.Collections.Generic.List<Universidad.Entidades.US_CAT_ESTATUS_USUARIO> ObtenTablaUsCatEstatusUsuario();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatEstatusUsuario", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablaUsCatEstatusUsuarioResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.US_CAT_ESTATUS_USUARIO>> ObtenTablaUsCatEstatusUsuarioAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoUsuario", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoUsuarioResponse")]
        Universidad.Entidades.US_CAT_TIPO_USUARIO ObtenCatTipoUsuario(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoUsuario", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoUsuarioResponse")]
        System.Threading.Tasks.Task<Universidad.Entidades.US_CAT_TIPO_USUARIO> ObtenCatTipoUsuarioAsync(int idTipoUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogoNacionalidades", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogoNacionalidadesResponse")]
        System.Collections.Generic.List<Universidad.Entidades.PER_CAT_NACIONALIDAD> ObtenCatalogoNacionalidades();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogoNacionalidades", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogoNacionalidadesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.PER_CAT_NACIONALIDAD>> ObtenCatalogoNacionalidadesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoPersona", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoPersonaResponse")]
        System.Collections.Generic.List<Universidad.Entidades.PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoPersona", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatTipoPersonaResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.PER_CAT_TIPO_PERSONA>> ObtenCatTipoPersonaAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenColoniasPorCp", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenColoniasPorCpResponse")]
        System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenColoniasPorCp(int codigoPostal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenColoniasPorCp", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenColoniasPorCpResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS>> ObtenColoniasPorCpAsync(int codigoPostal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatEstados", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatEstadosResponse")]
        System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_ESTADO> ObtenCatEstados();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatEstados", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatEstadosResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_ESTADO>> ObtenCatEstadosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenMunicipios", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenMunicipiosResponse")]
        System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO> ObtenMunicipios(int estado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenMunicipios", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenMunicipiosResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO>> ObtenMunicipiosAsync(int estado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenColonias", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenColoniasResponse")]
        System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenColonias", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenColoniasResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS>> ObtenColoniasAsync(int estado, int municipio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCodigoPostal", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCodigoPostalResponse")]
        Universidad.Entidades.DIR_CAT_COLONIAS ObtenCodigoPostal(int estado, int municipio, int colonia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCodigoPostal", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCodigoPostalResponse")]
        System.Threading.Tasks.Task<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenCodigoPostalAsync(int estado, int municipio, int colonia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablasCatalogos", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablasCatalogosResponse")]
        System.Collections.Generic.List<Universidad.Entidades.Catalogos.ListasGenerica> ObtenTablasCatalogos();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenTablasCatalogos", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenTablasCatalogosResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.Catalogos.ListasGenerica>> ObtenTablasCatalogosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosColonias", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosColoniasResponse")]
        System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenCatalogosColonias();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosColonias", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosColoniasResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS>> ObtenCatalogosColoniasAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosMunicipios", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosMunicipiosResponse")]
        System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO> ObtenCatalogosMunicipios();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosMunicipios", ReplyAction="http://tempuri.org/IS_GestionCatalogos/ObtenCatalogosMunicipiosResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO>> ObtenCatalogosMunicipiosAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IS_GestionCatalogosChannel : Universidad.Controlador.SvcGestionCatalogos.IS_GestionCatalogos, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class S_GestionCatalogosClient : System.ServiceModel.ClientBase<Universidad.Controlador.SvcGestionCatalogos.IS_GestionCatalogos>, Universidad.Controlador.SvcGestionCatalogos.IS_GestionCatalogos {
        
        public S_GestionCatalogosClient() {
        }
        
        public S_GestionCatalogosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public S_GestionCatalogosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public S_GestionCatalogosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public S_GestionCatalogosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.US_CAT_TIPO_USUARIO> ObtenTablaUsCatTipoUsuarios() {
            return base.Channel.ObtenTablaUsCatTipoUsuarios();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.US_CAT_TIPO_USUARIO>> ObtenTablaUsCatTipoUsuariosAsync() {
            return base.Channel.ObtenTablaUsCatTipoUsuariosAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.US_CAT_NIVEL_USUARIO> ObtenTablaUsCatNivelUsuario() {
            return base.Channel.ObtenTablaUsCatNivelUsuario();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.US_CAT_NIVEL_USUARIO>> ObtenTablaUsCatNivelUsuarioAsync() {
            return base.Channel.ObtenTablaUsCatNivelUsuarioAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.US_CAT_ESTATUS_USUARIO> ObtenTablaUsCatEstatusUsuario() {
            return base.Channel.ObtenTablaUsCatEstatusUsuario();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.US_CAT_ESTATUS_USUARIO>> ObtenTablaUsCatEstatusUsuarioAsync() {
            return base.Channel.ObtenTablaUsCatEstatusUsuarioAsync();
        }
        
        public Universidad.Entidades.US_CAT_TIPO_USUARIO ObtenCatTipoUsuario(int idTipoUsuario) {
            return base.Channel.ObtenCatTipoUsuario(idTipoUsuario);
        }
        
        public System.Threading.Tasks.Task<Universidad.Entidades.US_CAT_TIPO_USUARIO> ObtenCatTipoUsuarioAsync(int idTipoUsuario) {
            return base.Channel.ObtenCatTipoUsuarioAsync(idTipoUsuario);
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.PER_CAT_NACIONALIDAD> ObtenCatalogoNacionalidades() {
            return base.Channel.ObtenCatalogoNacionalidades();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.PER_CAT_NACIONALIDAD>> ObtenCatalogoNacionalidadesAsync() {
            return base.Channel.ObtenCatalogoNacionalidadesAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona() {
            return base.Channel.ObtenCatTipoPersona();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.PER_CAT_TIPO_PERSONA>> ObtenCatTipoPersonaAsync() {
            return base.Channel.ObtenCatTipoPersonaAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenColoniasPorCp(int codigoPostal) {
            return base.Channel.ObtenColoniasPorCp(codigoPostal);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS>> ObtenColoniasPorCpAsync(int codigoPostal) {
            return base.Channel.ObtenColoniasPorCpAsync(codigoPostal);
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_ESTADO> ObtenCatEstados() {
            return base.Channel.ObtenCatEstados();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_ESTADO>> ObtenCatEstadosAsync() {
            return base.Channel.ObtenCatEstadosAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO> ObtenMunicipios(int estado) {
            return base.Channel.ObtenMunicipios(estado);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO>> ObtenMunicipiosAsync(int estado) {
            return base.Channel.ObtenMunicipiosAsync(estado);
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio) {
            return base.Channel.ObtenColonias(estado, municipio);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS>> ObtenColoniasAsync(int estado, int municipio) {
            return base.Channel.ObtenColoniasAsync(estado, municipio);
        }
        
        public Universidad.Entidades.DIR_CAT_COLONIAS ObtenCodigoPostal(int estado, int municipio, int colonia) {
            return base.Channel.ObtenCodigoPostal(estado, municipio, colonia);
        }
        
        public System.Threading.Tasks.Task<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenCodigoPostalAsync(int estado, int municipio, int colonia) {
            return base.Channel.ObtenCodigoPostalAsync(estado, municipio, colonia);
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.Catalogos.ListasGenerica> ObtenTablasCatalogos() {
            return base.Channel.ObtenTablasCatalogos();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.Catalogos.ListasGenerica>> ObtenTablasCatalogosAsync() {
            return base.Channel.ObtenTablasCatalogosAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS> ObtenCatalogosColonias() {
            return base.Channel.ObtenCatalogosColonias();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_COLONIAS>> ObtenCatalogosColoniasAsync() {
            return base.Channel.ObtenCatalogosColoniasAsync();
        }
        
        public System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO> ObtenCatalogosMunicipios() {
            return base.Channel.ObtenCatalogosMunicipios();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Universidad.Entidades.DIR_CAT_DELG_MUNICIPIO>> ObtenCatalogosMunicipiosAsync() {
            return base.Channel.ObtenCatalogosMunicipiosAsync();
        }
    }
}
