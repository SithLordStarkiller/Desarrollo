using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PubliPayments.Entidades.MYO
{
    public class InfoOrdenMYOModel
    {
        [Key]
        public string Etiqueta { get; set; }
        public int idVisita { get; set; }
        public string mayorAlLimite { get; set; }
        public string nombreUsuario { get; set; }
        public int idUsuario { get; set; }
        public int idFlock { get; set; }
    }
}
