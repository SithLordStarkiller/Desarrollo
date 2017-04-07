using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios
{
    public class Gestiones
    {
        public static void EnviarGestionBatch(List<Object> listaOrdenes, string origen)
        {
            var enviarGestion =
                new Task(() => EnviarGestionBatchTask(listaOrdenes, origen));
            enviarGestion.Start();
        }

        public static void EnviarGestion(int idOrden, string user, string origen)
        {
            var enviarGestion =
                        new Task(() => EnviarGestionTask(idOrden, user, origen));
            enviarGestion.Start();
        }

        public static void EnviarGestion(int idOrden, int idUsuario, string origen)
        {
            var enviarGestion =
                        new Task(() => EnviarGestionWrapper(idOrden, idUsuario, origen));
            enviarGestion.Start();
        }

        public static void EnviarGestionWrapper(int idOrden, int idUser, string origen)
        {
            try
            {
                String usuario = "";
                using (var ctx = new SistemasCobranzaEntities())
                {
                    var usuarioEncontrado = ctx.Usuario.FirstOrDefault(x => x.idUsuario == idUser);
                    if (usuarioEncontrado != null)
                        usuario = usuarioEncontrado.Usuario1;
                }
                EnviarGestionTask(idOrden, usuario, origen);
            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Gestiones", "Error: en EnviarGestionWrapper: " + e.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Excepción del envío de Gestión Orden No. " + idOrden
                            , "Fallo el envío de la orden " + idOrden + "\n" +
                            "Se atrapó la siguiente excepción: " + e.Message);
            }
        }

        public static void EnviarGestionBatchTask(List<Object> ordenes, string origen)
        {
            try
            {
                ConexionSql sql = ConexionSql.Instance;
                var result = new DataTable();

                if (ordenes.Count > 100)
                {

                    for (int i = 0; i < ordenes.Count; i += 100)
                    {
                        int limite = ordenes.Count - i > 100 ? i + 100 : ordenes.Count;
                        String[] subOrdenes = ordenes.GetRange(i, limite).Select(x => Convert.ToString((x))).ToArray();
                        DataSet resultTemp = sql.ObtenerListaEstados(subOrdenes);
                        DataTable tableTemp = resultTemp.Tables[0];
                        if (result.Rows.Count == 0) result = tableTemp;
                        else result.Merge(tableTemp);
                    }
                }
                else
                {
                    result = sql.ObtenerListaEstados(ordenes.Select(x => Convert.ToString((x))).ToArray()).Tables[0];
                }

                foreach (DataRow row in result.Rows)
                {
                    if (row["Estatus"].ToString() != "4") continue;

                    DataRow row1 = row;
                    var tareaEnvio = new Task(() => EnviarGestionTask(Int32.Parse(row1["idOrden"].ToString()), row1["Usuario"].ToString(), origen));
                    tareaEnvio.Start();

                    Thread.Sleep(2000); //Espero 2 segundos
                }

            }
            catch (Exception e)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Gestiones", "Error: EnviarGestionBatchTask - Falló envío de ordenes - excepción: " + e.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Excepción del envío de Gestión Ordenes",
                            "Fallo el envío de ordenes en batch.\nSe atrapó la siguiente excepción: " + e.Message);
            }
        }

        public static void EnviarGestionTask(int idOrden, string usuario, string origen)
        {
            int intento = 0;
            bool enviadoOk = false;
            while (intento <= 24 && !enviadoOk)
            {
                try
                {
                    EnviarGestionIntentoTask(idOrden, usuario, origen);
                    enviadoOk = true;
                }
                catch (Exception e)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "GestionesEnvio",
                        "Error: EnviarGestionTask - " + "Fallo envío - idOrden: " + idOrden + " - Int.Env: " +
                        intento + " - Error: " + e.Message);
                    //No se envia correo si es un internal server error o There was no endpoint listening at
                    if ((e.Message.IndexOf("500 Internal Server Error", StringComparison.Ordinal) < 1)
                        && (e.Message.IndexOf("There was no endpoint listening at", StringComparison.Ordinal) < 1)
                        && (e.Message.IndexOf("The request channel timed out while", StringComparison.Ordinal) < 1)
                        && (e.Message.IndexOf("Timeout expired.  The timeout period", StringComparison.Ordinal) < 1)
                        )
                        Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                            "Excepción del envío de Gestión Orden No. " + idOrden
                            , "Fallo el envío de la orden " + idOrden + " - Intento de envío:" + intento + "\n" +
                              "Se atrapó la siguiente excepción: " + e.Message);
                }
                intento++;
                if (!enviadoOk)
                    Thread.Sleep(intento <= 1 ? 600000 : 3600000);
            }
        }

        public static void EnviarGestionIntentoTask(int idOrden, string usuario, string origen)
        {
            ConexionSql cnn = ConexionSql.Instance;
            var ws = new wsGestiones.WSCSIBMClient();
            var datos = new List<String>();
            DataSet ds2 = cnn.ObtenerGestion(idOrden + "");
            DataTable gestion = ds2.Tables[0];
            var ruta = gestion.Rows[0]["CV_RUTA"].ToString();

            DataSet ds = new EntRespuestasWS().ObtenerRespuestasWS(idOrden,ruta);
            DataTable campos = ds.Tables[0];
         
            var numCred = gestion.Rows[0]["num_Cred"].ToString();
            datos.Add("1|" + numCred);
            datos.AddRange(from DataRow row in campos.Rows select row["Dato"].ToString());
            for (var i = 0; i < datos.Count; i++)
            {
                var n = datos[i];
                if (n.IndexOf("Lat:", StringComparison.Ordinal) > 0)
                {
                    datos[i] = n.Substring(0, n.IndexOf("Fecha:", StringComparison.Ordinal));
                }
                if (n.StartsWith("24|") || n.StartsWith("25|") || n.StartsWith("26|"))
                {
                    DateTime fecha = DateTime.ParseExact(n.Substring(3), "yyyyMMdd HH:mm:ss", CultureInfo.InvariantCulture);
                    datos[i] = n.Substring(0, 3) + fecha.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (n.StartsWith("91|"))
                {
                    DateTime fecha = DateTime.ParseExact(n.Substring(3), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    datos[i] = n.Substring(0, 3) + fecha.ToString("yyyy-MM-dd");
                }
                if (n.StartsWith("306|") || n.StartsWith("378|") || n.StartsWith("389|") || n.StartsWith("402|"))
                {
                    DateTime fecha = DateTime.ParseExact(n.Substring(4), "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    datos[i] = n.Substring(0, 4) + fecha.ToString("yyyy-MM-dd");
                }
                Trace.Write(datos[i] + ",");
            }
            Trace.WriteLine(".");
            var aEnviar = datos.ToArray();
            var fechaCaptura = DateTime.Now.ToString("yyyy-MM-dd");
            var despacho = gestion.Rows[0]["CV_DESPACHO"].ToString();
            var proveedor = gestion.Rows[0]["CV_PROVEEDOR"].ToString();

            var claveEstatusFinal = new EntDictamen().ObtenerDictamenWS(idOrden, ruta);
            //if (despacho.Length > 10)
            //    despacho = despacho.Substring(0, 10);
            var dat = string.Join(",", aEnviar);
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Gestiones",
                string.Format(
                    "Enviando a WS: numCredito={0},fecCap={1},idRuta=" + (ruta == "VBD" ? "VB" : "RDS-SOC") + ",despacho={2},usser={3},alterUssr={7},estado={4},movil={8},numMov={5},Datos={6}",
                    numCred, fechaCaptura, proveedor, despacho, origen, datos.Count, dat, usuario, claveEstatusFinal));
            var result = ws.insertaDatos(numCred, fechaCaptura, (ruta == "VBD" ? "VB": "RDS-SOC"), proveedor, despacho, usuario, origen, claveEstatusFinal, datos.Count, aEnviar);
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Gestiones",
                string.Format(
                    "Enviando a WS result = {0}", result));
            Trace.WriteLine(result);
            if (result.Equals("0000"))
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Gestiones", "Envío exitoso de órden " + idOrden);
            }
            else
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Gestiones", "Error: EnviarGestionTask - Falló envío de órden " + idOrden);
                if (!result.Trim().Equals("1020") && !result.Trim().Equals("9000"))
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error de envío de Gestión Orden No. " + idOrden
                            , "Falló el envío de la órden " + idOrden + "\n" +
                            "Servicio Web regresó la siguiente respuesta: " + result);
            }
        }

        public static void TestWsGestiones()
        {
            var direccion = GetClientAddress("WSCSIBM");
            var remoteAddress = new EndpointAddress(direccion);

            var myBinding = new BasicHttpBinding("WSCSIBMSoapBinding");
            myBinding.CloseTimeout = TimeSpan.FromMilliseconds(10000);
            myBinding.OpenTimeout = TimeSpan.FromMilliseconds(10000);
            myBinding.ReceiveTimeout = TimeSpan.FromMilliseconds(10000);
            myBinding.SendTimeout = TimeSpan.FromMilliseconds(10000);

            var ws = new wsGestiones.WSCSIBMClient(myBinding, remoteAddress);

            var result = ws.insertaDatos("", "", "", "", "", "", "", "", 0, null);
            if (result == null)
            {
                throw new ApplicationException("El resultado fue nulo");
            }
        }

        private static string GetClientAddress(string name)
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
    }
}
