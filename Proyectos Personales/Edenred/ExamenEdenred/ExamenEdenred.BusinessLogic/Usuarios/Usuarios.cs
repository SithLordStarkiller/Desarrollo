namespace ExamenEdenred.BusinessLogic.Usuarios
{
    using System.Collections.Generic;
    using Entities.Entities;

    public class Usuarios
    {
        public UsUsuarios ObtenUsuario(int usuarioId)
        {
            return new DataAccess.Usuarios.Usuarios().ObtenUsuario(usuarioId);
        }

        public bool GuardaArchivo(string texto)
        {
            return new DataAccess.Usuarios.Usuarios().GuardaArchivo(texto);
        }

        public List<UsCatTipoUsuarios> ObtenTipoUsuarios()
        {
            return new DataAccess.Usuarios.Usuarios().ObtenTiposUsuarios();
        }
    }
}
