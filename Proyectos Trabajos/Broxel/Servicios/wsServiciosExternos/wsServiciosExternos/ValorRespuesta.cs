using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wsServiciosExternos
{
    public class ValorRespuesta
    {
        [Key]
        public int IdOrden { get; set; }
        public int IdCampo { get; set; }
        public String Valor { get; set; }
    }
}