using System;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace ClienteRest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Comienza");
                //string filepath = @"C:\respuestas\RespuestaOrden.xml";
                string filepath = @"C:\Ordenes";
                string pathProcesadas = filepath + @"\Procesadas";
                if (!Directory.Exists(filepath))
                {
                    Console.WriteLine("Creando Directorio: {0}", filepath);
                    Directory.CreateDirectory(filepath);
                }
                if (!Directory.Exists(pathProcesadas))
                    {
                        Console.WriteLine("Creando Directorio: {0}", pathProcesadas);
                        Directory.CreateDirectory(pathProcesadas);
                    }
                

                Console.WriteLine("Obteniendo Archivos XML: {0}", filepath);

                var archivos = Directory.GetFiles(filepath);

                Console.WriteLine("Se encontraron {0} archivos", archivos.Count());

                string url = "http://localhost:2678//Api.svc//SendWorkOrderToClient";
               // string url = "https://wsformiik.broxel.com/Api.svc/SendWorkOrderToClient";
                //string url = "http://10.210.0.142:9037//Services.svc//SendWorkOrderToClient";
                Console.WriteLine("Url: {0}", url);

                foreach (var archivo in archivos)
                {
                    Console.WriteLine("Enviando a wsFormiik el archivo: {0}", archivo);

                    var formiikResponse = new XmlDocument();
                    formiikResponse.Load(archivo);

                    byte[] enviaBytes = Encoding.UTF8.GetBytes(formiikResponse.InnerXml);
                    //byte[] enviaBytes = Encoding.UTF8.GetBytes("{'365641':'Respuesta':'" + DateTime.Now.AddHours(3) + "'}");
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.KeepAlive = false;
                    request.Method = "POST";
                    request.ContentType = "text/plain";
                    request.ContentLength = enviaBytes.Length;
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(enviaBytes, 0, enviaBytes.Length);
                    requestStream.Close();

                    var response = (HttpWebResponse) request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        string result = new StreamReader(responseStream).ReadToEnd();

                        if (!result.Equals(string.Empty))
                        {
                            Console.WriteLine("Service Error: " + result);
                        }
                        else
                        {
                            Console.WriteLine("Send Successfully: " + result);

                            Console.WriteLine("Moviendo archivo {0} a directorio {1}", archivo, pathProcesadas);
                            File.Move(archivo, pathProcesadas + @"\" + Path.GetFileName(archivo));
                        }
                    }
                    else
                    {
                        Console.WriteLine("responseStream nulo");
                    }
                }

                Console.WriteLine("Se terminó de procesar los archivos");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Error:" + ex.Message);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
