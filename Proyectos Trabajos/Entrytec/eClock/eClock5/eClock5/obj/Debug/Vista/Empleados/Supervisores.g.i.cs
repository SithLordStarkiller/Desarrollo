﻿#pragma checksum "..\..\..\..\Vista\Empleados\Supervisores.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7DCEF39465B58E070E9424AF783717BC"
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


namespace eClock5.Vista.Empleados {
    
    
    /// <summary>
    /// Supervisores
    /// </summary>
    public partial class Supervisores : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Lbl_Usuario;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tbx_Usuario;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Lbl_TipoPermiso;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal eClock5.Controles.UC_Combo Cmb_Permisos;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Otorgar;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Quitar;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal eClock5.UC_Listado Lst_Main;
        
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
            System.Uri resourceLocater = new System.Uri("/eClock5;component/vista/empleados/supervisores.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
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
            
            #line 2 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
            ((eClock5.Vista.Empleados.Supervisores)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            
            #line 2 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
            ((eClock5.Vista.Empleados.Supervisores)(target)).IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UserControl_IsVisibleChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Lbl_Usuario = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Tbx_Usuario = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Lbl_TipoPermiso = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Cmb_Permisos = ((eClock5.Controles.UC_Combo)(target));
            return;
            case 6:
            this.Btn_Otorgar = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
            this.Btn_Otorgar.Click += new System.Windows.RoutedEventHandler(this.Btn_Otorgar_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Btn_Quitar = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\Vista\Empleados\Supervisores.xaml"
            this.Btn_Quitar.Click += new System.Windows.RoutedEventHandler(this.Btn_Quitar_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Lst_Main = ((eClock5.UC_Listado)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

