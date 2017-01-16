namespace PubliPayments.Entidades
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public int IdDominio { get; set; }
        public int IdRol { get; set; }
        public int Estatus { get; set; }
        public int IdPadre { get; set; }
        public int? Intentos { get; set; }
        
        public string Padre { get; set; }
        public string NombreDominio { get; set; }
        public string NomCorto { get; set; }
        public string Usuario { get; set; }
        public string NombreRol { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }

        public string Alta { get; set; }
        public string UltimoLogin { get; set; }
        public string Bloqueo { get; set; }
        public bool EsCallCenterOut { get; set; }

        public string[] Extra { get; set; }



        public UsuarioModel(int idUsuario,int idDominio,int idRol,int idPadre,string usuario,string nombre,string email,string password,string alta,string ultimoLogin,string bloqueo)
        {
            IdUsuario = idUsuario;
            IdDominio = idDominio;
            IdRol = idRol;
            IdPadre = idPadre;
            Usuario = usuario;
            Nombre = nombre;
            Email = email;
            Password = password;
            Alta = alta;
            UltimoLogin = ultimoLogin;
            Bloqueo = bloqueo;
        }

        public UsuarioModel()
        {
        }
    }

}
