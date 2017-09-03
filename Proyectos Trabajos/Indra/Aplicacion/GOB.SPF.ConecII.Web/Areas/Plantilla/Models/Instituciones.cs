using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class Instituciones
    {
    
        public Instituciones()
        {
        }

        [Display(Name = "IdInstitucion")]
        public int? IdInstitucion { get; set; }
        
        
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Negrita")]
        public bool Negrita { get; set; }
        
        
        [Display(Name = "Orden")]
        public decimal? Orden { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
                
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        
        public virtual TiposDocumento TiposDocumento { get; set; }
    }
    
}
