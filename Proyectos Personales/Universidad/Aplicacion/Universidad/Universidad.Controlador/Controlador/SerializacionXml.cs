using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Universidad.Controlador.Controlador
{
    public class SerializacionXml
    {
        public static string SerializeToXml(object obj)
        {
            var xmlDoc = new XmlDocument();
            var xmlSerializer = new XmlSerializer(obj.GetType());

            using (var xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, obj);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static Object DeserializeTo(string xmlSerializado, Object obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType()); 
            obj = xmlSerializer.Deserialize(new StringReader(xmlSerializado));
            return obj;
        }
    }
}
