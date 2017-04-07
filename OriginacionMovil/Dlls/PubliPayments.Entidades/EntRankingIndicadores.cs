using System;
using System.Collections.Generic;
using System.Linq;

namespace PubliPayments.Entidades
{
    public class EntRankingIndicadores
    {
        public List<String> ObtenerDelegacionUsuario(string idUsuario)
        {
            var sql =
                string.Format(
                    "select rd.Delegacion from Usuario u "+
                    "left join RelacionDelegaciones rd on rd.idUsuario=u.idUsuario"+
                    " where u.idUsuario='{0}'",
                    idUsuario);
            var contexto = new SistemasCobranzaEntities();
            var ordenes = contexto.Database.SqlQuery<String>(sql).ToList();
            return ordenes;
        }

        public List<IndicadoresRankingModel> ObtenerIndicadores(string permisoModulo)
        {
            var sql =
                string.Format(
                    "select fc_Clave as Clave,ud.fc_Descripcion as Descripcion"+
                    " from dbo.Utils_Descripciones ud"+
                    " left join dbo.Utils_Permisos up on ud.fi_idPermisos = up.idPermiso"+
                    " where ud.fc_Modulo = 'DASHBOARD'"+
                    " and ud.fi_Activo = 1"+
                    " and up.permisoModulo = '{0}'"+
                    " and fc_clave not in('DASH_TOTALMES','DASH_DIASREST')"+
                    " order by ud.fi_Parte,ud.fi_Orden",
                    permisoModulo);
            var contexto = new SistemasCobranzaEntities();
            var ordenes = contexto.Database.SqlQuery<IndicadoresRankingModel>(sql).ToList();
            return ordenes;
        }

        public List<ResultadosDespachosRankingModel> ObtenerDelegaciones(String tipoDashboard, String indicador,String tipoFormulario)
        {
            var contexto = new SistemasCobranzaEntities();
            /*
            var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            var indicadorParameter = new SqlParameter("@Indicador", indicador);
            var master = new SqlParameter("@Master", "DelegacionAdministrador");
            var tipoFormularioParameter = new SqlParameter("@TipoFormulario", tipoFormulario);
            */
            var sql =
                "EXEC ObtenerRankingIndicadoresMaster @Master = 'DelegacionAdministrador', @fc_DashBoard = '" + tipoDashboard +
                "', @Indicador = '" + indicador + "',@TipoFormulario ='" + tipoFormulario + "'";
            var ordenes = contexto.Database.SqlQuery<ResultadosDespachosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            
            return ordenes;
        }

        public List<ResultadosUsuariosRankingModel> ObtenerTablaDelegaciones(String tipoDashboard, String indicador, String despacho, int valor = 100,string tipoFormulario="")
        {
            var contexto = new SistemasCobranzaEntities();

           /* var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", SqlDbType.VarChar, 100) { Value = tipoDashboard};
            var indicadorParameter = new SqlParameter("@Indicador", SqlDbType.VarChar, 100) { Value = indicador };
            var despachoParameter = new SqlParameter("@fc_Despacho", SqlDbType.VarChar, 100) { Value =  despacho};
            var valorParameter = new SqlParameter("@valorSuperior", SqlDbType.Int) { Value =  valor};  //para calcular porcentaje
            var master = new SqlParameter("@Master", SqlDbType.VarChar, 100) { Value =  "DelegacionDespacho"};*/

            var sql =
                "EXEC ObtenerRankingIndicadoresMaster @Master = 'DelegacionDespacho', @fc_DashBoard = '" + tipoDashboard +
                "', @Indicador = '" + indicador + "', @valorSuperior = " + valor + ", @fc_Despacho = '" + despacho + "',@TipoFormulario ='" + tipoFormulario+"'";

            var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
           
            //var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>("ObtenerRankingIndicadoresMaster @Master, @fc_DashBoard, @Indicador,@valorSuperior,@fc_Despacho",master, tipoDashboardParameter, indicadorParameter, valorParameter, despachoParameter).ToList();
            return ordenes;
        }

        public List<ResultadosDespachosRankingModel> ObtenerTablaDespachos(String tipoDashboard, String indicador,String tipoFormulario)
        {
            var contexto = new SistemasCobranzaEntities();

            /*var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            var indicadorParameter = new SqlParameter("@Indicador", indicador);
            var master = new SqlParameter("@Master", "Despacho");
            var tipoFormularioParameter = new SqlParameter("@TipoFormulario", tipoFormulario);*/
            var sql =
                "EXEC ObtenerRankingIndicadoresMaster @Master = 'Despacho', @fc_DashBoard = '" + tipoDashboard +
                "', @Indicador = '" + indicador  + "',@TipoFormulario ='" + tipoFormulario + "'";
            var ordenes = contexto.Database.SqlQuery<ResultadosDespachosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            return ordenes;
        } 
 
        public List<ResultadosUsuariosRankingModel> ObtenerTablaGestores(String tipoDashboard, String indicador,String despacho,String supervisor,int valor=100,string tipoFormulario="")
        {
            var contexto = new SistemasCobranzaEntities();

            /*
            var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            var indicadorParameter = new SqlParameter("@Indicador", indicador);
            var despachoParameter = new SqlParameter("@fc_Despacho", despacho);
            var supervisorParameter = new SqlParameter("@idUsuarioPadre", supervisor);
            var valorParameter = new SqlParameter("@valorSuperior", valor);  //para calcular porcentaje
            var master = new SqlParameter("@Master", "Supervisor");
            var tipoFormularioParameter = new SqlParameter("@TipoFormulario", tipoFormulario);
            */

            var sql = "EXEC ObtenerRankingIndicadoresMaster @Master='Supervisor'," +
                      " @fc_DashBoard='" + tipoDashboard +
                      "', @Indicador='" + indicador + 
                      "',@fc_Despacho='" + despacho + 
                      "',@valorSuperior='" + valor +
                      "',@idUsuarioPadre='" + supervisor + 
                      "', @TipoFormulario ='" + tipoFormulario + "'";
            var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            return ordenes;
        }

        public List<ResultadosUsuariosRankingModel> ObtenerTablaDespachosDelegacion(String tipoDashboard, String indicador, String delegacion, int valor = 100, string tipoFormulario="")
        {
            var contexto = new SistemasCobranzaEntities();

            //var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            //var indicadorParameter = new SqlParameter("@Indicador", indicador);
            //var delegacionParameter = new SqlParameter("@fc_Delegacion", delegacion);
            //var valorParameter = new SqlParameter("@valorSuperior", valor);  //para calcular porcentaje
            //var master = new SqlParameter("@Master", "Delegacion");
            var sql = "EXEC ObtenerRankingIndicadoresMaster @Master='Delegacion', @fc_DashBoard='" + tipoDashboard +
                      "', @Indicador='" + indicador + "',@fc_Delegacion='" + delegacion + "',@valorSuperior=" + valor + ", @TipoFormulario ='"+tipoFormulario+"'";
            var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            return ordenes;
        }

        public List<ResultadosUsuariosRankingModel> ObtenerTablaSupervisoresDelegacion(String tipoDashboard, String indicador, String despacho, String delegacion, int valor = 100, string tipoFormulario="")
        {
            var contexto = new SistemasCobranzaEntities();

            //var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            //var indicadorParameter = new SqlParameter("@Indicador", indicador);
            //var despachoParameter = new SqlParameter("@fc_Despacho", despacho);
            //var delegacionParameter = new SqlParameter("@fc_Delegacion", delegacion);
            //var masterParameter = new SqlParameter("@Master", "");
            //var supervisorParameter = new SqlParameter("@idUsuarioPadre", "");
            //var valorParameter = new SqlParameter("@valorSuperior", valor);//para calcular porcentaje
            var sql = "EXEC ObtenerRankingIndicadoresMaster @fc_DashBoard=N'" + tipoDashboard + "',@Indicador=N'" +
                      indicador + "',@fc_Despacho=N'" + despacho + "',@valorSuperior=" + valor + ",@fc_Delegacion=N'" +
                      delegacion + "',  @TipoFormulario = '" + tipoFormulario+"'";
            var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            return ordenes;
        }

        public List<ResultadosUsuariosRankingModel> ObtenerTablaSupervisorValor(String tipoDashboard, String indicador, String despacho, String supervisor, int valor = 100, string tipoFormulario="")
        {
            var contexto = new SistemasCobranzaEntities();

            //var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            //var indicadorParameter = new SqlParameter("@Indicador", indicador);
            //var despachoParameter = new SqlParameter("@fc_Despacho", despacho);
            //var delegacionParameter = new SqlParameter("@fc_Delegacion", delegacion);
            //var masterParameter = new SqlParameter("@Master", "");
            //var supervisorParameter = new SqlParameter("@idUsuarioPadre", "");
            //var valorParameter = new SqlParameter("@valorSuperior", valor);//para calcular porcentaje
            var sql = "EXEC ObtenerRankingIndicadoresMaster @fc_DashBoard=N'" + tipoDashboard + "',@Indicador=N'" +
                      indicador + "',@fc_Despacho=N'" + despacho + "',@idUsuarioPadre='" + supervisor +
                      "',@valorSuperior=" + valor + ", @TipoFormulario = '" + tipoFormulario+"'";
            var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            return ordenes;
        }

        public List<ResultadosUsuariosRankingModel> ObtenerTablaGestoresDelegacion(String tipoDashboard, String indicador, String despacho, String supervisor, String delegacion, int valor = 100, string tipoFormulario="")
        {
            var contexto = new SistemasCobranzaEntities();

            //var tipoDashboardParameter = new SqlParameter("@fc_DashBoard", tipoDashboard);
            //var indicadorParameter = new SqlParameter("@Indicador", indicador);
            //var despachoParameter = new SqlParameter("@fc_Despacho", despacho);
            //var supervisorParameter = new SqlParameter("@idUsuarioPadre", supervisor);
            //var delegacionParameter = new SqlParameter("@fc_Delegacion", delegacion);
            //var valorParameter = new SqlParameter("@valorSuperior", valor);  //para calcular porcentaje
            var sql = "EXEC ObtenerRankingIndicadoresMaster @fc_DashBoard='" + tipoDashboard + "', @Indicador='" +
                      indicador + "',@fc_Despacho='" + despacho + "',@idUsuarioPadre='" + supervisor +
                      "',@valorSuperior=" + valor + ",@fc_Delegacion='" + delegacion + "', @TipoFormulario='" + tipoFormulario+"'";
            var ordenes = contexto.Database.SqlQuery<ResultadosUsuariosRankingModel>(sql).OrderBy(d => d.Posicion).ToList();
            return ordenes;
        }

    }
}
