using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiSolicitudServicio : UiEntity
    {
        public int Identificador { get; set; }

        #region Campo Cuota
        [DisplayName("Concepto*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo de concepto es requerido")]
        public int IdCuota { get; set; }
        #endregion
        
        #region Campos de Fechas
        [DisplayName("Fecha Inicio*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha inicio es requerida")]
        public string FechaInicio { get; set; }

        [DisplayName("Fecha Final*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha final es requerida")]
        public string FechaFinal { get; set; }

        [DisplayName("Fecha Examen*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha del examen es requerida")]
        public string FechaExamen { get; set; } 
        #endregion

        [DisplayName("Número de Personas*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El número de personas son requeridas")]
        public int NumeroPersonas { get; set; }

        [DisplayName("Horas/Duración del Curso*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Las horas del curso son requeridas")]
        public int HorasCurso { get; set; }
        
        [DisplayName("Tipo Instalación*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo instalación es requerido")]
        public int IdTipoInstalacion { get; set; }
        
        [DisplayName("Observaciones*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Las observaciones son requeridas")]
        public string Observaciones { get; set; }

        [DisplayName("Comité*:")]
        public string Comite { get; set; }

        [DisplayName("Tipo Bien a Custodiar*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo bien a custodiar es requerido")]
        public string TipoBienCustodiar { get; set; }

        //[Required(ErrorMessage = "Favor de seleccionar al menos un archivo.")]
        //[RegularExpression(@"^.*\.(pdf|PDF)$", ErrorMessage = "Sólo se permiten archivos pdf.")]
        public HttpPostedFileBase Document { get; set; }
        public string fileUrls { get; set; }
        public UiCampoServicio ConfiguracionCampos { get; set; }
    }
}