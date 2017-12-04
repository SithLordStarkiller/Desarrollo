using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOB.SPF.ConecII.Web.Servicios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Web.Models;

namespace GOB.SPF.ConecII.Web.Servicios.Tests
{
    using  Web.Resources;


    [TestClass()]
    public class ServiceConfigurationTests
    {
        [TestMethod()]
        public void NotificacionGuardarTest()
        {
            //var apiConfigurationClient = "api/Configuracion";

            //HttpClient client;
            //client = new HttpClient();
            
            //var response = client.PostAsJsonAsync(
            //    $"{apiConfigurationClient}{MethodApiConfiguration}", new
            //    {
            //        Item = new UiNotificacion
            //        {
            //            IdNotificacion = model.IdNotificacion
            //        }
            //    });

            //if (!response.Result.IsSuccessStatusCode)
            //    return false;

            //var query = response.Result.Content.ReadAsStringAsync().Result;

            //var jResult = JObject.Parse(query);
            //var success = Convert.ToBoolean(jResult["Success"]);
        }
    }
}