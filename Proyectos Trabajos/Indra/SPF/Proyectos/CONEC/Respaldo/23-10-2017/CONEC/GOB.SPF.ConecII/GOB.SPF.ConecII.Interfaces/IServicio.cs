using GOB.SPF.ConecII.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IServicio:IFila<int>, IDetalle<int>
    {
        DateTime? FechaInicio { get; set; }
        DateTime? FechaFin { get; set; }
        int? Integrantes { get; set; }
        ICouta Couta { get; set; }
        IEnumerable<IServicioDocumento> Documentos { get; set; }
        ITipoServicio TipoServicio { get; set; }
        DateTime? FechaInicial { get; set; }
        DateTime? FechaFinal { get; set; }
        string Observaciones { get; set; }
        bool? TieneComite { get; set; }
        string ObservacionesComite { get; set; }
        string BienCustodiar { get; set; }
        bool? Viable { get; set; }
        ITipoInstalacionesCapacitacion TipoInstalacionesCapacitacion { get; set; }
        IAcuerdo Acuerdo { get; set; }
        IAsistente Asistentes { get; set; }
    }
}
