using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ITipoDocumento:IFila<int>
    {
        int? TipoDocumento { get; set; }
        int? TipoActividad { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        bool? Confidencial { get; set; }
        bool? Requerido { get; set; }
    }
}
