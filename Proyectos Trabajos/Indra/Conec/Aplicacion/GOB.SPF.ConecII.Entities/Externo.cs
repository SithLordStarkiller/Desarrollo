using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Externo:TEntity
    {
        public int IdCliente { get; set; }
        public int IdTipoPersona { get; set; }
        public TipoContacto TipoContacto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Cargo { get; set; }
        public bool Activo { get; set; }

        public List<Telefono> Telefonos { get; set; }
        public List<Correo> Correos { get; set; }
    }
}
