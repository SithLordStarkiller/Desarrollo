using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class SolicitudesDTO
    {
        public SolicitudesDTO()
        {
            RegimenFiscales = new List<RegimenFiscal>();
            Sectores = new List<Sector>();
            TiposSolicitud = new List<TipoSolicitud>();
        }

        public int Identificador { get; set; }
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public string RFC { get; set; }
        public List<RegimenFiscal> RegimenFiscales { get; set; }
        public RegimenFiscal RegimenFiscal { get; set; }
        public List<Sector> Sectores { get; set; }
        public Sector Sector { get; set; }
        public List<TipoSolicitud> TiposSolicitud { get; set; }
        public TipoSolicitud TipoSolicitud { get; set; }
        public int DocumentoSoporte { get; set; }
        public string Folio { get; set; }
        public int Minuta { get; set; }
        public bool Cancelado { get; set; }

    }
}
