using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Cliente
    {
        public int Identificador { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public string RegimenFiscal { get; set; }
        public int? IdSector { get; set; }
        public string Sector { get; set; }
        public string RFC { get; set; }
        public bool IsActive { get; set; }
    }
}
