using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;
using Universidad.AccesoDatos;

namespace Universidad.LogicaNegocios.Personas
{
    public class MediosElectronicos
    {
        public bool ExisteCorreoUniversidad(string correo)
        {
            return new AccesoDatos.Personas.MediosElectronicos().ExisteCorreoUniversidadLinq(correo);
        }
    }
}
