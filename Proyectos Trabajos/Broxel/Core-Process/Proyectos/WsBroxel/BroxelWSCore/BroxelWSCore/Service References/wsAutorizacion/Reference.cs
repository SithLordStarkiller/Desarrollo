﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BroxelWSCore.wsAutorizacion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://127.0.0.1:8280/services/Autorizacion/Autorizacion.wsdl", ConfigurationName="wsAutorizacion.AutorizacionPortType")]
    public interface AutorizacionPortType {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://127.0.0.1:8280/services/Autorizacion/Autorizacion.wsdl/AutorizacionPortTyp" +
            "e/AutorizarRequest", ReplyAction="http://127.0.0.1:8280/services/Autorizacion/Autorizacion.wsdl/AutorizacionPortTyp" +
            "e/AutorizarResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        BroxelWSCore.wsAutorizacion.AutorizarResponse Autorizar(BroxelWSCore.wsAutorizacion.AutorizarRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://127.0.0.1:8280/services/Autorizacion/Autorizacion.wsdl/AutorizacionPortTyp" +
            "e/AutorizarRequest", ReplyAction="http://127.0.0.1:8280/services/Autorizacion/Autorizacion.wsdl/AutorizacionPortTyp" +
            "e/AutorizarResponse")]
        System.Threading.Tasks.Task<BroxelWSCore.wsAutorizacion.AutorizarResponse> AutorizarAsync(BroxelWSCore.wsAutorizacion.AutorizarRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Autorizar", WrapperNamespace="http://org.apache.synapse/xsd", IsWrapped=true)]
    public partial class AutorizarRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DatosPrivados;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=1)]
        public string Canal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=2)]
        public string TipoTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=3)]
        public string SecuenciaTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=4)]
        public string FechaTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=5)]
        public string HoraTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=6)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Comercio;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=7)]
        public string Terminal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=8)]
        public string ModoIngreso;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=9)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Tarjeta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=10)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FechaExpiracion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=11)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoSeguridad;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=12)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TrackI;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=13)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TrackII;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=14)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string PIN;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=15)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TipoCuenta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=16)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoMoneda;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=17)]
        public string Importe;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=18)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Plan;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=19)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Cuotas;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=20)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Ticket;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=21)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FechaDiferimiento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=22)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ImporteAdicional;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=23)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoAutorizacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=24)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoAutorizacionOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=25)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FechaTransaccionOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=26)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string HoraTransaccionOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=27)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TicketOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=28)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SecuenciaOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=29)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IDTransaccionOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=30)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IDTransaccionExterno;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=31)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IDTransaccionExternoOriginal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=32)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NuevoPIN;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=33)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TarjetaDestino;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=34)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TipoCodificacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=35)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DatosAdicionales;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=36)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SubTipoTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=37)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FeeTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=38)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FeeCompensacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=39)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FeeProcesamientoTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=40)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FeeProcesamientoCompensacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=41)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InstitucionAdquirente;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=42)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string InstitucionReenvio;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=43)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TipoComercio;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=44)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string NombreComercio;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=45)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Retrieval;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=46)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ImporteMonedaCompensacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=47)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ImporteMonedaLiquidacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=48)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoMonedaCompensacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=49)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoMonedaLiquidacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=50)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoPaisAdquirente;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=51)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoSeguridadBanda;
        
        public AutorizarRequest() {
        }
        
        public AutorizarRequest(
                    string DatosPrivados, 
                    string Canal, 
                    string TipoTransaccion, 
                    string SecuenciaTransaccion, 
                    string FechaTransaccion, 
                    string HoraTransaccion, 
                    string Comercio, 
                    string Terminal, 
                    string ModoIngreso, 
                    string Tarjeta, 
                    string FechaExpiracion, 
                    string CodigoSeguridad, 
                    string TrackI, 
                    string TrackII, 
                    string PIN, 
                    string TipoCuenta, 
                    string CodigoMoneda, 
                    string Importe, 
                    string Plan, 
                    string Cuotas, 
                    string Ticket, 
                    string FechaDiferimiento, 
                    string ImporteAdicional, 
                    string CodigoAutorizacion, 
                    string CodigoAutorizacionOriginal, 
                    string FechaTransaccionOriginal, 
                    string HoraTransaccionOriginal, 
                    string TicketOriginal, 
                    string SecuenciaOriginal, 
                    string IDTransaccionOriginal, 
                    string IDTransaccionExterno, 
                    string IDTransaccionExternoOriginal, 
                    string NuevoPIN, 
                    string TarjetaDestino, 
                    string TipoCodificacion, 
                    string DatosAdicionales, 
                    string SubTipoTransaccion, 
                    string FeeTransaccion, 
                    string FeeCompensacion, 
                    string FeeProcesamientoTransaccion, 
                    string FeeProcesamientoCompensacion, 
                    string InstitucionAdquirente, 
                    string InstitucionReenvio, 
                    string TipoComercio, 
                    string NombreComercio, 
                    string Retrieval, 
                    string ImporteMonedaCompensacion, 
                    string ImporteMonedaLiquidacion, 
                    string CodigoMonedaCompensacion, 
                    string CodigoMonedaLiquidacion, 
                    string CodigoPaisAdquirente, 
                    string CodigoSeguridadBanda) {
            this.DatosPrivados = DatosPrivados;
            this.Canal = Canal;
            this.TipoTransaccion = TipoTransaccion;
            this.SecuenciaTransaccion = SecuenciaTransaccion;
            this.FechaTransaccion = FechaTransaccion;
            this.HoraTransaccion = HoraTransaccion;
            this.Comercio = Comercio;
            this.Terminal = Terminal;
            this.ModoIngreso = ModoIngreso;
            this.Tarjeta = Tarjeta;
            this.FechaExpiracion = FechaExpiracion;
            this.CodigoSeguridad = CodigoSeguridad;
            this.TrackI = TrackI;
            this.TrackII = TrackII;
            this.PIN = PIN;
            this.TipoCuenta = TipoCuenta;
            this.CodigoMoneda = CodigoMoneda;
            this.Importe = Importe;
            this.Plan = Plan;
            this.Cuotas = Cuotas;
            this.Ticket = Ticket;
            this.FechaDiferimiento = FechaDiferimiento;
            this.ImporteAdicional = ImporteAdicional;
            this.CodigoAutorizacion = CodigoAutorizacion;
            this.CodigoAutorizacionOriginal = CodigoAutorizacionOriginal;
            this.FechaTransaccionOriginal = FechaTransaccionOriginal;
            this.HoraTransaccionOriginal = HoraTransaccionOriginal;
            this.TicketOriginal = TicketOriginal;
            this.SecuenciaOriginal = SecuenciaOriginal;
            this.IDTransaccionOriginal = IDTransaccionOriginal;
            this.IDTransaccionExterno = IDTransaccionExterno;
            this.IDTransaccionExternoOriginal = IDTransaccionExternoOriginal;
            this.NuevoPIN = NuevoPIN;
            this.TarjetaDestino = TarjetaDestino;
            this.TipoCodificacion = TipoCodificacion;
            this.DatosAdicionales = DatosAdicionales;
            this.SubTipoTransaccion = SubTipoTransaccion;
            this.FeeTransaccion = FeeTransaccion;
            this.FeeCompensacion = FeeCompensacion;
            this.FeeProcesamientoTransaccion = FeeProcesamientoTransaccion;
            this.FeeProcesamientoCompensacion = FeeProcesamientoCompensacion;
            this.InstitucionAdquirente = InstitucionAdquirente;
            this.InstitucionReenvio = InstitucionReenvio;
            this.TipoComercio = TipoComercio;
            this.NombreComercio = NombreComercio;
            this.Retrieval = Retrieval;
            this.ImporteMonedaCompensacion = ImporteMonedaCompensacion;
            this.ImporteMonedaLiquidacion = ImporteMonedaLiquidacion;
            this.CodigoMonedaCompensacion = CodigoMonedaCompensacion;
            this.CodigoMonedaLiquidacion = CodigoMonedaLiquidacion;
            this.CodigoPaisAdquirente = CodigoPaisAdquirente;
            this.CodigoSeguridadBanda = CodigoSeguridadBanda;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RespuestaAutorizacion", WrapperNamespace="http://org.apache.synapse/xsd", IsWrapped=true)]
    public partial class AutorizarResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=0)]
        public int CodigoError;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string MensajeError;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DatosPrivados;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=4)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string MensajeRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=5)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CategoriaRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=6)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string AccionRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=7)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CodigoAutorizacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=8)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IDTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=9)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string IDTransaccionExterno;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=10)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DisponibleConsumos;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=11)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DisponibleCuotas;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=12)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DisponibleAdelantos;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=13)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DisponiblePrestamos;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=14)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Saldo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=15)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SaldoEnDolares;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=16)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string PagoMinimo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=17)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DeudaAlDia;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=18)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string FechaVencimientoUltimaLiquidacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=19)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Movimientos;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=20)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TipoCodificacion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://org.apache.synapse/xsd", Order=21)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string DatosAdicionales;
        
        public AutorizarResponse() {
        }
        
        public AutorizarResponse(
                    int CodigoError, 
                    string MensajeError, 
                    string DatosPrivados, 
                    string CodigoRespuesta, 
                    string MensajeRespuesta, 
                    string CategoriaRespuesta, 
                    string AccionRespuesta, 
                    string CodigoAutorizacion, 
                    string IDTransaccion, 
                    string IDTransaccionExterno, 
                    string DisponibleConsumos, 
                    string DisponibleCuotas, 
                    string DisponibleAdelantos, 
                    string DisponiblePrestamos, 
                    string Saldo, 
                    string SaldoEnDolares, 
                    string PagoMinimo, 
                    string DeudaAlDia, 
                    string FechaVencimientoUltimaLiquidacion, 
                    string Movimientos, 
                    string TipoCodificacion, 
                    string DatosAdicionales) {
            this.CodigoError = CodigoError;
            this.MensajeError = MensajeError;
            this.DatosPrivados = DatosPrivados;
            this.CodigoRespuesta = CodigoRespuesta;
            this.MensajeRespuesta = MensajeRespuesta;
            this.CategoriaRespuesta = CategoriaRespuesta;
            this.AccionRespuesta = AccionRespuesta;
            this.CodigoAutorizacion = CodigoAutorizacion;
            this.IDTransaccion = IDTransaccion;
            this.IDTransaccionExterno = IDTransaccionExterno;
            this.DisponibleConsumos = DisponibleConsumos;
            this.DisponibleCuotas = DisponibleCuotas;
            this.DisponibleAdelantos = DisponibleAdelantos;
            this.DisponiblePrestamos = DisponiblePrestamos;
            this.Saldo = Saldo;
            this.SaldoEnDolares = SaldoEnDolares;
            this.PagoMinimo = PagoMinimo;
            this.DeudaAlDia = DeudaAlDia;
            this.FechaVencimientoUltimaLiquidacion = FechaVencimientoUltimaLiquidacion;
            this.Movimientos = Movimientos;
            this.TipoCodificacion = TipoCodificacion;
            this.DatosAdicionales = DatosAdicionales;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AutorizacionPortTypeChannel : BroxelWSCore.wsAutorizacion.AutorizacionPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AutorizacionPortTypeClient : System.ServiceModel.ClientBase<BroxelWSCore.wsAutorizacion.AutorizacionPortType>, BroxelWSCore.wsAutorizacion.AutorizacionPortType {
        
        public AutorizacionPortTypeClient() {
        }
        
        public AutorizacionPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AutorizacionPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AutorizacionPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AutorizacionPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BroxelWSCore.wsAutorizacion.AutorizarResponse BroxelWSCore.wsAutorizacion.AutorizacionPortType.Autorizar(BroxelWSCore.wsAutorizacion.AutorizarRequest request) {
            return base.Channel.Autorizar(request);
        }
        
        public int Autorizar(
                    ref string DatosPrivados, 
                    string Canal, 
                    string TipoTransaccion, 
                    string SecuenciaTransaccion, 
                    string FechaTransaccion, 
                    string HoraTransaccion, 
                    string Comercio, 
                    string Terminal, 
                    string ModoIngreso, 
                    string Tarjeta, 
                    string FechaExpiracion, 
                    string CodigoSeguridad, 
                    string TrackI, 
                    string TrackII, 
                    string PIN, 
                    string TipoCuenta, 
                    string CodigoMoneda, 
                    string Importe, 
                    string Plan, 
                    string Cuotas, 
                    string Ticket, 
                    string FechaDiferimiento, 
                    string ImporteAdicional, 
                    ref string CodigoAutorizacion, 
                    string CodigoAutorizacionOriginal, 
                    string FechaTransaccionOriginal, 
                    string HoraTransaccionOriginal, 
                    string TicketOriginal, 
                    string SecuenciaOriginal, 
                    string IDTransaccionOriginal, 
                    ref string IDTransaccionExterno, 
                    string IDTransaccionExternoOriginal, 
                    string NuevoPIN, 
                    string TarjetaDestino, 
                    ref string TipoCodificacion, 
                    ref string DatosAdicionales, 
                    string SubTipoTransaccion, 
                    string FeeTransaccion, 
                    string FeeCompensacion, 
                    string FeeProcesamientoTransaccion, 
                    string FeeProcesamientoCompensacion, 
                    string InstitucionAdquirente, 
                    string InstitucionReenvio, 
                    string TipoComercio, 
                    string NombreComercio, 
                    string Retrieval, 
                    string ImporteMonedaCompensacion, 
                    string ImporteMonedaLiquidacion, 
                    string CodigoMonedaCompensacion, 
                    string CodigoMonedaLiquidacion, 
                    string CodigoPaisAdquirente, 
                    string CodigoSeguridadBanda, 
                    out string MensajeError, 
                    out string CodigoRespuesta, 
                    out string MensajeRespuesta, 
                    out string CategoriaRespuesta, 
                    out string AccionRespuesta, 
                    out string IDTransaccion, 
                    out string DisponibleConsumos, 
                    out string DisponibleCuotas, 
                    out string DisponibleAdelantos, 
                    out string DisponiblePrestamos, 
                    out string Saldo, 
                    out string SaldoEnDolares, 
                    out string PagoMinimo, 
                    out string DeudaAlDia, 
                    out string FechaVencimientoUltimaLiquidacion, 
                    out string Movimientos) {
            BroxelWSCore.wsAutorizacion.AutorizarRequest inValue = new BroxelWSCore.wsAutorizacion.AutorizarRequest();
            inValue.DatosPrivados = DatosPrivados;
            inValue.Canal = Canal;
            inValue.TipoTransaccion = TipoTransaccion;
            inValue.SecuenciaTransaccion = SecuenciaTransaccion;
            inValue.FechaTransaccion = FechaTransaccion;
            inValue.HoraTransaccion = HoraTransaccion;
            inValue.Comercio = Comercio;
            inValue.Terminal = Terminal;
            inValue.ModoIngreso = ModoIngreso;
            inValue.Tarjeta = Tarjeta;
            inValue.FechaExpiracion = FechaExpiracion;
            inValue.CodigoSeguridad = CodigoSeguridad;
            inValue.TrackI = TrackI;
            inValue.TrackII = TrackII;
            inValue.PIN = PIN;
            inValue.TipoCuenta = TipoCuenta;
            inValue.CodigoMoneda = CodigoMoneda;
            inValue.Importe = Importe;
            inValue.Plan = Plan;
            inValue.Cuotas = Cuotas;
            inValue.Ticket = Ticket;
            inValue.FechaDiferimiento = FechaDiferimiento;
            inValue.ImporteAdicional = ImporteAdicional;
            inValue.CodigoAutorizacion = CodigoAutorizacion;
            inValue.CodigoAutorizacionOriginal = CodigoAutorizacionOriginal;
            inValue.FechaTransaccionOriginal = FechaTransaccionOriginal;
            inValue.HoraTransaccionOriginal = HoraTransaccionOriginal;
            inValue.TicketOriginal = TicketOriginal;
            inValue.SecuenciaOriginal = SecuenciaOriginal;
            inValue.IDTransaccionOriginal = IDTransaccionOriginal;
            inValue.IDTransaccionExterno = IDTransaccionExterno;
            inValue.IDTransaccionExternoOriginal = IDTransaccionExternoOriginal;
            inValue.NuevoPIN = NuevoPIN;
            inValue.TarjetaDestino = TarjetaDestino;
            inValue.TipoCodificacion = TipoCodificacion;
            inValue.DatosAdicionales = DatosAdicionales;
            inValue.SubTipoTransaccion = SubTipoTransaccion;
            inValue.FeeTransaccion = FeeTransaccion;
            inValue.FeeCompensacion = FeeCompensacion;
            inValue.FeeProcesamientoTransaccion = FeeProcesamientoTransaccion;
            inValue.FeeProcesamientoCompensacion = FeeProcesamientoCompensacion;
            inValue.InstitucionAdquirente = InstitucionAdquirente;
            inValue.InstitucionReenvio = InstitucionReenvio;
            inValue.TipoComercio = TipoComercio;
            inValue.NombreComercio = NombreComercio;
            inValue.Retrieval = Retrieval;
            inValue.ImporteMonedaCompensacion = ImporteMonedaCompensacion;
            inValue.ImporteMonedaLiquidacion = ImporteMonedaLiquidacion;
            inValue.CodigoMonedaCompensacion = CodigoMonedaCompensacion;
            inValue.CodigoMonedaLiquidacion = CodigoMonedaLiquidacion;
            inValue.CodigoPaisAdquirente = CodigoPaisAdquirente;
            inValue.CodigoSeguridadBanda = CodigoSeguridadBanda;
            BroxelWSCore.wsAutorizacion.AutorizarResponse retVal = ((BroxelWSCore.wsAutorizacion.AutorizacionPortType)(this)).Autorizar(inValue);
            MensajeError = retVal.MensajeError;
            DatosPrivados = retVal.DatosPrivados;
            CodigoRespuesta = retVal.CodigoRespuesta;
            MensajeRespuesta = retVal.MensajeRespuesta;
            CategoriaRespuesta = retVal.CategoriaRespuesta;
            AccionRespuesta = retVal.AccionRespuesta;
            CodigoAutorizacion = retVal.CodigoAutorizacion;
            IDTransaccion = retVal.IDTransaccion;
            IDTransaccionExterno = retVal.IDTransaccionExterno;
            DisponibleConsumos = retVal.DisponibleConsumos;
            DisponibleCuotas = retVal.DisponibleCuotas;
            DisponibleAdelantos = retVal.DisponibleAdelantos;
            DisponiblePrestamos = retVal.DisponiblePrestamos;
            Saldo = retVal.Saldo;
            SaldoEnDolares = retVal.SaldoEnDolares;
            PagoMinimo = retVal.PagoMinimo;
            DeudaAlDia = retVal.DeudaAlDia;
            FechaVencimientoUltimaLiquidacion = retVal.FechaVencimientoUltimaLiquidacion;
            Movimientos = retVal.Movimientos;
            TipoCodificacion = retVal.TipoCodificacion;
            DatosAdicionales = retVal.DatosAdicionales;
            return retVal.CodigoError;
        }
        
        public System.Threading.Tasks.Task<BroxelWSCore.wsAutorizacion.AutorizarResponse> AutorizarAsync(BroxelWSCore.wsAutorizacion.AutorizarRequest request) {
            return base.Channel.AutorizarAsync(request);
        }
    }
}
