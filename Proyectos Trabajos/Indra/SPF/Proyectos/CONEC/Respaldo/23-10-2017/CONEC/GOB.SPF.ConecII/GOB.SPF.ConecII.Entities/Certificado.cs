using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    /// <summary>
    /// Representa la informcion de un certificado (.cer)
    /// </summary>
    /// <remarks>
    /// 20/10/2017
    /// Horacio Torres
    /// Creado
    /// </remarks>
    public class Certificado : TEntity
    {
        public string Serie { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }
}
