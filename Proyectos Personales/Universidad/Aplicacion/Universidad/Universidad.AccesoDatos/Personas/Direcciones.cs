using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.Personas
{
    public class Direcciones
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public DIR_DIRECCIONES InsertaDireccionesLinq(DIR_DIRECCIONES direcciones)
        {
            using (var r = new Repositorio<DIR_DIRECCIONES>())
            {
                return r.Agregar(direcciones);
            }
        }

        public Task<DIR_DIRECCIONES> InsertaDireccionesLinqAsync(DIR_DIRECCIONES direcciones)
        {
            return Task.Run(() =>
            {
                using (var r = new Repositorio<DIR_DIRECCIONES>())
                {
                    return r.Agregar(direcciones);
                }
            });
        }
    }
}
