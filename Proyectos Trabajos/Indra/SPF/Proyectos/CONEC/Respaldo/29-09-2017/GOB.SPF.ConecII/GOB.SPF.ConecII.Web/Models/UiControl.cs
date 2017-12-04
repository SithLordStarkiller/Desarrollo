using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiControl : UiEntity
    {
        public int Identificador { get; set; }
        public int IdTipoControl { get; set; }
        public int IdModulo { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
}