using GOB.SPF.ConecII.Entities.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class RequestEtiquetas : RequestBase, IRequestBase<Etiquetas>
    {
        public Etiquetas Item { get; set; }
}
    
}
