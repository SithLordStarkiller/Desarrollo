using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface ICouta:IFila<int>
    {
        string Cocepto { get; set; }
        decimal CoutaBase { get; set; }
        int Iva { get; set; }
        DateTime? FechaAutorizacion { get; set; }
        DateTime? FechaEntradaVigor { get; set; }
        DateTime? FechaTermino { get; set; }
        DateTime? FechaPublicacionDOF { get; set; }
        bool? Activo { get; set; }
    }
}
