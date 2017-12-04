using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiFactorLeyIngreso : UiEntity
    {
        NumberFormatInfo pesos = new CultureInfo("en-US", false).NumberFormat;
        public int Identificador { get; set; }

        [DisplayName("Año*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El año es requerido")]
        public int IdAnio { get; set; }
        public List<UiAnio> Anios { get; set; }
        public string Anio { get; set; }

        [DisplayName("Mes*:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El mes es requerido")]
        public int IdMes { get; set; }
        public List<UiMes> Meses { get; set; }
        public string Mes { get; set; }

        [DisplayName("Factor*:")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "El máximo es de 2 puntos decimales.")]
        [Range(0, 999999999999.99999, ErrorMessage = "El máximo es de 12 dígitos enteros.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El factor es requerido")]
        public string FactorTexto { get; set; }
        public double Factor { get; set; }

        public bool IsActive { get; set; }
    }
}