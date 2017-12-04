using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Cuota : Request.RequestBase
    {
        public int Identificador { get; set; }
        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }
        public int IdReferencia { get; set; }
        public string Referencia { get; set; }
        public int IdDependencia { get; set; }
        public string Dependencia { get; set; }
        public string DescripcionDependencia { get; set; }
        public int IdJerarquia { get; set; }
        public string Jerarquia { get; set; }
        public string Concepto { get; set; }
        public int IdGrupoTarifario { get; set; }
        public string GrupoTarifario { get; set; }
        public decimal CuotaBase { get; set; }
        public int IdMedidaCobro { get; set; }
        public string MedidaCobro { get; set; }
        public decimal Iva { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public DateTime FechaEntradaVigor { get; set; }
        public DateTime FechaTermino { get; set; }
        public DateTime FechaPublicaDof { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }

    }
}
