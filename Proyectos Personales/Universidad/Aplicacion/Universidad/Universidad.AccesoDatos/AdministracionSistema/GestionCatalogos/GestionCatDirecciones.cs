using System.Collections.Generic;
using System.Linq;
using Universidad.Entidades;

namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    public class GestionCatDirecciones
    {
        //private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public List<DIR_CAT_COLONIAS> ObtenColoniasPorCpLinq(int codigoPostal)
        {
            using (var aux = new Repositorio<DIR_CAT_COLONIAS>())
            {
                return aux.Filtro(r => r.CODIGOPOSTAL == codigoPostal);
            }
        }

        public List<DIR_CAT_ESTADO> ObtenCatEstadosLinq()
        {
            using (var aux = new Repositorio<DIR_CAT_ESTADO>())
            {
                return aux.TablaCompleta();
            }
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenCatMunicipiosLinq(int estado)
        {
            using (var aux = new Repositorio<DIR_CAT_DELG_MUNICIPIO>())
            {
                return aux.Filtro(r => r.IDESTADO == estado);
            }
        }

        public List<DIR_CAT_COLONIAS> ObtenColoniasLinq(int estado, int municipio)
        {
            using (var aux = new Repositorio<DIR_CAT_COLONIAS>())
            {
                return aux.Filtro(r => r.IDESTADO == estado && r.IDMUNICIPIO == municipio);
            }
        }

        public DIR_CAT_COLONIAS ObtenCodigoPostalLinq(int estado, int municipio, int colonia)
        {
            using (var aux = new Repositorio<DIR_CAT_COLONIAS>())
            {
                return aux.Filtro(r => r.IDESTADO == estado && r.IDMUNICIPIO == municipio && r.IDCOLONIA == colonia).FirstOrDefault();
            }
        }

        public List<DIR_CAT_COLONIAS> ObtenCatalogoColonias()
        {
            using (var aux = new Repositorio<DIR_CAT_COLONIAS>())
            {
                return aux.TablaCompleta();
            }
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenCatalogoMunicipios()
        {
            using (var aux = new Repositorio<DIR_CAT_DELG_MUNICIPIO>())
            {
                return aux.TablaCompleta();
            }
        }

        public List<DIR_CAT_ESTADO> ObtenCatalogoEstados()
        {
            using (var aux = new Repositorio<DIR_CAT_ESTADO>())
            {
                return aux.TablaCompleta();
            }
        }

        public List<DIR_CAT_TIPO_ASENTAMIENTO> ObtenCatalogosTipoAsentamiento()
        {
            using (var aux = new Repositorio<DIR_CAT_TIPO_ASENTAMIENTO>())
            {
                return aux.TablaCompleta();
            }
        }
    }
}
