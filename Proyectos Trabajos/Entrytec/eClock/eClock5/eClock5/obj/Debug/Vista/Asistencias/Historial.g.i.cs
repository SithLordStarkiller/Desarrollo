﻿#pragma checksum "..\..\..\..\Vista\Asistencias\Historial.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D73C5A0F9D1BECA3840984BBAB0AA74C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using eClock5;
using eClock5.Controles;


namespace eClock5.Vista.Asistencias {
    
    
    /// <summary>
    /// Historial
    /// </summary>
    public partial class Historial : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 11 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal eClock5.UC_ToolBar TbarFP;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal eClock5.Controles.UC_RangoFechas RangoFechas;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Lvw_Datos;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_Seleccionado;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_PERSONA_LINK_ID;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_PERSONA_NOMBRE;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_ALMACEN_INC_FECHA;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_TIPO_INCIDENCIA_NOMBRE;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_ALMACEN_INC_NO;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_ALMACEN_INC_SALDO;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_TIPO_ALMACEN_INC_NOMBRE;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_ALMACEN_INC_COMEN;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_ALMACEN_INC_EXTRAS;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Vista\Asistencias\Historial.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn Gvc_ALMACEN_INC_ID;
        
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
            System.Uri resourceLocater = new System.Uri("/eClock5;component/vista/asistencias/historial.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Vista\Asistencias\Historial.xaml"
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
            this.TbarFP = ((eClock5.UC_ToolBar)(target));
            return;
            case 2:
            this.RangoFechas = ((eClock5.Controles.UC_RangoFechas)(target));
            return;
            case 3:
            this.Lvw_Datos = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.Gvc_Seleccionado = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            this.Gvc_PERSONA_LINK_ID = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 7:
            this.Gvc_PERSONA_NOMBRE = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 8:
            this.Gvc_ALMACEN_INC_FECHA = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 9:
            this.Gvc_TIPO_INCIDENCIA_NOMBRE = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 10:
            this.Gvc_ALMACEN_INC_NO = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 11:
            this.Gvc_ALMACEN_INC_SALDO = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 12:
            this.Gvc_TIPO_ALMACEN_INC_NOMBRE = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 13:
            this.Gvc_ALMACEN_INC_COMEN = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 14:
            this.Gvc_ALMACEN_INC_EXTRAS = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 15:
            this.Gvc_ALMACEN_INC_ID = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 33 "..\..\..\..\Vista\Asistencias\Historial.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\..\Vista\Asistencias\Historial.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Checked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

