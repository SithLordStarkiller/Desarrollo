
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    public partial class Contratantes
    {
    
        public Contratantes()
        {
        }
        
        
        [Display(Name = "IdContratante")]
        public int? IdContratante { get; set; }
        
        
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        
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
        public bool Activo { get; set; }
        
        public virtual TiposDocumento TiposDocumento { get; set; }
    }
    
}
