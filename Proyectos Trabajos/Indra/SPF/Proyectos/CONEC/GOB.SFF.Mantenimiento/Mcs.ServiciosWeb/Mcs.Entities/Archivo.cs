using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities
{
    public class Archivo
    {
        public string Nombre { get; set; }
        public byte[] ArchivoBinario { get; set; }
        public string ArchivoBase64 { get; set; }

        public string Directorio { get; set; }

        public string Modulo { get; set; }
        public string Extension { get; set; }
    }
}
