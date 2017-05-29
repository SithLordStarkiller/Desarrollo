using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using wsBroxel.App_Code;
using wsBroxel.App_Code.TokenBL;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for wsPrivateHelper
    /// </summary>
    [WebService(Namespace = "wsPrivateHelper")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class wsPrivateHelper : WebService
    {
        [WebMethod]
        public Tarjeta GetTarjetaFromCuenta(String numCuenta)
        {
            return !HttpContext.Current.Request.IsLocal ? null : Helper.GetTarjetaFromCuenta(numCuenta);
        }

        [WebMethod]
        public Tarjeta GetTarjetaFromCuentaAdicional(String numCuenta)
        {
            return !HttpContext.Current.Request.IsLocal ? null : Helper.GetTarjetaFromCuentaAdicional(numCuenta);
        }

        [WebMethod]
        public Tarjeta GetTarjetaFromCuentaYTerm(String numCuenta, String tarjetaEnmascarada)
        {
            return !HttpContext.Current.Request.IsLocal ? null : Helper.GetTarjetaFromCuentaYTerm(numCuenta, tarjetaEnmascarada);
        }

        [WebMethod]
        public maquila GetMaquila(String numCuenta)
        {
            wsAdmonUsuarios wsAdmonUsuarios = new wsAdmonUsuarios();
            return !HttpContext.Current.Request.IsLocal ? null :  wsAdmonUsuarios.GetMaquila(numCuenta);
        }

        [WebMethod]
        public String GetCuentaFromTarjeta(String tarjeta)
        {
            return !HttpContext.Current.Request.IsLocal ? null : Helper.GetCuentaFromTarjeta(tarjeta);
        }

        [WebMethod]
        public string Encrypt(string data)
        {
            return AesEncrypterToken.Encrypt(data);
        }
    }
}
