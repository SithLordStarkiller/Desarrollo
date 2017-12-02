using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{

    public class UiTiposPago : UiEntity
    {
        public int Identificador { get; set; }
        public int IdRegimenFiscal { get; set; }
        [DisplayName("Nombre*")]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }

        [DisplayName("Descripción*"), MaxLength(100)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción es requerido")]
        public string Descripcion { get; set; }

        [DisplayName("Actividad*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La actividad es requerido")]
        public bool Actividad { get; set; }
        public bool IsActive { get; set; }
    }
}