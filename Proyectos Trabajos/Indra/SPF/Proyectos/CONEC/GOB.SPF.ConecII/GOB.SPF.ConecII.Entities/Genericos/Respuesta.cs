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
    public class Respuesta<TObj> : IRespuesta<TObj>
    {
        public bool Exitoso { get; set; }
        public IPaging Paginado { get; set; }

        public TObj Resultado { get; set; }
    }
}
