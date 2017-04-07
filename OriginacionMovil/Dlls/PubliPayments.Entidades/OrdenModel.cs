namespace PubliPayments.Entidades
{
    public class OrdenModel
    {
        public int IdOrden { get; set; }
        public string NumCred { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioPadre { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdDominio { get; set; }
        public int IdVisita { get; set; }
        public string FechaAlta { get; set; }
        public int Estatus { get; set; }
        public string FechaModificacion { get; set; }
        public string FechaEnvio { get; set; }
        public string FechaRecepcion { get; set; }
        public string Auxiliar { get; set; }
        public int IdUsuarioAnterior { get; set; }
        public string Tipo { get; set; }
        public string Usuario { get; set; }
        public string UsuarioAnterior { get; set; }
        public string Ruta { get; set; }
        public bool EnviarMovil { get; set; }
        
    }
}
