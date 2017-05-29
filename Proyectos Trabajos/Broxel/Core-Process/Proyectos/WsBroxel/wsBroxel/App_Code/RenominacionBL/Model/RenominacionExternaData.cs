using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RenominacionBL.Model
{
    /// <summary>
    /// Datos para Realizar Renominación Externa.
    /// </summary>
    public class RenominacionExternaData
    {
        /// <summary>
        /// Nombre Completo de la persona física.
        /// </summary>
        public string NombreCompleto { set; get; }
        /// <summary>
        /// Genero de la persona física: M-Masculino, F-Femenino.
        /// </summary>
        public string Genero { set; get; }
        /// <summary>
        /// Nombre de la calle del domicilio.
        /// </summary>
        public string Calle { set; get; }
        /// <summary>
        /// Numero exterior del domicilio.
        /// </summary>
        public string NumExterior { set; get; }
        /// <summary>
        /// Numero interior del domicilio.
        /// </summary>
        public string NumInterior { set; get; }
        /// <summary>
        /// Colionia del domicilio.
        /// </summary>
        public string Colonia { set; get; }
        /// <summary>
        /// Estado del domicilio.
        /// </summary>
        public string Estado { set; get; }
        /// <summary>
        /// Municipio del domicilio.
        /// </summary>
        public string Municipio { set; get; }
        /// <summary>
        /// Código Postal del domicilio.
        /// </summary>
        public string CodigoPostal { set; get; }
        /// <summary>
        /// Número celular de 10 dígitos perteneciente a la persona física. 
        /// </summary>
        public string TelefonoMovil { set; get; }
        /// <summary>
        /// Número de cuenta de la persona física.
        /// </summary>
        public  string NumeroCuenta { set; get; }
        /// <summary>
        /// Fecha de nacimiento de la persona física. 
        /// </summary>
        public DateTime FechaNacimiento { set; get; }
        /// <summary>
        /// RFC de la persona física. 
        /// </summary>
        public string Rfc { set; get; }
        /// <summary>
        /// CURP de la persona física.
        /// </summary>
        public string Curp { set; get; }
        /// <summary>
        /// Número de seguro social de la persona física.
        /// </summary>
        public string Nss { set; get; }
        /// <summary>
        /// Correo electrónico de la persona física.
        /// </summary>
        public string Email { set; get; }
        /// <summary>
        /// Estado Civil de la persona física: S-Soltero, C-Casado.
        /// </summary>
        public string EstadoCivil { set; get; }
        /// <summary>
        /// Tiene hijos de la persona física: S-Si, N-No.
        /// </summary>
        public string TieneHijos { set; get; }
        /// <summary>
        /// Campo univoco de la persona física.
        /// </summary>
        public string CampoUnivoco { set; get; }
    }
}