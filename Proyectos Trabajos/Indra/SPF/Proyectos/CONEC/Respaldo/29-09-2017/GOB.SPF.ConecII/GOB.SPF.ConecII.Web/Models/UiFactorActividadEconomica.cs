using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactorActividadEconomica : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Clasificación Factor*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La clasificación factor es requerida")]
        public int IdClasificacionFactor { get; set; }
        public string ClasificadorFactor { get; set; }
        public List<UiClasificacionFactor> Clasificaciones { get; set; }

        [DisplayName("Factor*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El factor es requerido")]
        public int IdFactor { get; set; }
        [MaxLength(50)]
        public string Factor { get; set; }
        public List<UiFactor> Factores { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El grupo es requerido")]
        [DisplayName("Grupo*:")]
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public List<UiGrupo> Grupos { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La división es requerida")]
        [DisplayName("División*:")]
        public int IdDivision { get; set; }
        public string Division { get; set; }
        public List<UiDivision> Divisiones { get; set; }

        [DisplayName("Actividades*:")]
        public int IdFraccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Se requiere seleccionar al menos una actividad")]
        public List<UiFracciones> FraccionesTodos { get; set; }
        public List<UiFracciones> Fracciones { get; set; }
        public string Actividades { get; set; }

        [DisplayName("Descripción:"), MaxLength(300)]
        public string Descripcion { get; set; }
        public bool IsActive { get; set; }
    }
}