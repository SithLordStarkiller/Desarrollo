namespace Universidad.LogicaNegocios.GestionCatalogos
{
    using System.Collections.Generic;
    using AccesoDatos.AdministracionSistema.GestionCatalogos;
    using Entidades.Catalogos;
    using Entidades;

    public class GestionCatalogos
    {
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

        #endregion

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

        #region HOR_CAT_TURNO

        public List<HOR_CAT_TURNO> ObtenListaHorCatTurno()
        {
            return new Catalogos().ObtenListaHorCatTurnoLinq();
        }

        public HOR_CAT_TURNO InsertaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return new Catalogos().InsertaHorCatTurnoLinq(registro);
        }

        public bool ActualizaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return new Catalogos().ActualizaHorCatTurnoLinq(registro);
        }

        public bool EliminaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return new Catalogos().EliminaHorCatTurnoLinq(registro);
        }

        public HOR_CAT_TURNO ExtraeHorCatTurno(int idTurno)
        {
            return new Catalogos().ExtraeHorCatTurnoLinq(idTurno);
        }

        #endregion
        
        #region HOR_CAT_DIAS_SEMANA

        public List<HOR_CAT_DIAS_SEMANA> ObtenListaHorCatDiasSemana()
        {
            return new Catalogos().ObtenListaHorCatDiasSemanaLinq();
        }

        public HOR_CAT_DIAS_SEMANA InsertaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return new Catalogos().InsertaHorCatDiasSemanaLinq(registro);
        }

        public bool ActualizaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return new Catalogos().ActualizaHorCatDiasSemanaLinq(registro);
        }

        public bool EliminaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return new Catalogos().EliminaHorCatDiasSemanaLinq(registro);
        }

        public HOR_CAT_DIAS_SEMANA ExtraeHorCatDiasSemana(int idDia)
        {
            return new Catalogos().ExtraeHorCatDiasSemanaLinq(idDia);
        }

        #endregion

        #region AUL_CAT_TIPO_AULA

        public List<AUL_CAT_TIPO_AULA> ObtenListaAulCatTipoAula()
        {
            return new Catalogos().ObtenListaAulCatTipoAulaLinq();
        }

        public AUL_CAT_TIPO_AULA InsertaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return new Catalogos().InsertaAulCatTipoAulaLinq(registro);
        }

        public bool ActualizaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return new Catalogos().ActualizaAulCatTipoAulaLinq(registro);
        }

        public bool EliminaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return new Catalogos().EliminaAulCatTipoAulaLinq(registro);
        }

        public AUL_CAT_TIPO_AULA ExtraerAulCatTipoAulaSemana(int idTipoAula)
        {
            return new Catalogos().ExtraeAulCatTipoAulaLinq(idTipoAula);
        }

        #endregion

        #region CAR_CAT_ESPECIALIDAD

        public List<CAR_CAT_ESPECIALIDAD> ObtenListaCarCatEspecialidad()
        {
            return new Catalogos().ObtenListaCarCatEspecialidadLinq();
        }

        public CAR_CAT_ESPECIALIDAD InsertaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return new Catalogos().InsertaCarCatEspecialidadLinq(registro);
        }

        public bool ActualizaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return new Catalogos().ActualizaCarCatEspecialidadLinq(registro);
        }

        public bool EliminaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return new Catalogos().EliminaCarCatEspecialidadLinq(registro);
        }

        public CAR_CAT_ESPECIALIDAD ExtraerCarCatEspecialidad(int idEspecialidad)
        {
            return new Catalogos().ExtraeCarCatEspecialidadLinq(idEspecialidad);
        }

        #endregion

        #region CAL_CAT_TIPO_EVALUACION

        public List<CAL_CAT_TIPO_EVALUACION> ObtenListaCalCatTipoEvaluacion()
        {
            return new Catalogos().ObtenListaCalCatTipoEvaluacionLinq();
        }

        public CAL_CAT_TIPO_EVALUACION InsertaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return new Catalogos().InsertaCalCatTipoEvaluacionLinq(registro);
        }

        public bool ActualizaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return new Catalogos().ActualizaCalCatTipoEvaluacionLinq(registro);
        }

        public bool EliminaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return new Catalogos().EliminaCalCatTipoEvaluacionLinq(registro);
        }

        public CAL_CAT_TIPO_EVALUACION ExtraerCalCatTipoEvaluacion(int idTipoEvaluacion)
        {
            return new Catalogos().ExtraerCalCatTipoEvaluacionLinq(idTipoEvaluacion);
        }

        #endregion

        #region HOR_CAT_HORA

        public List<HOR_CAT_HORAS> ObtenListaHorCatHora()
        {
            return new Catalogos().ObtenListaHorCatHorasLinq();
        }

        public HOR_CAT_HORAS InsertaHorCatHoras(HOR_CAT_HORAS registro)
        {
            return new Catalogos().InsertaHorCatHorasLinq(registro);
        }

        public bool ActualizaHorCatHoras(HOR_CAT_HORAS registro)
        {
            return new Catalogos().ActualizaHorCatHorasLinq(registro);
        }

        public bool EliminaHorCatHoras(HOR_CAT_HORAS registro)
        {
            return new Catalogos().EliminaHorCatHorasLinq(registro);
        }

        public HOR_CAT_HORAS ExtraerHorCatHoras(int idHoras)
        {
            return new Catalogos().ExtraerHorCatHorasLinq(idHoras);
        }

        #endregion

        #region CAR_CAT_CARRERAS

        public List<CAR_CAT_CARRERAS> ObtenListaCarCatCarreras()
        {
            return new Catalogos().ObtenListaCarCatCarrerasLinq();
        }

        public CAR_CAT_CARRERAS InsertaCarCatCarreras(CAR_CAT_CARRERAS registro)
        {
            return new Catalogos().InsertaCarCatCarrerasLinq(registro);
        }

        public bool ActualizaCarCatCarreras(CAR_CAT_CARRERAS registro)
        {
            return new Catalogos().ActualizaCarCatCarrerasLinq(registro);
        }

        public bool EliminaCarCatCarreras(CAR_CAT_CARRERAS registro)
        {
            return new Catalogos().EliminaCarCatCarrerasLinq(registro);
        }

        public CAR_CAT_CARRERAS ExtraerCarCatCarreras(int idHoras)
        {
            return new Catalogos().ExtraerCarCatCarrerasLinq(idHoras);
        }

        #endregion

        #region MAT_CAT_MATERIAS

        public List<MAT_CAT_MATERIAS> ObtenListaMatCatMaterias()
        {
            return new Catalogos().ObtenListaMatCatMateriasLinq();
        }

        public MAT_CAT_MATERIAS InsertaMatCatMaterias(MAT_CAT_MATERIAS registro)
        {
            return new Catalogos().InsertaMatCatMateriasLinq(registro);
        }

        public bool ActualizaMatCatMaterias(MAT_CAT_MATERIAS registro)
        {
            return new Catalogos().ActualizaMatCatMateriasLinq(registro);
        }

        public bool EliminaMatCatMaterias(MAT_CAT_MATERIAS registro)
        {
            return new Catalogos().EliminaMatCatMateriasLinq(registro);
        }

        public MAT_CAT_MATERIAS ExtraerMatCatMaterias(int idHoras)
        {
            return new Catalogos().ExtraerMatCatMateriasLinq(idHoras);
        }

        #endregion

        #region MAT_CAT_CREDITOS_POR_HORAS

        public List<MAT_CAT_CREDITOS_POR_HORAS> ObtenListaMatCatCreditosPorHoras()
        {
            return new Catalogos().ObtenListaMatCatCreditosPorHorasLinq();
        }

        public MAT_CAT_CREDITOS_POR_HORAS InsertaMatCatCreditosPorHoras(MAT_CAT_CREDITOS_POR_HORAS registro)
        {
            return new Catalogos().InsertaMatCatCreditosPorHorasLinq(registro);
        }

        public bool ActualizaMatCatCreditosPorHoras(MAT_CAT_CREDITOS_POR_HORAS registro)
        {
            return new Catalogos().ActualizaMatCatCreditosPorHorasLinq(registro);
        }

        public bool EliminaMatCatCreditosPorHoras(MAT_CAT_CREDITOS_POR_HORAS registro)
        {
            return new Catalogos().EliminaMatCatCreditosPorHorasLinq(registro);
        }

        public MAT_CAT_CREDITOS_POR_HORAS ExtraerMatCatCreditosPorHoras(int idHoras)
        {
            return new Catalogos().ExtraerMatCatCreditosPorHorasLinq(idHoras);
        }

        #endregion

        #region HOR_HORAS_POR_DIA

        public List<HOR_HORAS_POR_DIA> ObtenListaHorHorasPorDia()
        {
            return new Catalogos().ObtenListaHorHorasPorDiaLinq();
        }

        public HOR_HORAS_POR_DIA InsertaHorHorasPorDia(HOR_HORAS_POR_DIA registro)
        {
            return new Catalogos().InsertaHorHorasPorDiaLinq(registro);
        }

        public bool ActualizaHorHorasPorDia(HOR_HORAS_POR_DIA registro)
        {
            return new Catalogos().ActualizaHorHorasPorDiaLinq(registro);
        }

        public bool EliminaHorHorasPorDia(HOR_HORAS_POR_DIA registro)
        {
            return new Catalogos().EliminaHorHorasPorDiaLinq(registro);
        }

        public HOR_HORAS_POR_DIA ExtraerHorHorasPorDia(int idHorasPorDia)
        {
            return new Catalogos().ExtraerHorHorasPorDiaLinq(idHorasPorDia);
        }

        #endregion
    }
}
