using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Negocios.Originacion.SeguridadMejoravitApp;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class LoginUsuario
    {
        private string _usuario;
        private string _password;
        private string _serialnumber;
        private string _processId;

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

            if (ConfigurationManager.AppSettings["Produccion"] == "true")
            {
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    var proxy = new WSSeguridadMejoravitAppSoapClient();
                    var output = proxy.ValidaUsuario(_usuario, _password);
                    if (output != null)
                    {
                        if (output.WSMensaje == "OK")
                        {
                            EntLogin.RegistraLoginUsuario(_usuario);
                            result = "<Authentication>" +
                                         "<Processes>" +
                                             "<ProcessId>"+_processId+"</ProcessId>" +
                                         "</Processes>" +
                                         "<GroupExternalId>om</GroupExternalId>" +
                                         "<RoleId>96B40AB5-34A4-4699-B3AD-F442F60874FE</RoleId>" +
                                     "</Authentication>";
                        }
                        else
                        {
                            result = output.WSMensaje;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "LoginUsuario - Originacion", "Produccion: " + ConfigurationManager.AppSettings["Produccion"] + ",Usuario: " + _usuario + (_serialnumber == "" ? "" : ", SerialNumber: " + _serialnumber) + ", Error: " + ex.StackTrace);
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
                                         "<GroupExternalId>om</GroupExternalId>" +
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
                var pass=Security.HashSHA512(_password);

                var valido=EntLogin.LoginUser(_usuario, pass);

                if (Convert.ToBoolean(valido["Valido"]))
                {
                    result = "<Authentication>" +
                                 "<Processes>" +
                                 "<ProcessId>" + _processId + "</ProcessId>" +
                                 "</Processes>" +
                                 "<GroupExternalId>om</GroupExternalId>" +
                                 "<RoleId>" + valido["Rol"] + "</RoleId>" +
                             "</Authentication>";
                }
                else
                {
                    result = "La informacion introducida es incorrecta.";
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
                                 "<GroupExternalId>om</GroupExternalId>" +
                                 "<RoleId>A1903AA9-49FB-4CE1-9F7E-FEC793898DE2</RoleId>" +
                             "</Authentication>";
                }
            }

            Logger.WriteLine(Logger.TipoTraceLog.Log, 1, "LoginUsuario - Originacion", "Produccion: " + ConfigurationManager.AppSettings["Produccion"] + ",Usuario: " + _usuario + (_serialnumber == "" ? "" : ", SerialNumber: " + _serialnumber) + ", Resultado: " + result);

            return result;
        }

    }
}
