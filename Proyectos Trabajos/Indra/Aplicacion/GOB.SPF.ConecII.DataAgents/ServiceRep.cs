using GOB.SPF.ConecII.DataAgents.Resources;
using GOB.SPF.ConecII.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.DataAgents
{
    public class ServiceREP : IService
    {
        private HttpClient client;
        private string apiRepClient;

        public ServiceREP()
        {
            apiRepClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiREP];
            client = new HttpClient();
        }

        #region JERARQUIAS
        public List<Jerarquia> ObtenerJerarquias(Paging paging)
        {
            List<Jerarquia> list = new List<Jerarquia>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiRepClient}{MethodApiCatalog.JerarquiasObtener}");

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;
                JObject jResult = JObject.Parse(query);

                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Jerarquia
                    {
                        Identificador = Convert.ToInt32(t["IdJerarquia"]),
                        Nombre = t["Jerarquia"].ToString(),
                        Nivel = Convert.ToInt32(t["Nivel"]),
                        IsActive = Convert.ToBoolean(t["Vigente"])
                    }).ToList();
                }
            }
            return list;
        }
        #endregion

        #region AREAS
        public List<Area> ObtenerAreas(Paging paging)
        {
            List<Area> list = new List<Area>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiRepClient}{MethodApiCatalog.AreasObtener}");

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;
                JObject jResult = JObject.Parse(query);

                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Area
                    {
                        Identificador = Isnumeric(t["IdCentroCosto"]),
                        Nombre = t["CcDescripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["CcVigente"])
                    }).ToList();
                }
            }
            return list;
        }

        private int Isnumeric(object data)
        {
            int response;
            try
            {
                response = Convert.ToInt32(data);
            }
            catch
            {
                response = 0;
            }
            return response;
        }

        #endregion

        #region INTEGRANTES
        public List<Integrante> ObtenerIntegrantes(Paging paging)
        {
            List<Integrante> list = new List<Integrante>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiRepClient}{MethodApiCatalog.IntegrantesObtener}");

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;
                JObject jResult = JObject.Parse(query);

                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Integrante
                    {
                        Identificador = Convert.ToInt32(t["Idintegrante"]),
                        Nombre = t["Nombre"].ToString(),
                        ApPaterno = t["ApPaterno"].ToString(),
                        ApMaterno = t["ApMaterno"].ToString(),
                        Correo = t["Correo"].ToString(),
                        CorreoPersonal = t["CorreoPersonal"].ToString(),
                        IdArea = t["IdArea"].ToString(),
                        Area = t["Area"].ToString(),
                        IdJerarquia = Convert.ToInt32(t["IdJerarquia"]),
                        Jerarquia = t["Jerarquia"].ToString(),
                        //Descripcion = t["Descripcion"].ToString(),
                        IsActive = true
                    }).ToList();
                }
            }
            return list;
        }
        #endregion
        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
    }
}
