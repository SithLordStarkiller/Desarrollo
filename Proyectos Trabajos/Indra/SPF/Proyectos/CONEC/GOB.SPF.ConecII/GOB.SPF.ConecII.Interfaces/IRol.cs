using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IRol:IRole<int>
    {
        new int Id { get; set; }
        new string Name { get; set; }
        int? IdParentRol { get; set; }
        string Descripcion { get; set; }
        int? IdArea { get; set; }
        DateTime FechaInicial { get; set; }
        DateTime? FechaFinal { get; set; }
        bool Activo { get; set; }
    }
}
