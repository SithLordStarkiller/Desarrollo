using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;
using proSeguridad;
using System.Transactions;

namespace proDatos
{
    public class clsDatImagen
    {
        public static clsEntResponseImagen insertarRegistroPersona(registro objRegistro)
        {
            clsEntResponseImagen response = new clsEntResponseImagen();
            return response;
        }

        public static List<spuInsertaNombreArchivoPDF_Result> nombreArchPDF(int idCertificacionRegistro)
        {
            sicerEntities conext_Entiti;

            List<spuInsertaNombreArchivoPDF_Result> respuesta = new List<spuInsertaNombreArchivoPDF_Result>();

            int idUsuario = (int)((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]).Usuario.IdUsuario;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            using (conext_Entiti = new sicerEntities(strConnection_Entiti))
            {
                    respuesta = conext_Entiti.spuInsertaNombreArchivoPDF(idCertificacionRegistro).ToList();
                    return respuesta;
                
            }
 
        }

    }
}
