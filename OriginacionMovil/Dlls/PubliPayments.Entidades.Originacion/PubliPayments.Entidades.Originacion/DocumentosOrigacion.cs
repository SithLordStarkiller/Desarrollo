using System.Collections.Generic;

namespace PubliPayments.Entidades.Originacion
{
    public class DocumentosOrigacion
    {
        public string Credito { get; set; }
        public string Token { get; set; }
        public Dictionary<string, bool> Documentos { get; set; }
    }
}
