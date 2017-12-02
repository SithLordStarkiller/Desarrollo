using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ITipoInstalacionesCapacitacion:IFila<int>
    {
        string Nombre { get; set; }
    }
}
