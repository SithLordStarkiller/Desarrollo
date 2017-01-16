using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using PubliPayments.Entidades.MYO;

namespace Publipayments.Negocios.MYO
{
    public static class CorreosMyo
    {
        public static void ReferenciasIncorrectas(List<ReferenciasModel> referencias, int idOrden)
        {
            var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
            var result = "";

            result += "<html><head/><body><h3>Hola ";
            result += datosOrden.nombreUsuario;
            result +=".</h3><p><h4> Muchas gracias por confiar en nosotros. Hace unos días recibimos tu solicitud de préstamo  con  tus documentos digitalizados.  " +
                     "Sin embargo no hemos logrado contactar a tus referencias (personales y/o familiares).</h4></p><br/>" +
                     "<br/><p><h4>Para que continuemos con tu solicitud es necesario que ingreses nuevamente  a la Plataforma myoclub.com, " +
                     "verifiques los datos de tus referencias y nos proporciones nuevas referencias.</h4></p><br/><br/><p> Gracias y Recibe un saludo del equipo MYO Club." +
                     "</p><br/><br/><table><tr><td>Para cualquier duda o aclaración contáctanos</td></tr><tr><td>info@myoclub.com</td></tr><tr>" +
                     "<td>01800 8000 MYO (696)</td></tr><tr><td>www.myoclub.com</td></tr></table></body></html>";

            //foreach (var referencia in referencias)
            //{
            //    if (referencia.Valor == "No")
            //    {
            //        result += "<li>"+referencia.Nombre+": "+referencia.Comentario+"</li>";
            //    }
            //}

            var notif=new Notificaciones();

            notif.Notificacion(datosOrden.idUsuario, "Myo Club: Referencias", result, "Normal",1);

        }


        public static void DocumentosIncorrectos(List<DocumentosModel> documentos, int idOrden)
        {
            var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
            var result = "";

            result += "<html><head/><body>" +
                      "<h3>Hola ";
            result += datosOrden.nombreUsuario;
            result += ".</h3>" +
                      "<p><h4> Muchas gracias por confiar en nosotros. Hace unos días recibimos  tu solicitud de crédito  con  tus documentos digitalizados.  " +
                      "Sin embargo se identificó que alguno o algunos de ellos no están correctos.  Para poder continuar con tu solicitud es necesario que  vuelvas a subir " +
                      "sólo aquellos documentos que están marcados con una X (Color ROJA)</h4></p>" +
                      "<table>"+
                      "<thead><th>Documentos</th><th>Validos</th><th>Comentario</th></thead><tbody>";

            foreach (var documento in documentos)
            {
                if (documento.Valor == "No")
                {
                    result += "<tr><td>" + documento.Nombre +
                              "</td><td><span style='color:#F00;font-weight: bolder;'>X</span></td><td>" +
                              documento.Comentario + "</td></tr>";
                }
                else
                {
                    result += "<tr><td>" + documento.Nombre +
                              "</td><td><span style='color:#F00;font-weight: bolder;'>&nbsp;</span></td><td>" +
                              documento.Comentario + "</td></tr>";
                }
            }

            result += "<tr><td>Para cualquier duda o aclaración contáctanos</td></tr>" +
                      "<tr><td>info@myoclub.com</td></tr>" +
                      "<tr><td>01800 8000 MYO (696)</td></tr>" +
                      "<tr><td>www.myoclub.com</td></tr>" +
                      "</tbody></table>" +

                      "<br/><br/>" +
                      "<p><h4>Los  documentos que nos puedes enviar son:</h4></p>" +
                      "<ul>" +
                      "<li>Identificación oficial:   Credencial de elector o  Pasaporte o Cédula Profesional o Credencial INAPAM o  Credencial IMSS.</li>" +
                      "<li>Comprobante de domicilio: Estados de cuentas bancarios a tu nombre o Recibo de pago de Luz o agua o teléfono  sin adeudo vencido de preferencia a tu nombre y con una antigüedad no mayor a 3 meses.</li>" +
                      "<li>Comprobante de ingresos: Recibos de nómina de los últimos 3 meses o  Estados de cuenta Bancario de los últimos 3 meses o Declaración Anual.</li>" +
                      "</ul>" +
                      "<br/><br/>" +
                      "<p><h4>Recuerda que para completar tu solicitud es necesario que envíes cuanto antes tus documentos a través de nuestra Plataforma.</h4></p>" +
                      "<br/><br/>" +
                      "<p> Gracias y Recibe un saludo del equipo MYO Club.</p>" +
                      "<br/><br/>" +
                      "<table>" +
                      "<tr><td>Para cualquier duda o aclaración contáctanos</td></tr>" +
                      "<tr><td>info@myoclub.com</td></tr>" +
                      "<tr><td>01800 8000 MYO (696)</td></tr>" +
                      "<tr><td>www.myoclub.com</td></tr>" +
                      "</table>" +
                      "</body></html>";

            

            var notif = new Notificaciones();

            notif.Notificacion(datosOrden.idUsuario, "Myo Club: Documentos", result, "Normal", 1);

        }


        public static void LegalRechazo(int idOrden)
        {
            
        }

    }
}
