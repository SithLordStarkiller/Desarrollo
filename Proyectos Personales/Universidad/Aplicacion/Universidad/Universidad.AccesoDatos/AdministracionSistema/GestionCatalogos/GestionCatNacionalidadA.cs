using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class GestionCatNacionalidadA
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        #region Obten catalogo nacionalidad

        public List<PER_CAT_NACIONALIDAD> ObtenNacionalidadLinq()
        {
            using (var r = new Repositorio<PER_CAT_NACIONALIDAD>())
            {
                return r.TablaCompleta();
            }
        }

        #endregion
    }
}
