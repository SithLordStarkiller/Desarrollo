namespace ExamenEdenred.Services.Usuarios
{
    using System.Collections.Generic;
    using Entities.Entities;

    public class Usuarios : IUsuarios
    {
        public UsUsuarios ExisteUsuario(int idUsuario)
        {
            return new BusinessLogic.Usuarios.Usuarios().ObtenUsuario(idUsuario);
        }

        public bool GuardaArchivo(string texto)
        {
            return new BusinessLogic.Usuarios.Usuarios().GuardaArchivo(texto);
        }

        public List<UsCatTipoUsuarios> ObtenCatTipoUsuarios()
        {
            return new BusinessLogic.Usuarios.Usuarios().ObtenTipoUsuarios();
        }
    }
}
