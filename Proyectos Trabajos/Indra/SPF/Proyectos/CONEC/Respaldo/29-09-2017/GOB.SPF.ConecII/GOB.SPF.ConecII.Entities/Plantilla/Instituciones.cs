using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class Instituciones : TEntity
    {
    
        public Instituciones()
        {
        }

        [Display(Name = "IdInstitucion")]
        public override int Identificador { get; set; }


        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }


        [Display(Name = "Nombre")]
        [Required]
        public string Nombre { get; set; }


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
