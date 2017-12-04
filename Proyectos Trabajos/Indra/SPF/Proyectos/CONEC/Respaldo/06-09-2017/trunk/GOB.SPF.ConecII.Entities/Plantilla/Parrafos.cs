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
        
        public virtual TiposDocumento TiposDocumento { get; set; }

        public virtual List<EtiquetasParrafo> ListaEtiquetasParrafo { get; set; }
    }
    
}
