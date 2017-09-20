using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public partial class UiInstituciones : UiEntity
    {
    
        public UiInstituciones()
        {
        }

        [Display(Name = "IdInstitucion")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }
        
        
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Negrita")]
        public bool? Negrita { get; set; }
        public bool EsNegrita { get { return Negrita ?? false; } set { Negrita = value; } }
        
        
        [Display(Name = "Orden")]
        public decimal? Orden { get; set; }
        
        
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
