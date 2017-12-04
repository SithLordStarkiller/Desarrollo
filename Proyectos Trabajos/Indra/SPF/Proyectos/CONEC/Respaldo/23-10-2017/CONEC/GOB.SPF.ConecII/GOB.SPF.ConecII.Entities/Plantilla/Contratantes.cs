using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    public partial class Contratantes : TEntity
    {
    
        public Contratantes()
        {
        }
        
        
        [Display(Name = "IdContratante")]
        public override int Identificador { get; set; }
        
        
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }
        
        
        [Display(Name = "Nombre")]
        [Required]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Cargo")]
        [Required]
        public string Cargo { get; set; }
        
        
        [Display(Name = "Domicilio")]
        [Required]
        public string Domicilio { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        
        public virtual PartesDocumento PartesDocumento { get; set; }
    }
    
}
