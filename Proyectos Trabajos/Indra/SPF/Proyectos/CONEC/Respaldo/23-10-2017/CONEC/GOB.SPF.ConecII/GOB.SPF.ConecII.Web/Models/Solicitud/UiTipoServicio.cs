using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTipoServicio:UiEntity
    {
        public  int Identificador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Clave { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}