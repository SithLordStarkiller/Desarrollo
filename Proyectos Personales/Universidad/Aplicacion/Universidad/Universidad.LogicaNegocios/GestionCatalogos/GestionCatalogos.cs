using System.Collections.Generic;
using Universidad.Entidades;
using Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos;

namespace Universidad.LogicaNegocios.GestionCatalogos
{
    public class GestionCatalogos
    {
        #region Propiedades de la clase
        private static readonly GestionCatalogos _ClassInstance = new GestionCatalogos();

        public static GestionCatalogos ClassInstance
        {
            get { return _ClassInstance; }
        }
        #endregion

        #region Metodos de Insercion
        public US_CAT_TIPO_USUARIO InsertaRegistroCatTipoUsuario(US_CAT_TIPO_USUARIO registro)
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.InsertaRegistro(registro);
        }
        #endregion

        #region Metodos de Extraccion

        public List<US_CAT_TIPO_USUARIO> ObtenListaCatTiposUsuario()
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.ObtenListaCatTiposUsuario();
        }

        public US_CAT_TIPO_USUARIO ObtenTipoUsuario(int idTipoUsuario)
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.ObtenCatTipoUsuario(idTipoUsuario);
        }

        public List<PER_CAT_NACIONALIDAD> ObtenNacionalidades()
        {
            return new GestionCatNacionalidadA().ObtenNacionalidadLinq();
        }

        public List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona()
        {
            return new GestionCatTipoPersona().ObtenCatTipoPersonaLinq();
        }

        #region Gestion de catalogos Direcciones

        public List<DIR_CAT_ESTADO> ObtenCatEstados()
        {
            return new GestionCatDirecciones().ObtenCatEstadosLinq();
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenCatMunicipios(int estado)
        {
            return new GestionCatDirecciones().ObtenCatMunicipiosLinq(estado);
        }

        public List<DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio)
        {
            return new GestionCatDirecciones().ObtenColoniasLinq(estado, municipio);
        }

        public List<DIR_CAT_COLONIAS> ObtenColoniasPorCp(int codigoPostal)
        {
            return new GestionCatDirecciones().ObtenColoniasPorCpLinq(codigoPostal);
        }

        public DIR_CAT_COLONIAS ObtenCodigoPostal(int estado, int municipio, int colonia)
        {
            return new GestionCatDirecciones().ObtenCodigoPostalLinq(estado, municipio, colonia);
        }

        #endregion

        #endregion
    }
}
