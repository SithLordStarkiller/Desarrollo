using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Servicios;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesEtiquetasParrafo: BaseServices<UiEtiquetasParrafo>
    {
        public ServicesEtiquetasParrafo(string controllerName) : base(controllerName)
        {
        }

        public List<UiEtiquetasParrafo> ObtenerPorIdParteDocumento(int page, int rows, int all, UiEtiquetasParrafo model)
        {

            List<UiEtiquetasParrafo> list = new List<UiEtiquetasParrafo>(); ;
            RequestBase<UiEtiquetasParrafo> requestBase = new RequestBase<UiEtiquetasParrafo>();
            requestBase.Paging = new Paging() { All = all, CurrentPage = page, Rows = rows };
            requestBase.Item = model;

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerPorParteDocumento"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    JArray arrayItems = jResult["List"] as JArray;
                    list = arrayItems.ToObject<List<UiEtiquetasParrafo>>();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        
    }
}