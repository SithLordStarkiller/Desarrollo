namespace wsBroxel
{
    /// <summary>
    /// Response de la Renominación Externa.
    /// </summary>
    public class RenominacionExternaResponse
    {
        /// <summary>
        /// Folio resultado de la renominación externa.
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Código de respuesta de la renominación externa: 0-Éxito, 1-Error.
        /// </summary>
        public int Respuesta { get; set; }
        /// <summary>
        /// Descripción acerca de la renominación externa.
        /// </summary>
        public string Descripcion { get; set; }
    }
}