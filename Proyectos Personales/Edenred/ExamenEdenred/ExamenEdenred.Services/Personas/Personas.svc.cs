namespace ExamenEdenred.Services.Personas
{

    public class Personas : IPersonas
    {
        public bool EliminaPersona(int idPersona)
        {
            return new BusinessLogic.Personas.Personas().EliminaPersona(idPersona);
        }
    }
}
