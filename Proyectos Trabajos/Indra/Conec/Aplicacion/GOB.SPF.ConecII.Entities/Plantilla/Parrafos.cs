using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class Parrafos : TEntity
    {
    
        public Parrafos()
        {
        }

        [Display(Name = "IdParrafo")]
        public override int Identificador { get; set; }
        
        
        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }

        [Display(Name = "IdTipoSeccion")]
        public int? IdTipoSeccion { get; set; }


        [Display(Name = "Parrafo")]
        [Required]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Texto")]
        [Required]
        public string Texto { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        
        public virtual PartesDocumento PartesDocumento { get; set; }

        public virtual List<EtiquetasParrafo> ListaEtiquetasParrafo { get; set; }
    }
    
}
