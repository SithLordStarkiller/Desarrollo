using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GOB.SPF.ConecII.Web.Models;
using System.Net.Http;
using System.Configuration;
using System.Globalization;
using GOB.SPF.ConecII.Web.Resources;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace GOB.SPF.ConecII.Web.Servicios
{
    public class ServicesContraprestacion : IService
    {
        private int pages { get; set; }
        internal int Pages { get { return pages; } }
        internal HttpClient client;
        string urlApiContraprestacion;

        public ServicesContraprestacion()
        {
            urlApiContraprestacion = ConfigurationManager.AppSettings[ResourceAppSettings.URLApiContraprestacion];
            client = new HttpClient();
        }

        #region Cetes
        public List<UiCete> ProcesarArchivoCetes(UiDocumento documento)
        {
            var obj = new
            {
                Archivo = new
                {
                    Base64 = documento.Base64
                },
                Paging = new { All = false, CurrentPage = 1, Rows = 1 }
            };

            List<UiCete> list = new List<UiCete>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiContraprestacion}{MethodApiContraprestacion.CetesPrevisualizar}", obj);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                var success = Convert.ToBoolean(jResult["Success"]);
                if (success)
                {
                    list = jResult["List"].Children().Select(t =>
                    {
                        var cete = new UiCete();
                        cete.Identificador = Convert.ToInt32(t["Identificador"]);
                        cete.Fecha = Convert.ToDateTime(t["Fecha"]);
                        cete.TasaRendimiento = Convert.ToDecimal(t["TasaRendimiento"]);
                        return cete;
                    }).OrderBy(t => t.Fecha).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
                else
                {
                    string mensaje = jResult["Errors"].Children().Select(t => t["Message"].ToString()).First();
                    throw new ConecWebException(mensaje);
                }
            }
            return list;
        }

        public bool SaveCete(List<UiCete> saveList)
        {
            bool success = false;

            var obj = new
            {
                Lista = saveList.Select(p => new
                {
                    Fecha = p.Fecha,
                    TasaRendimiento = p.TasaRendimiento
                }),
                Paging = new { All = false, CurrentPage = 1, Rows = 1 }
            };
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiContraprestacion}{MethodApiContraprestacion.CetesGuardar}", obj);

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

        public List<UiCete> CetesObtenerPorCriterio(UiCeteFiltro model, int page, int rows)
        {
            var list = new List<UiCete>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiContraprestacion}{MethodApiContraprestacion.CetesObtenerPorCriterio}",
                new
                {
                    Criterio = new
                    {
                        FechaInicial = model.FechaInicial,
                        FechaFinal = model.FechaFinal,
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
                    list = jResult["List"].Children().Select(t =>
                    {
                        var cete = new UiCete();
                        cete.Identificador = Convert.ToInt32(t["Identificador"]);
                        cete.Fecha = Convert.ToDateTime(t["Fecha"]);
                        cete.TasaRendimiento = Convert.ToDecimal(t["TasaRendimiento"]);
                        return cete;
                    }).OrderBy(t => t.Fecha).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
            }
            return list;
        }

        public List<UiAnio> ObtenerAniosCetes()
        {
            /* A partir del año 1975 al año actual */
            List<UiAnio> result = new List<UiAnio>();
            for (int i = 1975; i <= DateTime.Now.Year; i++)
            {
                result.Add(new UiAnio()
                {
                    Identificador = i,
                    Name = i.ToString()
                });
            }

            return result;
        }

        public List<UiMes> ObtenerMeses()
        {
            List<UiMes> result = new List<UiMes>();

            for (int i = 1; i <= 12; i++)
            {
                result.Add(new UiMes()
                {
                    Identificador = i,
                    Name = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i)
                });
            }

            return result;
        }
        #endregion

        #region Enteros Tesofe
        public List<UiEnteroTesofe> ProcesarArchivoEnterosTesofe(UiDocumento documento)
        {
            var obj = new
            {
                Archivo = new
                {
                    Base64 = documento.Base64
                },
                Paging = new { All = false, CurrentPage = 1, Rows = 1 }
            };

            List<UiEnteroTesofe> list = new List<UiEnteroTesofe>();
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiContraprestacion}{MethodApiContraprestacion.TesofePrevisualizar}", obj);
            if (response.Result.IsSuccessStatusCode)
            {
                string query = response.Result.Content.ReadAsStringAsync().Result;

                JObject jResult = JObject.Parse(query);
                var success = Convert.ToBoolean(jResult["Success"]);
                if (success)
                {
                    list = jResult["List"].Children().Select(t =>
                    {
                        var tesofe = new UiEnteroTesofe();

                        tesofe.NumeroOperacion = Convert.ToInt64(t["NumeroOperacion"]);
                        tesofe.RFC = t["RFC"].ToString();
                        tesofe.CURP = t["CURP"].ToString();
                        tesofe.RazonSocial = t["RazonSocial"].ToString();
                        tesofe.FechaPresentacion = Convert.ToDateTime(t["FechaPresentacion"]);
                        tesofe.Sucursal = Convert.ToInt32(t["Sucursal"]);
                        tesofe.LlavePago = t["LlavePago"].ToString();
                        tesofe.Banco = t["Banco"].ToString();
                        tesofe.MedioRecepcion = t["MedioRecepcion"].ToString();
                        tesofe.Dependencia = t["Dependencia"].ToString();
                        tesofe.Periodo = t["Periodo"].ToString();
                        tesofe.SaldoFavor = Convert.ToDecimal(t["SaldoFavor"]);
                        tesofe.Importe = Convert.ToDecimal(t["Importe"]);
                        tesofe.ParteActualizada = Convert.ToDecimal(t["ParteActualizada"]);
                        tesofe.Recargos = Convert.ToDecimal(t["Recargos"]);
                        tesofe.MultaCorreccion = Convert.ToDecimal(t["MultaCorreccion"]);
                        tesofe.Compensacion = Convert.ToDecimal(t["Compensacion"]);
                        tesofe.CantidadPagada = Convert.ToDecimal(t["CantidadPagada"]);
                        tesofe.ClaveReferenciaDPA = Convert.ToInt32(t["ClaveReferenciaDPA"]);
                        tesofe.CadenaDependencia = t["CadenaDependencia"].ToString();
                        tesofe.ImporteIVA = t["ImporteIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["ImporteIVA"]);
                        tesofe.ParteActualizadaIVA = t["ParteActualizadaIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["ParteActualizadaIVA"]);
                        tesofe.RecargosIVA = t["RecargosIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["RecargosIVA"]);
                        tesofe.MultaCorreccionIVA = t["MultaCorreccionIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["MultaCorreccionIVA"]);
                        tesofe.CantidadPagadaIVA = Convert.ToDecimal(t["CantidadPagadaIVA"]);
                        tesofe.TotalEfectivamentePagado = Convert.ToDecimal(t["TotalEfectivamentePagado"]);

                        return tesofe;
                    }).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
                }
                else
                {
                    string mensaje = jResult["Errors"].Children().Select(t => t["Message"].ToString()).First();
                    throw new ConecWebException(mensaje);
                }
            }
            return list;
        }

        public bool SaveEntero(List<UiEnteroTesofe> saveList)
        {
            bool success = false;

            var obj = new
            {
                Lista = saveList.Select(p => new
                {
                    NumeroOperacion = p.NumeroOperacion,
                    RFC = p.RFC,
                    CURP = p.CURP,
                    RazonSocial = p.RazonSocial,
                    FechaPresentacion = p.FechaPresentacion,
                    Sucursal = p.Sucursal,
                    LlavePago = p.LlavePago,
                    Banco = p.Banco,
                    MedioRecepcion = p.MedioRecepcion,
                    Dependencia = p.Dependencia,
                    Periodo = p.Periodo,
                    SaldoFavor = p.SaldoFavor,
                    Importe = p.Importe,
                    ParteActualizada = p.ParteActualizada,
                    Recargos = p.Recargos,
                    MultaCorreccion = p.MultaCorreccion,
                    Compensacion = p.Compensacion,
                    CantidadPagada = p.CantidadPagada,
                    ClaveReferenciaDPA = p.ClaveReferenciaDPA,
                    CadenaDependencia = p.CadenaDependencia,
                    ImporteIVA = p.ImporteIVA,
                    ParteActualizadaIVA = p.ParteActualizadaIVA,
                    RecargosIVA = p.RecargosIVA,
                    MultaCorreccionIVA = p.MultaCorreccionIVA,
                    CantidadPagadaIVA = p.CantidadPagadaIVA,
                    TotalEfectivamentePagado = p.TotalEfectivamentePagado,
                }),
                Paging = new { All = false, CurrentPage = 1, Rows = 1 }
            };
            Task<HttpResponseMessage> response = ServiceClient.Client.PostAsJsonAsync($"{urlApiContraprestacion}{MethodApiContraprestacion.TesofeGuardar}", obj);

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

        public List<UiEnteroTesofe> EnterosObtenerPorCriterio(UiEnteroTesofeFiltro model, int page, int rows)
        {
            var list = new List<UiEnteroTesofe>();

            Task<HttpResponseMessage> response =
                ServiceClient.Client.PostAsJsonAsync(
                    $"{urlApiContraprestacion}{MethodApiContraprestacion.TesofeObtenerPorCriterio}",
                    new
                    {
                        Criterio = new
                        {
                            FechaPresentacionInicial = model.FechaPresentacionInicial,
                            FechaPresentacionFinal = model.FechaPresentacionFinal,
                            ClaveReferenciaDPA = model.ClaveReferenciaDPA,
                            LlavePago = model.LlavePago,
                            NumeroOperacion = model.NumeroOperacion,
                            RFC = model.RFC,
                            RazonSocial = model.RazonSocial
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
                    list = jResult["List"].Children().Select(t =>
                    {
                        var entero = new UiEnteroTesofe();

                        entero.NumeroOperacion = Convert.ToInt64(t["NumeroOperacion"]);
                        entero.RFC = t["RFC"].ToString();
                        entero.CURP = t["CURP"].ToString();
                        entero.RazonSocial = t["RazonSocial"].ToString();
                        entero.FechaPresentacion = Convert.ToDateTime(t["FechaPresentacion"]);
                        entero.Sucursal = Convert.ToInt32(t["Sucursal"]);
                        entero.LlavePago = t["LlavePago"].ToString();
                        entero.Banco = t["Banco"].ToString();
                        entero.MedioRecepcion = t["MedioRecepcion"].ToString();
                        entero.Dependencia = t["Dependencia"].ToString();
                        entero.Periodo = t["Periodo"].ToString();
                        entero.SaldoFavor = Convert.ToDecimal(t["SaldoFavor"]);
                        entero.Importe = Convert.ToDecimal(t["Importe"]);
                        entero.ParteActualizada = Convert.ToDecimal(t["ParteActualizada"]);
                        entero.Recargos = Convert.ToDecimal(t["Recargos"]);
                        entero.MultaCorreccion = Convert.ToDecimal(t["MultaCorreccion"]);
                        entero.Compensacion = Convert.ToDecimal(t["Compensacion"]);
                        entero.CantidadPagada = Convert.ToDecimal(t["CantidadPagada"]);
                        entero.ClaveReferenciaDPA = Convert.ToInt32(t["ClaveReferenciaDPA"]);
                        entero.CadenaDependencia = t["CadenaDependencia"].ToString();
                        entero.ImporteIVA = t["ImporteIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["ImporteIVA"]);
                        entero.ParteActualizadaIVA = t["ParteActualizadaIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["ParteActualizadaIVA"]);
                        entero.RecargosIVA = t["RecargosIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["RecargosIVA"]);
                        entero.MultaCorreccionIVA = t["MultaCorreccionIVA"] == null ? null : (decimal?)Convert.ToDecimal(t["MultaCorreccionIVA"]);
                        entero.CantidadPagadaIVA = Convert.ToDecimal(t["CantidadPagadaIVA"]);
                        entero.TotalEfectivamentePagado = Convert.ToDecimal(t["TotalEfectivamentePagado"]);

                        return entero;
                    }).ToList();

                    pages = Convert.ToInt32(jResult["Page"].SelectToken("Pages"));
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