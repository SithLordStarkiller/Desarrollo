namespace Universidad.AccesoDatos.AdministracionSistema.GestionCatalogos
{
    using System;
    using System.Data.SqlClient;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Entidades.Catalogos;
    using Entidades;

    public class Catalogos
    {
        public List<CatalogosSistema> ObtenCatalogosTSql()
        {
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                "Usp_ObtenCatalogosSistema", null);

            var resultado = new List<CatalogosSistema>();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new CatalogosSistema
                             {
                                 NombreTabla = (string)row["TableName"]
                             }).ToList();
            }

            return resultado;
        }

        public List<AUL_CAT_TIPO_AULA> ObtenListaAUL_CAT_TIPO_AULALinq()
        {
            using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
            {
                return r.TablaCompleta();
            }
        }

        public List<AUL_CAT_TIPO_AULA> ObtenListaAUL_CAT_TIPO_AULATSql()
        {
            var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
               "Usp_ObtenListaAUL_CAT_TIPO_AULA", null);

            var resultado = new List<AUL_CAT_TIPO_AULA>();

            if (obj != null)
            {
                resultado = (from DataRow row in obj.Rows
                             select new AUL_CAT_TIPO_AULA
                             {
                                 IDTIPOAULA = (short)row["IDTIPOAULA"],
                                 TIPOAULA = (string)row["TIPOAULA"],
                                 DESCRIPCION = (string)row["DESCRIPCION"]
                             }).ToList();
            }

            return resultado;
        }

        public bool ActualizaRegistroAUL_CAT_TIPO_AULATSql(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdTipoAula", registro.IDTIPOAULA),
                    new SqlParameter("@TipoAula", registro.TIPOAULA),
                    new SqlParameter("@Descripcion", registro.DESCRIPCION)
                };

                var obj = ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                    "Usp_ActualizaRegistroAUL_CAT_TIPO_AULA", para);

                return obj != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActualizaRegistroAUL_CAT_TIPO_AULALinq(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
                {
                    return r.Actualizar(registro) != null;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AUL_CAT_TIPO_AULA NuevoRegistroAUL_CAT_TIPO_AULATSql(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdTipoAula", registro.IDTIPOAULA),
                    new SqlParameter("@TipoAula", registro.TIPOAULA),
                    new SqlParameter("@Descripcion", registro.DESCRIPCION)
                };

                ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                    "Usp_InsertaAUL_CAT_TIPO_AULA", para);

                return registro;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AUL_CAT_TIPO_AULA NuevoRegistroAUL_CAT_TIPO_AULALinq(AUL_CAT_TIPO_AULA registro)
        {
            using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
            {
                return r.Agregar(registro);
            }
        }

        public bool EliminaRegistroAUL_CAT_TIPO_AULATSql(int idTipoAula)
        {
            try
            {
                var para = new[]
                {
                    new SqlParameter("@IdTipoAula", idTipoAula)
                };

                ControladorSQL.ExecuteDataTable(ParametrosSQL.strCon_DBLsWebApp, CommandType.StoredProcedure,
                    "Usp_EliminaRegistroAUL_CAT_TIPO_AULA", para);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaRegistroAUL_CAT_TIPO_AULATLinq(int idTipoAula)
        {
            try
            {
                using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
                {
                    r.Eliminar(new AUL_CAT_TIPO_AULA { IDTIPOAULA = (short)idTipoAula });
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region HOR_CAT_TURNO

        public List<HOR_CAT_TURNO> ObtenListaHorCatTurnoLinq()
        {
            using (var r = new Repositorio<HOR_CAT_TURNO>())
            {
                return r.TablaCompleta();
            }
        }

        public HOR_CAT_TURNO InsertaHorCatTurnoLinq(HOR_CAT_TURNO registro)
        {
            using (var r = new Repositorio<HOR_CAT_TURNO>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaHorCatTurnoLinq(HOR_CAT_TURNO registro)
        {
            try
            {
                using (var r = new Repositorio<HOR_CAT_TURNO>())
                {
                    r.Actualizar(registro);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaHorCatTurnoLinq(HOR_CAT_TURNO registro)
        {
            try
            {
                using (var r = new Repositorio<HOR_CAT_TURNO>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HOR_CAT_TURNO ExtraeHorCatTurnoLinq(int idTurno)
        {
            using (var r = new Repositorio<HOR_CAT_TURNO>())
            {
                return r.Extraer(a => a.IDTURNO == idTurno);
            }
        }

        #endregion

        #region HOR_CAT_DIAS_SEMANA

        public List<HOR_CAT_DIAS_SEMANA> ObtenListaHorCatDiasSemanaLinq()
        {
            using (var r = new Repositorio<HOR_CAT_DIAS_SEMANA>())
            {
                return r.TablaCompleta();
            }
        }

        public HOR_CAT_DIAS_SEMANA InsertaHorCatDiasSemanaLinq(HOR_CAT_DIAS_SEMANA registro)
        {
            using (var r = new Repositorio<HOR_CAT_DIAS_SEMANA>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaHorCatDiasSemanaLinq(HOR_CAT_DIAS_SEMANA registro)
        {
            try
            {
                using (var r = new Repositorio<HOR_CAT_DIAS_SEMANA>())
                {
                    r.Actualizar(registro);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaHorCatDiasSemanaLinq(HOR_CAT_DIAS_SEMANA registro)
        {
            try
            {
                using (var r = new Repositorio<HOR_CAT_DIAS_SEMANA>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HOR_CAT_DIAS_SEMANA ExtraeHorCatDiasSemanaLinq(int idDia)
        {
            using (var r = new Repositorio<HOR_CAT_DIAS_SEMANA>())
            {
                return r.Extraer(a => a.IDDIA == idDia);
            }
        }

        #endregion

        #region AUL_CAT_TIPO_AULA

        public List<AUL_CAT_TIPO_AULA> ObtenListaAulCatTipoAulaLinq()
        {
            using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
            {
                return r.TablaCompleta();
            }
        }

        public AUL_CAT_TIPO_AULA InsertaAulCatTipoAulaLinq(AUL_CAT_TIPO_AULA registro)
        {
            using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaAulCatTipoAulaLinq(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
                {
                    r.Actualizar(registro);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaAulCatTipoAulaLinq(AUL_CAT_TIPO_AULA registro)
        {
            try
            {
                using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AUL_CAT_TIPO_AULA ExtraeAulCatTipoAulaLinq(int idTipoAula)
        {
            using (var r = new Repositorio<AUL_CAT_TIPO_AULA>())
            {
                return r.Extraer(a => a.IDTIPOAULA == idTipoAula);
            }
        }

        #endregion

        #region CAR_CAT_ESPECIALIDAD

        public List<CAR_CAT_ESPECIALIDAD> ObtenListaCarCatEspecialidadLinq()
        {
            using (var r = new Repositorio<CAR_CAT_ESPECIALIDAD>())
            {
                return r.TablaCompleta();
            }
        }

        public CAR_CAT_ESPECIALIDAD InsertaCarCatEspecialidadLinq(CAR_CAT_ESPECIALIDAD registro)
        {
            using (var r = new Repositorio<CAR_CAT_ESPECIALIDAD>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaCarCatEspecialidadLinq(CAR_CAT_ESPECIALIDAD registro)
        {
            try
            {
                using (var r = new Repositorio<CAR_CAT_ESPECIALIDAD>())
                {
                    r.Actualizar(registro);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaCarCatEspecialidadLinq(CAR_CAT_ESPECIALIDAD registro)
        {
            try
            {
                using (var r = new Repositorio<CAR_CAT_ESPECIALIDAD>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CAR_CAT_ESPECIALIDAD ExtraeCarCatEspecialidadLinq(int idEspecialidad)
        {
            using (var r = new Repositorio<CAR_CAT_ESPECIALIDAD>())
            {
                return r.Extraer(a => a.IDESPECIALIDAD == idEspecialidad);
            }
        }

        #endregion

        #region CAL_CAT_TIPO_EVALUACION

        public List<CAL_CAT_TIPO_EVALUACION> ObtenListaCalCatTipoEvaluacionLinq()
        {
            using (var r = new Repositorio<CAL_CAT_TIPO_EVALUACION>())
            {
                return r.TablaCompleta();
            }
        }

        public CAL_CAT_TIPO_EVALUACION InsertaCalCatTipoEvaluacionLinq(CAL_CAT_TIPO_EVALUACION registro)
        {
            using (var r = new Repositorio<CAL_CAT_TIPO_EVALUACION>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaCalCatTipoEvaluacionLinq(CAL_CAT_TIPO_EVALUACION registro)
        {
            try
            {
                using (var r = new Repositorio<CAL_CAT_TIPO_EVALUACION>())
                {
                    r.Actualizar(registro);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaCalCatTipoEvaluacionLinq(CAL_CAT_TIPO_EVALUACION registro)
        {
            try
            {
                using (var r = new Repositorio<CAL_CAT_TIPO_EVALUACION>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CAL_CAT_TIPO_EVALUACION ExtraerCalCatTipoEvaluacionLinq(int idEspecialidad)
        {
            using (var r = new Repositorio<CAL_CAT_TIPO_EVALUACION>())
            {
                return r.Extraer(a => a.IDTIPOEVALUACION == idEspecialidad);
            }
        }

        #endregion

        #region HOR_CAT_HORAS

        public List<HOR_CAT_HORAS> ObtenListaHorCatHorasLinq()
        {
            using (var r = new Repositorio<HOR_CAT_HORAS>())
            {
                return r.TablaCompleta();
            }
        }

        public HOR_CAT_HORAS InsertaHorCatHorasLinq(HOR_CAT_HORAS registro)
        {
            using (var r = new Repositorio<HOR_CAT_HORAS>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaHorCatHorasLinq(HOR_CAT_HORAS registro)
        {
            try
            {
                using (var r = new Repositorio<HOR_CAT_HORAS>())
                {
                    return r.Actualizar(registro) != null;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaHorCatHorasLinq(HOR_CAT_HORAS registro)
        {
            try
            {
                using (var r = new Repositorio<HOR_CAT_HORAS>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public HOR_CAT_HORAS ExtraerHorCatHorasLinq(int idHora)
        {
            using (var r = new Repositorio<HOR_CAT_HORAS>())
            {
                return r.Extraer(a => a.IDHORA == idHora);
            }
        }

        #endregion

        #region CAR_CAT_CARRERAS

        public List<CAR_CAT_CARRERAS> ObtenListaCarCatCarrerasLinq()
        {
            using (var r = new Repositorio<CAR_CAT_CARRERAS>())
            {
                return r.TablaCompleta();
            }
        }

        public CAR_CAT_CARRERAS InsertaCarCatCarrerasLinq(CAR_CAT_CARRERAS registro)
        {
            using (var r = new Repositorio<CAR_CAT_CARRERAS>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaCarCatCarrerasLinq(CAR_CAT_CARRERAS registro)
        {
            try
            {
                using (var r = new Repositorio<CAR_CAT_CARRERAS>())
                {
                    return r.Actualizar(registro) != null;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaCarCatCarrerasLinq(CAR_CAT_CARRERAS registro)
        {
            try
            {
                using (var r = new Repositorio<CAR_CAT_CARRERAS>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CAR_CAT_CARRERAS ExtraerCarCatCarrerasLinq(int idCarrera)
        {
            using (var r = new Repositorio<CAR_CAT_CARRERAS>())
            {
                return r.Extraer(a => a.IDCARRERA == idCarrera);
            }
        }

        #endregion

        #region MAT_CAT_MATERIAS

        public List<MAT_CAT_MATERIAS> ObtenListaMatCatMateriasLinq()
        {
            using (var r = new Repositorio<MAT_CAT_MATERIAS>())
            {
                return r.TablaCompleta();
            }
        }

        public MAT_CAT_MATERIAS InsertaMatCatMateriasLinq(MAT_CAT_MATERIAS registro)
        {
            using (var r = new Repositorio<MAT_CAT_MATERIAS>())
            {
                return r.Agregar(registro);
            }
        }

        public bool ActualizaMatCatMateriasLinq(MAT_CAT_MATERIAS registro)
        {
            try
            {
                using (var r = new Repositorio<MAT_CAT_MATERIAS>())
                {
                    return r.Actualizar(registro) != null;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminaMatCatMateriasLinq(MAT_CAT_MATERIAS registro)
        {
            try
            {
                using (var r = new Repositorio<MAT_CAT_MATERIAS>())
                {
                    return r.Eliminar(registro);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public MAT_CAT_MATERIAS ExtraerMatCatMateriasLinq(int idMateria)
        {
            using (var r = new Repositorio<MAT_CAT_MATERIAS>())
            {
                return r.Extraer(a => a.IDMATERIA == idMateria);
            }
        }

        #endregion
    }
}
