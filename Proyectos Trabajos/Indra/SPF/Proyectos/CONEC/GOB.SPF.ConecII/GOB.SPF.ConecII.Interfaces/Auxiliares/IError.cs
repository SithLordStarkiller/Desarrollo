using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces.Auxiliares
{
    public interface IError
    {
        TipoError Tipo { get; set; }
        string Mensaje { get; set; }
    }
}
