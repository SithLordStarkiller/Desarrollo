using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    
    public partial class UiTiposSeccion : UiEntity
    {

        public UiTiposSeccion()
        {
        }
        
        [Display(Name = "IdTipoSeccion")]
        public int? Identificador { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Orden")]
        public int? Orden { get; set; }

        [Display(Name = "Numerado")]
        public bool? Numerado { get; set; }
        public bool EsNumerado { get { return Numerado ?? false; } set { Numerado = value; } }

        [Display(Name = "Mensaje")]
        public string Mensaje { get; set; }

        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }

        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }

        [Display(Name = "Activo")]
        public bool? Activo { get; set; }
        public bool EsActivo { get { return Activo ?? false; } set { Activo = value; } }
        public bool? Visible { get; set; }
        public bool EsVisible { get { return Visible ?? false; } set { Visible = value; } }
        public bool? Etiqueta { get; set; }
        public bool EsEtiqueta { get { return Etiqueta ?? false; } set { Etiqueta = value; } }
    }
    
}
