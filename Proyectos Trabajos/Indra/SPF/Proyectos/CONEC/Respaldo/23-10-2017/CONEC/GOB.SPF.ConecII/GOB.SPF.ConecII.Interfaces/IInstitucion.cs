using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IInstitucion
    {
        string RFC { get; set; }
        IEnumerable<IContacto> Contactos { get; set; }
        IEnumerable<ISolicitante> Solicitantes { get; set; }
    }
}
