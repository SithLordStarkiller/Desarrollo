using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiServicioDocumento : UiDocumento
    {
        public string Observaciones { get; set; }
        public bool EsSoporte { get; set; }
    }
}