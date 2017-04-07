using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
   public class ValidacionesOCI
    {

       public static bool ValidarTelefonoConLada(string lada, string numTel)
       {
           var lenghtLada = lada.Length;
           var lenghtNumtel = numTel.Length;
           
               switch (lenghtLada)
               {
                   case 2:
                       if (lenghtNumtel==8)
                           return true;
                       break;
                   case 3:
                       if (lenghtNumtel == 7)
                           return true;
                       break;
               }
               return false; 
       }

       public static Dictionary<string, string> ObtenerDiccionarioFlexible(dynamic jsonRecibe)
       {
           var result= new Dictionary<string, string>();

           foreach (var tel in jsonRecibe)
           {
               var nombre = tel.Key;
               var va = tel.Value.ToString();

               result.Add(nombre,va);
           }

           Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ObtenerDiccionarioFlexible", "Aplicacion : " + ConfigurationManager.AppSettings["Aplicacion"]);

           if (ConfigurationManager.AppSettings["Aplicacion"] == "OriginacionMovil" || ConfigurationManager.AppSettings["Aplicacion"] == "OriginacionMovilTest")
           {
               Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ObtenerDiccionarioFlexible", "Es orginacion true ");
               if (result["EstadoCESI"] == "09" || result["EstadoCESI"] == "15")
               {
                   Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ObtenerDiccionarioFlexible", "Es orginacion estado : " + result["EstadoCESI"]);
                   var listaEstados = new EntEstadisCesi().ObtenerEstadisCesi();
                   Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ObtenerDiccionarioFlexible", "Listados : " + listaEstados.Count);

                   Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ObtenerDiccionarioFlexible", "Estado : " + result["EstadoCESI"] + " -> " + listaEstados[result["Estado"]]);
                   result["EstadoCESI"] = listaEstados[result["Estado"]];
               }
           }
           return result;
       }

       public static string validarCampos(Dictionary<string, string> respuestas)
       {
           var result = "";

           if (!ValidarTelefonoConLada(respuestas["LadaR2"], respuestas["Telefono1Ref2"]))
           {
               result = "El teléfono de la Referencia Dos tiene un formato incorrecto";
           }
           if (!ValidarTelefonoConLada(respuestas["LadaR1"], respuestas["Telefono1Ref1"]))
           {
               result = "El teléfono de la Referencia Uno tiene un formato incorrecto";
           }
           if (!ValidarTelefonoConLada(respuestas["LadaCelular"], respuestas["Telefono1Cte"]))
           {
               result = "El teléfono celular del solicitante tiene un formato incorrecto";
           }
           if (!ValidarTelefonoConLada(respuestas["Lada"], respuestas["Telefono2Cte"]))
           {
               result = "El teléfono del solicitante tiene un formato incorrecto";
           }
           if (!ValidarTelefonoConLada(respuestas["LadaEmpresa"], respuestas["TelEmpresa"]))
           {
               result = "El teléfono de Empresa tiene un formato incorrecto";
           }

           return result;
       }
    }
}
