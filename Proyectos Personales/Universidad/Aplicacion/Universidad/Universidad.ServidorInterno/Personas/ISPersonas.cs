using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Universidad.Entidades;

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
    }
}
