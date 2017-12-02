using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class NotaDeCredito : TEntity
    {
        public string Folio { get; set; }
        public int IdRecibo { get; set; }
        public Recibo Recibo { get; set; }

    }
}
