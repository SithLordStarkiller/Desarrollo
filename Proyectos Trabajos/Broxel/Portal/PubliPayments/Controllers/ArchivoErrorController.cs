using System;
using System.Web.Mvc;
using System.Text;
using PubliPayments.Entidades;

namespace PubliPayments.Controllers
{
    public class ArchivoErrorController : Controller
    {
        public String Index()
        {
            return "Hi!";

        }

        public ActionResult Descargar(String arg = "1")
        {
            int index = Int32.Parse(arg);
            System.Diagnostics.Debug.WriteLine(index);
            ConexionSql conn = ConexionSql.Instance;
            try
            {
                //String filename = String.Format("error.txt");
                StringBuilder resultado = new StringBuilder();
                String temporal;
                long contador = 0;
                Response.AddHeader("content-disposition", "attachment;filename=reporte.txt");
                Response.ContentType = "text/plain";
                do
                {
                    temporal = conn.ObtenerErrorEspecificoConRango(arg, contador);
                    resultado.Append(temporal);
                    contador += 16384;
                    var buffer = Encoding.ASCII.GetBytes(temporal);
                    if (Response.IsClientConnected)
                    {
                        Response.OutputStream.Write(buffer, 0, buffer.Length);
                        Response.Flush();
                    }
                    else
                    {
                        return null;
                    }

                } while (temporal.Length >= 16383);
                Response.Flush();
                return null;
            }
            catch (Exception)
            {

                Response.Flush();
                return null;
            }

        }
    }
}