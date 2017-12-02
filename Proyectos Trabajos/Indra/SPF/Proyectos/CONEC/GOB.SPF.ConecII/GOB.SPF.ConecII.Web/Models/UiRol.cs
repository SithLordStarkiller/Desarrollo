using GOB.SPF.ConecII.Interfaces;
using Microsoft.AspNet.Identity;

namespace GOB.SPF.ConecII.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UiRol : UiEntity,IRol
    {
        public UiRol()
        {
        }

        public UiRol(int id)
        {
            Id = id;
        }
        public int Id { get; set; }

        [Display(Name = @"Nombre del rol")]
        [Required(ErrorMessage = @"El Nombre es requerido")]
        [MaxLength(100,ErrorMessage = @"Se ha alcanzado el maximo de caracteres")]
        public string Name { get; set; }

        [Display(Name = @"Descripcion del rol")]
        [Required(ErrorMessage = @"La descripcion es requerida")]
        [MaxLength(100, ErrorMessage = @"Se ha alcanzado el maximo de caracteres")]
        public string Descripcion { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
        public int? IdParentRol { get; set; }
        public int? IdArea { get; set; }

        public bool Externo { get; set; }
        public string Area { get; set; }
    }
}