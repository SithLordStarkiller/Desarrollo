using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactorEntidadFederativa : UiEntity
    {
        public int Identificador { get; set; }
        public List<UiClasificacionFactor> ClasificacionesFactor { get; set; }

        [DisplayName("Clasificación Factor*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La clasificación factor es requerida")]
        public int IdClasificadorFactor { get; set; }
        public string ClasificadorFactor { get; set; }
        public List<UiFactor> Factores { get; set; }
        public string Factor { get; set; }

        [DisplayName("Factor*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El factor es requerido")]
        public int IdFactor { get; set; }

        [DisplayName("Descripción:")]
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }

        [DisplayName("Estados:*")]
        public List<UiEstado> Estados { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Se requiere seleccionar al menos un estado")]
        public List<UiEstado> EstadosDestino { get; set; }
        public string EntidadesFederativas { get; set; }
        public bool IsActive { get; set; }
    }
}