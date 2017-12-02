using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IServicioDocumento: IDocumento, IFila<int>, IDetalle<int>
    {
        string Observaciones { get; set; }
    }
}
