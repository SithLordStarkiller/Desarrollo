using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class CaratulaContratoModel
    {
        public string TasaInteres { get; set; }
        public string Domicilio { get; set; }
        public string MontoCredito { get; set; }
        public string NumeroTarjeta { get; set; }
        public string NSS { get; set; }
        public string NombreAcreditado { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string Identificacion { get; set; }
        public string PlazoCredito { get; set; }
        public string AmortizacionMensual { get; set; }
        public string LugarFecha { get; set; }
        public string MontoTotal { get; set; }
        public string NumeroCredito { get; set; }

        public CaratulaContratoModel()
        {
            
        }

        /// <summary>
        /// Ejecuta el store para obtener la informacion del pdf Caraturla Contrato
        /// </summary>
        /// <param name="idOrden">id Orden que se quiere mostrar</param>
        /// <returns></returns>
        public static CaratulaContratoModel ObtenerCaratulaContratoModel(int idOrden)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerCaratulaContratoModel", "Store Procedure");
            try
            {
                var sql = "exec ObtenerCaratulaContrato " + "@idOrden = " + idOrden;
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                return LlenarModelo(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerCaratulaContratoModel", "Error:" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Recibe un dataset Para leerlo y llenar el modelo CaratulaContratoModel
        /// </summary>
        /// <param name="dataSet">Datos que devuelve el store ObtenerCaratulaContrato</param>
        /// <returns></returns>
        public static CaratulaContratoModel LlenarModelo(DataSet dataSet)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "CaratulaContrato", "Llenando modelo");
            var modelo = new CaratulaContratoModel();

            try
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (var colum in dataSet.Tables[0].Columns)
                    {
                        string columnName = colum.ToString();
                        switch (columnName)
                        {
                            case "TasaInteres":
                                modelo.TasaInteres = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.TasaInteres))
                                {
                                    throw new Exception("CaratulaContratoModel Falta TasaInteres");
                                }
                                break;
                            case "Domicilio":
                                modelo.Domicilio = dataSetString(dataSet, columnName);
                                break;
                            case "MontoCredito":
                                modelo.MontoCredito = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.MontoCredito))
                                {
                                    throw new Exception("CaratulaContratoModel Falta MontoCredito");
                                }
                                break;
                            case "NumeroTarjeta":
                                modelo.NumeroTarjeta = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NumeroTarjeta))
                                {
                                    throw new Exception("CaratulaContratoModel Falta NumeroTarjeta");
                                }
                                break;
                            case "NSS":
                                modelo.NSS = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NSS))
                                {
                                    throw new Exception("CaratulaContratoModel Falta NSS");
                                }
                                break;
                            case "NombreAcreditado":
                                modelo.NombreAcreditado = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NombreAcreditado))
                                {
                                    throw new Exception("CaratulaContratoModel Falta NombreAcreditado");
                                }
                                break;
                            case "CURP":
                                modelo.CURP = dataSetString(dataSet, columnName);
                                break;
                            case "RFC":
                                modelo.RFC = dataSetString(dataSet, columnName);
                                break;
                            case "Identificacion":
                                modelo.Identificacion = dataSetString(dataSet, columnName);
                                break;
                            case "PlazoCredito":
                                modelo.PlazoCredito = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.PlazoCredito))
                                {
                                    throw new Exception("CaratulaContratoModel Falta PlazoCredito");
                                }
                                break;
                            case "AmortizacionMensual":
                                modelo.AmortizacionMensual = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.AmortizacionMensual))
                                {
                                    throw new Exception("CaratulaContratoModel Falta AmortizacionMensual");
                                }
                                break;
                            case "LugarFecha":
                                modelo.LugarFecha = dataSetString(dataSet, columnName);
                                break;
                            case "MontoTotal":
                                modelo.MontoTotal = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.MontoTotal))
                                {
                                    throw new Exception("CaratulaContratoModel Falta MontoTotal");
                                }
                                break;
                            case "NumeroCredito":
                                modelo.NumeroCredito = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NumeroCredito))
                                {
                                    throw new Exception("CaratulaContratoModel Falta MontoTotal");
                                }
                                break;
                        }
                    }
                }
                else
                {
                    modelo = null;
                }
                return modelo;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "LLenarModelo CaratulaContrato", "Error:" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Regresa el string de cada registro del dataset
        /// </summary>
        /// <param name="dataSet">Datos de store ObtenerCaratulaContrato</param>
        /// <param name="columnName">nombre de la columna del cual quiere el registro</param>
        /// <returns></returns>
        private static string dataSetString(DataSet dataSet, string columnName)
        {
            return dataSet.Tables[0].Rows[0][columnName] != null ? dataSet.Tables[0].Rows[0][columnName].ToString() : "";
        }
    }
}
