using GOB.SPF.ConecII.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Plantilla
{
    public partial class UiFormatoPlantilla : UiEntity
    {
        public UiFormatoPlantilla()
        {}

        public virtual UiTiposDocumento TiposDocumento { get; set; }
        
        public virtual UiPartesDocumento PartesDocumento { get; set; }

        public virtual List<UiParrafos> ListaParrafos { get; set; }

        public virtual List<UiInstituciones> ListaInstituciones { get; set; }
    }
}
