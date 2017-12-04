using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public partial class UiPartesDocumento : UiEntity
    {
    
        public UiPartesDocumento()
        {
        }

        [Display(Name = "IdParteDocumento")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        
        [Display(Name = "RutaLogo")]
        public string RutaLogo { get; set; }
        
        
        [Display(Name = "Folio")]
        public string Folio { get; set; }
        
        
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }
        
        
        [Display(Name = "LugarFecha")]
        public string LugarFecha { get; set; }
        
        
        [Display(Name = "Paginado")]
        public bool? Paginado { get; set; }
        public bool? EsPaginado { get { return Paginado ?? false; } set { Paginado = value; } }
        


        [Display(Name = "Ccp")]
        public string Ccp { get; set; }
        
        
        [Display(Name = "Puesto")]
        public string Puesto { get; set; }
        
        
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        public bool EsActivo { get { return Activo ?? false; } set { Activo = value; } }

        public virtual UiTiposDocumento TiposDocumento { get; set; }
    }
    
}
