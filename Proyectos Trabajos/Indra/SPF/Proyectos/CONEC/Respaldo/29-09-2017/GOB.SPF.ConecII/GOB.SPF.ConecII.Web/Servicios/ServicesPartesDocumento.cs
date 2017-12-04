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
    public class ServicesPartesDocumento: BaseServices<UiPartesDocumento>
    {
        public ServicesPartesDocumento(string controllerName) : base(controllerName)
        {
        }

        public UiPartesDocumento ObtenerPorIdTipoDocumento(UiPartesDocumento model)
        {
            
            RequestBase<UiPartesDocumento> requestBase = new RequestBase<UiPartesDocumento>();
            requestBase.Item = model;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{"ObtenerPorIdTipoDocumento"}", requestBase);
            if (response.Result.IsSuccessStatusCode)
            {

                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    model = jResult["Entity"].ToObject<UiPartesDocumento>();
                }
            }
            return model;
        }
    }
}