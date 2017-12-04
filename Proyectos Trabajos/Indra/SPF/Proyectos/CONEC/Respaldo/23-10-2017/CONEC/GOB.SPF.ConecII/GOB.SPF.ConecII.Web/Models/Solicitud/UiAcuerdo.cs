using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiAcuerdo:UiEntity
    {
        public int Identificador { get; set; }
        public DateTime Fecha { get; set; }
        public string Convenio { get; set; }
        public Guid Responsable { get; set; }
        public bool IsActive { get; set; }
    }
}
