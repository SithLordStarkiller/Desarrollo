using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiEstado : UiEntity
    {
        public int Identificador { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public bool IsActive { get; set; }
    }
}