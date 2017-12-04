using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactorLeyIngreso : UiEntity
    {
        public int Identificador { get; set; }

        [DisplayName("Año*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El año es requerido")]
        public List<UiAnio> Anios { get; set; }
        public string Anio { get; set; }
        public int IdAnio { get; set; }

        [DisplayName("Mes*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El mes es requerido")]
        public List<UiMes> Meses { get; set; }
        public string Mes { get; set; }
        public int IdMes { get; set; }

        [DisplayName("Factor*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El factor es requerido")]
        public decimal Factor { get; set; }

        public bool IsActive { get; set; }
    }
}