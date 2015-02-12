
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.Personas
{
    public class Telefonos
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public PER_CAT_TELEFONOS InsertaTelefonosLinq(PER_CAT_TELEFONOS personaTelefonos)
        {
            using (var r = new Repositorio<PER_CAT_TELEFONOS>())
            {
                return r.Agregar(personaTelefonos);
            }
        }

         public Task<PER_CAT_TELEFONOS> InsertaDireccionesLinqAsync(PER_CAT_TELEFONOS personaTelefonos)
         {
             return Task.Run(() =>
             {
                 using (var r = new Repositorio<PER_CAT_TELEFONOS>())
                 {
                     return r.Agregar(personaTelefonos);
                 }
             });
         }
        
    }
}
