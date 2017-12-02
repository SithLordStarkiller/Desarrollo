using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Auxiliares;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ITelefono
    {
        int? Numero { get; set; }
        int? Extension { get; set; }
        TipoTelefono Tipo { get; set; }
    }
}
