using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsBroxel.App_Code;

namespace wsBroxel
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs ev)
        {
            
            //broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();

            //////////////var cuentas = broxelSqlEntities.CuentasTmp.Where(x=>x.FechaSaldo == null).ToList();

            //////////////Parallel.ForEach(cuentas, (cuenta) =>
            //////////////{
            //////////////    BroxelSQLEntities broxelSqlEntities2 = new BroxelSQLEntities();
            //////////////    BroxelService broxelService = new BroxelService();
            //////////////    var cuen = broxelSqlEntities2.CuentasTmp.FirstOrDefault(x => x.Cuenta == cuenta.Cuenta);
            //////////////    var result = broxelService.BloqueoDeCuenta(cuenta.Cuenta, "WebServiceBloqDupl");

            //////////////    cuen.RespCASA = result.CodigoRespuesta + "-" + result.UserResponse;
            //////////////    cuen.NumAutorizacion = result.NumeroAutorizacion;
            //////////////    cuen.FechaSaldo = DateTime.Now;
            //////////////    broxelSqlEntities2.SaveChanges();
            //////////////});

            //////////////////BroxelService broxelService = new BroxelService();
            //////////////////BroxelSQLEntities broxelSqlEntities = new BroxelSQLEntities();
            //////////////////var cuentas = broxelSqlEntities.CargosDetalle.Where(x => x.IdSolicitud == 14 && x.CodigoRespuesta == -1).ToList();

            //////////////////foreach (var cuenta in cuentas)
            //////////////////{
            //////////////////    Tarjeta t = Helper.GetTarjetaFromCuenta(cuenta.Cuenta);
            //////////////////    var result = broxelService.ReversoCargo(Convert.ToInt32(cuenta.IdMovimiento), t.NumeroTarjeta, t.Cvc2, t.FechaExpira, 2);

            //////////////////    //var result = broxelService.ActivacionDeCuenta(cuenta.Cuenta, "AldoGBeforeCargo20151209-150.08");
            //////////////////    //var result2 = broxelService.GetSaldosPorCuenta(cuenta.Cuenta, "AldoGBeforeCargo");
            //////////////////    Response.Write(cuenta.Cuenta + ":" + result.CodigoRespuesta);
            //////////////////    Response.Write("<br>");
            //////////////////}
            //////////////////Response.Write("OK");

            //////////////////var cuentas = broxelcoRdgEntities.cuentastmp.Where(x => x.Id > 0).ToList();

            //////////////////foreach (var cuenta in cuentas)
            //////////////////{
            //////////////////    var cuentaStr = Helper.GetCuentaFromTarjeta(cuenta.Tarjeta);
            //////////////////    //var result = broxelService.BloqueoDeCuenta(cuenta.Cuenta, "WebService");
            //////////////////    var result2 = broxelService.GetSaldosPorCuenta(cuentaStr, "WebService");
            //////////////////    Response.Write(cuentaStr + ":" + result2.EstadoOperativo + ":" + result2.Saldos.DisponibleCompras);
            //////////////////    Response.Write("<br>");
            //////////////////}

            Response.Write("OK");
        }

        //public String EjecutaBloque(int inicio, int final)
        //{
        //    BroxelService wsBroxel = new BroxelService();
        //    broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
        //    String p = String.Empty;
        //    BroxelEntities broxelEntities = new BroxelEntities();

        //    var cambios = 
        //        broxelcoRdgEntities.Cambios.Where(x => x.Id >= inicio && x.Id < final && x.NIP=="-1" && (x.Activacion!="0" || x.Bloqueo!="0"))
        //            .OrderBy(x => x.Id)
        //            .ToList();
        //    int cont = 0;
        //    foreach (var cambio in cambios)
        //    {
        //        var loog =
        //            broxelEntities.LogTransaccionesSQL.Where(
        //                x => x.Usuario == "opsbroxel" && x.NumCuenta == cambio.Cuenta)
        //                .OrderBy(x => x.Id).ToList();
        //        if (loog.Count > 0)
        //        {
        //            var log = loog.First();
        //            if (log.Resultado == "1")
        //            {
        //                OperarCuentaResponse ocr = new OperarCuentaResponse();
        //                ocr = wsBroxel.BloqueoDeCuenta(cambio.Cuenta, "opsBroxel");
        //                cambio.Activacion = "0";
        //                cambio.CodActiva = log.NumAutorizacion;
        //                cambio.Bloqueo = ocr.CodigoRespuesta.ToString();
        //                cambio.CodBloqueo = ocr.NumeroAutorizacion;
        //                cambio.Resultado = cambio.Activacion + ":" + cambio.NIP + ":" + cambio.CodNIP;
        //                cont++;
        //            }
        //        }        

        //        if (cont % 5 == 0)
        //        {
        //            try
        //            {
        //                broxelcoRdgEntities.SaveChanges();
        //            }
        //            catch (DbEntityValidationException e)
        //            {
        //                foreach (var eve in e.EntityValidationErrors)
        //                {
        //                    p +=
        //                        String.Format(
        //                            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //                    p = eve.ValidationErrors.Aggregate(p,
        //                        (current, ve) =>
        //                            current +
        //                            String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                p += "error cuenta " + cambio.Cuenta + " " + e;
        //            }
        //        }
        //    }
        //    try
        //    {
        //        broxelcoRdgEntities.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            p +=
        //                String.Format(
        //                    "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            p = eve.ValidationErrors.Aggregate(p,
        //                (current, ve) =>
        //                    current +
        //                    String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        p += "error al guardar ultimas cuentas " + e;
        //    }
        //    return p + "OK";

        //}


        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public IEnumerable<object> cambios { get; set; }
    }
}