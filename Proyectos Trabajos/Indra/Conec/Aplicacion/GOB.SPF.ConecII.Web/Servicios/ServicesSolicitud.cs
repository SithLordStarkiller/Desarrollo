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
                        IdRegimenFiscal = t["RegimenFiscal"]["Identificador"] != null ? Convert.ToInt32(t["RegimenFiscal"]["Identificador"]) : 0,
                        RegimenFiscal = t["RegimenFiscal"]["Descripcion"] != null ? t["RegimenFiscal"]["Descripcion"].ToString() : null,
                        IdSector = t["Sector"]["Identificador"] != null ? Convert.ToInt32(t["Sector"]["Identificador"]) : 0,
                        Sector = t["Sector"]["Descripcion"] != null ? t["Sector"]["Descripcion"].ToString() : null,
                        RFC = t["Rfc"] != null ? t["Rfc"].ToString() : null,
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
                        NombreCorto = model.NombreCorto,
                        IdRegimenFiscal = model.IdRegimenFiscal,
                        IdSector = model.IdSector
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
        
        public UiDomicilioFiscal ObtenerDomicilioFiscal(int identificador)
        {
            UiDomicilioFiscal result = new UiDomicilioFiscal();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteDomicilioFiscalObtener}",
                new
                {
                    Item = new
                    {
                        Identificador = identificador
                    }
                });

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    result = new UiDomicilioFiscal
                    {
                        Identificador = Convert.ToInt32(jResult["Entity"].SelectToken("Identificador")),
                        IdCliente = Convert.ToInt32(jResult["Entity"].SelectToken("IdCliente")),
                        IdPais = Convert.ToInt32(jResult["Entity"].SelectToken("IdPais")),
                        IdEstado = Convert.ToInt32(jResult["Entity"].SelectToken("IdEstado")),
                        IdMunicipio = Convert.ToInt32(jResult["Entity"].SelectToken("IdMunicipio")),
                        IdAsentamiento = Convert.ToInt32(jResult["Entity"].SelectToken("IdAsentamiento")),
                        CodigoPostal = jResult["Entity"].SelectToken("CodigoPostal").ToString(),
                        Calle = jResult["Entity"].SelectToken("Calle").ToString(),
                        NoInterior = jResult["Entity"].SelectToken("NoInterior").ToString(),
                        NoExterior = jResult["Entity"].SelectToken("NoExterior").ToString()
                    };
                }
            }
            return result;
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
                        IdTipoPersona = Convert.ToInt32(t["IdTipoPersona"]),
                        Nombre = t["Nombre"].ToString(),
                        ApellidoPaterno = t["ApellidoPaterno"].ToString(),
                        ApellidoMaterno = t["ApellidoMaterno"].ToString(),
                        Cargo = t["Cargo"].ToString(),
                        Activo = Convert.ToBoolean(t["Activo"]),
                        Telefonos = t["Telefonos"].Select(r => new UiTelefonoContacto { }).ToList(),
                        Correos = t["Correos"].Select(q=>new UiCorreoContacto { }).ToList()
                    }).OrderBy(t => t.Identificador).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        #endregion

        #region Instalaciones
        public List<UiInstalacion> InstalacionObtenerTodos(int page, int rows, UiInstalacion model)
        {
            var list = new List<UiInstalacion>();
            var response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerTodos}",
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
            if (!response.Result.IsSuccessStatusCode) return list;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            if (!Convert.ToBoolean(jResult["Success"])) return list;

            list = jResult["List"].Children().Select(t => new UiInstalacion
            {
                Identificador = Convert.ToInt32(t["Identificador"])
            }).ToList();
            pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
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