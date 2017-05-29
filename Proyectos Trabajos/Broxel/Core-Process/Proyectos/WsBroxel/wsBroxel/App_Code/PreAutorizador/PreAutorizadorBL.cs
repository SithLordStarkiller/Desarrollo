using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.PreAutorizador;
using wsBroxel.wsOperaciones;


namespace wsBroxel.App_Code.PreAutorizador
{
    /// <summary>
    /// Lógica del negocio del preautorizador
    /// </summary>
    public class PreAutorizadorBL
    {
        /// <summary>
        /// Alta/baja de cuentas en grupos del preautorizador
        /// </summary>
        /// <param name="idUsuario">Usuario Broxel Online</param>
        /// <param name="cuenta">Cuenta a agregar/ eliminar</param>
        /// <param name="accion">1 Alta, 2 Baja</param>
        /// <param name="idGrupo">id de Grupo</param>
        /// <param name="key">encriptado</param>
        /// <param name="reglas"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PreAutResponse AltaBajaCuentaGrupo(int idUsuario, string cuenta, string key, List<Reglas> reglas)
        {
            try
            {

                using (var ctx = new broxelco_rdgEntities())
                {

                    //------------------------------------------------------------------------------------------------------------------//

                    var tar = Helper.GetTarjetaFromCuenta(cuenta);

                    if (tar == null)
                        throw new Exception("La cuenta es inexistente");

                    var request = CreateABRequest("broxel_controls", key, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        tar.NumeroTarjeta, reglas);

                    var url = ConfigurationManager.AppSettings["PreAutURL"];

                    var response = InvokeApi(request, url);

                    if (response == "")
                        throw new Exception("No existio respuesta por parte del preautorizador");

                    var apiRes = GetApiResponse(response);

                    //------------------------------------------------------------------------------------------------------------------//

                    if (apiRes.ProcessingCode == 100)
                    {
                        foreach (var item in reglas)
                        {

                            var log = new logAltaBajaCuentaGrupo
                            {
                                accion = (sbyte?)(item.Acciones == "A" ? 1 : 2),
                                cuenta = cuenta,
                                idUsuario = idUsuario,
                                fechaCreacion = DateTime.Now,
                                idGrupo = item.idGrupo,
                                fechaRespuesta = DateTime.Now,
                                processingCode = apiRes.ProcessingCode,
                                trackingNumber = apiRes.TrackingNumber,
                                descripcion = apiRes.Descripcion
                            };
                            ctx.logAltaBajaCuentaGrupo.Add(log);
                            ctx.SaveChanges();
                        }


                    }

                    //ctx.Entry(log).State = EntityState.Modified;
                    //ctx.SaveChanges();
                    return new PreAutResponse
                    {
                        IdTransaccion = apiRes.ProcessingCode,
                        Message = apiRes.Descripcion
                    };

                }
            }
            catch (Exception e)
            {
                return new PreAutResponse
                {
                    IdTransaccion = 0,
                    Message = "Ocurrio un error al añadir o eliminar al grupo" + e.Message
                };

            }
        }

        /// <summary>
        /// Obtener grupos preautorizador
        /// </summary>
        /// <returns></returns>
        public List<GruposPreAut> GetGruposPreAut()
        {
            return new MySqlDataAccess().GetGruposPreAut();
        }

        private string CreateABRequest(string usuario, string key, string fechaHora, string tarjeta, List<Reglas> regla)
        {
            //Declaramos objectos
            var p = new Principal();
            var e = new Ejemplo();
            var acc = new List<Acciones>();
            var req = new RequestObject();
            var r = new RequestObject();


            e.fechaHora = fechaHora;
            e.key = key;
            e.usuario = usuario;

            r.tarjeta = tarjeta;

            //Agregamos acciones y idGrupos al objeto
            foreach(var item in regla)
            {
                var a = new Acciones {accion = item.Acciones, id_grupo = item.idGrupo};
                acc.Add(a);
            }

            r.acciones = acc;
            req = r;
            p.requestObject = req;
            p.authData = e;

            //Serializamos el objeto
            var recibe = new JavaScriptSerializer();
            var request = recibe.Serialize(p);

            //Retornamos el request serializado
            return request;
        }

        /// <summary>
        /// Invocadora de API Rest
        /// </summary>
        /// <param name="msg">Mensaje</param>
        /// <param name="url">Url del servicio REST</param>
        /// <returns></returns>
        private string InvokeApi(String msg, String url)
        {
            Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " InvokeApi inicio");
            string responseString = "";
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " url->"+url);
                    Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " msg->" + msg);

                    responseString = client.UploadString(url, "POST", msg);
                    Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " InvokeApi ->  respuesta del api-preautorizador:" + responseString);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " InvokeApi error->" + e.Message);
                responseString = "";
            }
            Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "  InvokeApi fin");
            return responseString;
        }

        private PreAutApiResponse GetApiResponse(string response)
        {
            PreAutApiResponse res = null;
            try
            {
                Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "  GetApiResponse response:>" + response);
                var recibe = new JavaScriptSerializer();
                var jsonrecibe = recibe.Deserialize<dynamic>(response);
                
                res = new PreAutApiResponse
                {
                    Descripcion = jsonrecibe["description"],
                    ProcessingCode = Convert.ToInt32(jsonrecibe["processingCode"]),
                    TrackingNumber = Convert.ToInt32(jsonrecibe["trackingNumber"])
                };
            }
            catch (Exception e)
            {
                Trace.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "  GetApiResponse error->" + e.Message);
                res = null;
            }
            return res;
        }


    }
}