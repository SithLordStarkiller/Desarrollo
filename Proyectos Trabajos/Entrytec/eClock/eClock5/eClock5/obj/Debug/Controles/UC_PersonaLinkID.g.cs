﻿#pragma checksum "..\..\..\Controles\UC_PersonaLinkID.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AC352F4EB438E3A9C7C98941D9C23C7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34209
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace eClock5.Controles {
    
    
    /// <summary>
    /// UC_PersonaLinkID
    /// </summary>
    public partial class UC_PersonaLinkID : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Controles\UC_PersonaLinkID.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tbx_PersonaLinkID;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Controles\UC_PersonaLinkID.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tbx_PersonaNombre;
        
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
            System.Uri resourceLocater = new System.Uri("/eClock5;component/controles/uc_personalinkid.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controles\UC_PersonaLinkID.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 7 "..\..\..\Controles\UC_PersonaLinkID.xaml"
            ((eClock5.Controles.UC_PersonaLinkID)(target)).Initialized += new System.EventHandler(this.UserControl_Initialized);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\Controles\UC_PersonaLinkID.xaml"
            ((eClock5.Controles.UC_PersonaLinkID)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Tbx_PersonaLinkID = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\Controles\UC_PersonaLinkID.xaml"
            this.Tbx_PersonaLinkID.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Tbx_PersonaLinkID_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Tbx_PersonaNombre = ((System.Windows.Controls.TextBox)(target));
            
            #line 14 "..\..\..\Controles\UC_PersonaLinkID.xaml"
            this.Tbx_PersonaNombre.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Tbx_PersonaNombre_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

