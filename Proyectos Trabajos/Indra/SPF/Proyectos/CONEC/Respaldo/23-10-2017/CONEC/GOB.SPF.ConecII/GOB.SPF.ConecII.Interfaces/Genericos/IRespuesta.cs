using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces.Genericos
{
    public interface IRespuesta<TObj>
    {
        bool Exitoso { get; set; }
        TObj Resultado { get; set; }
        IPaging Paginado { get; set; }
    }
}
