using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public partial class UiEtiquetasParrafo : UiEntity
    {
    
        public UiEtiquetasParrafo()
        {
        }

        [Display(Name = "IdEtiquetaParrafo")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdParrafo")]
        public int? IdParrafo { get; set; }

        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }
        

        [Display(Name = "Etiqueta")]
        public string Etiqueta { get; set; }
        
        
        [Display(Name = "Contenido")]
        public string Contenido { get; set; }
        
        
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

        public virtual UiParrafos Parrafos { get; set; }
    }
    
}
