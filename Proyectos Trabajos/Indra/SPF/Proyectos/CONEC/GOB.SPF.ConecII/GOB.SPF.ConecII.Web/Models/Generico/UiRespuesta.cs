using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOB.SPF.ConecII.Interfaces;
using GOB.SPF.ConecII.Interfaces.Genericos;
using GOB.SPF.ConecII.Web.Models.Seguridad;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Web.Models.Generico
{
    public class UiRespuesta<TObj> : IRespuesta<TObj>
    {
        public bool Exitoso { get; set; }

        [JsonConverter(typeof(SerializableInterface<UiPaging>))]
        public IPaging Paginado { get; set; }

        public TObj Resultado { get; set; }
    }
}