using GOB.SPF.ConecII.Entities.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class RequestInstituciones : RequestBase, IRequestBase<Instituciones>
    {
        public Instituciones Item { get; set; }
    }

}