using System;
using System.Collections.Generic;

namespace GOB.SPF.ConecII.Entities
{
    public class Solicitud : Request.RequestBase
    {
        public Solicitud()
        {
            Cliente = new Cliente();
        }
        public int Identificador { get; set; }
        public int IdTipoSolicitud { get; set; }
        public int IdCliente { get; set; }
        public int Folio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int DocumentoSoporte { get; set; }
        public int? Minuta { get; set; }
        public List<Servicio> Servicios { get; set; }
        public Servicio Servicio { get; set; }
        public Amatzin.Documento Documento { get; set; }
        public Cliente Cliente { get; set; }
        public bool? Cancelado { get; set; }
        public Estatus Estatus { get; set; }
        public TipoSolicitud TipoSolicitud { get; set; }
    }
}
