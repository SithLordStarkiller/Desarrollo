using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Interfaces.Genericos
{
    public interface IPaging
    {
        bool All { get; set; }

        int CurrentPage { get; set; }

        int Pages { get; set; }

        int Rows { get; set; }

    }
}
