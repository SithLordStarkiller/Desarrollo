using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class GestionCatDirecciones
    {
        private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public List<DIR_CAT_COLONIAS> ObtenColoniasPorCpLinq(int codigoPostal)
        {
            using (var Aux = new Repositorio<DIR_CAT_COLONIAS>())
            {
                return Aux.Filtro(r => r.CODIGOPOSTAL == codigoPostal);
            }
        }

        public List<DIR_CAT_ESTADO> ObtenCatEstadosLinq()
        {
            using (var aux = new Repositorio<DIR_CAT_ESTADO>())
            {
                return aux.TablaCompleta();
            }
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenDelgMunicipiosLinq(int estado)
        {
            using (var aux = new Repositorio<DIR_CAT_DELG_MUNICIPIO>())
            {
                return aux.Filtro(r => r.IDESTADO == estado);
            }
        }

        public List<DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio)
        {
            using (var aux = new Repositorio<DIR_CAT_COLONIAS>())
            {
                return aux.Filtro(r => r.IDESTADO == estado && r.IDMUNICIPIO == municipio);
            }
        }
    }
}
