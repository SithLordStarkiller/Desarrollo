using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    public partial class ContratantesDtoBuscar : PageBase, IPageBase
    {

        [Display(Name = "IdContratante")]
        public int? IdContratanteBuscar { get; set; }

        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumentoBuscar { get; set; }

        [Display(Name = "Nombre")]
        public string NombreBuscar { get; set; }

        [Display(Name = "Cargo")]
        public string CargoBuscar { get; set; }

        [Display(Name = "Domicilio")]
        public string DomicilioBuscar { get; set; }

        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicialBuscar { get; set; }

        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinalBuscar { get; set; }

        [Display(Name = "Activo")]
        public bool? ActivoBuscar { get; set; }
    }
    
}
