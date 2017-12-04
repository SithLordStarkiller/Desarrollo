using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class BaseServices<TEntity> : IService
    where TEntity : class, new()
    {
        public string ControllerName { get; set; }

        private int pages { get; set; }

        internal int Pages { get { return pages; } }

        internal HttpClient client;
        string apiCatalogClient;
        internal BaseServices(string controllerName)
        {
            ControllerName = controllerName + "/";
            apiCatalogClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiCatalog].Replace("catalog/", string.Empty) + ControllerName;
            client = new HttpClient();
        }
        

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
        public List<TEntity> Obtener(int page, int rows)
        {
            List<TEntity> list = new List<TEntity>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerTodos"}",
                new
                {
                    Paging = new
                    {
                        All = false,
                        CurrentPage = page,
                        Rows = rows
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    JArray arrayItems = jResult["List"] as JArray;
                    list = arrayItems.ToObject<List<TEntity>>();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<TEntity> ObtenerPorCriterio(int page, int rows, TEntity model)
        {
            List<TEntity> list = new List<TEntity>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerPorCriterio"}",
                new
                {
                    model,
                    Paging = new
                    {
                        All = false,
                        CurrentPage = page,
                        Rows = rows
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    JArray arrayItems = jResult["List"] as JArray;
                    list = arrayItems.ToObject<List<TEntity>>();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool Save(TEntity model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"Save"}", model);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Sucess"]);
                if (!success)
                {
                    string mensaje = jResult["Errors"].Children().Select(t => t["Message"].ToString()).First();
                    throw new ConecWebException(mensaje);
                }

            }
            return success;
        }


        public bool CambiarEstatus(TEntity model)
        {
            bool success = false;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"CambiarEstatus"}", model);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }
    }
}