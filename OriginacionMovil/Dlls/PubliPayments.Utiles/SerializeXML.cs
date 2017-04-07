using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PubliPayments.Utiles
{
   public static class SerializeXML
    {
        /// <summary>
        /// Regresa el XML de cualquier objeto que se le pase
        /// </summary>
        /// <param name="objeto">Cualquier objeto serializable</param>
        /// <returns>XML del objeto pasado</returns>
        public static string SerializeObject(object objeto)
        {
            string xml;
            try
            {
                var xsSubmit = new XmlSerializer(objeto.GetType());
                using (var sww = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, objeto);
                    xml = sww.ToString(); // Your XML
                }
            }
            catch (Exception ex)
            {
                xml = "SerializeObject - " + ex.Message;
            }

            return xml;
        }
    }
}
