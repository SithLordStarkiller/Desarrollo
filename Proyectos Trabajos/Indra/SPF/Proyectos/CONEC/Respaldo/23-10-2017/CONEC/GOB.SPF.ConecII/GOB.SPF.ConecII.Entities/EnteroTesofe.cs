using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class EnteroTesofe
    {
        public long NumeroOperacion { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FechaPresentacion { get; set; }
        public int Sucursal { get; set; }
        public string LlavePago { get; set; }
        public string Banco { get; set; }
        public string MedioRecepcion { get; set; }
        public string Dependencia { get; set; }
        public string Periodo { get; set; }
        public decimal SaldoFavor { get; set; }
        public decimal Importe { get; set; }
        public decimal ParteActualizada { get; set; }
        public decimal Recargos { get; set; }
        public decimal MultaCorreccion { get; set; }
        public decimal Compensacion { get; set; }
        public decimal CantidadPagada { get; set; }
        public int ClaveReferenciaDPA { get; set; }
        public string CadenaDependencia { get; set; }
        public decimal? ImporteIVA { get; set; }
        public decimal? ParteActualizadaIVA { get; set; }
        public decimal? RecargosIVA { get; set; }
        public decimal? MultaCorreccionIVA { get; set; }
        public decimal CantidadPagadaIVA { get; set; }
        public decimal TotalEfectivamentePagado { get; set; }
        public DateTime FechaCarga { get; set; }
    }
}
