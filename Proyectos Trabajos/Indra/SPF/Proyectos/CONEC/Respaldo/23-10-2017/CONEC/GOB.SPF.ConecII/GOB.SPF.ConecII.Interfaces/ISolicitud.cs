using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ISolicitud:IFila<int>
    {
        string Folio { get; set; }
        DateTime? FechaRegistro { get; set; }
        int? DocumentoSoporte { get; set; }
        int? Minuta { get; set; }
        IEnumerable<IServicio> Servicios { get; set; }
        ICliente Cliente { get; set; }
        IDocumento Documento { get; set; }
        ITipoSolicitud TipoSolicitud { get; set; }

    }
}
