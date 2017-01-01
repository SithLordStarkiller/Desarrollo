namespace Broxel.Entities.Entidades
{
    public class UsUsuarios
    {
        public int IdUsuario { get; set; }
        public int? IdEstatus { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public UsUsuarios()
        {
        }

        public UsUsuarios(int idUsuario, int idEstatus, string usuario, string contrasena)
        {
            IdUsuario = idUsuario;
            IdEstatus = idEstatus;
            Usuario = usuario;
            Contrasena = contrasena;
        }
    }
}
