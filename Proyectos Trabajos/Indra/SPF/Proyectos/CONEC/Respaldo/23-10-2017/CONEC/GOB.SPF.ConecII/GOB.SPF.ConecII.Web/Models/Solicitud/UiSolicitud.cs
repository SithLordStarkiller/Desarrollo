using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Web.Models.Solicitud
{
    public class UiSolicitud : UiEntity
    { 
        public int Identificador { get; set; }
        public int Folio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int DocumentoSoporte { get; set; }
        public int? Minuta { get; set; }
        public List<UiServicio> Servicios { get; set; }
        public UiServicio Servicio { get; set; }
        public UiDocumento Documento { get; set; }
        public UiCliente Cliente { get; set; }
        public bool? Cancelado { get; set; }
        public UiTipoSolicitud TipoSolicitud { get; set; }

    }
}
