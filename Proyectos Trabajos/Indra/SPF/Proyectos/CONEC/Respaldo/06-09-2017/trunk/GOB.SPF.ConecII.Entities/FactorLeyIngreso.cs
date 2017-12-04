using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class FactorLeyIngreso : TEntity
    {
        public override int Identificador { get; set; }
        public string NombreLI { get; set; }
        public string DescLI { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public int IdAnio { get; set; }
        public int IdMes { get; set; }
        public decimal Factor { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}