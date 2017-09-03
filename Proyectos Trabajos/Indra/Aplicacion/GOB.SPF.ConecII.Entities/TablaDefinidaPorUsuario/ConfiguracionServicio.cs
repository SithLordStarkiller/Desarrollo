using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    using System.Data;
    using System.Data.SqlClient;
    public class ConfiguracionServicio : Request.RequestBase
    {
        public int IdTipoServicio { get; set; }
        public string IdCentroCostos { get; set; }
        public int IdRegimenFiscal { get; set; }
        public int IdTipoPago { get; set; }
        public int IdActividad { get; set; }
        public int IdTipoDocumento { get; set; }
        public decimal Tiempo { get; set; }
        public bool Aplica { get; set; }
        public bool Obigatoriedad { get; set; }
    }
}
