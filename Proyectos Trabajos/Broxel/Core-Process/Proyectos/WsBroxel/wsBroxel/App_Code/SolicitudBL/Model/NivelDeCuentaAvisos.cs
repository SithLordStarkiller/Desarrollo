namespace wsBroxel.App_Code.SolicitudBL.Model
{
    /// <summary>
    /// Clase contenedora de la configuración de avisos de nivel de cuenta.
    /// </summary>
    public class NivelDeCuentaAvisos
    {
        /// <summary>
        /// Identificador del Aviso de Nivel de Cuenta.
        /// </summary>
        public int IdAviso { set; get; }
        /// <summary>
        /// Id del Nivel de Cuenta.
        /// </summary>
        public int IdCatNivel { set; get; }
        /// <summary>
        /// Porcentaje limite estimado para el envio de avisos.
        /// </summary>
        public decimal Porcentaje { set; get; }
        /// <summary>
        /// Limite establecido dependiendo del Nivel.
        /// </summary>
        public decimal Limite { set; get; }
        /// <summary>
        /// Cuerpo del mensaje SMS a enviar.
        /// </summary>
        public string SMSBody { set; get; }
        /// <summary>
        /// Remitente del Mail.
        /// </summary>
        public string DeMail { set; get; }
        /// <summary>
        /// Asunto del Mail.
        /// </summary>
        public string Asunto { set; get; }
        /// <summary>
        /// Contraseña del Remitente.
        /// </summary>
        public string DePwd { set; get; }
        /// <summary>
        /// Alias del Remitente.
        /// </summary>
        public string DeAlias { set; get; }
        /// <summary>
        /// Cuerpo del Mail a enviar.
        /// </summary>
        public string MailBody { set; get; }
    }
}