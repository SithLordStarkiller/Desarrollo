using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PubliPayments.Entidades;
using PubliPayments.Negocios.WebServicePe;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class ParseXmlAsignacion
    {
        public NuevaOrden[] Sira(StringBuilder sbAsignacion)
        {
            try
            {
                XmlDocument xmlAsignacion = new XmlDocument();
                xmlAsignacion.LoadXml(sbAsignacion.ToString());

                XmlElement root = xmlAsignacion.DocumentElement;
                XmlNodeList Ordenes = root.ChildNodes;

                NuevaOrden[] asignarOrdenes = null;

                if (Ordenes.Count > 0)
                {
                    asignarOrdenes = new NuevaOrden[Ordenes.Count];
                    for (int i = 0; i < Ordenes.Count; i++)
                    {
                        XmlNode newOrden = Ordenes[i];
                        var collection = Ordenes[i].Attributes;
                        if (collection != null)
                        {
                            var idOrden = collection["id"].Value;
                            if (newOrden.HasChildNodes)
                            {
                                XmlNode parametros = newOrden.FirstChild;
                                XmlNodeList campos = parametros.ChildNodes;
                                var cuerpo = new string[campos.Count];

                                for (int j = 0; j < campos.Count; j++)
                                {
                                    var xmlAttributeCollection = campos[j].Attributes;
                                    if (xmlAttributeCollection != null)
                                        cuerpo[j] = xmlAttributeCollection["llave"].Value + "|" +
                                                    xmlAttributeCollection["valor"].Value;
                                }
                                asignarOrdenes[i] = new NuevaOrden() { IdOrden = idOrden, Parametros = cuerpo };
                            }
                        }
                    }
                }
                return asignarOrdenes;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ParseXmlAsignacion", "Sira:" + ex.Message);
                return null;
            }
          
        }
    }
}
