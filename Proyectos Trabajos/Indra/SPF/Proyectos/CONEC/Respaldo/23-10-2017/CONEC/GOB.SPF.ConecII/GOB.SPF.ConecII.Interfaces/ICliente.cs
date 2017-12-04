using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ICliente:IFila<int>
    {
        string RFC { get; set; }
        string RazonSocial { get; set; }
        string NombreCorto { get; set; }
        string NombreLargo { get; set; }
        IEnumerable<IInstalacion> Instalaciones { get; set; }
    }
}
