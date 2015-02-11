using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.Personas
{
    public class Personas
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public PER_PERSONAS InsertaFotografiaLinq(PER_PERSONAS persona)
        {
            using (var r = new Repositorio<PER_PERSONAS>())
            {
                return r.Agregar(persona);
            }
        }
    }
}
