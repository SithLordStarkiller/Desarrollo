using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class EntRankingIndicadores
    {
        /// <summary>
        /// Metodo que  obtiene la delegacion del usuario
        /// </summary>
        /// <param name="idUsuario">id del usuario</param>
        /// <returns>delegacion a la que pertenece el usuario</returns>
        public List<String> ObtenerDelegacionUsuario(string idUsuario)
        {
            var delegacionUsuarioList = new List<String>(new [] {""});
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@idUsuario", SqlDbType.VarChar) {Value = idUsuario};
                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "ObtenerDelegacionUsuario", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    delegacionUsuarioList = dsArchivos.Tables[0].AsEnumerable()
                        .Select(r => r.Field<String>(0))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerDelegacionUsuario: " + ex.Message);
                return delegacionUsuarioList;
            }

            return delegacionUsuarioList;
        }

        /// <summary>
        /// Metodo que obtiene la lista de indicadores del Ranking, por medio del modulo y ruta
        /// </summary>
        /// <param name="permisoModulo">Modulo, ejemplo: "Despacho", "Supervisor"</param>
        /// <param name="ruta">Ruta del formulario </param>
        /// <returns>Lista de los indicadores que se van a mostrar</returns>
        public List<IndicadoresRankingModel> ObtenerIndicadores(string permisoModulo, string ruta)
        {
            var indicadores = new List<IndicadoresRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@PermisoModulo", SqlDbType.VarChar) { Value = permisoModulo };
                parametros[1] = new SqlParameter("@NombreRuta", SqlDbType.VarChar) { Value = ruta };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerIndicadoresRanking", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<IndicadoresRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerIndicadores: " + ex.Message);
                return new List<IndicadoresRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Créditos asignados móvil por delegación
        /// </summary>
        /// <param name="tipoDashboard">Nombre del indicador</param>
        /// <param name="indicador">Rol que está ejecutando la consulta</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosDespachosRankingModel> ObtenerDelegaciones(String tipoDashboard, String indicador,String tipoFormulario)
        {
            var indicadores = new List<ResultadosDespachosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@Master", SqlDbType.VarChar) { Value = "DelegacionAdministrador" };
                parametros[1] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[2] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[3] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosDespachosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerDelegaciones: " + ex.Message);
                return new List<ResultadosDespachosRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Tabla Delegaciones
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="despacho">Nombre del indicador</param>
        /// <param name="valor">Constante 100</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosUsuariosRankingModel> ObtenerTablaDelegaciones(String tipoDashboard, String indicador, String despacho, int valor = 100,string tipoFormulario="")
        {
            var indicadores = new List<ResultadosUsuariosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@Master", SqlDbType.VarChar) { Value = "DelegacionDespacho" };
                parametros[1] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[2] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[3] = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value = valor };
                parametros[4] = new SqlParameter("@fc_Despacho", SqlDbType.VarChar) { Value = despacho };
                parametros[5] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosUsuariosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaDelegaciones: " + ex.Message);
                return new List<ResultadosUsuariosRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Tabla Despachos
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosDespachosRankingModel> ObtenerTablaDespachos(String tipoDashboard, String indicador,String tipoFormulario)
        {
            var indicadores = new List<ResultadosDespachosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@Master", SqlDbType.VarChar) { Value = "Despacho" };
                parametros[1] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[2] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[3] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosDespachosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaDespachos: " + ex.Message);
                return new List<ResultadosDespachosRankingModel>();
            }
        } 
        
        /// <summary>
        /// Obtiene Tabla Gestores
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="despacho">Nombre del indicador</param>
        /// <param name="supervisor">Id del Supervisor</param>
        /// <param name="valor">Constante 100</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosUsuariosRankingModel> ObtenerTablaGestores(String tipoDashboard, String indicador,String despacho,String supervisor,int valor=100,string tipoFormulario="")
        {
            var indicadores = new List<ResultadosUsuariosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@Master", SqlDbType.VarChar) { Value = "Supervisor" };
                parametros[1] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[2] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[3] = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value = valor };
                parametros[4] = new SqlParameter("@fc_Despacho", SqlDbType.VarChar) { Value = despacho };
                parametros[5] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosUsuariosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaGestores: " + ex.Message);
                return new List<ResultadosUsuariosRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Tabla Despachos - Delegacion
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="delegacion">Id de la delegacion</param>
        /// <param name="valor">Constante 100</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosUsuariosRankingModel> ObtenerTablaDespachosDelegacion(String tipoDashboard, String indicador, String delegacion, int valor = 100, string tipoFormulario="")
        {
            var indicadores = new List<ResultadosUsuariosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@Master", SqlDbType.VarChar) { Value = "Delegacion" };
                parametros[1] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[2] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[3] = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value = valor };
                parametros[4] = new SqlParameter("@fc_Delegacion", SqlDbType.VarChar) { Value = delegacion };
                parametros[5] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosUsuariosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaDespachosDelegacion: " + ex.Message);
                return new List<ResultadosUsuariosRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Tabla Supervisores - Delegacion
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="despacho">Nombre del indicador</param>
        /// <param name="delegacion">Id de la delegacion</param>
        /// <param name="valor">Constante 100</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosUsuariosRankingModel> ObtenerTablaSupervisoresDelegacion(String tipoDashboard, String indicador, String despacho, String delegacion, int valor = 100, string tipoFormulario="")
        {
            var indicadores = new List<ResultadosUsuariosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[1] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[2] = new SqlParameter("@fc_Despacho", SqlDbType.VarChar) { Value = despacho };
                parametros[3] = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value = valor };
                parametros[4] = new SqlParameter("@fc_Delegacion", SqlDbType.VarChar) { Value = delegacion };
                parametros[5] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosUsuariosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaSupervisoresDelegacion: " + ex.Message);
                return new List<ResultadosUsuariosRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Tabla Supervisor - Valor
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="despacho">Nombre del indicador</param>
        /// <param name="supervisor">Id del supervisor</param>
        /// <param name="valor">Constante 100</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosUsuariosRankingModel> ObtenerTablaSupervisorValor(String tipoDashboard, String indicador, String despacho, String supervisor, int valor = 100, string tipoFormulario="")
        {
            var indicadores = new List<ResultadosUsuariosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[6];
                parametros[0] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[1] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[2] = new SqlParameter("@fc_Despacho", SqlDbType.VarChar) { Value = despacho };
                parametros[3] = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value = valor };
                parametros[4] = new SqlParameter("@idUsuarioPadre", SqlDbType.VarChar) { Value = supervisor };
                parametros[5] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosUsuariosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaSupervisorValor: " + ex.Message);
                return new List<ResultadosUsuariosRankingModel>();
            }
        }

        /// <summary>
        /// Obtiene Tabla Gestores - Delegacion
        /// </summary>
        /// <param name="tipoDashboard">Rol que ejecuta la consulta</param>
        /// <param name="indicador">Rol que ejecuta la consulta</param>
        /// <param name="despacho">Nombre del indicador</param>
        /// <param name="supervisor">Id del supervisor</param>
        /// <param name="delegacion">Id de la delegacion</param>
        /// <param name="valor">Constante 100</param>
        /// <param name="tipoFormulario">Nombre de la Ruta</param>
        /// <returns>Lista con la infomación</returns>
        public List<ResultadosUsuariosRankingModel> ObtenerTablaGestoresDelegacion(String tipoDashboard, String indicador, String despacho, String supervisor, String delegacion, int valor = 100, string tipoFormulario="")
        {
            var indicadores = new List<ResultadosUsuariosRankingModel>();
            try
            {
                var instancia = ConnectionDB.Instancia;
                var parametros = new SqlParameter[7];
                parametros[0] = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar) { Value = tipoDashboard };
                parametros[1] = new SqlParameter("@Indicador", SqlDbType.VarChar) { Value = indicador };
                parametros[2] = new SqlParameter("@fc_Despacho", SqlDbType.VarChar) { Value = despacho };
                parametros[3] = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value = valor };
                parametros[4] = new SqlParameter("@idUsuarioPadre", SqlDbType.VarChar) { Value = supervisor };
                parametros[5] = new SqlParameter("@fc_Delegacion", SqlDbType.VarChar) { Value = delegacion };
                parametros[6] = new SqlParameter("@TipoFormulario", SqlDbType.VarChar) { Value = tipoFormulario };

                var dsArchivos = instancia.EjecutarDataSet("SqlDefault", "dbo.ObtenerRankingIndicadoresMaster", parametros);
                if (dsArchivos != null && dsArchivos.Tables.Count > 0)
                {
                    indicadores = dsArchivos.Tables[0].ToList<ResultadosUsuariosRankingModel>();
                }

                return indicadores;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntRankingIndicadores",
                    "Error en ObtenerTablaGestoresDelegacion: " + ex.Message);
                return new List<ResultadosUsuariosRankingModel>();
            }
        }
    }
}
