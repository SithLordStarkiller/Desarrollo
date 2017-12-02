using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Amatzin.Entities
{
    public class Archivo
    {
        public long ArchivoId { get; set; }
        public long ArchivoIdParent { get; set; }
        public string Nombre { get; set; }
        public string NombreSistema { get; set; }
        public string Extension { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Directorio { get; set; }
        public string Referencia { get; set; }
        public TableStorage Stream { get; set; }

        public string Base64 { get; set; }

    }
}
