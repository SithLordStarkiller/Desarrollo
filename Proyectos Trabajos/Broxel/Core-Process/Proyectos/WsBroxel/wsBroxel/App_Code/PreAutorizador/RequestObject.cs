using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel
{
    public class RequestObject
    {
        public string tarjeta { get; set; }

        public List<Acciones> acciones { get; set; }
    }
}