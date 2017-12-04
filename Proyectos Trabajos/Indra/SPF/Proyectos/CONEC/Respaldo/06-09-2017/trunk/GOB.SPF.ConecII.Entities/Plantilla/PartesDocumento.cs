
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class PartesDocumento : TEntity
    {
    
        public PartesDocumento()
        {
        }

        [Display(Name = "IdParteDocumento")]
        public override int Identificador { get; set; }
        
        
        [Display(Name = "IdTipoDocumento")]
        public int? IdTipoDocumento { get; set; }
        
        
        [Display(Name = "RutaLogo")]
        public string RutaLogo { get; set; }
        
        
        [Display(Name = "Folio")]
        public string Folio { get; set; }
        
        
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }
        
        
        [Display(Name = "LugarFecha")]
        public string LugarFecha { get; set; }
        
        
        [Display(Name = "Paginado")]
        public bool Paginado { get; set; }
        
        
        [Display(Name = "Ccp")]
        public string Ccp { get; set; }
        
        
        [Display(Name = "Puesto")]
        public string Puesto { get; set; }
        
        
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        
        
        [Display(Name = "FechaInicial")]
        public DateTime? FechaInicial { get; set; }
        
        
        [Display(Name = "FechaFinal")]
        public DateTime? FechaFinal { get; set; }
        
        
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        public virtual TiposDocumento TiposDocumento { get; set; }
    }
    
}
