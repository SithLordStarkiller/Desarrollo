using GOB.SPF.ConecII.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestConfiguracionServicio : RequestBase
    {
        public ConfiguracionServicio Item { get; set; }

        public List<ConfiguracionServicio> ListInsertar { get; set; }

    }
    
}
