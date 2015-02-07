using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.Personas
{
    public class MediosElectronicos
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public bool ExisteCorreoUniversidadLinq(string correo)
        {
            List<PER_MEDIOS_ELECTRONICOS> lista;

            using (var aux = new Repositorio<PER_MEDIOS_ELECTRONICOS>())
            {
                lista = aux.Filtro(r => r.CORREO_ELECTRONICO_UNIVERSIDAD == correo);
            }

            return lista.Count != 0;
        }

        public PER_MEDIOS_ELECTRONICOS InsertaMediosElectronicosLinq(PER_MEDIOS_ELECTRONICOS mediosElectronicos)
        {
            using (var r = new Repositorio<PER_MEDIOS_ELECTRONICOS>())
            {
                return r.Agregar(mediosElectronicos);
            }
        }
    }
}
