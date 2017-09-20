using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public partial class UiParrafos : UiEntity
    {
    
        public UiParrafos()
        {
        }

        [Display(Name = "IdParrafo")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }

        [Display(Name = "IdTipoSeccion")]
        public int? IdTipoSeccion { get; set; }


        [Display(Name = "Parrafo")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Texto")]
        public string Texto { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        public bool EsActivo { get { return Activo ?? false; } set { Activo = value; } }

        public virtual UiPartesDocumento PartesDocumento { get; set; }

        public virtual List<UiEtiquetasParrafo> ListaEtiquetasParrafo { get; set; }
    }
    
}
