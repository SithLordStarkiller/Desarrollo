using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiClienteContacto : UiExterno
    {
        public int Numero { get; set; }

        [Required]
        public int IdTipoContacto { get; set; }
        public string TipoContacto { get; set; }
    }
}