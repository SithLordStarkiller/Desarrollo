namespace Universidad.ServidorInterno.GestionCatalogos
{
    using System.Collections.Generic;
    using Entidades.Catalogos;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "S_GestionCatalogos" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione S_GestionCatalogos.svc o S_GestionCatalogos.svc.cs en el Explorador de soluciones e inicie la depuración.
    
    public class S_GestionCatalogos : IS_GestionCatalogos
    {
        public List<CatalogosSistema> ObtenCatalogosSistemas()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenCatalogosSistema();
        }

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

        public List<AUL_CAT_TIPO_AULA> ObtenListaAUL_CAT_TIPO_AULA()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaAUL_CAT_TIPO_AULA();
        }

        public bool ActualizaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaRegistroAUL_CAT_TIPO_AULA(registro);
        }

        public AUL_CAT_TIPO_AULA InsertaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaRegistroAUL_CAT_TIPO_AULA(registro);
        }
        public bool EliminaRegistroAUL_CAT_TIPO_AULA(int idTipoAula)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaRegistroAUL_CAT_TIPO_AULA(idTipoAula);
        }

        #region HOR_CAT_TURNO

        public List<HOR_CAT_TURNO> ObtenListaHorCatTurno()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaHorCatTurno();
        }

        public HOR_CAT_TURNO InsertaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaHorCatTurno(registro);
        }

        public bool ActualizaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaHorCatTurno(registro);
        }

        public bool EliminaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaHorCatTurno(registro);
        }

        public HOR_CAT_TURNO ExtraeHorCatTurno(int idTurno)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraeHorCatTurno(idTurno);
        }

        #endregion

        #region HOR_CAT_DIAS_SEMANA

        public List<HOR_CAT_DIAS_SEMANA> ObtenListaHorCatDiasSemana()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaHorCatDiasSemana();
        }

        public HOR_CAT_DIAS_SEMANA InsertaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaHorCatDiasSemana(registro);
        }

        public bool ActualizaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaHorCatDiasSemana(registro);
        }

        public bool EliminaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaHorCatDiasSemana(registro);
        }

        public HOR_CAT_DIAS_SEMANA ExtraerHorCatDiasSemana(int idTurno)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraeHorCatDiasSemana(idTurno);
        }

        #endregion

        #region AUL_CAT_TIPO_AULA

        public List<AUL_CAT_TIPO_AULA> ObtenListaAulCatTipoAula()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaAulCatTipoAula();
        }

        public AUL_CAT_TIPO_AULA InsertaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaAulCatTipoAula(registro);
        }

        public bool ActualizaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaAulCatTipoAula(registro);
        }

        public bool EliminaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaAulCatTipoAula(registro);
        }

        public AUL_CAT_TIPO_AULA ExtraerAulCatTipoAula(int idTurno)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraerAulCatTipoAulaSemana(idTurno);
        }

        #endregion

        #region CAR_CAT_ESPECIALIDAD

        public List<CAR_CAT_ESPECIALIDAD> ObtenListaCarCatEspecialidad()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaCarCatEspecialidad();
        }

        public CAR_CAT_ESPECIALIDAD InsertaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaCarCatEspecialidad(registro);
        }

        public bool ActualizaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaCarCatEspecialidad(registro);
        }

        public bool EliminaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaCarCatEspecialidad(registro);
        }

        public CAR_CAT_ESPECIALIDAD ExtraerCarCatEspecialidad(int idEspecialidad)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraerCarCatEspecialidad(idEspecialidad);
        }

        #endregion

        #region CAL_CAT_TIPO_EVALUACION

        public List<CAL_CAT_TIPO_EVALUACION> ObtenListaCalCatTipoEvaluacion()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaCalCatTipoEvaluacion();
        }

        public CAL_CAT_TIPO_EVALUACION InsertaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaCalCatTipoEvaluacion(registro);
        }

        public bool ActualizaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaCalCatTipoEvaluacion(registro);
        }

        public bool EliminaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaCalCatTipoEvaluacion(registro);
        }

        public CAL_CAT_TIPO_EVALUACION ExtraerCalCatTipoEvaluacion(int idEspecialidad)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraerCalCatTipoEvaluacion(idEspecialidad);
        }

        #endregion

        #region CAL_CAT_HORAS

        public List<HOR_CAT_HORAS> ObtenListaHorCatHoras()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaHorCatHora();
        }

        public HOR_CAT_HORAS InsertaHorCatHoras(HOR_CAT_HORAS registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaHorCatHoras(registro);
        }

        public bool ActualizaHorCatHoras(HOR_CAT_HORAS registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaHorCatHoras(registro);
        }

        public bool EliminaHorCatHoras(HOR_CAT_HORAS registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaHorCatHoras(registro);
        }

        public HOR_CAT_HORAS ExtraerHorCatHoras(int idHoras)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraerHorCatHoras(idHoras);
        }

        #endregion

        #region CAL_CAT_HORAS

        public List<CAR_CAT_CARRERAS> ObtenListaCarCatCarreras()
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ObtenListaCarCatCarreras();
        }

        public CAR_CAT_CARRERAS InsertaCarCatCarreras(CAR_CAT_CARRERAS registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().InsertaCarCatCarreras(registro);
        }

        public bool ActualizaCarCatCarreras(CAR_CAT_CARRERAS registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ActualizaCarCatCarreras(registro);
        }

        public bool EliminaCarCatCarreras(CAR_CAT_CARRERAS registro)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().EliminaCarCatCarreras(registro);
        }

        public CAR_CAT_CARRERAS ExtraerCarCatCarreras(int idHoras)
        {
            return new LogicaNegocios.GestionCatalogos.GestionCatalogos().ExtraerCarCatCarreras(idHoras);
        }

        #endregion
    }
}