using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces.Genericos
{
    public interface IDetalle<PId>
    {
        PId IdPadre { get; set; }
    }
}
