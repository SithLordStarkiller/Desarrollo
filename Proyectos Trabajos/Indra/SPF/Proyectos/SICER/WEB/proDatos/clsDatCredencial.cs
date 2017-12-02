using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using proSeguridad;
using System.Data.SqlClient;

namespace proDatos
{
    public class clsDatCredencial
    {
        public static string[] consultarCredenciales()
        {
            
            sicerEntities context;
            if (System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] == null)
            {
                return null;
            }
            string strConnection = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);

            using (context = new sicerEntities(strConnection))
            {
                string[] strCredenciales = { "", "", "" };
                var resultado = context.spuConsultarCredenciales();

                List<spuConsultarCredenciales_Result> lisTemporal = new List<spuConsultarCredenciales_Result>();
                foreach (var item in resultado)
                {
                    lisTemporal.Add(new spuConsultarCredenciales_Result
                    {
                        creUsuario = item.creUsuario,
                        crePassword = item.crePassword,
                        creDominio = item.creDominio
                    });
                }

                foreach (spuConsultarCredenciales_Result spResultado in lisTemporal)
                {
                    strCredenciales[0] = spResultado.creUsuario;
                    strCredenciales[1] = spResultado.crePassword;
                    strCredenciales[2] = spResultado.creDominio;
                }
                return strCredenciales;
            }
        
        
        
        
        }

    }
}
