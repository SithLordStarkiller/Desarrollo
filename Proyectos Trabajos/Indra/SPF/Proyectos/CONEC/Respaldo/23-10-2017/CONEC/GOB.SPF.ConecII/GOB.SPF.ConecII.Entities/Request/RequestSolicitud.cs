using GOB.SPF.ConecII.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestSolicitud : RequestBase
    {
        public Solicitud Item { get; set; }
        public SolicitudesDTO DTO { get; set; }
        public ConfiguracionCampoServicio ConfiguracionServicio { get; set; }
    }
}
