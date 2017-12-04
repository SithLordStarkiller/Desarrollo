namespace GOB.SPF.ConecII.Web.Servicios
{
    using Models;
    using Resources;
    using Entities;
    using Entities.Request;

    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class ServiceConfiguration
    {
        #region Variables
        private int pages { get; set; }

        internal int Pages { get { return pages; } }

        internal HttpClient client;

        readonly string _apiConfigurationClient;

        internal ServiceConfiguration()
        {
            _apiConfigurationClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiConfiguracion];
            client = new HttpClient();
        }

        #endregion

        #region DIVISIONES
        public List<UiPlantilla> PlantillaObtener(int page, int rows)
        {
            List<UiPlantilla> list = new List<UiPlantilla>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{_apiConfigurationClient}{MethodApiCatalog.PlantillaObtener}",
                new
                {
                    Paging = new { All = false, CurrentPage = page, Rows = rows }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiPlantilla
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["NombreDivision"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiPlantilla> PlantillaObtenerPorCriterio(int page, int rows, UiPlantilla model)
        {
            List<UiPlantilla> list = new List<UiPlantilla>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{_apiConfigurationClient}{MethodApiCatalog.PlantillaObtenerPorCriterio}",
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
                    list = jResult["List"].Children().Select(t => new UiPlantilla
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["NombreDivision"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool PlantillaGuardar(UiPlantilla model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{_apiConfigurationClient}{MethodApiCatalog.PlantillaGuadar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        NombreDivision = model.Nombre
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;

        }


        #endregion Divisiones

        #region Notificaciones y alertas

        #region Notificaciones

        public bool NotificacionGuardar(UiNotificacion model)
        {
            var response = client.PostAsJsonAsync(
            $"{_apiConfigurationClient}{MethodApiConfiguration.NotificacionesGuardar}", new
            {
                Item = new UiNotificacion
                {
                    IdNotificacion = model.IdNotificacion
                }
            });

            if (!response.Result.IsSuccessStatusCode)
                return false;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            var success = Convert.ToBoolean(jResult["Success"]);

            return success;
        }

        public List<UiNotificacion> ObtenerNotificaciones(int page, int rows)
        {
            var list = new List<UiNotificacion>();
            var response = client.PostAsJsonAsync($"{_apiConfigurationClient}{MethodApiConfiguration.NotificacionesTodos}",
                 new
                 {
                     Paging = new
                     {
                         All = false,
                         CurrentPage = 1,// page,
                         Rows = 1000,//rows
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiNotificacion()
                    {
                        IdNotificacion = Convert.ToInt32(t["IdNotificacion"]),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = Convert.ToString(t["TipoServicio"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        Fase = Convert.ToString(t["Fase"]),
                        IdFase = Convert.ToInt32(t["IdFase"]),
                        Actividad = Convert.ToString(t["Actividad"]),
                        CuerpoCorreo = Convert.ToString(t["CuerpoCorreo"]),
                        EsCorreo = Convert.ToBoolean(t["EsCorreo"]),
                        EsSistema = Convert.ToBoolean(t["EsSistema"]),
                        EmitirAlerta = Convert.ToBoolean(t["EmitirAlerta"]),
                        TiempoAlerta = Convert.ToInt32(t["TiempoAlerta"]),
                        Frecuencia = Convert.ToInt32(t["Frecuencia"]),
                        AlertaEsCorreo = Convert.ToBoolean(t["AlertaEsCorreo"]),
                        AlertaEsSistema = Convert.ToBoolean(t["AlertaEsSistema"]),
                        CuerpoAlerta = Convert.ToString(t["CuerpoAlerta"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiNotificacion> ObtenerNotificacionPorId(int page, int rows, UiNotificacion model)
        {
            var list = new List<UiNotificacion>(); ;
            var response = client.PostAsJsonAsync($"{_apiConfigurationClient}{MethodApiConfiguration.NotificacionesObtenerPorId}",
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
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiNotificacion
                    {
                        IdNotificacion = Convert.ToInt32(t["IdNotificacion"]),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = Convert.ToString(t["TipoServicio"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        Actividad = Convert.ToString(t["Actividad"]),
                        CuerpoCorreo = Convert.ToString(t["CuerpoCorreo"]),
                        EsCorreo = Convert.ToBoolean(t["EsCorreo"]),
                        EsSistema = Convert.ToBoolean(t["EsSistema"]),
                        EmitirAlerta = Convert.ToBoolean(t["EmitirAlerta"]),
                        TiempoAlerta = Convert.ToInt32(t["TiempoAlerta"]),
                        Frecuencia = Convert.ToInt32(t["Frecuencia"]),
                        AlertaEsCorreo = Convert.ToBoolean(t["AlertaEsCorreo"]),
                        AlertaEsSistema = Convert.ToBoolean(t["AlertaEsSistema"]),
                        CuerpoAlerta = Convert.ToString(t["CuerpoAlerta"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        #endregion

        #region ReceptoresAlerta

        public bool ReceptoresAlertaGuardarLista(List<ReceptorAlerta> receptores, Notificaciones notificacion)
        {
            var response = client.PostAsJsonAsync(
            $"{_apiConfigurationClient}{MethodApiConfiguration.ReceptoresAlertaGuardarLista}", new RequestReceptoresAlerta
            {
                Receptores = receptores,
                Notificacion = notificacion
            });

            if (!response.Result.IsSuccessStatusCode)
                return false;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            var success = Convert.ToBoolean(jResult["Success"]);

            return success;
        }

        #endregion

        #endregion
    }
}