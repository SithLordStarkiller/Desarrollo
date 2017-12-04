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
    class ServiceCove: IService
    {
        private HttpClient client;
        private string apiCatalogClient;

        public ServiceCove()
        {
            apiCatalogClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiMCS];
            client = new HttpClient();            
        }

        #region GRUPOTARIFARIOS 
        public List<GrupoTarifario> ObtenerGrupoTarifario()
        {
            List<GrupoTarifario> list = new List<GrupoTarifario>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoTarifarioObtener}");
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new GrupoTarifario
                    {
                        Identificador = Convert.ToInt32(t["IdGrupoTarifario"]),
                        Nivel = Convert.ToInt32(t["Nivel"]),
                        Nombre = t["grupoTarifario"].ToString(),
                        IsActive = Convert.ToBoolean(t["Vigente"])
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
