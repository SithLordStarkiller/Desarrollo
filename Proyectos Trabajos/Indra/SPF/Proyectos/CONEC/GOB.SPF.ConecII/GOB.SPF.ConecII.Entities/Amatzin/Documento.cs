using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Amatzin
{
    public class Documento
    {
        /// <summary>
        /// Identificador del archivo
        /// </summary>
        public int ArchivoId { get; set; }

        /// <summary>
        /// Si el archivo se modifica, se envia el Identificador del archivo anterior.
        /// </summary>
        public int ArchivoIdParent { get; set; }

        public string Base64 { get; set; }

        /// <summary>
        /// Directorio del archivo
        /// </summary>
        public string Directorio { get; set; }

        /// <summary>
        /// Extensión del archivo.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Fecha en que se registro el archivo
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Nombre del archivo.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Sistema que lo esta utilizando
        /// </summary>
        public string NombreSistema { get; set; }

        /// <summary>
        /// Desde que aplicación se esta utilizando el servicio. (Conec, servicio, consola, etc)
        /// </summary>
        public string Referencia { get; set; }
        public TipoDocumento TipoDocumento { get; set; }

    }
}
