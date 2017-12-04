using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IPersona
    {
        string Nombre { get; set; }
        string Paterno { get; set; }
        string Materno { get; set; }
        string RFC { get; set; }
        IEnumerable<ICorreo> Correo { get; set; }
        IEnumerable<ITelefono> Telefono { get; set; }
    }
}
