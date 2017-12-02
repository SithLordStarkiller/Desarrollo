using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    
    public partial class UiContratantes : UiEntity
    {
    
        public UiContratantes()
        {
        }
        
        
        [Display(Name = "IdContratante")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }
        
        
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }
        
        
        [Display(Name = "Domicilio")]
        public string Domicilio { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        public bool EsActivo { get { return Activo ?? false; } set { Activo = value; } }

        public virtual UiPartesDocumento PartesDocumento { get; set; }
    }
    
}
