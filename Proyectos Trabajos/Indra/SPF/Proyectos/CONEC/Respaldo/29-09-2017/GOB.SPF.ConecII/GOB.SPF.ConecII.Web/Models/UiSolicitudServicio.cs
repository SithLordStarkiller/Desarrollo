using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiSolicitudServicio
    {
        #region DATOS DEL CLIENTE
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string NombreCorto { get; set; }
        public string RegimenFiscal { get; set; }
        public string Sector { get; set; }
        #endregion DATOS DEL CLIENTE
        //public byte[] foto { get; set; }
        #region DATOS DEL SOLICITANTE
        public string NombreSolicitante { get; set; }
        public string apPaternoSolicitante { get; set; }
        public string apMaternoSolicitante { get; set; }
        public string telefonoCasa { get; set; }
        public string telefonoCelular { get; set; }
        public string telefonoLaboral { get; set; }
        public string emailPersonal { get; set; }
        public string emailLaboral { get; set; }
        #endregion DATOS DEL SOLICITANTE

        #region DATOS DEL CONTACTO
        public int IdTipoContacto { get; set; }
        public string NombreContacto { get; set; }
        public string apPaternoContacto { get; set; }
        public string apMaternoContacto { get; set; }
        public string cargoContacto { get; set; }
        public string telefonoCasaContacto { get; set; }
        public string telefonoCelularContacto { get; set; }
        public string telefonoLaboralContacto { get; set; }
        public string emailPersonalContacto { get; set; }
        public string emailLaboralContacto { get; set; }
        #endregion DATOS DEL CONTACTO

        #region DOMICILIO FISCAL
        public DateTime fechaRegistro { get; set; }
        public string curp { get; set; }
        public string gradoEscolar { get; set; }
        public string entidadFederativa { get; set; }
        public string municipioDelegacion { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string CodigoPostal { get; set; }
        #endregion DOMICILIO FISCAL

        #region TIPO SERVICIO CARGA ARCHIVO
        public int IdTipoServicio { get; set; }
        public string Archivo { get; set; }

        #endregion TIPO SERVICIO CARGA ARCHIVO
    }
}