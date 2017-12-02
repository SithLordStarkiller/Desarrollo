using GOB.SPF.ConecII.Entities.Amatzin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class FirmaDocumento
    {
        public string Password { get; set; }

        public TipoDocumento TipoDocumento { get; set; }

        public Documento DocumentoCer { get; set; }

        public Documento DocumentoKey { get; set; }
    }
}
