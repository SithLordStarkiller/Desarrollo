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
using Newtonsoft.Json;

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

        public UiCliente ClienteObtenerPorId(int identificador)
        {
            var result = new UiCliente();

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteObtenerPorId}",
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
                    result = new UiCliente();

                    result.Identificador = Convert.ToInt32(jResult["Entity"]["Identificador"]);
                    result.RazonSocial = jResult["Entity"]["RazonSocial"].ToString();
                    result.NombreCorto = jResult["Entity"]["NombreCorto"].ToString();
                    result.RFC = jResult["Entity"]["Rfc"].ToString();
                    result.IdRegimenFiscal = Convert.ToInt32(jResult["Entity"]["RegimenFiscal"]["Identificador"]);
                    result.RegimenFiscal = jResult["Entity"]["RegimenFiscal"]["Descripcion"].ToString();
                    result.IdSector = Convert.ToInt32(jResult["Entity"]["Sector"]["Identificador"]);
                    result.Sector = jResult["Entity"]["Sector"]["Descripcion"].ToString();
                    result.IsActive = Convert.ToBoolean(jResult["Entity"]["IsActive"]);

                    result.DomicilioFiscal = new UiDomicilioFiscal();
                    result.DomicilioFiscal.Identificador = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["Identificador"]);
                    result.DomicilioFiscal.IdCliente = Convert.ToInt32(jResult["Entity"]["Identificador"]);
                    result.DomicilioFiscal.IdPais = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["IdPais"]);

                    if (jResult["Entity"]["DomicilioFiscal"]["Asentamiento"] != null)
                    {
                        result.DomicilioFiscal.IdEstado = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Estado"]["Identificador"]);
                        result.DomicilioFiscal.IdMunicipio = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Municipio"]["Identificador"]);
                        result.DomicilioFiscal.IdAsentamiento = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Identificador"]);
                        result.DomicilioFiscal.CodigoPostal = jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["CodigoPostal"].ToString();
                    }

                    result.DomicilioFiscal.Calle = jResult["Entity"]["DomicilioFiscal"]["Calle"].ToString();
                    result.DomicilioFiscal.NoInterior = jResult["Entity"]["DomicilioFiscal"]["NoInterior"].ToString();
                    result.DomicilioFiscal.NoExterior = jResult["Entity"]["DomicilioFiscal"]["NoExterior"].ToString();

                    result.Solicitantes = jResult["Entity"]["Solicitantes"].Children().Select((p, index) => new UiSolicitante
                    {
                        Numero = index,
                        Identificador = Convert.ToInt32(p["Identificador"]),
                        IdCliente = Convert.ToInt32(jResult["Entity"]["Identificador"]),
                        IdTipoPersona = Convert.ToInt32(p["IdTipoPersona"]),
                        Nombre = p["Nombre"].ToString(),
                        ApellidoPaterno = p["ApellidoPaterno"].ToString(),
                        ApellidoMaterno = p["ApellidoMaterno"].ToString(),
                        Cargo = p["Cargo"].ToString(),
                        IsActive = Convert.ToBoolean(p["Activo"]),
                        Telefonos = p["Telefonos"].Children().Select(q => new UiTelefonoContacto
                        {
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                            TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                            Numero = q["Numero"].ToString(),
                            Extension = q["Extension"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList(),
                        Correos = p["Correos"].Children().Select(q => new UiCorreoContacto
                        {
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            CorreoElectronico = q["CorreoElectronico"].ToString(),
                            isActive = Convert.ToBoolean(q["Activo"])
                        }).ToList()
                    }).ToList();
                    result.Contactos = jResult["Entity"]["Contactos"].Children().Select((p, index) => new UiClienteContacto
                    {
                        Numero = index,
                        Identificador = Convert.ToInt32(p["Identificador"]),
                        IdCliente = Convert.ToInt32(jResult["Entity"]["Identificador"]),
                        IdTipoPersona = Convert.ToInt32(p["IdTipoPersona"]),
                        IdTipoContacto = Convert.ToInt32(p["TipoContacto"]["Identificador"]),
                        TipoContacto = p["TipoContacto"]["Nombre"].ToString(),
                        Nombre = p["Nombre"].ToString(),
                        ApellidoPaterno = p["ApellidoPaterno"].ToString(),
                        ApellidoMaterno = p["ApellidoMaterno"].ToString(),
                        Cargo = p["Cargo"].ToString(),
                        IsActive = Convert.ToBoolean(p["Activo"]),
                        Telefonos = p["Telefonos"].Children().Select(q => new UiTelefonoContacto
                        {
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                            TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                            Numero = q["Numero"].ToString(),
                            Extension = q["Extension"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList(),
                        Correos = p["Correos"].Children().Select(q => new UiCorreoContacto
                        {
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            CorreoElectronico = q["CorreoElectronico"].ToString(),
                            isActive = Convert.ToBoolean(q["Activo"])
                        }).ToList()
                    }).ToList();
                }
            }
            return result;
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
        public List<UiCliente> ObtenerClientes(UiCliente model, int page = 1, int rows = 20)
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
                        IdSector = model.IdSector,
                        RFC = model.RFC
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
                        Identificador = Convert.ToInt32(jResult["Entity"]["Identificador"]),
                        IdCliente = Convert.ToInt32(jResult["Entity"]["Cliente"]["Identificador"]),
                        IdPais = Convert.ToInt32(jResult["Entity"]["IdPais"]),
                        IdEstado = Convert.ToInt32(jResult["Entity"]["Asentamiento"]["Estado"]["Identificador"]),
                        Estado = jResult["Entity"]["Asentamiento"]["Estado"]["Nombre"].ToString(),
                        IdMunicipio = Convert.ToInt32(jResult["Entity"]["Asentamiento"]["Municipio"]["Identificador"]),
                        Municipio = jResult["Entity"]["Asentamiento"]["Municipio"]["Nombre"].ToString(),
                        IdAsentamiento = Convert.ToInt32(jResult["Entity"]["Asentamiento"]["Identificador"]),
                        Asentammiento = jResult["Entity"]["Asentamiento"]["Nombre"].ToString(),
                        CodigoPostal = jResult["Entity"]["Asentamiento"]["CodigoPostal"].ToString(),
                        Calle = jResult["Entity"]["Calle"].ToString(),
                        NoInterior = jResult["Entity"]["NoInterior"].ToString(),
                        NoExterior = jResult["Entity"]["NoExterior"].ToString()
                    };
                }
            }
            return result;
        }
        public bool SaveCliente(UiCliente model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        RazonSocial = model.RazonSocial,
                        NombreCorto = model.NombreCorto,
                        RFC = model.RFC,
                        RegimenFiscal = new
                        {
                            Identificador = model.IdRegimenFiscal
                        },
                        Sector = new
                        {
                            Identificador = model.IdSector
                        },
                        DomicilioFiscal = new
                        {
                            Identificador = model.DomicilioFiscal.Identificador,
                            Asentamiento = new
                            {
                                Identificador = model.DomicilioFiscal.IdAsentamiento,
                                CodigoPostal = model.DomicilioFiscal.CodigoPostal,
                                Municipio = new { Identificador = model.DomicilioFiscal.IdMunicipio },
                                Estado = new { Identificador = model.DomicilioFiscal.IdEstado }
                            },
                            Calle = model.DomicilioFiscal.Calle,
                            NoExterior = model.DomicilioFiscal.NoExterior,
                            NoInterior = model.DomicilioFiscal.NoInterior
                        },
                        Contactos = model.Contactos != null ? model.Contactos.Select(p => new
                        {
                            Identificador = p.Identificador,
                            IdTipoPersona = p.IdTipoPersona,
                            TipoContacto = new { Identificador = p.IdTipoContacto },
                            Nombre = p.Nombre,
                            ApellidoPaterno = p.ApellidoPaterno,
                            ApellidoMaterno = p.ApellidoMaterno,
                            Cargo = p.Cargo,
                            Activo = p.IsActive,
                            Telefonos = p.Telefonos != null ? p.Telefonos.Select(q => new
                            {
                                Identificador = q.Identificador,
                                TipoTelefono = new { Identificador = q.IdTipoTelefono },
                                Numero = q.Numero,
                                Extension = q.Extension,
                                Activo = q.IsActive
                            }) : null,
                            Correos = p.Correos != null ? p.Correos.Select(r => new
                            {
                                Identificador = r.Identificador,
                                CorreoElectronico = r.CorreoElectronico,
                                Activo = r.isActive
                            }) : null
                        }) : null,
                        Solicitantes = model.Solicitantes != null ? model.Solicitantes.Select(p => new
                        {
                            Identificador = p.Identificador,
                            IdTipoPersona = p.IdTipoPersona,
                            Nombre = p.Nombre,
                            ApellidoPaterno = p.ApellidoPaterno,
                            ApellidoMaterno = p.ApellidoMaterno,
                            Cargo = p.Cargo,
                            Activo = p.IsActive,
                            Telefonos = p.Telefonos != null ? p.Telefonos.Select(q => new
                            {
                                Identificador = q.Identificador,
                                TipoTelefono = new { Identificador = q.IdTipoTelefono },
                                Numero = q.Numero,
                                Extension = q.Extension,
                                Activo = q.IsActive
                            }) : null,
                            Correos = p.Correos != null ? p.Correos.Select(r => new
                            {
                                Identificador = r.Identificador,
                                CorreoElectronico = r.CorreoElectronico,
                                Activo = r.isActive
                            }) : null
                        }) : null
                    }
                });
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

        public bool ClienteCambiarEstatus(UiCliente model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IsActive = model.IsActive
                    }
                });
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
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Telefonos = t["Telefonos"].Select(r => new UiTelefonoContacto { }).ToList(),
                        Correos = t["Correos"].Select(q => new UiCorreoContacto { }).ToList()
                    }).OrderBy(t => t.Identificador).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        #endregion

        #region Instalaciones
        public List<UiInstalacion> InstalacionObtenerTodos(int page, int rows)
        {
            var list = new List<UiInstalacion>();
            var response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerTodos}",
                 new
                 {
                     Paging = new
                     {
                         All = true,
                         CurrentPage = page,
                         Rows = rows
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

        public UiInstalacion InstalacionObtenerPorId(UiInstalacion model)
        {
            var item = new UiInstalacion();

            var response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerTodos}",
                 new
                 {
                     Item = model
                 });

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    var entity = jResult["Entity"];
                    item = JsonConvert.DeserializeObject<UiInstalacion>(entity.ToString());
                }
            }

            return item;
        }

        public List<UiInstalacion> InstalacionObtenerPorCriterio(UiInstalacion entity, Paging paging)
        {
            var list = new List<UiInstalacion>();
            var response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerPorCriterio}",
                 new
                 {
                     Item = entity,
                     Paging = paging
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

        public bool InstalacionCambiarEstatus(UiInstalacion entity)
        {
            var response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionCambiarEstatus}",
                 new
                 {
                     Item = entity
                 });

            if (!response.Result.IsSuccessStatusCode) return false;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);
            var result = Convert.ToBoolean(jResult["Success"]);

            return result;
        }

        public UiInstalacion InstalacionGuardar(UiInstalacion entity)
        {
            var item = new UiInstalacion();

            var response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionCambiarEstatus}",
                 new
                 {
                     Item = entity
                 });

            if (!response.Result.IsSuccessStatusCode) return item;

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    var dato = jResult["Entity"];
                    item = JsonConvert.DeserializeObject<UiInstalacion>(dato.ToString());
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return item;
        }

        #endregion

        #region SOLICITUDES

        public List<UiSolicitudes> ObtenerSolicitudes(int page, int rows)
        {
            List<UiSolicitudes> list = new List<UiSolicitudes>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiSolicitudes
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdCliente = Convert.ToInt32(t["IdCliente"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        RFC = t["RFC"].ToString(),
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        RegimenFiscal = Convert.ToString(t["NombreRegimenFiscal"]),
                        IdSector = Convert.ToInt32(t["IdSector"]),
                        Sector = t["NombreSector"].ToString(),
                        IdTipoSolicitud = Convert.ToInt32(t["IdTipoSolicitud"]),
                        TipoSolicitud = t["NombreTipoSolicitud"].ToString(),
                        DocumentoSoporte = Convert.ToInt32(t["DocumentoSoporte"]),
                        Folio = t["Folio"].ToString(),
                        Minuta = Convert.ToInt32(t["Minuta"]),
                        Cancelado = Convert.ToBoolean(t["Cancelado"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiSolicitudes> SolicitudesObtenerPorCriterio(int page, int rows, UiSolicitudes model)
        {
            List<UiSolicitudes> list = new List<UiSolicitudes>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        RazonSocial = model.RazonSocial,
                        NombreCorto = model.NombreCorto,
                        RFC = model.RFC,
                        IdRegimenFiscal = model.IdRegimenFiscal,
                        IdSector = model.IdSector
                    }//,
                    //Paging = new
                    //{
                    //    All = false,
                    //    CurrentPage = page,
                    //    Rows = rows
                    //}
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiSolicitudes
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdCliente = Convert.ToInt32(t["IdCliente"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        RFC = t["RFC"].ToString(),
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        RegimenFiscal = Convert.ToString(t["NombreRegimenFiscal"]),
                        IdSector = Convert.ToInt32(t["IdSector"]),
                        Sector = t["NombreSector"].ToString(),
                        IdTipoSolicitud = Convert.ToInt32(t["IdTipoSolicitud"]),
                        TipoSolicitud = t["NombreTipoSolicitud"].ToString(),
                        DocumentoSoporte = Convert.ToInt32(t["DocumentoSoporte"]),
                        Folio = t["Folio"].ToString(),
                        Minuta = Convert.ToInt32(t["Minuta"]),
                        Cancelado = Convert.ToBoolean(t["Cancelado"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiSolicitudes> SolicitudesObtenerPorId(int id)
        {
            List<UiSolicitudes> list = new List<UiSolicitudes>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesObtenerPorId}",
                new
                {
                    Item = new
                    {
                        Identificador = id
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiSolicitudes
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdCliente = Convert.ToInt32(t["IdCliente"]),
                        RazonSocial = t["RazonSocial"].ToString(),
                        NombreCorto = t["NombreCorto"].ToString(),
                        RFC = t["RFC"].ToString(),
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        RegimenFiscal = Convert.ToString(t["NombreRegimenFiscal"]),
                        IdSector = Convert.ToInt32(t["IdSector"]),
                        Sector = t["NombreSector"].ToString(),
                        IdTipoSolicitud = Convert.ToInt32(t["IdTipoSolicitud"]),
                        TipoSolicitud = t["NombreTipoSolicitud"].ToString(),
                        DocumentoSoporte = Convert.ToInt32(t["DocumentoSoporte"]),
                        Folio = t["Folio"].ToString(),
                        Minuta = Convert.ToInt32(t["Minuta"]),
                        Cancelado = Convert.ToBoolean(t["Cancelado"])
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