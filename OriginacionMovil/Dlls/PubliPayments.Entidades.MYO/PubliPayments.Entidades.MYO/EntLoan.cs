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

        public DataSet ActualizarField(string field, int LoanDocumentationRequestId, string value, int Status,string observations)
        {
            var ds = new DataSet();
            try
            {
                ConexionSql.EstalecerConnectionString(
                    ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();

                var sc = new SqlCommand("ActualizarFiled", cnn);

                var parametros = new SqlParameter[5];

                parametros[0] = new SqlParameter("@Field", SqlDbType.VarChar) { Value = field };
                parametros[1] = new SqlParameter("@LoanDocumentationRequestId", SqlDbType.BigInt) { Value = LoanDocumentationRequestId };
                parametros[2] = new SqlParameter("@Value", SqlDbType.VarChar) { Value = value };
                parametros[3] = new SqlParameter("@Status", SqlDbType.BigInt) { Value = Status };
                parametros[4] = new SqlParameter("@Observation", SqlDbType.VarChar) { Value = observations };
                
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

        public bool RechazoCorrecionMyo(int id)
        {
            var ds = new DataSet();
            try
            {
                ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["FlockMYO"].ConnectionString);
                var instancia = ConexionSql.Instance;
                var cnn = instancia.IniciaConexion();


                var sc = new SqlCommand("ValidacionSiRechazoCorr", cnn);

                var parametros = new SqlParameter[1];
                parametros[0] = new SqlParameter("@id", SqlDbType.BigInt) { Value = id };

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

            if (ds.Tables.Count <= 0)
                return false;

            if (ds.Tables[0].Rows.Count <= 0)
                return false;

            var num = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            if(num > 0)
                return true;

            return false;
        }

        public Documento ObtenerDocumento(string solicitud, string token)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("ObtenerDocumento", cnn);

                var parametros = new SqlParameter[2];

                parametros[0] = new SqlParameter("@Solicitud", SqlDbType.VarChar) { Value = solicitud };
                parametros[1] = new SqlParameter("@Token", SqlDbType.VarChar) { Value = token };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "ObtenerDocumento - Error: " + ex.Message);
            }

            return ds.Tables.Count > 0 ? BindingDocumento(ds.Tables[0].Rows[0]) : new Documento();
        }

        public bool InsActDocumento(Documento doc)
        {
            var instancia = ConexionSql.Instance;
            var cnn = instancia.IniciaConexion();
            var ds = new DataSet();
            try
            {
                var sc = new SqlCommand("InsActDocumento", cnn);

                var parametros = new SqlParameter[9];

                parametros[0] = new SqlParameter("@Solicitud", SqlDbType.VarChar) { Value = doc.IdSolicitud };
                parametros[1] = new SqlParameter("@Token", SqlDbType.VarChar) { Value = doc.Token };
                parametros[2] = new SqlParameter("@FechaAlta", SqlDbType.DateTime) { Value = doc.FechaAlta };
                parametros[3] = new SqlParameter("@FechaRespuesta", SqlDbType.DateTime) { Value = doc.FechaRespuesta };
                parametros[4] = new SqlParameter("@UsuarioRespuesta", SqlDbType.VarChar) { Value = doc.UsuarioRespuesta };
                parametros[5] = new SqlParameter("@Respuesta", SqlDbType.VarChar) { Value = doc.Respuesta };
                parametros[6] = new SqlParameter("@UrlInterno", SqlDbType.VarChar) { Value = doc.UrlInterno };
                parametros[7] = new SqlParameter("@UrlExterno", SqlDbType.VarChar) { Value = doc.UrlExterno };
                parametros[8] = new SqlParameter("@Intentos", SqlDbType.VarChar) { Value = doc.Intentos };

                sc.Parameters.AddRange(parametros);
                sc.CommandType = CommandType.StoredProcedure;
                var sda = new SqlDataAdapter(sc);
                sda.Fill(ds);
                instancia.CierraConexion(cnn);
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "EntLoan", "InsActDocumento - Error: " + ex.Message);
                return false;
            }
        }

        private Documento BindingDocumento(DataRow row)
        {
            var face = new Documento();
            var props = face.GetType().GetProperties().ToDictionary(p => p.Name);

            foreach (DataColumn col in row.Table.Columns)
            {
                var name = col.ColumnName;
                if (row[name] == DBNull.Value || !props.ContainsKey(name)) continue;
                var item = row[name];
                var p = props[name];
                Type t = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                if (p.PropertyType != col.DataType)
                    item = Convert.ChangeType(item, t);
                p.SetValue(face, item, null);
            }

            return face;
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
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "EntLoan", "ObtenerXmlCirculoAzul - Error : " + ex.Message);
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
