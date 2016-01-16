namespace Universidad.LogicaNegocios.GestionCatalogos
{
    using System.Collections.Generic;
    using Entidades;
    using AccesoDatos.AdministracionSistema.GestionCatalogos;
    using Entidades.Catalogos;
    

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

        public AUL_CAT_TIPO_AULA InsertaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            //return new Catalogos().NuevoRegistroAUL_CAT_TIPO_AULATSql(registro);
            return new Catalogos().NuevoRegistroAUL_CAT_TIPO_AULALinq(registro);
        }

        public bool ActualizaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            //return new Catalogos().ActualizaRegistroAUL_CAT_TIPO_AULATSql(registro);
            return new Catalogos().ActualizaRegistroAUL_CAT_TIPO_AULALinq(registro);
        }

        public bool EliminaRegistroAUL_CAT_TIPO_AULA(int idTipoAula)
        {
            //return new Catalogos().EliminaRegistroAUL_CAT_TIPO_AULATSql(idTipoAula);
            return new Catalogos().EliminaRegistroAUL_CAT_TIPO_AULATLinq(idTipoAula);
        }

        #endregion

        #region Metodos de Extraccion

        public List<US_CAT_TIPO_USUARIO> ObtenListaCatTiposUsuario()
        {
            return Gestion_CAT_Tipos_Usuario.ClassInstance.ObtenListaCatTiposUsuario();
        }

        public List<US_CAT_NIVEL_USUARIO> ObtenListaCatNivelUsuario()
        {
            return Gestion_CAT_Nivel_Usuario.ClassInstance.ObtenListaCatNivelUsuario();
        }

        public List<US_CAT_ESTATUS_USUARIO> ObtenListaCatEstatusUsuario()
        {
            return Gestion_CAT_Estatus_Usuario.ClassInstance.ObtenListaCatEstatusUsuario();
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

        public List<ListasGenerica> ObtenTablasCatalogos()
        {
            return new AccesoDatos.AdministracionSistema.GestionCatalogos.GestionCatalogos().ObtenTablasCatalogosTsql();
        }

        public List<CatalogosSistema> ObtenCatalogosSistema()
        {
            return new Catalogos().ObtenCatalogosTSql();
        }

        public List<AUL_CAT_TIPO_AULA> ObtenListaAUL_CAT_TIPO_AULA()
        {
            //return new Catalogos().ObtenListaAUL_CAT_TIPO_AULATSql();
            return new Catalogos().ObtenListaAUL_CAT_TIPO_AULALinq();
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

        public List<DIR_CAT_COLONIAS> ObtenCatalogosColonias()
        {
            return new GestionCatDirecciones().ObtenCatalogoColonias();
        }

        public List<DIR_CAT_DELG_MUNICIPIO> ObtenCatalogosMunicipios()
        {
            return new GestionCatDirecciones().ObtenCatalogoMunicipios();
        }

        #endregion

        #endregion
    }
}
