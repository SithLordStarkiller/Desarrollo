namespace ExamenEdenred.BusinessLogic.Personas
{
    public class Personas
    {
        public bool EliminaPersona(int idPersona)
        {
            return new DataAccess.Personas.Personas().EliminaPersona(idPersona);
        }
    }
}
