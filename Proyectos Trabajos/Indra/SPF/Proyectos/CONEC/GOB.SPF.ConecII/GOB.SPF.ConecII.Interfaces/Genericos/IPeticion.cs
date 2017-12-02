using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces.Genericos
{
    public interface IPeticion<TObj>
    {
        IPaging Paginado { get; set; }
        TObj Solicitud { get; set; }
        string Usuario { get; set; }
    }
}
