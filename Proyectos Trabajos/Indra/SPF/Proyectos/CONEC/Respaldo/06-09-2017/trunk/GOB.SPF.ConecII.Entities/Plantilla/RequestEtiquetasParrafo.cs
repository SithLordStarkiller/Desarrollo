using GOB.SPF.ConecII.Entities.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class RequestEtiquetasParrafo : RequestBase
    {
        public EtiquetasParrafo Item { get; set; }
    }

}