using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Solicitudes : Request.RequestBase
    {
        public Solicitudes()
        {
            RegimenFiscal = new RegimenFiscal();
            Sector = new Sector();
            TipoSolicitud = new TipoSolicitud();
        }
        public int Identificador { get; set; }
        public int IdCliente { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCorto { get; set; }
        public string RFC { get; set; }
        public int IdRegimenFiscal { get; set; }
        public List<RegimenFiscal> RegimenFiscales { get; set; }
        public RegimenFiscal RegimenFiscal { get; set; }
        public string NombreRegimenFiscal { get; set; }
        public int IdSector { get; set; }
        public List<Sector> Sectores { get; set; }
        public Sector Sector { get; set; }
        public string NombreSector { get; set; }
        public int IdTipoSolicitud { get; set; }
        public List<TipoSolicitud> TiposSolicitud { get; set; }
        public TipoSolicitud TipoSolicitud { get; set; }
        public string NombreTipoSolicitud { get; set; }
        public int DocumentoSoporte { get; set; }
        public string Folio { get; set; }
        public int Minuta { get; set; }
        public bool Cancelado { get; set; }
    }
}