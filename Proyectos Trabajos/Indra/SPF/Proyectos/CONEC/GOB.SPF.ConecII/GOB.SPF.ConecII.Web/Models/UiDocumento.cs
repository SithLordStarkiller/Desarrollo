using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiDocumento : UiEntity
    {
        public int Numero { get; set; }
        public int Identificador { get; set; }
        public UiTiposDocumento TipoDocumento { get; set; }
        public int IdTipoDocumento { get; set; }
        public int DocumentoSoporte { get; set; }
        public string Base64 { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool IsActive { get; set; }
        public Guid DocumentId { get; set; }
    }
}