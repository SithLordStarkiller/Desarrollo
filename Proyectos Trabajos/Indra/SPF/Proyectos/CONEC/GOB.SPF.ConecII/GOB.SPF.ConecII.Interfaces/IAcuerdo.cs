using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IAcuerdo
    {
        DateTime? Fecha { get; set; }
        string Convenio { get; set; }
        Guid Responsable { get; set; }
    }
}
