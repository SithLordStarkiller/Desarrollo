using GOB.SPF.ConecII.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class UiEstiquetas : UiEntity
    {
    
        public UiEstiquetas()
        {
        }

        [Display(Name = "IdEtiqueta")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        
        [Display(Name = "Etiqueta")]
        public string Etiqueta { get; set; }
        
        
        [Display(Name = "Contenido")]
        public string Contenido { get; set; }
        
        
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
        
        public virtual UiTiposDocumento TiposDocumento { get; set; }
    }
    
}
