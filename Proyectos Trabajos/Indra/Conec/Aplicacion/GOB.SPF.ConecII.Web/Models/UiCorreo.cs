using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiCorreo : UiEntity
    {
        [Required]
        public string CorreoElectronico { get; set; }
    }
}