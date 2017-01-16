using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class ConsultaBuroModel
    {
        public string i { get; set; }
        public string LugarFecha { get; set; }
        public string EntidadFinanciera { get; set; }
        public string Nombre { get; set; }

        public ConsultaBuroModel()
        {

        }

        public static ConsultaBuroModel ObtenerConsultaBuroModel(int idOrden)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerConsultaBuroModel", "Store procedure");
            try
            {
                var sql = "exec ObtenerConsultaBuro " + "@idOrden = " + idOrden;
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerConsultaBuroModel", "Error - " + ex.Message);
                return null;
            }
        }

        public static ConsultaBuroModel LlenarModelo(DataSet dataSet)
        {
            var modelo = new ConsultaBuroModel();

            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ConsultaBuroModel", "Llenando modelo");
            try
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (var colum in dataSet.Tables[0].Columns)
                    {
                        string columnName = colum.ToString();
                        switch (columnName)
                        {
                            case "i":
                                modelo.i = DataSetString(dataSet, columnName);
                                break;
                            case "LugarFecha":
                                modelo.LugarFecha = DataSetString(dataSet, columnName);
                                break;
                            case "EntidadFinanciera":
                                modelo.EntidadFinanciera = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.EntidadFinanciera))
                                {
                                    throw new Exception("ConsultaBuroModel Falta EntidadFinanciera");
                                }
                                break;
                            case "Nombre":
                                modelo.Nombre = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.Nombre))
                                {
                                    throw new Exception("ConsultaBuroModel Falta Nombre");
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "LlenarModelo ConsultaBuroModel", "Error - " + ex.Message);
                return null;
            }
        }

        private static string DataSetString(DataSet dataSet, string columnName)
        {
            return dataSet.Tables[0].Rows[0][columnName] != null ? dataSet.Tables[0].Rows[0][columnName].ToString() : "";
        }
    }
}
