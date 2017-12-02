using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Modulo : Request.RequestBase
    {
        public int Id { get; set; }
        public int IdPadre { get; set; }
        public IList<IModulo> SubModulos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
        public IList<IRol> RolesAutorizados{ get; set; }
    }
}
