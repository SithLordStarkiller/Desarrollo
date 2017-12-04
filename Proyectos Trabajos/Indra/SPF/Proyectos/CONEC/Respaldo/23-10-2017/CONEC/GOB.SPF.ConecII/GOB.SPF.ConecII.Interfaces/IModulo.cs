using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IModulo:IFila<int>, IDetalle<int?>
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        DateTime FechaInicial { get; set; }
        DateTime? FechaFinal { get; set; }
        bool Activo { get; set; }
        string Controlador { get; set; }
        string Accion { get; set; }
        IEnumerable<IModulo> SubModulos { get; set; }
        IEnumerable<string> RolesAutorizados { get; set; }
    }
}
