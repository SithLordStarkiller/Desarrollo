﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.33440
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PubliPayments.Negocios.wsGestiones {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.infonavit.org.mx", ConfigurationName="wsGestiones.WSCSIBM")]
    public interface WSCSIBM {
        
        // CODEGEN: El parámetro 'array' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="insertaDatosReturn")]
        PubliPayments.Negocios.wsGestiones.insertaDatosResponse insertaDatos(PubliPayments.Negocios.wsGestiones.insertaDatosRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="insertaDatos", WrapperNamespace="http://ws.infonavit.org.mx", IsWrapped=true)]
    public partial class insertaDatosRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=0)]
        public string numCredito;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=1)]
        public string fechaCaptura;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=2)]
        public string idRuta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=3)]
        public string despacho;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=4)]
        public string usser;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=5)]
        public string alterUssr;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=6)]
        public string estado;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=7)]
        public string movil;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=8)]
        public int numMovimientos;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=9)]
        [System.Xml.Serialization.XmlElementAttribute("array")]
        public string[] array;
        
        public insertaDatosRequest() {
        }
        
        public insertaDatosRequest(string numCredito, string fechaCaptura, string idRuta, string despacho, string usser, string alterUssr, string estado, string movil, int numMovimientos, string[] array) {
            this.numCredito = numCredito;
            this.fechaCaptura = fechaCaptura;
            this.idRuta = idRuta;
            this.despacho = despacho;
            this.usser = usser;
            this.alterUssr = alterUssr;
            this.estado = estado;
            this.movil = movil;
            this.numMovimientos = numMovimientos;
            this.array = array;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="insertaDatosResponse", WrapperNamespace="http://ws.infonavit.org.mx", IsWrapped=true)]
    public partial class insertaDatosResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.infonavit.org.mx", Order=0)]
        public string insertaDatosReturn;
        
        public insertaDatosResponse() {
        }
        
        public insertaDatosResponse(string insertaDatosReturn) {
            this.insertaDatosReturn = insertaDatosReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSCSIBMChannel : PubliPayments.Negocios.wsGestiones.WSCSIBM, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSCSIBMClient : System.ServiceModel.ClientBase<PubliPayments.Negocios.wsGestiones.WSCSIBM>, PubliPayments.Negocios.wsGestiones.WSCSIBM {
        
        public WSCSIBMClient() {
        }
        
        public WSCSIBMClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSCSIBMClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSCSIBMClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSCSIBMClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PubliPayments.Negocios.wsGestiones.insertaDatosResponse PubliPayments.Negocios.wsGestiones.WSCSIBM.insertaDatos(PubliPayments.Negocios.wsGestiones.insertaDatosRequest request) {
            return base.Channel.insertaDatos(request);
        }
        
        public string insertaDatos(string numCredito, string fechaCaptura, string idRuta, string despacho, string usser, string alterUssr, string estado, string movil, int numMovimientos, string[] array) {
            PubliPayments.Negocios.wsGestiones.insertaDatosRequest inValue = new PubliPayments.Negocios.wsGestiones.insertaDatosRequest();
            inValue.numCredito = numCredito;
            inValue.fechaCaptura = fechaCaptura;
            inValue.idRuta = idRuta;
            inValue.despacho = despacho;
            inValue.usser = usser;
            inValue.alterUssr = alterUssr;
            inValue.estado = estado;
            inValue.movil = movil;
            inValue.numMovimientos = numMovimientos;
            inValue.array = array;
            PubliPayments.Negocios.wsGestiones.insertaDatosResponse retVal = ((PubliPayments.Negocios.wsGestiones.WSCSIBM)(this)).insertaDatos(inValue);
            return retVal.insertaDatosReturn;
        }
    }
}