using System;

namespace GOB.SPF.ConecII.Entities
{
    public class EnteroTesofeCriterio : TEntity
    {
        public DateTime? FechaPresentacionInicial { get; set; }
        public DateTime? FechaPresentacionFinal { get; set; }
        public string RazonSocial { get; set; }
        public string LlavePago { get; set; }
        public long? NumeroOperacion { get; set; }
        public string RFC { get; set; }
        public int? ClaveReferenciaDPA { get; set; }
        public DateTime? FechaCargaInicial { get; set; }
        public DateTime? FechaCargaFinal { get; set; }
    }
}
