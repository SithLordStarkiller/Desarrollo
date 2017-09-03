using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{    
    public partial class InstitucionesDtoBuscar : PageBase, IPageBase
    {
        [Display(Name = "IdInstitucion")]
        public int? IdInstitucionBuscar { get; set; }

        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumentoBuscar { get; set; }

        [Display(Name = "Nombre")]
        public string NombreBuscar { get; set; }

        [Display(Name = "Negrita")]
        public bool? NegritaBuscar { get; set; }

        [Display(Name = "Orden")]
        public decimal? OrdenBuscar { get; set; }

        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicialBuscar { get; set; }

        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinalBuscar { get; set; }

        [Display(Name = "Activo")]
        public bool? ActivoBuscar { get; set; }
    }
    
}
