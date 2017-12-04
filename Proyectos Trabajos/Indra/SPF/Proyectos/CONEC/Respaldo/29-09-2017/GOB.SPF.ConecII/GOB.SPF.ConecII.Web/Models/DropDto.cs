using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class DropDto
    {
        
        
        [Display(Name = "Identificador")]
        public int? Identificador { get; set; }
        
        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}
