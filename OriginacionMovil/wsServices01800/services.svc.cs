using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.ModelBinding;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios.Originacion;
using PubliPayments.Utiles;
using Utilerias;
using wsServices01800.AppCode;
using wsServices01800.AppCode.Requests;
using wsServices01800.Formiik;

namespace wsServices01800
{
    public class services : Iservices
    {
        private static readonly Hashtable Oficinas;
        static services()
        {
            PubliPayments.Entidades.ConexionSql.EstalecerConnectionString(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            PubliPayments.Entidades.ConnectionDB.EstalecerConnectionString("SqlDefault", ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            Inicializa.Inicializar(ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString);
            Oficinas = new Hashtable();
            GetOficinasInfonavit();
        }

        public void DoWork()
        {
        }

        public FlexibleUpdateResponse FlexibleUpdateWorkOrder(Stream r)
        {
            var dbUtil = new DBUtil();
            var reader = new StreamReader(r);
            var text = reader.ReadToEnd();


            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "FlexibleUpdateWorkOrder",text);
            Trace.WriteLine("FlexibleUpdateWorkOrder - "+text);

            FlexibleUpdateResponse jsonRes = null;
            var recibe = new JavaScriptSerializer();
            var jsonrecibe = recibe.Deserialize<dynamic>(text);
            var bl = new ServiceBL();
            string externalid = jsonrecibe["ExternalId"];
            string idworkorder = jsonrecibe["IdWorkOrder"];
            string idworkorderformtype = jsonrecibe["IdWorkOrderFormType"];
            string username = jsonrecibe["Username"];
            string workordertype = jsonrecibe["WorkOrderType"];
            string action = jsonrecibe["Action"];

            var idDb = dbUtil.InsertRequestFormiik(text,externalid,idworkorder,idworkorderformtype,username,workordertype,action);

            switch (action)
            {
                case "Precalificacion":
                case "ConsultaPlazos":
                    recibe = new JavaScriptSerializer();
                    jsonrecibe = recibe.Deserialize<dynamic>(text);
                    var desc = "";
                    try
                    {
                        var inputFieldsPrecalifica = jsonrecibe["InputFields"];
                        var nss = inputFieldsPrecalifica["Nss"];
                        desc += "nss: ";
                        var usuario = jsonrecibe["Username"];
                        desc += "usuario: ";
                        var pension = RetrieveOptional(inputFieldsPrecalifica, "PensionAlimenticia");
                        desc += "pension: ";
                        var oficina = RetrieveOptional(inputFieldsPrecalifica, "OficinasCESI");
                        desc += "oficina: ";
                        string nombreOficina = null;

                        pension = pension ?? "0";
                        pension = pension == "" ? "0" : pension;

                        if (!string.IsNullOrEmpty(oficina))
                        {
                            nombreOficina = (String)Oficinas[((string)oficina).PadLeft(5,'0')];    
                        }
                        else
                        {
                            throw new Exception("El valor de la oficina no puede ser nulo, asegurese de haber seleccionado una oficina y  reintente.");    
                        }
                        
                        if (string.IsNullOrEmpty(nombreOficina))
                            nombreOficina = "No Identificada";
                        desc += "nss: ";
                        jsonRes = bl.Precalificacion(nss,idDb,usuario,pension,nombreOficina,oficina);
                        desc += "nss: ";
                    }
                    catch (Exception e)
                    {
                        jsonRes = new FlexibleUpdateResponse();
                        
                        jsonRes.AfectedFields.Add(new FlexibleField("Mensaje",true,false,true));
                        jsonRes.UpdateFieldsValues.Add("Mensaje","2131321 - Existió un error al consultar NSS. Reintente más tarde. Descripción sistemas: " + e.Message +", "+e.InnerException.Message);

                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta Terminada" };
                        jsonRes.FormiikReservedWords.Add(msj);
                    }
                    break;
                case "BuroCredito":
                    recibe = new JavaScriptSerializer();
                    jsonrecibe = recibe.Deserialize<dynamic>(text);
                    try
                    {
                        var inputFieldsBuro = jsonrecibe["InputFields"];
                        var entradaBuro = new InputFieldsBuroCredito
                        {
                            AMaterno = inputFieldsBuro["AMaterno"],
                            APaterno = inputFieldsBuro["APaterno"],
                            Calle = inputFieldsBuro["Calle"],
                            Colonia = inputFieldsBuro["Colonia"],
                            Cp = inputFieldsBuro["Cp"],
                            Delegacion = inputFieldsBuro["Delegacion"],
                            Estado = inputFieldsBuro["Estado"],
                            Nombres = inputFieldsBuro["Nombres"],
                            NumeroExt = inputFieldsBuro["NumeroExt"],
                            NumeroInt = inputFieldsBuro["NumeroInt"],
                            Rfc = inputFieldsBuro["RfcPrecalificacion"]
                        };
                        jsonRes = bl.BuroCredito(entradaBuro);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "services.svc", "Buro de creidto, " + e.StackTrace);
                        jsonRes = new FlexibleUpdateResponse();

                        jsonRes.UpdateFieldsValues.Add("SujetoDeCredito", "No");
                        jsonRes.UpdateFieldsValues.Add("Razon", "Error en la consulta a buró. Reintente más tarde. Error Sistemas: " + e.Message);
                        jsonRes.UpdateFieldsValues.Add("RutaPdf", "");

                        jsonRes.AfectedFields.Add(new FlexibleField("SujetoDeCredito", true, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("Razon", true, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("RutaPdf", true, true, true));

                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                        jsonRes.FormiikReservedWords.Add(msj);

                                            
                    }
                    break;
                case "ValCred":
                    recibe = new JavaScriptSerializer();
                    jsonrecibe = recibe.Deserialize<dynamic>(text);
                    try
                    {
                        var inputFieldsBuro = jsonrecibe["InputFields"];
                        
                        jsonRes = bl.ValidezCredito(inputFieldsBuro["Nss"]);
                    }
                    catch (Exception e)
                    {
                        jsonRes = new FlexibleUpdateResponse();

                        jsonRes.UpdateFieldsValues.Add("SujetoDeCredito", "No");
                        jsonRes.UpdateFieldsValues.Add("Razon", "Error en la consulta a buró. Reintente más tarde. Error Sistemas: " + e.Message);
                        jsonRes.UpdateFieldsValues.Add("RutaPdf", "");

                        jsonRes.AfectedFields.Add(new FlexibleField("SujetoDeCredito", true, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("Razon", true, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("RutaPdf", true, true, true));

                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                        jsonRes.FormiikReservedWords.Add(msj);
                    }
                    break;
                case "registraTarjeta":
                    recibe = new JavaScriptSerializer();
                    jsonrecibe = recibe.Deserialize<dynamic>(text);
                    try
                    {
                        var inputFieldsBuro = jsonrecibe["InputFields"];

                        var resultado = (Dictionary<bool, string>)RegistroCreditos.RegistrarTc(inputFieldsBuro["CifradoTC"], jsonrecibe["ExternalId"], inputFieldsBuro["Nss"], jsonrecibe["Username"].ToString());

                        if (resultado.ContainsKey(true))
                        {
                            jsonRes = new FlexibleUpdateResponse();
                            var mensaje = resultado[true];
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "registraTarjeta", "Mensaje : " + mensaje + " Orden : " + jsonrecibe["ExternalId"]);
                            
                            jsonRes.UpdateFieldsValues.Add("MensajeTC", mensaje);

                            //jsonRes.AfectedFields.Add(new FlexibleField("NumTC", true, true, true));
                            //jsonRes.AfectedFields.Add(new FlexibleField("CorroboraNumTC", true, true, true));
                            //jsonRes.AfectedFields.Add(new FlexibleField("Envio_TC", true, true, true));
                            //jsonRes.AfectedFields.Add(new FlexibleField("CifradoTC", true, true, true));
                            jsonRes.AfectedFields.Add(new FlexibleField("MensajeTC", true, false, true));

                            var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                            jsonRes.FormiikReservedWords.Add(msj);
                        }
                        else
                        {
                            jsonRes = new FlexibleUpdateResponse();
                            var mensaje = resultado[false];
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "registraTarjeta", "Mensaje : " + mensaje + " Orden : " + jsonrecibe["ExternalId"]);

                            jsonRes.UpdateFieldsValues.Add("NumTC", "");
                            jsonRes.UpdateFieldsValues.Add("CorroboraNumTC", "");
                            jsonRes.UpdateFieldsValues.Add("CifradoTC", "");
                            jsonRes.UpdateFieldsValues.Add("MensajeTC", mensaje);

                            jsonRes.AfectedFields.Add(new FlexibleField("NumTC", false, true, true));
                            jsonRes.AfectedFields.Add(new FlexibleField("CorroboraNumTC", false, true, true));
                            jsonRes.AfectedFields.Add(new FlexibleField("Envio_TC", false, true, true));
                            jsonRes.AfectedFields.Add(new FlexibleField("CifradoTC", true, true, false));
                            jsonRes.AfectedFields.Add(new FlexibleField("MensajeTC", false, true,true));

                            var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = mensaje };
                            jsonRes.FormiikReservedWords.Add(msj);
                        }

                    }
                    catch (Exception e)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "registraTarjeta", "Orden : " + jsonrecibe["ExternalId"] + "Error al registrar tarjeta : " + e.Message);

                        jsonRes = new FlexibleUpdateResponse();

                        //jsonRes.UpdateFieldsValues.Add("NumTC", "");
                        //jsonRes.UpdateFieldsValues.Add("CorroboraNumTC", "");
                        //jsonRes.UpdateFieldsValues.Add("CifradoTC", "");
                        //jsonRes.UpdateFieldsValues.Add("MensajeTC", "Error al registrar la tarjeta");

                        //jsonRes.AfectedFields.Add(new FlexibleField("NumTC", false, true, true));
                        //jsonRes.AfectedFields.Add(new FlexibleField("CorroboraNumTC", false, true, true));
                        //jsonRes.AfectedFields.Add(new FlexibleField("Envio_TC", false, true, true));
                        //jsonRes.AfectedFields.Add(new FlexibleField("CifradoTC", true, true, false));
                        //jsonRes.AfectedFields.Add(new FlexibleField("MensajeTC", false, true, true));

                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Error al registrar la tarjeta" };
                        
                        jsonRes.FormiikReservedWords.Add(msj);
                    }
                    
                    break;
                case "ObtenerEstadosUsuario":
                    recibe = new JavaScriptSerializer();
                    jsonrecibe = recibe.Deserialize<dynamic>(text);
                    try
                    {
                        var inputFieldsBuro = jsonrecibe["InputFields"];

                        //var resultado = (Dictionary<bool, string>)RegistroCreditos.RegistrarTc(inputFieldsBuro["CifradoTC"], jsonrecibe["ExternalId"], inputFieldsBuro["Nss"]);

                            jsonRes = new FlexibleUpdateResponse();

                            //var estados = LoginUsuario.ObtenerEstadosUsuario(jsonrecibe["Username"].ToString());

                            var estados = new LoginUsuario().ObtenerEstadosUsuario(jsonrecibe["Username"].ToString());

                            jsonRes.UpdateFieldsValues.Add("EstadoCESI", estados);

                            //jsonRes.AfectedFields.Add(new FlexibleField("NumTC", true, true, true));
                            //jsonRes.AfectedFields.Add(new FlexibleField("CorroboraNumTC", true, true, true));
                            //jsonRes.AfectedFields.Add(new FlexibleField("Envio_TC", true, true, true));
                            //jsonRes.AfectedFields.Add(new FlexibleField("CifradoTC", true, true, true));
                            jsonRes.AfectedFields.Add(new FlexibleField("EstadoCESI", false, false, true));
                            
                            var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                            jsonRes.FormiikReservedWords.Add(msj);
                        

                    }
                    catch (Exception e)
                    {
                        jsonRes = new FlexibleUpdateResponse();

                        jsonRes.UpdateFieldsValues.Add("EstadoCESI", "![CDATA[<root><Value></Value><Items></Items></root>]]");

                        //jsonRes.AfectedFields.Add(new FlexibleField("NumTC", true, true, true));
                        //jsonRes.AfectedFields.Add(new FlexibleField("CorroboraNumTC", true, true, true));
                        //jsonRes.AfectedFields.Add(new FlexibleField("Envio_TC", true, true, true));
                        //jsonRes.AfectedFields.Add(new FlexibleField("CifradoTC", true, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("EstadoCESI", false, false, true));

                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                        jsonRes.FormiikReservedWords.Add(msj);
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "ObtenerEstadosUsuario: " + e.Message);
                        
                    }

                    break;
                case "RegistrarCredito":
                    recibe = new JavaScriptSerializer();
                    jsonrecibe = recibe.Deserialize<dynamic>(text);
                    try
                    {
                        var inputFieldsBuro = jsonrecibe["InputFields"];

                        jsonRes = new FlexibleUpdateResponse();

                        var orig = new Originacion(ValidacionesOCI.ObtenerDiccionarioFlexible(inputFieldsBuro), inputFieldsBuro["ExternalType"], jsonrecibe["ExternalId"], "", jsonrecibe["Username"]);

                        var result = orig.GeneraOriginacion();

                        jsonRes.UpdateFieldsValues.Add("ResultadoRegistro", result);
                        if (result == "El teléfono de la Referencia Dos tiene un formato incorrecto")
                        {
                            jsonRes.UpdateFieldsValues.Add("LadaR2", "" );
                            jsonRes.UpdateFieldsValues.Add("Telefono1Ref2", "" );
                        }
                        if (result == "El teléfono de la Referencia Uno tiene un formato incorrecto")
                        {
                            jsonRes.UpdateFieldsValues.Add("LadaR1", "" );
                            jsonRes.UpdateFieldsValues.Add("Telefono1Ref1", "" );
                        }
                        if (result == "El teléfono celular del solicitante tiene un formato incorrecto")
                        {
                            jsonRes.UpdateFieldsValues.Add("LadaCelular", "" );
                            jsonRes.UpdateFieldsValues.Add("Telefono1Cte","" );
                        }
                        if (result == "El teléfono del solicitante tiene un formato incorrecto")
                        {
                            jsonRes.UpdateFieldsValues.Add("Lada", "" );
                            jsonRes.UpdateFieldsValues.Add("Telefono2Cte", "");
                        }
                        if (result == "El teléfono de Empresa tiene un formato incorrecto")
                        {
                            jsonRes.UpdateFieldsValues.Add("LadaEmpresa",  "" );
                            jsonRes.UpdateFieldsValues.Add("TelEmpresa",  "" );
                        }
                        //if (result != "Registro OK" && result != "El teléfono de Empresa tiene un formato incorrecto")
                        //{
                        //    jsonRes.UpdateFieldsValues.Add("LadaEmpresa",  "" );
                        //    jsonRes.UpdateFieldsValues.Add("TelEmpresa", "");
                        //}

                        jsonRes.AfectedFields.Add(new FlexibleField("ResultadoRegistro", true, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("LadaEmpresa", false, true, true));
                        jsonRes.AfectedFields.Add(new FlexibleField("TelEmpresa", false, true, true));


                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                        var msj1 = new FlexibleUpdateReservedWords { ReservedWord = "ClientError", Value = (result != "Registro OK").ToString() };
                        jsonRes.FormiikReservedWords.Add(msj);
                        jsonRes.FormiikReservedWords.Add(msj1);
                        Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsFormiik", "RegistrarCredito: " + result);

                    }
                    catch (Exception e)
                    {
                        jsonRes = new FlexibleUpdateResponse();

                        //jsonRes.UpdateFieldsValues.Add("ResultadoRegistro", "asdfghjklo");
                        jsonRes.UpdateFieldsValues.Add("ResultadoRegistro", "Ocurrio un error al intentar registrar el credito.");
                        jsonRes.AfectedFields.Add(new FlexibleField("CreditoValidado", true, true, true));

                        jsonRes.AfectedFields.Add(new FlexibleField("ResultadoRegistro", true, true, true));

                        var msj = new FlexibleUpdateReservedWords { ReservedWord = "AlertMessage", Value = "Consulta terminada" };
                        var msj1 = new FlexibleUpdateReservedWords { ReservedWord = "ClientError", Value = "true" };
                        jsonRes.FormiikReservedWords.Add(msj);
                        jsonRes.FormiikReservedWords.Add(msj1);
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsFormiik", "RegistrarCredito: " + e.Message);

                    }

                    break;
            }

            if (idDb != 0)
            {
                dbUtil.UpdateRequestFormiik(idDb, JSonParser.ObjectToJson(jsonRes));
            }

            //Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "FlexibleUpdateWorkOrder", "Respuesta : " + new JavaScriptSerializer().Serialize(jsonRes));
            return jsonRes;
        }


        public Stream SendWorkOrderToClient(Stream respuesta)
        {
            Trace.WriteLine(string.Format("{0} - SendWorkOrderToClient", DateTime.Now));
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "SendWorkOrderToClient Iniciado ");
            //Recibe las respuestas de Formiik en sus sistemas 
            //Devuelve string vacío si recibió la repuesta

            WorkOrderResponse workorderresponse = new WorkOrderResponse();

            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "SendWorkOrderToClient Load Iniciado");
            //Carga un documento XML enviado por formiik con todas las propiedades de una respuesta
            //(ver objeto WorkOrderResponse
            workorderresponse.Load(respuesta);
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "SendWorkOrderToClient Load Terminado");
            
            //A partir de este momento Ud. puede implementar algun metodo
            //para guardar las respuestas. El método que se muestra es solo un demo
            //return workorderresponse.Save();
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "SendWorkOrderToClient SaveFull Iniciado");
            var saveFullStream = workorderresponse.SaveFull();
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "SendWorkOrderToClient SaveFull Terminado");

            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "SendWorkOrderToClient Terminado ");
            return saveFullStream;

        }

        private object RetrieveOptional(dynamic value, string query)
        {
            object ret = null;
            try
            {
                ret = value[query];
            }
            catch (Exception e)
            {
                ret = null;
            }
            return ret;
        }

        private static void GetOficinasInfonavit()
        {
            using (var ctx = new SistemasOriginacionMovilEntities())
            {
                var oficinas = ctx.CatOficinasInfonavit.ToList();
                if (oficinas.Count > 0)
                {
                    foreach (var oficina in oficinas)
                    {
                        Oficinas.Add(oficina.oficina, oficina.NombreOficina);
                    }
                }
            }
        }

        public Stream ValidateUserSimpleReturnGroup(Stream streamUser)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "wsServices01800", "ValidateUserSimpleReturnGroup:Entro");
            try
            {
                //Permite el ingreso al sistema por portal/dispositivo. Devuelve string vacío si es un usuarío válido de lo contrario se 
                //retorna el error del acceso (p.e. "username no registrado", "password incorrecto").
                //Metodo que Ud. puede implmentar para validar a sus usuarios
                //el que se muestra es solo un demo
                StreamReader reader = new StreamReader(streamUser);
                string xmlUser = reader.ReadToEnd();
                XmlDocument xmlUserDoc = new XmlDocument();
                //Carga la cadena string a un XmlDoc
                xmlUserDoc.LoadXml(xmlUser);

                var lu = new LoginUsuario(xmlUserDoc.GetElementsByTagName("username").Item(0).InnerText,
                    xmlUserDoc.GetElementsByTagName("password").Item(0).InnerText);


                return new MemoryStream(Encoding.UTF8.GetBytes(lu.login()));
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "wsServices01800", "ValidateUserSimpleReturnGroup: "+ex.Message+(ex.InnerException!=null?"||| inner: "+ex.InnerException.Message:""));
                return new MemoryStream();
            }
        }

        //Implementación ValidateUserForDevice
        public Stream ValidateUserForDeviceReturnGroup(Stream streamUserDevice)
        {
            /* Permite el ingreso al sistema por dispositivo. Devuelve string vacío si es un usuarío válido de lo contrario se 
             * retorna el error del acceso (p.e. "username no registrado", "password incorrecto").*/
            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "Services", "ValidateUserForDeviceReturnGroup1");
            StreamReader reader = new StreamReader(streamUserDevice);
            string xmlUser = reader.ReadToEnd();
            XmlDocument xmlUserDoc = new XmlDocument();
            xmlUserDoc.LoadXml(xmlUser);
            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "Services", "ValidateUserForDeviceReturnGroup2");
            var lu = new LoginUsuario(xmlUserDoc.GetElementsByTagName("username").Item(0).InnerText, xmlUserDoc.GetElementsByTagName("password").Item(0).InnerText, xmlUserDoc.GetElementsByTagName("serialnumber").Item(0).InnerText);


            return new MemoryStream(Encoding.UTF8.GetBytes(lu.loginMovil()));
        }

        public Stream GetUserCatalog(Stream streamUserDevice)
        {
            /* Permite el ingreso al sistema por dispositivo. Devuelve string vacío si es un usuarío válido de lo contrario se 
             * retorna el error del acceso (p.e. "username no registrado", "password incorrecto").*/

            StreamReader reader = new StreamReader(streamUserDevice);
            string xmlUser = reader.ReadToEnd();
            XmlDocument xmlUserDoc = new XmlDocument();
            xmlUserDoc.LoadXml(xmlUser);

            var result = "<UserCatalog><Catalog Name=\"EstadoCESI\"><registry><Valor_Es>AGUASCALIENTES</Valor_Es><Cod_Es>01</Cod_Es></registry><registry><Valor_Es>BAJA CALIFORNIA</Valor_Es><Cod_Es>02</Cod_Es></registry>" +
                         "<registry><Valor_Es>BAJA CALIFORNIA SUR</Valor_Es><Cod_Es>03</Cod_Es></registry><registry><Valor_Es>CAMPECHE</Valor_Es><Cod_Es>04</Cod_Es></registry><registry><Valor_Es>COAHUILA</Valor_Es><Cod_Es>05</Cod_Es></registry>" +
                         "<registry><Valor_Es>COLIMA</Valor_Es><Cod_Es>06</Cod_Es></registry><registry><Valor_Es>CHIAPAS</Valor_Es><Cod_Es>07</Cod_Es></registry><registry><Valor_Es>CHIHUAHUA</Valor_Es><Cod_Es>08</Cod_Es></registry>" +
                         "<registry><Valor_Es>DISTRITO FEDERAL</Valor_Es><Cod_Es>09</Cod_Es></registry><registry><Valor_Es>DURANGO</Valor_Es><Cod_Es>10</Cod_Es></registry><registry><Valor_Es>GUANAJUATO</Valor_Es><Cod_Es>11</Cod_Es></registry>" +
                         "<registry><Valor_Es>GUERRERO</Valor_Es><Cod_Es>12</Cod_Es></registry><registry>" +
                         "<Valor_Es>HIDALGO</Valor_Es><Cod_Es>13</Cod_Es></registry><registry><Valor_Es>JALISCO</Valor_Es><Cod_Es>14</Cod_Es></registry><registry><Valor_Es>MEXICO</Valor_Es><Cod_Es>15</Cod_Es></registry>" +
                         "<registry><Valor_Es>MICHOACAN</Valor_Es><Cod_Es>16</Cod_Es></registry><registry><Valor_Es>MORELOS</Valor_Es><Cod_Es>17</Cod_Es></registry><registry><Valor_Es>NAYARIT</Valor_Es><Cod_Es>18</Cod_Es></registry>" +
                         "<registry><Valor_Es>NUEVO LEON</Valor_Es><Cod_Es>19</Cod_Es></registry><registry><Valor_Es>OAXACA</Valor_Es><Cod_Es>20</Cod_Es></registry><registry><Valor_Es>PUEBLA</Valor_Es><Cod_Es>21</Cod_Es></registry>" +
                         "<registry><Valor_Es>QUERETARO</Valor_Es><Cod_Es>22</Cod_Es></registry><registry><Valor_Es>QUINTANA ROO</Valor_Es><Cod_Es>23</Cod_Es></registry><registry><Valor_Es>SAN LUIS POTOSI</Valor_Es><Cod_Es>24</Cod_Es></registry>" +
                         "<registry><Valor_Es>SINALOA</Valor_Es><Cod_Es>25</Cod_Es></registry><registry><Valor_Es>SONORA</Valor_Es><Cod_Es>26</Cod_Es></registry><registry><Valor_Es>TABASCO</Valor_Es><Cod_Es>27</Cod_Es></registry>" +
                         "<registry><Valor_Es>TAMAULIPAS</Valor_Es><Cod_Es>28</Cod_Es></registry><registry><Valor_Es>TLAXCALA</Valor_Es><Cod_Es>29</Cod_Es></registry><registry><Valor_Es>VERACRUZ</Valor_Es><Cod_Es>30</Cod_Es></registry>" +
                         "<registry><Valor_Es>YUCATAN</Valor_Es><Cod_Es>31</Cod_Es></registry><registry><Valor_Es>ZACATECAS</Valor_Es><Cod_Es>32</Cod_Es></registry></Catalog>" +
                         "" +
                         "<Catalog Name=\"OficinasCESI\"><registry><Cod_Of>12063</Cod_Of><Cod_Es>12</Cod_Es><Valor_Of>Acapulco</Valor_Of></registry><registry><Cod_Of>1069</Cod_Of><Cod_Es>01</Cod_Es><Valor_Of>Aguascalientes</Valor_Of></registry>" +
                         "<registry><Cod_Of>9028</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>Barranca del Muerto</Valor_Of></registry><registry><Cod_Of>014</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>Oficinas Apoyo</Valor_Of></registry><registry><Cod_Of>4075</Cod_Of><Cod_Es>04</Cod_Es><Valor_Of>Campeche</Valor_Of></registry>" +
                         "<registry><Cod_Of>23076</Cod_Of><Cod_Es>23</Cod_Es><Valor_Of>Cancun</Valor_Of></registry><registry><Cod_Of>28045</Cod_Of><Cod_Es>28</Cod_Es><Valor_Of>Cd. Victoria</Valor_Of></registry>" +
                         "<registry><Cod_Of>11056</Cod_Of><Cod_Es>11</Cod_Es><Valor_Of>Celaya</Valor_Of></registry><registry><Cod_Of>23089</Cod_Of><Cod_Es>23</Cod_Es><Valor_Of>Chetumal</Valor_Of></registry>" +
                         "<registry><Cod_Of>8003</Cod_Of><Cod_Es>08</Cod_Es><Valor_Of>Chihuahua</Valor_Of></registry><registry><Cod_Of>4092</Cod_Of><Cod_Es>04</Cod_Es><Valor_Of>Ciudad del Carmen</Valor_Of></registry>" +
                         "<registry><Cod_Of>26084</Cod_Of><Cod_Es>26</Cod_Es><Valor_Of>Ciudad Guaymas</Valor_Of></registry><registry><Cod_Of>8004</Cod_Of><Cod_Es>08</Cod_Es><Valor_Of>Ciudad Juarez</Valor_Of></registry>" +
                         "<registry><Cod_Of>26083</Cod_Of><Cod_Es>26</Cod_Es><Valor_Of>Ciudad Nogales</Valor_Of></registry><registry><Cod_Of>26002</Cod_Of><Cod_Es>26</Cod_Es><Valor_Of>Ciudad Obregon</Valor_Of></registry>" +
                         "<registry><Cod_Of>24082</Cod_Of><Cod_Es>24</Cod_Es><Valor_Of>Ciudad Valles</Valor_Of></registry><registry><Cod_Of>9026</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>Coapa</Valor_Of></registry>" +
                         "<registry><Cod_Of>30022</Cod_Of><Cod_Es>30</Cod_Es><Valor_Of>Coatzacoalcos</Valor_Of></registry><registry><Cod_Of>6072</Cod_Of><Cod_Es>06</Cod_Es><Valor_Of>Colima</Valor_Of></registry>" +
                         "<registry><Cod_Of>30023</Cod_Of><Cod_Es>30</Cod_Es><Valor_Of>Cordoba</Valor_Of></registry><registry><Cod_Of>9032</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>CTM</Valor_Of></registry>" +
                         "<registry><Cod_Of>17043</Cod_Of><Cod_Es>17</Cod_Es><Valor_Of>Cuernavaca</Valor_Of></registry><registry><Cod_Of>25006</Cod_Of><Cod_Es>25</Cod_Es><Valor_Of>Culiacan</Valor_Of></registry>" +
                         "<registry><Cod_Of>10065</Cod_Of><Cod_Es>10</Cod_Es><Valor_Of>Durango</Valor_Of></registry><registry><Cod_Of>2054</Cod_Of><Cod_Es>02</Cod_Es><Valor_Of>Ensenada</Valor_Of></registry>" +
                         "<registry><Cod_Of>9030</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>Ermita</Valor_Of></registry><registry><Cod_Of>32090</Cod_Of><Cod_Es>32</Cod_Es><Valor_Of>Fresnillo</Valor_Of></registry>" +
                         "<registry><Cod_Of>10066</Cod_Of><Cod_Es>10</Cod_Es><Valor_Of>Gomez Palacio</Valor_Of></registry><registry><Cod_Of>14009</Cod_Of><Cod_Es>14</Cod_Es><Valor_Of>Guadalajara</Valor_Of></registry>" +
                         "<registry><Cod_Of>26001</Cod_Of><Cod_Es>26</Cod_Es><Valor_Of>Hermosillo</Valor_Of></registry><registry><Cod_Of>11057</Cod_Of><Cod_Es>11</Cod_Es><Valor_Of>Irapuato</Valor_Of></registry>" +
                         "<registry><Cod_Of>30042</Cod_Of><Cod_Es>30</Cod_Es><Valor_Of>Jalapa</Valor_Of></registry><registry><Cod_Of>20080</Cod_Of><Cod_Es>20</Cod_Es><Valor_Of>Juchitan</Valor_Of></registry>" +
                         "<registry><Cod_Of>3062</Cod_Of><Cod_Es>03</Cod_Es><Valor_Of>La Paz</Valor_Of></registry><registry><Cod_Of>9027</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>La Viga</Valor_Of></registry>" +
                         "<registry><Cod_Of>16061</Cod_Of><Cod_Es>16</Cod_Es><Valor_Of>Lazaro Cardenas</Valor_Of></registry><registry><Cod_Of>11055</Cod_Of><Cod_Es>11</Cod_Es><Valor_Of>Leon</Valor_Of></registry>" +
                         "<registry><Cod_Of>25078</Cod_Of><Cod_Es>25</Cod_Es><Valor_Of>Los Mochis</Valor_Of></registry><registry><Cod_Of>28048</Cod_Of><Cod_Es>28</Cod_Es><Valor_Of>Matamoros</Valor_Of></registry>" +
                         "<registry><Cod_Of>25007</Cod_Of><Cod_Es>25</Cod_Es><Valor_Of>Mazatlan</Valor_Of></registry><registry><Cod_Of>31044</Cod_Of><Cod_Es>31</Cod_Es><Valor_Of>Merida</Valor_Of></registry>" +
                         "<registry><Cod_Of>2052</Cod_Of><Cod_Es>02</Cod_Es><Valor_Of>Mexicali</Valor_Of></registry><registry><Cod_Of>19005</Cod_Of><Cod_Es>19</Cod_Es><Valor_Of>Monterrey</Valor_Of></registry>" +
                         "<registry><Cod_Of>16059</Cod_Of><Cod_Es>16</Cod_Es><Valor_Of>Morelia</Valor_Of></registry><registry><Cod_Of>28049</Cod_Of><Cod_Es>28</Cod_Es><Valor_Of>Nuevo Laredo</Valor_Of></registry>" +
                         "<registry><Cod_Of>20058</Cod_Of><Cod_Es>20</Cod_Es><Valor_Of>Oaxaca</Valor_Of></registry><registry><Cod_Of>30024</Cod_Of><Cod_Es>30</Cod_Es><Valor_Of>Orizaba</Valor_Of></registry>" +
                         "<registry><Cod_Of>13070</Cod_Of><Cod_Es>13</Cod_Es><Valor_Of>Pachuca</Valor_Of></registry><registry><Cod_Of>5077</Cod_Of><Cod_Es>05</Cod_Es><Valor_Of>Piedras Negras</Valor_Of></registry>" +
                         "<registry><Cod_Of>30025</Cod_Of><Cod_Es>30</Cod_Es><Valor_Of>Poza Rica</Valor_Of></registry><registry><Cod_Of>21013</Cod_Of><Cod_Es>21</Cod_Es><Valor_Of>Puebla</Valor_Of></registry>" +
                         "<registry><Cod_Of>14010</Cod_Of><Cod_Es>14</Cod_Es><Valor_Of>Puerto Vallarta</Valor_Of></registry><registry><Cod_Of>22067</Cod_Of><Cod_Es>22</Cod_Es><Valor_Of>Queretaro</Valor_Of></registry>" +
                         "<registry><Cod_Of>28047</Cod_Of><Cod_Es>28</Cod_Es><Valor_Of>Reynosa</Valor_Of></registry><registry><Cod_Of>11088</Cod_Of><Cod_Es>11</Cod_Es><Valor_Of>Salamanca</Valor_Of></registry>" +
                         "<registry><Cod_Of>5050</Cod_Of><Cod_Es>05</Cod_Es><Valor_Of>Saltillo</Valor_Of></registry><registry><Cod_Of>3081</Cod_Of><Cod_Es>03</Cod_Es><Valor_Of>San Jos? del Cabo</Valor_Of></registry>" +
                         "<registry><Cod_Of>24008</Cod_Of><Cod_Es>24</Cod_Es><Valor_Of>San Luis Potosi</Valor_Of></registry><registry><Cod_Of>26085</Cod_Of><Cod_Es>26</Cod_Es><Valor_Of>San Luis Rio Colorado</Valor_Of></registry>" +
                         "<registry><Cod_Of>28046</Cod_Of><Cod_Es>28</Cod_Es><Valor_Of>Tampico</Valor_Of></registry><registry><Cod_Of>7079</Cod_Of><Cod_Es>07</Cod_Es><Valor_Of>Tapachula</Valor_Of></registry>" +
                         "<registry><Cod_Of>18073</Cod_Of><Cod_Es>18</Cod_Es><Valor_Of>Tepic</Valor_Of></registry><registry><Cod_Of>2053</Cod_Of><Cod_Es>02</Cod_Es><Valor_Of>Tijuana</Valor_Of></registry>" +
                         "<registry><Cod_Of>15011</Cod_Of><Cod_Es>15</Cod_Es><Valor_Of>Tlalnepantla</Valor_Of></registry><registry><Cod_Of>29071</Cod_Of><Cod_Es>29</Cod_Es><Valor_Of>Tlaxcala</Valor_Of></registry>" +
                         "<registry><Cod_Of>15012</Cod_Of><Cod_Es>15</Cod_Es><Valor_Of>Toluca</Valor_Of></registry><registry><Cod_Of>5051</Cod_Of><Cod_Es>05</Cod_Es><Valor_Of>Torreon</Valor_Of></registry>" +
                         "<registry><Cod_Of>7074</Cod_Of><Cod_Es>07</Cod_Es><Valor_Of>Tuxtla Gutierrez</Valor_Of></registry><registry><Cod_Of>16060</Cod_Of><Cod_Es>16</Cod_Es><Valor_Of>Uruapan</Valor_Of></registry>" +
                         "<registry><Cod_Of>9029</Cod_Of><Cod_Es>09</Cod_Es><Valor_Of>Vallejo</Valor_Of></registry><registry><Cod_Of>30021</Cod_Of><Cod_Es>30</Cod_Es><Valor_Of>Veracruz</Valor_Of></registry>" +
                         "<registry><Cod_Of>27064</Cod_Of><Cod_Es>27</Cod_Es><Valor_Of>Villahermosa</Valor_Of></registry><registry><Cod_Of>32068</Cod_Of><Cod_Es>32</Cod_Es><Valor_Of>Zacatecas</Valor_Of></registry>" +
                         "<registry><Cod_Of>16087</Cod_Of><Cod_Es>16</Cod_Es><Valor_Of>Zamora</Valor_Of></registry></Catalog></UserCatalog>";

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Services - Originacion", "GetUserCatalog: Usuario: " + xmlUserDoc.GetElementsByTagName("username").Item(0).InnerText);

            return new MemoryStream(Encoding.UTF8.GetBytes(result));
        }

        public string GenerarDocSolicitudCreditoSimple(Stream streamuser) //SolicitudInscripcionCreditoSimpleModel solicitud)
        //public string GenerarDocSolicitudCreditoSimple(string texto)
        {

            var json = new StreamReader(streamuser).ReadToEnd(); 
            //return string.Empty;
            SolicitudesExternasLoggerNegocio.AddLog(DateTime.Now, "Inicio solicitud: generar doc solicitud crédito", json, "");
            try
            {
                var docsOrden = new DocumentosOrden();
                var urlPDF = docsOrden.GenerarDocSolicitudCreditoSimple(json);
                SolicitudesExternasLoggerNegocio.AddLog(DateTime.Now, "Solicitud de doc generación crédito exitosa", json, urlPDF);
                return urlPDF;
            }
            catch (Exception ex)
            {
                SolicitudesExternasLoggerNegocio.AddLog(DateTime.Now, "Error al procesar solicitud | mensaje: " + ex.Message + " | Inner Excepción: " + ex.InnerException, json, "");
                return "-1 | " + ex.Message;
            }
            /*
             "nss": "",
	"curp": "",
	"rfc": "",
	"apellidoPaterno": "",
	"apellidoMaterno": "",
	"nombre": "",
	"calle": "",
	"numCasaExterior": "",
	"numCasaInterior": "",
	"colonia": "",
	"entidad": "",
	"municipio": "",
	"cp": "",
	"identificacionTipo": "",
	"identificacionNum": "",
	"identificacionFechaDia": "",
	"identificacionFechaMes": "",
	"identificacionFechaAno": "",
	"telefonoLada": "",
	"telefonoNumero": "",
	"celular": "",
	"genero": "",
	"email": "",
	"estadoCivil": "",
	"regimen": "",
	"tipoDevivienda": "",
	"dependientes": "",
	"escolaridad": "option",
	"empresaNombre": "",
	"empresaNRP": "",
	"empresaLada": "",
	"empresaTelefono": "",
	"empresaExt": "",
	"horarioLaboralEntrada": "",
	"horarioLaboralSalida": "",
	"referencia1ApellidoPaterno": "",
	"referencia1ApellidoMaterno": "",
	"referencia1Nombre": "",
	"referencia1Lada": "",
	"referencia1Telefono": "",
	"referencia1Celular": "",
	"referencia2ApellidoPaterno": "",
	"referencia2ApellidoMaterno": "",
	"referencia2Nombre": "",
	"referencia2Lada": "",
	"referencia2Telefono": "",
	"referencia2Celular": "",
	"pansionAlimenticia": "",
	"creditoPlazo": "",
	"montoManoDeObra": "",
	"montoCreditoSolicitado": "",
	"submit": "submit"
             
             
             
             */


            //return urlPDF;

        }

        public string RegenerarDocumentos(Stream streamUserDevice)
        {
            var reader = new StreamReader(streamUserDevice);
            var text = reader.ReadToEnd();
            var recibe = new JavaScriptSerializer();
            var jsonrecibe = recibe.Deserialize<dynamic>(text);
            var idOrden = Convert.ToInt32(jsonrecibe["idOrden"]);
            var documento = jsonrecibe["documento"];

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Services - Originacion", "RegenerarDocumentos: idOrden: " + idOrden + " Doc: " + documento);
            try
            {
                var regenera = new RegenerarDocumento(idOrden);
                var docAGenerar = (DocumentosRegenerar) Convert.ToInt32(documento);
                regenera.GenerarDocumentos(docAGenerar);

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Services - Originacion", "Terminado RegenerarDocumentos: idOrden: " + idOrden + " Doc: " + documento);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "Services - Originacion", "Error RegenerarDocumentos: idOrden: " + idOrden + " Doc: " + documento + " Error: " + ex.Message);
            }

            return "Terminado - Revise log para detalles";
        }


        public string ObtenerDocumentosExcepcion(DocumentosOrigacion modelo)
        {
            try
            {
                var ori = new Originacion();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, GetType().Name,
                    " Modelo Recibido : " + new JavaScriptSerializer().Serialize(modelo));

                var res = ori.ReasignaionFlujosExcepcion(modelo);

                if (res != "OK")
                    return res;

                var orden = new string[1][];

                var modelorden = new EntOrdenes().ObtenerOrdenxCredito(modelo.Credito);

                orden[0] = new[] { modelorden.IdOrden.ToString(CultureInfo.InvariantCulture) };

                res = ori.ReasignarOrden(orden);
                return res.Contains("Error:") ? res : "OK";
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 1, GetType().Name, "ObtenerDocumentosExcepcion Error : " + ex.Message);
                return "Error: " + ex.Message;
            }
        }


        #region V.0.0.1 - 30/03/2017 - EDDER RODEA - MÉTODO PARA ACTUALIZAR REFERENCIA DE CREDITO
        public int RecepcionRefNumCredito( EReferenciaNumeroCredito modelo )
        {
            if (modelo == null)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ActualizaRefNumCredito - Originacion", "NumCredito:" + "Null" + " Error: Número Crédito Null" );
                return 0;
            }
            if (string.IsNullOrEmpty(modelo.CV_CREDITO))
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ActualizaRefNumCredito - Originacion", "NumCredito:" + modelo.CV_CREDITO + " Error: Número Crédito Vacío");
                return 0;
            }

            return DReferenciaNumeroCredito.EjecutaRefNumCredito(modelo.CV_CREDITO, "UPDATE") == 1 ? 1 : 0;
        }
        #endregion

        #region V.0.0.1 - 30/03/2017 - EDDER RODEA - MÉTODO PARA OBTENER REFERENCIAS DE CREDITO PENDIENTES
        public string ListaRefNumCredito()
        {
            var sJson = JsonConvert.SerializeObject( DReferenciaNumeroCredito.ObtenerRefNumCredito( string.Empty, "SELECT"));

            return sJson;

        }
        #endregion
    }
}
