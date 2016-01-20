namespace Universidad.ServidorInterno.GestionCatalogos
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entidades.Catalogos;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IS_GestionCatalogos" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IS_GestionCatalogos
    {
        [OperationContract]
        List<AUL_CAT_TIPO_AULA> ObtenListaAUL_CAT_TIPO_AULA();

        [OperationContract]
        AUL_CAT_TIPO_AULA InsertaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro);

        [OperationContract]
        bool EliminaRegistroAUL_CAT_TIPO_AULA(int idTipoAula);

        [OperationContract]
        List<CatalogosSistema> ObtenCatalogosSistemas();

        [OperationContract]
        bool ActualizaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro);
        
        [OperationContract]
        List<US_CAT_TIPO_USUARIO> ObtenTablaUsCatTipoUsuarios();

        [OperationContract]
        List<US_CAT_NIVEL_USUARIO> ObtenTablaUsCatNivelUsuario();

        [OperationContract]
        List<US_CAT_ESTATUS_USUARIO> ObtenTablaUsCatEstatusUsuario();

        [OperationContract]
        US_CAT_TIPO_USUARIO ObtenCatTipoUsuario(int idTipoUsuario);

        [OperationContract]
        List<PER_CAT_NACIONALIDAD> ObtenCatalogoNacionalidades();

        [OperationContract]
        List<PER_CAT_TIPO_PERSONA> ObtenCatTipoPersona();

        [OperationContract]
        List<DIR_CAT_COLONIAS> ObtenColoniasPorCp(int codigoPostal);

        [OperationContract]
        List<DIR_CAT_ESTADO> ObtenCatEstados();

        [OperationContract]
        List<DIR_CAT_DELG_MUNICIPIO> ObtenMunicipios(int estado);

        [OperationContract]
        List<DIR_CAT_COLONIAS> ObtenColonias(int estado, int municipio);

        [OperationContract]
        DIR_CAT_COLONIAS ObtenCodigoPostal(int estado, int municipio, int colonia);

        [OperationContract]
        List<ListasGenerica> ObtenTablasCatalogos();

        [OperationContract]
        List<DIR_CAT_COLONIAS> ObtenCatalogosColonias();

        [OperationContract]
        List<DIR_CAT_DELG_MUNICIPIO> ObtenCatalogosMunicipios();

        #region HOR_CAT_TURNO

        [OperationContract]
        List<HOR_CAT_TURNO> ObtenListaHorCatTurno();

        [OperationContract]
        HOR_CAT_TURNO InsertaHorCatTurno(HOR_CAT_TURNO registro);

        [OperationContract]
        bool ActualizaHorCatTurno(HOR_CAT_TURNO registro);

        [OperationContract]
        bool EliminaHorCatTurno(HOR_CAT_TURNO registro);

        [OperationContract]
        HOR_CAT_TURNO ExtraeHorCatTurno(int idTurno);

        #endregion

        #region HOR_CAT_DIAS_SEMANA

        [OperationContract]
        List<HOR_CAT_DIAS_SEMANA> ObtenListaHorCatDiasSemana();

        [OperationContract]
        HOR_CAT_DIAS_SEMANA InsertaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro);

        [OperationContract]
        bool ActualizaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro);

        [OperationContract]
        bool EliminaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro);

        [OperationContract]
        HOR_CAT_DIAS_SEMANA ExtraerHorCatDiasSemana(int idTurno);

        #endregion

        #region AUL_CAT_TIPO_AULA

        [OperationContract]
        List<AUL_CAT_TIPO_AULA> ObtenListaAulCatTipoAula();

        [OperationContract]
        AUL_CAT_TIPO_AULA InsertaAulCatTipoAula(AUL_CAT_TIPO_AULA registro);

        [OperationContract]
        bool ActualizaAulCatTipoAula(AUL_CAT_TIPO_AULA registro);

        [OperationContract]
        bool EliminaAulCatTipoAula(AUL_CAT_TIPO_AULA registro);

        [OperationContract]
        AUL_CAT_TIPO_AULA ExtraerAulCatTipoAula(int idTurno);

        #endregion

        #region CAR_CAT_ESPECIALIDAD

        [OperationContract]
        List<CAR_CAT_ESPECIALIDAD> ObtenListaCarCatEspecialidad();

        [OperationContract]
        CAR_CAT_ESPECIALIDAD InsertaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro);

        [OperationContract]
        bool ActualizaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro);

        [OperationContract]
        bool EliminaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro);

        [OperationContract]
        CAR_CAT_ESPECIALIDAD ExtraerCarCatEspecialidad(int idEspecialidad);

        #endregion

        #region CAL_CAT_TIPO_EVALUACION

        [OperationContract]
        List<CAL_CAT_TIPO_EVALUACION> ObtenListaCalCatTipoEvaluacion();

        [OperationContract]
        CAL_CAT_TIPO_EVALUACION InsertaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro);

        [OperationContract]
        bool ActualizaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro);

        [OperationContract]
        bool EliminaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro);

        [OperationContract]
        CAL_CAT_TIPO_EVALUACION ExtraerCalCatTipoEvaluacion(int idEspecialidad);

        #endregion
    }
}