using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public static class FileExtendMethods
    {
        public static string ToBase64(this HttpPostedFileBase file)
        {
            var result = "";

            byte[] theBytes = new byte[file.ContentLength];
            using (BinaryReader theReader = new BinaryReader(file.InputStream))
            {
                theBytes = theReader.ReadBytes(file.ContentLength);
            }

            result = Convert.ToBase64String(theBytes);

            return result;
        }

        public static string ToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}