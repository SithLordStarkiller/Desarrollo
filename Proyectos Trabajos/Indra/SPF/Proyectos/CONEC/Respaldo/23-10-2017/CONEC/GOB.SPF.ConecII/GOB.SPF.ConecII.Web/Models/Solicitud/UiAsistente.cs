using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiAsistente:UiEntity
    {
        public int Identificador { get; set; }
        public Guid idPersona { get; set; }
        public bool Activo { get; set; }
    }
}
