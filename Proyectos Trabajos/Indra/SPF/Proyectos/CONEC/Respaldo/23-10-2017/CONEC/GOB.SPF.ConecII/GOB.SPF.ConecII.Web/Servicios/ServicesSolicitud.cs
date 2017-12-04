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
using System.Net.Http.Formatting;
using GOB.SPF.ConecII.Web.Models.Solicitud;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesSolicitud : IService
    {
        internal HttpClient client;
        string urlApiSolicitud;
        public ServicesSolicitud()
        {
            urlApiSolicitud = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiSolicitud];
            client = new HttpClient();
        }

        internal int Pages { get { return pages; } }
        private int pages { get; set; }
        #region Clientes
        public bool ClienteCambiarEstatus(UiCliente model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteCambiarEstatus}",
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

        public List<Models.Solicitud.UiSolicitud> ClienteListaSolicitudObtenerPorId(int identificador)
        {
            var list = new List<Models.Solicitud.UiSolicitud>();

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteListaSolicitudObtenerPorId}",
                new
                {
                    IdCliente = identificador
                });

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t =>
                    {
                        var solicitud = new Models.Solicitud.UiSolicitud();
                        solicitud.Folio = Convert.ToInt32(t["Folio"]);
                        solicitud.Identificador = Convert.ToInt32(t["Identificador"]);
                        solicitud.FechaRegistro = Convert.ToDateTime(t["FechaRegistro"]);
                        solicitud.TipoSolicitud = new UiTipoSolicitud
                        {
                            Identificador = Convert.ToInt32(t["TipoSolicitud"]["Identificador"]),
                            Nombre = Convert.ToString(t["TipoSolicitud"]["Nombre"])
                        };
                        return solicitud;
                    }).OrderBy(t => t.Identificador).ToList();
                }
            }
            return list;
        }

        public UiCliente ClienteObtenerPorId(int identificador)
        {
            var result = new UiCliente();

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteObtenerPorId}",
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
                        Telefonos = p["Telefonos"].Children().Select((q, idx) => new UiTelefonoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                            TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                            Numero = q["Numero"].ToString(),
                            Extension = q["Extension"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList(),
                        Correos = p["Correos"].Children().Select((q, idx) => new UiCorreoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            CorreoElectronico = q["CorreoElectronico"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
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
                        Telefonos = p["Telefonos"].Children().Select((q, idx) => new UiTelefonoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                            TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                            Numero = q["Numero"].ToString(),
                            Extension = q["Extension"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList(),
                        Correos = p["Correos"].Children().Select((q, idx) => new UiCorreoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            CorreoElectronico = q["CorreoElectronico"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList()
                    }).ToList();
                    result.Documentos = jResult["Entity"]["Documentos"]["$values"].Children().Select((p, index) =>
                    {
                        var documento = new UiDocumento();
                        documento.Numero = index;
                        documento.Identificador = Convert.ToInt32(p["Identificador"]);
                        documento.Nombre = p["Nombre"].ToString();
                        documento.TipoDocumento = new UiTiposDocumento
                        {
                            Identificador = Convert.ToInt32(p["TipoDocumento"]["Identificador"])
                        };
                        documento.DocumentoSoporte = Convert.ToInt32(p["ArchivoId"]);
                        documento.FechaRegistro = Convert.ToDateTime(p["FechaRegistro"]);
                        documento.IsActive = Convert.ToBoolean(p["Activo"]);

                        return documento;
                    }).ToList();
                }
            }
            return result;
        }

        public List<UiCliente> ClientesObtenerPorCriterio(UiCliente model, int page = 1, int rows = 20)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        IsActive = model.IsActive,
                        RazonSocial = model.RazonSocial,
                        NombreCorto = model.NombreCorto,
                        RegimenFiscal = new { Identificador = model.IdRegimenFiscal },
                        Sector = new { Identificador = model.IdSector },
                        RFC = model.RFC
                    },
                    Paging = new
                    {
                        All = true,
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

        public List<UiCliente> ClientesObtenerPorNombreCorto(string searchText)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ObtenerTodosPorNombreCorto}",
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

        public List<UiCliente> ClientesObtenerPorRazonSocial(string searchText)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ObtenerTodosPorRazonSocial}",
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
        public UiCliente ClienteSolicitudObtenerPorId(int identificador)
        {
            var result = new UiCliente();

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteSolicitudObtenerPorId}",
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
                        result.DomicilioFiscal.Estado = Convert.ToString(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Estado"]["Nombre"]);
                        result.DomicilioFiscal.IdMunicipio = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Municipio"]["Identificador"]);
                        result.DomicilioFiscal.Municipio = Convert.ToString(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Municipio"]["Nombre"]);
                        result.DomicilioFiscal.IdAsentamiento = Convert.ToInt32(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Identificador"]);
                        result.DomicilioFiscal.Asentammiento = Convert.ToString(jResult["Entity"]["DomicilioFiscal"]["Asentamiento"]["Nombre"]);
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
                        Telefonos = p["Telefonos"].Children().Select((q, idx) => new UiTelefonoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                            TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                            Numero = q["Numero"].ToString(),
                            Extension = q["Extension"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList(),
                        Correos = p["Correos"].Children().Select((q, idx) => new UiCorreoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            CorreoElectronico = q["CorreoElectronico"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
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
                        Telefonos = p["Telefonos"].Children().Select((q, idx) => new UiTelefonoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                            TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                            Numero = q["Numero"].ToString(),
                            Extension = q["Extension"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList(),
                        Correos = p["Correos"].Children().Select((q, idx) => new UiCorreoContacto
                        {
                            Indice = idx,
                            Identificador = Convert.ToInt32(q["Identificador"]),
                            IdExterno = Convert.ToInt32(p["Identificador"]),
                            CorreoElectronico = q["CorreoElectronico"].ToString(),
                            IsActive = Convert.ToBoolean(q["Activo"])
                        }).ToList()
                    }).ToList();
                    result.Documentos = jResult["Entity"]["Documentos"]["$values"].Children().Select((p, index) =>
                    {
                        var documento = new UiDocumento();
                        documento.Numero = index;
                        documento.Identificador = Convert.ToInt32(p["Identificador"]);
                        documento.Nombre = p["Nombre"].ToString();
                        documento.TipoDocumento = new UiTiposDocumento
                        {
                            Identificador = Convert.ToInt32(p["TipoDocumento"]["Identificador"])
                        };
                        documento.DocumentoSoporte = Convert.ToInt32(p["ArchivoId"]);
                        documento.FechaRegistro = Convert.ToDateTime(p["FechaRegistro"]);
                        documento.IsActive = Convert.ToBoolean(p["Activo"]);

                        return documento;
                    }).ToList();

                    if (jResult["Entity"]["Solicitud"] != null)
                    {
                        result.Solicitudes = jResult["Entity"]["Solicitud"]["$values"].Children().Select(p =>
                        {
                            var solicitudes = new Models.Solicitud.UiSolicitud();
                            solicitudes.Identificador = Convert.ToInt32(p["Identificador"]);
                            solicitudes.Cliente = new UiCliente
                            {
                                Identificador = Convert.ToInt32(p["IdCliente"])
                            };
                            solicitudes.TipoSolicitud = new UiTipoSolicitud
                            {
                                Identificador = Convert.ToInt32(p["TipoSolicitud"]["Identificador"]),
                                Nombre = Convert.ToString(p["TipoSolicitud"]["Nombre"])
                            };
                            solicitudes.Servicios = p["Servicios"].Children().Select(idx =>
                            {
                                var servicio = new UiServicio();
                                servicio.Identificador = Convert.ToInt32(idx["Identificador"]);
                                servicio.FechaInicial = Convert.ToDateTime(idx["FechaInicio"]);
                                servicio.FechaInicio = Convert.ToDateTime(idx["FechaInicio"]);
                                servicio.FechaFin = Convert.ToDateTime(idx["FechaFin"]);
                                servicio.FechaFinal = Convert.ToDateTime(idx["FechaFin"]);
                                servicio.NumeroPersonas = Convert.ToInt32(idx["NumeroPersonas"]);
                                servicio.HorasCurso = Convert.ToInt32(idx["HorasCurso"]);
                                servicio.Observaciones = Convert.ToString(idx["Observaciones"]);
                                servicio.TieneComite = Convert.ToBoolean(idx["TieneComite"]);
                                servicio.ObservacionesComite = Convert.ToString(idx["ObservacionesComite"]);
                                servicio.BienCustodia = Convert.ToString(idx["BienCustodia"]);
                                servicio.Viable = Convert.ToBoolean(idx["Viable"]);
                                servicio.FechaComite = Convert.ToDateTime(idx["FechaComite"]);
                                servicio.Cuota = new UiCuota();
                                servicio.Cuota.Identificador = Convert.ToInt32(idx["Cuota"]["Identificador"]);
                                servicio.TipoServicio = new UiTipoServicio();
                                servicio.TipoServicio.Identificador =
                                    Convert.ToInt32(idx["TipoServicio"]["Identificador"]);
                                servicio.TipoServicio.Nombre = Convert.ToString(idx["TipoServicio"]["Nombre"]);
                                servicio.TipoServicio.Clave = Convert.ToString(idx["TipoServicio"]["Clave"]);
                                servicio.TipoServicio.Activo = Convert.ToBoolean(idx["TipoServicio"]["Activo"]);
                                servicio.TipoInstalacionesCapacitacion = new UiTipoInstalacionesCapacitacion();
                                servicio.TipoInstalacionesCapacitacion.Identificador =
                                    Convert.ToInt32(idx["TipoInstalacionesCapacitacion"]["Identificador"]);
                                servicio.Documentos = idx["Documentos"].Children().Select((d) =>
                                {
                                    var documento = new UiServicioDocumento();
                                    documento.Observaciones = Convert.ToString(d["Observaciones"]);
                                    documento.FechaRegistro = Convert.ToDateTime(d["FechaRegistro"]);
                                    documento.Nombre = Convert.ToString(d["Nombre"]);
                                    documento.TipoDocumento = new UiTiposDocumento();
                                    documento.TipoDocumento.Identificador =
                                        Convert.ToInt32(d["TipoDocumento"]["Identificador"]);
                                    return documento;
                                }).ToList();
                                return servicio;
                            }).ToList();

                            return solicitudes;

                        }).ToList();
                    }
                }
            }
            return result;
        }
        public List<UiCliente> ObtenerClientes(int page, int rows)
        {
            List<UiCliente> list = new List<UiCliente>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClientesObtener}",
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
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        IsActive = true,
                        RazonSocial = model.RazonSocial,
                        Instalaciones = model.Instalaciones,
                        RFC = model.RFC
                    },
                    Paging = new
                    {
                        All = true,
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
                        IsActive = Convert.ToBoolean(t["IsActive"]),
                        //Instalaciones = 
                        //Instalaciones = t["Instalaciones"].Select(x => new UiInstalacion
                        //{
                        //    Identificador = Convert.ToInt32(x["Identificacor"])
                        //})
                        //Instalaciones.Identificador = t["Instalaciones"]["Identificador"] != null ? Convert.ToInt32(t["Instalaciones"]["Identificador"]) : 0

                    }).OrderBy(t => t.Identificador).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        public UiDomicilioFiscal ObtenerDomicilioFiscal(int identificador)
        {
            UiDomicilioFiscal result = new UiDomicilioFiscal();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteDomicilioFiscalObtener}",
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

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ClienteGuardar}",
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
                                Activo = r.IsActive
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
                                Activo = r.IsActive
                            }) : null
                        }) : null,
                        Documentos = model.Documentos != null ? model.Documentos.Select(t => new
                        {
                            Identificador = t.Identificador,
                            ArchivoId = t.DocumentoSoporte,
                            Nombre = t.Nombre,
                            Base64 = t.Base64,
                            Activo = t.IsActive,
                            TipoDocumento = new { Identificador = t.IdTipoDocumento }
                        }).ToList() : null
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
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitantesObtener}",
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
        public bool InstalacionCambiarEstatus(UiInstalacion entity)
        {
            var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionCambiarEstatus}",
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

        public bool InstalacionGuardar(UiCliente entity)
        {
            var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionSave}",
                 new
                 {
                     Item = entity
                 });

            return response.Result.IsSuccessStatusCode;
        }

        public List<UiCliente> InstalacionObtenerPorCriterio(UiCliente model)
        {
            var list = new List<UiCliente>();
            var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerPorCriterio}",
                 new
                 {
                     Item = new
                     {
                         IsActive = true,
                         NombreCorto = model.NombreCorto,
                         RazonSocial = model.RazonSocial,
                         Instalaciones = model.Instalaciones,
                         Rfc = model.RFC
                     },
                     Paging = new
                     {
                         All = true,
                         CurrentPage = 1,
                         Rows = 1000,
                         pages = 1
                     }
                 });

            if (!response.Result.IsSuccessStatusCode) return list;

            var query = response.Result.Content.ReadAsStringAsync().Result;

            var jResult = JObject.Parse(query);

            if (!Convert.ToBoolean(jResult["Success"])) return list;

            list = jResult["List"].Children().Select(t => new UiCliente
            {
                Identificador = Convert.ToInt32(t["Identificador"]),

                RFC = t["Rfc"]?.ToString() ?? "",

                RazonSocial = t["RazonSocial"]?.ToString() ?? "",

                NombreCorto = t["NombreCorto"]?.ToString() ?? "",

                Instalaciones = t["Instalaciones"]?.Select(i => new UiInstalacion
                {
                    Identificador = i["Identificador"] == null ? 0 : Convert.ToInt32(i["Identificador"]),

                    Zona = i["Zona"] != null ? new UiZona
                    {
                        IdZona = i["Zona"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Zona"]["Identificador"]),
                        Nombre = i["Zona"]["Nombre"]?.ToString() ?? ""
                    } : new UiZona(),

                    Estacion = i["Estacion"] != null ? new UiEstaciones
                    {
                        IdEstacion = i["Estacion"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Estacion"]["Identificador"]),
                        Nombre = (i["Estacion"]["Nombre"]).ToString()
                    } : new UiEstaciones(),

                    Nombre = i["Nombre"]?.ToString() ?? "",

                    TipoInstalacion = i["TipoInstalacion"] != null ? new UiTipoInstalacion
                    {
                        Identificador = i["TipoInstalacion"]["Identificador"] == null ? 0 : Convert.ToInt32(i["TipoInstalacion"]["Identificador"])
                    } : new UiTipoInstalacion(),

                    TelefonosInstalacion = i["TelefonosInstalacion"]?.Select(tel => new UiTelefonoInstalacion
                    {
                        TipoTelefono = tel["TipoTelefono"] != null ? new UiTipoTelefono
                        {
                            Identificador = Convert.ToInt32(tel["TipoTelefono"]["Identificador"])
                        } : new UiTipoTelefono(),

                        Numero = tel["Numero"]?.ToString() ?? "",

                        IsActive = tel["Activo"] != null && Convert.ToBoolean(tel["Activo"])

                    }).ToList() ?? new List<UiTelefonoInstalacion>(),

                    CorreosInstalacion = i["CorreosInstalacion"]?.Select(cor => new UiCorreoInstalacion
                    {
                        Identificador = cor["Identificador"] != null ? Convert.ToInt32(cor["Identificador"]) : 0,

                        CorreoElectronico = cor["CorreoElectronico"]?.ToString() ?? "",

                        IsActive = cor["Activo"] != null && Convert.ToBoolean(cor["Activo"])

                    }).ToList() ?? new List<UiCorreoInstalacion>(),

                    FechaInicio = i["FechaInicio"] != null ? new DateTime() : Convert.ToDateTime(i["FechaInicio"]),

                    FechaFin = i["FechaFin"] != null ? new DateTime() : Convert.ToDateTime(i["FechaFin"]),

                    Calle = i["Calle"]?.ToString() ?? "",

                    NumInterior = i["NumInterior"]?.ToString() ?? "",

                    NumExterior = i["NumExterior"]?.ToString() ?? "",

                    Referencia = i["Referencia"]?.ToString() ?? "",

                    Colindancia = i["Colindancia"]?.ToString() ?? "",

                    Asentamiento = i["Asentamiento"] == null ? new UiAsentamiento() : new UiAsentamiento
                    {
                        Nombre = i["Asentamiento"]["Nombre"]?.ToString() ?? "",

                        CodigoPostal = i["Asentamiento"]["CodigoPostal"]?.ToString() ?? "",

                        Estado = i["Asentamiento"]["Estado"] == null ? new UiEstado() : new UiEstado
                        {
                            Identificador = i["Asentamiento"]["Estado"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Asentamiento"]["Estado"]["Identificador"]),
                            Nombre = i["Asentamiento"]["Estado"]["Nombre"]?.ToString() ?? ""
                        },

                        Municipio = i["Asentamiento"]["Municipio"] == null ? new UiMunicipio() : new UiMunicipio
                        {
                            Identificador = i["Asentamiento"]["Municipio"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Asentamiento"]["Municipio"]["Identificador"]),
                            Nombre = i["Asentamiento"]["Municipio"]["Nombre"]?.ToString() ?? ""
                        }
                    },

                    Latitud = i["Latitud"]?.ToString() ?? "",

                    Longitud = i["Longitud"]?.ToString() ?? "",

                    Divicion = i["Division"] == null ? new UiDivision() : new UiDivision
                    {
                        Identificador = i["Division"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Division"]["Identificador"])
                    },

                    Grupo = i["Grupo"] == null ? new UiGrupo() : new UiGrupo
                    {
                        Identificador = i["Grupo"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Grupo"]["Identificador"])
                    },

                    Fraccion = i["Fraccion"] == null ? new UiFracciones() : new UiFracciones
                    {
                        Identificador = i["Fraccion"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Fraccion"]["Identificador"])
                    },

                    Activo = i["Activo"] != null && Convert.ToBoolean(i["Activo"])

                }).ToList() ?? new List<UiInstalacion>()
            }).ToList();
            pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
            return list;
        }

        public UiInstalacion InstalacionObtenerPorId(UiInstalacion model)
        {
            var item = new UiInstalacion();

            var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerPorId}",
                new
                {
                    Item = model,
                    Paging = new
                    {
                        All = true,
                        CurrentPage = 1,
                        Rows = 1000,
                        pages = 1
                    }
                });

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    var entity = jResult["Entity"];

                    item = new UiInstalacion();
                    item.Identificador = entity["Identificador"] == null ? 0 : Convert.ToInt32(entity["Identificador"]);
                    item.Zona = entity["Zona"] != null
                        ? new UiZona
                        {
                            IdZona = entity["Zona"]["Identificador"] == null ? 0 : Convert.ToInt32(entity["Zona"]["Identificador"]),
                            Nombre = entity["Zona"]["Nombre"].ToString()
                        }
                        : new UiZona();
                    item.Estacion = entity["Estacion"] != null
                        ? new UiEstaciones
                        {
                            IdEstacion = Convert.ToInt32(entity["Estacion"]["Identificador"]),
                            Nombre = entity["Estacion"]["Nombre"].ToString()
                        }
                        : new UiEstaciones();
                    item.Nombre = entity["Nombre"]?.ToString() ?? "";
                    item.TipoInstalacion = entity["TipoInstalacion"] != null
                        ? new UiTipoInstalacion
                        {
                            Identificador =
                                entity["TipoInstalacion"]["Identificador"] == null
                                    ? 0
                                    : Convert.ToInt32(entity["TipoInstalacion"]["Identificador"]),
                            Nombre = entity["TipoInstalacion"]["Nombre"]?.ToString() ?? string.Empty
                        }
                        : new UiTipoInstalacion();
                    item.TelefonosInstalacion = entity["TelefonosInstalacion"]?.Select(tel => new UiTelefonoInstalacion
                    {
                        TipoTelefono = tel["TipoTelefono"] != null
                            ? new UiTipoTelefono
                            {
                                Identificador = Convert.ToInt32(tel["TipoTelefono"]["Identificador"]),
                                Name = (tel["TipoTelefono"]["Nombre"]).ToString()
                            }
                            : new UiTipoTelefono(),

                        Numero = tel["Numero"]?.ToString() ?? "",

                        IsActive = tel["Activo"] != null && Convert.ToBoolean(tel["Activo"])

                    }).ToList() ?? new List<UiTelefonoInstalacion>();
                    item.CorreosInstalacion = entity["CorreosInstalacion"]?.Select(cor =>
                    {
                        var instalacion = new UiCorreoInstalacion();
                        instalacion.Identificador = cor["Identificador"] != null
                            ? Convert.ToInt32(cor["Identificador"])
                            : 0;
                        instalacion.CorreoElectronico = cor["CorreoElectronico"]?.ToString() ?? "";
                        instalacion.IsActive = cor["Activo"] != null && Convert.ToBoolean(cor["Activo"]);
                        return instalacion;
                    }).ToList() ?? new List<UiCorreoInstalacion>();
                    item.FechaInicio = entity["FechaInicio"] != null ? new DateTime() : Convert.ToDateTime(entity["FechaInicio"]);
                    item.FechaFin = entity["FechaFin"] != null ? new DateTime() : Convert.ToDateTime(entity["FechaFin"]);
                    item.Calle = entity["Calle"]?.ToString() ?? "";
                    item.NumInterior = entity["NumInterior"]?.ToString() ?? "";
                    item.NumExterior = entity["NumExterior"]?.ToString() ?? "";
                    item.Referencia = entity["Referencia"]?.ToString() ?? "";
                    item.Colindancia = entity["Colindancia"]?.ToString() ?? "";
                    item.Asentamiento = entity["Asentamiento"] == null
                        ? new UiAsentamiento()
                        : new UiAsentamiento
                        {
                            Nombre = entity["Asentamiento"]["Nombre"]?.ToString() ?? "",

                            CodigoPostal = entity["Asentamiento"]["CodigoPostal"]?.ToString() ?? "",

                            Estado = entity["Asentamiento"]["Estado"] == null
                                ? new UiEstado()
                                : new UiEstado
                                {
                                    Identificador =
                                        entity["Asentamiento"]["Estado"]["Identificador"] == null
                                            ? 0
                                            : Convert.ToInt32(entity["Asentamiento"]["Estado"]["Identificador"])
                                },

                            Municipio = entity["Asentamiento"]["Municipio"] == null
                                ? new UiMunicipio()
                                : new UiMunicipio
                                {
                                    Identificador =
                                        entity["Asentamiento"]["Municipio"]["Identificador"] == null
                                            ? 0
                                            : Convert.ToInt32(entity["Asentamiento"]["Municipio"]["Identificador"])
                                }
                        };
                    item.Latitud = entity["Latitud"]?.ToString() ?? "";
                    item.Longitud = entity["Longitud"]?.ToString() ?? "";
                    item.Divicion = entity["Division"] == null
                        ? new UiDivision()
                        : new UiDivision
                        {
                            Identificador =
                                entity["Division"]["Identificador"] == null
                                    ? 0
                                    : Convert.ToInt32(entity["Division"]["Identificador"])
                        };
                    item.Grupo = entity["Grupo"] == null
                        ? new UiGrupo()
                        : new UiGrupo
                        {
                            Identificador =
                                entity["Grupo"]["Identificador"] == null
                                    ? 0
                                    : Convert.ToInt32(entity["Grupo"]["Identificador"])
                        };
                    item.Fraccion = entity["Fraccion"] == null
                        ? new UiFracciones()
                        : new UiFracciones
                        {
                            Identificador =
                                entity["Fraccion"]["Identificador"] == null
                                    ? 0
                                    : Convert.ToInt32(entity["Fraccion"]["Identificador"])
                        };
                    item.Activo = entity["Activo"] != null && Convert.ToBoolean(entity["Activo"]);
                }
            }

            return item;
        }

        public List<UiInstalacion> InstalacionObtenerPorIdSolicitud(UiSolicitud solicitud)
        {
            var list = new List<UiInstalacion>();

            var response = ServiceClient.Client.PostAsJsonAsync(
                $"{urlApiSolicitud}{MethodApiSolicitud.ServiciosInstalacionObtenerPorIdCliente}",
                new
                {
                    Item = solicitud,
                    Paging = new
                    {
                        All = true,
                        CurrentPage = 1,
                        Rows = 1000,
                        pages = 1
                    }
                });

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    var entity = jResult["List"];
                    list = entity.Children().Select(p =>
                    {
                        var instalacion = new UiInstalacion();
                        instalacion.Identificador = p["Identificador"] == null ? 0 : Convert.ToInt32(p["Identificador"]);
                        instalacion.IdCliente = p["IdCliente"] == null ? 0 : Convert.ToInt32(p["IdCliente"]);
                        instalacion.Nombre = p["Nombre"]?.ToString() ?? "";
                        instalacion.Activo = p["Activo"] != null && Convert.ToBoolean(p["Activo"]);
                        instalacion.Zona = p["Zona"] != null
                            ? new UiZona
                            {
                                IdZona = p["Zona"]["Identificador"] == null ? 0 : Convert.ToInt32(p["Zona"]["Identificador"]),
                                Nombre = p["Zona"]["Nombre"].ToString(),
                                Vigente = p["Zona"]["Vigente"] != null && Convert.ToBoolean(p["Zona"]["Vigente"])
                            }
                            : new UiZona();
                        instalacion.Estacion = p["Estacion"] != null
                            ? new UiEstaciones
                            {
                                IdEstacion = Convert.ToInt32(p["Estacion"]["Identificador"]),
                                Nombre = p["Estacion"]["Nombre"].ToString()
                            }
                            : new UiEstaciones();
                        instalacion.TipoInstalacion = p["TipoInstalacion"] != null
                        ? new UiTipoInstalacion
                        {
                            Identificador =
                                p["TipoInstalacion"]["Identificador"] == null
                                    ? 0
                                    : Convert.ToInt32(p["TipoInstalacion"]["Identificador"]),
                            Nombre = p["TipoInstalacion"]["Nombre"]?.ToString() ?? string.Empty
                        }
                        : new UiTipoInstalacion();
                        instalacion.Asentamiento = p["Asentamiento"] == null
                        ? new UiAsentamiento()
                        : new UiAsentamiento
                        {
                            Estado = p["Asentamiento"]["Estado"] == null
                                ? new UiEstado()
                                : new UiEstado
                                {
                                    Identificador =
                                        p["Asentamiento"]["Estado"]["Identificador"] == null
                                            ? 0
                                            : Convert.ToInt32(p["Asentamiento"]["Estado"]["Identificador"]),
                                    Nombre = p["Asentamiento"]["Estado"]["Nombre"] == null
                                        ? ""
                                        : Convert.ToString(p["Asentamiento"]["Estado"]["Nombre"])
                                },

                            Municipio = p["Asentamiento"]["Municipio"] == null
                                ? new UiMunicipio()
                                : new UiMunicipio
                                {
                                    Identificador =
                                        p["Asentamiento"]["Municipio"]["Identificador"] == null
                                            ? 0
                                            : Convert.ToInt32(p["Asentamiento"]["Municipio"]["Identificador"]),
                                    Nombre =
                                        p["Asentamiento"]["Municipio"]["Nombre"] == null
                                            ? ""
                                            : Convert.ToString(p["Asentamiento"]["Municipio"]["Nombre"])
                                }
                        };

                        instalacion.Activo = p["Activo"] != null && Convert.ToBoolean(p["Activo"]);
                        instalacion.Seleccionado = p["Seleccionado"] != null && Convert.ToBoolean(p["Seleccionado"]);

                        return instalacion;
                    }).ToList();

                }
            }


            return list;
        }

        public List<UiCliente> InstalacionObtenerTodos(int page, int rows)
        {
            var list = new List<UiCliente>();
            var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.InstalacionObtenerTodos}",
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

            list = jResult["List"].Children().Select(t => new UiCliente
            {
                Identificador = Convert.ToInt32(t["Identificador"]),

                RFC = t["Rfc"]?.ToString() ?? "",

                RazonSocial = t["RazonSocial"]?.ToString() ?? "",

                NombreCorto = t["NombreCorto"]?.ToString() ?? "",

                Instalaciones = t["Instalaciones"]?.Select(i => new UiInstalacion
                {
                    Identificador = i["Identificador"] == null ? 0 : Convert.ToInt32(i["Identificador"]),

                    Zona = i["Zona"] != null ? new UiZona
                    {
                        IdZona = i["Zona"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Zona"]["Identificador"]),
                        Nombre = i["Zona"]["Nombre"]?.ToString() ?? ""
                    } : new UiZona(),

                    Estacion = i["Estacion"] != null ? new UiEstaciones
                    {
                        IdEstacion = i["Estacion"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Estacion"]["Identificador"]),
                        Nombre = (i["Estacion"]["Nombre"]).ToString()
                    } : new UiEstaciones(),

                    Nombre = i["Nombre"]?.ToString() ?? "",

                    TipoInstalacion = i["TipoInstalacion"] != null ? new UiTipoInstalacion
                    {
                        Identificador = i["TipoInstalacion"]["Identificador"] == null ? 0 : Convert.ToInt32(i["TipoInstalacion"]["Identificador"])
                    } : new UiTipoInstalacion(),

                    TelefonosInstalacion = i["TelefonosInstalacion"]?.Select(tel => new UiTelefonoInstalacion
                    {
                        TipoTelefono = tel["TipoTelefono"] != null ? new UiTipoTelefono
                        {
                            Identificador = Convert.ToInt32(tel["TipoTelefono"]["Identificador"])
                        } : new UiTipoTelefono(),

                        Numero = tel["Numero"]?.ToString() ?? "",

                        IsActive = tel["Activo"] != null && Convert.ToBoolean(tel["Activo"])

                    }).ToList() ?? new List<UiTelefonoInstalacion>(),

                    CorreosInstalacion = i["CorreosInstalacion"]?.Select(cor => new UiCorreoInstalacion
                    {
                        Identificador = cor["Identificador"] != null ? Convert.ToInt32(cor["Identificador"]) : 0,

                        CorreoElectronico = cor["CorreoElectronico"]?.ToString() ?? "",

                        IsActive = cor["Activo"] != null && Convert.ToBoolean(cor["Activo"])

                    }).ToList() ?? new List<UiCorreoInstalacion>(),

                    FechaInicio = i["FechaInicio"] != null ? new DateTime() : Convert.ToDateTime(i["FechaInicio"]),

                    FechaFin = i["FechaFin"] != null ? new DateTime() : Convert.ToDateTime(i["FechaFin"]),

                    Calle = i["Calle"]?.ToString() ?? "",

                    NumInterior = i["NumInterior"]?.ToString() ?? "",

                    NumExterior = i["NumExterior"]?.ToString() ?? "",

                    Referencia = i["Referencia"]?.ToString() ?? "",

                    Colindancia = i["Colindancia"]?.ToString() ?? "",

                    Asentamiento = i["Asentamiento"] == null ? new UiAsentamiento() : new UiAsentamiento
                    {
                        Nombre = i["Asentamiento"]["Nombre"]?.ToString() ?? "",

                        CodigoPostal = i["Asentamiento"]["CodigoPostal"]?.ToString() ?? "",

                        Estado = i["Asentamiento"]["Estado"] == null ? new UiEstado() : new UiEstado
                        {
                            Identificador = i["Asentamiento"]["Estado"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Asentamiento"]["Estado"]["Identificador"]),
                            Nombre = i["Asentamiento"]["Estado"]["Nombre"]?.ToString() ?? ""
                        },

                        Municipio = i["Asentamiento"]["Municipio"] == null ? new UiMunicipio() : new UiMunicipio
                        {
                            Identificador = i["Asentamiento"]["Municipio"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Asentamiento"]["Municipio"]["Identificador"]),
                            Nombre = i["Asentamiento"]["Municipio"]["Nombre"]?.ToString() ?? ""
                        }
                    },

                    Latitud = i["Latitud"]?.ToString() ?? "",

                    Longitud = i["Longitud"]?.ToString() ?? "",

                    Divicion = i["Division"] == null ? new UiDivision() : new UiDivision
                    {
                        Identificador = i["Division"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Division"]["Identificador"])
                    },

                    Grupo = i["Grupo"] == null ? new UiGrupo() : new UiGrupo
                    {
                        Identificador = i["Grupo"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Grupo"]["Identificador"])
                    },

                    Fraccion = i["Fraccion"] == null ? new UiFracciones() : new UiFracciones
                    {
                        Identificador = i["Fraccion"]["Identificador"] == null ? 0 : Convert.ToInt32(i["Fraccion"]["Identificador"])
                    },

                    Activo = i["Activo"] != null && Convert.ToBoolean(i["Activo"])

                }).ToList() ?? new List<UiInstalacion>()
            }).ToList();

            pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));

            return list;
        }
        #endregion        

        #region TIPO_INSTALACIONES

        public List<UiTipoInstalacion> ObtenerTipoInstalacion(int page, int rows)
        {
            List<UiTipoInstalacion> list = new List<UiTipoInstalacion>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.TipoInstalacionObtenerTodos}",
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
                    list = jResult["List"].Children().Select(t => new UiTipoInstalacion
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString()
                    }).OrderBy(t => t.Identificador).ToList();
                }
            }
            return list;
        }

        #endregion

        #region Solicitud

        public bool ActualizarSolicitudAcuerdos(Models.Solicitud.UiSolicitud model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesActualizarAcuerdos}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Cliente = new
                        {
                            Identificador = model.Cliente.Identificador
                        },
                        TipoSolicitud = new
                        {
                            Identificador = model.Identificador
                        },
                        Documento = new
                        {
                            ArchivoId = model.Documento.Identificador
                        },
                        Minuta = model.Minuta,
                        Folio = model.Folio,
                        Servicios = model.Servicios != null ? model.Servicios.Select(serv => new
                        {
                            Identificador = serv.Identificador,
                            TieneComite = serv.TieneComite,
                            Observaciones = serv.Observaciones,
                            Viable = serv.Viable,
                            FechaComite = serv.FechaComite,
                            Acuerdos = serv.Acuerdos != null ? serv.Acuerdos.Select(acue => new
                            {
                                IdentificadorPadre = serv.Identificador,
                                Identificador = acue.Identificador,
                                Fecha = acue.Fecha,
                                Convenio = acue.Convenio,
                                Responsable = acue.Responsable,
                                IsActive = acue.IsActive
                            }) : null,
                            Asistente = serv.Asistentes != null ? serv.Asistentes.Select(asist => new
                            {
                                Identificador = asist.Identificador,
                                IdPersona = asist.idPersona,
                                Activo = asist.Activo,
                                IdentificadorPadre = serv.Identificador
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

        public async Task<ResultPage<Models.Solicitud.UiSolicitud>> Guardar(Models.Solicitud.UiSolicitud model)
        {
            var peticion = await ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudGuardar}", model);
            var resultados = await peticion.Content.ReadAsStringAsync();
            var resultado = await peticion.Content.ReadAsAsync<ResultPage<Models.Solicitud.UiSolicitud>>();

            return resultado;
        }

        public Models.Solicitud.UiSolicitud ObtenerSolicitudAcuerdosPorId(int identificador)
        {
            Models.Solicitud.UiSolicitud result = new Models.Solicitud.UiSolicitud();

            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesObtenerConAcuerdosPorIdSolicitud}",
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
                    result.Identificador = Convert.ToInt32(jResult["Entity"]["Identificador"]);
                    result.Cliente = new UiCliente() { Identificador = Convert.ToInt32(jResult["Entity"]["Cliente"]["Identificador"]), RazonSocial = jResult["Entity"]["Cliente"]["RazonSocial"].ToString() };
                    result.TipoSolicitud = new UiTipoSolicitud() { Identificador = Convert.ToInt32(jResult["Entity"]["TipoSolicitud"]["Identificador"]), Descripcion = jResult["Entity"]["TipoSolicitud"]["Descripcion"].ToString() };
                    result.Documento = new UiDocumento() { Identificador = Convert.ToInt32(jResult["Entity"]["Documento"]["ArchivoId"]) };
                    result.Folio = Convert.ToInt32(jResult["Entity"]["Folio"]);
                    result.FechaRegistro = Convert.ToDateTime(jResult["Entity"]["FechaRegistro"]);
                    result.Servicios = jResult["Entity"]["Servicios"].Children().Select((serv, index) => new UiServicio
                    {
                        Identificador = Convert.ToInt32(serv["Identificador"]),
                        TipoServicio = new UiTipoServicio() { Identificador = Convert.ToInt32(serv["TipoServicio"]["Identificador"]), Nombre = serv["TipoServicio"]["Nombre"].ToString() },
                        Cuota = new UiCuota() { Identificador = Convert.ToInt32(serv["Cuota"]["Identificador"]) },
                        TipoInstalacionesCapacitacion = new UiTipoInstalacionesCapacitacion() { Identificador = Convert.ToInt32(serv["TipoInstalacionesCapacitacion"]["Identificador"]), Nombre = serv["TipoInstalacionesCapacitacion"]["Nombre"].ToString() },
                        Integrantes = Convert.ToInt32(serv["Integrantes"]),
                        HorasCurso = Convert.ToInt32(serv["HorasCurso"]),
                        FechaInicial = Convert.ToDateTime(serv["FechaInicial"]),
                        FechaFinal = Convert.ToDateTime(serv["FechaFinal"]),
                        Observaciones = serv["Observaciones"].ToString(),
                        TieneComite = Convert.ToBoolean(serv["TieneComite"]),
                        ObservacionesComite = serv["ObservacionesComite"].ToString(),
                        BienCustodia = serv["BienCustodia"].ToString(),
                        FechaComite = Convert.ToDateTime(serv["FechaComite"]),
                        Viable = Convert.ToBoolean(serv["Viable"]),
                        Acuerdos = serv["Acuerdos"].Children().Select((acue, idx) => new UiAcuerdo
                        {
                            Identificador = Convert.ToInt32(acue["Identificador"]),
                            Convenio = acue["Convenio"].ToString(),
                            Fecha = Convert.ToDateTime(acue["Fecha"]),
                            Responsable = Guid.Parse(acue["Responsable"].ToString()),
                            IsActive = Convert.ToBoolean(acue["IsActive"])
                        }).ToList()
                    }).ToList();
                    result.Minuta = !String.IsNullOrEmpty(jResult["Entity"]["Minuta"].ToString()) ? Convert.ToInt32(jResult["Entity"]["Minuta"]) : 0;
                    result.Cancelado = !String.IsNullOrEmpty(jResult["Entity"]["Cancelado"].ToString()) ? Convert.ToBoolean(jResult["Entity"]["Minuta"]) : false;
                }
            }
            return result;
        }

        public Models.Solicitud.UiSolicitud SolicitudObtenerDetallePorId(int IdSolicitud)
        {
            Models.Solicitud.UiSolicitud result = new Models.Solicitud.UiSolicitud();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ObtenerConDetalleServiciosClientePorIdSolicitud}",
                 new
                 {
                     Item = new
                     {
                         Identificador = IdSolicitud
                     }
                 });

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    result = new Models.Solicitud.UiSolicitud
                    {
                        Identificador = Convert.ToInt32(jResult["Entity"]["Identificador"]),
                        Folio = Convert.ToInt32(jResult["Entity"]["Folio"]),
                        TipoSolicitud = new UiTipoSolicitud()
                        {
                            Identificador = Convert.ToInt32(jResult["Entity"]["TipoSolicitud"]["Identificador"]),
                            Nombre = jResult["Entity"]["TipoSolicitud"]["Nombre"].ToString()
                        },
                        DocumentoSoporte = Convert.ToInt32(jResult["Entity"]["DocumentoSoporte"]),
                        FechaRegistro = Convert.ToDateTime(jResult["Entity"]["FechaRegistro"]),
                        Cancelado = Convert.ToBoolean(jResult["Entity"]["Cancelado"]),
                        Cliente = new UiCliente()
                        {
                            Identificador = Convert.ToInt32(jResult["Entity"]["Cliente"]["Identificador"]),
                            IdRegimenFiscal = Convert.ToInt32(jResult["Entity"]["Cliente"]["RegimenFiscal"]["Identificador"]),
                            RegimenFiscal = jResult["Entity"]["Cliente"]["RegimenFiscal"]["Descripcion"].ToString(),
                            IdSector = Convert.ToInt32(jResult["Entity"]["Cliente"]["Sector"]["Identificador"]),
                            Sector = jResult["Entity"]["Cliente"]["Sector"]["Descripcion"].ToString(),
                            RazonSocial = jResult["Entity"]["Cliente"]["RazonSocial"].ToString(),
                            NombreCorto = jResult["Entity"]["Cliente"]["NombreCorto"].ToString(),
                            RFC = jResult["Entity"]["Cliente"]["Rfc"].ToString(),
                            Solicitantes = jResult["Entity"]["Cliente"]["Solicitantes"].Children().Select((p, index) => new UiSolicitante
                            {
                                Identificador = Convert.ToInt32(p["Identificador"]),
                                IdCliente = Convert.ToInt32(jResult["Entity"]["Cliente"]["Identificador"]),
                                IdTipoPersona = Convert.ToInt32(p["IdTipoPersona"]),
                                Nombre = p["Nombre"].ToString(),
                                ApellidoPaterno = p["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = p["ApellidoMaterno"].ToString(),
                                Cargo = p["Cargo"].ToString(),
                                IsActive = Convert.ToBoolean(p["Activo"]),
                                Telefonos = p["Telefonos"].Children().Select((q, idx) => new UiTelefonoContacto
                                {
                                    Indice = idx,
                                    Identificador = Convert.ToInt32(q["Identificador"]),
                                    IdExterno = Convert.ToInt32(p["Identificador"]),
                                    IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                                    TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                                    Numero = q["Numero"].ToString(),
                                    Extension = q["Extension"].ToString(),
                                    IsActive = Convert.ToBoolean(q["Activo"])
                                }).ToList(),
                                Correos = p["Correos"].Children().Select((q, idx) => new UiCorreoContacto
                                {
                                    Indice = idx,
                                    Identificador = Convert.ToInt32(q["Identificador"]),
                                    IdExterno = Convert.ToInt32(p["Identificador"]),
                                    CorreoElectronico = q["CorreoElectronico"].ToString(),
                                    IsActive = Convert.ToBoolean(q["Activo"])
                                }).ToList()
                            }).ToList(),
                            Contactos = jResult["Entity"]["Cliente"]["Contactos"].Children().Select((p, index) => new UiClienteContacto
                            {
                                Numero = index,
                                Identificador = Convert.ToInt32(p["Identificador"]),
                                IdCliente = Convert.ToInt32(jResult["Entity"]["Cliente"]["Identificador"]),
                                IdTipoPersona = Convert.ToInt32(p["IdTipoPersona"]),
                                IdTipoContacto = Convert.ToInt32(p["TipoContacto"]["Identificador"]),
                                TipoContacto = p["TipoContacto"]["Nombre"].ToString(),
                                Nombre = p["Nombre"].ToString(),
                                ApellidoPaterno = p["ApellidoPaterno"].ToString(),
                                ApellidoMaterno = p["ApellidoMaterno"].ToString(),
                                Cargo = p["Cargo"].ToString(),
                                IsActive = Convert.ToBoolean(p["Activo"]),
                                Telefonos = p["Telefonos"].Children().Select((q, idx) => new UiTelefonoContacto
                                {
                                    Indice = idx,
                                    Identificador = Convert.ToInt32(q["Identificador"]),
                                    IdExterno = Convert.ToInt32(p["Identificador"]),
                                    IdTipoTelefono = Convert.ToInt32(q["TipoTelefono"]["Identificador"]),
                                    TipoTelefono = q["TipoTelefono"]["Nombre"].ToString(),
                                    Numero = q["Numero"].ToString(),
                                    Extension = q["Extension"].ToString(),
                                    IsActive = Convert.ToBoolean(q["Activo"])
                                }).ToList(),
                                Correos = p["Correos"].Children().Select((q, idx) => new UiCorreoContacto
                                {
                                    Indice = idx,
                                    Identificador = Convert.ToInt32(q["Identificador"]),
                                    IdExterno = Convert.ToInt32(p["Identificador"]),
                                    CorreoElectronico = q["CorreoElectronico"].ToString(),
                                    IsActive = Convert.ToBoolean(q["Activo"])
                                }).ToList()
                            }).ToList()
                        },
                        Servicios = jResult["Entity"]["Servicios"].Children().Select((s, index) => new UiServicio
                        {
                            Identificador = Convert.ToInt32(s["Identificador"]),
                            NumeroPersonas = Convert.ToInt32(s["NumeroPersonas"]),
                            HorasCurso = Convert.ToInt32(s["HorasCurso"]),
                            FechaInicial = Convert.ToDateTime(s["FechaInicial"]),
                            FechaFinal = Convert.ToDateTime(s["FechaFinal"]),
                            Observaciones = s["Observaciones"].ToString(),
                            BienCustodia = s["BienCustodia"].ToString(),
                            TipoServicio = new UiTipoServicio()
                            {
                                Identificador = Convert.ToInt32(s["TipoServicio"]["Identificador"]),
                                Nombre = s["TipoServicio"]["Nombre"].ToString()
                            },
                            Cuota = new UiCuota()
                            {
                                Identificador = Convert.ToInt32(s["Cuota"]["Identificador"]),
                                Concepto = s["Cuota"]["Concepto"].ToString()
                            },
                            TipoInstalacionesCapacitacion = new UiTipoInstalacionesCapacitacion()
                            {
                                Identificador = Convert.ToInt32(s["TipoInstalacionesCapacitacion"]["Identificador"]),
                                Nombre = s["TipoInstalacionesCapacitacion"]["Nombre"].ToString()
                            },
                            Documentos = s["Documentos"].Children().Select((d, idx) => new UiServicioDocumento
                            {
                                Identificador = Convert.ToInt32(d["ArchivoId"]),
                                Nombre = d["Nombre"].ToString()
                            }).ToList(),
                            Instalaciones = s["Instalaciones"].Children().Select((i, idx) => new UiInstalacion
                            {
                                NumInterior = i["NumInterior"].ToString(),
                                NumExterior = i["NumExterior"].ToString(),
                                Zona = new UiZona
                                {
                                    IdZona = Convert.ToInt32(i["Zona"]["IdZona"]),
                                    Nombre = i["Zona"]["Nombre"].ToString()
                                },
                                Estacion = new UiEstaciones
                                {
                                    Identificador = Convert.ToInt32(i["Estacion"]["Identificador"]),
                                    Nombre = i["Estacion"]["Nombre"].ToString()
                                },
                                Nombre = i["Nombre"].ToString(),
                                Asentamiento = new UiAsentamiento()
                                {
                                    Estado = new UiEstado()
                                    {
                                        Identificador = Convert.ToInt32(i["Asentamiento"]["Estado"]["Identificador"]),
                                        Nombre = i["Asentamiento"]["Estado"]["Nombre"].ToString()
                                    },
                                    Municipio = new UiMunicipio()
                                    {
                                        Identificador = Convert.ToInt32(i["Asentamiento"]["Municipio"]["Identificador"]),
                                        Nombre = i["Asentamiento"]["Municipio"]["Nombre"].ToString()
                                    }
                                }
                            }).ToList()
                        }).ToList()
                    };
                }
            }
            return result;
        }

        public List<Models.Solicitud.UiSolicitud> SolicitudObtenerPorCriterio(int page, int rows, Models.Solicitud.UiSolicitud model, DateTime? fechaRegistroMin, DateTime? fechaRegistroMax)
        {
            List<Models.Solicitud.UiSolicitud> list = new List<Models.Solicitud.UiSolicitud>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Folio = model.Folio.ToString(),
                        Servicios = model.Servicios,
                        RazonSocial = model.Cliente.RazonSocial,
                        NombreCorto = model.Cliente.NombreCorto,
                        RFC = model.Cliente.RFC,
                        Cancelado = model.Cancelado
                    },
                    Paging = new
                    {
                        All = false,
                        CurrentPage = page,
                        Rows = rows
                    },
                    DTO = new
                    {
                        FechaRegistroMin = fechaRegistroMin,
                        FechaRegistroMax = fechaRegistroMax
                    }
                });

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(sol => new Models.Solicitud.UiSolicitud
                    {
                        Identificador = Convert.ToInt32(sol["Identificador"]),
                        Cliente = new UiCliente()
                        {
                            Identificador = Convert.ToInt32(sol["Cliente"]["Identificador"]),
                            RazonSocial = sol["Cliente"]["RazonSocial"].ToString(),
                            NombreCorto = sol["Cliente"]["NombreCorto"].ToString(),
                            RFC = sol["Cliente"]["RFC"].ToString(),
                            IdRegimenFiscal = Convert.ToInt32(sol["Cliente"]["RegimenFiscal"]["Identificador"]),
                            RegimenFiscal = sol["Cliente"]["RegimenFiscal"]["Nombre"].ToString(),
                            IdSector = Convert.ToInt32(sol["Cliente"]["Sector"]["Identificador"]),
                            Sector = sol["Cliente"]["Sector"]["Descripcion"].ToString()
                        },
                        TipoSolicitud = new UiTipoSolicitud() { Identificador = Convert.ToInt32(sol["TipoSolicitud"]["Identificador"]), Nombre = sol["TipoSolicitud"]["Nombre"].ToString() },
                        DocumentoSoporte = Convert.ToInt32(sol["DocumentoSoporte"]),
                        Folio = Convert.ToInt32(sol["Folio"].ToString()),
                        FechaRegistro = Convert.ToDateTime(sol["FechaRegistro"].ToString()),
                        Minuta = !String.IsNullOrEmpty(sol["Minuta"].ToString()) ? Convert.ToInt32(sol["Minuta"]) : 0,
                        Cancelado = !String.IsNullOrEmpty(sol["Cancelado"].ToString()) ? Convert.ToBoolean(sol["Cancelado"]) : false
                    }).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<Models.Solicitud.UiSolicitud> SolicitudObtenerPorIdCliente(int IdCliente)
        {
            List<Models.Solicitud.UiSolicitud> list = new List<Models.Solicitud.UiSolicitud>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.SolicitudesObtenerPorId}",
                 new
                 {
                     IdCliente = IdCliente
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new Models.Solicitud.UiSolicitud
                    {
                        Identificador = Convert.ToInt32(t["Identificador"])
                    }).ToList();
                }
            }
            return list;
        }
        #endregion


        #region Servicios Solicitud.

        public List<Models.Solicitud.UiSolicitud> ServiciObtener(int page, int rows, string razonSocial = "", string nombreCorto = "", string rfc = "", int? estatus = null)
        {

            var list = new List<UiSolicitud>();
            var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ServiciosObtener}",
                new
                {
                    Paging = new
                    {
                        All = true,
                        CurrentPage = page,
                        Rows = rows,
                        RazonSocial = razonSocial,
                        NombreCorto = nombreCorto,
                        Rfc = rfc,
                        Estatus = estatus
                    }
                });

            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;

                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(sol =>
                    {
                        var solicitud =
                            new UiSolicitud
                            {
                                Identificador = Convert.ToInt32(sol["Identificador"]),
                                Cliente = new UiCliente
                                {
                                    NombreCorto = sol["Cliente"]["NombreCorto"].ToString(),
                                    RFC = sol["Cliente"]["Rfc"].ToString()
                                },
                                Servicios = sol["Servicios"].Children().Select(
                                    s =>
                                    {
                                        var servicio = new UiServicio
                                        {
                                            Identificador = Convert.ToInt32(s["Identificador"]),
                                            TipoServicio = new UiTipoServicio
                                            {
                                                Identificador = Convert.ToInt32(s["TipoServicio"]["Identificador"]),
                                                Nombre = s["TipoServicio"]["Nombre"].ToString()
                                            }
                                        };
                                        return servicio;
                                    }).ToList()
                            };
                        return solicitud;
                    }).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiSolicitud> ServicioObtenerPorCriterio(int page, int rows, UiSolicitud solicitudCriterio)
        {
            var list = new List<UiSolicitud>();
            if (solicitudCriterio.Cliente != null)
            {
                var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ServiciosObtenerPorCriterio}",
                    new
                    {
                        Paging = new
                        {
                            All = true,
                            CurrentPage = page,
                            Rows = rows
                        },
                        Item = new
                        {
                            Identificador = solicitudCriterio.Identificador,
                            Cliente = new
                            {
                                RazonSocial = solicitudCriterio.Cliente?.RazonSocial,
                                NombreCorto = solicitudCriterio.Cliente?.NombreCorto,
                                Rfc = solicitudCriterio.Cliente?.RFC
                            },
                            Servicio = new
                            {
                                Estatus = new { Identificador = solicitudCriterio.Servicios?.FirstOrDefault()?.Estatus?.Identificador }
                            }
                        }
                    });

                if (response.Result.IsSuccessStatusCode)
                {
                    var query = response.Result.Content.ReadAsStringAsync().Result;

                    var jResult = JObject.Parse(query);
                    if (Convert.ToBoolean(jResult["Success"]))
                    {
                        list = jResult["List"].Children().Select(sol =>
                        {
                            var solicitud =
                                new Models.Solicitud.UiSolicitud
                                {
                                    Identificador = Convert.ToInt32(sol["Identificador"]),
                                    Cliente = new UiCliente
                                    {
                                        NombreCorto = sol["Cliente"]["NombreCorto"].ToString(),
                                        RFC = sol["Cliente"]["Rfc"].ToString()
                                    },
                                    Servicios = sol["Servicios"].Children().Select(
                                        s =>
                                        {
                                            var servicio = new UiServicio
                                            {
                                                Identificador = Convert.ToInt32(s["Identificador"]),
                                                TipoServicio = new UiTipoServicio
                                                {
                                                    Identificador = Convert.ToInt32(s["TipoServicio"]["Identificador"]),
                                                    Nombre = s["TipoServicio"]["Nombre"].ToString()
                                                }
                                            };
                                            return servicio;
                                        }).ToList()
                                };
                            return solicitud;
                        }).ToList();

                        pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Obtiene los datos de un servicio.
        /// </summary>
        /// <param name="identificador">Identificador del servicio a editar</param>
        /// <returns></returns>
        public UiSolicitud ServicioSolicitudObtenerPorId(long identificador)
        {
            var result = new UiSolicitud();
            //var response = ServiceClient.Client.PostAsJsonAsync($"{urlApiSolicitud}{MethodApiSolicitud.ServicioObtenerPorId}",
            //    new
            //    {
            //        Item = new
            //        {
            //            Identificador = identificador
            //        }
            //    });

            if (true)//response.Result.IsSuccessStatusCode)
            {
                //var query = response.Result.Content.ReadAsStringAsync().Result;

                //var jResult = JObject.Parse(query);
                if (true)//Convert.ToBoolean(jResult["Success"]))
                {
                    result = new UiSolicitud();

                    result.Identificador = 4013;
                    result.Cliente = new UiCliente
                    {
                        Identificador = 23,
                        RazonSocial = "Comisión Nacional del Agua",
                        RFC = "CON010101FD1"
                    };
                    result.Servicio = new UiServicio()
                    {
                        TipoServicio = new UiTipoServicio()
                        {
                            Nombre = "Servicios de Seguridad - Custodia de personas"
                        },
                        Documentos = new List<UiServicioDocumento>()
                        {
                            new UiServicioDocumento()
                            {
                                Nombre = "Documento 1",
                                Extension = "pdf",
                                FechaRegistro = DateTime.Now,
                                Observaciones ="Ninguna"
                            },
                            new UiServicioDocumento()
                            {
                                Nombre = "Documento 2",
                                Extension = "pdf",
                                FechaRegistro = DateTime.Now,
                                Observaciones ="Ninguna"
                            },
                            new UiServicioDocumento()
                            {
                                Nombre = "Documento 3",
                                Extension = "pdf",
                                FechaRegistro = DateTime.Now,
                                Observaciones ="Ninguna"
                            }
                        },
                        ServicioInstalaciones = new List<UiServicioInstalacion>() {
                            new UiServicioInstalacion()
                            {
                                Identificador = 1,
                                Nombre = "Instalacion 1",
                                DireccionCompleta = "Av. Miguel Angel de Quevedo No. 915, Coyoacán, El Rosedal, 04330 Ciudad de México, CDMX",
                                Fraccion = new UiFracciones()
                                {
                                    Nombre = "Fracción 1",
                                    Grupo = "Grupo 12",
                                    Division ="División 5",
                                    Descripcion = "COMPRENDE A LAS EMPRESAS QUE REALIZAN TRABAJOS AGRÍCOLAS, FLORICULTURA, FRUTICULTURA, HORTICULTURA, JARDINERÍA   ORNAMENTAL, YA SEA QUE SE REALICEN INTRAMUROS O BAJO TECHO EN INVERNÁCULOS O VIVEROS, ASÍ COMO AQUELLAS EMPRESAS QUE PRESTAN SERVICIOS TALES COMO   PREPARACIÓN DE LA TIERRA, DESMONTE, CULTIVO, COSECHA, EMPAQUE, FERTILIZACIÓN (SIN EMPLEO DE AERONAVES), DESPEPITE DE ALGODÓN, OPERACIÓN DE SISTEMAS DE    RIEGO Y OTROS. EXCEPTO LA FUMIGACIÓN CLASIFICADA EN LAS FRACCIONES 899 Y 8910 Y LA FERTILIZACIÓN CON AERONAVES CLASIFICADAS EN LA 8910"
                                },
                                FactorActividad = new UiFactor { Nombre = "Factor Actividad"},
                                FactorDistancia = new UiFactor { Nombre = "Factor Distancia"},
                                FactorCriminalidad = new UiFactor { Nombre = "Factor Criminalidad"},
                                FechaInicio = DateTime.Now,
                                FechaFin =DateTime.Now.AddDays(8),
                                CotizacionDetalle = new UiCotizacionDetalle()
                                {
                                    Couta= new UiCuota(),
                                    Turno=new UiTurno(),
                                    Cantidad=1,
                                    Descripcion="",
                                    FechaInicio=DateTime.Now,
                                    FechaFin=DateTime.Now.AddDays(8),
                                    Cancelado=false,
                                    AplicaFli=false,
                                    Lunes     =true,
                                    Martes    =true,
                                    Miercoles =true,
                                    Jueves    =true,
                                    Viernes   =true,
                                    Sabado    =true,
                                    Domingo   =true,
                                    EstadoDeFuerza = new UiEstadoDeFuerza(),
                                    GastoInherentes = new List<UiGastosInherente>()
                                }
                            },
                            new UiServicioInstalacion()
                            {
                                Identificador = 1,
                                Nombre = "Instalacion 2",
                                DireccionCompleta = "Av. Miguel Angel de Quevedo No. 915, Coyoacán, El Rosedal, 04330 Ciudad de México, CDMX",
                                Fraccion = new UiFracciones()
                                {
                                    Nombre = "Fracción 1",
                                    Grupo = "Grupo 12",
                                    Division ="División 5",
                                    Descripcion = "COMPRENDE A LAS EMPRESAS QUE REALIZAN TRABAJOS AGRÍCOLAS, FLORICULTURA, FRUTICULTURA, HORTICULTURA, JARDINERÍA   ORNAMENTAL, YA SEA QUE SE REALICEN INTRAMUROS O BAJO TECHO EN INVERNÁCULOS O VIVEROS, ASÍ COMO AQUELLAS EMPRESAS QUE PRESTAN SERVICIOS TALES COMO   PREPARACIÓN DE LA TIERRA, DESMONTE, CULTIVO, COSECHA, EMPAQUE, FERTILIZACIÓN (SIN EMPLEO DE AERONAVES), DESPEPITE DE ALGODÓN, OPERACIÓN DE SISTEMAS DE    RIEGO Y OTROS. EXCEPTO LA FUMIGACIÓN CLASIFICADA EN LAS FRACCIONES 899 Y 8910 Y LA FERTILIZACIÓN CON AERONAVES CLASIFICADAS EN LA 8910"
                                },
                                FactorActividad = new UiFactor { Nombre = "Factor Actividad"},
                                FactorDistancia = new UiFactor { Nombre = "Factor Distancia"},
                                FactorCriminalidad = new UiFactor { Nombre = "Factor Criminalidad"},
                                FechaInicio = DateTime.Now,
                                FechaFin =DateTime.Now.AddDays(8)
                            },
                            new UiServicioInstalacion()
                            {
                                Identificador = 1,
                                Nombre = "Instalacion 3",
                                DireccionCompleta = "Av. Miguel Angel de Quevedo No. 915, Coyoacán, El Rosedal, 04330 Ciudad de México, CDMX",
                                Fraccion = new UiFracciones()
                                {
                                    Nombre = "Fracción 1",
                                    Grupo = "Grupo 12",
                                    Division ="División 5",
                                    Descripcion = "COMPRENDE A LAS EMPRESAS QUE REALIZAN TRABAJOS AGRÍCOLAS, FLORICULTURA, FRUTICULTURA, HORTICULTURA, JARDINERÍA   ORNAMENTAL, YA SEA QUE SE REALICEN INTRAMUROS O BAJO TECHO EN INVERNÁCULOS O VIVEROS, ASÍ COMO AQUELLAS EMPRESAS QUE PRESTAN SERVICIOS TALES COMO   PREPARACIÓN DE LA TIERRA, DESMONTE, CULTIVO, COSECHA, EMPAQUE, FERTILIZACIÓN (SIN EMPLEO DE AERONAVES), DESPEPITE DE ALGODÓN, OPERACIÓN DE SISTEMAS DE    RIEGO Y OTROS. EXCEPTO LA FUMIGACIÓN CLASIFICADA EN LAS FRACCIONES 899 Y 8910 Y LA FERTILIZACIÓN CON AERONAVES CLASIFICADAS EN LA 8910"
                                },
                                FactorActividad = new UiFactor { Nombre = "Factor Actividad"},
                                FactorDistancia = new UiFactor { Nombre = "Factor Distancia"},
                                FactorCriminalidad = new UiFactor { Nombre = "Factor Criminalidad"},
                                FechaInicio = DateTime.Now,
                                FechaFin =DateTime.Now.AddDays(8)
                            },
                            new UiServicioInstalacion()
                            {
                                Identificador = 1,
                                Nombre = "Instalacion 4",
                                DireccionCompleta = "Av. Miguel Angel de Quevedo No. 915, Coyoacán, El Rosedal, 04330 Ciudad de México, CDMX",
                                Fraccion = new UiFracciones()
                                {
                                    Nombre = "Fracción 1",
                                    Grupo = "Grupo 12",
                                    Division ="División 5",
                                    Descripcion = "COMPRENDE A LAS EMPRESAS QUE REALIZAN TRABAJOS AGRÍCOLAS, FLORICULTURA, FRUTICULTURA, HORTICULTURA, JARDINERÍA   ORNAMENTAL, YA SEA QUE SE REALICEN INTRAMUROS O BAJO TECHO EN INVERNÁCULOS O VIVEROS, ASÍ COMO AQUELLAS EMPRESAS QUE PRESTAN SERVICIOS TALES COMO   PREPARACIÓN DE LA TIERRA, DESMONTE, CULTIVO, COSECHA, EMPAQUE, FERTILIZACIÓN (SIN EMPLEO DE AERONAVES), DESPEPITE DE ALGODÓN, OPERACIÓN DE SISTEMAS DE    RIEGO Y OTROS. EXCEPTO LA FUMIGACIÓN CLASIFICADA EN LAS FRACCIONES 899 Y 8910 Y LA FERTILIZACIÓN CON AERONAVES CLASIFICADAS EN LA 8910"
                                },
                                FactorActividad = new UiFactor { Nombre = "Factor Actividad"},
                                FactorDistancia = new UiFactor { Nombre = "Factor Distancia"},
                                FactorCriminalidad = new UiFactor { Nombre = "Factor Criminalidad"},
                                FechaInicio = DateTime.Now,
                                FechaFin =DateTime.Now.AddDays(8)
                            }
                        },
                        NumeroPersonas = 80
                    };
                }
            }
            return result;

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