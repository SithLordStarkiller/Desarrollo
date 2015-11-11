using System;
using System.Collections.Generic;
using Universidad.Entidades;
using Universidad.Entidades.Personas;
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

        public PER_PERSONAS BuscarPersona(string idPersonaLink)
        {
            return new Persona().BuscarPersona(idPersonaLink);
        }

        public DatosCompletosPersona BuscarPersonaCompleta(string idPersonaLink)
        {
            return new Persona().BuscarPersonaCompleta(idPersonaLink);
        }

        public List<PER_PERSONAS> ObtenListaPersonas()
        {
            return new Persona().ObtenListaPersonas();
        }

        public List<PER_PERSONAS> ObtenListaPersonaFiltro(string idPersona, DateTime? fechaInicio, DateTime? fechaFinal,int? idTipoPersona)
        {
            return new Persona().ObtenListaPersonasFiltro(idPersona,fechaInicio,fechaFinal,idTipoPersona);
        }

        public List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatTipoPersona();
        }

        public DIR_DIRECCIONES ObtenDirecciones(PER_PERSONAS persona)
        {
            return new Persona().ObtenDirecciones(persona);
        }

        public PER_CAT_TELEFONOS ObtenTelefonos(PER_PERSONAS persona)
        {
            return new Persona().ObtenTelefonos(persona);
        }

        public PER_MEDIOS_ELECTRONICOS ObtenMediosElectronicos(PER_PERSONAS personas)
        {
            return new Persona().ObtenMediosElectronicos(personas);
        }

        public PER_FOTOGRAFIA ObtenFotografia(PER_PERSONAS personas)
        {
            return new Persona().ObtenFotografia(personas);
        }

        public PER_CAT_TIPO_PERSONA ObtenTipoPersona(int idTipoPersona)
        {
            return new Persona().ObtenTipoPersona(idTipoPersona);
        }

        public PER_CAT_NACIONALIDAD ObtenPersonaPais(int idPersonaPais)
        {
            return new Persona().ObtenPersonaPais(idPersonaPais);
        }
    }
}
