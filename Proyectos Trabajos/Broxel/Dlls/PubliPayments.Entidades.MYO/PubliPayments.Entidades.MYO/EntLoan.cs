using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades.MYO
{
    public class EntLoan
    {
        public DataSet ObtenerDatosFlock()
        {
            ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerDatosFlock", cnn) { CommandType = CommandType.StoredProcedure };
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ObtenerDatosFlock - Error: " + ex.Message);
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }
            return ds;
        }

        public DataSet ObtenerDatosReferencias(int identificador, string tipo)
        {
            ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                SqlCommand sc;
                switch (tipo)
                {
                    case "ACREDITADO":
                        sc = new SqlCommand("[ObtenerReferenciasAcreditado]", cnn);
                        break;
                    //case "INVERSIONISTA_1":
                    //    sc = new SqlCommand("[ObtenerReferenciasInversionista_1]", cnn);
                    //    break;
                    //case "INVERSIONISTA_2":
                    //    sc = new SqlCommand("[ObtenerReferenciasInversionista_2]", cnn);
                    //    break;
                    default:
                        return new DataSet();
                }

                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@Identificador", SqlDbType.BigInt) { Value = identificador };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ObtenerDatosReferencias - Error: " + ex.Message);
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }
            return ds;
        }


        public DataSet ObtenerRespuestas(string idOrden, int tipoRegreso = 0)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerRespuestas", cnn);

                var parametros = new SqlParameter[5];

                parametros[0] = new SqlParameter("@tipo", SqlDbType.Int) { Value = 0 };
                parametros[1] = new SqlParameter("@idOrden", SqlDbType.NVarChar, 20) { Value = idOrden };
                parametros[2] = new SqlParameter("@reporte", SqlDbType.Int) { Value = -1 };
                parametros[3] = new SqlParameter("@idUsuarioPadre", SqlDbType.Int) { Value = 0 };
                parametros[4] = new SqlParameter("@TipoRegreso", SqlDbType.Int) { Value = tipoRegreso };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ObtenerRespuestas - Error: " + ex.Message);
            }
            return ds;
        }

        public DataSet ActualizarAcreditado(string id, int status, string tipo)
        {
            var ds = new DataSet();
            try
            {
                ConexionSql.EstalecerConnectionString(
                    ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();


                var sc = new SqlCommand("ActualizarStatusDocumentos", cnn);

                var parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter("@Id", SqlDbType.BigInt) { Value = id };
                parametros[1] = new SqlParameter("@Status", SqlDbType.SmallInt) { Value = status };
                parametros[2] = new SqlParameter("@Tipo", SqlDbType.VarChar) { Value = tipo };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);

                instancia.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ActualizarAcreditado - Error: " + ex.Message);
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }
            return ds;
        }

        public Dictionary<string, string> ObtenerUrlsMyo(string tipo, int id)
        {
            var ds = new DataSet();
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();


                var sc = new SqlCommand("ObtenerUrlsMYO", cnn);

                var parametros = new SqlParameter[2];
                parametros[0] = new SqlParameter("@id", SqlDbType.BigInt) { Value = id };
                parametros[1] = new SqlParameter("@tipo", SqlDbType.VarChar) { Value = tipo };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ObtenerUrlsMyo - Error: " + ex.Message);
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }

            return ds.Tables[0].AsEnumerable().ToDictionary(row => row.Field<string>(0), row => row.Field<string>(1));
        }

        public bool ActualizarDatosRefImg(string comentario, int id, string tipo, int status)
        {
            try
            {
                ConexionSql.EstalecerConnectionString(
                    ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();
                var ds = new DataSet();
                var sc = new SqlCommand("ActualizarDatosRefImg", cnn);
                var parametros = new SqlParameter[4];
                parametros[0] = new SqlParameter("@id", SqlDbType.BigInt) { Value = id };
                parametros[1] = new SqlParameter("@tipo", SqlDbType.VarChar) { Value = tipo };
                parametros[2] = new SqlParameter("@Comentario", SqlDbType.VarChar) { Value = comentario };
                parametros[3] = new SqlParameter("@Status", SqlDbType.Int) { Value = status };
                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);

                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ActualizarDatosRefImg - Error: " + ex.Message);
                return false;
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }
        }

        public DataSet ObtenerXmlCirculoAzul(int id)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "EntLoan", "ObtenerXmlCirculoAzul - id: " + id);
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["CirCred"].ConnectionString);
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();
                var ds = new DataSet();
                string sql = "SELECT "
                             +
                             "XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Nombre/ApellidoPaterno)[1]','varchar(50)') as ApellidoPaterno, "
                             +
                             "XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Nombre/ApellidoMaterno)[1]','varchar(50)') as ApellidoPaterno, "
                             +
                             "XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Nombre/Nombres)[1]','varchar(50)') as ApellidoPaterno, "
                             +
                             "XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Nombre/RFC)[1]','varchar(50)') as ApellidoPaterno "
                             +
                             ",XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Direccion)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/ColoniaPoblacion)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/DelegacionMunicipio)[1]','varchar(100)')  "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Ciudad)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Estado)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/CP)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/FechaResidencia)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/NumeroTelefono)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/TipoDomicilio)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/TipoAsentamiento)[1]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/FechaRegistroDomicilio)[1]','varchar(100)') as Domicilio1 "
                             +
                             "       ,XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Direccion)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/ColoniaPoblacion)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/DelegacionMunicipio)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Ciudad)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Estado)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/CP)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/FechaResidencia)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/NumeroTelefono)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/TipoDomicilio)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/TipoAsentamiento)[2]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/FechaRegistroDomicilio)[2]','varchar(100)') as Domicilio2 "
                             +
                             "       ,XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Direccion)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/ColoniaPoblacion)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/DelegacionMunicipio)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Ciudad)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/Estado)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/CP)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/FechaResidencia)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/NumeroTelefono)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/TipoDomicilio)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/TipoAsentamiento)[3]','varchar(100)') "
                             +
                             "+ ', ' + XML_RESPUESTA.value('(/Respuesta/Personas/Persona/Domicilios/Domicilio/FechaRegistroDomicilio)[3]','varchar(100)') as Domicilio3 "
                             + "From CL.CL_CIRCULO_CREDITO_RESPUESTA "
                             + "Where COD_CLIENTE = " + id;

                var sc = new SqlCommand(sql, cnn);

                sc.CommandType = CommandType.Text;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);

                return ds;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntLoan",
                    "ObtenerXmlCirculoAzul - Error : " + ex.Message);
                return null;
            }
            finally
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            }

            ////var xml = new XmlDocument();
            //StreamReader Stream;
            //Stream = new StreamReader(ConfigurationManager.AppSettings["DirectorioXmlCA"]);
            ////xml.Load(ConfigurationManager.AppSettings["DirecotioXmlCA"]);

            //var contents = Stream.ReadToEnd();
            //var byteArray = Encoding.UTF8.GetBytes(contents);
            //var stream = new MemoryStream(byteArray);
            //var principal = new XmlDocument();
            //principal.Load(stream);

            ////var root = principal.DocumentElement;

            ////Console.WriteLine(root.FirstChild);
            ////Console.WriteLine(root.FirstChild.ChildNodes);

            //////XmlNode nodo = xml.SelectSingleNode("Informacion/VariablesSociodemograficas/CODIGO_SOLICITUD");
            //////nodo.InnerText = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            ////var channel = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["UrlCirculoBuro"]);

            ////channel.Method = "POST";
            ////channel.ContentType = "application/x-www-form-urlencoded";
            ////var sw = new StreamWriter(channel.GetRequestStream());
            ////principal.Save(sw);
            ////var sr = channel.GetResponse();
            ////var responseStream = sr.GetResponseStream();
            ////var xmlResult = XElement.Load(responseStream);

            //return principal;
        }
    }
}
