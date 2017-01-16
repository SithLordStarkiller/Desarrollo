using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class ReciboTarjetaModel
    {
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anno { get; set; }
        public string NombreTrabajador { get; set; }
        public string NumeroTarjeta { get; set; }
        public string Credito { get; set; }

        public ReciboTarjetaModel()
        {

        }

        public static ReciboTarjetaModel ObtenerReciboTarjetaModel(int idOrden)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerReciboTarjetaModel", "Store Procedure");

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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerReciboTarjetaModel", "Error - " + ex.Message);
                return null;
            }
        }

        public static ReciboTarjetaModel LlenarModelo(DataSet dataSet)
        {
            var modelo = new ReciboTarjetaModel();

            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ReciboTarjetaModel", "Llenando modelo");
            try
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (var colum in dataSet.Tables[0].Columns)
                    {
                        string columnName = colum.ToString();
                        switch (columnName)
                        {
                            case "Mes":
                                modelo.Mes = DataSetString(dataSet, columnName);
                                break;
                            case "Credito":
                                modelo.Credito = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.Credito))
                                {
                                    throw new Exception("ReciboTarjetaModel Falta Credito");
                                }
                                break;
                            case "Ano":
                                modelo.Anno = DataSetString(dataSet, columnName);
                                break;
                            case "NombreTrabajador":
                                modelo.NombreTrabajador = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NombreTrabajador))
                                {
                                    throw new Exception("ReciboTarjetaModel Falta NombreTrabajador");
                                }
                                break;
                            case "Dia":
                                modelo.Dia = DataSetString(dataSet, columnName);
                                break;
                            case "NumeroTarjeta":
                                modelo.NumeroTarjeta = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NumeroTarjeta))
                                {
                                    throw new Exception("ReciboTarjetaModel Falta NumeroTarjeta");
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "LlenarModelo ReciboTarjetaModel", "Error - " + ex.Message);
                return null;
            }
        }

        private static string DataSetString(DataSet dataSet, string columnName)
        {
            return dataSet.Tables[0].Rows[0][columnName] != null ? dataSet.Tables[0].Rows[0][columnName].ToString() : "";
        }
    }
}
