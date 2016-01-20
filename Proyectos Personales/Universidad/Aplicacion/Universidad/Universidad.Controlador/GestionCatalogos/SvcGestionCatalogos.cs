﻿namespace Universidad.Controlador.GestionCatalogos
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Universidad.Controlador.SvcGestionCatalogos;
    using Entidades.Catalogos;
    using Entidades.ControlUsuario;

    public class SvcGestionCatalogos
    {
        private readonly S_GestionCatalogosClient _servicio;

        public SvcGestionCatalogos(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new S_GestionCatalogosClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"GestionCatalogos/", "S_GestionCatalogos.svc"));
        }

        public Task<List<US_CAT_ESTATUS_USUARIO>> ObtenTablaUsCatEstatusUsuario()
        {
            return Task.Run(() => _servicio.ObtenTablaUsCatEstatusUsuarioAsync());
        }

        public Task<List<US_CAT_NIVEL_USUARIO>> ObtenTablaUsCatNivelUsuario()
        {
            return Task.Run(() => _servicio.ObtenTablaUsCatNivelUsuarioAsync());
        }

        public Task<List<US_CAT_TIPO_USUARIO>> ObtenTablaUsCatTipoUsuario()
        {
            return Task.Run(() => _servicio.ObtenTablaUsCatTipoUsuariosAsync());
        }

        public Task<US_CAT_TIPO_USUARIO> ObtenTipoUsuario(int idTipoPersona)
        {
            return Task.Run(() => _servicio.ObtenCatTipoUsuarioAsync(idTipoPersona));
        }

        public Task<List<PER_CAT_NACIONALIDAD>> ObtenCatNacionalidad()
        {
            return Task.Run(() => _servicio.ObtenCatalogoNacionalidadesAsync());
        }

        public Task<List<PER_CAT_TIPO_PERSONA>> ObtenCatTipoPersona()
        {
            return Task.Run(() => _servicio.ObtenCatTipoPersonaAsync());
        }

        public Task<List<DIR_CAT_COLONIAS>> ObtenColoniasPorCpPersona(int codigoPostal)
        {
            return Task.Run(() => _servicio.ObtenColoniasPorCpAsync(codigoPostal));
        }

        public Task<List<DIR_CAT_ESTADO>> ObtenCatEstados()
        {
            return Task.Run(() => _servicio.ObtenCatEstadosAsync());
        }

        public Task<List<DIR_CAT_DELG_MUNICIPIO>> ObtenMunicipios(int estado)
        {
            return Task.Run(() => _servicio.ObtenMunicipiosAsync(estado));
        }

        public Task<List<DIR_CAT_COLONIAS>> ObtenColonias(int estado, int municipio)
        {
            return Task.Run(() => _servicio.ObtenColoniasAsync(estado, municipio));
        }
        public Task<DIR_CAT_COLONIAS> ObtenCodigoPostal(int estado, int municipio, int colonia)
        {
            return Task.Run(() => _servicio.ObtenCodigoPostalAsync(estado, municipio, colonia));
        }

        public Task<List<DIR_CAT_COLONIAS>> ObtenCatalogosColonias()
        {
            return Task.Run(() => _servicio.ObtenCatalogosColoniasAsync());
        }

        public Task<List<DIR_CAT_DELG_MUNICIPIO>> ObtenCatalogosMunicipios()
        {
            return Task.Run(() => _servicio.ObtenCatalogosMunicipiosAsync());
        }

        public Task<List<ListasGenerica>> ObtenTablasCatalogos()
        {
            return Task.Run(() => _servicio.ObtenTablasCatalogosAsync());
        }

        public Task<List<CatalogosSistema>> ObtenCatalogosSistema()
        {
            return Task.Run(() => _servicio.ObtenCatalogosSistemasAsync());
        }

        public Task<List<AUL_CAT_TIPO_AULA>> ObntenListaAUL_CAT_TIPO_AULA()
        {
            return Task.Run(() => _servicio.ObtenListaAUL_CAT_TIPO_AULAAsync());
        }

        public Task<bool> ActualizaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            return Task.Run(() => _servicio.ActualizaRegistroAUL_CAT_TIPO_AULAAsync(registro));
        }

        public Task<AUL_CAT_TIPO_AULA> InsertaRegistroAUL_CAT_TIPO_AULA(AUL_CAT_TIPO_AULA registro)
        {
            return Task.Run(() => _servicio.InsertaRegistroAUL_CAT_TIPO_AULAAsync(registro));
        }

        public Task<bool> EliminaResgistroAUL_CAT_TIPO_AULA(int idTipoAula)
        {
            return Task.Run(() => _servicio.EliminaRegistroAUL_CAT_TIPO_AULAAsync(idTipoAula));
        }

        #region HOR_CAT_TURNO

        public Task<List<HOR_CAT_TURNO>> ObtenListaHorCatTurno()
        {
            return Task.Run(() => _servicio.ObtenListaHorCatTurnoAsync());
        }

        public Task<HOR_CAT_TURNO> InsertaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return Task.Run(() => _servicio.InsertaHorCatTurnoAsync(registro));
        }

        public Task<bool> ActualizaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return Task.Run(() => _servicio.ActualizaHorCatTurnoAsync(registro));
        }

        public Task<bool> EliminaHorCatTurno(HOR_CAT_TURNO registro)
        {
            return Task.Run(() => _servicio.EliminaHorCatTurnoAsync(registro));
        }

        public Task<HOR_CAT_TURNO> ExtraerHorCatTurno(int idTurno)
        {
            return Task.Run(() => _servicio.ExtraeHorCatTurnoAsync(idTurno));
        }

        #endregion

        #region HOR_CAT_DIAS_SEMANA

        public Task<List<HOR_CAT_DIAS_SEMANA>> ObtenListaHorCatDiasSemana()
        {
            return Task.Run(() => _servicio.ObtenListaHorCatDiasSemanaAsync());
        }

        public Task<HOR_CAT_DIAS_SEMANA> InsertaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return Task.Run(() => _servicio.InsertaHorCatDiasSemanaAsync(registro));
        }

        public Task<bool> ActualizaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return Task.Run(() => _servicio.ActualizaHorCatDiasSemanaAsync(registro));
        }

        public Task<bool> EliminaHorCatDiasSemana(HOR_CAT_DIAS_SEMANA registro)
        {
            return Task.Run(() => _servicio.EliminaHorCatDiasSemanaAsync(registro));
        }

        public Task<HOR_CAT_DIAS_SEMANA> ExtraerHorCatDiasSemana(int idTurno)
        {
            return Task.Run(() => _servicio.ExtraerHorCatDiasSemanaAsync(idTurno));
        }

        #endregion

        #region AUL_CAT_TIPO_AULA

        public Task<List<AUL_CAT_TIPO_AULA>> ObtenListaAulCatTipoAula()
        {
            return Task.Run(() => _servicio.ObtenListaAulCatTipoAulaAsync());
        }

        public Task<AUL_CAT_TIPO_AULA> InsertaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return Task.Run(() => _servicio.InsertaAulCatTipoAulaAsync(registro));
        }

        public Task<bool> ActualizaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return Task.Run(() => _servicio.ActualizaAulCatTipoAulaAsync(registro));
        }

        public Task<bool> EliminaAulCatTipoAula(AUL_CAT_TIPO_AULA registro)
        {
            return Task.Run(() => _servicio.EliminaAulCatTipoAulaAsync(registro));
        }

        public Task<AUL_CAT_TIPO_AULA> ExtraerAulCatTipoAulaSemana(int idTurno)
        {
            return Task.Run(() => _servicio.ExtraerAulCatTipoAulaAsync(idTurno));
        }

        #endregion

        #region CAR_CAT_ESPECIALIDAD

        public Task<List<CAR_CAT_ESPECIALIDAD>> ObtenListaCarCatEspecialidad()
        {
            return Task.Run(() => _servicio.ObtenListaCarCatEspecialidadAsync());
        }

        public Task<CAR_CAT_ESPECIALIDAD> InsertaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return Task.Run(() => _servicio.InsertaCarCatEspecialidadAsync(registro));
        }

        public Task<bool> ActualizaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return Task.Run(() => _servicio.ActualizaCarCatEspecialidadAsync(registro));
        }

        public Task<bool> EliminaCarCatEspecialidad(CAR_CAT_ESPECIALIDAD registro)
        {
            return Task.Run(() => _servicio.EliminaCarCatEspecialidadAsync(registro));
        }

        public Task<CAR_CAT_ESPECIALIDAD> ExtraerCarCatEspecialidad(int idTurno)
        {
            return Task.Run(() => _servicio.ExtraerCarCatEspecialidadAsync(idTurno));
        }

        #endregion

        #region CAL_CAT_TIPO_EVALUACION

        public Task<List<CAL_CAT_TIPO_EVALUACION>> ObtenListaCalCatTipoEvaluacion()
        {
            return Task.Run(() => _servicio.ObtenListaCalCatTipoEvaluacionAsync());
        }

        public Task<CAL_CAT_TIPO_EVALUACION> InsertaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return Task.Run(() => _servicio.InsertaCalCatTipoEvaluacionAsync(registro));
        }

        public Task<bool> ActualizaCalCatTipoEvaluacion(CAL_CAT_TIPO_EVALUACION registro)
        {
            return Task.Run(() => _servicio.ActualizaCalCatTipoEvaluacionAsync(registro));
        }

        public Task<bool> EliminaCarCatEspecialidad(CAL_CAT_TIPO_EVALUACION registro)
        {
            return Task.Run(() => _servicio.EliminaCalCatTipoEvaluacionAsync(registro));
        }

        public Task<CAL_CAT_TIPO_EVALUACION> ExtraerCalCatTipoEvaluacion(int idTurno)
        {
            return Task.Run(() => _servicio.ExtraerCalCatTipoEvaluacionAsync(idTurno));
        }

        #endregion

    }
}
