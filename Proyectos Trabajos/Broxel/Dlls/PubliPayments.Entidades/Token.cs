using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using PubliPayments.Utiles;

namespace PubliPayments.Entidades
{
    public class Token
    {
        private readonly String _tokenString;

        public Token(string identif, int idUsuarioLog)
        {
            try
            {
                string uri = System.Configuration.ConfigurationManager.AppSettings["uriAutenticado"];
                DateTime fechaAc = DateTime.Now;
                String fecActual = fechaAc.Day.ToString(CultureInfo.InvariantCulture)
                                   + fechaAc.Second.ToString(CultureInfo.InvariantCulture)
                                   + fechaAc.Minute.ToString(CultureInfo.InvariantCulture)
                                   + fechaAc.Hour.ToString(CultureInfo.InvariantCulture);

                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/json";
                //webRequest.ContentLength = dataStream.Length;
                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        valor = Convert.ToBase64String(Encoding.UTF8.GetBytes(fecActual + "," + identif))
                    });

                    streamWriter.Write(json);
                }
                WebResponse webResponse = webRequest.GetResponse();

                using (var sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    _tokenString = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioLog, "Tocken",
                    "Error: al ejecutar constructor: " + ex.Message +
                    (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
            }
        }

        public string ObtenerToken()
        {
            return _tokenString;
        }
    }
}
