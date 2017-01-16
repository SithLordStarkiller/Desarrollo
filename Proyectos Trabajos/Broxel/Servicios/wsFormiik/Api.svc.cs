using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Script.Serialization;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios;
using PubliPayments.Negocios.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments
{
    [ServiceBehavior(Namespace = Constantes.Namespace)]
    public class Api : IApi
    {
        readonly string _directorioLogeo = ConfigurationManager.AppSettings["DirectorioLog"];
        
        static Api()
        {
            ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            ConnectionDB.EstalecerConnectionString("SqlDefault", ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            ConnectionDB.EstalecerConnectionString("BroxelSMSs", ConfigurationManager.ConnectionStrings["BroxelSMSs"].ConnectionString);
            Inicializa.Inicializar(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            var serviciosDatos = new EntUsuariosServicios().ObtenerUsuariosServicios(1);
            var dicServicios = serviciosDatos.ToDictionary(x => x.Usuario, x => x.Password);
            Inicializa.InicializarEmail(dicServicios);

            //var t1 = new Task(() =>
            //{
            //    var bc = new BloqueoConcurrencia();
            //    var bcmodel = bc.BloquearConcurrencia(
            //        new BloqueoConcurrenciaModel
            //        {
            //            Aplicacion = "wsFormiik",
            //            Llave = "TEST06",
            //            Origen = "SendWorkOrderToClient"
            //        }, 30000, 1);

            //    if (bcmodel.Id > 0)
            //    {
            //        //Concurrencia OK
            //        if (!string.IsNullOrEmpty(bcmodel.Resultado))
            //        {
            //            //Si ya existe resultado lo regresa y ya no continua con el proceso
            //            return; //bcmodel.Resultado
            //        }
            //    }
            //    else
            //    {
            //        //Maneja el error en caso de que no sea valida la concurrencia
            //        return;// "Error"
            //    }

            //    //Continua con el proceso cuando termina actualiza el resultado
            //    Thread.Sleep(10000);

            //    bcmodel.Resultado = "OK";
            //    bc.ActualizarConcurrencia(bcmodel, 1);
            //    Debug.WriteLine("OK - 1");
            //});

            //var t2 = new Task(() =>
            //{
            //    var bc = new BloqueoConcurrencia();
            //    var bcmodel = bc.BloquearConcurrencia(
            //        new BloqueoConcurrenciaModel
            //        {
            //            Aplicacion = "wsFormiik",
            //            Llave = "TEST06",
            //            Origen = "SendWorkOrderToClient"
            //        }, 30000, 1);

            //    if (bcmodel.Id > 0)
            //    {
            //        //Concurrencia OK
            //        if (!string.IsNullOrEmpty(bcmodel.Resultado))
            //        {
            //            //Si ya existe resultado lo regresa y ya no continua con el proceso
            //            return; //bcmodel.Resultado
            //        }
            //    }
            //    else
            //    {
            //        //Maneja el error en caso de que no sea valida la concurrencia
            //        return;// "Error"
            //    }

            //    //Continua con el proceso cuando termina actualiza el resultado
            //    Thread.Sleep(10000);

            //    bcmodel.Resultado = "OK";
            //    bc.ActualizarConcurrencia(bcmodel, 1);
            //    Debug.WriteLine("OK - 2");
            //});
            ////test concurrencia
            //t1.Start();
            //t2.Start();

        }

        //Implementación ValidateUserSimple
        public string ValidateUserSimple(Stream streamUser)
        {
            Trace.WriteLine(string.Format("{0} - ValidateUserSimple", DateTime.Now));

            //Permite el ingreso al sistema por dispositivo. Devuelve string vacío si es un usuarío válido de lo contrario se 
            //retorna el error del acceso (p.e. "username no registrado", "password incorrecto").

            //Metodo que Ud. puede implmentar para validar a sus usuarios
            //el que se muestra es solo un demo

            StreamReader reader = new StreamReader(streamUser);
            string xmlUser = reader.ReadToEnd();

            XmlDocument xmlUserDoc = new XmlDocument();

            //Carga la cadena string a un XmlDoc
            xmlUserDoc.LoadXml(xmlUser);

            UsuarioDevice usuario = new UsuarioDevice();

            usuario.UserName = xmlUserDoc.GetElementsByTagName("username").Item(0).InnerText;
            usuario.Password = xmlUserDoc.GetElementsByTagName("password").Item(0).InnerText;

            return usuario.Login();

        }

        //Implementación SendWorkOrderToClient
        public Stream SendWorkOrderToClient(Stream respuesta)
        {
            Trace.WriteLine(string.Format("{0} - SendWorkOrderToClient", DateTime.Now));
            //Recibe las respuestas de Formiik en sus sistemas 
            //Devuelve string vacío si recibió la repuesta

            var workorderresponse = new WorkOrderResponse();

            //Carga un documento XML enviado por formiik con todas las propiedades de una respuesta
            //(ver objeto WorkOrderResponse
            workorderresponse.Load(respuesta);

            //A partir de este momento Ud. puede implementar algun metodo
            //para guardar las respuestas. El método que se muestra es solo un demo
            //return workorderresponse.Save();
            return workorderresponse.SaveFull();
        }

        //Implementación GetUserCatalog
        public string GetUserCatalog(Stream streamUser)
        {
            Trace.WriteLine(string.Format("{0} - GetUserCatalog", DateTime.Now));
            //Permite a Formiik recuperar los catalogos del usuario
            //Ud. debe de formar una cadena XML con los catalogos
            //y retonarla a formiik

            StreamReader reader = new StreamReader(streamUser);
            string xmlUser = reader.ReadToEnd();

            XmlDocument xmlUserDoc = new XmlDocument();

            //Carga la cadena string a un XmlDoc
            xmlUserDoc.LoadXml(xmlUser);

            UsuarioDevice usuario = new UsuarioDevice();

            usuario.UserName = xmlUserDoc.GetElementsByTagName("username").Item(0).InnerText;
            usuario.Password = xmlUserDoc.GetElementsByTagName("password").Item(0).InnerText;


            //El siguinete es solo un demo que toma los catalogos de un archivo
            //En su caso buscará los catalogos correspondientes del usuario
            StreamReader objReader = new StreamReader( _directorioLogeo+"catalogos.xml");
            string strXml = objReader.ReadToEnd();

            return (strXml);
        }

        //Implementación SendErrors
        public void SendErrors(Stream errores)
        {
            Trace.WriteLine(string.Format("{0} - SendErrors", DateTime.Now));
            //Recibe string que contiene un XML con los errores que se hayan generado en los métodos 
            //AddWorkOrdersXML, CancelWorkOrdersXML y UpdateWorkOrdersXML. 

            var reader = new StreamReader(errores);
            string text = reader.ReadToEnd();

            //Documento XML que púede usarse para guardar los errores.
            var xmlerrors = new XmlDocument();
            xmlerrors.LoadXml(text);

            Email.EnviarEmail("sistemasdesarrollo@publipayments.com",
                "Error wsFormiik SendErrors",
                string.Format("{0} - {1}", DateTime.Now, text));

            //Una vez que tiene el reporte de errores en un documento XML
            //puede guardar implenter un proceso para guardarlos en un
            //log o una base de datos

            var writer = new XmlTextWriter(string.Format( _directorioLogeo+"Errores-{0:yyyy-MM-dd-HHmmss}.xml", DateTime.Now), null)
            {
                Formatting = Formatting.Indented
            };
            xmlerrors.Save(writer);
            writer.Close();

        }

        //Implementación ValidateUserForDevice
        public string ValidateUserForDevice(Stream streamUserDevice)
        {
            Trace.WriteLine(string.Format("{0} - ValidateUserForDevice", DateTime.Now));
            /* Permite el ingreso al sistema por dispositivo. Devuelve string vacío si es un usuarío válido de lo contrario se 
             * retorna el error del acceso (p.e. "username no registrado", "password incorrecto").*/

            //Metodo que Ud. puede implmentar para validar a sus usuarios
            //el método que se muestra es solo un demo

            var reader = new StreamReader(streamUserDevice);
            string xmlUser = reader.ReadToEnd();

            XmlDocument xmlUserDoc = new XmlDocument();

            //Carga la cadena string a un XmlDoc
            xmlUserDoc.LoadXml(xmlUser);

            UsuarioDevice usuariodevice = new UsuarioDevice();

            usuariodevice.UserName = xmlUserDoc.GetElementsByTagName("username").Item(0).InnerText;
            usuariodevice.Password = xmlUserDoc.GetElementsByTagName("password").Item(0).InnerText;
            usuariodevice.SerialNumber = xmlUserDoc.GetElementsByTagName("serialnumber").Item(0).InnerText;

            return usuariodevice.Login();

            //return "";
        }

        //Implementación FlexibleUpdateWorkOrder
        public string FlexibleUpdateWorkOrder(Stream updateorder)
        {
            Trace.WriteLine(string.Format("{0} - FlexibleUpdateWorkOrder", DateTime.Now));

            var reader = new StreamReader(updateorder);
            string text = reader.ReadToEnd();

            string fileLogName = string.Format(_directorioLogeo+"ordenes-flexibles{0:yyyy-MM-dd}.csv", DateTime.Now);
            StreamWriter log;

            if (!File.Exists(fileLogName))
            {
                log = new StreamWriter(fileLogName);
            }
            else
            {
                log = File.AppendText(fileLogName);
            }

            log.WriteLine(text);
            log.Close();

            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //RecibeConsulta rci = serializer.Deserialize<RecibeConsulta>(text);

            var recibe = new JavaScriptSerializer();
            var jsonrecibe = recibe.Deserialize<dynamic>(text);

            //string externalid = jsonrecibe["ExternalId"];
            //string idworkorder = jsonrecibe["IdWorkOrder"];
            //string idworkorderformtype = jsonrecibe["IdWorkOrderFormType"];
            //string username = jsonrecibe["Username"];
            string workordertype = jsonrecibe["WorkOrderType"];


            //String returnfields = ConfigurationManager.AppSettings["returnfields"];
            String fomularioregresapositivo = ConfigurationManager.AppSettings["fomularioregresapositivo"];
            String fomularioregresanegativo = ConfigurationManager.AppSettings["fomularioregresanegativo"];

            //String[][] arrReturnfields = returnfields.Split( ';' ).Select( t => t.Split( ':' ) ).ToArray();
            String[][] arrFomularioregresapositivo = fomularioregresapositivo.Split(';').Select(t => t.Split('|')).ToArray();
            String[][] arrFomularioregresanegativo = fomularioregresanegativo.Split(';').Select(t => t.Split('|')).ToArray();


            string respuesta = "";
            var rand = new Random();

            if (rand.Next(0, 2) == 1)
            {
                foreach (var item in arrFomularioregresapositivo)
                {
                    if (item[0] == workordertype)
                    {
                        respuesta = item[1];
                    }
                }
            }
            else
            {
                foreach (var item in arrFomularioregresanegativo)
                {
                    if (item[0] == workordertype)
                    {
                        respuesta = item[1];
                    }
                }
            }
            return respuesta;
        }

        public string ReceiveStatusNotification(Stream notification)
        {
            Trace.WriteLine(string.Format("{0} - ReceiveStatusNotification", DateTime.Now));

            var reader = new StreamReader(notification);
            string text = reader.ReadToEnd();

            Logger.WriteLine(Logger.TipoTraceLog.Log, 0,"RecieveStatusNotification",text);

            string fileLogName = string.Format(_directorioLogeo + "StatusNotif_{0:yyyy-MM-dd}.csv", DateTime.Now);
            StreamWriter log;

            if (!File.Exists(fileLogName))
            {
                log = new StreamWriter(fileLogName);
            }
            else
            {
                log = File.AppendText(fileLogName);
            }

            log.WriteLine(text);
            log.Close();


            var datos = text.Replace("'", "").Replace("{", "").Replace("}", "").Split(':');

            if (datos[1]=="Response")
            {
                var ent = new EntOrdenes();
                ent.ActualizaOrdenesSincronizando(datos[0]);
            }
            
            
            string respuesta = "";
            return respuesta;
        }

        public Stream UpdateStatusNotification(Stream notification)
        {
            Trace.WriteLine(string.Format("{0} - UpdateStatusNotification", DateTime.Now));
            string result = "";
            string text = "";
            try
            {
                using (var reader = new StreamReader(notification))
                {
                    text = reader.ReadToEnd();    
                }
                
                Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "UpdateStatusNotification", text);

                string fileLogName = string.Format(_directorioLogeo + "Notification_{0:ssss-MM-dd}.csv", DateTime.Now);

                using (StreamWriter log=!File.Exists(fileLogName) ? new StreamWriter(fileLogName) : File.AppendText(fileLogName))
                {
                    log.WriteLine(text);
                    log.Close();
         
                }
                var datos = text.Replace("'", "").Replace("{", "").Replace("}", ",");
                var ent = new Orden();
                result = ent.InsertaBitacoraEstadosOrdenes(datos.Substring(0, datos.Length - 1));
                if (result != "")
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "UpdateStatusNotification",result);
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "UpdateStatusNotification",ex.Message );
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(result!=""? "Error":"OK"));
        }
    }
}
