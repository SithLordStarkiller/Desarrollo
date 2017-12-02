using GOB.SPF.ConecII.Entities.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{

    public partial class RequestContratantes : RequestBase, IRequestBase<Contratantes>
    {
        public Contratantes Item { get; set; }
    }

}
