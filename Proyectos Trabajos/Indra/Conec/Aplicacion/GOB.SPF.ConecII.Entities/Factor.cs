using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Factor : Request.RequestBase
    {
        public int Identificador { get; set; }
        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }
        public int IdClasificacionFactor { get; set; }
        public string ClasificadorFactor { get; set; }
        public int IdMedidaCobro { get; set; }
        public string MedidaCobro { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Cuota { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public DateTime FechaEntradaVigor { get; set; }
        public DateTime FechaTermino { get; set; }
        public DateTime FechaPublicacionDof { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
