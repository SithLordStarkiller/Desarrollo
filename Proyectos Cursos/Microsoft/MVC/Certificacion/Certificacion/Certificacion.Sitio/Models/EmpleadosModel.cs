using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;


namespace Certificacion.Sitio.Models
{
    public class EmpleadosModel
    {
        [DisplayName("IdEmpleado")]
        public int IdEmpleado { get; set; }
        
        [DisplayName("Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre no puede estar vacio.")]
        public string Nombre { get; set; }

        [DisplayName("Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El apellido no puede estar vacio.")]
        public string Apellido { get; set; }

        [DisplayName("Genero")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El genero no puede estar vacio.")]
        public string Genero { get; set; }

        [DisplayName("Fecha de ingreso")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Es necesaria una fecha de ingreso.")]
        public DateTime? FechaIngreso { get; set; }

        [DisplayName("Salario")]
        public decimal? Salario { get; set; }

        [DisplayName("IdDepartamento")]
        public int IdDepartemento { get; set; }
    }
}