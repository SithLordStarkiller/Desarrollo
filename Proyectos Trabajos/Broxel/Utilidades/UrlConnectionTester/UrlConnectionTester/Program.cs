using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace UrlConnectionTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Url a conectarse: ");
            var direccion = Console.ReadLine();
            var r = ObtenerDireccionWeb(direccion);
            Console.WriteLine(r != "" ? "OK" : "Fallo");
            Console.ReadKey();
        }

        private static string  ObtenerDireccionWeb(string direccion)
        {
            var resultado = "";

            ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;

            var wc = new WebClientTimeOut { TimeOut = 10000 }; //10 segundos de timeout
            Console.WriteLine("Abriendo lectura...");
            Stream data = null;
            try
            {
                data = wc.OpenRead(direccion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
            }

            if (data != null)
            {
                try
                {
                    Console.WriteLine("Seteando time out...");
                    data.ReadTimeout = 10000; //10 segundos de timeout
                    var reader = new StreamReader(data);
                    Console.WriteLine("Leyendo informacion de la URL...");
                    resultado = reader.ReadToEnd();
                    Console.WriteLine("Cerrando stream...");
                    data.Close();
                    Console.WriteLine("Cerrando reader...");
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return resultado;
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            Console.WriteLine("Validando: ServerCertificateValidationCallback");
            return true;
        }
    }

    public class WebClientTimeOut : WebClient
    {
        public int TimeOut { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            if (w != null)
                w.Timeout = TimeOut;
            return w;
        }
    }
}
