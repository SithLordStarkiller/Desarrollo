using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PubliPayments.Negocios.Originacion
{
    class Documento
    {
        [Key]

        public String NombreDocumento { get; set; }
        public int Fase { get; set; }
        public bool Cargado { get; set; }
        


    }
}
