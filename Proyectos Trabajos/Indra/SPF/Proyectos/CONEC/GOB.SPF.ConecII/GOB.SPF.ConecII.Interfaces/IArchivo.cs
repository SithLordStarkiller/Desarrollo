using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IArchivo
    {
        int? IdFisico { get; set; }
        string Nombre { get; set; }
        string Base64 { get; set; }
        string Extension { get; set; }
    }
}
