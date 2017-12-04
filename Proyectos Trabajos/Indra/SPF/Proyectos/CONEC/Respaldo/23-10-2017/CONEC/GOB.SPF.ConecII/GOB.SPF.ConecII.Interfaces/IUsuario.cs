using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace GOB.SPF.ConecII.Interfaces
{
    public interface IUsuario:IUser<int>
    {
        Guid? IdPersona { get; set; }
        int IdExterno { get; set; }
        string Login { get; set; }
        DateTime FechaInical { get; set; }
        DateTime? FechaFinal { get; set; }
        bool Activo { get; set; }
    }
}
