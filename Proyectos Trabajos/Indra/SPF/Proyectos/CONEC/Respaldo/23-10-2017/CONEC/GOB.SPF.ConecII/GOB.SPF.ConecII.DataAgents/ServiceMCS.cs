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
    public class ServiceMCS : IService
    {
        private HttpClient client;
        private string apiMcsClient;

        public ServiceMCS()
        {
            apiMcsClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiMCS];
            client = new HttpClient();
        }

        #region Asentamientos

        public List<Asentamiento> ObtenerAsentamientos(Asentamiento asentamiento)
        {
            var list = new List<Asentamiento>();

            var response = client.PostAsJsonAsync($"{apiMcsClient}{MethodApiCatalog.AsentamientosObtener}", new
            {
                IdEstado = asentamiento.Estado.Identificador,
                CodigoPostal = asentamiento.CodigoPostal,
                IdMunicipio = asentamiento.Municipio.Identificador
            });

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jObject = JObject.Parse(query);
                if (Convert.ToBoolean(jObject["Success"]))
                {
                    list = jObject["Value"].Children().Select(p => new Asentamiento()
                    {
                        Identificador = Convert.ToInt32(p["IdAsentamiento"]),
                        Nombre = p["Asentamiento"].ToString(),
                        CodigoPostal = p["CodigoPostal"].ToString(),
                        Estado = new Estado { Identificador = Convert.ToInt32(p["IdEstado"]), Nombre = p["Estado"].ToString() },
                        Municipio = new Municipio { Identificador = Convert.ToInt32(p["IdMunicipio"]), Nombre = p["Municipio"].ToString() },
                    }).OrderBy(x => x.Nombre).ToList(); 
                }
            }
            return list;
        }

        #endregion

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
            List<GrupoTarifario> list = new List<GrupoTarifario>();
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

        #region MUNICIPIO
        public List<Municipio> ObtenerMunicipios(Estado estado)
        {
            List<Municipio> list = new List<Municipio>(); ;
            Task<HttpResponseMessage> response = client.GetAsync($"{apiMcsClient}{MethodApiCatalog.MunicipiosObtener}/{estado.Identificador}");
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Municipio
                    {
                        Identificador = Convert.ToInt32(t["IdMunicipio"]),
                        IdEstado = Convert.ToInt32(t["IdEstado"]),
                        Nombre = t["Descripcion"].ToString(),
                        Activo = Convert.ToBoolean(t["Vigente"]),
                        Estado = new Estado { Identificador = Convert.ToInt32(t["IdEstado"]) }
                    }).ToList();
                }
            }
            return list;

        }
        #endregion

        #region Tipo instalación

        public List<TipoInstalacion> ObtenerTiposInstalacion()
        {
            var list = new List<TipoInstalacion>();
            var response = client.GetAsync($"{apiMcsClient}{MethodApiCatalog.TiposInstalacionObtener}");
            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;
                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new TipoInstalacion()
                    {
                        Identificador = Convert.ToInt32(t["IdTipoInstalacion"]),
                        Nombre = t["Nombre"].ToString(),
                    }).ToList();
                }
            }
            return list;
        }


        #endregion

        #region Estacion

        public List<Estacion> ObtenerEstaciones()
        {
            var list = new List<Estacion>();
            var response = client.GetAsync($"{apiMcsClient}{MethodApiCatalog.EstacionesObtener}");
            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;
                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Estacion()
                    {
                        Identificador = Convert.ToInt32(t["IdEstacion"]),
                        Nombre = t["Estacion"].ToString(),
                    }).ToList();
                }
            }
            return list;
        }

        #endregion

        #region Zona
        public List<Zona> ObtenerZonas()
        {
            var list = new List<Zona>();
            var response = client.GetAsync($"{apiMcsClient}{MethodApiCatalog.ZonasObtener}");
            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;
                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["Value"].Children().Select(t => new Zona()
                    {
                        Identificador = Convert.ToInt32(t["IdZona"]),
                        Nombre = t["Zona"].ToString(),
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
