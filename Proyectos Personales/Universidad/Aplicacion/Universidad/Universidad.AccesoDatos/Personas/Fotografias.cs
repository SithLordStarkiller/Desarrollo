using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.Personas
{
    public class Fotografias
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public PER_FOTOGRAFIA InsertaFotografiaLinq(PER_FOTOGRAFIA personaFotografia)
        {
            using (var r = new Repositorio<PER_FOTOGRAFIA>())
            {
                return r.Agregar(personaFotografia);
            }
        }
    }
}
