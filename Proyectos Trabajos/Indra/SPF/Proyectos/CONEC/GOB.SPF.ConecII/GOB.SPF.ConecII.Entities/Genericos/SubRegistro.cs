using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Genericos
{
    public abstract class SubRegistro<TParentId>
    {
        [JsonIgnore]
        public TParentId IdentificadorPadre { get; set; }
    }
}
