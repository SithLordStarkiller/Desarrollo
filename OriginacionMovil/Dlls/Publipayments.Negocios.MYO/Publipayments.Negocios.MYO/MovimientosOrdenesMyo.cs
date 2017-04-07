using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using PubliPayments.Entidades;
using PubliPayments.Entidades.MYO;
using PubliPayments.Utiles;

namespace Publipayments.Negocios.MYO
{
    public static class MovimientosOrdenesMyo
    {
        public static string AutorizaMyo(int idOrden, string idUsuario, int idVisitaAct)
        {
            var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
            try
            {
                if (datosOrden.Etiqueta == "ACREDITADO")
                {
                    if (datosOrden.idVisita == 4)
                    {
                        EntOrdenesMyo.AutorizaMyo(idOrden, 4, idVisitaAct, "", idUsuario);
                        EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), idVisitaAct, 3, idOrden, "");

                        var ent = new EntLoan();
                        ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 6, datosOrden.Etiqueta);
                    }
                    else
                    {
                        EntOrdenesMyo.AutorizaMyo(idOrden, 3, idVisitaAct, "", idUsuario);
                        EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), idVisitaAct, 3, idOrden, "");

                    }

                    //else if (datosOrden.idVisita == 2)
                    //{
                    //    if (datosOrden.mayorAlLimite != "0")
                    //    {
                    //        EntOrdenesMyo.AutorizaMyo(idOrden, 3, 3, "", idUsuario);
                    //        EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 3, 3, idOrden, "");
                    //        var ent = new EntLoan();
                    //        ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 5, datosOrden.Etiqueta);
                    //    }
                    //    else
                    //    {
                    //        EntOrdenesMyo.AutorizaMyo(idOrden, 4, -1, "", idUsuario);
                    //        EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 2, 4, idOrden, "");
                    //        var ent = new EntLoan();
                    //        ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 6, datosOrden.Etiqueta);
                    //    }
                    //}
                    //else if (datosOrden.idVisita == 3)
                    //{
                    //    EntOrdenesMyo.AutorizaMyo(idOrden, 4, -1, "", idUsuario);
                    //    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 3, 4, idOrden, "");
                    //    var ent = new EntLoan();
                    //    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 6, datosOrden.Etiqueta);
                    //}
                }

                #region inv
                //if (datosOrden.Etiqueta == "Inversionista" && datosOrden.idVisita == 1)
                //{
                //    EntOrdenesMyo.AutorizaMyo(idOrden, 3, 4, "", idUsuario);
                //    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 4, 3, idOrden, "");
                //}
                //else
                //{
                //    if (datosOrden.Etiqueta == "Inversionista" && datosOrden.idVisita == 4)
                //    {
                //        EntOrdenesMyo.AutorizaMyo(idOrden, 3, 5, "", idUsuario);
                //        EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 5, 3, idOrden, "");
                //    }
                //    else
                //    {
                //        if (datosOrden.Etiqueta == "Inversionista" && datosOrden.idVisita == 5)
                //        {
                //            EntOrdenesMyo.AutorizaMyo(idOrden, 4, -1, "", idUsuario);
                //            EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), 5, 4, idOrden, "");
                //            var ent = new EntLoan();
                //            ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 5, datosOrden.Etiqueta);
                //        }
                //    }
                //}
                #endregion inv
            }
            catch (Exception ex)
            {
                return "Ocurrio un error al procesar la instruccion. Por favor intente de nuevo.";
            }
            return "Autorizado";
        }

        public static string RechazaMyo(int idOrden, string idUsuario)
        {
            try
            {
                var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
                EntOrdenesMyo.RechazaMyo(idOrden, idUsuario);
                EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 2, idOrden, "");
                var ent = new EntLoan();
                ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), -1, datosOrden.Etiqueta);
            }
            catch (Exception ex)
            {
                return "Ocurrio un error al procesar la instruccion. Por favor intente de nuevo.";
            }
            return "Rechazado";
        }

        public static string ReasignaMyo(int idOrden, string idUsuario)
        {
            try
            {
                var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
                if (datosOrden.idVisita == 1)
                {
                    //EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "DocCotejadoINE");
                    //EntRespuestasMYO.BorrarRespuestasOrden(idOrden, "DocCotejadoRENAPO");
                    EntOrdenesMyo.ReasignaMyo(idOrden, idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 1, idOrden, "I");
                    var ent = new EntLoan();
                    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 3, datosOrden.Etiqueta);
                }
                else if (datosOrden.idVisita == 2)
                {

                    EntOrdenesMyo.ReasignaMyo(idOrden, idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 1, idOrden, "I");
                    var ent = new EntLoan();
                    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 3, datosOrden.Etiqueta);
                }
                else if (datosOrden.idVisita == 4)
                {

                    EntOrdenesMyo.ReasignaMyo(idOrden, idUsuario);
                    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 1, idOrden, "I");
                    var ent = new EntLoan();
                    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 3, datosOrden.Etiqueta);
                }
                //else
                //{
                //    EntOrdenesMyo.ReasignaMyo(idOrden, idUsuario);
                //    EntOrdenesMyo.InsertarBitacoreRegistros(Convert.ToInt32(idUsuario), datosOrden.idVisita, 1, idOrden, "I");
                //    var ent = new EntLoan();
                //    ent.ActualizarAcreditado(datosOrden.idFlock.ToString(), 3, datosOrden.Etiqueta);
                //}
            }
            catch (Exception ex)
            {
                return "Ocurrio un error al procesar la instruccion. Por favor intente de nuevo.";
            }
            return "Reasignado";
        }

        public static string ProcesarRespuestas(int idOrden, int idusuario)
        {
            var formularioModel = ObtenerFormulariosXOrden(idOrden, idusuario, 2)[0];
            string resultado = "";
            switch (formularioModel.Nombre.ToUpper())
            {
                case "MYOMC":
                    break;
                case "MYOREFERENCIAS":
                    resultado = ValidaReferencias(idOrden, idusuario);
                    break;
            }
            return resultado;
        }

        public static string ValidaReferencias(int idOrden, int idusuario)
        {
            var entLoan = new EntLoan();
            var respuestas = new EntRespuestasMYO().ProcesarRespuestasMyo(idOrden);
            string resultado = "";
            if (respuestas.Tables.Count > 0)
            {
                var situacion = respuestas.Tables[0].Rows[0]["Resultado"].ToString();

                switch (situacion.ToUpper())
                {
                    case "VALIDO":
                        resultado = AutorizaMyo(idOrden, idusuario.ToString(CultureInfo.InvariantCulture),1);
                        break;
                    case "REASIGNAR":
                        var referenciasList = new List<ReferenciasModel>();
                        foreach (DataRow rows in respuestas.Tables[1].Rows)
                        {
                            EntRespuestasMYO.BorrarRespuestasOrden(idOrden, rows["tipoRef"].ToString());
                            entLoan.ActualizarDatosRefImg(rows["resultado"].ToString(), Convert.ToInt32(rows["idRef"].ToString()), rows["tipoRef"].ToString().Substring(0, rows["tipoRef"].ToString().Length - 1), 3);
                            referenciasList.Add(new ReferenciasModel { Comentario = rows["resultado"].ToString(), IdDocumento = rows["idRef"].ToString(), Nombre = rows["tipoRef"].ToString() });
                        }

                        CorreosMyo.ReferenciasIncorrectas(referenciasList, idOrden);
                        resultado = ReasignaMyo(idOrden, idusuario.ToString());
                        break;
                    case "RECHAZAR":
                        resultado = RechazaMyo(idOrden, idusuario.ToString());
                        break;
                }
            }
            return resultado;

        }
        /// <summary>
        ///  Obtiene los datos del formulario que esta relacionado a la Orden dada
        /// </summary>
        /// <param name="idOrden">orden a a buscar</param>
        /// <param name="idusuario">id usuario a la que este asignada, para el caso que el formulario sea distinto entre dominios</param>
        /// <param name="captura">tipo de captura a buscar 1-mobil, 2-CW, 0 - sin filtro</param>
        /// <returns>Lista de los formularios relacionados</returns>
        public static List<FormularioModel> ObtenerFormulariosXOrden(int idOrden, int idusuario, int captura)
        {
            var entFormulario = new EntFormulario();
            return entFormulario.ObtenerFormularioXOrden(idOrden, idusuario, captura);
        }

        static readonly string ConsultaOfac = ConfigurationManager.AppSettings["CunsultaOFAC"] ?? "";
        static readonly bool ValidaOFac = (ConsultaOfac == "true");

        public static bool ValidaMyo(Dictionary<string, string> docs, int idOrden, int idVisita)
        {
            const string cadRec = "Hola! Lo sentimos, tu  solicitud de Crédito ha sido Rechazada";
            const string cadNom =
                "El nombre que aparece en la identificación NO coincide con los del solicitante ni con su CURP, favor de corregirlo";
            const string cadIng = "Ingresa a la pantalla de \"Datos del Empleo\" y asegurate que el Sueldo Mensual que registraste coincida con tus Comprobantes de Ingresos";
            const string cadCurp =
                "Hola, el número de cuenta de tu CURP no está registrado, es necesario que te des de alta en el RENAPO y la vuelvas a cargar";

            const string cadDom =
                "Ingresa a la pantalla de Domicilio y asegurate que el Domicilio que registraste sea EXACTAMENTE igual al de tu Comprobante de Domicilio";
            var respuestas = ObtenerRespuestasDic(new EntLoan().ObtenerRespuestas(idOrden.ToString(CultureInfo.InvariantCulture)));
            var datosOrden = EntOrdenesMyo.ObtenerInfoOrdenMyo(idOrden);
            var res = "";
            string resAut = "";

            try
            {
                if (idVisita == 1)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "Enviando a validar Orden : " + idOrden + " Visita 1");
                    if (ValidaOFac)
                    {
                        res = new wsClientes.ClientesClient().ConsultaOFAC(respuestas["Name"],
                            respuestas["LastNameFather"],
                            respuestas["LastNameMother"], "", "", "", "");
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                            "OFAC Orden : " + idOrden + " Res : " + res);
                    }

                    if (docs.Count == 0)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                            "Enviando a validar Orden : " + idOrden + " OFAC No");
                        //if (res.ToUpper() == "EXISTEN COINCIDENCIAS")
                        resAut = AutorizaMyo(idOrden, "-1", 3);
                        //else
                        //{
                        //    var resDes = NivCua(datosOrden.idFlock);
                        //    if (resDes)
                        //        resAut = AutorizaMyo(idOrden, "-1", 4);
                        //}
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                            "OFAC Orden : " + idOrden + " ResM : " + resAut);
                        return resAut == "Autorizado";
                    }
                    var entLoan = new EntLoan();

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl", "Procesando Orden : " + idOrden);

                    var docsRes = ObtenerDocsResp(idOrden);

                    foreach (var doc in docs)
                    {
                        EntRespuestasMYO.BorrarRespuestasOrden(idOrden, doc.Key + "Rechazo");
                        EntRespuestasMYO.InsertarRespuesta(idOrden, doc.Key + "Rechazo", doc.Value);

                        if (doc.Key.ToUpper() == "IDENTIFICACION_OFICIAL")
                        {
                            var idLoanImage = docsRes.FirstOrDefault(x => x.Key == "Identidad1");
                            if (idLoanImage.Key != null)
                            {
                                var idLoan = idLoanImage.Value.Split('*')[1];
                                entLoan.ActualizarDatosRefImg(doc.Value, Convert.ToInt32(idLoan), "IMGACR", -1);
                            }
                            else
                            {
                                var idLoanImage1 = docsRes.FirstOrDefault(x => x.Key == "INEBack1");
                                var idLoanImage2 = docsRes.FirstOrDefault(x => x.Key == "INEFront1");
                                if (idLoanImage1.Value == "") continue;
                                if (idLoanImage2.Value == "") continue;
                                var idLoan1 = idLoanImage1.Value.Split('*')[1];
                                var idLoan2 = idLoanImage2.Value.Split('*')[1];
                                entLoan.ActualizarDatosRefImg(doc.Value, Convert.ToInt32(idLoan1), "IMGACR", -1);
                                entLoan.ActualizarDatosRefImg(doc.Value, Convert.ToInt32(idLoan2), "IMGACR", -1);
                            }
                        }
                        else if (doc.Key.ToUpper() == "COMPROBANTE_INGRESOS")
                        {
                            var idLoanImage = docsRes.FirstOrDefault(x => x.Key == "Ingreso1");
                            if (idLoanImage.Value == "")
                            {
                                new EntLoan().ActualizarField("IncomeNet", datosOrden.idFlock, "", 3, "");
                                continue;
                            }
                            var idLoan = idLoanImage.Value.Split('*')[1];
                            entLoan.ActualizarDatosRefImg(doc.Value, Convert.ToInt32(idLoan), "IMGACR", -1);

                            if (doc.Value.Contains(cadIng))
                                new EntLoan().ActualizarField("IncomeNet", datosOrden.idFlock, "", -1, doc.Value);
                            else
                                new EntLoan().ActualizarField("IncomeNet", datosOrden.idFlock, "", 3, "");
                        }
                        else if (doc.Key.ToUpper() == "COMPROBANTE_DOMICILIO")
                        {
                            var idLoanImage = docsRes.FirstOrDefault(x => x.Key == "Domicilio1");
                            if (idLoanImage.Value == "") continue;
                            var idLoan = idLoanImage.Value.Split('*')[1];
                            entLoan.ActualizarDatosRefImg(doc.Value, Convert.ToInt32(idLoan), "IMGACR", -1);
                        }
                        else if (doc.Key.ToUpper() == "CURP")
                        {
                            if (doc.Value.Contains(cadCurp))
                                new EntLoan().ActualizarField("Curp", datosOrden.idFlock, "", -1, doc.Value);
                            else
                                new EntLoan().ActualizarField("Curp", datosOrden.idFlock, "", 3, "");
                        }
                    }

                    if (docs.ContainsKey("IDENTIFICACION_OFICIAL"))
                    {
                        if (docs["IDENTIFICACION_OFICIAL"].Contains(cadRec))
                        {
                            resAut = RechazaMyo(idOrden, "-1");
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                                "OFAC Orden : " + idOrden + " ResM : " + resAut);
                            return resAut == "Rechazado";
                        }
                        if (docs["IDENTIFICACION_OFICIAL"].Contains(cadNom))
                        {
                            resAut = AutorizaMyo(idOrden, "-1", 2);
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                                "OFAC Orden : " + idOrden + " ResM : " + resAut);
                            return resAut == "Autorizado";
                        }
                    }
                    if (docs.ContainsKey("COMPROBANTE_DOMICILIO"))
                        if (docs["COMPROBANTE_DOMICILIO"].Contains(cadDom))
                        {
                            resAut = AutorizaMyo(idOrden, "-1", 2);
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                                "OFAC Orden : " + idOrden + " ResM : " + resAut);
                            return resAut == "Autorizado";
                        }

                    resAut = ReasignaMyo(idOrden, "-1");
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "OFAC Orden : " + idOrden + " ResM : " + resAut);
                    return resAut == "Reasignado";

                    //if (res.ToUpper() == "EXISTEN COINCIDENCIAS")
                    //{
                    resAut = AutorizaMyo(idOrden, "-1", 3);
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "OFAC Orden : " + idOrden + " ResM : " + resAut);
                    return resAut == "Autorizado";
                    //}
                }
                if (idVisita == 2)
                {
                    if (respuestas.ContainsKey("ConfirmacionNombre"))
                    {
                        if (respuestas["ConfirmacionNombre"].ToUpper() == "NO")
                        {
                            resAut = RechazaMyo(idOrden, "-1");
                            return resAut == "Reasignado";
                        }
                    }

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "Enviando a validar Orden : " + idOrden + " Visita 2");
                    resAut = ReasignaMyo(idOrden, "-1");
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "OFAC Orden : " + idOrden + " ResM : " + resAut);

                    return resAut == "Reasignado";
                }
                if (idVisita == 3)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "Enviando a validar Orden : " + idOrden + " Visita 4");
                    var homonimo = docs["Homonimo"];

                    if (homonimo.ToUpper() == "SI")
                    {
                        var resDes = NivCua(datosOrden.idFlock);

                        if (!resDes)
                            return false;

                        resAut = AutorizaMyo(idOrden, "-1", 4);
                    }
                    else
                        resAut = RechazaMyo(idOrden, "-1");

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl",
                        "OFAC Orden : " + idOrden + " ResM : " + resAut);
                    return resAut == "Autorizado";
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "MesaControl",
                    "Error Orden : " + idOrden + " Msj : " + ex.Message);
                return false;
            }
        }

        public static bool NivCua(int idLoan)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.75.23:8081/api/BroxelAPI");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json =
                        "{ " +
                        "	CodApp : 'A07E70F8-34F9-48C1-ACC7-B64889886E74', " +
                        "   Version : '1.0', " +
                        "   GuidClase : '394445ED-3642-45BA-B88E-560F01C6202A', " +
                        "   GuidMetodo : 'B47F3347-BCED-4B2C-ABA0-0C9A38F3CE51', " +
                        "   Plataforma : 'Web', " +
                        "   DetalleParametro : { " +
                        "       usuario: 'brxl007',  " +
                        "       password : '}5gI=OQ]mq:44fXTUuai', " +
                        "       email : 'mail@mail.com', " +
                        "       idPrestamo: " + idLoan + " " +
                        "	}, " +
                        "	Seguridad: { " +
                        "		IdSecure:'ffDZZdxqYzb3Yev9oqN2Fou7wbCRBVTA+00w5ghaehI=' " +
                        "	} " +
                        "} ";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                string result;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                var jss = new JavaScriptSerializer();
                var model = jss.Deserialize<ResponseApiBroxel>(result);
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "MesaControl", "OFAC Loan : " + idLoan + " Modelo Niv : " + new JavaScriptSerializer().Serialize(model));
                return model.Codigo == 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; }

        public static Dictionary<string, string> ObtenerDocsResp(int idOrden)
        {
            var dic = new Dictionary<string, string>();
            var docs = new EntGestionadas().ObtenerDocumentosOrden(idOrden.ToString(CultureInfo.InvariantCulture), "MYO");

            foreach (DataRow row in docs.Tables[1].Rows)
            {
                if (row["NombreCampo"].ToString() == "Domicilio1")
                    dic.Add(row["NombreCampo"].ToString(), row["Valor"].ToString());

                if (row["NombreCampo"].ToString() == "Identidad1" || row["NombreCampo"].ToString() == "INEBack1" || row["NombreCampo"].ToString() == "INEFront1")
                    dic.Add(row["NombreCampo"].ToString(), row["Valor"].ToString());

                if (row["NombreCampo"].ToString() == "Ingreso1")
                    dic.Add(row["NombreCampo"].ToString(), row["Valor"].ToString());
            }
            return dic;
        }

        public static Dictionary<string, string> ObtenerRespuestasDic(DataSet ds)
        {
            var dic = new Dictionary<string, string>();
            if (ds.Tables.Count <= 0)
                return new Dictionary<string, string>();

            if (ds.Tables[0].Rows.Count <= 0)
                return new Dictionary<string, string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dic.Add("Name", row["Primer nombre"] + " " + row["Segundo nombre"]);
                dic.Add("LastNameFather", row["Apellido paterno"].ToString());
                dic.Add("LastNameMother", row["Apellido materno"].ToString());
                if (ds.Tables[0].Columns.Contains("ConfirmacionNombre"))
                    dic.Add("ConfirmacionNombre", row["ConfirmacionNombre"].ToString());
            }
            return dic;
        }
    }

    public class ResponseApiBroxel
    {
        public int Codigo { get; set; }
        public string MensajeRespuesta { get; set; }
        public string Resultado { get; set; }
    }
}
