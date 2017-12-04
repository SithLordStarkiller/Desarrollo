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
    public class ServicesSolicitud : IService
    {
        private int pages { get; set; }

        internal int Pages { get { return pages; } }

        internal HttpClient client;
        string urlApiSolicitud;

        public ServicesSolicitud()
        {
            urlApiSolicitud = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiSolicitud];
            client = new HttpClient();
        }

        #region Clientes
        public List<UiCliente> ClientesObtenerPorRazonSocial(string searchText)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ObtenerTodosPorRazonSocial}",
                new
                {
                    Item = new { RazonSocial = searchText },
                    Paging = new { All = false, CurrentPage = 1, Rows = 1 }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiCliente
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        IdRegimenFiscal = t["IdRegimenFiscal"] != null ? Convert.ToInt32(t["IdRegimenFiscal"]) : 0,
                        RegimenFiscal = t["RegimenFiscal"] != null ? t["RegimenFiscal"].ToString() : null,
                        IdSector = t["IdSector"] != null ? Convert.ToInt32(t["IdSector"]) : 0,
                        Sector = t["Sector"] != null ? t["Sector"].ToString() : null,
                        RFC = t["RFC"] != null ? t["RFC"].ToString() : null,
                        IsActive = Convert.ToBoolean(t["IsActive"])
                    }).OrderBy(t => t.Identificador).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        public List<UiCliente> ClientesObtenerPorNombreCorto(string searchText)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ObtenerTodosPorNombreCorto}",
                new
                {
                    Item = new { NombreCorto = searchText },
                    Paging = new { All = false, CurrentPage = 1, Rows = 1 }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiCliente
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        IdRegimenFiscal = t["IdRegimenFiscal"] != null ? Convert.ToInt32(t["IdRegimenFiscal"]) : 0,
                        RegimenFiscal = t["RegimenFiscal"] != null ? t["RegimenFiscal"].ToString() : null,
                        IdSector = t["IdSector"] != null ? Convert.ToInt32(t["IdSector"]) : 0,
                        Sector = t["Sector"] != null ? t["Sector"].ToString() : null,
                        RFC = t["RFC"] != null ? t["RFC"].ToString() : null,
                        IsActive = Convert.ToBoolean(t["IsActive"])
                    }).OrderBy(t => t.Identificador).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        public List<UiCliente> ObtenerClientes(int page, int rows)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClientesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiCliente
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        IdRegimenFiscal = t["IdRegimenFiscal"] != null ? Convert.ToInt32(t["IdRegimenFiscal"]) : 0,
                        RegimenFiscal = t["RegimenFiscal"] != null ? t["RegimenFiscal"].ToString() : null,
                        IdSector = t["IdSector"] != null ? Convert.ToInt32(t["IdSector"]) : 0,
                        Sector = t["Sector"] != null ? t["Sector"].ToString() : null,
                        RFC = t["RFC"] != null ? t["RFC"].ToString() : null,
                        IsActive = Convert.ToBoolean(t["IsActive"])
                    }).OrderBy(t => t.Identificador).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        public List<UiCliente> ObtenerClientes(int page, int rows, UiCliente model)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        IsActive = model.IsActive,
                        RazonSocial = model.RazonSocial,
                        NombreCorto = model.NombreCorto
                    },
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
                    list = jResult["List"].Children().Select(t => new UiCliente
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        IdRegimenFiscal = t["IdRegimenFiscal"] != null ? Convert.ToInt32(t["IdRegimenFiscal"]) : 0,
                        RegimenFiscal = t["RegimenFiscal"] != null ? t["RegimenFiscal"].ToString() : null,
                        IdSector = t["IdSector"] != null ? Convert.ToInt32(t["IdSector"]) : 0,
                        Sector = t["Sector"] != null ? t["Sector"].ToString() : null,
                        RFC = t["RFC"] != null ? t["RFC"].ToString() : null,
                        IsActive = Convert.ToBoolean(t["IsActive"])
                    }).OrderBy(t => t.Identificador).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        #endregion

        #region Solicitante
        public List<UiSolicitante> ObtenerSolicitantes(int IdCliente)
        {
            List<UiSolicitante> list = new List<UiSolicitante>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitantesObtener}",
                new
                {
                    Paging = new { All = false, CurrentPage = 0, Rows = 0 }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiSolicitante
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdCliente = Convert.ToInt32(t["IdCliente"]),
                        IdTipoContacto = Convert.ToInt32(t["IdTipoContacto"]),
                        Nombre = t["Nombre"].ToString(),
                        ApellidoPaterno = t["ApellidoPaterno"].ToString(),
                        ApellidoMaterno = t["ApellidoMaterno"].ToString(),
                        Cargo = t["Cargo"].ToString(),
                        Activo = Convert.ToBoolean(t["Activo"])
                    }).OrderBy(t => t.Identificador).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        } 
        #endregion

        #region Instalaciones

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