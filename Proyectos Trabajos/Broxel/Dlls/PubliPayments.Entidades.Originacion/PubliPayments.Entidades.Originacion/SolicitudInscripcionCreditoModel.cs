using System;
using System.Data;
using System.Data.SqlClient;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.Originacion
{
    public class SolicitudInscripcionCreditoModel
    {
        public string NSS { get; set; } 
        public string CURP { get; set; } 
        public string RFC { get; set; } 
        public string ApellidoPaterno { get; set; } 
        public string ApellidoMaterno { get; set; }
        public string Colonia { get; set; }
        public string Entidad { get; set; }
        public string Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string TelefonoLada { get; set; }
        public string Telefono { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string NombrePatron { get; set; }
        public string NRPPatron { get; set; }
        public string Regimen { get; set; }
        public string ReferenciaLada1 { get; set; }
        public string ReferenciaTelefono1 { get; set; }
        public string ReferenciaLada2 { get; set; }
        public string ReferenciaTelefono2 { get; set; }
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string NumeroCredito { get; set; }
        public string NumeroTarjeta { get; set; }
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public string Domicilio { get; set; }
        public string DiaIdentificacion { get; set; }
        public string MesIdentificacion { get; set; }
        public string AnoIdentificacion { get; set; }
        public string ReferenciaNombre1 { get; set; }
        public string ReferenciaApellidoPaterno1 { get; set; }
        public string ReferenciaApellidoMaterno1 { get; set; }
        public string ReferenciaNombre2 { get; set; }
        public string ReferenciaApellidoPaterno2 { get; set; }
        public string ReferenciaApellidoMaterno2 { get; set; }
        public string MontoCredito { get; set; }
        public string MontoManoObra { get; set; }
        public string Plazo { get; set; }
        public string ReferenciaCelular1 { get; set; }
        public string ReferenciaCelular2 { get; set; }



        public string HorarioHoraDe { get; set; }        
        public string NumeroDependientes { get; set; }
        public string Text4 { get; set; }
        public string PensionAlimenticia { get; set; }
        public string EmpresaTelefono { get; set; }
        public string HorarioMinutosDe { get; set; }
        public string Escolaridad { get; set; }
        public string Vivienda { get; set; }
        public string EmpresaExtension { get; set; }
        public string Celular { get; set; }
        public string EmpresaLada { get; set; }
        public string CorreoElectronico { get; set; }
        public string HorarioMinutosA { get; set; }
        public string HorarioHoraA { get; set; }
        

        public SolicitudInscripcionCreditoModel()
        {
            
        }

        public static SolicitudInscripcionCreditoModel ObtenerSolicitudInscripcionCredito(int idOrden)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerSolicitudInscripcionCredito", "Store Procedure");

            try
            {
                var sql = "exec ObtenerSolicitudCredito " + "@idOrden = " + idOrden;
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ObtenerSolicitudInscripcionCredito", "Error - " + ex.Message);
                return null;
            }
        }

        public static SolicitudInscripcionCreditoModel LlenarModelo(DataSet dataSet)
        {
            var modelo = new SolicitudInscripcionCreditoModel();

            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "SolicitudInscripcionCredito", "LlenarModelo");

            try
            {
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (var colum in dataSet.Tables[0].Columns)
                    {
                        var columnName = colum.ToString();
                        switch (columnName)
                        {
                            case "NSS":
                                modelo.NSS = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NSS))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta NSS");
                                }
                                break;
                            case "CURP":
                                modelo.CURP = dataSetString(dataSet, columnName);
                                break;
                            case "RFC":
                                modelo.RFC = dataSetString(dataSet, columnName);
                                break;
                            case "ApellidoPaterno":
                                modelo.ApellidoPaterno = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.ApellidoPaterno))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta ApellidoPaterno");
                                }
                                break;
                            case "ApellidoMaterno":
                                modelo.ApellidoMaterno = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.ApellidoPaterno))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta ApellidoPaterno");
                                }
                                break;
                            case "Colonia":
                                modelo.Colonia = dataSetString(dataSet, columnName);
                                break;
                            case "Entidad":
                                modelo.Entidad = dataSetString(dataSet, columnName);
                                break;
                            case "Municipio":
                                modelo.Municipio = dataSetString(dataSet, columnName);
                                break;
                            case "CodigoPostal":
                                modelo.CodigoPostal = dataSetString(dataSet, columnName);
                                break;
                            case "TipoIdentificacion":
                                modelo.TipoIdentificacion = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.TipoIdentificacion))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta TipoIdentificacion");
                                }
                                if (modelo.TipoIdentificacion == "1")
                                {
                                    modelo.TipoIdentificacion = "Elector";
                                }
                                else if (modelo.TipoIdentificacion == "2")
                                {
                                    modelo.TipoIdentificacion = "Pasaporte";
                                }
                                break;
                            case "NumeroIdentificacion":
                                modelo.NumeroIdentificacion = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NumeroIdentificacion))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta NumeroIdentificacion");
                                }
                                break;
                            case "TelefonoLada":
                                modelo.TelefonoLada = dataSetString(dataSet, columnName);
                                break;
                            case "Telefono":
                                modelo.Telefono = dataSetString(dataSet, columnName);
                                break;
                            case "Genero":
                                modelo.Genero = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.Genero))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta Genero");
                                }
                                if (modelo.Genero == "Masculino")
                                {
                                    modelo.Genero = "1";
                                }
                                else if (modelo.Genero == "Femenino")
                                {
                                    modelo.Genero = "2";
                                }
                                break;
                            case "EstadoCivil":
                                modelo.EstadoCivil = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.EstadoCivil))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta EstadoCivil");
                                }
                                break;
                            case "NombrePatron":
                                modelo.NombrePatron = dataSetString(dataSet, columnName);
                                break;
                            case "NRPPatron":
                                modelo.NRPPatron = dataSetString(dataSet, columnName);
                                break;
                            case "Regimen":
                                modelo.Regimen = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaLada1":
                                modelo.ReferenciaLada1 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaTelefono1":
                                modelo.ReferenciaTelefono1 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaLada2":
                                modelo.ReferenciaLada2 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaTelefono2":
                                modelo.ReferenciaTelefono2 = dataSetString(dataSet, columnName);
                                break;
                            case "Dia":
                                modelo.Dia = dataSetString(dataSet, columnName);
                                break;
                            case "Mes":
                                modelo.Mes = dataSetString(dataSet, columnName);
                                break;
                            case "Ano":
                                modelo.Ano = dataSetString(dataSet, columnName);
                                break;
                            case "NumeroCredito":
                                modelo.NumeroCredito = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NumeroCredito))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta NumeroCredito");
                                }
                                break;
                            case "NumeroTarjeta":
                                modelo.NumeroTarjeta = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.NumeroTarjeta))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta NumeroTarjeta");
                                }
                                break;
                            case "Ciudad":
                                modelo.Ciudad = dataSetString(dataSet, columnName);
                                break;
                            case "Nombre":
                                modelo.Nombre = dataSetString(dataSet, columnName);
                                break;
                            case "Domicilio":
                                modelo.Domicilio = dataSetString(dataSet, columnName);
                                break;
                            case "DiaIdentificacion":
                                modelo.DiaIdentificacion = dataSetString(dataSet, columnName);
                                break;
                            case "MesIdentificacion":
                                modelo.MesIdentificacion = dataSetString(dataSet, columnName);
                                break;
                            case "AnoIdentificacion":
                                modelo.AnoIdentificacion = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaNombre1":
                                modelo.ReferenciaNombre1 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaApellidoPaterno1":
                                modelo.ReferenciaApellidoPaterno1 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaApellidoMaterno1":
                                modelo.ReferenciaApellidoMaterno1 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaNombre2":
                                modelo.ReferenciaNombre2 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaApellidoPaterno2":
                                modelo.ReferenciaApellidoPaterno2 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaApellidoMaterno2":
                                modelo.ReferenciaApellidoMaterno2 = dataSetString(dataSet, columnName);
                                break;
                            case "MontoCredito":
                                modelo.MontoCredito = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.MontoCredito))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta MontoCredito");
                                }
                                break;
                            case "MontoManoObra":
                                modelo.MontoManoObra = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.MontoManoObra))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta MontoManoObra");
                                }
                                break;
                            case "plazo":
                                modelo.Plazo = dataSetString(dataSet, columnName);
                                if (string.IsNullOrEmpty(modelo.Plazo))
                                {
                                    throw new Exception("SolicitudInscripcionCreditoModel Falta Plazo");
                                }
                                break;
                            case "ReferenciaCelular1":
                                modelo.ReferenciaCelular1 = dataSetString(dataSet, columnName);
                                break;
                            case "ReferenciaCelular2":
                                modelo.ReferenciaCelular2 = dataSetString(dataSet, columnName);
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "LlenarModelo SolicitudInscripcionCredito", "Error - " + ex.Message);
                return null;
            }
        }

        private static string dataSetString(DataSet dataSet, string columnName)
        {            
            return dataSet.Tables[0].Rows[0][columnName] != null ? dataSet.Tables[0].Rows[0][columnName].ToString() : "";
        }

    }
}
