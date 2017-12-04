using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    
    public partial class UiArchivo : UiEntity
    {
    
        public UiArchivo()
        {
        }
        
        
        [Display(Name = "IdArchivo")]
        public int? Identificador { get; set; }

        [Display(Name = "IdParteDocumento")]
        public int? IdParteDocumento { get; set; }


        [Display(Name = "Imagen")]
        public string Imagen { get; set; }
        
    }
    
}
