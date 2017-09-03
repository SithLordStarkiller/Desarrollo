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
    public class ServiceMCS:IService
    {
        private HttpClient client;
        private string apiMcsClient;

        public ServiceMCS()
        {
            apiMcsClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiMCS];
            client = new HttpClient();
        }

        #region ESTADOS
        public List<Estado> ObtenerEstados()
        {
            List<Estado> list = new List<Estado>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiMcsClient}{MethodApiCatalog.EntidadFederativaObtener}");
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;
                
                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Estado
                    {
                        Identificador = Convert.ToInt32(t["IdEstado"]),
                        IdPais = Convert.ToInt32(t["IdPais"]),
                        Nombre = t["Estado"].ToString(),
                        IsActive = Convert.ToBoolean(t["Vigente"])
                    }).ToList();
                }
            }
            return list;

        }
        #endregion

        #region TARIFARIO
        public List<GrupoTarifario> ObtenerTarifario(Paging paging)
        {
            List<GrupoTarifario> list = new List<GrupoTarifario>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiMcsClient}{MethodApiCatalog.GrupoTarifarioObtener}");
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new GrupoTarifario
                    {
                        Identificador = Convert.ToInt32(t["IdGrupoTarifario"]),
                        Nombre = t["GrupoTarifario"].ToString(),
                        Nivel = Convert.ToInt32(t["Nivel"]),
                        IsActive = Convert.ToBoolean(t["Vigente"])
                    }).ToList();
                }
            }
            return list;

        }
        #endregion


        public List<Municipio> ObtenerMunicipios(Estado estado)
        {
            List<Municipio> list = new List<Municipio>(); ;
            Task<HttpResponseMessage> response = client.GetAsync("{apiMcsClient}{MethodApiCatalog.MunicipiosObtener}/{estado.Identificador}");
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Municipio
                    {
                        Identificador = Convert.ToInt32(t["IdMunicipio"]),
                        Nombre = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Vigente"]),
                        Estado = new Estado { Identificador = Convert.ToInt32(t["IdEstado"]) }
                    }).ToList();
                }
            }
            return list;

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
