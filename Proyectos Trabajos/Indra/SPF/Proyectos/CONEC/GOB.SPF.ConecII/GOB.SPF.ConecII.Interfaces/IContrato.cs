using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IContrato:IFila<int>
    {
        IEnumerable<IServicio> Servicios { get; set; }
        bool? EsFirmado { get; set; }
        DateTime? FechaFirma { get; set; }
        string Folio { get; set; }
    }
}
