using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RenominacionBL.Model
{
    /// <summary>
    /// Datos de OriginacionTarjetahabiente
    /// </summary>
    public class OriginacionData
    {
        public int IdOriginacion { set; get; }
        public string Nombre { set; get; }
        public string ApellidoPaterno { set; get; }
        public string ApellidoMaterno { set; get; }
        public string Genero { set; get; }
        public string Rfc { set; get; }
        public string Calle { set; get; }
        public string NumExterior { set; get; }
        public string NumInterior { set; get; }
        public string Colonia { set; get; }
        public string Municipio { set; get; }
        public string CodigoPostal { set; get; }
        public string MontoPrestamoNumero { set; get; }
        public string Consecutivo { set; get; }
        public string NumCuenta { set; get; }
        public string NumTarjeta { set; get; }
        public string TelefonoMovil { set; get; }
        public string StatusOriginacion { set; get; }
        public string ClaveCliente { set; get; }
        public string Producto { set; get; }
        public string FechaNacimiento { set; get; }
        public string Email { set; get; }
        public string Clabe { set; get; }
        public string EstadoCivil { set; get; }
    }
}