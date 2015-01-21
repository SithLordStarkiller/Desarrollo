using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;
namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class GestionCatTipoPersona
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        #region Metodos Extraccion

        public List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersonaLinq()
        {
            using (var r = new Repositorio<PER_CAT_TIPO_PERSONA>())
            {
                return r.TablaCompleta();
            }
        }

        #endregion
    }
}
