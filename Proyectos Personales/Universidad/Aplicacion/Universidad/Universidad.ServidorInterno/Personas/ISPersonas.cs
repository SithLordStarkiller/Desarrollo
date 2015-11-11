using System;
using System.Collections.Generic;
using System.ServiceModel;
using Universidad.Entidades;
using Universidad.Entidades.Personas;

namespace Universidad.ServidorInterno.Personas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISPersonas" in both code and config file together.
    [ServiceContract]
    public interface ISPersonas
    {
        [OperationContract]
        bool ExisteCorreoUniversidad(string correo);

        [OperationContract]
        PER_MEDIOS_ELECTRONICOS InsertaMediosElectronicos(PER_MEDIOS_ELECTRONICOS mediosElectronicos);

        [OperationContract]
        PER_PERSONAS InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona,
            DIR_DIRECCIONES personaDirecciones);

        [OperationContract]
        PER_PERSONAS BuscarPersona(string idPersonaLink);

        [OperationContract]
        DatosCompletosPersona BuscarPersonaCompleta(string idPersonaLink);

        [OperationContract]
        List<PER_PERSONAS> ObtenListaPersonas();

        [OperationContract]
        List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona();

        [OperationContract]
        List<PER_PERSONAS> ObtenListaPersonaFiltro(string idPersona, DateTime? fechaInicio, DateTime? fechaFinal, int? idTipoPersona);

        [OperationContract]
        DIR_DIRECCIONES ObtenDirecciones(PER_PERSONAS persona);

        [OperationContract]
        PER_CAT_TELEFONOS ObtenTelefonos(PER_PERSONAS persona);

        [OperationContract]
        PER_MEDIOS_ELECTRONICOS ObtenMediosElectronicos(PER_PERSONAS personas);

        [OperationContract]
        PER_FOTOGRAFIA ObtenFotografia(PER_PERSONAS personas);

        [OperationContract]
        PER_CAT_TIPO_PERSONA ObtenTipoPersona(int idTipoPersona);

        [OperationContract]
        PER_CAT_NACIONALIDAD ObtenPersonaPais(int idPersonaPais);
    }
}
