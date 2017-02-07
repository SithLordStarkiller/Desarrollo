namespace Broxel.Helpers.CustomExceptions
{
    using System;

    public class UserNotFindException : Exception
    {
        public string Usuario { get; }
        public string Contrasena { get; }

        public UserNotFindException(string usuario, string contrasena) : base("No se encontro al usuario o fallo contraseña")
        {
            Usuario = usuario;
            Contrasena = contrasena;
        }
    }
}
