using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Persona : IPersona
    {
        public IEnumerable<ICorreo> Correo { get; set; }
        public string Materno { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string RFC { get; set; }
        public IEnumerable<ITelefono> Telefono { get; set; }
    }
}
