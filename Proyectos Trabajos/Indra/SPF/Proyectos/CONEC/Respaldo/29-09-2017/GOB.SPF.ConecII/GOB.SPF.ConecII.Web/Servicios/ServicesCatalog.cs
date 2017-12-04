using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesCatalog : IService
    {
        private int pages { get; set; }

        internal int Pages { get { return pages; } }

        public HttpClient client;
        string apiCatalogClient;
        public ServicesCatalog()
        {
            apiCatalogClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiCatalog];
            client = new HttpClient();
        }

        #region ROLESMODULOSCONTROL
        public List<UiRolModuloControl> ObtenerRolesModulosControl(int page, int rows)
        {
            List<UiRolModuloControl> list = new List<UiRolModuloControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosControlObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiRolModuloControl
                    {
                        Identificador = Convert.ToInt32(t["IdRolModuloControl"]),
                        IdRolModulo = Convert.ToInt32(t["IdRolModulo"]),
                        IdControl = Convert.ToInt32(t["IdControl"]),
                        Consulta = Convert.ToBoolean(t["Consulta"]),
                        Captura = Convert.ToBoolean(t["Capura"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiRolModuloControl> ObtenerRolesModulosControlListado()
        {
            List<UiRolModuloControl> list = new List<UiRolModuloControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosControlObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiRolModuloControl
                    {
                        Identificador = Convert.ToInt32(t["IdRolModuloControl"]),
                        IdRolModulo = Convert.ToInt32(t["IdRolModulo"]),
                        IdControl = Convert.ToInt32(t["IdControl"]),
                        Consulta = Convert.ToBoolean(t["Consulta"]),
                        Captura = Convert.ToBoolean(t["Capura"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiRolModuloControl> RolesModulosControlObtenerPorCriterio(int page, int rows, UiRolModuloControl model)
        {
            List<UiRolModuloControl> list = new List<UiRolModuloControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosControlObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        //Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiRolModuloControl
                    {
                        Identificador = Convert.ToInt32(t["IdRolModuloControl"]),
                        IdRolModulo = Convert.ToInt32(t["IdRolModulo"]),
                        IdControl = Convert.ToInt32(t["IdControl"]),
                        Captura = Convert.ToBoolean(t["Capura"]),
                        Consulta = Convert.ToBoolean(t["Consulta"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveRolesModulosControl(UiRolModuloControl model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosControlGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdRolModulo = model.IdRolModulo,
                        IdControl = model.IdControl,
                        Captura = model.Captura,
                        Consulta = model.Consulta
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

        public bool RolesModulosControlCambiarEstatus(UiRolModuloControl model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosControlActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        //Activo = model.IsActive
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

        #endregion ROLESMODULOSCONTROL

        #region TIPOCONTROL
        public List<UiTipoControl> ObtenerTiposControl(int page, int rows)
        {
            List<UiTipoControl> list = new List<UiTipoControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposControlObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiTipoControl
                    {
                        Identificador = Convert.ToInt32(t["IdTipoControl"]),
                        Nombre = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiTipoControl> ObtenerTiposControlListado()
        {
            List<UiTipoControl> list = new List<UiTipoControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposControlObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiTipoControl
                    {
                        Identificador = Convert.ToInt32(t["IdTipoControl"]),
                        Nombre = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiTipoControl> TiposControlObtenerPorCriterio(int page, int rows, UiTipoControl model)
        {
            List<UiTipoControl> list = new List<UiTipoControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposControlObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiTipoControl
                    {
                        Identificador = Convert.ToInt32(t["IdTipoControl"]),
                        Nombre = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveTiposControl(UiTipoControl model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposControlGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Nombre,
                        Activo = model.IsActive
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

        public bool TiposControlCambiarEstatus(UiTipoControl model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposControlActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion TIPOCONTROL

        #region CONTROLES
        public List<UiControl> ObtenerControles(int page, int rows)
        {
            List<UiControl> list = new List<UiControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ControlesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiControl
                    {
                        Identificador = Convert.ToInt32(t["IdControl"]),
                        IdTipoControl = Convert.ToInt32(t["IdTipoControl"]),
                        IdModulo = Convert.ToInt32(t["IdModulo"]),
                        Nombre = t["Nombre"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiControl> ObtenerControlesListado()
        {
            List<UiControl> list = new List<UiControl>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ControlesObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiControl
                    {
                        Identificador = Convert.ToInt32(t["IdControl"]),
                        IdTipoControl = Convert.ToInt32(t["IdTipoControl"]),
                        IdModulo = Convert.ToInt32(t["IdModulo"]),
                        Nombre = t["Nombre"].ToString()
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiControl> ControlesObtenerPorCriterio(int page, int rows, UiControl model)
        {
            List<UiControl> list = new List<UiControl>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ControlesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        //                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiControl
                    {
                        Identificador = Convert.ToInt32(t["IdControl"]),
                        IdTipoControl = Convert.ToInt32(t["IdTipoControl"]),
                        IdModulo = Convert.ToInt32(t["IdModulo"]),
                        Nombre = t["Nombre"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveControles(UiControl model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ControlesGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdModulo = model.IdModulo,
                        IdTipoControl = model.IdTipoControl,
                        Nombre = model.Nombre
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

        public bool ControlesCambiarEstatus(UiControl model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ControlesActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador//,
                        //Activo = model.IsActive
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

        #endregion CONTROLES

        #region ROLESMODULOS
        public List<UiRolModulo> ObtenerRolesModulos(int page, int rows)
        {
            List<UiRolModulo> list = new List<UiRolModulo>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiRolModulo
                    {
                        Identificador = Convert.ToInt32(t["IdRolModulo"]),
                        IdRol = Convert.ToInt32(t["IdRol"]),
                        IdModulo = Convert.ToInt32(t["IdModulo"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiRolModulo> ObtenerRolesModulosListado()
        {
            List<UiRolModulo> list = new List<UiRolModulo>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiRolModulo
                    {
                        Identificador = Convert.ToInt32(t["IdRolModulo"]),
                        IdRol = Convert.ToInt32(t["IdRol"]),
                        IdModulo = Convert.ToInt32(t["IdModulo"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiRolModulo> RolesModulosObtenerPorCriterio(int page, int rows, UiRolModulo model)
        {
            List<UiRolModulo> list = new List<UiRolModulo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiRolModulo
                    {
                        Identificador = Convert.ToInt32(t["IdRolModulo"]),
                        IdRol = Convert.ToInt32(t["IdRol"]),
                        IdModulo = Convert.ToInt32(t["IdModulo"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveRolesModulos(UiRolModulo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdRol = model.IdRol,
                        IdModulo = model.IdModulo
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

        public bool RolesModulosCambiarEstatus(UiRolModulo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesModulosActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion ROLESMODULOS

        #region ROLESUSUARIOS
        public List<UiRolUsuario> ObtenerRolesUsuarios(int page, int rows)
        {
            List<UiRolUsuario> list = new List<UiRolUsuario>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesUsuariosObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiRolUsuario
                    {
                        IdUsuario = Convert.ToInt32(t["IdUsuario"]),
                        IdRol = Convert.ToInt32(t["IdRol"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiRolUsuario> ObtenerRolesUsuariosListado()
        {
            List<UiRolUsuario> list = new List<UiRolUsuario>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesUsuariosObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiRolUsuario
                    {
                        IdUsuario = Convert.ToInt32(t["IdUsuario"]),
                        IdRol = Convert.ToInt32(t["IdRol"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiRolUsuario> RolesUsuariosObtenerPorCriterio(int page, int rows, UiRolUsuario model)
        {
            List<UiRolUsuario> list = new List<UiRolUsuario>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesUsuariosObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiRolUsuario
                    {
                        IdUsuario = Convert.ToInt32(t["IdUsuario"]),
                        IdRol = Convert.ToInt32(t["IdRol"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveRolesUsuarios(UiRolUsuario model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesUsuariosGuardar}",
                new
                {
                    Item = new
                    {
                        IdUsuario = model.IdUsuario,
                        IdRol = model.IdRol
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

        public bool RolesUsuariosCambiarEstatus(UiRolUsuario model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesUsuariosActualizar}",
                new
                {
                    Item = new
                    {
                        IdUsuario = model.IdUsuario,
                        IdRol = model.IdRol,
                        Activo = model.IsActive
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

        #endregion ROLESUSUARIOS

        #region USUARIOS
        public List<UiUsuario> ObtenerUsuarios(int page, int rows)
        {
            List<UiUsuario> list = new List<UiUsuario>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.UsuariosObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiUsuario
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdPersona = Convert.ToInt32(t["IdPersona"]),
                        IdExterno = Convert.ToInt32(t["IdExterno"]),
                        Login = t["Login"].ToString(),
                        Password = Convert.ToByte(t["Password"].ToString()),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiUsuario> ObtenerUsuariosListado()
        {
            List<UiUsuario> list = new List<UiUsuario>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.UsuariosObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiUsuario
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdPersona = Convert.ToInt32(t["IdPersona"]),
                        IdExterno = Convert.ToInt32(t["IdExterno"]),
                        Login = t["Login"].ToString(),
                        Password = Convert.ToByte(t["Password"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiUsuario> UsuariosObtenerPorCriterio(int page, int rows, UiUsuario model)
        {
            List<UiUsuario> list = new List<UiUsuario>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.UsuariosObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiUsuario
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdPersona = Convert.ToInt32(t["IdPersona"]),
                        IdExterno = Convert.ToInt32(t["IdExterno"]),
                        Login = t["Login"].ToString(),
                        Password = Convert.ToByte(t["Password"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveUsuarios(UiUsuario model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.UsuariosGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdPersona = model.IdPersona,
                        IdExterno = model.IdExterno,
                        Login = model.Login,
                        Password = model.Password
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

        public bool UsuariosCambiarEstatus(UiUsuario model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.UsuariosActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion USUARIOS

        #region MÓDULOS
        public List<UiModulo> ObtenerModulos(int page, int rows)
        {
            List<UiModulo> list = new List<UiModulo>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ModulosObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiModulo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        //Enlace = t["Enlace"].ToString(),
                        IdPadre = Convert.ToInt32(t["IdentificadorSubModulo"]),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiModulo> ObtenerModulosListado()
        {
            List<UiModulo> list = new List<UiModulo>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ModulosObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiModulo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        IdPadre = Convert.ToInt32(t["IdentificadorSubModulo"]),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiModulo> ModulosObtenerPorCriterio(int page, int rows, UiModulo model)
        {
            List<UiModulo> list = new List<UiModulo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ModulosObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiModulo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        IdPadre = Convert.ToInt32(t["IdentificadorSubModulo"]),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveModulos(UiModulo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ModulosGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        //Enlace = model.Enlace,
                        Descripcion = model.Descripcion,
                        IdentificadorSubModulo = model.IdPadre
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

        public bool ModulosCambiarEstatus(UiModulo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ModulosActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion MÓDULOS

        #region Roles
        public List<UiRol> ObtenerRoles(int page, int rows)
        {
            List<UiRol> list = new List<UiRol>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiRol
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdentificadorSubRol = Convert.ToInt32(t["IdentificadorSubRol"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiRol> ObtenerRolesListado()
        {
            List<UiRol> list = new List<UiRol>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiRol
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdentificadorSubRol = Convert.ToInt32(t["IdentificadorSubRol"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiRol> RolesObtenerPorCriterio(int page, int rows, UiRol model)
        {
            List<UiRol> list = new List<UiRol>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiRol
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdentificadorSubRol = Convert.ToInt32(t["IdentificadorSubRol"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveRoles(UiRol model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion,
                        IdentificadorSubRol = model.IdentificadorSubRol
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

        public bool RolCambiarEstatus(UiRol model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RolesActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion Roles

        #region DIVISIONES
        public List<UiDivision> ObtenerDivisiones(int page, int rows)
        {
            List<UiDivision> list = new List<UiDivision>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesObtener}",
                new
                {
                    Paging = new { All = true, CurrentPage = page, Rows = rows }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiDivision
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["NombreDivision"].ToString(),
                        Descripcion = t["DescDivision"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiDivision> ObtenerDivisionesListado()
        {
            List<UiDivision> list = new List<UiDivision>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesObtenerListado}",
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
                    list = jResult["List"].Children().Select(t => new UiDivision
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["NombreDivision"].ToString(),
                        Descripcion = t["DescDivision"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        public List<UiDivision> DivisionesObtenerPorCriterio(int page, int rows, UiDivision model)
        {
            List<UiDivision> list = new List<UiDivision>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiDivision
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["NombreDivision"].ToString(),
                        Descripcion = t["DescDivision"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveDivisiones(UiDivision model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        NombreDivision = model.Name,
                        DescDivision = model.Descripcion
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

        public bool DivisionCambiarEstatus(UiDivision model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion Divisiones

        #region GRUPOS
        public List<UiGrupo> GrupoObtener(int page, int rows)
        {
            List<UiGrupo> list = new List<UiGrupo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoObtener}",
                   new
                   {
                       paging = new
                       {
                           All = true,
                           CurrentPage = page,
                           Rows = rows
                       }
                   }
                );
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiGrupo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdDivision = Convert.ToInt32(t["IdDivision"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Division = t["Division"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiGrupo> GrupoObtenerPorCriterio(int page, int rows, UiGrupo model)
        {
            List<UiGrupo> list = new List<UiGrupo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador,
                        IdDivision = model.IdDivision
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
                    list = jResult["List"].Children().Select(t => new UiGrupo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdDivision = Convert.ToInt32(t["IdDivision"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Division = t["Division"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool GrupoGuardar(UiGrupo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdDivision = model.IdDivision,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion
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

        public bool GrupoActualizar(UiGrupo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        public List<UiGrupo> GrupoObtenerPorIdDivisionFraccion(UiFracciones model)
        {
            List<UiGrupo> list = new List<UiGrupo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoObtenerPorIdDivision}",
                new
                {
                    Item = new
                    {
                        IdDivision = model.IdDivision
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiGrupo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                }
            }
            return list;

        }

        #endregion

        /// <summary>
        /// /////////////////////////Integracion 18 08 2017/////////////////////////////////////////
        /// JZR
        /// </summary>
        /// 

        #region TIPOS_SERVICIO

        public List<UiTiposServicio> ObtenerTodosTiposServicio(int page, int rows)
        {
            List<UiTiposServicio> list = new List<UiTiposServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposServicioObtener}",
                   new
                   {
                       paging = new
                       {
                           All = true,
                           CurrentPage = page,
                           Rows = rows
                       }
                   }
                );
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiTiposServicio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Clave = t["Clave"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiTiposServicio> ObtenerTiposServicio(int page, int rows)
        {
            List<UiTiposServicio> list = new List<UiTiposServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposServicioObtener}",
                   new
                   {
                       paging = new
                       {
                           All = false,
                           CurrentPage = page,
                           Rows = rows
                       }
                   }
                );
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiTiposServicio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Clave = t["Clave"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiTiposServicio> TiposServicioObtenerPorCriterio(int page, int rows, UiTiposServicio model)
        {
            List<UiTiposServicio> list = new List<UiTiposServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposServicioObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiTiposServicio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Clave = t["Clave"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveTiposServicio(UiTiposServicio model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposServicioGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion,
                        Clave = model.Clave
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

        public bool CambiarEstatusTiposServicio(UiTiposServicio model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposServicioCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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
            /*
             bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesActualizar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

             */

        }

        #endregion

        #region CLASIFICACION_FACTOR

        public List<UiClasificacionFactor> ObtenerClasificacionFactor(int page, int rows)
        {
            List<UiClasificacionFactor> list = new List<UiClasificacionFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ClasificacionFactorObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiClasificacionFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiClasificacionFactor> ClasificacionFactorObtenerPorCriterio(int page, int rows, UiClasificacionFactor model)
        {
            List<UiClasificacionFactor> list = new List<UiClasificacionFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ClasificacionFactorObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiClasificacionFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveClasificacionFactor(UiClasificacionFactor model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ClasificacionFactorGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion
                    },

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

        public bool CambiarEstatusClasificacionFactor(UiClasificacionFactor model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ClasificacionFactorCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        #region DEPENDENCIAS

        public List<UiDependencias> ObtenerTodosDependencias(int page, int rows)
        {
            List<UiDependencias> list = new List<UiDependencias>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DependenciaObtener}",
                new
                {
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
                    list = jResult["List"].Children().Select(t => new UiDependencias
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiDependencias> ObtenerDependencias(int page, int rows)
        {
            List<UiDependencias> list = new List<UiDependencias>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DependenciaObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiDependencias
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiDependencias> DependenciasObtenerPorCriterio(int page, int rows, UiDependencias model)
        {
            List<UiDependencias> list = new List<UiDependencias>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DependenciaObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiDependencias
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveDependencias(UiDependencias model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DependenciaGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion
                    },

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

        public bool CambiarEstatusDependencias(UiDependencias model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DependenciaCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        #region REFERENCIAS
        public List<UiReferencia> ObtenerReferencias(int page, int rows)
        {
            List<UiReferencia> list = new List<UiReferencia>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ReferenciaObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiReferencia
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        ClaveReferencia = Convert.ToInt32(t["ClaveReferencia"]),
                        Descripcion = t["Descripcion"].ToString(),
                        EsProducto = Convert.ToBoolean(t["EsProducto"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiReferencia> ReferenciasObtenerPorCriterio(int page, int rows, UiReferencia model)
        {
            List<UiReferencia> list = new List<UiReferencia>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ReferenciaObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiReferencia
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        ClaveReferencia = Convert.ToInt32(t["ClaveReferencia"]),
                        Descripcion = t["Descripcion"].ToString(),
                        EsProducto = Convert.ToBoolean(t["EsProducto"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveReferencias(UiReferencia model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ReferenciaGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        ClaveReferencia = model.ClaveReferencia,
                        Descripcion = model.Descripcion,
                        EsProducto = model.EsProducto
                    },

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

        public bool ReferenciaCambiarEstatus(UiReferencia model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ReferenciaCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        #region GASTOS_INHERENTES
        public List<UiGastosInherente> ObtenerGastosInherentes(int page, int rows)
        {
            List<UiGastosInherente> list = new List<UiGastosInherente>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GastosInherenteObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiGastosInherente
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiGastosInherente> GastosInherentesObtenerPorCriterio(int page, int rows, UiGastosInherente model)
        {
            List<UiGastosInherente> list = new List<UiGastosInherente>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GastosInherenteObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiGastosInherente
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveGastosInherentes(UiGastosInherente model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GastosInherenteGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion
                    },

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

        public bool GastosInherenteCambiarEstatus(UiGastosInherente model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GastosInherenteCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        #region PERIODOS
        public List<UiPeriodo> ObtenerPeriodos(int page, int rows)
        {
            List<UiPeriodo> list = new List<UiPeriodo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.PeriodoObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiPeriodo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiPeriodo> PeriodosObtenerPorCriterio(int page, int rows, UiPeriodo model)
        {
            List<UiPeriodo> list = new List<UiPeriodo>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.PeriodoObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador
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
                    list = jResult["List"].Children().Select(t => new UiPeriodo
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SavePeriodos(UiPeriodo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.PeriodoGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion
                    },

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

        public bool PeriodoCambiarEstatus(UiPeriodo model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.PeriodoCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        #region FACTOR

        public List<UiFactor> ObtenerTodosFactor(int page, int rows)
        {
            List<UiFactor> list = new List<UiFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorObtener}",
                 new
                 {
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
                    list = jResult["List"].Children().Select(t => new UiFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),
                        IdClasificacionFactor = Convert.ToInt32(t["IdClasificacionFactor"]),
                        ClasificadorFactor = t["ClasificadorFactor"].ToString(),
                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = t["MedidaCobro"].ToString(),
                        Cuota = Convert.ToDecimal(t["Cuota"]),
                        CuotaTexto = "$" + Convert.ToString(t["CuotaTexto"]).Replace(",", "."),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicacionDof = Convert.ToDateTime(t["FechaPublicacionDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactor> ObtenerFactor(int page, int rows)
        {
            List<UiFactor> list = new List<UiFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorObtener}",
                 new
                 {
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
                    list = jResult["List"].Children().Select(t => new UiFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),
                        IdClasificacionFactor = Convert.ToInt32(t["IdClasificacionFactor"]),
                        ClasificadorFactor = t["ClasificadorFactor"].ToString(),
                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = t["MedidaCobro"].ToString(),
                        CuotaTexto = "$" + Convert.ToString(t["CuotaTexto"]).Replace(",", "."),
                        Cuota = Convert.ToDecimal(t["Cuota"]),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicacionDof = Convert.ToDateTime(t["FechaPublicacionDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactor> FactorObtenerPorCriterio(int page, int rows, UiFactor model)
        {
            List<UiFactor> list = new List<UiFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Identificador = model.Identificador,
                        IdTipoServicio = model.IdTipoServicio,
                        IdClasificacionFactor = model.IdClasificacionFactor
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
                    list = jResult["List"].Children().Select(t => new UiFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),
                        IdClasificacionFactor = Convert.ToInt32(t["IdClasificacionFactor"]),
                        ClasificadorFactor = t["ClasificadorFactor"].ToString(),
                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = t["MedidaCobro"].ToString(),
                        Cuota = Convert.ToDecimal(t["Cuota"]),
                        CuotaTexto = "$" + Convert.ToString(t["CuotaTexto"]).Replace(",", "."),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicacionDof = Convert.ToDateTime(t["FechaPublicacionDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveFactor(UiFactor model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdTipoServicio = model.IdTipoServicio,
                        IdClasificacionFactor = model.IdClasificacionFactor,
                        IdMedidaCobro = model.IdMedidaCobro,
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        Cuota = model.CuotaTexto,
                        FechaAutorizacion = model.FechaAutorizacion,
                        FechaEntradaVigor = model.FechaEntradaVigor,
                        FechaTermino = model.FechaTermino,
                        FechaPublicacionDof = model.FechaPublicacionDof
                    },

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

        public bool CambiarEstatusFactor(UiFactor model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        public List<UiFactor> ObtenerFactorPorClasificacion(int idClasificacion)
        {
            List<UiFactor> list = new List<UiFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorObtenerPorClasificacion}",
                 new
                 {
                     Item = new { IdClasificacionFactor = idClasificacion }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),
                        IdClasificacionFactor = Convert.ToInt32(t["IdClasificacionFactor"]),
                        ClasificadorFactor = t["ClasificadorFactor"].ToString(),
                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = t["MedidaCobro"].ToString(),
                        Cuota = Convert.ToDecimal(t["Cuota"]),
                        CuotaTexto = "$" + Convert.ToString(t["CuotaTexto"]).Replace(",", "."),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicacionDof = Convert.ToDateTime(t["FechaPublicacionDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactor> ClasificacionObtieneFactor(int idClasificacion)
        {
            List<UiFactor> list = new List<UiFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaDTOObtenerFactor}",
                 new
                 {
                     Item = new { IdClasificacionFactor = idClasificacion }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactor
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),
                        IdClasificacionFactor = Convert.ToInt32(t["IdClasificacionFactor"]),
                        ClasificadorFactor = t["ClasificadorFactor"].ToString(),
                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = t["MedidaCobro"].ToString(),
                        Cuota = Convert.ToDecimal(t["Cuota"]),
                        CuotaTexto = "$" + Convert.ToString(t["CuotaTexto"]).Replace(",", "."),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicacionDof = Convert.ToDateTime(t["FechaPublicacionDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        #endregion

        #region FACTOR_ENTIDAD_FEDERATIVA
        public List<UiFactorEntidadFederativa> ObtenerFactorEntidadFederativa(int page, int rows)
        {
            List<UiFactorEntidadFederativa> list = new List<UiFactorEntidadFederativa>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaObtener}",
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
                    list = jResult["List"].Children().Select(t =>
                    {
                        var federativa = new UiFactorEntidadFederativa();
                        federativa.IdClasificadorFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador"));
                        federativa.ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString();
                        federativa.IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador"));
                        federativa.Factor = t["Factor"].SelectToken("Nombre").ToString();
                        federativa.Descripcion = t["Descripcion"].ToString();
                        federativa.Estados = t["Estados"].Select(tt =>
                        {
                            var estado = new UiEstado();
                            estado.Nombre = tt["Nombre"].ToString();
                            estado.Identificador = Convert.ToInt32(tt["Identificador"]);
                            return estado;
                        }).ToList();
                        federativa.IsActive = Convert.ToBoolean(t["Activo"]);
                        federativa.EntidadesFederativas = String.Join(", ", t["Estados"].Select(tt => tt["Nombre"].ToString()));
                        federativa.Identificador = Convert.ToInt32(t["Identificador"]);
                        return federativa;
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactorEntidadFederativa> ObtenerFactorEntidadFederativaAgrupados(int page, int rows)
        {
            List<UiFactorEntidadFederativa> list = new List<UiFactorEntidadFederativa>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaObtenerAgrupados}",
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
                    list = jResult["List"].Children().Select(t => new UiFactorEntidadFederativa
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificadorFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Estados = t["Estados"].Select(tt => new UiEstado { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        EntidadesFederativas = String.Join(", ", t["Estados"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactorEntidadFederativa> FactorEntidadFederativaObtenerPorCriterio(int page, int rows, UiFactorEntidadFederativa model)
        {
            List<UiFactorEntidadFederativa> entidad = new List<UiFactorEntidadFederativa>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        IdClasificadorFactor = model.IdClasificadorFactor,
                        IdFactor = model.IdFactor,
                        IdEstado = model.IdEstado
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

                    entidad = jResult["List"].Children().Select(t => new UiFactorEntidadFederativa
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificadorFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Estados = t["Estados"].Select(tt => new UiEstado { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        EntidadesFederativas = String.Join(", ", t["Estados"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                }
            }
            return entidad;
        }

        public bool SaveFactorEntidadFederativa(UiFactorEntidadFederativa model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaGuardar}",
                new
                {
                    DTO = new
                    {
                        Identificador = model.Identificador,
                        Clasificacion = new { Identificador = model.IdClasificadorFactor },
                        Factor = new { Identificador = model.IdFactor },
                        Descripcion = model.Descripcion,
                        Estados = model.Estados.Select(e => new { identificador = e.Identificador })
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

        public bool CambiarEstatusFactorEntidadFederativa(UiFactorEntidadFederativa model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        public List<UiFactorEntidadFederativa> FactorEntidadFederativaObtenerSinAgrupar(UiPaging paging)
        {
            List<UiFactorEntidadFederativa> list = new List<UiFactorEntidadFederativa>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaSinAgrupar}",
                 new
                 {
                     Paging = new
                     {
                         All = true
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactorEntidadFederativa
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificadorFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Estados = t["Estados"].Select(tt => new UiEstado { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        EntidadesFederativas = String.Join(", ", t["Estados"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public UiFactorEntidadFederativa FactorEntidadFederativaObtener(UiPaging paging)
        {
            UiFactorEntidadFederativa entidad = new UiFactorEntidadFederativa();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaDTOObtener}",
                 new
                 {
                     Paging = new
                     {
                         All = paging.All
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {

                    entidad.ClasificacionesFactor = jResult["Entity"]
                        .SelectToken("Clasificaciones").Children().Select(c => new UiClasificacionFactor
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Nombre = c["Nombre"].ToString()
                        }).ToList();
                    entidad.Estados = jResult["Entity"]
                        .SelectToken("Estados").Children().Select(c => new UiEstado
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Nombre = c["Nombre"].ToString()
                        }).ToList();
                    entidad.Factores = jResult["Entity"]
                        .SelectToken("Factores").Children().Select(c => new UiFactor
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Nombre = c["Nombre"].ToString()
                        }).ToList();

                    entidad.IsActive = Convert.ToBoolean(jResult["Activo"]);
                    if (entidad.Descripcion != null)
                    {
                        entidad.Descripcion = jResult["Descripcion"].ToString();
                    }
                }
            }
            return entidad;
        }

        public List<UiFactorEntidadFederativa> ClasificacionObtieneEstados(int idClasificacion)
        {
            List<UiFactorEntidadFederativa> list = new List<UiFactorEntidadFederativa>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaDTOObtenerEstados}",
                new
                {
                    IdClasificadorFactor = idClasificacion
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactorEntidadFederativa
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificadorFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        IdEstado = Convert.ToInt32(t["IdEstado"]),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Estados = t["Estados"].Select(tt => new UiEstado { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        EntidadesFederativas = String.Join(", ", t["Estados"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                }
            }
            return list;
        }

        #endregion

        #region FACTOR_MUNICIPIO

        public List<UiFactorMunicipio> ObtenerTodosFactorMunicipio(int page, int rows)
        {
            List<UiFactorMunicipio> list = new List<UiFactorMunicipio>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresMunicipioObtenerTodos}",
                 new
                 {
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
                    list = jResult["List"].Children().Select(t => new UiFactorMunicipio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdEstado = Convert.ToInt32(t["Estados"].FirstOrDefault().SelectToken("Identificador")),
                        NomEstado = t["Estados"].FirstOrDefault().SelectToken("Nombre").ToString(),
                        Municipios = t["Municipios"].Select(tt => new UiMunicipio { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        MunicipiosGrupo = String.Join(", ", t["Municipios"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        public List<UiFactorMunicipio> FactorMunicipioObtener(int page, int rows)
        {
            List<UiFactorMunicipio> list = new List<UiFactorMunicipio>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresMunicipioObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiFactorMunicipio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdEstado = Convert.ToInt32(t["Estados"].FirstOrDefault().SelectToken("Identificador")),
                        NomEstado = t["Estados"].FirstOrDefault().SelectToken("Nombre").ToString(),
                        Municipios = t["Municipios"].Select(tt => new UiMunicipio { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        MunicipiosGrupo = String.Join(", ", t["Municipios"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        public List<UiMunicipio> ObtenerMunicipios(int id)
        {
            List<UiMunicipio> list = new List<UiMunicipio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.MunicipiosObtener}",
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

                    list = jResult["List"].Children().Select(t => new UiMunicipio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdEstado = Convert.ToInt32(t["IdEstado"]),
                        Nombre = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])

                    }).ToList();
                }

            }
            return list;
        }
        public bool SaveFactorMunicipio(UiFactorMunicipio model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorMunicipioGuardar}",
                new
                {
                    DTO = new
                    {
                        Identificador = model.Identificador,
                        Clasificacion = new { Identificador = model.IdClasificacionFactor },
                        Factor = new { Identificador = model.IdFactor },
                        Descripcion = model.Descripcion,
                        Estado = new { Identificador = model.IdEstado },
                        Municipios = model.Municipios.Select(m => new { Identificador = m.Identificador })
                    },
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
        public List<UiFactorMunicipio> ObtenerPorCriterioFactorMunicipio(int page, int rows, UiFactorMunicipio model)
        {
            List<UiFactorMunicipio> entidad = new List<UiFactorMunicipio>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresMunicipioObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        IdClasificacionFactor = model.IdClasificacionFactor,
                        IdFactor = model.IdFactor,
                        IdEstado = model.IdEstado,
                        IdMunicipio = model.IdMunicipio
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

                    entidad = jResult["List"].Children().Select(t => new UiFactorMunicipio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdEstado = Convert.ToInt32(t["Estados"].FirstOrDefault().SelectToken("Identificador")),
                        NomEstado = t["Estados"].FirstOrDefault().SelectToken("Nombre").ToString(),
                        Municipios = t["Municipios"].Select(tt => new UiMunicipio { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        MunicipiosGrupo = String.Join(", ", t["Municipios"].Select(tt => tt["Nombre"].ToString()))
                    }).ToList();
                }
            }
            return entidad;
        }
        public List<UiFactorMunicipio> ClasificacionObtieneMunicipios(int idClasificacion)
        {
            List<UiFactorMunicipio> list = new List<UiFactorMunicipio>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorMunicipiosDTOObtenerMunicipios}",
                new
                {
                    IdClasificacionFactor = idClasificacion
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactorMunicipio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificacionFactor = Convert.ToInt32(t["IdClasificacionFactor"]),
                        IdEstado = Convert.ToInt32(t["IdEstado"]),
                        IdMunicipio = Convert.ToInt32(t["IdMunicipio"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                    }).ToList();
                }
            }
            return list;
        }

        #endregion

        #region FACTOR_LEY_INGRESO

        public List<UiFactorLeyIngreso> ObtenerFactorLeyIngreso(int page, int rows)
        {
            List<UiFactorLeyIngreso> list = new List<UiFactorLeyIngreso>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorLeyIngresoObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiFactorLeyIngreso
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdAnio = Convert.ToInt32(t["IdAnio"]),
                        Anio = t["Anio"].ToString(),
                        IdMes = Convert.ToInt32(t["IdMes"]),
                        Mes = t["Mes"].ToString(),
                        Factor = Convert.ToDouble(t["Factor"]),
                        FactorTexto = "$" + Convert.ToString(t["FactorTexto"]).Replace(",", "."),
                        IsActive = Convert.ToBoolean(t["Activo"])

                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }


        public List<UiFactorLeyIngreso> FactorLeyIngresoObtenerPorCriterio(int page, int rows, UiFactorLeyIngreso model)
        {
            List<UiFactorLeyIngreso> list = new List<UiFactorLeyIngreso>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorLeyIngresoObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        IdAnio = model.IdAnio
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
                    list = jResult["List"].Children().Select(t => new UiFactorLeyIngreso
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdAnio = Convert.ToInt32(t["IdAnio"]),
                        Anio = t["Anio"].ToString(),
                        IdMes = Convert.ToInt32(t["IdMes"]),
                        Mes = t["Mes"].ToString(),
                        Factor = Convert.ToDouble(t["Factor"]),
                        IsActive = Convert.ToBoolean(t["Activo"])

                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveFactorLeyIngreso(UiFactorLeyIngreso model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorLeyIngresoGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        IdAnio = model.IdAnio,
                        IdMes = model.IdMes,
                        Factor = model.FactorTexto
                    },

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

        public bool CambiarEstatusFactorLeyIngreso(UiFactorLeyIngreso model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorLeyIngresoCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        #region FACTOR_ACTIVIDAD_ECONOMICA

        public List<UiFactorActividadEconomica> ObtenerFactorActividadEconomica(int page, int rows)
        {
            List<UiFactorActividadEconomica> list = new List<UiFactorActividadEconomica>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresActividadEconomicaObtenerTodos}",
                 new
                 {
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
                    list = jResult["List"].Children().Select(t => new UiFactorActividadEconomica
                    {
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdGrupo = Convert.ToInt32(t["Grupo"].SelectToken("Identificador")),
                        Grupo = t["Grupo"].SelectToken("Nombre").ToString(),
                        IdDivision = Convert.ToInt32(t["Division"].SelectToken("Identificador")),
                        Division = t["Division"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Fracciones = t["Fracciones"].Select(tt => new UiFracciones { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Actividades = String.Join(", ", t["Fracciones"].Select(tt => tt["Nombre"].ToString())),
                        Identificador = Convert.ToInt32(t["Identificador"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactorActividadEconomica> ObtenerFactorActividadEconomicaAgrupados(int page, int rows)
        {
            List<UiFactorActividadEconomica> list = new List<UiFactorActividadEconomica>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresActividadEconomicaAgrupados}",
                 new
                 {
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
                    list = jResult["List"].Children().Select(t => new UiFactorActividadEconomica
                    {
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdGrupo = Convert.ToInt32(t["Grupo"].SelectToken("Identificador")),
                        Grupo = t["Grupo"].SelectToken("Nombre").ToString(),
                        IdDivision = Convert.ToInt32(t["Division"].SelectToken("Identificador")),
                        Division = t["Division"].SelectToken("NombreDivision").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Fracciones = t["Fracciones"].Select(tt => new UiFracciones { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Actividades = String.Join(", ", t["Fracciones"].Select(tt => tt["Nombre"].ToString())),
                        Identificador = Convert.ToInt32(t["Identificador"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactorActividadEconomica> FactorActividadEconomicaObtenerPorCriterio(int page, int rows, UiFactorActividadEconomica model)
        {
            List<UiFactorActividadEconomica> list = new List<UiFactorActividadEconomica>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresActividadEconomicaObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        IdClasificacionFactor = model.IdClasificacionFactor,
                        IdFactor = model.IdFactor,
                        IdGrupo = model.IdGrupo,
                        IdDivision = model.IdDivision
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

                    list = jResult["List"].Children().Select(t => new UiFactorActividadEconomica
                    {
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdGrupo = Convert.ToInt32(t["Grupo"].SelectToken("Identificador")),
                        Grupo = t["Grupo"].SelectToken("Nombre").ToString(),
                        IdDivision = Convert.ToInt32(t["Division"].SelectToken("Identificador")),
                        Division = t["Division"].SelectToken("NombreDivision").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Fracciones = t["Fracciones"].Select(tt => new UiFracciones { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Actividades = String.Join(", ", t["Fracciones"].Select(tt => tt["Nombre"].ToString())),
                        Identificador = Convert.ToInt32(t["Identificador"])
                    }).ToList();
                }
            }
            return list;
        }

        public bool SaveFactorActividadEconomica(UiFactorActividadEconomica model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresActividadEconomicaSave}",
                new
                {
                    DTO = new
                    {
                        Identificador = model.Identificador,
                        Clasificacion = new { Identificador = model.IdClasificacionFactor },
                        Factor = new { Identificador = model.IdFactor },
                        Descripcion = model.Descripcion,
                        Fracciones = model.Fracciones.Select(e => new { identificador = e.Identificador })
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

        public bool CambiarEstatusFactorActividadEconomica(UiFactorActividadEconomica model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        public List<UiFactorActividadEconomica> FactorActividadEconomicaObtenerSinAgrupar(int page, int rows)
        {
            List<UiFactorActividadEconomica> list = new List<UiFactorActividadEconomica>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresActividadEconomicaSinAgrupar}",
                 new
                 {
                     Paging = new
                     {
                         All = true
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactorActividadEconomica
                    {
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdGrupo = Convert.ToInt32(t["Grupo"].SelectToken("Identificador")),
                        Grupo = t["Grupo"].SelectToken("Nombre").ToString(),
                        IdDivision = Convert.ToInt32(t["Division"].SelectToken("Identificador")),
                        Division = t["Division"].SelectToken("NombreDivision").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdFraccion = Convert.ToInt32(t["IdFraccion"]),
                        //Fracciones = t["Fracciones"].Select(tt => new UiFracciones { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Actividades = String.Join(", ", t["Fracciones"].Select(tt => tt["Nombre"].ToString())),
                        Identificador = Convert.ToInt32(t["Identificador"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public UiFactorActividadEconomica FactorActividadEconomicaObtener(UiPaging paging)
        {
            UiFactorActividadEconomica list = new UiFactorActividadEconomica();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresActividadEconomicaAgrupados}",
                 new
                 {
                     Paging = new
                     {
                         All = paging.All
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {

                    list.Clasificaciones = jResult["List"]
                        .SelectToken("Clasificaciones").Children().Select(c => new UiClasificacionFactor
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Nombre = c["Nombre"].ToString()
                        }).ToList();
                    list.Factores = jResult["List"]
                        .SelectToken("Factores").Children().Select(c => new UiFactor
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Nombre = c["Nombre"].ToString()
                        }).ToList();
                    list.Divisiones = jResult["List"]
                        .SelectToken("Division").Children().Select(c => new UiDivision
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Name = c["NombreDivision"].ToString()
                        }).ToList();
                    list.Grupos = jResult["List"]
                        .SelectToken("Grupo").Children().Select(c => new UiGrupo
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Name = c["Nombre"].ToString()
                        }).ToList();
                    list.Fracciones = jResult["List"]
                        .SelectToken("Fracciones").Children().Select(c => new UiFracciones
                        {
                            Identificador = Convert.ToInt32(c["Identificador"]),
                            Nombre = c["Nombre"].ToString()
                        }).ToList();
                    list.IsActive = Convert.ToBoolean(jResult["Activo"]);
                    if (list.Descripcion != null)
                    {
                        list.Descripcion = jResult["Descripcion"].ToString();
                    }
                }
            }
            return list;
        }

        public List<UiFactorActividadEconomica> ClasificacionObtieneFracciones(int idClasificacion)
        {
            List<UiFactorActividadEconomica> list = new List<UiFactorActividadEconomica>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaDTOObtenerEstados}",
                new
                {
                    IdClasificadorFactor = idClasificacion
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFactorActividadEconomica
                    {
                        IdClasificacionFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdGrupo = Convert.ToInt32(t["Grupo"].SelectToken("Identificador")),
                        Grupo = t["Grupo"].SelectToken("Nombre").ToString(),
                        IdDivision = Convert.ToInt32(t["Division"].SelectToken("Identificador")),
                        Division = t["Division"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Fracciones = t["Fracciones"].Select(tt => new UiFracciones { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Actividades = String.Join(", ", t["Fracciones"].Select(tt => tt["Nombre"].ToString())),
                        Identificador = Convert.ToInt32(t["Identificador"])
                    }).ToList();
                }
            }
            return list;
        }

        #endregion

        #region ESTADO
        public List<UiEstado> ObtenerEstado(int page, int rows)
        {
            List<UiEstado> list = new List<UiEstado>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.EstadoObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiEstado
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiEstado> EstadoObtenerPorCriterio(int page, int rows, UiEstado model)
        {
            List<UiEstado> list = new List<UiEstado>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiEstado
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveEstado(UiEstado model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.EstadoObtener}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Nombre
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        public bool CambiarEstatusEstado(UiEstado model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        #endregion

        #region ANIO
        public List<UiAnio> ObtenerAnio(int page, int rows)
        {
            List<UiAnio> list = new List<UiAnio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.AnioObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiAnio
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveAnio(UiAnio model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        #endregion

        #region MES
        public List<UiMes> ObtenerMes(int page, int rows)
        {
            List<UiMes> list = new List<UiMes>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.MesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiMes
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveMes(UiMes model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        #endregion

        #region MEDIDA_COBRO

        public List<UiMedidaCobro> ObtenerMedidaCobro(int page, int rows)
        {
            List<UiMedidaCobro> list = new List<UiMedidaCobro>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.MedidaCobroObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiMedidaCobro
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiMedidaCobro> MedidaCobroObtenerPorCriterio(int page, int rows, UiMedidaCobro model)
        {
            List<UiMedidaCobro> list = new List<UiMedidaCobro>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.MedidaCobroObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiMedidaCobro
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveMedidaCobro(UiMedidaCobro model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.MedidaCobroGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name,
                        Descripcion = model.Descripcion
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

        public bool CambiarEstatusMedidaCobro(UiMedidaCobro model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.MedidaCobroCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        #endregion

        #region FRACCIONES
        public List<UiFracciones> ObtenerFracciones(int page, int rows)
        {
            List<UiFracciones> list = new List<UiFracciones>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FraccionesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiFracciones
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdGrupo = Convert.ToInt32(t["IdGrupo"]),
                        IdDivision = Convert.ToInt32(t["IdDivision"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Division = t["Division"].ToString(),
                        Grupo = t["Grupo"].ToString()
                    }).OrderBy(t => t.Nombre).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFracciones> ObtenerTodosFracciones(int page, int rows)
        {
            List<UiFracciones> list = new List<UiFracciones>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FraccionesObtener}",
                new
                {
                    Paging = new { All = true, CurrentPage = page, Rows = rows }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiFracciones
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdGrupo = Convert.ToInt32(t["IdGrupo"]),
                        IdDivision = Convert.ToInt32(t["IdDivision"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Division = t["Division"].ToString(),
                        Grupo = t["Grupo"].ToString()
                    }).OrderBy(t => t.Nombre).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFracciones> ObtenerFraccionesPorCriterio(int page, int rows, UiFracciones model)
        {
            List<UiFracciones> list = new List<UiFracciones>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FraccionesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        Nombre = model.Nombre
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
                    list = jResult["List"].Children().Select(t => new UiFracciones
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Nombre = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdGrupo = Convert.ToInt32(t["IdGrupo"]),
                        IdDivision = Convert.ToInt32(t["IdDivision"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Division = t["Division"].ToString(),
                        Grupo = t["Grupo"].ToString()
                    }).OrderBy(t => t.Nombre).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveFracciones(UiFracciones model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FraccionesGuardar}",
                                                                            new
                                                                            {
                                                                                Item = new
                                                                                {
                                                                                    Identificador = model.Identificador,
                                                                                    Nombre = model.Nombre,
                                                                                    Descripcion = model.Descripcion,
                                                                                    IdGrupo = model.IdGrupo,
                                                                                    Activo = model.IsActive,
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

        public bool FraccionCambiarEstatus(UiFracciones model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FraccionesCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
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

        public string FraccionValidarRegistro(UiFracciones model)
        {
            string resultado = string.Empty;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FraccionValidar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Nombre,
                        IdGrupo = model.IdGrupo
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                resultado = jResult["Resultado"].ToString();
            }

            return resultado;
        }

        #endregion FRACCIONES        

        #region CUOTAS
        public List<UiCuota> ObtenerCuotas(int page, int rows)
        {
            List<UiCuota> list = new List<UiCuota>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.CuotaObtener}",
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
                    DateTime parsedDate;
                    list = jResult["List"].Children().Select(t => new UiCuota
                    {

                        Identificador = Convert.ToInt32(t["Identificador"]),

                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),

                        IdReferencia = Convert.ToInt32(t["IdReferencia"]),
                        Referencia = t["Referencia"].ToString(),
                        EsProducto = Convert.ToBoolean(t["EsProducto"]),

                        IdDependencia = Convert.ToInt32(t["IdDependencia"]),
                        Dependencia = t["Dependencia"].ToString(),
                        DescripcionDependencia = t["DescripcionDependencia"].ToString(),

                        Concepto = t["Concepto"].ToString(),

                        IdJerarquia = Convert.ToInt32(t["IdJerarquia"]),
                        Jerarquia = t["Jerarquia"].ToString(),

                        IdGrupoTarifario = Convert.ToInt32(t["IdGrupoTarifario"]),
                        GrupoTarifario = Convert.ToString(t["GrupoTarifario"]),

                        CuotaBase = Convert.ToDecimal(t["CuotaBase"]),

                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = Convert.ToString(t["IdMedidaCobro"]),

                        Iva = Convert.ToDecimal(t["Iva"]),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        // FechaAutorizacion = JsonConvert.DeserializeObject<DateTime>(t["FechaAutorizacion"].ToString(), new IsoDateTimeConverter { DateTimeFormat = "yyyy-mm-dd:hh:mm:ss" }),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicaDof = Convert.ToDateTime(t["FechaPublicaDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiCuota> CuotasObtenerPorCriterio(int page, int rows, UiCuota model)
        {
            List<UiCuota> list = new List<UiCuota>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.CuotaObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        IdTipoServicio = model.IdTipoServicio,
                        Concepto = model.Concepto,
                        EsProducto = model.EsProducto,
                        Ano = model.Ano,
                        EsProductoSearch = model.EsProducto
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
                    list = jResult["List"].Children().Select(t => new UiCuota
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),

                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),

                        IdReferencia = Convert.ToInt32(t["IdReferencia"]),
                        Referencia = t["Referencia"].ToString(),
                        EsProducto = Convert.ToBoolean(t["EsProducto"]),

                        IdDependencia = Convert.ToInt32(t["IdDependencia"]),
                        Dependencia = t["Dependencia"].ToString(),
                        DescripcionDependencia = t["DescripcionDependencia"].ToString(),

                        Concepto = t["Concepto"].ToString(),

                        IdJerarquia = Convert.ToInt32(t["IdJerarquia"]),
                        Jerarquia = t["Jerarquia"].ToString(),

                        IdGrupoTarifario = Convert.ToInt32(t["IdGrupoTarifario"]),
                        GrupoTarifario = Convert.ToString(t["GrupoTarifario"]),

                        CuotaBase = Convert.ToDecimal(t["CuotaBase"]),

                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = Convert.ToString(t["IdMedidaCobro"]),

                        Iva = Convert.ToDecimal(t["Iva"]),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicaDof = Convert.ToDateTime(t["FechaPublicaDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public bool SaveCuotas(UiCuota model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.CuotaGuardar}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,

                        IdTipoServicio = model.IdTipoServicio,
                        TipoServicio = model.TipoServicio,

                        IdReferencia = model.IdReferencia,
                        Referencia = model.Referencia,

                        IdDependencia = model.IdDependencia,
                        Dependencia = model.Dependencia,
                        DescripcionDependencia = model.DescripcionDependencia,

                        Concepto = model.Concepto,

                        IdJerarquia = model.IdJerarquia,
                        Jerarquia = model.Jerarquia,

                        IdGrupoTarifario = model.IdGrupoTarifario,
                        GrupoTarifario = model.GrupoTarifario,

                        CuotaBase = model.CuotaBase,

                        IdMedidaCobro = model.IdMedidaCobro,
                        MedidaCobro = model.IdMedidaCobro,

                        Iva = model.Iva,
                        FechaAutorizacion = model.FechaAutorizacion,
                        FechaEntradaVigor = model.FechaEntradaVigor,
                        FechaTermino = model.FechaTermino,
                        FechaPublicaDof = model.FechaPublicaDof,
                        Activo = model.IsActive
                    },

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

        public bool CuotaCambiarEstatus(UiCuota model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.CuotaCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        public List<UiCuota> ObtenerCuotasConceptos(int page, int rows)
        {
            List<UiCuota> list = new List<UiCuota>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ObtenerCuotasConceptos}",
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
                    list = jResult["List"].Children().Select(t => new UiCuota
                    {

                        Identificador = Convert.ToInt32(t["Identificador"]),

                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        TipoServicio = t["TipoServicio"].ToString(),

                        IdReferencia = Convert.ToInt32(t["IdReferencia"]),
                        Referencia = t["Referencia"].ToString(),

                        IdDependencia = Convert.ToInt32(t["IdDependencia"]),
                        Dependencia = t["Dependencia"].ToString(),
                        DescripcionDependencia = t["DescripcionDependencia"].ToString(),

                        Concepto = t["Concepto"].ToString(),

                        IdJerarquia = Convert.ToInt32(t["IdJerarquia"]),
                        Jerarquia = t["Jerarquia"].ToString(),

                        IdGrupoTarifario = Convert.ToInt32(t["IdGrupoTarifario"]),
                        GrupoTarifario = Convert.ToString(t["GrupoTarifario"]),

                        CuotaBase = Convert.ToDecimal(t["CuotaBase"]),

                        IdMedidaCobro = Convert.ToInt32(t["IdMedidaCobro"]),
                        MedidaCobro = Convert.ToString(t["IdMedidaCobro"]),

                        Iva = Convert.ToDecimal(t["Iva"]),
                        FechaAutorizacion = Convert.ToDateTime(t["FechaAutorizacion"]),
                        FechaEntradaVigor = Convert.ToDateTime(t["FechaEntradaVigor"]),
                        FechaTermino = Convert.ToDateTime(t["FechaTermino"]),
                        FechaPublicaDof = Convert.ToDateTime(t["FechaPublicaDof"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiCuota> CuotaObtenerAnos(int page, int rows)
        {
            List<UiCuota> list = new List<UiCuota>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.CuotaObtenerAnos}",
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
                    list = jResult["List"].Children().Select(t => new UiCuota
                    {
                        Ano = Convert.ToInt32(t["Ano"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }


        #endregion

        /// <summary>
        /// ---------------------JZR Fizcalizacion----------------------- 
        /// </summary>
        /// 

        #region REGIMEN FISCAL
        public List<UiRegimenFiscal> ObtenerRegimenFiscal(int page, int rows)
        {
            List<UiRegimenFiscal> list = new List<UiRegimenFiscal>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RegimenFiscalObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiRegimenFiscal
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = Convert.ToString(t["Nombre"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiRegimenFiscal> RegimenFiscalObtenerPorCriterio(int page, int rows, UiRegimenFiscal model)
        {
            List<UiRegimenFiscal> list = new List<UiRegimenFiscal>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.RegimenFiscalObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiRegimenFiscal
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = Convert.ToString(t["Nombre"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        #endregion

        #region TIPOS PAGOS
        public List<UiTiposPago> ObtenerTiposPago(int page, int rows)
        {
            List<UiTiposPago> list = new List<UiTiposPago>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposPagoObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiTiposPago
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = Convert.ToString(t["Nombre"]),
                        Descripcion = Convert.ToString(t["Descripcion"]),
                        Actividad = Convert.ToBoolean(t["Actividad"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiTiposPago> TiposPagoObtenerPorCriterio(int page, int rows, UiTiposPago model)
        {
            List<UiTiposPago> list = new List<UiTiposPago>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposPagoObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiTiposPago
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = Convert.ToString(t["Nombre"]),
                        Descripcion = Convert.ToString(t["Descripcion"]),
                        Actividad = Convert.ToBoolean(t["Actividad"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        #endregion

        #region ACTIVIDADES
        public List<UiActividad> ObtenerActividades(int page, int rows)
        {
            List<UiActividad> list = new List<UiActividad>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ActividadObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiActividad
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdFase = Convert.ToInt32(t["IdFase"]),
                        Name = Convert.ToString(t["Nombre"]),
                        Descripcion = Convert.ToString(t["Descripcion"]),
                        SePuedeAplicarPlazo = Convert.ToBoolean(t["SePuedeAplicarPlazo"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        Validacion = Convert.ToBoolean(t["Validacion"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        public List<UiActividad> ActividadesPorCriterio(int page, int rows, UiActividad model)
        {
            List<UiActividad> list = new List<UiActividad>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ActividadObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiActividad
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdFase = Convert.ToInt32(t["IdFase"]),
                        Name = Convert.ToString(t["Nombre"]),
                        Descripcion = Convert.ToString(t["Descripcion"]),
                        SePuedeAplicarPlazo = Convert.ToBoolean(t["Actividad"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;

        }

        #endregion

        #region TIPOS_DOCUMENTO
        public List<UiTiposDocumento> ObtenerTiposDocumento(int page, int rows)
        {
            List<UiTiposDocumento> list = new List<UiTiposDocumento>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TipoDocumentoObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiTiposDocumento
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Descripcion = t["Descripcion"].ToString(),
                        Name = t["Nombre"].ToString(),
                        Confidencial = Convert.ToBoolean(t["Confidencial"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        Actividad = t["Actividad"].ToString()

                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiTiposDocumento> TipoDocumentoObtenerPorCriterio(int page, int rows, UiTiposDocumento model)
        {
            List<UiTiposDocumento> list = new List<UiTiposDocumento>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TipoDocumentoObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive,
                        IdActividad = model.IdActividad
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
                    list = jResult["List"].Children().Select(t => new UiTiposDocumento
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Descripcion = t["Descripcion"].ToString(),
                        Name = t["Nombre"].ToString(),
                        Confidencial = Convert.ToBoolean(t["Confidencial"]),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        Actividad = t["Actividad"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveTipoDocumento(UiTiposDocumento model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TipoDocumentoGuardar}",
               new
               {
                   Item = new
                   {
                       Nombre = model.Name,
                       IdActividad = model.IdActividad,
                       Identificador = model.Identificador,
                       Confidencial = model.Confidencial,
                       Descripcion = model.Descripcion

                   },

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


        public bool TipoDocumentoCambiarEstatus(UiTiposDocumento model)
        {
            bool success = false;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TipoDocumentoCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

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

        //JZR 25_08_2017_19_26//

        #region JERARQUIAS
        public List<UiJerarquia> ObtenerJerarquias(int page, int rows)
        {
            List<UiJerarquia> list = new List<UiJerarquia>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.JeraquiasObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiJerarquia
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Nivel = Convert.ToInt32(t["Nivel"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiJerarquia> JerarquiasObtenerPorCriterio(int page, int rows, UiJerarquia model)
        {
            List<UiJerarquia> list = new List<UiJerarquia>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.JeraquiasObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiJerarquia
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveJerarquias(UiJerarquia model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.JeraquiasObtener}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        public bool CambiarJerarquias(UiJerarquia model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.JeraquiasCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        #endregion

        #region GRUPOTARIFARIO
        public List<UiGrupoTarifario> ObtenerGrupoTarifario(int page, int rows)
        {
            List<UiGrupoTarifario> list = new List<UiGrupoTarifario>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoTarifariosObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiGrupoTarifario
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Nivel = Convert.ToInt32(t["Nivel"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiGrupoTarifario> GrupoTarifarioObtenerPorCriterio(int page, int rows, UiGrupoTarifario model)
        {
            List<UiGrupoTarifario> list = new List<UiGrupoTarifario>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoTarifariosObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiGrupoTarifario
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public bool SaveGrupoTarifario(UiGrupoTarifario model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoTarifariosObtener}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Nombre = model.Name
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        public bool CambiarEstatusGrupoTarifario(UiGrupoTarifario model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GrupoTarifariosCambiarEstatus}",
                new
                {
                    Item = new
                    {
                        Identificador = model.Identificador,
                        Activo = model.IsActive
                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        #endregion

        #region CONFIGURACION_SERVICIO

        public List<UiConfiguracionServicio> ConfiguracionServicioObtenerConfiguracionPaginado(int page, int rows)
        {
            List<UiConfiguracionServicio> list = new List<UiConfiguracionServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ConfiguracionServicioObtenerPaginado}",
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
                    list = jResult["List"].Children().Select(t => new UiConfiguracionServicio
                    {
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        IdCentroCostos = t["IdCentroCostos"].ToString(),
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        IdTipoPago = Convert.ToInt32(t["IdTipoPago"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        IdTipoDocumento = Convert.ToInt32(t["IdTipoDocumento"]),
                        Tiempo = Convert.ToInt32(t["Tiempo"]),
                        Aplica = Convert.ToBoolean(t["Aplica"]),
                        Obigatoriedad = Convert.ToBoolean(t["Obigatoriedad"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiConfiguracionServicio> ConfiguracionServicioObtenerPorIdConfiguracionServicio(int idConfiguracionServicio)
        {
            List<UiConfiguracionServicio> list = new List<UiConfiguracionServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ConfiguracionServicioObtenerPorId}",
                 new
                 {
                     Item = new
                     {
                         idConfiguracionServicio = idConfiguracionServicio
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiConfiguracionServicio
                    {
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        IdCentroCostos = t["IdCentroCostos"].ToString(),
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        IdTipoPago = Convert.ToInt32(t["IdTipoPago"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        IdTipoDocumento = Convert.ToInt32(t["IdTipoDocumento"]),
                        Tiempo = Convert.ToInt32(t["Tiempo"]),
                        Aplica = Convert.ToBoolean(t["Aplica"]),
                        Obigatoriedad = Convert.ToBoolean(t["Obigatoriedad"])
                    }).ToList();
                }
            }
            return list;
        }

        public List<UiConfiguracionServicio> ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto(UiConfiguracionServicio model)
        {
            List<UiConfiguracionServicio> list = new List<UiConfiguracionServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto}/" + model.IdTipoServicio + "/" + model.IdCentroCostos,//$"{apiCatalogClient}{MethodApiCatalog.ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto}",
                 new
                 {
                     Item = new
                     {
                         idCentroCosto = model.IdCentroCostos,
                         idTipoServicio = model.IdTipoServicio
                     }
                 });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiConfiguracionServicio
                    {
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        IdCentroCostos = t["IdCentroCostos"].ToString(),
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        IdTipoPago = Convert.ToInt32(t["IdTipoPago"]),
                        IdActividad = Convert.ToInt32(t["IdActividad"]),
                        IdTipoDocumento = Convert.ToInt32(t["IdTipoDocumento"]),
                        Tiempo = Convert.ToInt32(t["Tiempo"]),
                        Aplica = Convert.ToBoolean(t["Aplica"]),
                        Obigatoriedad = Convert.ToBoolean(t["Obigatoriedad"])
                    }).ToList();
                }
            }
            return list;
        }

        /*TERMINAR*/

        public bool SaveConfiguracionServicio(List<UiConfiguracionServicio> listaConfiguracion)
        {
            bool success = false;


            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ConfiguracionServicioInsertarConfiguraciones}",
                                                                            new
                                                                            {
                                                                                ListInsertar = listaConfiguracion.Select(x => new
                                                                                {
                                                                                    IdTipoServicio = x.IdTipoServicio,
                                                                                    IdCentroCostos = x.IdCentroCostos,
                                                                                    IdRegimenFiscal = x.IdRegimenFiscal,
                                                                                    IdTipoPago = x.IdTipoPago,
                                                                                    IdActividad = x.IdActividad,
                                                                                    IdTipoDocumento = x.IdTipoDocumento,
                                                                                    Tiempo = x.Tiempo,
                                                                                    Aplica = x.Aplica,
                                                                                    Obigatoriedad = x.Obigatoriedad
                                                                                })
                                                                            });

            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
            }
            return success;
        }

        public List<UiDetalleConfiguracionServicio> ConfiguracionDetalle(int page, int rows)
        {
            List<UiDetalleConfiguracionServicio> list = new List<UiDetalleConfiguracionServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ConfiguracionDetalle}/" + page + "/" + rows,
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
                    list = jResult["List"].Children().Select(t => new UiDetalleConfiguracionServicio
                    {
                        Estatus = Convert.ToBoolean(t["Activo"]),
                        FechaRegistro = DateTime.Now,
                        IdRegimenFiscal = Convert.ToInt32(t["IdRegimenFiscal"]),
                        IdTipoPago = Convert.ToInt32(t["IdTipoPago"]),
                        IdTipoServicio = Convert.ToInt32(t["IdTipoServicio"]),
                        RegimenFiscal = t["RegimenFiscal"].ToString(),
                        TipoPago = t["TipoPago"].ToString(),
                        TipoServicio = t["TipoServicio"].ToString(),
                        IdCentroCosto = Convert.ToInt32(t["IdCentroCosto"])

                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }


        #endregion CONFIGURACION_SERVICIO

        #region INTEGRANTES
        public List<UiIntegrante> ObtenerIntegrantes(int page, int rows)
        {
            List<UiIntegrante> list = new List<UiIntegrante>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.IntegrantesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiIntegrante
                    {
                        Identificador = (string.IsNullOrEmpty(t["Identificador"].ToString()) ? new Guid() : new Guid(t["Identificador"].ToString())),
                        Nombre = t["Nombre"].ToString(),
                        ApPaterno = t["ApPaterno"].ToString(),
                        ApMaterno = t["ApMaterno"].ToString(),
                        Correo = t["Correo"].ToString(),
                        CorreoPersonal = t["CorreoPersonal"].ToString(),
                        IdArea = t["IdArea"].ToString(),
                        Area = t["Area"].ToString(),
                        IdJerarquia = Convert.ToInt32(t["IdJerarquia"]),
                        Jerarquia = t["Jerarquia"].ToString(),
                        //Descripcion = t["Descripcion"].ToString(),
                        IsActive = true
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiIntegrante> IntegrantesObtenerPorCriterio(int page, int rows, UiIntegrante model)
        {
            List<UiIntegrante> list = new List<UiIntegrante>(); ;
            Task<HttpResponseMessage> response =
                client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.IntegrantesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiIntegrante
                    {

                        Identificador = new Guid(t["Idintegrante"].ToString()),
                        Nombre = t["Nombre"].ToString(),
                        ApPaterno = t["ApPaterno"].ToString(),
                        ApMaterno = t["ApMaterno"].ToString(),
                        Correo = t["Correo"].ToString(),
                        CorreoPersonal = t["CorreoPersonal"].ToString(),
                        IdArea = t["IdArea"].ToString(),
                        Area = t["Area"].ToString(),
                        IdJerarquia = Convert.ToInt32(t["IdJerarquia"]),
                        Jerarquia = t["Jerarquia"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = true
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        #endregion

        #region FASES
        public List<UiFases> ObtenerFases(int page, int rows)
        {
            List<UiFases> list = new List<UiFases>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FasesObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiFases
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFases> JerarquiasObtenerPorCriterio(int page, int rows, UiFases model)
        {
            List<UiFases> list = new List<UiFases>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FasesObtenerPorCriterio}",
                new
                {
                    Item = new
                    {
                        Activo = model.IsActive
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
                    list = jResult["List"].Children().Select(t => new UiFases
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IsActive = Convert.ToBoolean(t["Activo"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        #endregion

        #region SECTORES
        public List<UiSector> ObtenerSector()
        {
            List<UiSector> list = new List<UiSector>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.SectorObtener}",
                new
                {
                    Paging = new
                    {
                        All = false,
                        CurrentPage = 1,
                        Rows = 1
                    }
                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiSector
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Descripcion = Convert.ToString(t["Descripcion"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        #endregion

        public void Servicio()
        {

        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }

        #region AREAS

        public List<UiArea> ObtenerAreas(int page, int rows)
        {
            List<UiArea> list = new List<UiArea>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.AreaObtener}",
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
                    list = jResult["List"].Children().Select(t => new UiArea
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString(),
                        IsActive = Convert.ToBoolean(t["IsActive"])
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        #endregion

        #region Asentamientos

        public List<UiAsentamiento> ObtenerAsentamientos(UiAsentamiento model)
        {
            var list = new List<UiAsentamiento>();
            var response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.AsentamientosObtener}", new
            {
                Item = new
                {
                    Estado = new { Identificador = model.Estado.Identificador },
                    Municipio = new { Identificador = model.Municipio.Identificador },
                    CodigoPostal = model.CodigoPostal
                }
            });
            if (response.Result.IsSuccessStatusCode)
            {
                var query = response.Result.Content.ReadAsStringAsync().Result;
                var jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(p => new UiAsentamiento
                    {
                        Identificador = Convert.ToInt32(p["Identificador"]),
                        Nombre = p["Nombre"].ToString(),
                        Municipio = new UiMunicipio { Identificador = Convert.ToInt32(p["Municipio"]["Identificador"]) },
                        Estado = new UiEstado { Identificador = Convert.ToInt32(p["Estado"]["Identificador"]) },
                        CodigoPostal = p["CodigoPostal"].ToString()
                    }).ToList();
                }
            }
            return list;
        }
        #endregion

        #region TIPOS CONTACTO

        public List<UiTipoContacto> TiposContacto()
        {
            List<UiTipoContacto> list = new List<UiTipoContacto>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TipoContactoObtener}",new{});
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiTipoContacto
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        #endregion

        #region TIPOS TELEFONO
        public List<UiTipoTelefono> TiposTelefono()
        {
            List<UiTipoTelefono> list = new List<UiTipoTelefono>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TipoTelefonoObtener}", new { });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                if (Convert.ToBoolean(jResult["Success"]))
                {
                    list = jResult["List"].Children().Select(t => new UiTipoTelefono
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Name = t["Nombre"].ToString()
                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }
        #endregion
    }
}