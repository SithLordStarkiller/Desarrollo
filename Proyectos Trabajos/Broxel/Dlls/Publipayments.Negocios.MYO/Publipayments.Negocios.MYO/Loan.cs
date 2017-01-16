using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;
using PubliPayments.Entidades.MYO;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace Publipayments.Negocios.MYO
{
    public class Loan
    {
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

                    var orden = entOrdenes.ObtenerOrdenxCredito((tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) + id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));

                    DataSet respuestas = null;
                    if (orden == null)
                    {
                        var registro =
                            new EntCredito().InsertaCreditoOrden(
                                (tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) +
                                id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'), "MYOMCG", 846, 1,
                                96, "MYO", "", tipo, id.ToString(CultureInfo.InvariantCulture));
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

                        dicRespuestas.Add("Id",
                            (tipo == "ACREDITADO" ? "AC" : (tipo == "INVERSIONISTA_1" ? "II" : "IB")) +
                            id.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'));

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
                        EntOrdenesMyo.AutorizaMyo(orden.IdOrden, 3, orden.IdVisita == 1 ? 1 : 2, " ", "0");
                    }

                    var idOrden = orden.IdOrden;
                    var urls = loan.ObtenerUrlsMyo(tipo, id);

                    if (urls.Any())
                        dicRespuestas = dicRespuestas.Concat(urls).ToDictionary(s => s.Key, s => s.Value);

                    if (respuestas != null)
                        for (var z = 0; z <= respuestas.Tables[0].Columns.Count - 1; z++)
                        {
                            var col = respuestas.Tables[0].Columns[z];
                            var row = respuestas.Tables[0].Rows[0];

                            if (dicRespuestas.ContainsKey(col.ColumnName) && row[col.ColumnName].ToString() != "") dicRespuestas.Remove(col.ColumnName);
                        }

                    //Datos adicionales
                    dicRespuestas.Add("FormiikResponseSource", "CapturaWeb");
                    dicRespuestas.Add("ExternalType", "MyoMC");

                    var respuesta = new Respuesta();
                    respuesta.GuardarRespuesta(idOrden, dicRespuestas, "", "847", 1,
                        bool.Parse(ConfigurationManager.AppSettings["Produccion"]), ConfigurationManager.AppSettings["Aplicacion"]);

                    EntOrdenesMyo.InsertarBitacoreRegistros(0, orden.IdVisita, orden.Estatus, orden.IdOrden, orden.Tipo);
                    loan.ActualizarAcreditado(id.ToString(CultureInfo.InvariantCulture), 2, tipo);

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
