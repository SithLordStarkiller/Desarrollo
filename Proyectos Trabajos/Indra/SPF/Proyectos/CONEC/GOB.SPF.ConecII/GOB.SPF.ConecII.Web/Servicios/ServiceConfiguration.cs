namespace GOB.SPF.ConecII.Web.Servicios
{
    using Models;
    using Resources;
    using Entities;
    using Entities.Request;

    using Newtonsoft.Json;
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

        readonly string _apiConfiguracionClient;

        internal ServiceConfiguration()
        {
            _apiConfiguracionClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiConfiguracionServicio];
            client = new HttpClient();
        }

        #endregion


        #region CONFIGURACION_SOLICITUD_SERVICIO

        public UiCampoServicio ObtenerServicioPorIdTipoServicio(int idTipoServicio)
        {
            UiCampoServicio entity = new UiCampoServicio();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiConfiguration.ObtenerCamposPorIdTipoServicio}",
                 new
                 {
                     Item = new
                     {
                         Identificador = idTipoServicio
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    var entityTemp = jResult["Entity"];

                    entity.Identificador = Convert.ToInt32(entityTemp["Identificador"]);
                    entity.IdTipoServicio = Convert.ToInt32(entityTemp["IdTipoServicio"]);
                    entity.Cuota = Convert.ToBoolean(entityTemp["Cuota"]);
                    entity.TipoInstalacion = Convert.ToBoolean(entityTemp["TipoInstalacion"]);
                    entity.NumeroPersonas = Convert.ToBoolean(entityTemp["NumeroPersonas"]);
                    entity.HorasCurso = Convert.ToBoolean(entityTemp["HorasCurso"]);
                    entity.FechaInicial = Convert.ToBoolean(entityTemp["FechaInicial"]);
                    entity.FechaFinal = Convert.ToBoolean(entityTemp["FechaFinal"]);
                    entity.BienCustodia = Convert.ToBoolean(entityTemp["BienCustodia"]);
                    entity.Observaciones = Convert.ToBoolean(entityTemp["Observaciones"]);
                    entity.InstalacionesCliente = Convert.ToBoolean(entityTemp["InstalacionesCliente"]);
                    entity.Documentos = Convert.ToBoolean(entityTemp["Documentos"]);
                    entity.Viable = Convert.ToBoolean(entityTemp["Viable"]);
                    entity.TieneComite = Convert.ToBoolean(entityTemp["TieneComite"]);
                    entity.FechaComite = Convert.ToBoolean(entityTemp["FechaComite"]);
                    entity.ObservacionesComite = Convert.ToBoolean(entityTemp["ObservacionesComite"]);
                    entity.InstalacionesClienteMultiple = Convert.ToBoolean(entityTemp["InstalacionesClienteMultiple"]);
                    entity.PeriodoCapacitacion = Convert.ToBoolean(entityTemp["PeriodoCapacitacion"]);
                    entity.EstadoDeFuerza = Convert.ToBoolean(entityTemp["EstadoDeFuerza"]);
                    entity.Signatarios = Convert.ToBoolean(entityTemp["Signatarios"]);
                    entity.CuotaInstalacion = Convert.ToBoolean(entityTemp["CuotaInstalacion"]);
                    entity.EstacionMonitoreoLocal = Convert.ToBoolean(entityTemp["EstacionMonitoreoLocal"]);
                }
            }
            return entity;
        }

        #endregion

        #region DIVISIONES
        public List<UiPlantilla> PlantillaObtener(int page, int rows)
        {
            List<UiPlantilla> list = new List<UiPlantilla>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiCatalog.PlantillaObtener}",
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
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiCatalog.PlantillaObtenerPorCriterio}",
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

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiCatalog.PlantillaGuadar}",
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
            var response = ServiceClient.Client.PostAsJsonAsync(
            $"{_apiConfiguracionClient}{MethodApiConfiguration.NotificacionesGuardar}", new
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
            var response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiConfiguration.NotificacionesTodos}",
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
                    list = jResult["List"].Children().Select(t => new UiNotificacion
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
                        CuerpoAlerta = Convert.ToString(t["CuerpoAlerta"]),
                        Activo = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public UiNotificacion ObtenerNotificacionPorId(int page, int rows, UiNotificacion model)
        {
            var item = new UiNotificacion();
            var response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiConfiguration.NotificacionesObtenerPorId}",
                new
                {
                    Item = model,
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
                    var entity = jResult["Entity"];
                    item = JsonConvert.DeserializeObject<UiNotificacion>(entity.ToString());
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return item;
        }

        #endregion

        #region ReceptoresAlerta

        public bool ReceptoresAlertaGuardarLista(List<ReceptorAlerta> receptores, Notificaciones notificacion)
        {
            var response = ServiceClient.Client.PostAsJsonAsync(
            $"{_apiConfiguracionClient}{MethodApiConfiguration.ReceptoresAlertaGuardarLista}", new RequestReceptoresAlerta
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

        public bool ReceptoresAlertaActualizar(List<ReceptorAlerta> receptores, Notificaciones notificacion)
        {
            var response = ServiceClient.Client.PostAsJsonAsync(
            $"{_apiConfiguracionClient}{MethodApiConfiguration.ReceptoresAlertaActualizar}", new RequestReceptoresAlerta
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

        public List<UiReceptorAlerta> ReceptoresAlertaObtenerTodos(int page, int rows, UiReceptorAlerta model)
        {
            var list = new List<UiReceptorAlerta>();
            var response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiConfiguration.ReceptoresAlertaObtenerTodos}",
                 new
                 {
                     Notificacion = model,
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
                    list = jResult["List"].Children().Select(t => new UiReceptorAlerta
                    {
                        IdNotificacion = Convert.ToInt32(t["IdNotificacion"]),
                        IdRol = (int?)(t["IdRol"]),
                        IdPersona = (t["IdPersona"] == null) ? new Guid() : new Guid(t["IdPersona"].ToString()),
                        IdTipoReceptor = (int?)(t["IdTipoReceptor"]),
                        IdTipoContacto = (int?)(t["IdTipoContacto"]),
                        Correo = Convert.ToString(t["Correo"]),
                        EsCopia = Convert.ToBoolean(t["EsCopia"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        #endregion

        #endregion

        #region AreasValidadoras

        public bool AreasValidadorasGuadar(List<UiAreasValidadoras> model)
        {
            var response = ServiceClient.Client.PostAsJsonAsync(
            $"{_apiConfiguracionClient}{MethodApiConfiguration.AreasValidadorasGuadar}", new
            {
                Lista = model
            });

            if (!response.Result.IsSuccessStatusCode)
                return false;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            var success = Convert.ToBoolean(jResult["Success"]);

            return success;
        }

        public List<UiAreasValidadoras> AreasValidadorasObtenerTodos(int page, int rows)
        {
            var list = new List<UiAreasValidadoras>();
            var response = ServiceClient.Client.PostAsJsonAsync($"{_apiConfiguracionClient}{MethodApiConfiguration.AreasValidadorasObtenerTodos}",
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
                    list = jResult["List"].Children().Select(t => new UiAreasValidadoras
                    {
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = Convert.ToString(t["TipoServicio"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        Actividad = Convert.ToString(t["Actividad"]),
                        IdCentroCosto = Convert.ToString(t["IdCentroCosto"]),
                        Obligatorio = Convert.ToBoolean(t["Obligatorio"]),
                        EsActivo = Convert.ToBoolean(t["EsActivo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool AreasValidadorasActualizar(List<UiAreasValidadoras> model)
        {
            var response = ServiceClient.Client.PostAsJsonAsync(
            $"{_apiConfiguracionClient}{MethodApiConfiguration.AreasValidadorasActualizar}", new
            {
                Lista = model
            });

            if (!response.Result.IsSuccessStatusCode)
                return false;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            var success = Convert.ToBoolean(jResult["Success"]);

            return success;
        }

        public bool AreasValidadorasModificarEstatus(UiAreasValidadoras model)
        {
            var response = ServiceClient.Client.PostAsJsonAsync(
            $"{_apiConfiguracionClient}{MethodApiConfiguration.AreasValidadorasModificarEstatus}", new
            {
                Item = model
            });

            if (!response.Result.IsSuccessStatusCode)
                return false;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            var success = Convert.ToBoolean(jResult["Success"]);

            return success;
        }

        #endregion
    }
}