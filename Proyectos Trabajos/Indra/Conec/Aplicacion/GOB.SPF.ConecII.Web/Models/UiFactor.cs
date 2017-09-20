using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactor : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Tipo Servicio*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo servicio es requerido")]
        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }

        [DisplayName("Clasificación Factor*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La clasificadción del factor es requerida")]
        public int IdClasificacionFactor { get; set; }
        public string ClasificadorFactor { get; set; }

        [DisplayName("Medida Cobro*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La medida cobro es requerida")]
        public int IdMedidaCobro { get; set; }
        public string MedidaCobro { get; set; }

        [DisplayName("Descripción*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [DisplayName("Nombre*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El factor es requerido")]
        public string Nombre { get; set; }

        [DisplayName("Cuota*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La cuota es requerida")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal Cuota { get; set; }

        [DisplayName("Fecha Autorización*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha autorización es requerida")]
        public DateTime FechaAutorizacion { get; set; }

        [DisplayName("Fecha Entrada Vigor*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha entrada vigor es requerida")]
        public DateTime FechaEntradaVigor { get; set; }

        [DisplayName("Fecha Termino*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha termino es requerida")]
        public DateTime FechaTermino { get; set; }

        [DisplayName("Fecha Publicación*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha publicación es requerida")]
        public DateTime FechaPublicacionDof { get; set; }

        public bool IsActive { get; set; }
    }
}