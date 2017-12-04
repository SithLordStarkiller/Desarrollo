using GOB.SPF.ConecII.Entities.DTO;
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

        internal int pages { get; set; }

        internal int Pages { get { return pages; } }

        internal HttpClient client;
        public string apiCatalogClient;
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
            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();
            requestBase.Paging = new Paging() { All = 1, CurrentPage = page, Rows = rows };

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerTodos"}", requestBase);
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

        public List<TEntity> ObtenerPorCriterio(int page, int rows, int all, TEntity model)
        {
            List<TEntity> list = new List<TEntity>(); ;
            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();
            requestBase.Paging = new Paging() { All = all, CurrentPage = page, Rows = rows };
            requestBase.Item = model;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerPorCriterio"}", requestBase);
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

        public List<TEntity> ObtenerPorId(TEntity model)
        {
            List<TEntity> list = new List<TEntity>();            
            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();
            requestBase.Item = model;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerPorId"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    JArray arrayItems = jResult["List"] as JArray;
                    list = arrayItems.ToObject<List<TEntity>>();
                }

            }
            return list;
        }

        public bool Save(TEntity model)
        {
            bool success = false;

            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();
            requestBase.Item = model;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"Save"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
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
            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();
            requestBase.Item = model;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"CambiarEstatus"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
                if (!success)
                {
                    string mensaje = jResult["Errors"].Children().Select(t => t["Message"].ToString()).First();
                    throw new ConecWebException(mensaje);
                }
            }
            return success;
        }


        public List<DropDto> ConsultaList()
        {
            List<DropDto> list = new List<DropDto>(); ;
            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();


            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"DropDownList"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    JArray arrayItems = jResult["List"] as JArray;
                    list = arrayItems.ToObject<List<DropDto>>();
                }
            }
            return list;
        }

        public List<DropDto> ConsultaListCriterio(TEntity model)
        {
            List<DropDto> list = new List<DropDto>(); ;
            RequestBase<TEntity> requestBase = new RequestBase<TEntity>();
            
            requestBase.Item = model;


            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"DropDownListCriterio"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    JArray arrayItems = jResult["List"] as JArray;
                    list = arrayItems.ToObject<List<DropDto>>();
                }
            }
            return list;
        }

    }
}