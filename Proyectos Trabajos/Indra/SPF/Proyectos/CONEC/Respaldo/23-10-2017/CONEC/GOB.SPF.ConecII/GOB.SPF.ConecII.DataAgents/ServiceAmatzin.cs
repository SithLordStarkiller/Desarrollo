namespace GOB.SPF.ConecII.DataAgents
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using Resources;
    using Entities;
    using Entities.Amatzin;
    using Newtonsoft.Json.Linq;

    public class ServiceAmatzin : IService
    {
        private HttpClient client;
        private string apiAmatzin;

        public ServiceAmatzin()
        {
            apiAmatzin = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiAmatzin];
            client = new HttpClient();
        }

        public int RegistrarArchivo(Documento archivo)
        {
            int resultado = 0;

            var response = client.PostAsJsonAsync($"{apiAmatzin}{MethodApiCatalog.RegistrarArchivo}", new
            {
                ArchivoId = archivo.ArchivoId,
                Nombre = archivo.Nombre,
                Directorio = archivo.Directorio,
                Referencia = archivo.Referencia,
                Base64 = archivo.Base64
            });

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jObject = JObject.Parse(query);
                if (Convert.ToBoolean(jObject["Success"]))
                {
                    resultado = Convert.ToInt32(jObject["Entity"]["ArchivoId"]);
                }
            }
            return resultado;
        }
        public Documento ConsultarArchivo(long archivoId)
        {

            Documento resultado = new Documento();

            var response = client.GetAsync($"{apiAmatzin}{MethodApiCatalog.ConsultarArchivo}{archivoId}");
            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jObject = JObject.Parse(query);
                if (Convert.ToBoolean(jObject["Success"]))
                {
                    resultado = new Documento
                    {
                        ArchivoId = Convert.ToInt32(jObject["List"][0]["ArchivoId"]),
                        ArchivoIdParent = Convert.ToInt32(jObject["List"][0]["ArchivoIdParent"]),
                        Base64 = jObject["List"][0]["Base64"].ToString(),
                        Directorio = jObject["List"][0]["Directorio"].ToString(),
                        Extension = jObject["List"][0]["Extension"].ToString(),
                        FechaRegistro = Convert.ToDateTime(jObject["List"][0]["FechaRegistro"]),
                        NombreSistema = jObject["List"][0]["NombreSistema"].ToString(),
                        Nombre = jObject["List"][0]["Nombre"].ToString(),
                        Referencia = jObject["List"][0]["Referencia"].ToString()
                    };
                }
            }
            return resultado;
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
    }
}
