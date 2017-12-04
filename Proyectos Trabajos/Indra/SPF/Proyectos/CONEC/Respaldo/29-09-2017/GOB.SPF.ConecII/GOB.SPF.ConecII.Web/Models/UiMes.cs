using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiMes : UiEntity
    {
        public int Identificador { get; set; }

        public string Name { get; set; }
    }
}