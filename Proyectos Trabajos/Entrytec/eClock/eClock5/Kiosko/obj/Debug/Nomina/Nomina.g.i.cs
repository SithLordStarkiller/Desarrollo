﻿#pragma checksum "..\..\..\Nomina\Nomina.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1F8B7E02C018BA020D01C5253C26AA3E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Kiosko.Controles;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using eClock5;


namespace Kiosko.Nomina {
    
    
    /// <summary>
    /// Nomina
    /// </summary>
    public partial class Nomina : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Nomina\Nomina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal eClock5.UC_ToolBar ToolBar;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Nomina\Nomina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Lbl_Contrasena;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Nomina\Nomina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Tbx_Contrasena;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Nomina\Nomina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Lbl_ContratoNomina;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Nomina\Nomina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Kiosko.Controles.UC_Teclado Teclado;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Kiosco;component/nomina/nomina.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Nomina\Nomina.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\Nomina\Nomina.xaml"
            ((Kiosko.Nomina.Nomina)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded_2);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ToolBar = ((eClock5.UC_ToolBar)(target));
            return;
            case 3:
            this.Lbl_Contrasena = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Tbx_Contrasena = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.Lbl_ContratoNomina = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Teclado = ((Kiosko.Controles.UC_Teclado)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

