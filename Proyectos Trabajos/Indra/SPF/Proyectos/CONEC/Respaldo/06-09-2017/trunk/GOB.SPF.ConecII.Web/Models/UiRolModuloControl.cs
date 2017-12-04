using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiRolModuloControl : UiEntity
    {
        public int Identificador { get; set; }
        public int IdRolModulo { get; set; }
        public int IdControl { get; set; }
        public bool Captura { get; set; }
        public bool Consulta { get; set; }
        public bool IsActive { get; set; }
    }
}