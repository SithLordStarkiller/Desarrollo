using System.Collections.Generic;
using Universidad.Entidades;
using Universidad.Entidades.Catalogos;
using Universidad.LogicaNegocios.Personas;


namespace Universidad.ServidorInterno.GestionCatalogos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_GestionCatalogos" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_GestionCatalogos.svc o S_GestionCatalogos.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class S_GestionCatalogos : IS_GestionCatalogos
    {
        public List<US_CAT_TIPO_USUARIO> ObtenTablaUsCatTipoUsuarios()
        {
            return LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.ObtenListaCatTiposUsuario();
        }

        public List<US_CAT_NIVEL_USUARIO> ObtenTablaUsCatNivelUsuario()
        {
            return LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.ObtenListaCatNivelUsuario();
        }

        public List<US_CAT_ESTATUS_USUARIO> ObtenTablaUsCatEstatusUsuario()
        {
            return LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.ObtenListaCatEstatusUsuario();
        }

        public List<PER_CAT_NACIONALIDAD> ObtenCatalogoNacionalidades()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenNacionalidades();
        }

        public US_CAT_TIPO_USUARIO ObtenCatTipoUsuario(int idTipoUsuario)
        {
            return LogicaNegocios.GestionCatalogos.GestionCatalogos.ClassInstance.ObtenTipoUsuario(idTipoUsuario);

        }

        public List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatTipoPersona();
        }

        public List<DIR_CAT_COLONIAS> ObtenColoniasPorCp(int codigoPostal)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenColoniasPorCp(codigoPostal);
        }

        public List<DIR_CAT_ESTADO> ObtenCatEstados()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatEstados();
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenMunicipios(int estado)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatMunicipios(estado);
        }

        public List<DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenColonias(estado, municipio);
        }

        public DIR_CAT_COLONIAS ObtenCodigoPostal(int estado, int municipio, int colonia)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCodigoPostal(estado, municipio, colonia);
        }

        public List<ListasGenerica> ObtenTablasCatalogos()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenTablasCatalogos();
        }

        public List<DIR_CAT_COLONIAS> ObtenCatalogosColonias()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatalogosColonias();
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenCatalogosMunicipios()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatalogosMunicipios();
        }
    }
}
