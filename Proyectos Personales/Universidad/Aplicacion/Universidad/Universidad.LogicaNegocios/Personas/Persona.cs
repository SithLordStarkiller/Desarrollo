using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;
using Universidad.LogicaNegocios.Personas;

namespace Universidad.LogicaNegocios.Personas
{
    public class Persona
    {
        public PER_PERSONAS InsertarPersona(PER_CAT_TELEFONOS personaTelefonos,
            PER_MEDIOS_ELECTRONICOS personaMediosElectronicos, PER_FOTOGRAFIA personaFotografia, PER_PERSONAS persona)
        {
            var mediosElectronicos = new AccesoDatos.Personas.MediosElectronicos().InsertaMediosElectronicosLinq(personaMediosElectronicos);

            return null;
        }
    }
}
