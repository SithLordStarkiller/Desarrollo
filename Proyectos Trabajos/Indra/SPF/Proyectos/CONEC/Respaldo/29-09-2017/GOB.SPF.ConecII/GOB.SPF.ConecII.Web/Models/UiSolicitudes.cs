using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiSolicitudes : UiEntity
    {
        public int Identificador { get; set; }
        public int IdCliente { get; set; }

        [DisplayName("Razón Social*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La razón social es requerida")]
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public string RFC { get; set; }

        [DisplayName("Regimen Fiscal*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El regimen fiscal es requerido")]
        public int IdRegimenFiscal { get; set; }
        public List<UiRegimenFiscal> RegimenFiscales { get; set; }
        public string RegimenFiscal { get; set; }

        [DisplayName("Sector*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El sector es requerido")]
        public int IdSector { get; set; }
        public List<UiSector> Sectores { get; set; }
        public string Sector { get; set; }

        [DisplayName("Tipo Solicitud*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo solicitud es requerido")]
        public int IdTipoSolicitud { get; set; }
        public List<UiTipoSolicitud> TiposSolicitud { get; set; }
        public string TipoSolicitud { get; set; }
        public int DocumentoSoporte { get; set; }
        public string Folio { get; set; }
        public int Minuta { get; set; }
        public bool Cancelado { get; set; }
    }
}