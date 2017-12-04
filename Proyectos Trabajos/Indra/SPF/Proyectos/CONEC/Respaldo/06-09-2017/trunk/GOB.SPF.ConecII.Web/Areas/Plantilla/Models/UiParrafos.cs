using GOB.SPF.ConecII.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class UiParrafos : UiEntity
    {
    
        public UiParrafos()
        {
        }

        [Display(Name = "IdParrafo")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        
        [Display(Name = "Parrafo")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Texto")]
        public string Texto { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        
        public virtual UiTiposDocumento TiposDocumento { get; set; }

        public virtual List<UiEtiquetasParrafo> ListaEtiquetasParrafo { get; set; }
    }
    
}
