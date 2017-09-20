using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiRolUsuario : UiEntity
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public bool IsActive { get; set; }
    }
}