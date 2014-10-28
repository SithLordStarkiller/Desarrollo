﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessApplication1.Web.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationErrorResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationErrorResources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BusinessApplication1.Web.Resources.ValidationErrorResources", typeof(ValidationErrorResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La respuesta de seguridad no puede tener más de 128 caracteres.
        /// </summary>
        public static string ValidationErrorBadAnswerLength {
            get {
                return ResourceManager.GetString("ValidationErrorBadAnswerLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El nombre descriptivo no puede tener más de 255 caracteres.
        /// </summary>
        public static string ValidationErrorBadFriendlyNameLength {
            get {
                return ResourceManager.GetString("ValidationErrorBadFriendlyNameLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a La contraseña debe tener 7 caracteres como mínimo y 50 como máximo.
        /// </summary>
        public static string ValidationErrorBadPasswordLength {
            get {
                return ResourceManager.GetString("ValidationErrorBadPasswordLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Una contraseña tiene que incluir al menos un carácter especial, por ejemplo, @ o #.
        /// </summary>
        public static string ValidationErrorBadPasswordStrength {
            get {
                return ResourceManager.GetString("ValidationErrorBadPasswordStrength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a El nombre de usuario debe tener 4 caracteres como mínimo y 255 como máximo.
        /// </summary>
        public static string ValidationErrorBadUserNameLength {
            get {
                return ResourceManager.GetString("ValidationErrorBadUserNameLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Correo electrónico no válido. Una dirección debe tener el formato user@company.com.
        /// </summary>
        public static string ValidationErrorInvalidEmail {
            get {
                return ResourceManager.GetString("ValidationErrorInvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nombre de usuario no válido. Tiene que incluir solo caracteres alfanuméricos.
        /// </summary>
        public static string ValidationErrorInvalidUserName {
            get {
                return ResourceManager.GetString("ValidationErrorInvalidUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Las contraseñas no coinciden.
        /// </summary>
        public static string ValidationErrorPasswordConfirmationMismatch {
            get {
                return ResourceManager.GetString("ValidationErrorPasswordConfirmationMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Este campo es obligatorio.
        /// </summary>
        public static string ValidationErrorRequiredField {
            get {
                return ResourceManager.GetString("ValidationErrorRequiredField", resourceCulture);
            }
        }
    }
}