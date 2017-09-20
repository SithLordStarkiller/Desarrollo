using GOB.SPF.ConecII.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestFactorMunicipio:RequestBase
    {
        public FactorMunicipio Item { get; set; }

        public FactorMunicipioDTO DTO { get; set; }

    }
}
