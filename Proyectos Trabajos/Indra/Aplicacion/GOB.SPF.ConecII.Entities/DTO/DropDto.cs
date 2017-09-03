using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class DropDto
    {
        [PrimaryKeyAttribute("Id", AutoIncrement = false)]
        [ColumnAttribute]
        [Display(Name = "Id")]
        public int? Id { get; set; }
        [ColumnAttribute]
        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}
