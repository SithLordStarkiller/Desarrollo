using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubliPayments.Entidades
{
    public class CatalogoGeneralModel
    {
        public int id { get; set; }
        public string Valor { get; set; }
        public string Llave { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } //ayuda a mantener la fecha de creacion del objeto
    } 
}
