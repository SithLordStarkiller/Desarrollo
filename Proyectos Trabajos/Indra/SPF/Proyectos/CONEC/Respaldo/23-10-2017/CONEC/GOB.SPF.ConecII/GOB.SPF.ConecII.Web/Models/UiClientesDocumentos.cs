using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiClientesDocumentos : UiEntity
    {
        public int Identificador { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoDocumento { get; set; }
        public int DocumentoSoporte { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool IsActive { get; set; }
    }
}