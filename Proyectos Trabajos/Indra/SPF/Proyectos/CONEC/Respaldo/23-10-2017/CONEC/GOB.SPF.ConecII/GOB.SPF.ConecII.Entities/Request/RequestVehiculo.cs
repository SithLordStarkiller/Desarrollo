using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestVehiculo:RequestBase
    {
        public VehiculoTarifario Item { get; set; }
    }
}
