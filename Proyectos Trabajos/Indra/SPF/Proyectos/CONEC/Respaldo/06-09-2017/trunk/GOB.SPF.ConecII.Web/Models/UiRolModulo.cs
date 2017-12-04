using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiRolModulo : UiEntity
    {
        public int Identificador { get; set; }
        public int IdRol { get; set; }
        public int IdModulo { get; set; }
        public bool IsActive { get; set; }
    }
}