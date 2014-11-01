﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.17929
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eClockSync5.ES_Sesion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ES_Sesion.S_Sesion")]
    public interface S_Sesion {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/DoWork", ReplyAction="urn:S_Sesion/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/CreaSesion", ReplyAction="urn:S_Sesion/CreaSesionResponse")]
        string CreaSesion(string Usuario, string Clave);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/ObtenUsuarioID", ReplyAction="urn:S_Sesion/ObtenUsuarioIDResponse")]
        int ObtenUsuarioID(string SesionSeguridad);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/ObtenSuscripcionID", ReplyAction="urn:S_Sesion/ObtenSuscripcionIDResponse")]
        int ObtenSuscripcionID(string SesionSeguridad);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/ObtenPerfilID", ReplyAction="urn:S_Sesion/ObtenPerfilIDResponse")]
        int ObtenPerfilID(string SesionSeguridad);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/CierraSesion", ReplyAction="urn:S_Sesion/CierraSesionResponse")]
        bool CierraSesion(string SesionSeguridad);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/ObtenListado", ReplyAction="urn:S_Sesion/ObtenListadoResponse")]
        System.Data.DataSet ObtenListado(string SesionSeguridad, int SuscripcionID, string NombreTabla, string CampoLlave, string CampoNombre, string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/ObtenDatos", ReplyAction="urn:S_Sesion/ObtenDatosResponse")]
        System.Data.DataTable ObtenDatos(string SesionSeguridad, string Tabla, string Llaves, System.Data.DataTable Modelo);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:S_Sesion/GuardaDatos", ReplyAction="urn:S_Sesion/GuardaDatosResponse")]
        int GuardaDatos(string SesionSeguridad, string Tabla, string Llaves, System.Data.DataTable Modelo, int SuscripcionID, bool EsNuevo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface S_SesionChannel : eClockSync5.ES_Sesion.S_Sesion, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class S_SesionClient : System.ServiceModel.ClientBase<eClockSync5.ES_Sesion.S_Sesion>, eClockSync5.ES_Sesion.S_Sesion {
        
        public S_SesionClient() {
        }
        
        public S_SesionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public S_SesionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public S_SesionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public S_SesionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public string CreaSesion(string Usuario, string Clave) {
            return base.Channel.CreaSesion(Usuario, Clave);
        }
        
        public int ObtenUsuarioID(string SesionSeguridad) {
            return base.Channel.ObtenUsuarioID(SesionSeguridad);
        }
        
        public int ObtenSuscripcionID(string SesionSeguridad) {
            return base.Channel.ObtenSuscripcionID(SesionSeguridad);
        }
        
        public int ObtenPerfilID(string SesionSeguridad) {
            return base.Channel.ObtenPerfilID(SesionSeguridad);
        }
        
        public bool CierraSesion(string SesionSeguridad) {
            return base.Channel.CierraSesion(SesionSeguridad);
        }
        
        public System.Data.DataSet ObtenListado(string SesionSeguridad, int SuscripcionID, string NombreTabla, string CampoLlave, string CampoNombre, string CampoDescripcion, string CampoImagen, bool MostrarBorrados, string OtroFiltro) {
            return base.Channel.ObtenListado(SesionSeguridad, SuscripcionID, NombreTabla, CampoLlave, CampoNombre, CampoDescripcion, CampoImagen, MostrarBorrados, OtroFiltro);
        }
        
        public System.Data.DataTable ObtenDatos(string SesionSeguridad, string Tabla, string Llaves, System.Data.DataTable Modelo) {
            return base.Channel.ObtenDatos(SesionSeguridad, Tabla, Llaves, Modelo);
        }
        
        public int GuardaDatos(string SesionSeguridad, string Tabla, string Llaves, System.Data.DataTable Modelo, int SuscripcionID, bool EsNuevo) {
            return base.Channel.GuardaDatos(SesionSeguridad, Tabla, Llaves, Modelo, SuscripcionID, EsNuevo);
        }
    }
}