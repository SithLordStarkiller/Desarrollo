using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PubliPayments.Entidades
{
    public class EntReportes
    {
        /// <summary>
        /// Actualiza en estatus 2 un reporte o si no existe un registro en el mes corriente lo inserta
        /// </summary>
        /// <param name="idUsuarioReporte">idUsuario que solicita el reporte</param>
        /// <param name="tipoReporte">Tipo de reporte que se necesita 1-General,2-asignacion</param>
        /// <returns>String que contiene el id registro modificado, "Error:" algo paso </returns>
        /// JARO
        public string ActualizarEstatusReporte(int idUsuarioReporte, int tipoReporte)
        {
            var instancia = ConnectionDB.Instancia;
            var parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@idUsuarioReporte", SqlDbType.Int) {Value = idUsuarioReporte};
            parametros[1] = new SqlParameter("@TipoReporte", SqlDbType.Int) {Value = tipoReporte};
            var result = instancia.EjecutarEscalar("SqlDefault", "ActualizarEstatusReporte", parametros);
            return result;
        }

        /// <summary>
        /// Se obtiene la infomacion necesaria para armar el reporte de Asignacion
        /// </summary>
        /// <param name="delegacion">Número de delegación que se necesita buscar</param>
        /// <param name="despacho">idDominio del cual se necesita buscar</param>
        /// <param name="idsupervisor">idUsuario del cual se necesita buscar </param>
        /// <returns>Tablas para armar el reporte, 1-identificadores que une campos clave del reporte, 2- tabla con informacion distintiva</returns>
        /// JARO
        public DataSet ObtenerReporteAsignacion(string delegacion, string despacho, string idsupervisor)
        {
            var instancia = ConnectionDB.Instancia;
            instancia.TimeOut = 300;
            var parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@Delegacion", SqlDbType.VarChar, 50) {Value = delegacion};
            parametros[1] = new SqlParameter("@Despacho", SqlDbType.VarChar, 50) {Value = despacho};
            parametros[2] = new SqlParameter("@idSupervisor", SqlDbType.VarChar, 50) {Value = idsupervisor};

            return instancia.EjecutarDataSet("SqlDefault", "ObtenerReporteAsignacion", parametros);

        }
       
        /// <summary>
        /// Obtiene infomacion de un registro de la tabla de Reportes
        /// </summary>
        /// <param name="idUsuarioPadre">idUsuario al que esta relacionado el reporte</param>
        /// <param name="idReporte">id del reporte a buscar</param>
        /// <param name="tipo">Tipo del reporte necesitado, 1-General,2-Asignacion</param>
        /// <returns></returns>
        public DataSet ObtenerEstatusReporte(int idUsuarioPadre, int? idReporte, int tipo)
        {

            var instancia = ConnectionDB.Instancia;
            var sqlQuery = idReporte != null
                ? String.Format(
                    "SELECT idReporte,tipo,Estatus,Fecha FROM Reportes WITH (NOLOCK) WHERE idReporte = '{0}';",
                    idReporte)
                : String.Format(
                    "SELECT  TOP 1 idReporte,tipo,Estatus,Fecha FROM Reportes WITH (NOLOCK) where idPadre = '{0}' and Tipo = '{1}' AND CONVERT(CHAR(7), Fecha, 120) = CONVERT(CHAR(7), GETDATE(), 120) ORDER BY Fecha DESC;",
                    idUsuarioPadre, tipo);

            return instancia.EjecutarDataSet("SqlDefault", sqlQuery);
        }

        /// <summary>
        /// Inserta el texto correspondiente al reporte generado
        /// </summary>
        /// <param name="idReporte">id del reporte a actualizar</param>
        /// <param name="reporteTxt">Texto que corresponde al reporte</param>
        /// <param name="idPadre">id de usuario propietario del reporte</param>
        /// <param name="tipo">tipo de reporte a insertar</param>
       
        public void InsertaReporteTexto(int idReporte, StringBuilder reporteTxt, int idPadre, int tipo)
        {
            var instancia = ConnectionDB.Instancia;
            instancia.TimeOut = 300;
            var parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("@idReporte", SqlDbType.Int) { Value = idReporte };
            parametros[1] = new SqlParameter("@ReporteTxt", SqlDbType.NVarChar) { Value = reporteTxt.ToString() };
            parametros[2] = new SqlParameter("@idPadre", SqlDbType.Int) { Value = idPadre };
            parametros[3] = new SqlParameter("@tipo ", SqlDbType.Int) { Value = tipo };
            
            instancia.EjecutarDataSet("SqlDefault", "InsertaReporte", parametros);
        }


        public String VerificaBloqueoAsignaciones()
        {
            var instancia = ConnectionDB.Instancia;
            const string sqlQuery = "SELECT  valor FROM catalogogeneral WITH (NOLOCK) WHERE llave='BloqueoReasignaciones'";
             return instancia.EjecutarEscalar("SqlDefault", sqlQuery);
           
            
        }
    }
}
