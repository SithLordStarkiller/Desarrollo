using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class DocumentoPreventivoDerhabienteModel
    {
        public string Nombre { get; set; }
        public string NoCredito { get; set; }
        public string NoUnicoAsocATarjeta { get; set; }
        public string FechaCompleta { get; set; }
        
        public string Lugar { get; set; }


        public static DocumentoPreventivoDerhabienteModel ObtenerDocPreventivoModel(int idOrden)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "DocumentoPreventivoDerhabienteModel", "Store Procedure");

            try
            {
                var sql = "exec ObtenerReciboTarjeta " + "@idOrden = " + idOrden;
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerDocPreventivoModel", "Error - " + ex.Message);
                return null;
            }
        }

        public static DocumentoPreventivoDerhabienteModel LlenarModelo(DataSet dataSet)
        {
            var modelo = new DocumentoPreventivoDerhabienteModel();

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "DocumentoPreventivoDerhabienteModel", "Llenando modelo");
            try
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    string fechaDia = string.Empty;
                    string fechaMes = string.Empty;
                    string fechaAnnio = string.Empty;
                    //Propieadades no asignadas
                    modelo.NoUnicoAsocATarjeta = string.Empty;
                    modelo.Lugar = string.Empty;
                    foreach (var colum in dataSet.Tables[0].Columns)
                    {
                        string columnName = colum.ToString();
                        
                        switch (columnName)
                        {
                            case "Mes":
                                fechaMes = DataSetString(dataSet, columnName);
                                break;
                            case "Credito":
                                modelo.NoCredito = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NoCredito))                                
                                    throw new Exception("DocumentoPreventivoDerhabienteModel Falta Crédito");                                
                                break;
                            case "Ano":
                                fechaAnnio = DataSetString(dataSet, columnName);
                                break;
                            case "NombreTrabajador":
                                modelo.Nombre = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.Nombre))
                                {
                                    throw new Exception("DocumentoPreventivoDerhabienteModel Falta NombreTrabajador");
                                }
                                break;
                            case "Dia":
                                fechaDia = DataSetString(dataSet, columnName);
                                break;
                            case "NumeroTarjeta":
                                modelo.NoUnicoAsocATarjeta = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NoUnicoAsocATarjeta))
                                {
                                    throw new Exception("DocumentoPreventivoDerhabienteModel Falta Numero Tarjeta");
                                }
                                break;
                            case "Lugar":
                                modelo.Lugar = DataSetString(dataSet, columnName);
                                break;

                        }                        
                        modelo.FechaCompleta = fechaDia + " de " + fechaMes  + " de " + fechaAnnio;
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "LlenarModelo DocumentoPreventivoDerhabienteModel", "Error - " + ex.Message);
                return null;
            }
        }

        private static string DataSetString(DataSet dataSet, string columnName)
        {
            return dataSet.Tables[0].Rows[0][columnName] != null ? dataSet.Tables[0].Rows[0][columnName].ToString() : "";
        }
    }
}
