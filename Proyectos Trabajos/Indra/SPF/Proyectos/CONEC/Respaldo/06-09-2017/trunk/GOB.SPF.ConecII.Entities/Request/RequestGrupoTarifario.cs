using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestGrupoTarifario : RequestBase
    {
        public GrupoTarifario Item { get; set; }
    }
}
