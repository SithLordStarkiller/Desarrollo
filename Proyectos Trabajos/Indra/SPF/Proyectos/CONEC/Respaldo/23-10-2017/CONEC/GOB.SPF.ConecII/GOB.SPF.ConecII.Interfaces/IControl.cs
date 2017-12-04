using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IControl
    {
        int Identificador { get; set; }
        int IdTipoControl { get; set; }
        int IdModulo { get; set; }
        string Nombre { get; set; }
        string Descripcion { get; set; }
        DateTime FechaInicial { get; set; }
        DateTime? FechaFinal { get; set; }
        bool Activo { get; set; }
    }
}
