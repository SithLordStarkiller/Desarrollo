using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ComCredencial.RequestResponses;

namespace ComCredencial
{
    public class BroxelService
    {
        #region Métodos Públicos

        public ComercioResponse Agregar(RequestResponses.Comercio request)
        {
            var ctx = new BroxelEntities();
            var response = new ComercioResponse();
            var com = ctx.Comercio.FirstOrDefault(x => x.CodigoComercio == request.CodigoComercio);
            if (com != null)
            {
                response.Success = 0;
                return response;
            }
            com = ctx.Comercio.FirstOrDefault(x => x.idComercio == request.idComercio);
            if (com != null)
            {
                response.Success = 0;
                return response;
            }
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                ctx.Comercio.Add(new Comercio
                {
                    CodigoComercio = request.CodigoComercio,
                    idComercio = request.idComercio,
                    Comercio1 = request.NombreComercio
                });
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Success = 0;
                response.UserResponse = ex.ToString();
            }
            return response;
        }

        public NominarCuentaResponse Nominar(NominarCuentaRequest request, string numCuenta)
        {
            var broxelEntities = new BroxelEntities();
            var fechaHoraCreacion = DateTime.Now;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            String cuentaString = request.Cuenta.ToXML();
            cuentaString = cuentaString.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            cuentaString = cuentaString.Replace("<", "&lt;");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace(" />", "/>");
            var webReq = WebRequest.Create("https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/RemoteOperations");
            var soapRequestXml = new XmlDocument();
            string requestToString;

            soapRequestXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                   "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                        "<Execute xmlns=\"http://org.apache.synapse/xsd\">" +
                                            "<Operation>NominacionCuenta</Operation>" +
                                            "<Session xsi:nil=\"true\"/>" +
                                            "<PrevTransactionKey xsi:nil=\"true\"/>" +
                                            "<PrivateData xsi:nil=\"true\"/>" +
                                            "<Encoding xsi:nil=\"true\">XML</Encoding>" +
                                            "<Schema xsi:nil=\"true\">NominacionCuenta</Schema>" +
                                            "<Message>" + cuentaString + "</Message>" +
                                        "</Execute>" +
                                        "</s:Body>" +
                                   "</s:Envelope>");
            webReq.Method = "POST";
            var requestStream = webReq.GetRequestStream();
            soapRequestXml.Save(requestStream);


            var httpResponse = webReq.GetResponse();
            var responseStream = new StreamReader(httpResponse.GetResponseStream());
            var soapResponseString = responseStream.ReadToEnd();
            var soapResponseXml = new XmlDocument();
            soapResponseXml.LoadXml(soapResponseString);

            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                soapRequestXml.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                requestToString = stringWriter.GetStringBuilder().ToString();
            }

            var xmlFragment = soapResponseXml.InnerXml;  //credencialResp.Movimientos.InnerXml.ToString();
            xmlFragment = xmlFragment.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Replace("<soapenv:Envelope xmlns:soapenv=\"http://www.w3.org/2003/05/soap-envelope\">", "").Replace("<soapenv:Body>", "").Replace("</soapenv:Body>", "").Replace("</soapenv:Envelope>", "").Replace("xmlns=\"http://ws.apache.org/ns/synapse\"", "");
            var strReader = new StringReader(xmlFragment);

            var xmlDoc = XDocument.Load(strReader);
            var tarjetaArray = from x in xmlDoc.Descendants("Response") select new { ReturnCode = x.Element("ReturnCode").Value, ReturnMessage = x.Element("ReturnMessage").Value, Message = x.Element("Message").Value };
            //var IdentificadorTransaccion = from X in TarjetaArray.First().Message.Descendants() select new {IdentificadorTransaccion = X.Element("IdentificadorTransaccion").Value};
            var messageString = tarjetaArray.First().Message;
            var posIniciaIdentificador = messageString.IndexOf("&lt;IdentificadorTransaccion&gt;", StringComparison.Ordinal);
            var posFinalIdentificador = messageString.IndexOf("&lt;/IdentificadorTransaccion&gt;", StringComparison.Ordinal);
            var identificadorTransaccion = (posIniciaIdentificador != posFinalIdentificador && posIniciaIdentificador != -1) ? messageString.Substring(posIniciaIdentificador + 32, posFinalIdentificador - posIniciaIdentificador - 32) : "999";

            var nominar = new NominarCuentaResponse
            {
                CodigoRespuesta = Convert.ToInt32(tarjetaArray.First().ReturnCode),
                UserResponse = tarjetaArray.First().ReturnMessage,
                Success = tarjetaArray.First().ReturnCode == "-1" ? 1 : 0,
                FechaCreacion = Convert.ToString(fechaHoraCreacion),
                NumeroAutorizacion = identificadorTransaccion,
            };

            var renominacion = new Renominacion
            {
                Cuenta = numCuenta,
                CodigoRespuesta = nominar.CodigoRespuesta.ToString(CultureInfo.InvariantCulture),
                FechaHoraCreacion = fechaHoraCreacion,
                MensajeRespuesta = nominar.UserResponse,
                Request = requestToString,
                Response = soapResponseXml.InnerXml,
                IdentificadorTransaccion = identificadorTransaccion
            };
            broxelEntities.Renominacion.Add(renominacion);
            try
            {
                broxelEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error Renominando  ", "Error " + exception,
                      "yMQ3E3ert6", "Errores ");
            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com, jesus.valdiviezo@broxel.com", "Error renominando 2  ", "Error " + e,
                     "yMQ3E3ert6", "Errores ");
            }
            nominar.IdRenominacion = renominacion.Id;
            return nominar;
        }

        public NominarCuentaResponse Nominar(NominarCuentaRequest request)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            String cuentaString = request.Cuenta.ToXML();
            cuentaString = cuentaString.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            cuentaString = cuentaString.Replace("<", "&lt;");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace("/ >", "/>");
            cuentaString = cuentaString.Replace(" />", "/>");
            var webReq = WebRequest.Create("https://" + ConfigurationManager.AppSettings["CredencialHost"] + ":" + ConfigurationManager.AppSettings["CredencialPort"] + "/services/RemoteOperations");
            var soapRequestXml = new XmlDocument();

            soapRequestXml.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                   "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                                        "<s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                        "<Execute xmlns=\"http://org.apache.synapse/xsd\">" +
                                            "<Operation>NominacionCuenta</Operation>" +
                                            "<Session xsi:nil=\"true\"/>" +
                                            "<PrevTransactionKey xsi:nil=\"true\"/>" +
                                            "<PrivateData xsi:nil=\"true\"/>" +
                                            "<Encoding xsi:nil=\"true\">XML</Encoding>" +
                                            "<Schema xsi:nil=\"true\">NominacionCuenta</Schema>" +
                                            "<Message>" + cuentaString + "</Message>" +
                                        "</Execute>" +
                                        "</s:Body>" +
                                   "</s:Envelope>");
            webReq.Method = "POST";
            var requestStream = webReq.GetRequestStream();
            soapRequestXml.Save(requestStream);


            var httpResponse = webReq.GetResponse();
            var responseStream = new StreamReader(httpResponse.GetResponseStream());
            var soapResponseString = responseStream.ReadToEnd();
            var soapResponseXml = new XmlDocument();
            soapResponseXml.LoadXml(soapResponseString);

            var xmlFragment = soapResponseXml.InnerXml;  //credencialResp.Movimientos.InnerXml.ToString();
            xmlFragment = xmlFragment.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "").Replace("<soapenv:Envelope xmlns:soapenv=\"http://www.w3.org/2003/05/soap-envelope\">", "").Replace("<soapenv:Body>", "").Replace("</soapenv:Body>", "").Replace("</soapenv:Envelope>", "").Replace("xmlns=\"http://ws.apache.org/ns/synapse\"", "");
            var strReader = new StringReader(xmlFragment);

            var xmlDoc = XDocument.Load(strReader);
            var tarjetaArray = from x in xmlDoc.Descendants("Response") select new { ReturnCode = x.Element("ReturnCode").Value, ReturnMessage = x.Element("ReturnMessage").Value };
            var nominar = new NominarCuentaResponse
            {
                CodigoRespuesta = Convert.ToInt32(tarjetaArray.First().ReturnCode),
                UserResponse = tarjetaArray.First().ReturnMessage,
                Success = tarjetaArray.First().ReturnCode == "-1" ? 1 : 0,
                FechaCreacion = Convert.ToString(DateTime.Now),
            };
            //GuardarBitacora(request, nominar);
            return nominar;
        }

        #endregion
    }
}
