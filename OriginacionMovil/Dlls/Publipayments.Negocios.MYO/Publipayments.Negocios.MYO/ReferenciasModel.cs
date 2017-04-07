using System;

namespace Publipayments.Negocios.MYO
{
    public class ReferenciasModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Url { get; set; }
        public string Valor { get; set; }
        public string Comentario { get; set; }
        public string IdDocumento { get; set; }
    }

    public class ResultModel
    {
        public string Dictamen { get; set; }
        public bool Valido { get; set; }
    }

    public class DocumentosModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Url { get; set; }
        public string Valor { get; set; }
        public string Comentario { get; set; }
        public string IdDocumento { get; set; }
    }

}
