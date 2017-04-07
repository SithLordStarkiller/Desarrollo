using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class Reportes
    {
        /// <summary>
        /// Actualiza en estatus 2 un reporte o si no existe un registro en el mes corriente lo inserta
        /// </summary>
        /// <param name="idUsuarioReporte">idUsuario que solicita el reporte</param>
        /// <param name="tipoReporte">Tipo de reporte que se necesita 1-General,2-asignacion</param>
        /// <returns>String que contiene el id registro modificado, "Error:" algo paso </returns>
        public string ActualizarEstatusReporte(int idUsuarioReporte, int tipoReporte)
        {
            try
            {
                return new EntReportes().ActualizarEstatusReporte(idUsuarioReporte, tipoReporte);
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt16(idUsuarioReporte), "Reportes","ActualizarEstatusReporte- "+ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Se obtiene la infomacion necesaria para armar el reporte de Asignacion
        /// </summary>
        /// <param name="idusuarioLog">id usuario para registraren log</param>
        /// <param name="delegacion">Número de delegación que se necesita buscar</param>
        /// <param name="despacho">idDominio del cual se necesita buscar</param>
        /// <param name="idsupervisor">idUsuario del cual se necesita buscar </param>
        /// <returns>Tablas para armar el reporte, 1-identificadores que une campos clave del reporte, 2- tabla con informacion distintiva</returns>
        public DataSet ObtenerReporteAsignacion(string idusuarioLog,string delegacion, string despacho, string idsupervisor)
        {
            try
            {
                return new EntReportes().ObtenerReporteAsignacion(delegacion, despacho, idsupervisor);
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt16(idusuarioLog), "Reportes","ObtenerReporteAsignacion- "+ ex.Message);
            }
            return null;
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
            try
            {
                return new EntReportes().ObtenerEstatusReporte(idUsuarioPadre, idReporte, tipo);
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt16(idUsuarioPadre), "Reportes","ObtenerEstatusReporte- "+ ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Inserta el texto correspondiente al reporte generado
        /// </summary>
        /// <param name="idReporte">id del reporte a actualizar</param>
        /// <param name="reporteTxt">Texto que corresponde al reporte</param>
        /// <param name="idPadre">id de usuario propietario del reporte</param>
        /// <param name="tipo">tipo de reporte a insertar</param>
        public void InsertaReporteTexto(int idReporte, StringBuilder reporteTxt, int idPadre,int tipo)
        {
            try
            {
                new EntReportes().InsertaReporteTexto(idReporte, reporteTxt, idPadre,tipo);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idPadre, "Reportes","InsertaReporteTexto- "+ ex.Message);
            }
           
        }


        public string VerificaBloqueoAsignaciones()
        {
            try
            {
                return new EntReportes().VerificaBloqueoAsignaciones();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Reportes","VerificaBloqueoAsignaciones-" +ex.Message);
            }
            return "0";
        }
        /// <summary>
        /// Genera el reporte en formato csv 
        /// </summary>
        /// <param name="dataSource">Origen de los datos a procesar</param>
        /// <returns>StringBuilder con la informacion procesada</returns>
        public StringBuilder ArmarReporteCSV(DataSet dataSource)
        {
            var response = new StringBuilder();
            try
            {
                if (dataSource.Tables[0].Rows.Count > 0)
                {
                    int numTablas = dataSource.Tables.Count-1;
                    var header = dataSource.Tables[0].Rows[0].ItemArray[0].ToString().Split('|');
                    response.AppendLine(header[0]);

                    if (response.Length > 1)
                    {
                        DataTable table = dataSource.Tables[numTablas];
                        var columns = header[1].Split(',');
                        if (table.Rows.Count==0)
                            response.AppendLine("No hay datos disponibles");
                        else
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                var sbR = new StringBuilder();
                                foreach (var c in columns)
                                {
                                    sbR.Append(row[c] != null ? "," + SinComas(row[c]) : ",");
                                }
                                response.AppendLine(sbR.Remove(0, 1).ToString());
                            }   
                        }
                    }
                    else
                    {
                        response.AppendLine("No hay datos disponibles");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Reportes", "ArmarReporteCSV - " + ex.Message);
                response.Clear();
                response.AppendLine("No hay datos disponibles");
            }
            return response;
        }


         /// <summary>
        /// Genera el reporte en formato csv 
        /// </summary>
        /// <param name="dataSource">Origen de los datos a procesar</param>
        /// <returns>StringBuilder con la informacion procesada</returns>
        public StringBuilder ArmarReporteCSVAsignacion(DataSet dataSource)
        {
            var response = new StringBuilder();
            try
            {
                if (dataSource.Tables[0].Rows.Count > 0)
                {
                    int numTablas = dataSource.Tables.Count-1;
                    var header = dataSource.Tables[0].Rows[0].ItemArray[0].ToString().Split('|');
                    response.AppendLine(header[0]);

                    if (response.Length > 1)
                    {
                        DataTable table = dataSource.Tables[numTablas];
                        var columns = header[1].Split(',');
                        if (table.Rows.Count==0)
                            response.AppendLine("No hay datos disponibles");
                        else
                        {

                            DataTable claves = dataSource.Tables[numTablas-1];
                            var clavesDir = new Dictionary<int, string>();
                            foreach (DataRow row in claves.Rows)
                            {

                                clavesDir.Add(Convert.ToInt32(row["idClave"]), string.Format("{0},{1},{2}", row["TX_NOMBRE_DESPACHO"], row["CV_DESPACHO"], row["CC_DESPACHO"]));
                            }


                            foreach (DataRow row in table.Rows)
                            {
                                var sbR = new StringBuilder();
                                foreach (var c in columns)
                                {
                                    if (c.StartsWith("Clave") )
                                    {
                                        var key =Convert.ToInt32(row[c].ToString());
                                        sbR.Append(","+clavesDir[key]);
                                    }
                                    else
                                        sbR.Append(row[c] != null ? "," + row[c] : ",");
                                }
                                response.AppendLine(sbR.Remove(0, 1).ToString());
                            }   
                        }
                    }
                    else
                    {
                        response.AppendLine("No hay datos disponibles");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Reportes", "ArmarReporteCSV - " + ex.Message);
                response.Clear();
                response.AppendLine("No hay datos disponibles");
            }
            return response;
        }


        private string SinComas(Object valor)
        {
            string respuesta = "";
            if (valor != null)
            {
                respuesta = valor.ToString();
                respuesta = respuesta.Replace(",", " ");
            }
            return respuesta;
        }
    }
}
