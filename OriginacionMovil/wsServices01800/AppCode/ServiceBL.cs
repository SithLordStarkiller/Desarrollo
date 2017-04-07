using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using IBC.AppCode;
using IInfonavit;
using IInfonavit.Infonavit;
using IInfonavit.PrecalificacionMejoravit2;
using PubliPayments.Entidades;
using PubliPayments.Negocios.Originacion.ConsultaEstatus2;
using PubliPayments.Utiles;
using wsServices01800.AppCode.Requests;
using wsServices01800.Formiik;

namespace wsServices01800.AppCode
{
    public class ServiceBL
    {
        private static readonly bool IsProduction = ConfigurationManager.AppSettings["ProducBuro"] != null && ConfigurationManager.AppSettings["ProducBuro"].ToLower() == "true";
        public FlexibleUpdateResponse Precalificacion(string nss, long id, string usuario ="", string pension = "", string nombreOficina = "", string oficina = "")
        {
            var fResponse = new FlexibleUpdateResponse();
            var msj = new FlexibleUpdateReservedWords();

            var dbUtil = new DBUtil();


            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL/Precalificacion", "Nss : " + nss + " usuario : " + usuario + "Pension : " + pension);

                if (string.IsNullOrEmpty(usuario))
                    usuario = "usuarioDefault";

                var entrada = new ProgramInterfaceWs_entrada {ws_nss = nss, ws_pen_alim = pension, ws_proceso = ""};

                var proxyInfo = new Consultas();

                var idP=dbUtil.InsPrecalificaRequest(entrada, id,usuario,nombreOficina,oficina);

                //var salida = proxyInfo.Precalifica(entrada);

                WsRespuestaPreca salida ;

                if (ConfigurationManager.AppSettings["Produccion"]=="true")
                {
                    salida = (WsRespuestaPreca)proxyInfo.PrecalificaNew(entrada, usuario, nombreOficina, oficina.Length >= 5 ? oficina: oficina.PadLeft(5, '0'));

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL/Precalificacion", "Nss : " + nss + "Respuesta : " + salida.WSMensaje);
                }
                else
                {
                    var wsr1 = new WsRespuestaPlazos
                {
                    Plazo = "12",
                    Contarias = "14100",
                    MontoCredito = "13000",
                    MontoManoObra = "100",
                    PagoMensual = "1300"
                };
                var wsr2 = new WsRespuestaPlazos
                {
                    Plazo = "18",
                    Contarias = "14200",
                    MontoCredito = "13000",
                    MontoManoObra = "100",
                    PagoMensual = "1400"
                };
                var wsr3 = new WsRespuestaPlazos
                {
                    Plazo = "24",
                    Contarias = "14300",
                    MontoCredito = "13000",
                    MontoManoObra = "100",
                    PagoMensual = "1500"
                };
                var wsr4 = new WsRespuestaPlazos
                {
                    Plazo = "30",
                    Contarias = "14400",
                    MontoCredito = "13000",
                    MontoManoObra = "100",
                    PagoMensual = "1600"
                };

                var salida1 = new WsRespuestaPreca
                {
                    WSMensaje = "OK",
                    WSidMensaje = "0000",
                    WSCurp = "RASM820302HGTMNG05",
                    WSGastosApertura = "0",
                    WSNombreEmpresa = "01800Pagos",
                    WSNombreTitular = "Ramirez Sanchez Miguel Angel",
                    WSNumPlazos = "4",
                    WSNumRegistroPatronal = "13132313132",
                    WSPuntMin = "0",
                    WSPuntTotal = "12",
                    WSRFC = "RASM820302IC2",
                    WSTasaInteres = "18.5",
                    WS_Ocur = new WsRespuestaPlazos[] {wsr1, wsr2, wsr3, wsr4}
                };

                salida = salida1;
                }
                
                if (idP > 0)
                    dbUtil.InsPrecalificaResponse(salida,idP);

                if (salida.WSidMensaje.Trim() != "0000")
                {
                    fResponse.AfectedFields.Add(new FlexibleField("Mensaje", true, false, true));
                    fResponse.UpdateFieldsValues.Add("Mensaje", ((salida.WSidMensaje.Trim() == "0004" || salida.WSidMensaje.Trim() == "0005") ? "Existió un error al consultar NSS: " : "") + salida.WSMensaje.Trim());
                    fResponse.UpdateFieldsValues.Add("CURP", "");
                    fResponse.UpdateFieldsValues.Add("ActivacionPestanas", "No" );
                    fResponse.AfectedFields.Add(new FlexibleField("ActivacionPestanas", true, true, false));
                    fResponse.AfectedFields.Add(new FlexibleField("CURP", true, true, true));

                    msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta Terminada" };
                    fResponse.FormiikReservedWords.Add(msj);

                    return fResponse;
                }

                var campos = ObtenerDatosPrecalificacion(salida);
                if (ConfigurationManager.AppSettings["ReiniciarCurp"].ToString()=="true")
                {
                    salida.WSCurp = "";
                }
                var reinicioCredito = EntCredito.CancelarCredito(nss);

                fResponse.UpdateFieldsValues.Add("Nss", nss);
                fResponse.UpdateFieldsValues.Add("RfcPrecalificacion",salida.WSRFC);
                fResponse.UpdateFieldsValues.Add("Nombre", salida.WSNombreTitular);
                fResponse.UpdateFieldsValues.Add("PlazosInfo", GetPlazosInfo(salida));
                fResponse.UpdateFieldsValues.Add("NumeroPlazos",salida.WSNumPlazos);
                fResponse.UpdateFieldsValues.Add("TasaInteres",salida.WSTasaInteres);
                fResponse.UpdateFieldsValues.Add("Plazo", FormatOcurDropDown(salida.WS_Ocur));
                fResponse.UpdateFieldsValues.Add("TablaINE", "<html><head></head><body>" +
                                                             "<a href = \"http://listanominal.ife.org.mx/\">Consulta INE</a>" +
                                                             //"<br/><br/>" +
                                                             //"<a href=\"http://www.renapo.gob.mx/swb/swb/RENAPO/consultacurp\">Consulta CURP</a>" +
                                                             "</body></html>");
                fResponse.UpdateFieldsValues.Add("Mensaje", reinicioCredito!="OK"?reinicioCredito:(salida.WSCurp.Trim() == "" ? "CURP Incorrecto/Incompleto: Informar al Derechohabiente que es necesario realizar la corrección, debe registrarse/ingresar a Mi Cuenta Infonavit o solicitar la corrección directamente en su AFORE." : ""));
                fResponse.UpdateFieldsValues.Add("CURP", salida.WSCurp.Trim());
                fResponse.UpdateFieldsValues.Add("Nombres", campos["Nombres"]);
                fResponse.UpdateFieldsValues.Add("APaterno", campos["APaterno"]);
                fResponse.UpdateFieldsValues.Add("Amaterno", campos["Amaterno"]);
                fResponse.UpdateFieldsValues.Add("GeneroCB", campos["GeneroCB"]);
                fResponse.UpdateFieldsValues.Add("FechadeNacimientoCB", campos["FechadeNacimientoCB"]);
                fResponse.UpdateFieldsValues.Add("ActivacionPestanas", salida.WSCurp.Trim()==""?"No":"Si");

                fResponse.AfectedFields.Add(new FlexibleField("Nss", false, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("RfcPrecalificacion", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("Nombre", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("PlazosInfo", false, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("NumeroPlazos",true,true,false));
                fResponse.AfectedFields.Add(new FlexibleField("TasaInteres", true, true, false));
                fResponse.AfectedFields.Add(new FlexibleField("Plazo",false,true,true));
                fResponse.AfectedFields.Add(new FlexibleField("TablaINE",false,true,true));
                fResponse.AfectedFields.Add(new FlexibleField("Mensaje", true, false, salida.WSCurp.Trim() == "" || reinicioCredito != "OK"));
                fResponse.AfectedFields.Add(new FlexibleField("CURP", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("Nombres", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("APaterno", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("Amaterno", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("GeneroCB", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("FechadeNacimientoCB", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("ActivacionPestanas", true, true, false));
                
                for (var i = 0; i < salida.WS_Ocur.Length; i++)
                {
                    if (i < Convert.ToInt32(salida.WSNumPlazos))
                    {
                        fResponse.UpdateFieldsValues.Add("Plazo_" + (i+1).ToString("D2"),
                            FormatOcur(salida.WS_Ocur[i], salida.WSGastosApertura));
                        fResponse.AfectedFields.Add(new FlexibleField("Plazo_" + (i + 1).ToString("D2"), true, true, true));
                        fResponse.AfectedFields.Add(new FlexibleField("Plazo_Correcto_" + (i + 1).ToString("D2"), false, true,
                            true));
                    }
                    else
                    {
                        fResponse.AfectedFields.Add(new FlexibleField("Plazo_" + (i + 1).ToString("D2"), true, false, false));
                        fResponse.AfectedFields.Add(new FlexibleField("Plazo_Correcto_" + (i + 1).ToString("D2"), false, false,
                            true));
                    }
                }


                msj.ReservedWord = "AlertMessage";
                msj.Value = "Consulta terminada";
                fResponse.FormiikReservedWords.Add(msj);
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL/Precalificacion", "Nss : " + nss + "Respuesta Correcta ");

            }
            catch(Exception e)
            {

                fResponse = new FlexibleUpdateResponse();
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0,"Precalificacion",e.Message+" - "+e.InnerException==null?"":e.InnerException.Message);
                fResponse.AfectedFields.Add(new FlexibleField("Mensaje", true, false, true));
                //fResponse.UpdateFieldsValues.Add("Mensaje", "777898 - Existió un error al consultar NSS. Reintente más tarde. Descripción sistemas: "+e.StackTrace.ToString());
                fResponse.UpdateFieldsValues.Add("Mensaje", "777898 - "+ e.StackTrace.ToString());

                msj = new FlexibleUpdateReservedWords {ReservedWord = "AlertMessage", Value = "Consulta Terminada"};
                fResponse.FormiikReservedWords.Add(msj);

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL/Precalificacion", "Nss : " + nss + "Respuesta Incorrecta Error : " + e.Message);

            }
            return fResponse;

        }

        public string FormatOcurDropDown(WsRespuestaPlazos[] ocur)
        {
            var res = "![CDATA[<root><Value><Text>001</Text></Value><Items>{0}</Items></root>]]";

            try
            {
                var content = "";
                for (var i = 0; i < ocur.Length; i++)
                {
                    content += "<Text>" + (i+1).ToString("D3") + "," +
                               ocur[i].Plazo.ToString(CultureInfo.InvariantCulture) + " meses - $"
                               + Convert.ToDouble(ocur[i].MontoCredito).ToString("f2",CultureInfo.InvariantCulture) +
                               "</Text>";
                }
                res = res.Replace("{0}", content);
            }
            catch
            {
                res = "";
            }
            return res;
        }

        public string FormatOcur(WsRespuestaPlazos ocur, string gastos)
        {
            var res = "";
            try
            {
                res = "Plazo: " + ocur.Plazo + " meses \nCrédito a otorgar:" 
                    + String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(ocur.MontoCredito)) + " \nGastos por apertura: "
                    + String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(gastos)) + " \nContaria con: " 
                    + String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(ocur.Contarias)) + " \nPago mensual: "
                    + String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(ocur.PagoMensual));
            }
            catch
            {
                res = "";
            }
            return res;
        }
        public FlexibleUpdateResponse BuroCredito(InputFieldsBuroCredito entrada)
        {
            //var jsonRes = "";
            var fResponse = new FlexibleUpdateResponse();
            var msj = new FlexibleUpdateReservedWords();

            try
            {
                var i = false;

                var bc = new ConsultasBc();

                var resultado = "";
                if (IsProduction)
                {
                    i = bc.ConsultaBuro(entrada.APaterno, entrada.AMaterno, entrada.Nombres, entrada.Rfc, entrada.Calle,
                    entrada.NumeroExt, entrada.NumeroInt, entrada.Colonia, entrada.Delegacion, entrada.Estado,
                    entrada.Cp, ref resultado);    
                }
                else
                {
                    i = true;
                }
                
                 
                if (!i)
                {
                    fResponse.UpdateFieldsValues.Add("SujetoDeCredito","No");
                    fResponse.UpdateFieldsValues.Add("Razon",resultado==""?"El cliente tiene adeudos pendientes":resultado);
                    fResponse.UpdateFieldsValues.Add("RutaPdf","");
                }
                else
                {
                    fResponse.UpdateFieldsValues.Add("SujetoDeCredito", "Si");
                    fResponse.UpdateFieldsValues.Add("Razon", "");
                    fResponse.UpdateFieldsValues.Add("RutaPdf", "");
                }


                fResponse.AfectedFields.Add(new FlexibleField("SujetoDeCredito", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("Razon", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("RutaPdf", true, true, true));

                msj.ReservedWord = "AlertMessage";
                msj.Value = "Consulta terminada";
                fResponse.FormiikReservedWords.Add(msj);

            }
            catch(Exception e)
            {
                fResponse = new FlexibleUpdateResponse();
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "services.svc", "Buro de creidto, " + e.StackTrace);
                fResponse.UpdateFieldsValues.Add("SujetoDeCredito", "No");
                fResponse.UpdateFieldsValues.Add("Razon", "Error en la consulta a buró. Reintente más tarde. Error Sistemas: " + e.Message);
                fResponse.UpdateFieldsValues.Add("RutaPdf", "");

                /*
                var sujetoDeCreditoFlex = new FlexibleField("SujetoDeCredito", true, true, true);
                var razonFlex = new FlexibleField("Razon", true, true, true);
                var rutaPdfFlex = new FlexibleField("RutaPdf", true, true, true);
                */
                fResponse.AfectedFields.Add(new FlexibleField("SujetoDeCredito", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("Razon", true, true, true));
                fResponse.AfectedFields.Add(new FlexibleField("RutaPdf", true, true, true));

                msj = new FlexibleUpdateReservedWords {ReservedWord = "AlertMessage", Value = "Consulta terminada"};
                fResponse.FormiikReservedWords.Add(msj);
            }
            return fResponse;
          
        }
        private string GetPlazosInfo(WsRespuestaPreca salida)
        {
            var res = "<html><body>" +
                        "<table><td>Plazo</td><td>Crédito a otorgar</td><td>Gastos por apertura</td>" +
                        "<td>Contarías con</td><td>Monto mensual de pago</td>{content}" +
                        "</table></body><style>#table, th, td, tr {border: 1.5px solid black; border-collapse: " +
                        "collapse;width:auto;font-size:12px;font-family:Calibri;padding:5px;}</style></html>"; 
             
            var content = "";
            for (var i = 0; i < Convert.ToInt32(salida.WSNumPlazos); i++)
            {
                content = content + GetContent(salida.WS_Ocur[i], salida.WSGastosApertura);
            }
            res = res.Replace("{content}", content); 
            
            return res;
        }

        private string GetContent(WsRespuestaPlazos ocur, string gastos)
        {
            var res = "<tr><td>{ws_plazo}</td><td>{ws_mto_credo}</td><td>{ws_gastos}</td><td>{ws_contarias}</td>" +
                      "<td>{ws_pago}</td></tr>";

            res = res.Replace("{ws_plazo}", ocur.Plazo);
            res = res.Replace("{ws_mto_credo}", String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(ocur.MontoCredito)));
            res = res.Replace("{ws_gastos}",
                String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(gastos)));
            res = res.Replace("{ws_contarias}",
                String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(ocur.Contarias)));
            res = res.Replace("{ws_pago}",
                String.Format(CultureInfo.CreateSpecificCulture("es-MX"), "{0:C}", Convert.ToDouble(ocur.PagoMensual)));
            return res;
        }

        

        private Dictionary<string, string> ObtenerDatosPrecalificacion(WsRespuestaPreca datos)
        {
            var result = new Dictionary<string, string>();
            var nombres = "";
            var aPaterno = "";
            var amaterno = "";
            var generoCb = "";
            var fechadeNacimientoCb = "";

            //var nomH = datos.WSNombreTitular.Split(' ');

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL", "Separar nombre : " + datos.WSNombreTitular + " CURP : " + datos.WSCurp);

            var nombreSeparado = separar(datos.WSNombreTitular);

            var arregloNombres = nombreSeparado.Split('@');

            aPaterno = arregloNombres[0];

            var inicioNombres = 1;
            if (datos.WSCurp.ToUpper() != "X")
            {
                amaterno = arregloNombres[1];
                inicioNombres = 2;
            }

            for (var i = inicioNombres; i < arregloNombres.Length; i++)
            {
                nombres = nombres + arregloNombres[i] + " ";
            }

            nombres = nombres.Substring(0, nombres.Length - 1);

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL", "Separado nombre : " + nombres + " Paterno : " + aPaterno + " Materno : " + amaterno + " CURP : " + datos.WSCurp);

            //aPaterno = nomH[0];
            //amaterno = nomH[1];

            //for (var i =2;i<nomH.Length;i++)
            //{
            //    nombres += (nomH[i]+" ");
            //}

            var curpH = datos.WSCurp;

            fechadeNacimientoCb = curpH[8].ToString() + curpH[9].ToString()+"/"+curpH[6].ToString() + curpH[7].ToString()+"/"+curpH[4].ToString() + curpH[5].ToString();

            generoCb = curpH[10] == 'M' ? "Femenino" : "Masculino";

            result.Add("Nombres",nombres);
            result.Add("APaterno", aPaterno);
            result.Add("Amaterno", amaterno);
            result.Add("GeneroCB", generoCb);
            result.Add("FechadeNacimientoCB", fechadeNacimientoCb);

            return result;
        }

        public string separar(string rng)
        {

            string[] nombreArr = null;
            string nuevaCadena = null;
            int i = 0;

            //Dvidir el nombre por palabras en un arreglo
            nombreArr = rng.Split(' ');

            //Analizar cada palabra dentro del arreglo
            for (i = 0; i <= nombreArr.Length - 1; i++)
            {
                switch (nombreArr[i].ToLower())
                {

                    //Palabras que forman parte de un apellido compuesto
                    //Agregar nuevas palabras separadas por una coma
                    case "de":
                    case "del":
                    case "la":
                    case "las":
                    case "los":
                    case "san":
                    case "da":
                    case "das":
                    case "der":
                    case "di":
                    case "die":
                    case "dd":
                    case "el":
                    case "le":
                    case "les":
                    case "mac":
                    case "mc":
                    case "van":
                    case "von":
                    case "y":
                        //Insertar espacio en blanco
                        nuevaCadena = nuevaCadena + nombreArr[i] + " ";
                        break;
                    default:
                        //Insertar caracter delimitador
                        nuevaCadena = nuevaCadena + nombreArr[i] + "@";
                        break;
                }
            }
            return nuevaCadena.Substring(0, nuevaCadena.Length - 1); ;
        }


        public FlexibleUpdateResponse ValidezCredito(string nss)
        {
            var fResponse = new FlexibleUpdateResponse();
            var msj = new FlexibleUpdateReservedWords();
            
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0,"ServiceBL","ValidezCredito - NSS: "+nss);

                if (ConfigurationManager.AppSettings["Produccion"] == "true")
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var cliente = new WSConsultaEstatusMejoravitAppSoapClient();

                    var res = cliente.ConsultaEstatus(nss);

                    if (res == null)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL", "ValidezCredito - NSS: " + nss + ", Error: 998855848 - res null");
                        fResponse.UpdateFieldsValues.Add("ValidacionExitosa", "No");
                        fResponse.UpdateFieldsValues.Add("LeyendaValidacion",
                            "El crédito fue cancelado. Para cualquier aclaración, acuda a las oficinas del Infonavit mas cercanas.");
                        fResponse.AfectedFields.Add(new FlexibleField("ValidacionExitosa", true, false, false));
                        fResponse.AfectedFields.Add(new FlexibleField("LeyendaValidacion", true, false, true));
                    }
                    else
                    {
                        if (res.WSidMensaje == "0000")
                        {
                            fResponse.UpdateFieldsValues.Add("ValidacionExitosa", "Si");
                            fResponse.UpdateFieldsValues.Add("LeyendaValidacion",
                                "Crédito Válido. Por favor continue en la siguiente pestaña.");
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL", "ValidezCredito - NSS: " + nss + ", Validacion Exitosa: "+res.WSMensaje);
                            fResponse.AfectedFields.Add(new FlexibleField("ValidacionExitosa", true, false, false));
                            fResponse.AfectedFields.Add(new FlexibleField("LeyendaValidacion", true, false, true));
                            //fResponse.AfectedFields.Add(new FlexibleField("MensajeTC", true, true, true));
                        }
                        else
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ServiceBL", "ValidezCredito - NSS: " + nss + ", Validacion No Exitosa: " + res.WSMensaje);
                            fResponse.UpdateFieldsValues.Add("ValidacionExitosa", "No");
                            fResponse.UpdateFieldsValues.Add("LeyendaValidacion",
                                "El crédito fue cancelado. Para cualquier aclaración, acuda a las oficinas del Infonavit mas cercanas.");
                            fResponse.AfectedFields.Add(new FlexibleField("ValidacionExitosa", true, false, false));
                            fResponse.AfectedFields.Add(new FlexibleField("LeyendaValidacion", true, false, true));
                        }
                    }
                }
                else
                {
                    fResponse.UpdateFieldsValues.Add("ValidacionExitosa", "Si");
                    fResponse.UpdateFieldsValues.Add("LeyendaValidacion", "Crédito Válido. Por favor continue en la siguiente pestaña.");
                    fResponse.AfectedFields.Add(new FlexibleField("ValidacionExitosa", true, false, false));
                    fResponse.AfectedFields.Add(new FlexibleField("LeyendaValidacion", true, false, true));
                    //fResponse.AfectedFields.Add(new FlexibleField("MensajeTC", false, true, true));
                }
                
                msj.ReservedWord = "AlertMessage";
                msj.Value = "Consulta terminada";
                fResponse.FormiikReservedWords.Add(msj);
            }
            catch(Exception e)
            {
                fResponse = new FlexibleUpdateResponse();
                fResponse.AfectedFields.Add(new FlexibleField("Mensaje", true, false, true));
                fResponse.UpdateFieldsValues.Add("Mensaje", "99885556 - "+ e.StackTrace);

                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ServiceBL", "ValidezCredito - NSS: " + nss + ", Error: 99885556 - " + e.StackTrace);

                msj = new FlexibleUpdateReservedWords {ReservedWord = "AlertMessage", Value = "Consulta Terminada"};
                fResponse.FormiikReservedWords.Add(msj);
            }
            return fResponse;

        }

    }
}