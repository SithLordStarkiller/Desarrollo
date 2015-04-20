using Universidad.Entidades;
using Universidad.LogicaNegocios.Personas;

namespace Universidad.ServidorInterno.Personas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SPersonas" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SPersonas.svc or SPersonas.svc.cs at the Solution Explorer and start debugging.
    public class SPersonas : ISPersonas
    {
        public bool ExisteCorreoUniversidad(string correo)
        {
            return new MediosElectronicos().ExisteCorreoUniversidad(correo);
        }

        public PER_MEDIOS_ELECTRONICOS InsertaMediosElectronicos(PER_MEDIOS_ELECTRONICOS mediosElectronicos)
        {
            return new MediosElectronicos().InsertaMediosElectronicos(mediosElectronicos);
        }

        public PER_PERSONAS InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona,
            DIR_DIRECCIONES personaDirecciones)
        {
            return new Persona().InsertarPersona(personaTelefonos, personaMediosElectronicos, personaFotografia, persona,
                personaDirecciones);
        }

    }
}
