
using GOB.SPF.ConecII.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class UiTiposDocumento: UiEntity
    {
    
        public UiTiposDocumento()
        {
        }
        
        [Display(Name = "IdTipoDocumento")]
        public int? Identificador { get; set; }
        
        
        [Display(Name = "Documento")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
    
}
