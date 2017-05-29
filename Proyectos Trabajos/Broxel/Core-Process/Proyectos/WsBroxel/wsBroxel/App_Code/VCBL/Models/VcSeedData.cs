namespace wsBroxel.App_Code.VCBL.Models
{
    /// <summary>
    /// Datos de la semilla(seed).
    /// </summary>
    public class VcSeedData
    {
        /// <summary>
        /// Valor de la semilla(seed).
        /// </summary>
        public string Seed { set; get; }

        /// <summary>
        /// Status que identifica si fue enviada la semilla(seed) con éxito.
        /// </summary>
        public bool Status { set; get; }

        /// <summary>
        /// Descripción del status de la semilla(seed).
        /// </summary>
        public string DescStatus { set; get; }
    }
}