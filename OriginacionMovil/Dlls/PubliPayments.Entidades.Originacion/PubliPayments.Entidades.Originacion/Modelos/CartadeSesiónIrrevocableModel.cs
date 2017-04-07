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
    public class CartadeSesionIrrevocableModel
    {
        public string Ciudad { get; set; }
        public string FechaDia { get; set; }
        public string FechaMes{ get; set; }
        public string FechaAnio{ get; set; }
        public string NombreTrabajador { get; set; }


        public string NoSeguridadSocial { get; set; }

        public static CartadeSesionIrrevocableModel ObtenerCartadeSesionIrrevocable(int idOrden)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ObtenerCartadeSesionIrrevocable", "Store Procedure");

            try
            {
                var sql = "exec ObtenerCartadeSesionIrrevocable " + "@idOrden = " + idOrden;
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerCartadeSesionIrrevocable", "Error - " + ex.Message);
                return null;
            }
        }

        public static CartadeSesionIrrevocableModel LlenarModelo(DataSet dataSet)
        {
            var modelo = new CartadeSesionIrrevocableModel();

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "CartadeSesionIrrevocableModel", "Llenando modelo");
            try
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    string fechaDia = string.Empty;
                    string fechaMes = string.Empty;
                    string fechaAnnio = string.Empty;
                    foreach (var colum in dataSet.Tables[0].Columns)
                    {
                        string columnName = colum.ToString();
                        switch (columnName)
                        {
                            case "Mes":
                                modelo.FechaMes = DataSetString(dataSet, columnName);
                                break;
                            case "nss":
                                modelo.NoSeguridadSocial = DataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NoSeguridadSocial))
                                {
                                    throw new Exception("CartadeSesionIrrevocableModel Falta Credito");
                                }
                                break;
                            case "Ano":
                                modelo.FechaAnio = DataSetString(dataSet, columnName);
                                break;
                            
                            case "Dia":
                                modelo.FechaDia = DataSetString(dataSet, columnName);
                                break;

                            case "Lugar":
                                modelo.Ciudad = DataSetString(dataSet, columnName);
                                break;

                            case "NombreTrabajador":
                                modelo.NombreTrabajador = DataSetString(dataSet, columnName);
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "LlenarModelo CartadeSesionIrrevocableModel", "Error - " + ex.Message);
                return null;
            }
        }

        private static string DataSetString(DataSet dataSet, string columnName)
        {
            return dataSet.Tables[0].Rows[0][columnName] != null ? dataSet.Tables[0].Rows[0][columnName].ToString() : "";
        }
    }
}
