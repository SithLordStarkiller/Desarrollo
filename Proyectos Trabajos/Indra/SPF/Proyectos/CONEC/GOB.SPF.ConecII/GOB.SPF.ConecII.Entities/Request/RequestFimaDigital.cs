using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities.Amatzin;

namespace GOB.SPF.ConecII.Entities.Request
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 13/10/2017
    /// Horacio Torres
    /// Creado
    /// </remarks>
    public class RequestFimaDigital : RequestBase
    {
        public int Identificador { get; set; }
        public Enumeracion.DocumentoFirma IdTipo { get; set; }
        public Documento Certificado { get; set; }
        public Documento Llave { get; set; }
        public string Password { get; set; }
      
    }
}
