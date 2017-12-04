using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Control : Request.RequestBase, IControl
    {
        public int Identificador { get; set; }
        public int IdTipoControl { get; set; }
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
