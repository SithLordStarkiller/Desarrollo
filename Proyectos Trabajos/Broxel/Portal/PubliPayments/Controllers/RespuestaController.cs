using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Utiles;
using Respuesta = PubliPayments.Negocios.Respuesta;


namespace PubliPayments.Controllers
{
    public class RespuestaController : Controller
    {
        public ActionResult Ver(String idOrden, String proceso, string conexionBd = "SqlDefault")
        {
            var guidTiempos = Tiempos.Iniciar();
            var ordenHtml = new StringBuilder();
            var tabla1 = new StringBuilder();
            var tabla2 = new StringBuilder();
            ConexionSql cnnSql = ConexionSql.Instance;
            string usuarioN="";
            string columna = "";
            string valor = "";
            string mapsVal = "";
            const string mapsHref = "<a target='_blank' href='http://maps.google.com/maps@valor'><img src='/imagenes/icono-googlemaps-ios.png' width='60px' height='60px'></a>";
            try
            {   
                const string fotoHref = "<a target='_blank' href='@valor' data-lightbox='@idOrden' ><img src='@valor' width='60px' height='60px'></a>";
                const string docHref = "<a target='_blank' href='@valor'><img src='/imagenes/pdf.jpg' width='40px' height='40px'></a>";
                const string tarjetaCredito = "<div id='CifradoTC'><img src='/imagenes/candado.jpg' width='40px' height='40px' onclick='encriptaTC(@idOrden);'></div>";

                var restriccion = !( "0,1,5".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)) || Config.EsCallCenter || Config.AplicacionActual().Nombre.Contains("MYO"));

                int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                DataSet ds = new Respuesta().ObtenerRespuestas(0, idOrden, Config.AplicacionActual().Nombre.Contains("OriginacionMovil") ? -1 : 0, idUsuario, restriccion,conexionBd);

                var columnsName = ds.Tables[0].Columns.Cast<DataColumn>().Select(dc => dc.ColumnName).ToList();
                //var columnsDataType = ds.Tables[0].Columns.Cast<DataColumn>().Select(dc => dc.DataType.ToString()).ToList();
                //var columnsGrouped = columnsName.Zip(columnsDataType, (nombre, tipo) => nombre + ":" + tipo);
                //var columnsFound = String.Join(",", columnsGrouped);
                ordenHtml.Clear();
                tabla1.Clear();
                tabla2.Clear();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var respuestasArr = ds.Tables[0].Rows[0].ItemArray;
                    
                    if (proceso == "1")
                    {
                        tabla1.Append("<table border='1' bordercolor='#DCDCDC'  style='float:left;width: 400px;'>");
                        tabla2.Append("<table border='1' bordercolor='#DCDCDC'  style='float:left;width: 400px;'>");
                        for (int i = 0; i < columnsName.Count; i++)
                        {
                            if (respuestasArr[i]!=null&&respuestasArr[i].ToString().Trim() != "")
                            {
                                if (respuestasArr[i].ToString().StartsWith("Lat:"))
                                {
                                    mapsVal = respuestasArr[i].ToString().Substring(0, respuestasArr[i].ToString().IndexOf("Sat"));
                                    mapsVal = mapsVal.Replace("Lat:", "?q=").Replace("Lon:", ",") + "&z=17"; 
                                }
                                columna = columnsName[i].ToUpper().StartsWith("CIFRADOTC") ? "TARJETA CREDITO" : columnsName[i].ToUpper();
                                valor = ((Config.AplicacionActual().Nombre.Contains("OriginacionMovil") && (columna.StartsWith("DOC"))) ?
                                               docHref.Replace("@valor", respuestasArr[i].ToString()) : (respuestasArr[i].ToString().StartsWith("http:") || respuestasArr[i].ToString().StartsWith("https:")) ?
                                              fotoHref.Replace("@valor", respuestasArr[i].ToString()).Replace("@idOrden", idOrden)
                                            : (respuestasArr[i].ToString().StartsWith("Lat:")) ?
                                               mapsHref.Replace("@valor", mapsVal)
                                               : (Config.AplicacionActual().Nombre.Contains("OriginacionMovil") && (columna.StartsWith("TARJETA CREDITO"))) ?
                                               tarjetaCredito.Replace("@idOrden", idOrden) : respuestasArr[i].ToString());
                                if (columnsName[i].ToUpper() == "USUARIO") { usuarioN = respuestasArr[i].ToString(); }
                                
                                if(i % 2 == 0 || i==0)
                                {
                                    tabla1.Append("<tr class='" + ((i % 4 == 0 || i==0 ) ? "GridCelda" : "GridCeldaAlt") + "'>");
                                    tabla1.Append("<td>" + columna + "</td>");
                                    tabla1.Append("<td>" + valor + "</td>");
                                    tabla1.Append("</tr>");
                                }
                                else 
                                {
                                    tabla2.Append("<tr class='" + (((i-1) % 4 == 0 || i == 1) ? "GridCelda" : "GridCeldaAlt") + "'>");
                                    tabla2.Append("<td>" + columna + "</td>");
                                    tabla2.Append("<td>" + valor + "</td>");
                                    tabla2.Append("</tr>");
                                }
                                
                            }
                        }
                        tabla1.Append("</table>");
                        tabla2.Append("</table>");
                    }
                }
                else
                {
                    ordenHtml.Append("<div>No se encuentra  el registro</div>");
                }
                ordenHtml.Append(tabla1);
                ordenHtml.Append(tabla2);
            }
            catch (Exception ex)
            {
                int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuario, "Respuesta/Ver","Error: " + ex.Message);
                ordenHtml.Append("<div>No se encuentra  el registro</div>");
            }
            Tiempos.Terminar(guidTiempos);
            return Json(new { usuario = usuarioN, HtmlResp = ordenHtml.ToString(), orden = idOrden }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult VerComentario(String idOrden, String proceso)
        {
            var guidTiempos = Tiempos.Iniciar();
            var context = new SistemasCobranzaEntities();
            var id = Convert.ToInt32(idOrden);
            var orden = context.Ordenes.FirstOrDefault(x => x.idOrden == id);
            if (orden == null)
            {
                Tiempos.Terminar(guidTiempos);
                return Json(new { usuario = "No encontrado", HtmlResp = "<div>No se encuentra  el registro</div>", orden = idOrden }, JsonRequestBehavior.AllowGet);
            }
            var usuario = context.Usuario.FirstOrDefault(x => x.idUsuario == orden.idUsuario);
            if (usuario == null)
            {
                Tiempos.Terminar(guidTiempos);
                return Json(new { usuario = "No encontrado", HtmlResp = "<div>No se encuentra  el usuario</div>", orden = idOrden }, JsonRequestBehavior.AllowGet);
            }
            string usuarioN = usuario.Usuario1;
            var dsComent = new EntRespuestas().ObtenerValorCampoRespuesta(Convert.ToInt32(idOrden), "comentario_final");
            if (dsComent.Tables[0].Rows.Count > 0)
            {
                string ordenHtml = dsComent.Tables[0].Rows[0]["valor"].ToString();
                Tiempos.Terminar(guidTiempos);
                return Json(new { usuario = usuarioN, HtmlResp = ordenHtml, orden = idOrden }, JsonRequestBehavior.AllowGet);
            }
            Tiempos.Terminar(guidTiempos);
            return Json(new { usuario = "No encontrado", HtmlResp = "<div>No se encuentra  el comentario</div>", orden = idOrden }, JsonRequestBehavior.AllowGet);
        }
    }
}