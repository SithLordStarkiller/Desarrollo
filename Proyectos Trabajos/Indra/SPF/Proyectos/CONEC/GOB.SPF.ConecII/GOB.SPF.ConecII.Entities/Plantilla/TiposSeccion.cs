using GOB.SPF.ConecII.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    
    public partial class TiposSeccion : TEntity
    {

        public TiposSeccion()
        {
        }
        
        [Display(Name = "IdTipoSeccion")]
        public int? IdTipoSeccion { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Orden")]
        public int? Orden { get; set; }

        [Display(Name = "Numerado")]
        public bool? Numerado { get; set; }

        [Display(Name = "Mensaje")]
        public string Mensaje { get; set; }

        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }

        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }

        [Display(Name = "Activo")]
        public bool? Activo { get; set; }

        [Display(Name = "Etiqueta")]
        public bool? Etiqueta { get; set; }

    }

}
