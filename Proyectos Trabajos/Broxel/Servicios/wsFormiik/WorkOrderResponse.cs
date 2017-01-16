using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using PubliPayments.Entidades;
using PubliPayments.Utiles;
using PubliPayments.Negocios;

namespace PubliPayments
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

        readonly string _directorioLogeo = ConfigurationManager.AppSettings["DirectorioLog"];
  
        public void Load(Stream strXml)
        {
            var reader = new StreamReader(strXml);
            string text = reader.ReadToEnd();

            var xmlworkorderresponse = new XmlDocument(); //xmldoc Respuesta Completa 
            var xmlresponse = new XmlDocument();          //xmldoc Respuesta Detalle

            //Carga la cadena string a un XmlDoc
            xmlworkorderresponse.LoadXml(text);

            XmlElement root = xmlworkorderresponse.DocumentElement;
            if (root != null)
            {
                XmlNode nodeXmlResponse = root.FirstChild;
                XmlNode newNode = xmlworkorderresponse.ImportNode(nodeXmlResponse, true);
                xmlresponse.LoadXml(newNode.OuterXml);


                ExternalId = root.Attributes["ExternalId"].Value;
                if (!Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                {
                    ExternalId = root.Attributes["ExternalId"].Value;
                    ProductId = root.Attributes["ProductId"].Value;
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
                }
            }
            XmlResponse = xmlresponse.InnerXml;
            XmlFullResponse = xmlworkorderresponse.InnerXml;
        }

        public Stream Save()
        {

            string fileLogName = string.Format(_directorioLogeo + "ordenes-{0:yyyy-MM-dd-HHmmssfff}" + new Random().Next(1, 999) + ".csv", DateTime.Now);
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

                log.WriteLine(ToString());
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
            string mensaje;
            string fileXmlName = "";
            var bc = new BloqueoConcurrencia();
            var bcmodel = bc.BloquearConcurrencia(
                new BloqueoConcurrenciaModel
                {
                    Aplicacion = "wsFormiik",
                    Llave = ExternalId + (FinalDate!=null?  ("_" + FinalDate.Trim().Replace(" ","").Replace(":","")) :""),
                    Origen = "WorkOrderResponse.SaveFull"
                }, 60000, 1);
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "wsFormiik", "Recibiendo orden: " + ExternalId);

                if (bcmodel.Error != 0)
                {
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error wsFormiik :aplicación " + Config.AplicacionActual().Nombre, "SaveFull-Llave: " + bcmodel.Llave + " - " + bcmodel.ErrorMensaje);
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "SaveFull-Llave: " + bcmodel.Llave + " - " + bcmodel.ErrorMensaje);
                    return new MemoryStream(Encoding.UTF8.GetBytes(Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA") ? "OK" : string.Empty));
                }

                if (bcmodel.Id > 0)
                {
                    //Concurrencia OK
                    if (!string.IsNullOrEmpty(bcmodel.Resultado))
                    {
                        return
                            new MemoryStream(
                                Encoding.UTF8.GetBytes(Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA")
                                    ? "OK"
                                    : string.Empty));
                    }
                }
                else
                {
                    //Maneja el error en caso de que no sea valida la concurrencia
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error wsFormiik :aplicación " + Config.AplicacionActual().Nombre, "SaveFull-llave:" + bcmodel.Llave + " la concurrencia fue invalida");
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "SaveFull-llave:" + bcmodel.Llave + " la concurrencia fue invalida");
                    return new MemoryStream(Encoding.UTF8.GetBytes("Error -6: No se puede guardar en base de datos"));
                }
           

                //Creo un nuevo xmlDoument para leer la respuesta
                var xmlDoc = new XmlDocument();
                
                try
                {
                    xmlDoc.LoadXml(XmlFullResponse);

                    fileXmlName =
                        string.Format(_directorioLogeo + "O{0}_{1}.xml",
                            ExternalId, bcmodel.Id);
                    xmlDoc.Save(fileXmlName);

                }
                catch (Exception ex)
                {
                    bcmodel.Resultado = ex.Message;
                    bc.ActualizarConcurrencia(bcmodel, 1);
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                        "Error wsFormiik :aplicacion " + Config.AplicacionActual().Nombre,
                        "SaveFull-llave: " + bcmodel.Llave + " - No puede guardar el archivo " + fileXmlName + " - Error:" +
                        ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik",
                        "SaveFull-llave: " + bcmodel.Llave + " - No puede guardar el archivo - Error:" + ex.Message);
                    return new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
                }

                //Busco la raíz
                XmlElement root = xmlDoc.DocumentElement;
                //Obtengo el primer hijo de la Raiz que es el tag del formulario
                if (root != null)
                {
                    XmlNode nodePrimerHijo = root.FirstChild;

                    Dictionary<string, string> respuestas =
                        root.Attributes.Cast<XmlAttribute>().ToDictionary(itematt => itematt.Name, itematt => itematt.Value);

                    XmlNodeList subFormularios = nodePrimerHijo.ChildNodes;
                    foreach (XmlNode node in subFormularios)
                    {
                        if (node.Name == "DTrabajador" && Config.AplicacionActual().Nombre.ToUpper().Contains("ORIGINACIONMOVIL"))
                        {
                            continue;
                        }

                        if(Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                        {
                            if (node.Name != "#text")
                            {
                                string nombrecampo = node.Name;

                                try
                                {
                                    if (node.HasChildNodes && node.ChildNodes[0].Name != "#text")
                                    {
                                        foreach (XmlNode nodoHijo in node.ChildNodes)
                                        {
                                            if (nodoHijo.Name != "#text")
                                                respuestas.Add(nodoHijo.Name, nodoHijo.InnerText);   
                                        }
                                    }
                                    else
                                    {
                                        respuestas.Add(nombrecampo, node.InnerText);   
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Trace.WriteLine("M-SaveFull- " + ex.Message + " - id = " + ExternalId);
                                }
                            }
                        }
                        else
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
                                            Trace.WriteLine("M-SaveFull- " + ex.Message + " - id = " + ExternalId);
                                        }
                                    }
                                    else
                                    {
                                        Trace.WriteLine("M-SaveFull-" + nodocampo.Name + " - id = " + ExternalId);
                                    }
                                }
                            }
                        }
                    
                    }
                 

                    if (respuestas.ContainsKey("ExternalId"))
                    {
//                        int idOrden = Convert.ToInt32(respuestas["ExternalId"]);
                        var resp = new Respuesta();
                        int idOrden = resp.PrepararOrden(respuestas["ExternalId"], respuestas["ExternalType"], respuestas["AssignedTo"]);

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "wsFormiik", "Procesando orden: " + ExternalId);

                        Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, "idOrden = " + idOrden));

                        mensaje = resp.GuardarRespuesta(idOrden, respuestas, "wsFormiik", AssignedTo, 1, Config.AplicacionActual().Productivo, Config.AplicacionActual().Nombre);
                        bcmodel.Resultado = mensaje == ""
                            ? "OK"
                            : mensaje;
                        bc.ActualizarConcurrencia(bcmodel, 1);

                        if (!string.IsNullOrEmpty(mensaje))
                        {
                            Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", mensaje + " - id = " + ExternalId);
                            return new MemoryStream(Encoding.UTF8.GetBytes(mensaje));
                        }
                    }
                    else
                    {
                        mensaje = "No se encontró el atributo ExternalId";
                        throw new Exception(mensaje);
                        //Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                        //Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", mensaje);
                        //return new MemoryStream(Encoding.UTF8.GetBytes(String.Empty));
                    }
                }
                else
                {
                    throw new Exception("Root NULL - id = " + ExternalId);
                    //Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "Root NULL - id = " + ExternalId);
                    //return new MemoryStream(Encoding.UTF8.GetBytes("Error en el XML - Root NULL"));
                }
                Trace.WriteLine(string.Format("{0} - {1}" + " - id = " + ExternalId, DateTime.Now, "OK" ));
                return new MemoryStream(Encoding.UTF8.GetBytes(Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA") ? bcmodel.Resultado ?? "OK" : string.Empty));
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                bcmodel.Resultado = mensaje;
                bc.ActualizarConcurrencia(bcmodel, 1);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                    "Error wsFormiik.SaveFull :aplicación " + Config.AplicacionActual().Nombre,
                    "Id = " + ExternalId + Environment.NewLine + "Archivo: " + fileXmlName + Environment.NewLine +
                    "Error: " + mensaje + Environment.NewLine + "Trace = " + ex.StackTrace);
                Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "Id = " + ExternalId + " - " +  mensaje );
                return new MemoryStream(Encoding.UTF8.GetBytes(string.Empty));
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13:yyyy-MM-dd HH:mm:ss}"
                 , ProductId, ExternalId, ExternalType, AssignedTo, InitialDate, FinalDate, ResponseDate
                 , InitialLatitude, FinalLatitude, InitialLongitude, FinalLongitude, FormiikResponseSource, XmlResponse, DateTime.Now);
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