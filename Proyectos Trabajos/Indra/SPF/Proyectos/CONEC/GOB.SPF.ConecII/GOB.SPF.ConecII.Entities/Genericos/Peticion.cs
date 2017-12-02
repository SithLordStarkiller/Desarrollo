using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces;
using GOB.SPF.ConecII.Interfaces.Genericos;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Entities.Genericos
{
    public class Peticion<TObj> : IPeticion<TObj>
    {
        [JsonConverter(typeof(SerializableInterface<Paging>))]
        public IPaging Paginado { get; set; }
        public TObj Solicitud { get; set; }
        public string Usuario { get; set; }
    }
}
