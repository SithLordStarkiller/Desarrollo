using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class FormatoPlantilla
    {
        public FormatoPlantilla()
        {}

        public virtual TiposDocumento TiposDocumento { get; set; }
        
        public virtual PartesDocumento PartesDocumento { get; set; }

        public virtual List<Parrafos> ListaParrafos { get; set; }

        public virtual List<Instituciones> ListaInstituciones { get; set; }
    }
}
