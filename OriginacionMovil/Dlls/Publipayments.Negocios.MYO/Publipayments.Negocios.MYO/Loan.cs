using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml;
using PubliPayments.Entidades.MYO;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using Publipayments.Negocios.MYO.MesaControl;
using PubliPayments.Utiles;

namespace Publipayments.Negocios.MYO
{
    public class Loan
    {
        //Si se modifican estos textos modificar en Publipayments.Negocios.MYO MovimientosOrdenesMyo.cs
        const string cadRec = "Hola! Lo sentimos, tu  solicitud de Crédito ha sido Rechazada";
        const string cadNom = "El nombre que aparece en la identificación NO coincide con los del solicitante ni con su CURP, favor de corregirlo";
        const string cadIng = "Ingresa a la pantalla de \"Datos del Empleo\" y asegurate que el Sueldo Mensual que registraste coincida con tus Comprobantes de Ingresos";
        const string cadCurp = "Hola, el número de cuenta de tu CURP no está registrado, es necesario que te des de alta en el RENAPO y la vuelvas a cargar";
        const string cadDom = "Ingresa a la pantalla de Domicilio y asegurate que el Domicilio que registraste sea EXACTAMENTE igual al de tu Comprobante de Domicilio";

        /// <summary>
        /// Obtiene los datos capturados por el UI de Flock
        /// </summary>
        public void GenerarXml()
        {
            string tipo;
            var id = -1;
            Dictionary<string, string> dicRespuestas;
            var loan = new EntLoan();
            var entOrdenes = new EntOrdenes();
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Loan", "GenerarXml Inicio");
            var datos = loan.ObtenerDatosFlock();
            if (datos.Tables.Count == 0) return; if (datos.Tables[0].Rows.Count <= 0) return;

            var idDist = (from d in datos.Tables[0].AsEnumerable() select d[0] + "|" + d[3]).Distinct().ToList();

            foreach (var resDist in idDist)
            {
                try
                {
                    tipo = resDist.Split('|')[1];
                    id = int.Parse(resDist.Split('|')[0]);
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Loan", "GenerarXml - id " + id);
                    var id1 = id;
                    var tipo1 = tipo;
                    dicRespuestas = datos.Tables[0].AsEnumerable().Where(row => row.Field<int>("id") == id1 && row.Field<string>("tipo") == tipo1).ToDictionary(row => row.Field<string>("campo"), row => row.Field<string>("respuesta"));

                    var datosReferencias = loan.ObtenerDatosReferencias(id, tipo);

                    if (datosReferencias.Tables.Count > 0)
                    {
                        var lista = datosReferencias.Tables[0].AsEnumerable()
                            .ToDictionary(row => row.Field<string>("Id"), row => row.Field<string>("Datos"));

                        dicRespuestas = dicRespuestas.Concat(lista).ToDictionary(s => s.Key, s => s.Value);
                    }

                    dicRespuestas.Add("Id", (tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) + id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));

                    var orden = entOrdenes.ObtenerOrdenxCredito((tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) + id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));

                    DataSet respuestas = null;

                    var resRechazo = loan.RechazoCorrecionMyo(id);

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Rechazo Correcion : " + resRechazo + " Loan : " + id);

                    if (orden == null)
                    {
                        var registro =
                            new EntCredito().InsertaCreditoOrden(
                                (tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) +
                                id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'), "MYOMCG", 846, 1,
                                96, "MYO", "", tipo, id.ToString(CultureInfo.InvariantCulture), "", "");
                        if (!registro)
                        {
                            const string mensaje = "GenerarXml - No se generó correctamente el crédito";
                            dicRespuestas.Clear();
                            Trace.WriteLine(string.Format("{0} - {1}", DateTime.Now, mensaje));
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Loan", mensaje);
                            continue;
                        }

                        orden =
                            entOrdenes.ObtenerOrdenxCredito((tipo == "ACREDITADO"
                                ? "AC"
                                : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) +
                                                                    id.ToString(CultureInfo.InvariantCulture)
                                                                        .PadLeft(10, '0'));

                        //dicRespuestas.Add("Id",
                        //    (tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) +
                        //    id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));

                        var consultaCirculo = LeerXml(dicRespuestas);
                        if (consultaCirculo != "")
                        {
                            var direcciones = consultaCirculo.Split('|');
                            var ii = 1;
                            for (var xx = 0; xx <= direcciones.Length - 1; xx++)
                                if (direcciones[xx] != "")
                                {
                                    dicRespuestas.Add("CirCreDireccion" + (ii).ToString(CultureInfo.InvariantCulture), direcciones[xx]);
                                    ii++;
                                }
                        }
                        else
                            dicRespuestas.Add("CirCreDireccion1", "No hay datos en circulo de credito");
                    }
                    else
                    {
                        respuestas = loan.ObtenerRespuestas(orden.IdOrden.ToString(CultureInfo.InvariantCulture), 1);

                        var dicRespuestasOld = DicRespuestas(respuestas);

                        //var autMesaNom = false;
                        //var autMesaDom = false;

                        //if(dicRespuestasOld.Count > 0)
                        //    if (dicRespuestasOld.Count == 1 && dicRespuestasOld.ContainsKey("IDENTIFICACION_OFICIALRechazo"))
                        //        if (dicRespuestasOld["IDENTIFICACION_OFICIALRechazo"].Contains(cadNom))
                        //            autMesaNom = true;

                        //if (dicRespuestasOld.Count > 0)
                        //    if (dicRespuestasOld.Count == 1 && dicRespuestasOld.ContainsKey("IDENTIFICACION_OFICIALRechazo"))
                        //        if (dicRespuestasOld["IDENTIFICACION_OFICIALRechazo"].Contains(cadNom))
                        //            autMesaNom = true;

                        //if (orden.IdVisita == 1)
                        //{
                        //    if(resRechazo)
                        //        EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 2, " ", "0");

                        //    if (autMesaNom)
                        //    {
                        //        EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 3, " ", "0");
                        //        orden.IdVisita = 3;
                        //    }
                        //    else
                        //        EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 1, " ", "0");
                        //}
                        ////else if(orden.IdVisita == 2)
                        ////    EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 3, " ", "0");
                        ////else if (orden.IdVisita == 3)
                        ////    EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 3, " ", "0");
                        //else if (orden.IdVisita == 4)
                        //    EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 4, " ", "0");

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Actualizando Loan : " + id + " IdOrden : " + orden.IdOrden + " IdVisita : " + orden.IdVisita);

                        if (resRechazo && orden.IdVisita == 1)
                            EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 2, " ", "0");
                        else if (!resRechazo && orden.IdVisita == 2)
                            EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 3, " ", "0");
                        else if (orden.IdVisita == 4)
                            EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, 4, " ", "0");
                        else
                            EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, orden.IdVisita == 1 ? 1 : 2, " ", "0");
                    }

                    if (resRechazo && orden.IdVisita == 1)
                        dicRespuestas.Add("RechazoUsuarioNo", "No acepto Rechazo");

                    var idOrden = orden.IdOrden;
                    var urls = loan.ObtenerUrlsMyo(tipo, id);

                    if (urls.Any())
                        dicRespuestas = dicRespuestas.Concat(urls).ToDictionary(s => s.Key, s => s.Value);

                    var dicAuxMc = new Dictionary<string, string>(dicRespuestas);

                    if (respuestas != null)
                    {

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Respuestas Loan : " + id + " Columnas " + respuestas.Tables[0].Columns.Count);

                        for (var z = 0; z <= respuestas.Tables[0].Columns.Count - 1; z++)
                        {
                            var col = respuestas.Tables[0].Columns[z];
                            var row = respuestas.Tables[0].Rows[0];

                            if (dicRespuestas.ContainsKey(col.ColumnName) && row[col.ColumnName].ToString() != "")
                                dicRespuestas.Remove(col.ColumnName);
                        }
                    }

                    //Datos adicionales
                    dicRespuestas.Add("FormiikResponseSource", "CapturaWeb");
                    dicRespuestas.Add("ExternalType", "MyoMC");

                    //dicRespuestas = (Dictionary<string, string>) dicRespuestas.Where(m => m.Value != "");

                    dicRespuestas = dicRespuestas.Where(x => x.Value != "").ToDictionary(x => x.Key, x => x.Value);

                    var respuesta = new Respuesta();
                    respuesta.GuardarRespuesta(idOrden, dicRespuestas, "", "847", 1,
                        bool.Parse(ConfigurationManager.AppSettings["Produccion"]), ConfigurationManager.AppSettings["Aplicacion"]);

                    var urlMyo = ConfigurationManager.AppSettings["URLMYO"] + "";
                    var docLocal = ConfigurationManager.AppSettings["DocLocal"] + "";
                    var docLocal2 = ConfigurationManager.AppSettings["DocLocal2"] + "";

                    EntOrdenesMyo.InsertarBitacoreRegistros(0, orden.IdVisita, orden.Estatus, orden.IdOrden, orden.Tipo);

                    var urlDocMc = "";

                    if ((orden.IdVisita == 1) && !resRechazo)
                    {
                        try
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Mandando a MC : " + orden.IdOrden + " IdVisita : " + orden.IdVisita);
                            if (urls.Any())
                            {
                                if (!urls.ContainsKey("DocMC1"))
                                {
                                    Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Loan", "No se encontro el archivo DocMC : " + orden.IdOrden);
                                    continue;
                                }

                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Ruta : " + urls["DocMC1"].Split('*')[0]);

                                var path1 = urls["DocMC1"].Split('*')[0].Replace("http://", "").Replace("https://", "");

                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Ruta : " + path1);

                                var url = path1.Replace(docLocal, urlMyo);

                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "DocLocal : " + docLocal + " urlMyo : " + urlMyo);

                                url = url.Replace(docLocal2, urlMyo);

                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "DocLocal2 : " + docLocal2 + " urlMyo : " + urlMyo);
                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Ruta Ori : " + path1);
                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "Ruta Remplaza : " + url);
                                urlDocMc = url;

                                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "urlDocMc : " + urlDocMc);
                            }

                            var model = new ModelosModeloEnviarDocumento()
                            {
                                Url = urlDocMc,
                                Xml = dicAuxMc
                            };

                            var res = new MesaControlClient().EnviarDocumento(model);

                            if (res == null)
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Loan", "Error Modelo Nulo MC ");
                                throw new Exception("Error al registrar : " + res.Mensaje);
                            }

                            EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "COMPROBANTE_DOMICILIORechazo");
                            EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "IDENTIFICACION_OFICIALRechazo");
                            EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "COMPROBANTE_INGRESOSRechazo");
                            EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "CURPRechazo");

                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "ResMC : " + new JavaScriptSerializer().Serialize(res));

                            if (res.Codigo == 1)
                                loan.ActualizarAcreditado(id.ToString(CultureInfo.InvariantCulture), 2, tipo);
                            else
                                throw new Exception("Error al registrar : " + res.Mensaje);

                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Loan",
                                "Error al enviar a mesa de control : " + ex.Message);
                        }
                    }
                    else
                    {
                        loan.ActualizarAcreditado(id.ToString(CultureInfo.InvariantCulture), 2, tipo);
                    }

                    dicRespuestas.Clear();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "Loan", "GenerarXml id = " + id + ", Error: " + ex.Message);
                    Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error al procesar datos Flock", "Error al procesar el id : " + id);
                }
            }
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, "Loan", "GenerarXml Final");
        }

        public Dictionary<string, string> DicRespuestas(DataSet ds)
        {
            var dic = new Dictionary<string, string>();
            foreach (DataRow rw in ds.Tables[0].Rows)
            {
                if(ds.Tables[0].Columns.Contains("COMPROBANTE_DOMICILIORechazo"))
                    if(!string.IsNullOrEmpty(rw["COMPROBANTE_DOMICILIORechazo"].ToString()))
                        dic.Add("COMPROBANTE_DOMICILIORechazo",rw["COMPROBANTE_DOMICILIORechazo"].ToString());

                if(ds.Tables[0].Columns.Contains("COMPROBANTE_INGRESOSRechazo"))
                    if (!string.IsNullOrEmpty(rw["COMPROBANTE_INGRESOSRechazo"].ToString()))
                        dic.Add("COMPROBANTE_INGRESOSRechazo", rw["COMPROBANTE_INGRESOSRechazo"].ToString());

                if(ds.Tables[0].Columns.Contains("CURPRechazo"))
                    if (!string.IsNullOrEmpty(rw["CURPRechazo"].ToString()))
                        dic.Add("CURPRechazo", rw["CURPRechazo"].ToString());

                if(ds.Tables[0].Columns.Contains("IDENTIFICACION_OFICIALRechazo"))
                    if (!string.IsNullOrEmpty(rw["IDENTIFICACION_OFICIALRechazo"].ToString()))
                        dic.Add("IDENTIFICACION_OFICIALRechazo", rw["IDENTIFICACION_OFICIALRechazo"].ToString());
            }

            return dic;
        }

        /// <summary>
        /// Consulta Circulo de credito, Cotejo de documentos con informacion de circulo de credito
        /// </summary>
        /// <param name="dicRespuestas">Datos que se envian a circulo de credito</param>
        /// <returns>Cadena con domicilios del acreditado</returns>
        public string LeerXml(Dictionary<string, string> dicRespuestas)
        {
            //var nodosLista = new List<XmlNode>();
            //var nodosRespuesta = new List<XmlNode>();
            var direcciones = "";
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Loan", "LeerXml Circulo Credito ");

                var xml = new EntLoan().ObtenerXmlCirculoAzul(dicRespuestas["Id"] != null ? int.Parse(dicRespuestas["Id"].Substring(2)) : 0);

                if (xml == null) return "";

                foreach (DataRow dato2 in xml.Tables[0].Rows)
                    direcciones = dato2[4] + "|" + dato2[5] + "|" + dato2[6];

                return direcciones;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "Loan", "LeerXml Circulo Credito - " + ex.Message);
                return "";
            }
        }

        ///// <summary>
        ///// Respuesta de Circulo de credito
        ///// </summary>
        ///// <param name="xml"></param>
        ///// <returns></returns>
        //public XmlDocument ObtenerDatosWs(XmlDocument xml)
        //{
        //    var channel = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["UrlCirculoBuro"]);
        //    channel.Method = "POST";
        //    channel.ContentType = "application/x-www-form-urlencoded";
        //    var sw = new StreamWriter(channel.GetRequestStream());
        //    xml.Save(sw);
        //    var sr = channel.GetResponse();
        //    var responseStream = sr.GetResponseStream();
        //    var xmlResult = new XmlDocument();
        //    if (responseStream != null) xmlResult.Load(@"C:\MYO\Respuesta.xml");
        //    //if (responseStream != null) xmlResult.Load(responseStream);
        //    return xmlResult;
        //}

        public void ObtenerValor(XmlNode nodo, ref List<XmlNode> listaNodos)
        {
            if (nodo.HasChildNodes)
            {
                listaNodos.Add(nodo);
                foreach (XmlNode nodo2 in nodo)
                    ObtenerValor(nodo2, ref listaNodos);
            }
            else
                listaNodos.Add(nodo);
        }
    }
}
