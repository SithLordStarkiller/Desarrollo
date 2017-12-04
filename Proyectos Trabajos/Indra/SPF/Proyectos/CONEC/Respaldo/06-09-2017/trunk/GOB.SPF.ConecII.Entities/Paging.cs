using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Paging
    {
        public bool All { get; set; }

        public int CurrentPage { get; set; }

        public int Pages { get; set; }

        public int Rows { get; set; }
    }
}
