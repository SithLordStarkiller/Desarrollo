//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IBC
{
    using System;
    using System.Collections.Generic;
    
    public partial class DireccionBCResp
    {
        public int idPersonaRespBC { get; set; }
        public int idDireccionResp { get; set; }
        public string direccion1 { get; set; }
        public string direccion2 { get; set; }
        public string coloniaPoblacion { get; set; }
        public string delegacionMunicipio { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string cp { get; set; }
        public string fechaResidencia { get; set; }
        public string numeroTelefono { get; set; }
        public string extension { get; set; }
        public string fax { get; set; }
        public string tipoDomicilio { get; set; }
        public string indicadorEspecialDomicilio { get; set; }
        public string fechaReporteDireccion { get; set; }
    
        public virtual PersonaRespBC PersonaRespBC { get; set; }
    }
}
