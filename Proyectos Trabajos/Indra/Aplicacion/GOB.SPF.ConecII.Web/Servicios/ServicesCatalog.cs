using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace GOB.SPF.ConecII.Web.Servicios
{
    internal class ServicesCatalog : IService
    {
        private int pages { get; set; }

        internal int Pages { get { return pages; } }

        internal HttpClient client;
        string apiCatalogClient;
        internal ServicesCatalog()
        {
            apiCatalogClient = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiCatalog];
            client = new HttpClient();
        }

        #region DIVISIONES
        public List<UiDivision> ObtenerDivisiones(int page, int rows)
        {
            List<UiDivision> list = new List<UiDivision>();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.DivisionesObtener}",
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

        public List<UiTiposServicio> ObtenerTiposServicio(int page, int rows)
        {
            List<UiTiposServicio> list = new List<UiTiposServicio>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.TiposServicioObtener}",
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
            }
            return success;
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
            }
            return success;
        }

        #endregion

        #region DEPENDENCIAS

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
                    list = jResult["List"].Children().Select(t => new UiReferencia
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        ClaveReferencia = Convert.ToInt32(t["ClaveReferencia"]),
                        Descripcion = t["Descripcion"].ToString(),
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
            List<UiGastosInherente> list = new List<UiGastosInherente>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.GastosInherenteObtenerPorCriterio}",
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
            }
            return success;

        }
        #endregion

        #region FACTOR
        public List<UiFactor> ObtenerFactor(int page, int rows)
        {
            List<UiFactor> list = new List<UiFactor>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorObtener}",
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
                        CuotaFactor = Convert.ToDecimal(t["CuotaFactor"]),
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
                        CuotaFactor = Convert.ToDecimal(t["CuotaFactor"]),
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
                        CuotaFactor = model.CuotaFactor,
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
                        CuotaFactor = Convert.ToDecimal(t["CuotaFactor"]),
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
            List<UiFactorEntidadFederativa> list = new List<UiFactorEntidadFederativa>(); ;
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
                    list = jResult["List"].Children().Select(t => new UiFactorEntidadFederativa
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificadorFactor = Convert.ToInt32(t["Clasificacion"].SelectToken("Identificador")),
                        ClasificadorFactor = t["Clasificacion"].SelectToken("Nombre").ToString(),
                        IdFactor = Convert.ToInt32(t["Factor"].SelectToken("Identificador")),
                        Factor = t["Factor"].SelectToken("Nombre").ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        Estados = t["Estados"].Select(tt=> new UiEstado { Identificador = Convert.ToInt32(tt["Identificador"]), Nombre = tt["Nombre"].ToString() }).ToList(),
                        IsActive = Convert.ToBoolean(t["Activo"]),
                        EntidadesFederativas = String.Join(",",t["Estados"].Select(tt=> tt["Nombre"].ToString()))

                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiFactorEntidadFederativa> FactorEntidadFederativaObtenerPorCriterio(int page, int rows, UiFactorEntidadFederativa model)
        {
            List<UiFactorEntidadFederativa> list = new List<UiFactorEntidadFederativa>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorEntidadFederativaObtenerPorCriterio}",
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
                    list = jResult["List"].Children().Select(t => new UiFactorEntidadFederativa
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdClasificadorFactor = Convert.ToInt32(t["IdClasificadorFactor"]),
                        ClasificadorFactor = t["ClasificadorFactor"].ToString(),
                        IdFactor = Convert.ToInt32(t["IdFactor"]),
                        Factor = t["Factor"].ToString(),
                        Descripcion = t["Descripcion"].ToString(),
                        IdEntidFed = Convert.ToInt32(t["IdEntidFed"]),
                        IsActive = Convert.ToBoolean(t["Activo"])

                    }).ToList();
                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
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
                        IdClasificacion = model.IdClasificadorFactor,
                        IdFactor = model.IdFactor,
                        Descripcion = model.Descripcion,
                        Estados = model.Estados.Select(e=>new { identificador = e.Identificador })                    },

                });
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                success = Convert.ToBoolean(jResult["Success"]);
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
            }
            return success;
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

                }
            }
            return entidad;
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
                        Factor = Convert.ToDecimal(t["Factor"]),
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
                    list = jResult["List"].Children().Select(t => new UiFactorLeyIngreso
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        IdAnio = Convert.ToInt32(t["IdAnio"]),
                        Anio = t["Anio"].ToString(),
                        IdMes = Convert.ToInt32(t["IdMes"]),
                        Mes = t["Mes"].ToString(),
                        Factor = Convert.ToDecimal(t["Factor"]),
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
                        Factor = model.Factor
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
            }
            return success;
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
                    }).OrderBy(t=>t.Nombre).ToList();
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
                    string mensaje = jResult["Errors"].Children().Select(t =>t["Message"].ToString()).First();
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


        #region FACTOR MUNICIPIO
        public UiFactorMunicipio ObtenerFactorMunicipio(UiPaging paging)
        {
            UiFactorMunicipio entidad = new UiFactorMunicipio();
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactoresMunicipioObtener}",
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

                    entidad.Clasificaciones = jResult["Entity"]
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

                }
            }
            return entidad;
        }

        public bool FactorMunicipioGuardar(UiFactorMunicipio model)
        {
            bool success = false;

            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.FactorMunicipioGuardar}",
                new
                {
                    FactorMunicipioDTO = new
                    {                        
                        IdClasificacion = model.IdClasificacion,
                        IdFactor = model.IdFactor,
                        Descripcion = model.Descripcion,
                        IdEstado = model.IdEstado,
                        Municipios = model.Municipios.Select(m=>new { Identificador = m.Identificador })
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
                        Nombre = t["Nombre"].ToString(),
                    }).ToList();
                }

            }
            return list;
        }

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

        public List<UiCuota> CuotasObtenerPorCriterio(int page, int rows, UiCuota model)
        {
            List<UiCuota> list = new List<UiCuota>(); ;
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.CuotaObtenerPorCriterio}",
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
            }
            return success;

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
                        IsActive = Convert.ToBoolean(t["Activo"])
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
                        IsActive = Convert.ToBoolean(t["Activo"])

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
                    list = jResult["List"].Children().Select(t => new UiTiposDocumento
                    {
                        Identificador = Convert.ToInt32(t["Identificador"]),
                        Descripcion = t["Descripcion"].ToString(),
                        Name = t["Nombre"].ToString(),
                        Confidencial = Convert.ToBoolean(t["Confidencial"]),
                        IsActive = Convert.ToBoolean(t["Activo"])
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
                success = Convert.ToBoolean(jResult["Sucess"]);
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
                        Tiempo = Convert.ToDecimal(t["Tiempo"]),
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
                        Tiempo = Convert.ToDecimal(t["Tiempo"]),
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
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto}",
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
                        Tiempo = Convert.ToDecimal(t["Tiempo"]),
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
                        Identificador = Convert.ToInt32(t["Idintegrante"]),
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
            Task<HttpResponseMessage> response = client.PostAsJsonAsync($"{apiCatalogClient}{MethodApiCatalog.IntegrantesObtenerPorCriterio}",
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

                        Identificador = Convert.ToInt32(t["Idintegrante"]),
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
    }
}