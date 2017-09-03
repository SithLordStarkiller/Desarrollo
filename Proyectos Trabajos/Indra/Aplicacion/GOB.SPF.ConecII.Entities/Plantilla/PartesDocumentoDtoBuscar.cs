

using GOB.SPF.ConecII.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    public partial class PartesDocumentoDtoBuscar : PageBase, IPageBase
    {

        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumentoBuscar { get; set; }

        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumentoBuscar { get; set; }

        [Display(Name = "RutaLogo")]
        public string RutaLogoBuscar { get; set; }

        [Display(Name = "Folio")]
        public string FolioBuscar { get; set; }

        [Display(Name = "Asunto")]
        public string AsuntoBuscar { get; set; }

        [Display(Name = "LugarFecha")]
        public string LugarFechaBuscar { get; set; }

        [Display(Name = "Paginado")]
        public bool? PaginadoBuscar { get; set; }

        [Display(Name = "Ccp")]
        public string CcpBuscar { get; set; }

        [Display(Name = "Puesto")]
        public string PuestoBuscar { get; set; }

        [Display(Name = "Nombre")]
        public string NombreBuscar { get; set; }

        [Display(Name = "Direccion")]
        public string DireccionBuscar { get; set; }

        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicialBuscar { get; set; }

        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinalBuscar { get; set; }

        [Display(Name = "Activo")]
        public bool? ActivoBuscar { get; set; }
    }
    
}
