using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Negocios;

namespace PubliPayments.Controllers
{
    public class EstatusServiciosController : Controller
    {
        ProcesoMonitor monitor = new ProcesoMonitor();
        string _servicioFormiik = ConfigurationManager.AppSettings["ServicioFormiik"]; // "Us3rPPMC$1";
        //
        // GET: /EstatusServicios/cbdccbacba
      
        public ActionResult Index()
        {
            var lista = new List<Func<EstatusServiciosModel>>
            {
                MonitoreoBaseDatos,
                MonitoreoWebServiceAddress,
                MonitoreoWebServiceWsdl,
                MonitoreoWebServiceEcho,
                MonitoreoSftp,
                MonitoreoNubeMovilAddress,
                MonitoreoNubeMovilWsdl,
                MonitoreoNubeMovilAddress1,
                MonitoreoNubeMovilWsdl1,
                MonitoreoRespuestaAddress,
                MonitoreoRespuestaWsdl,
                MonitoreoBaseDatosUltima,
                MonitoreoBaseDatosRecibida,
                MonitoreoSincronizando,
                MonitoreoUltimaActGestionMovil
            };

            var listaResultado =  new EstatusServiciosModel[lista.Count];

            Parallel.For(0, lista.Count, i => 
            {
                var func = lista[i];
                var sw = Stopwatch.StartNew();
                listaResultado[i] = func();
                listaResultado[i].Tiempo = sw.ElapsedMilliseconds;
            });

            var estatusModel = new List<EstatusServiciosModel>(listaResultado);

            return View(estatusModel);
        }

        public ActionResult Monitoreo()
        {
            var lista = new List<Func<EstatusServiciosModel>>
            {
                MonitoreoBaseDatos,
                MonitoreoWebServiceAddress,
                MonitoreoWebServiceWsdl,
                MonitoreoWebServiceEcho,
                MonitoreoSftp,
                MonitoreoNubeMovilAddress,
                MonitoreoNubeMovilWsdl,
                MonitoreoNubeMovilAddress1,
                MonitoreoNubeMovilWsdl1,
                MonitoreoRespuestaAddress,
                MonitoreoRespuestaWsdl,
                MonitoreoBaseDatosUltima,
                MonitoreoBaseDatosRecibida
            };

            var listaResultado = new EstatusServiciosModel[lista.Count];

            Parallel.For(0, lista.Count, i =>
            {
                var func = lista[i];
                var sw = Stopwatch.StartNew();
                listaResultado[i] = func();
                listaResultado[i].Tiempo = sw.ElapsedMilliseconds;
            });

            var estatusModel = new List<EstatusServiciosModel>(listaResultado);

            var contenido = new StringBuilder();
            foreach (var estatus in estatusModel)
            {
                contenido.Append("[");
                if (estatus.Resultado.StartsWith("OK"))
                    contenido.Append(4);
                else if (estatus.Resultado.StartsWith("W:"))
                    contenido.Append(2);
                else contenido.Append(0);
                contenido.Append("]");
            }

            return Content(contenido.ToString());
        }

        public ActionResult MonitoreoT()
        {
            var lista = new List<Func<EstatusServiciosModel>>
            {
                MonitoreoBaseDatos,
                MonitoreoWebServiceAddress,
                MonitoreoWebServiceWsdl,
                MonitoreoWebServiceEcho,
                MonitoreoSftp,
                MonitoreoNubeMovilAddress,
                MonitoreoNubeMovilWsdl,
                MonitoreoNubeMovilAddress1,
                MonitoreoNubeMovilWsdl1,
                MonitoreoRespuestaAddress,
                MonitoreoRespuestaWsdl,
                MonitoreoBaseDatosUltima,
                MonitoreoBaseDatosRecibida
            };

            var listaResultado = new EstatusServiciosModel[lista.Count];

            Parallel.For(0, lista.Count, i =>
            {
                var func = lista[i];
                var sw = Stopwatch.StartNew();
                listaResultado[i] = func();
                listaResultado[i].Tiempo = sw.ElapsedMilliseconds;
            });

            var estatusModel = new List<EstatusServiciosModel>(listaResultado);

            var contenido = new StringBuilder();
            foreach (var estatus in estatusModel)
            {
                contenido.Append("[");
                contenido.Append(estatus.Tiempo);
                contenido.Append("]");
            }

            return Content(contenido.ToString());
        }

        /// <summary>
        /// Página para el monitoreo operativo
        /// </summary>
        /// <returns>Regresa el modelo de la vista </returns>
        public ActionResult MonitoreoOperativo()
        {
            var swTotal = Stopwatch.StartNew();
            var ent = new EntMonitoreo(Config.AplicacionActual().idAplicacion);

            var lista = ent.ObtenerMonitoreos();

            var listaResultado = new EstatusServiciosModel[lista.Count];

            Parallel.For(0, lista.Count, i =>
            {
                Monitoreo monitoreo = lista[i];
                var sw = Stopwatch.StartNew();
                var resultado = new EstatusServiciosModel();
                try
                {
                    int res = EjecutarMonitoreo(ref resultado, monitoreo);
                    if (res > monitoreo.Error && !monitoreo.SoloWarning)
                    {
                        resultado.Resultado = "Error: " + res.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        if (res > monitoreo.Warning || res > monitoreo.Error)
                        {
                            resultado.Resultado = "Warning: " + res.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            if (res < 0 && monitoreo.ErrorNegativo)
                            {
                                resultado.Resultado = "Error: " + res.ToString(CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                resultado.Resultado = res.ToString(CultureInfo.InvariantCulture);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultado.Resultado = "Error: " + ex.Message;
                }
                sw.Stop();
                resultado.Tiempo = sw.ElapsedMilliseconds;
                listaResultado[i] = resultado;
            });

            var estatusModel = new List<EstatusServiciosModel>(listaResultado);
            swTotal.Stop();
            ViewBag.TiempoTotal = swTotal.ElapsedMilliseconds;

            return View(estatusModel);
        }

        public int EjecutarMonitoreo(ref EstatusServiciosModel result, Monitoreo monitoreo)
        {
            result.Servicio = monitoreo.Nombre;
            return monitor.EjecutarStoredProcedure(monitoreo.Ejecutar);
        }

        private EstatusServiciosModel MonitoreoWebServiceAddress()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "WebService Gestiones Infonavit - Address";
                var direccion = getClientAddress("WSCSIBM");
                var r = ObtenerDireccionWeb(direccion);
                estatus.Resultado = r != "" ? "OK" : "FAIL";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoWebServiceWsdl()
        {
            var estatus = new EstatusServiciosModel();

            //WebService WSDL
            try
            {
                estatus.Servicio = "WebService Gestiones Infonavit - WSDL";
                var direccion = getClientAddress("WSCSIBM");
                var r = ObtenerDireccionWeb(direccion + "?WSDL");
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoWebServiceEcho()
        {
            var estatus = new EstatusServiciosModel();

            //WebService ECHO
            try
            {
                estatus.Servicio = "WebService Gestiones Infonavit - ECHO";
                Gestiones.TestWsGestiones();
                estatus.Resultado = "OK";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoBaseDatos()
        {
            var estatus = new EstatusServiciosModel();
            //Base de datos
            try
            {
                estatus.Servicio = "Base de datos";
                var resultado = string.Empty;
                resultado = monitor.MonitoreoBaseDatos();
                estatus.Resultado = resultado;
                
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoNubeMovilAddress()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "Azure Movil Address";
                var direccion = getClientAddress("BasicHttpBinding_IBackEnd");
                var r = ObtenerDireccionWeb(direccion);
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoNubeMovilWsdl()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "Azure Movil WSDL";
                var direccion = getClientAddress("BasicHttpBinding_IBackEnd");
                var r = ObtenerDireccionWeb(direccion + "?WSDL");
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoNubeMovilAddress1()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "Azure Movil Address1";
                var direccion = getClientAddress("BasicHttpBinding_IBackEnd1");
                var r = ObtenerDireccionWeb(direccion);
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoNubeMovilWsdl1()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "Azure Movil WSDL1";
                var direccion = getClientAddress("BasicHttpBinding_IBackEnd1");
                var r = ObtenerDireccionWeb(direccion + "?WSDL");
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoRespuestaAddress()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "WebService Respuesta Address";
                var r = ObtenerDireccionWeb(_servicioFormiik);
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoRespuestaWsdl()
        {
            var estatus = new EstatusServiciosModel();

            //WebService Address
            try
            {
                estatus.Servicio = "WebService Respuesta WSDL";
                var r = ObtenerDireccionWeb(_servicioFormiik + "?WSDL");
                estatus.Resultado = r != "" ? "OK" : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private string getClientAddress(string name)
        {
            var section = (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client");
            string epServicio = "";
            for (int i = 0; i < section.Endpoints.Count; i++)
            {
                if (section.Endpoints[i].Name == name)
                {
                    epServicio = section.Endpoints[i].Address.ToString();
                    break;
                }
            }
            return epServicio;
        }

        private EstatusServiciosModel MonitoreoBaseDatosUltima()
        {
            var estatus = new EstatusServiciosModel();

            //Base de datos
            try
            {
                estatus.Servicio = "Último envío a la nube";
                var resultado = string.Empty;
                resultado = monitor.MonitoreoBaseDatosUltima();
                estatus.Resultado = resultado;
                
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoBaseDatosRecibida()
        {
            var estatus = new EstatusServiciosModel();

            //Base de datos
            try
            {
                estatus.Servicio = "Última respuesta recibida";
                var resultado = string.Empty;
                resultado = monitor.MonitoreoBaseDatosRecibida();
                estatus.Resultado = resultado;
            
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoSincronizando()
        {
            var estatus = new EstatusServiciosModel();
            //Base de datos
            try
            {
                estatus.Servicio = "Número de órdenes en sincronizando";
                var resultado = string.Empty;
                resultado = monitor.MonitoreoSincronizando();
                estatus.Resultado = resultado;
            
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoUltimaActGestionMovil()
        {
            var estatus = new EstatusServiciosModel();

            //Base de datos
            try
            {
                estatus.Servicio = "Última actualización Gestión Móvil";
                var resultado = string.Empty;
                resultado = monitor.MonitoreoUltimaActGestionMovil();
                estatus.Resultado = resultado;
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private EstatusServiciosModel MonitoreoSftp()
        {
            var estatus = new EstatusServiciosModel();

            //Base de datos
            try
            {
                var Host = ConfigurationManager.AppSettings["sFTPhost"];
                var Username = ConfigurationManager.AppSettings["sFTPusername"];
                var Password = ConfigurationManager.AppSettings["sFTPpassword"];
                var RemoteDirectory = ConfigurationManager.AppSettings["sFTPRemoteDirectory"];
                var Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["sFTPPort"]);

                estatus.Servicio = "Repositorio de Archivos Infonavit - sFTP";
                var s = new SFTP();
                var resultado = s.CheckSftp(Host, Puerto, Username, Password, RemoteDirectory);
                estatus.Resultado = resultado == "OK" ? resultado : "Fallo";
            }
            catch (Exception ex)
            {
                estatus.Resultado = ex.Message;
            }

            return estatus;
        }

        private string ObtenerDireccionWeb(string direccion)
        {
            var resultado = "";

            ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;

            var wc = new  WebClientTimeOut {TimeOut = 10000}; //10 segundos de timeout

            Stream data = wc.OpenRead(direccion);
            if (data != null)
            {
                data.ReadTimeout = 10000; //10 segundos de timeout
                var reader = new StreamReader(data);
                resultado = reader.ReadToEnd();
                data.Close();
                reader.Close();
            }
            return resultado;
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
