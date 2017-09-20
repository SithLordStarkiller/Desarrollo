using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTelefono : UiEntity
    {
        [Required]
        public int IdTipoTelefono { get; set; }

        [Required]
        public string Numero { get; set; }
        public string Extension { get; set; }
    }
}