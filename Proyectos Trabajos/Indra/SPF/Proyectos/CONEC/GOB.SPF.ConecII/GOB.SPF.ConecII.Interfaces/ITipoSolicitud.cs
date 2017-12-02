using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ITipoSolicitud:IFila<int>
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        bool Activo { get; set; }
    }
}
