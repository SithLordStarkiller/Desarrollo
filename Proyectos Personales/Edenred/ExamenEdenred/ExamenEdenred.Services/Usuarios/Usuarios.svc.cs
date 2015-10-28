using System;
using System.Data;
using System.IO;
using System.Linq;

namespace ExamenEdenred.Services.Usuarios
{
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
    }
}
