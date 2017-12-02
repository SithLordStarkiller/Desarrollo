using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiPlantilla:UiEntity
    {
        public int Identificador { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}