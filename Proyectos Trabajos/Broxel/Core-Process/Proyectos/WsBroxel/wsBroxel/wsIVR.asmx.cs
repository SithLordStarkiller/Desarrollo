using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using wsBroxel.App_Code;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for wsIVR
    /// </summary>
    [WebService(Namespace = "wsIVR")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsIVR : WebService
    {
        BroxelService broxelService = new BroxelService();
        //SE USA EN IVR
        [WebMethod]
        public SaldoResponse GetSaldoPorTarjeta(String tarjeta, int tipoSaldo)
        {
            String NumCuenta = Helper.GetCuentaFromTarjeta(tarjeta);
            SaldoResponse response = new SaldoResponse();
            if (!String.IsNullOrEmpty(NumCuenta))
            {
                var result = broxelService.GetSaldoPorCuenta(NumCuenta, tipoSaldo, 2);
                response.Success = 1;
                response.Saldos.Saldo = result;
                if (result != -1)
                    response.UserResponse = "OK";
            }
            return response;
        }

        //SE USA EN IVR
        [WebMethod]
        public OperarCuentaResponse ActivaTarjeta(String tarjeta, String FE)
        {
            String NumCuenta = Helper.GetCuentaFromTarjeta(tarjeta);
            Tarjeta t = Helper.GetTarjetaFromCuenta(NumCuenta);
            if (t == null || t.FechaExpira != FE)
                return new OperarCuentaResponse();
            return broxelService.ActivacionDeCuenta(NumCuenta,"THPorTelefono");
        }

        //SE USA EN IVR
        [WebMethod]
        public Int32 TarjetaDeBanxico(String tarjeta)
        {
            broxelco_rdgEntities _brmy = new broxelco_rdgEntities();

            String queryToDB = "select rc.* from registro_tc rc "
                               + "join registri_broxel r "
                               + "on rc.id = r.id_de_registro "
                               + "and left(rc.numero_tc,6)='" + tarjeta.Substring(0, 6) + "' "
                               + "and right(rc.numero_tc,4)='" + tarjeta.Substring(12, 4) + "' "
                               + "and right(r.folio_de_registro,6)='" + tarjeta.Substring(6, 6) + "'";
            var maq = _brmy.vw_registri.SqlQuery(queryToDB).ToList();
            try
            {
                return (maq[0].codigo_de_producto == "K153" || maq[0].codigo_de_producto == "K185") ? 1 : 0;
            }
            catch
            {
                throw new Exception(queryToDB);
                return 2;
            }
        }
    }
}
