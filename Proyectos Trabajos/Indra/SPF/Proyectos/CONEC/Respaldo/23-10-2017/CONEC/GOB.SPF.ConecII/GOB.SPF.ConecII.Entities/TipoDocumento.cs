using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class TipoDocumento : TEntity
    {
        public override int Identificador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
        public int IdActividad { get; set; }
        public string Actividad { get; set; }
        public bool Confidencial { get; set; }
    }
}