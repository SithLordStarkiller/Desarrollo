using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiEntity
    {
        [JsonIgnore]
        public UiEnumEntity Action { get; set; }
    }
}