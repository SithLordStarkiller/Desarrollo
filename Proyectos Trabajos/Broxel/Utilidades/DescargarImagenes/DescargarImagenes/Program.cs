using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DescargarImagenes
{
    class Program
    {
        private const string _carpeta = @"C:\London201602\";
        private  static  List<Imagen> lista = new List<Imagen>(); 
        private  static  object blockeo = new object();
        private static int _Procesadas = 0;
        private static int _Total = 0;
        private static bool ejecutando = true;
        private static void Main(string[] args)
        {
            var tareaHeader = new Task(Header);
            Console.Clear();
            var listaTareas = new List<Task>();
            
            //const string connectionString =
            //    "Data Source=192.168.75.220;Initial Catalog=SistemasCobranzaAbril2015;Persist Security Info=True;User ID=devuser;Password=Z341nao27L";
            const string connectionString =
               "Data Source=192.168.80.187;Initial Catalog=SistemasCobranzaFebrero2016;Persist Security Info=True;User ID=devuser;Password=Z341nao27L";
            
            Console.WriteLine("Iniciando...");
            const string queryString =
                "SELECT idOrden, Valor FROM Respuestas WITH (NOLOCK) WHERE SUBSTRING(LOWER(Valor),1,5) = 'https' UNION ALL SELECT idOrden, Valor FROM BitacoraRespuestas WITH (NOLOCK) WHERE SUBSTRING(LOWER(Valor),1,5) = 'https'";

            if (!Directory.Exists(_carpeta))
            {
                Directory.CreateDirectory(_carpeta);
            }

            using (var connection =
                new SqlConnection(connectionString))
            {
                var command =
                    new SqlCommand(queryString, connection);
                Console.WriteLine("Conectando...");
                connection.Open();

                Console.WriteLine("Ejecutando data reader...");
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Procesando imágenes...");

                
                tareaHeader.Start();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    var imagen = reader.GetString(1);
                    var orden = reader.GetInt32(0).ToString(CultureInfo.InvariantCulture);
                    var tarea = new Task(() => DescargaImagenOrden(orden, imagen));
                    tarea.Start();
                    listaTareas.Add(tarea);
                    _Total ++;
                    Thread.Sleep(1);
                }

                // Call Close when done reading.
                reader.Close();
            }

            Console.WriteLine("Esperando a que terminen las tareas...");

            Task.WaitAll(listaTareas.ToArray());

            Console.WriteLine("Procesando imágenes con errores...");
            try
            {
                Parallel.ForEach(lista, imagen =>
                {
                    DescargaImagenOrden(imagen.Orden, imagen.Url);
                });
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            ejecutando = false;
            Task.WaitAll(tareaHeader);
            Console.WriteLine("Terminado...");
            Console.WriteLine("Recuerda Ejecutarlo nuevamente por si fallo algún archivo...");
            Console.ReadKey();
        }

        private static void Header()
        {
            while (ejecutando)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Imágenes encontradas totales: {0} - Imágenes procesadas {1}             ", _Total, _Procesadas);
                Console.WriteLine("========================================================================");
                Thread.Sleep(1000);
            }
        }


        private static void DescargaImagenOrden( string orden, string url)
        {
            try
            {
                var uri = new Uri(url);

                string filename = Path.GetFileName(uri.LocalPath);

                using (var client = new WebClient())
                {
                    if (!Directory.Exists(_carpeta + orden + "\\"))
                    {
                        Directory.CreateDirectory(_carpeta + orden + "\\");
                    }

                    if (File.Exists(_carpeta + orden + "\\" + filename) && new FileInfo(_carpeta + orden + "\\" + filename).Length == 0)
                    {
                        var texto = string.Format("{0} - Archivo Vacio: {1}                                        ",
                            orden,
                            url.Replace("https://ekbalam.blob.core.windows.net/response-files/", "")
                                .Replace("https://imagenes.publipayments.com/", ""));
                        texto = texto.Substring(0, 79);
                        Console.WriteLine(texto);
                        File.Delete(_carpeta + orden + "\\" + filename);
                    }

                    if (!File.Exists(_carpeta + orden + "\\" + filename))
                    {
                        client.DownloadFile(url, _carpeta + orden + "\\" + filename);
                        var texto = string.Format("{0} - {1}                                        ",
                            orden,
                            url.Replace("https://ekbalam.blob.core.windows.net/response-files/", "")
                                .Replace("https://imagenes.publipayments.com/", ""));
                        texto = texto.Substring(0, 79);
                        Console.WriteLine(texto);
                    }

                    Interlocked.Increment(ref _Procesadas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                lock (blockeo)
                {
                    lista.Add(new Imagen {Orden = orden, Url = url});
                }
            }

        }

        //private static void DownloadRemoteImageFile(string uri, string fileName)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(uri);
        //    var response = (HttpWebResponse)request.GetResponse();

        //    // Check that the remote file was found. The ContentType
        //    // check is performed since a request for a non-existent
        //    // image file might be redirected to a 404-page, which would
        //    // yield the StatusCode "OK", even though the image was not
        //    // found.
        //    if ((response.StatusCode == HttpStatusCode.OK ||
        //        response.StatusCode == HttpStatusCode.Moved ||
        //        response.StatusCode == HttpStatusCode.Redirect) &&
        //        response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
        //    {

        //        // if the remote file was found, download oit
        //        using (Stream inputStream = response.GetResponseStream())
        //        using (Stream outputStream = File.OpenWrite(fileName))
        //        {
        //            byte[] buffer = new byte[4096];
        //            int bytesRead = 0;
        //            do
        //            {
        //                if (inputStream != null) bytesRead = inputStream.Read(buffer, 0, buffer.Length);
        //                outputStream.Write(buffer, 0, bytesRead);
        //            } while (bytesRead != 0);
        //        }
        //    }
        //}
    }

    public class Imagen
    {
        public string Orden { get; set; }
        public string Url { get; set; }
    }
}
