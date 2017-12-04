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
        
        
        [Display(Name = "Id")]
        public int? Id { get; set; }
        
        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}
