using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class Etiquetas : TEntity
    {
    
        public Etiquetas()
        {
        }

        [Display(Name = "IdEtiqueta")]
        public override int Identificador { get; set; }
        
        
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }
        
        
        [Display(Name = "Etiqueta")]
        [Required]
        public string Etiqueta { get; set; }
        
        
        [Display(Name = "Contenido")]
        [Required]
        public string Contenido { get; set; }
        
        
        [Display(Name = "Negrita")]
        public bool? Negrita { get; set; }
        
        
        [Display(Name = "Orden")]
        [Required]
        public decimal? Orden { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        
        public virtual PartesDocumento PartesDocumento { get; set; }
    }
    
}
