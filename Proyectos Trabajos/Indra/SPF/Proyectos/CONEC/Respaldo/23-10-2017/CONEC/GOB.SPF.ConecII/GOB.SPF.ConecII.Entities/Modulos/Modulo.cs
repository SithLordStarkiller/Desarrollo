using GOB.SPF.ConecII.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Modulos
{
    public class Modulo : IModulo
    {
        public Modulo()
        {
            RolesAutorizados = new List<string>();
            SubModulos = new List<Modulo>();
        }
        public string Accion { get; set; }
        public bool Activo { get; set; }
        public string Controlador { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime FechaInicial { get; set; }
        public int Id { get; set; }
        public int? IdPadre { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<string> RolesAutorizados { get; set; }
        
        public IEnumerable<IModulo> SubModulos { get; set; }
    }
}
