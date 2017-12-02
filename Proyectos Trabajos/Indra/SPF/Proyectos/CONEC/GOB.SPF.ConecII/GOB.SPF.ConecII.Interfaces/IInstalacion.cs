using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IInstalacion:IFila<int>
    {
        string Nombre { get; set; }
        string NombreCorto { get; set; }
        double? Latitud { get; set; }
        double? Longitud { get; set; }
    }
}
