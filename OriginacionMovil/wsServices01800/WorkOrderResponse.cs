using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using PubliPayments.Entidades;
using PubliPayments.Negocios.Originacion;
using PubliPayments.Utiles;
using PubliPayments.Negocios;

namespace wsServices01800
{
    [DataContract(Namespace = "")]
    public class WorkOrderResponse
    {
        [DataMember]
        public string ProductId { get; set; }

        [DataMember]
        public string ExternalId { get; set; }

        [DataMember]
        public string ExternalType { get; set; }

        [DataMember]
        public string AssignedTo { get; set; }

        [DataMember]
        public string InitialDate { get; set; }

        [DataMember]
        public string FinalDate { get; set; }

        [DataMember]
        public string ResponseDate { get; set; }

        [DataMember]
        public string InitialLatitude { get; set; }

        [DataMember]
        public string FinalLatitude { get; set; }

        [DataMember]
        public string InitialLongitude { get; set; }

        [DataMember]
        public string FinalLongitude { get; set; }

        [DataMember]
        public string FormiikResponseSource { get; set; }

        [DataMember]
        public string XmlResponse { get; set; }

        [DataMember]
        public string XmlFullResponse { get; set; }

        private Random _rnd = new Random();
        private readonly string _directorioLogeo = ConfigurationManager.AppSettings["DirectorioLog"];

        public void Load(Stream strXml)
        {
            try
            {
                StreamReader reader = new StreamReader(strXml);
                string text = reader.ReadToEnd();

                XmlDocument xmlworkorderresponse = new XmlDocument(); //xmldoc Respuesta Completa 
                XmlDocument xmlresponse = new XmlDocument(); //xmldoc Respuesta Detalle

                //Carga la cadena string a un XmlDoc
                xmlworkorderresponse.LoadXml(text);

                XmlElement root = xmlworkorderresponse.DocumentElement;
                XmlNode nodeXmlResponse = root.FirstChild;

                XmlNode newNode = xmlworkorderresponse.ImportNode(nodeXmlResponse, true);

                xmlresponse.LoadXml(newNode.OuterXml);


                ProductId = root.Attributes["ProductId"].Value;
                ExternalId = root.Attributes["ExternalId"].Value;
                ExternalType = root.Attributes["ExternalType"].Value;
                AssignedTo = root.Attributes["AssignedTo"].Value;
                InitialDate = root.Attributes["InitialDate"].Value;
                FinalDate = root.Attributes["FinalDate"].Value;
                ResponseDate = root.Attributes["ResponseDate"].Value;
                InitialLatitude = root.Attributes["InitialLatitude"].Value;
                FinalLatitude = root.Attributes["FinalLatitude"].Value;
                InitialLongitude = root.Attributes["InitialLongitude"].Value;
                FinalLongitude = root.Attributes["FinalLongitude"].Value;
                FormiikResponseSource = root.Attributes["FormiikResponseSource"].Value;
                XmlResponse = xmlresponse.InnerXml;
                XmlFullResponse = xmlworkorderresponse.InnerXml;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", "Load:" + ex.Message);
            }
        }

        public Stream Save()
        {

            string fileLogName =
                string.Format(_directorioLogeo + "ordenes-{0:yyyy-MM-dd-HHmmssfff}" + _rnd.Next(1, 999) + ".csv",
                    DateTime.Now);
            StreamWriter log;
            try
            {
                if (!File.Exists(fileLogName))
                {
                    log = new StreamWriter(fileLogName);
                    //log.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}"
                    //, "ProductId", "ExternalId", "ExternalType", "AssignedTo", "InitialDate", "FinalDate", "ResponseDate"
                    //, "InitialLatitude", "FinalLatitude", "InitialLongitude", "FinalLongitude", "FormiikResponseSource"
                    //, "XmlResponse"));
                }
                else
                {
                    log = File.AppendText(fileLogName);
                }

                log.WriteLine(this.ToString());
                log.Close();
                return new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));

            }
            catch (Exception ex)
            {
                return new MemoryStream(Encoding.UTF8.GetBytes(ex.Message));
            }
        }

        public Stream SaveFull()
        {
            string mensaje = "";
        
            string fileXmlName = "";
            var bc = new BloqueoConcurrencia();
            var bcmodel = bc.BloquearConcurrencia(
                new BloqueoConcurrenciaModel
                {
                    Aplicacion = "wsServices01800",
                    Llave = ExternalId + "_" + FinalDate.Trim().Replace(" ", "").Replace(":", ""),
                    Origen = "WorkOrderResponse.SaveFull"
                }, 60000, 1);
            try
            {
                //Creo un nuevo xmlDoument para leer la respuesta
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "wsServices01800", "Recibiendo orden: " + ExternalId);

                if (bcmodel.Error != 0)
                {
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error wsServices01800 :aplicacion " + Config.AplicacionActual().Nombre, "SaveFull-llave:" + bcmodel.Llave + " excedio el tiempo de espera");
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", "SaveFull-llave:" + bcmodel.Llave + " excedio el tiempo de espera");
                    // todo revisar si es correto tenga SIRA
                    return new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
                }

                if (bcmodel.Id > 0)
                {
                    //Concurrencia OK
                    if (!string.IsNullOrEmpty(bcmodel.Resultado))
                    {
                        return
                            new MemoryStream(
                                // todo se quito SIRA
                                Encoding.UTF8.GetBytes(string.Empty));
                    }
                }
                else
                {
                    //Maneja el error en caso de que no sea valida la concurrencia
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error wsServices01800 :aplicacion " + Config.AplicacionActual().Nombre, "SaveFull-llave:" + bcmodel.Llave + " la concurrencia fue invalida");
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", "SaveFull-llave:" + bcmodel.Llave + " la concurrencia fue invalida");
                    return new MemoryStream(Encoding.UTF8.GetBytes("Error -6: No se puede guardar en base de datos"));
                }
                // hasta aqui la parte de bloqueo

                var xmlDoc = new XmlDocument();

                try
                {
                xmlDoc.LoadXml(this.XmlFullResponse);

                string fileLogName =
                        string.Format(_directorioLogeo + "O{0}_{1}.xml",
                            ExternalId, bcmodel.Id);
                xmlDoc.Save(fileLogName);

                }
                catch (Exception ex)
                {
                    bcmodel.Resultado = ex.Message;
                    bc.ActualizarConcurrencia(bcmodel, 1);
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                        "Error wsServices01800 :aplicacion " + Config.AplicacionActual().Nombre,
                        "SaveFull-llave: " + bcmodel.Llave + " - No puede guardar el archivo " + fileXmlName + " - Error:" +
                        ex.Message);                                                 
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800",
                        "SaveFull-llave: " + bcmodel.Llave + " - No puede guardar el archivo - Error:" + ex.Message);
                    return new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
              

                }
                //Busco la raíz
                XmlElement root = xmlDoc.DocumentElement;
                //Obtengo el primer hijo de la Raiz que es el tag del formulario
                XmlNode nodePrimerHijo = root.FirstChild;

                ////Voy a crear un nuevo xmlDocument para guardar la respuesta
                //XmlDocument xmlDocNew = new XmlDocument();
                ////La Raiz es el nombre del formulario
                //XmlElement rootnew = xmlDocNew.CreateElement("Respuesta");
                //xmlDocNew.AppendChild(rootnew);
                ////Creo un nuevo tag llamado Respuesta

                Dictionary<string, string> respuestas =
                    root.Attributes.Cast<XmlAttribute>().ToDictionary(itematt => itematt.Name, itematt => itematt.Value);

                Dictionary<string, string> respuestas2=new Dictionary<string, string>();

                XmlNodeList subFormularios = nodePrimerHijo.ChildNodes;
                foreach (XmlNode node in subFormularios)
                {
                    if (node.Name == "DTrabajador" && Config.AplicacionActual().Nombre.Contains("OriginacionMovil"))
                    {
                        if (node.HasChildNodes)
                        {
                            XmlNodeList campos = node.ChildNodes;
                            foreach (XmlNode nodocampo in campos)
                            {
                                if (nodocampo.Name != "#text")
                                {
                                    string nombrecampo = nodocampo.Name;

                                    try
                                    {
                                        if (nodocampo.HasChildNodes && nodocampo.ChildNodes[0].Name != "#text")
                                        {
                                            foreach (XmlNode nodoHijo in nodocampo.ChildNodes)
                                            {
                                                if (nodoHijo.Name != "#text")
                                                    respuestas2.Add(nodoHijo.Name, nodoHijo.InnerText);
                                            }
                                        }
                                        else
                                        {
                                            respuestas2.Add(nombrecampo, nodocampo.InnerText);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Trace.WriteLine("M-SaveFull- " + ex.Message);
                                    }
                                }
                                else
                                {
                                    Trace.WriteLine("M-SaveFull-" + nodocampo.Name);
                                }
                            }
                        }
                        continue;
                    }
                    if (node.HasChildNodes)
                    {
                        XmlNodeList campos = node.ChildNodes;
                        foreach (XmlNode nodocampo in campos)
                        {
                            if (nodocampo.Name != "#text")
                            {
                                string nombrecampo = nodocampo.Name;

                                try
                                {
                                    if (nodocampo.HasChildNodes && nodocampo.ChildNodes[0].Name != "#text")
                                    {
                                        foreach (XmlNode nodoHijo in nodocampo.ChildNodes)
                                        {
                                            if (nodoHijo.Name != "#text")
                                                respuestas.Add(nodoHijo.Name, nodoHijo.InnerText);
                                        }
                                    }
                                    else
                                    {
                                        respuestas.Add(nombrecampo, nodocampo.InnerText);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Trace.WriteLine("M-SaveFull- " + ex.Message);
                                }
                            }
                            else
                            {
                                Trace.WriteLine("M-SaveFull-" + nodocampo.Name);
                            }
                        }
                    }
                }

                if (respuestas.ContainsKey("ExternalId"))
                {
                    var orig = new Originacion(respuestas,respuestas2,ExternalType,bcmodel,bc,ExternalId,fileXmlName,AssignedTo,Config.AplicacionActual().Productivo);
                    return orig.CargarOriginacion();

                }

                mensaje = "No se encontró el atributo ExternalId";
                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", mensaje);
                return new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;

                bcmodel.Resultado = mensaje;
                bc.ActualizarConcurrencia(bcmodel, 1);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                    "Error wsFormiik.SaveFull :aplicacion " + Config.AplicacionActual().Nombre,
                    "Id = " + ExternalId + Environment.NewLine + "Archivo: " + fileXmlName + Environment.NewLine +
                    "Error: " + mensaje + Environment.NewLine + "Trace = " + ex.StackTrace);
                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", "Id = " + ExternalId + " - " + mensaje);
                return new MemoryStream(Encoding.UTF8.GetBytes(mensaje));
            }
        }

        public override string ToString()
        {
            return
                string.Format(
                    "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13:yyyy-MM-dd HH:mm:ss}"
                    , ProductId, ExternalId, ExternalType, AssignedTo, InitialDate, FinalDate, ResponseDate
                    , InitialLatitude, FinalLatitude, InitialLongitude, FinalLongitude, FormiikResponseSource,
                    XmlResponse, DateTime.Now);
        }

        public DataSet ConvertXmlToDataSet(string xmlData)
        {
            XmlTextReader reader = null;
            try
            {
                var xmlDs = new DataSet();
                var stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDs.ReadXml(reader);
                return xmlDs;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
    }
}