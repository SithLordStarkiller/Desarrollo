using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces;

namespace GOB.SPF.ConecII.Entities
{
    public class Cotizacion:TEntity
    {
        public Servicio Servicio { get; set; }
        public List<CotizacionDetalle> Detalles { get; set; }
        public int IdCotizacionAnterior { get; set; }
        public string Folio { get; set; }
        public bool EsValida { get; set; }
        public int Documento { get; set; }
        public byte[] Firma { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }

    }
}
