﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.42
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=2.0.50727.42.
// 
#pragma warning disable 1591

namespace eClock.eClock {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WS_ServicioRDatosSoap", Namespace="IDOSOFT/eClock/FARMACOS/170983")]
    public partial class WS_ServicioRDatos : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback NombreTurnoOperationCompleted;
        
        private System.Threading.SendOrPostCallback NombreTipoTurnoOperationCompleted;
        
        private System.Threading.SendOrPostCallback RecepcionDatosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ObtenerIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback GuardarRegistrosOperationCompleted;
        
        private System.Threading.SendOrPostCallback Borrador_RegOperationCompleted;
        
        private System.Threading.SendOrPostCallback GuardarRegistros_F2OperationCompleted;
        
        private System.Threading.SendOrPostCallback Mandar_CadenaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WS_ServicioRDatos() {
            this.Url = "http://localhost/eClock/WS_ServicioRDatos.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event NombreTurnoCompletedEventHandler NombreTurnoCompleted;
        
        /// <remarks/>
        public event NombreTipoTurnoCompletedEventHandler NombreTipoTurnoCompleted;
        
        /// <remarks/>
        public event RecepcionDatosCompletedEventHandler RecepcionDatosCompleted;
        
        /// <remarks/>
        public event ObtenerIDCompletedEventHandler ObtenerIDCompleted;
        
        /// <remarks/>
        public event GuardarRegistrosCompletedEventHandler GuardarRegistrosCompleted;
        
        /// <remarks/>
        public event Borrador_RegCompletedEventHandler Borrador_RegCompleted;
        
        /// <remarks/>
        public event GuardarRegistros_F2CompletedEventHandler GuardarRegistros_F2Completed;
        
        /// <remarks/>
        public event Mandar_CadenaCompletedEventHandler Mandar_CadenaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/NombreTurno", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string NombreTurno() {
            object[] results = this.Invoke("NombreTurno", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginNombreTurno(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("NombreTurno", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndNombreTurno(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void NombreTurnoAsync() {
            this.NombreTurnoAsync(null);
        }
        
        /// <remarks/>
        public void NombreTurnoAsync(object userState) {
            if ((this.NombreTurnoOperationCompleted == null)) {
                this.NombreTurnoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNombreTurnoOperationCompleted);
            }
            this.InvokeAsync("NombreTurno", new object[0], this.NombreTurnoOperationCompleted, userState);
        }
        
        private void OnNombreTurnoOperationCompleted(object arg) {
            if ((this.NombreTurnoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NombreTurnoCompleted(this, new NombreTurnoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/NombreTipoTurno", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string NombreTipoTurno() {
            object[] results = this.Invoke("NombreTipoTurno", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginNombreTipoTurno(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("NombreTipoTurno", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndNombreTipoTurno(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void NombreTipoTurnoAsync() {
            this.NombreTipoTurnoAsync(null);
        }
        
        /// <remarks/>
        public void NombreTipoTurnoAsync(object userState) {
            if ((this.NombreTipoTurnoOperationCompleted == null)) {
                this.NombreTipoTurnoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNombreTipoTurnoOperationCompleted);
            }
            this.InvokeAsync("NombreTipoTurno", new object[0], this.NombreTipoTurnoOperationCompleted, userState);
        }
        
        private void OnNombreTipoTurnoOperationCompleted(object arg) {
            if ((this.NombreTipoTurnoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NombreTipoTurnoCompleted(this, new NombreTipoTurnoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/RecepcionDatos", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RecepcionDatos(string Dato) {
            object[] results = this.Invoke("RecepcionDatos", new object[] {
                        Dato});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRecepcionDatos(string Dato, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("RecepcionDatos", new object[] {
                        Dato}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndRecepcionDatos(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RecepcionDatosAsync(string Dato) {
            this.RecepcionDatosAsync(Dato, null);
        }
        
        /// <remarks/>
        public void RecepcionDatosAsync(string Dato, object userState) {
            if ((this.RecepcionDatosOperationCompleted == null)) {
                this.RecepcionDatosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecepcionDatosOperationCompleted);
            }
            this.InvokeAsync("RecepcionDatos", new object[] {
                        Dato}, this.RecepcionDatosOperationCompleted, userState);
        }
        
        private void OnRecepcionDatosOperationCompleted(object arg) {
            if ((this.RecepcionDatosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecepcionDatosCompleted(this, new RecepcionDatosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/ObtenerID", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int ObtenerID() {
            object[] results = this.Invoke("ObtenerID", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginObtenerID(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ObtenerID", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public int EndObtenerID(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void ObtenerIDAsync() {
            this.ObtenerIDAsync(null);
        }
        
        /// <remarks/>
        public void ObtenerIDAsync(object userState) {
            if ((this.ObtenerIDOperationCompleted == null)) {
                this.ObtenerIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnObtenerIDOperationCompleted);
            }
            this.InvokeAsync("ObtenerID", new object[0], this.ObtenerIDOperationCompleted, userState);
        }
        
        private void OnObtenerIDOperationCompleted(object arg) {
            if ((this.ObtenerIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ObtenerIDCompleted(this, new ObtenerIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/GuardarRegistros", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GuardarRegistros(string Datos) {
            object[] results = this.Invoke("GuardarRegistros", new object[] {
                        Datos});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGuardarRegistros(string Datos, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GuardarRegistros", new object[] {
                        Datos}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGuardarRegistros(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GuardarRegistrosAsync(string Datos) {
            this.GuardarRegistrosAsync(Datos, null);
        }
        
        /// <remarks/>
        public void GuardarRegistrosAsync(string Datos, object userState) {
            if ((this.GuardarRegistrosOperationCompleted == null)) {
                this.GuardarRegistrosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGuardarRegistrosOperationCompleted);
            }
            this.InvokeAsync("GuardarRegistros", new object[] {
                        Datos}, this.GuardarRegistrosOperationCompleted, userState);
        }
        
        private void OnGuardarRegistrosOperationCompleted(object arg) {
            if ((this.GuardarRegistrosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GuardarRegistrosCompleted(this, new GuardarRegistrosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/Borrador_Reg", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Borrador_Reg(string Dia) {
            this.Invoke("Borrador_Reg", new object[] {
                        Dia});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginBorrador_Reg(string Dia, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Borrador_Reg", new object[] {
                        Dia}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndBorrador_Reg(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        public void Borrador_RegAsync(string Dia) {
            this.Borrador_RegAsync(Dia, null);
        }
        
        /// <remarks/>
        public void Borrador_RegAsync(string Dia, object userState) {
            if ((this.Borrador_RegOperationCompleted == null)) {
                this.Borrador_RegOperationCompleted = new System.Threading.SendOrPostCallback(this.OnBorrador_RegOperationCompleted);
            }
            this.InvokeAsync("Borrador_Reg", new object[] {
                        Dia}, this.Borrador_RegOperationCompleted, userState);
        }
        
        private void OnBorrador_RegOperationCompleted(object arg) {
            if ((this.Borrador_RegCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Borrador_RegCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/GuardarRegistros_F2", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GuardarRegistros_F2(string Datos) {
            object[] results = this.Invoke("GuardarRegistros_F2", new object[] {
                        Datos});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGuardarRegistros_F2(string Datos, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GuardarRegistros_F2", new object[] {
                        Datos}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGuardarRegistros_F2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GuardarRegistros_F2Async(string Datos) {
            this.GuardarRegistros_F2Async(Datos, null);
        }
        
        /// <remarks/>
        public void GuardarRegistros_F2Async(string Datos, object userState) {
            if ((this.GuardarRegistros_F2OperationCompleted == null)) {
                this.GuardarRegistros_F2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnGuardarRegistros_F2OperationCompleted);
            }
            this.InvokeAsync("GuardarRegistros_F2", new object[] {
                        Datos}, this.GuardarRegistros_F2OperationCompleted, userState);
        }
        
        private void OnGuardarRegistros_F2OperationCompleted(object arg) {
            if ((this.GuardarRegistros_F2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GuardarRegistros_F2Completed(this, new GuardarRegistros_F2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("IDOSOFT/eClock/FARMACOS/170983/Mandar_Cadena", RequestNamespace="IDOSOFT/eClock/FARMACOS/170983", ResponseNamespace="IDOSOFT/eClock/FARMACOS/170983", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Mandar_Cadena() {
            object[] results = this.Invoke("Mandar_Cadena", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginMandar_Cadena(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Mandar_Cadena", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndMandar_Cadena(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void Mandar_CadenaAsync() {
            this.Mandar_CadenaAsync(null);
        }
        
        /// <remarks/>
        public void Mandar_CadenaAsync(object userState) {
            if ((this.Mandar_CadenaOperationCompleted == null)) {
                this.Mandar_CadenaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnMandar_CadenaOperationCompleted);
            }
            this.InvokeAsync("Mandar_Cadena", new object[0], this.Mandar_CadenaOperationCompleted, userState);
        }
        
        private void OnMandar_CadenaOperationCompleted(object arg) {
            if ((this.Mandar_CadenaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Mandar_CadenaCompleted(this, new Mandar_CadenaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void NombreTurnoCompletedEventHandler(object sender, NombreTurnoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NombreTurnoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NombreTurnoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void NombreTipoTurnoCompletedEventHandler(object sender, NombreTipoTurnoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NombreTipoTurnoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NombreTipoTurnoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void RecepcionDatosCompletedEventHandler(object sender, RecepcionDatosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecepcionDatosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RecepcionDatosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void ObtenerIDCompletedEventHandler(object sender, ObtenerIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ObtenerIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ObtenerIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GuardarRegistrosCompletedEventHandler(object sender, GuardarRegistrosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GuardarRegistrosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GuardarRegistrosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void Borrador_RegCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GuardarRegistros_F2CompletedEventHandler(object sender, GuardarRegistros_F2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GuardarRegistros_F2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GuardarRegistros_F2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void Mandar_CadenaCompletedEventHandler(object sender, Mandar_CadenaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Mandar_CadenaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Mandar_CadenaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591