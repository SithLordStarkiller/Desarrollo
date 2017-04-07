using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios.Originacion.SeguridadMejoravit2;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class LoginUsuario
    {
        private string _usuario;
        private string _password;
        private string _serialnumber;
        private string _processId;

        public LoginUsuario()
        {
            
        }

        public LoginUsuario(string usuario,string password,string serialnumber="")
        {
            _usuario = usuario;
            _password = password;
            _serialnumber = serialnumber;
            _processId = EntLogin.ObtenerProductId(ConfigurationManager.AppSettings["Aplicacion"]);
        }

        public string loginMovil()
        {
            var result = "";
            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "LoginUsuario - loginMovil", "1");
            if (ConfigurationManager.AppSettings["Produccion"] == "true")
            {
                try
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "LoginUsuario - loginMovil", "2");
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var proxy = new WSSeguridadMejoravitAppSoapClient();
                    var output = proxy.ValidaUsuario(_usuario, _password);

                    var modelo = new LoginUsuarioModelo() {Usuario = _usuario,Password = _password};

                    var entidad = SerializeXML.SerializeObject(modelo);

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "TraceServiciosYY", "RequestLogin - " + entidad);

                    var LogRequest = SerializeXML.SerializeObject(output);

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "TraceServiciosYY", "ResultadoLogin - " + LogRequest);

                    Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "LoginUsuario - loginMovil", "3");
                    if (output != null)
                    {
                        if (output.WSMensaje == "OK")
                        {
                            var idUsario=EntLogin.RegistraLoginUsuario(_usuario);
                            result = "<Authentication>" +
                                         "<Processes>" +
                                             "<ProcessId>"+_processId+"</ProcessId>" +
                                         "</Processes>" +
                                         "<GroupExternalId>" + (ConfigurationManager.AppSettings["Aplicacion"] == "OriginacionMovil" ? "om" : "omtest") + "</GroupExternalId>" +
                                         "<RoleId>96B40AB5-34A4-4699-B3AD-F442F60874FE</RoleId>" +
                                     "</Authentication>";
                            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "LoginUsuario - loginMovil", "4");
                            var estados = "";

                            foreach (var est in output.WS_Ocur)
                            {
                                estados += (","+est.Estado);
                            }

                            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "LoginUsuario - loginMovil", "5 - "+estados);
                            EntLogin.RegistraEstadoUsuario(estados.Substring(1), idUsario);
                        }
                        else
                        {
                            result = output.WSMensaje;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "LoginUsuario - Originacion", "Produccion: " + ConfigurationManager.AppSettings["Produccion"] + ",Usuario: " + _usuario + (_serialnumber == "" ? "" : ", SerialNumber: " + _serialnumber) + ", Error: " + ex.Message + " -???- " + (ex.InnerException == null ? "" : ex.InnerException.Message));
                    result = "Ocurrio un error al conectarse al servicio. Por favor comuniquese con soporte.";
                }
            }
            else
            {
                if (ConfigurationManager.AppSettings["PermitirLogin"] == "false")
                {
                    result = "La informacion introducida es incorrecta.";
                }
                else
                {
                    result = "<Authentication>" +
                                         "<Processes>" +
                                             "<ProcessId>" + _processId + "</ProcessId>" +
                                         "</Processes>" +
                                         "<GroupExternalId>" + (ConfigurationManager.AppSettings["Aplicacion"] == "OriginacionMovil" ? "om" : "omtest") + "</GroupExternalId>" +
                                         "<RoleId>96B40AB5-34A4-4699-B3AD-F442F60874FE</RoleId>" +
                                     "</Authentication>";
                }
            }

            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "LoginUsuario - Originacion", "Produccion: " + ConfigurationManager.AppSettings["Produccion"] + ",Usuario: " + _usuario + (_serialnumber == "" ? "" : ", SerialNumber: " + _serialnumber) + ", Resultado: " + result);

            return result;
        }

        public string login()
        {
            var result = "";

            if (ConfigurationManager.AppSettings["Produccion"] == "true")
            {
                var pass = new Security().HashSHA512(_password);

                var valido = new EntLogin().LoginUser(_usuario, pass);

                if (Convert.ToBoolean(valido["Valido"]))
                {
                    result = "<Authentication>" +
                                 "<Processes>" +
                                 "<ProcessId>" + _processId + "</ProcessId>" +
                                 "</Processes>" +
                                 "<GroupExternalId>" + (ConfigurationManager.AppSettings["Aplicacion"] == "OriginacionMovil" ? "om" : "omtest") + "</GroupExternalId>" +
                                 "<RoleId>" + valido["Rol"] + "</RoleId>" +
                             "</Authentication>";
                }
                else
                    result = "La informacion introducida es incorrecta.";
            }
            else
            {
                if (ConfigurationManager.AppSettings["PermitirLogin"] == "false")
                    result = "La informacion introducida es incorrecta.";
                else
                {
                    result = "<Authentication>" +
                                 "<Processes>" +
                                 "<ProcessId>" + _processId + "</ProcessId>" +
                                 "</Processes>" +
                                 "<GroupExternalId>" + (ConfigurationManager.AppSettings["Aplicacion"] == "OriginacionMovil" ? "om" : "omtest") + "</GroupExternalId>" +
                                 "<RoleId>A1903AA9-49FB-4CE1-9F7E-FEC793898DE2</RoleId>" +
                             "</Authentication>";
                }
            }

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "LoginUsuario - Originacion", "Produccion: " + ConfigurationManager.AppSettings["Produccion"] + ",Usuario: " + _usuario + (_serialnumber == "" ? "" : ", SerialNumber: " + _serialnumber) + ", Resultado: " + result);

            return result;
        }


        public string ObtenerEstadosUsuario(string usuario)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "LoginUsuario - ObtenerEstadosUsuario", "Produccion: " + ConfigurationManager.AppSettings["Produccion"] + ",Usuario: " + usuario);
            if (ConfigurationManager.AppSettings["Produccion"] == "true")
            {
                var estados = new EntLogin().ObtenerEstadosUsuario(usuario);

                var result = "![CDATA[<root><Items>";

                foreach (var estado in estados)
                {
                    result += ("<Text>"+estado.Key+","+estado.Value+"</Text>");
                }

                result += "</Items></root>]]";

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "LoginUsuario - ObtenerEstadosUsuario", result);

                return result;
            }
            else
            {
                //return "![CDATA[<root><Value><Text>02</Text></Value><Items><Text>01,AGUASCALIENTES</Text><Text>02,BAJA CALIFORNIA</Text><Text>03,BAJA CALIFORNIA SUR</Text></Items></root>]]";
                return "![CDATA[<root><Value><Text>02</Text></Value><Items><Text>09,DISTRITO FEDERAL</Text><Text>15,MEXICO</Text></Items></root>]]";
            }
        }
    }

    public class LoginUsuarioModelo
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
