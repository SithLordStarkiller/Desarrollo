namespace Universidad.Entidades.ControlUsuario
{
    public class Sesion
    {
        public string Conexion { get; set; }
        public bool RecordarSesion { get; set; }
        public bool RecordarContrasena { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}
