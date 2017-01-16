using System;

namespace PubliPayments.Models
{
    public class GvDespachosModel
    {
        public int IdDominio { get; set; }
        public string NomCorto { get; set; }
        public DateTime FechaAlta { get; set; }
        public string EstatusTxt { get; set; }
        public string Menu { get; set; }
    }
}