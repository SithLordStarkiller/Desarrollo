using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class FactorActividadEconomica : Request.RequestBase
  {
    public int Identificador { get; set; }
    public string DescFacActividadEconomica { get; set; }
    public int IdFraccion { get; set; }
    public int IdFactor { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFinal { get; set; }
    public bool Activo { get; set; }
  }
}