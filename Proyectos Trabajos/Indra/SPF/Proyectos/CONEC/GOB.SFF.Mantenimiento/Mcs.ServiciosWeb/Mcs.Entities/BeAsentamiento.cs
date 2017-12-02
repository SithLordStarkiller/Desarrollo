
namespace Mcs.Entities
{
    /// <summary>
    /// Clase entidad de la tabla Asentamiento.
    /// </summary>
    public class BeAsentamiento 
    {
        /// <summary>
        /// Clave del Asentamiento.
        /// </summary>
        public int IdAsentamiento { get; set; }
        /// <summary>
        /// Nombre del Asentamiento.
        /// </summary>
        public string Asentamiento { get; set; }
        /// <summary>
        /// Clave del Tipo de Asentamiento.
        /// </summary>
        public int IdTipoAsentamiento { get; set; }
        /// <summary>
        /// Nombre del Tipo de Asentamiento.
        /// </summary>
        public string TipoAsentamiento { get; set; }
        /// <summary>
        /// Clave de la Entidad Federativa.
        /// </summary>
        public int IdEstado { get; set; }
        /// <summary>
        /// Nombre de la Entidad Federativa.
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// Código Postal del Asentamiento.
        /// </summary>
        public string CodigoPostal { get; set; }
        /// <summary>
        /// Clave del Municipio relacionado con el Asentamiento.
        /// </summary>
        public int IdMunicipio { get; set; }
        /// <summary>
        /// Nombre del Municipio.
        /// </summary>
        public string Municipio { get; set; }
    }
}
