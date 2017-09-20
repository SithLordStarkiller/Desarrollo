using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestBase
    {
        public string Usuario { get; set; }

        public Paging Paging { get; set; }
    }
}
