using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using wsBroxel.App_Code.GenericBL;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.TCControlVL.Model;
using wsBroxel.App_Code.Utils;

namespace wsBroxel.App_Code
{
    public static class Mailing
    {

        public static void EnviaCorreoOlvidoContrasenia(String Tarjetahabiente, String usuario, String password, String correo, String host)
        {


            broxelco_rdgEntities ctx = new broxelco_rdgEntities();
            var datos = ctx.MailingThemesOnline.FirstOrDefault(x => x.Hostname ==host);
           if (datos == null) return;

            const string comillas = "\"";

            String Comentario = "<html xmlns='http://www.w3.org/1999/xhtml'><head>";
            Comentario += " <meta name='viewport' content='width=device-width'>";
            Comentario += " <meta charset='utf-8'>";
            Comentario += "   <meta name='Mejoravit' content='Mejoravit'>";

            Comentario += " <script type='text/javascript' src='http://broxtel.com/bxf/comunicados/nuevos_templates/jquery-1.7.1.js'></script>";
            Comentario += "  <script type='text/javascript'>hsjQuery = window['jQuery']</script>";
            Comentario += " <link href='http://broxtel.com/bxf/comunicados/nuevos_templates/style.css' rel='stylesheet'>";
            Comentario += " <link rel='canonical' href='http://get.invisionapp.com/weekly-digest-34'>";
            Comentario += "<meta name='viewport' content='width=device-width, initial-scale=1'>";


            Comentario += " <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            Comentario += "</head>";

            Comentario += "<body><div id='hs_cos_wrapper_module_2307418810' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div>";
            Comentario += "<center>";
            Comentario += "<table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' bgcolor='#F3F3F2'>";

            Comentario += "<tbody><tr height='30'><td align='center' valign='bottom'>";
            Comentario += "<table class='master-table' width='600'>";
            Comentario += "<tbody><tr><td align='center' valign='bottom'>";
            Comentario += "<table class='responsive-table' width='580' border='0' cellpadding='0' cellspacing='0' valign='top'>";
            Comentario += "<tbody><tr><td valign='bottom' align='right'><a href='#' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-weight:200 !important; font-size:12px; color:#989ca5 !important; padding: 1px 0; border-bottom: 1px solid #bfc4c8; text-decoration: none !important; " + comillas + " data-hs-link-id='0'></a></td></tr>";
            Comentario += "</tbody></table>";
            Comentario += "</td></tr>";
            Comentario += "</tbody></table>";
            Comentario += "</td></tr>";
            Comentario += "<tr height='8' style='font-size: 0; line-height: 0;'><td>&nbsp;</td></tr>";

            Comentario += "<tr><td align='center' valign='top'><div id=hs_cos_wrapper_header' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_1' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_2' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_7' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_4' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_5' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_6' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_3' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_8' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_9' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_10' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_11' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_design_links' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><table class='master-table' width='600'>";

            Comentario += "<tbody><tr><td align='center'>";
            Comentario += "<table class='responsive-table' width='570' border='0' cellpadding='0' cellspacing='0' valign='top'>";
            Comentario += "<tbody><tr>";
            Comentario += String.Format("<td width='70'><a href='#' data-hs-link-id='0'><img src='https://images.broxel.com/Comunicados/Correo/Bienvenida/{0} ' width='236' height='54' alt='BXF' style='border:0;'></a></td>", datos.ImagenLogo);
            Comentario += "<td class='responsive-header-cell-big' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:25px;line-height:30px !important;font-weight:200 !important;color:#252b33 !important;" + comillas + ">&nbsp;</td>";
            Comentario += "<td class='responsive-header-cell' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:13px !important;line-height:30px !important;font-weight:400 !important;color:#7e8890 !important;" + comillas + " align='right'>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr>";

            Comentario += "</tbody></table></td></tr>";
            Comentario += "<tr height='40'><td>&nbsp;</td></tr><tr><td align='center' valign='top'>";
            Comentario += "<table class='responsive-table' width='580' bgcolor='#ffffff' border='0' cellpadding='0' cellspacing='0' valign='top' style='overflow:hidden !important;border-radius:3px;'>";
            Comentario += String.Format("<tbody><tr><td><a href='#' data-hs-link-id='0'><img src='https://images.broxel.com/Comunicados/Correo/Bienvenida/{0}' class='hero-image' width='100%' style='border: 0; max-width: 100% !important;'></a></td></tr>", datos.ImagenFooter);
            Comentario += "<tr height='46'>";
            Comentario += "<td style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:11px; text-align:center;" + comillas + "></td></tr><tr><td align='center'><table width='81%'>";
            Comentario += "<tbody><tr><td align='center'><h2 style=" + comillas + "margin: 0 !important; font-family:'Open Sans', arial, sans-serif !important;font-size:28px !important;line-height:38px !important;font-weight:200 !important;color:#000000  !important; text-align:left;" + comillas + ">Restablece tu contraseña</h2></td></tr>";

            Comentario += "</tbody></table></td></tr><tr height='25'><td>&nbsp;</td></tr><tr><td align='center'><table class='story-4' border='0' cellpadding='0' cellspacing='0' width='78%'><tbody><tr>";
            Comentario += "<td align='center' style=" + comillas + " font-family:'Open Sans', arial, sans-serif !important;font-size:16px !important;line-height:27px !important;font-weight:400 !important;color:#7e8890 !important; " + comillas + " >";
            Comentario += "<p style='margin-top:-17px; text-align:justify;'><p style='margin-top:10px; text-align:justify;'><span style='text-align:left; color:#D90C2C;'>";
            Comentario += "Estimad@: </span>  " + Tarjetahabiente;
            Comentario += "<p style='margin-top:-17px; text-align:left;'><br />";
            Comentario += "Recibimos una solicitud de contraseña de tu usuario: <strong>" + usuario + "</strong><p style='text-align:left;'>Por tal motivo se ha generado una contraseña temporal. <p style='text-align:left;'>";
            Comentario += "Para reactivar tu cuenta ingresa a <a href='" + datos.RecuperaUrl + "'>" + datos.NombreServicio + "</a> utilizando: <br />Tu usuario de siempre: <strong>" + usuario + "</strong><br />";

            Comentario += "Tu password temporal: <strong>" + password + "</strong>";
            Comentario += "<p style='text-align:justify;'></td></tr></tbody></table></td></tr><tr height='3'><td></td></tr><tr><td align='center' valign='top'>&nbsp;</td></tr><tr height='7'><td>&nbsp;</td></tr></tbody></table></td></tr><tr><td align='center' valign='top'>&nbsp;</td></tr>";
            Comentario += "<tr><td align='center' valign='top'><table class='responsive-table' width='580' bgcolor='#ffffff' border='0' cellpadding='0' cellspacing='0' valign='top' style='overflow:hidden !important;border-radius:3px;'><tbody> <tr><td>&nbsp;</td></tr><tr height='25'><td>&nbsp;</td></tr><tr>";
            Comentario += "<td align='center'><table class='story-4' border='0' cellpadding='0' cellspacing='0' width='78%'>";
            Comentario += "<tbody><tr><td align='center' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:16px !important;line-height:30px !important;font-weight:400 !important;color:#7e8890 !important;" + comillas + "><p style='margin-top:-30px; text-align:justify;'><br />";
            Comentario += "Atentamente: ";
            Comentario += "<span style='font-weight:bold;'>" + datos.NombreServicio + "</span><br />";
            Comentario += "En caso de tener alguna duda o comentario, comunicarse al: " + datos.Telefono + " o ingresa a <a href='" + datos.Hostname + "'>" + datos.NombreServicio + "</a></p>";
            Comentario += "<p style='margin-bottom:-15px;'>&nbsp;</p></td></tbody></table></td></tr><tr height='3'><td></td></tr><tr height='7'><td>&nbsp;</td></tr></tbody></table></td></tr><tr height='20'> <td align='center'><a href='#' data-hs-link-id='0'></td></tr><tr>";

            Comentario += "<td align='center'></td></tr></tbody></table></td></tr></tbody></table></center><script type='text/javascript' src='https://static.hsstatic.net/content_shared_assets/static-1.3962/js/public_common.js'></script>";
            Comentario += "<script src='InVision-Sept2014-main.min.js'></script></body></html>";

            Helper.SendMail(datos.Remitente, correo, "Recuperación de contraseña", Comentario, datos.Contrasena);


            //broxelco_rdgEntities ctx = new broxelco_rdgEntities();
            //var datos = ctx.MailingThemesOnline.FirstOrDefault(x => x.Hostname == host);
            //if (datos == null) return;
  
            //String Comentario = "<html xmlns='http://www.w3.org/1999/xhtml'>";
            //Comentario += "<body style='text-align: center;'>";
            //Comentario += "<center>";
            //Comentario += "<table cellpadding='0' cellspacing='0' width='600px' bgcolor='#ffffff' style='font-family:Arial; font-size:12px; color:#949494;text-align:left;'>";
            //Comentario += "<tr>";
            //Comentario += "<td>";

            ////Comentario += "<img src='http://catarsyslab.com/media/Broxel/Logo_Bx.gif' style='display: block;' />";
            //Comentario += "</td>";
            //Comentario += "<td>";
            //Comentario += DateTime.Now.ToString("dd/MM/yyyy");
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr>";
            //Comentario += "<td colspan='2'>";
            ////Comentario += "<img src='http://catarsyslab.com/media/Broxel/Borde_Bx.gif' style='display: block;' />";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr>";
            //Comentario += "<td colspan='2'>";
            //Comentario += "<span style='font-size:14px'>Estimad@: <strong><span style='color:#333333'>" + Tarjetahabiente + "</span></strong><br/><br/></span>";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr style='background:#F2F2F2'>";
            //Comentario += "<td colspan='2'>";
            //Comentario += "<br/>&nbsp;&nbsp;&nbsp;Recibimos una solicitud de contraseña de tu usuario: <strong><span style='color:#333333'>" + usuario + "</span></strong><br/>";
            //Comentario += "&nbsp;&nbsp;&nbsp;Por tal motivo se ha generado una contraseña temporal: <br/><br/>";
            //Comentario += "<span style='color:#37C7E6;font-weight:bold;font-size:13px'>&nbsp;&nbsp;&nbsp;Para reactivar tu cuenta ingresa a <a href='" + datos.RecuperaUrl + "' style='color:#37C7E6;font-weight:bold;font-size:13px;text-decoration:underline;' >" + datos.NombrePagina + "</a> utilizando:</span><br/><br/>";
            //Comentario += "&nbsp;&nbsp;&nbsp;Tu usuario de siempre: <strong><span style='color:#333333'>" + usuario + "</span></strong><br/>";
            //Comentario += "&nbsp;&nbsp;&nbsp;Tu password temporal: <strong><span style='color:#333333'>" + password + "</span></strong><br/><br/>";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr>";
            //Comentario += "<td colspan='2'>";
            ////Comentario += "<img src='http://catarsyslab.com/media/Broxel/Borde_Bx.gif' style='display: block;' />";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr>";
            //Comentario += "<td colspan='2' style='text-align:center;'>";
            //Comentario += "<span style='color:#37C7E6;font-weight:bold;font-size:13px'>Gracias por formar parte de la comunidad de Servicios Online</span><br/><br/>";
            //Comentario += "Da clic aquí para revisar los <a href='" + datos.PaginaTerminos + "' style='color:#37C7E6;font-weight:bold;font-size:13px;text-decoration:underline;' >Terminos y Condiciones</a> de " + datos.NombrePagina;
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr>";
            //Comentario += "<td colspan='2'>";
            ////Comentario += "<img src='http://catarsyslab.com/media/Broxel/Borde_Bx.gif' style='display: block;' />";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr style='text-align:left'>";
            //Comentario += "<td>";
            //Comentario += "<span style='color:#37C7E6; font-size:13px;font-weight:bold;'>" + datos.Telefono + "</span>";
            //Comentario += "</td>";
            //Comentario += "<td>";
            //Comentario += "<a style='color:#37C7E6; font-size:13px;font-weight:bold;' href='mailto" + datos.ContactoMail + "'>" + datos.ContactoMail + "</a>";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "<tr>";
            //Comentario += "<td colspan='2'>";
            ////Comentario += "<img src='http://catarsyslab.com/media/Broxel/Footer_Bx.gif' style='display: block;' />";
            //Comentario += "</td>";
            //Comentario += "</tr>";
            //Comentario += "</table>";
            //Comentario += "</center>";
            //Comentario += "</body>";
            //Comentario += "</html>";
            ////SendMail(datos.Remitente, correo, "Recuperación de contraseña.", Comentario);
            //Helper.SendMail(datos.Remitente, correo, "Recuperación de contraseña", Comentario, datos.Contrasena);
        }

        public static void EnviaCorreoBienvenida(String Tarjetahabiente, String Tarjeta, String usuario, String correo, String host, String producto="")
        {
            if (host == null)
                return;
            String hostname = "http://" + host.ToLower();
            broxelco_rdgEntities ctx = new broxelco_rdgEntities();
            var datos = ctx.MailingThemesOnline.FirstOrDefault(x => x.Hostname == host.ToUpper());
            if (datos == null) return;

            //validación Merchant account
            string[] productosMerchant = ConfigurationManager.AppSettings["Merchant"].Split(',');
            string linkFintechBroxel = "https://fintech.broxel.com";

            foreach (var productoMerchant in productosMerchant)
            {
                if ("'" + producto + "'" == productoMerchant)
                {
                    hostname = linkFintechBroxel;
                    datos.NombrePagina = datos.NombreServicio;
                    break;
                }   
            }

            var comentario =
               datos.BodyHTML.Replace("{Tarjetahabiente}", Tarjetahabiente)
                   .Replace("{datos.NombrePagina}", datos.NombrePagina)
                   .Replace("{Tarjeta}", Tarjeta)
                   .Replace("{usuario}", usuario)
                   .Replace("{datos.linkPortal}", hostname)
                   .Replace("{datos.PaginaTerminos}", datos.PaginaTerminos)
                   .Replace("{datos.Telefono}", datos.Telefono)
                   .Replace("{datos.ContactoMail}", datos.ContactoMail)
                   .Replace("{hoy}", DateTime.Now.ToString("dd/MM/yyyy"))
                   .Replace("{datos.NombreServicio}", datos.NombreServicio);

            Helper.SendMail(datos.Remitente, correo, "Bienvenid@ a " + datos.NombreServicio, comentario, datos.Contrasena);
        }


        /// <summary>
        /// Metodo que envia el estatus de la tarjeta
        /// </summary>
        /// <param name="Tarjetahabiente"></param>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="correo"></param>
        /// <param name="host"></param>
        public static void EnviaCorreoTarjetaBloqueada(OperarTarjetaResponse res, OperarTarjetaRequest request,string Estado)
        {
            if (EstadosTarjetas.BolBloqueoTemporal != Estado)
                return;
            if (res.Success != 1)
                return;
            if (res.CodigoRespuesta != 0)
                return;

            if (!(new MySqlDataAccess().ValidaProductoMejoravit(request.NumCuenta)))
                return;

            broxelco_rdgEntities ctx = new broxelco_rdgEntities();
            var datos = ctx.MailConfig.FirstOrDefault(x => x.idServicio == 4);
            if (datos == null) return;

            var usuario = ctx.UsuariosOnlineBroxel.FirstOrDefault(x => x.Id == request.UserID);
            String emailUsuario = usuario.CorreoElectronico;

            string correoDestino = usuario.CorreoElectronico;
            String Comentario = "<html xmlns='http://www.w3.org/1999/xhtml'><head>";
            const string comillas = "\"";

            Comentario += " <meta name='viewport' content='width=device-width'>";
            Comentario += " <meta charset='utf-8'>";
            Comentario += "   <meta name='Mejoravit' content='Mejoravit'>";

            Comentario += " <script type='text/javascript' src='http://broxtel.com/bxf/comunicados/nuevos_templates/jquery-1.7.1.js'></script>";
            Comentario += "  <script type='text/javascript'>hsjQuery = window['jQuery']</script>";
            Comentario += " <link href='http://broxtel.com/bxf/comunicados/nuevos_templates/style.css' rel='stylesheet'>";
            Comentario += " <link rel='canonical' href='http://get.invisionapp.com/weekly-digest-34'>";
            Comentario += "<meta name='viewport' content='width=device-width, initial-scale=1'>";


            Comentario += " <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />";
            Comentario += "</head>";

            Comentario += "<body><div id='hs_cos_wrapper_module_2307418810' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div>";
            Comentario += "<center>";
            Comentario += "<table align='center' border='0' cellpadding='0' cellspacing='0' height='100%' width='100%' bgcolor='#F3F3F2'>";

            Comentario += "<tbody><tr height='30'><td align='center' valign='bottom'>";
            Comentario += "<table class='master-table' width='600'>";
            Comentario += "<tbody><tr><td align='center' valign='bottom'>";
            Comentario += "<table class='responsive-table' width='580' border='0' cellpadding='0' cellspacing='0' valign='top'>";
            Comentario += "<tbody><tr><td valign='bottom' align='right'><a href='#' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-weight:200 !important; font-size:12px; color:#989ca5 !important; padding: 1px 0; border-bottom: 1px solid #bfc4c8; text-decoration: none !important; " + comillas + " data-hs-link-id='0'></a></td></tr>";
            Comentario += "</tbody></table>";
            Comentario += "</td></tr>";
            Comentario += "</tbody></table>";
            Comentario += "</td></tr>";
            Comentario += "<tr height='8' style='font-size: 0; line-height: 0;'><td>&nbsp;</td></tr>";

            Comentario += "<tr><td align='center' valign='top'><div id=hs_cos_wrapper_header' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_1' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_2' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_7' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_4' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_5' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_6' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_3' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_8' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_9' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_10' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_article_11' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><div id='hs_cos_wrapper_design_links' class='hs_cos_wrapper hs_cos_wrapper_widget hs_cos_wrapper_type_custom_widget' style='color: inherit; font-size: inherit; line-height: inherit; margin: inherit; padding: inherit' data-hs-cos-general-type='widget' data-hs-cos-type='custom_widget'>";
            Comentario += "</div><table class='master-table' width='600'>";

            Comentario += "<tbody><tr><td align='center'>";
            Comentario += "<table class='responsive-table' width='570' border='0' cellpadding='0' cellspacing='0' valign='top'>";
            Comentario += "<tbody><tr>";
            Comentario += "<td width='70'><a href='#' data-hs-link-id='0'><img src='https://images.broxel.com/Comunicados/Correo/Bienvenida/logo_mejoravit.png' width='236' height='54' alt='BXF' style='border:0;'></a></td>";
            Comentario += "<td class='responsive-header-cell-big' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:25px;line-height:30px !important;font-weight:200 !important;color:#252b33 !important;" + comillas + ">&nbsp;</td>";
            Comentario += "<td class='responsive-header-cell' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:13px !important;line-height:30px !important;font-weight:400 !important;color:#7e8890 !important;" + comillas + " align='right'></td></tr>";

            Comentario += "</tbody></table></td></tr>";
            Comentario += "<tr height='40'><td>&nbsp;</td></tr><tr><td align='center' valign='top'>";
            Comentario += "<table class='responsive-table' width='580' bgcolor='#ffffff' border='0' cellpadding='0' cellspacing='0' valign='top' style='overflow:hidden !important;border-radius:3px;'>";
            Comentario += "<tbody><tr><td><a href='#' data-hs-link-id='0'><img src='https://images.broxel.com/Comunicados/Correo/Bienvenida/Head_mejoravit.png' class='hero-image' width='100%' style='border: 0; max-width: 100% !important;'></a></td></tr>";
            Comentario += "<tr height='46'>";
            Comentario += "<td style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:11px; text-align:center;" + comillas + "></td></tr><tr><td align='center'><table width='81%'>";
            Comentario += "<tbody><tr><td align='center'><h2 style=" + comillas + "margin: 0 !important; font-family:'Open Sans', arial, sans-serif !important;font-size:28px !important;line-height:38px !important;font-weight:200 !important;color:#000000  !important; text-align:center;" + comillas + ">Tarjeta {Estatus}</h2></td></tr>";

            Comentario += "</tbody></table></td></tr><tr height='25'><td>&nbsp;</td></tr><tr><td align='center'><table class='story-4' border='0' cellpadding='0' cellspacing='0' width='78%'><tbody><tr>";
            Comentario += "<td align='center' style=" + comillas + " font-family:'Open Sans', arial, sans-serif !important;font-size:16px !important;line-height:27px !important;font-weight:400 !important;color:#7e8890 !important; " + comillas + " >";
            Comentario += "<p style='margin-top:-17px; text-align:justify;'><p style='margin-top:10px; text-align:justify;'><span style='text-align:left;'>";
            Comentario += "Estimado tarjetahabiente, le notificamos que su tarjeta con terminación {Terminacion} ha sido {Estatus}. <br><br>" + DateTime.Now.ToString("dd/MM/yyyy");
            Comentario += "<p style='margin-top:-17px; text-align:left;'><br />";

            Comentario += "<p style='text-align:justify;'></td></tr></tbody></table></td></tr><tr height='3'><td></td></tr><tr><td align='center' valign='top'>&nbsp;</td></tr><tr height='7'><td>&nbsp;</td></tr></tbody></table></td></tr><tr><td align='center' valign='top'>&nbsp;</td></tr>";
            Comentario += "<tr><td align='center' valign='top'><table class='responsive-table' width='580' bgcolor='#ffffff' border='0' cellpadding='0' cellspacing='0' valign='top' style='overflow:hidden !important;border-radius:3px;'><tbody> <tr><td>&nbsp;</td></tr><tr height='25'><td>&nbsp;</td></tr><tr>";
            Comentario += "<td align='center'><table class='story-4' border='0' cellpadding='0' cellspacing='0' width='78%'>";
            Comentario += "<tbody><tr><td align='center' style=" + comillas + "font-family:'Open Sans', arial, sans-serif !important;font-size:16px !important;line-height:30px !important;font-weight:400 !important;color:#7e8890 !important;" + comillas + "><p style='margin-top:-30px; text-align:justify;'><br />";
            Comentario += "Atentamente: ";
            Comentario += "<span style='font-weight:bold;'>Mejoravit</span><br />";
            Comentario += "En caso de tener alguna duda o comentario, comunicarse al: 01800-2-MEJORA (01800-2-635672) o ingresa a <a href=' https://mejoravit.com/ '>Mejoravi.com</a></p>";
            Comentario += "<p style='margin-bottom:-15px;'>&nbsp;</p></td></tbody></table></td></tr><tr height='3'><td></td></tr><tr height='7'><td>&nbsp;</td></tr></tbody></table></td></tr><tr height='20'> <td align='center'><a href='#' data-hs-link-id='0'></td></tr><tr>";

            Comentario += "<td align='center'></td></tr></tbody></table></td></tr></tbody></table></center><script type='text/javascript' src='https://static.hsstatic.net/content_shared_assets/static-1.3962/js/public_common.js'></script>";
            Comentario += "<script src='InVision-Sept2014-main.min.js'></script></body></html>";

            Comentario = Comentario.Replace("{Estatus}", "Bloqueada")
                       .Replace("{Terminacion}", request.Tarjeta.NumeroTarjeta.Substring(request.Tarjeta.NumeroTarjeta.Length-4));



            Helper.SendMail(datos.de, correoDestino, "Tarjeta bloqueada", Comentario, AesEncrypter.Decrypt(datos.dePwd, "securepwd"));

        }

        /// <summary>
        /// Método que envía un correo por configuración(Alta de CLABE, cambio de NIP, entre otros).
        /// </summary>
        /// <param name="idMailConfig">id de mail (Se verifica la configuración en la tabla de MailConfig)</param>
        /// <param name="idServicio">id de servicio (Se verifica la configuración en la tabla de MailConfig)</param>
        /// <param name="correoDestino">correo usuario</param>
        /// <param name="listaEtiquetasReplace">listado de etiquetas con nuevos valores a remplazar</param>
        public static void EnviaCorreoAvisoMovimiento(int idMailConfig, int idServicio, string correoDestino, List<MailReplacer> listaEtiquetasReplace)
        {
            try {
                Trace.WriteLine(string.Format("{0:dd/MM/yy HH:mm:ss.fff} Inicio de  EnviaCorreoAvisoMovimiento ", DateTime.Now));
                var configuracionMail = new GenericBL.GenericBL().GetMailConfig(idMailConfig, idServicio, listaEtiquetasReplace);
                if (configuracionMail != null)
                {
                    Helper.SendMail(configuracionMail.de, correoDestino, configuracionMail.asunto, configuracionMail.preconfBody, configuracionMail.dePwd, configuracionMail.deAlias);
                }
                else
                {
                    Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "EnviaCorreoAvisoMovimiento", "El método: GetMailConfig regreso null ya que se ingreso una configuración no valida, idMailConfig:  " + idMailConfig + " idServicio: " + idServicio, "67896789");
                }
                Trace.WriteLine(string.Format("{0:dd/MM/yy HH:mm:ss.fff}  Fin de  EnviaCorreoAvisoMovimiento", DateTime.Now));
            }
            catch (Exception ex)
            {
                Helper.SendMail("broxelonline@broxel.com", "luis.huerta@broxel.com", "EnviaCorreoAvisoMovimiento", "Se intento enviar el correo de notificación de movimiento, error generado:" + ex, "67896789");
                Trace.WriteLine(string.Format("{0:dd/MM/yy HH:mm:ss.fff} ERROR -> EnviaCorreoAvisoMovimiento:  {1}", DateTime.Now, ex.Message));
            }
        }
    }
}