using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Integrante : Request.RequestBase
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Correo { get; set; }
        public string CorreoPersonal { get; set; }
        public string IdArea { get; set; }
        public string Area { get; set; }
        public int IdJerarquia { get; set; }
        public string Jerarquia { get; set; }
        //public bool IsActive { get; set; } 
        public bool IsActive = true;

    }
}
