using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IJerarquia:IFila<int>
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        int? Nivel { get; set; }
        DateTime? FechaInicial { get; set; }
        DateTime? FechaFinal { get; set; }
        bool? Activo { get; set; }
    }
}
