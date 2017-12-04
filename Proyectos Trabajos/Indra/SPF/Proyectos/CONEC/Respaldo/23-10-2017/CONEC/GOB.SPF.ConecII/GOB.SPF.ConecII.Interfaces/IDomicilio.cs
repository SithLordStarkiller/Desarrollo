using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IDomicilio
    {
        string Calle { get; set; }
        string Exterior { get; set; }
        string Interior { get; set; }
        IAsentamiento Asentamiento { get; set; }
        IMunicipio Municipio { get; set; }
        IEstado Estado { get; set; }
    }
}
