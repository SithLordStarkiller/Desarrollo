namespace ExamenEdenred.Services.Usuarios
{
    using Entities.Entities;
    
    public class Usuarios : IUsuarios
    {
        public UsUsuarios ExisteUsuario(int idUsuario)
        {
            return new BusinessLogic.Usuarios.Usuarios().ObtenUsuario(idUsuario);
        }

        public void GuardaArchivo(string archivo)
        {
            
        }
    }
}
