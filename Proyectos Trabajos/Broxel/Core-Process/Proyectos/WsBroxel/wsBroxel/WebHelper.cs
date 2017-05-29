using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using wsBroxel.App_Code;

namespace wsBroxel
{
    public static class WebHelper
    {
        //public static String ProcesaBloque(int inicio, int final)
        //{
        //    BroxelService wsBroxel = new BroxelService();
        //    broxelco_rdgEntities broxelcoRdgEntities = new broxelco_rdgEntities();
        //    String p = String.Empty;

        //    var cambios =
        //        broxelcoRdgEntities.Cambios.Where(x => x.NIP == "211" && x.Id >= inicio && x.Id < final)
        //            .OrderBy(x => x.Id)
        //            .ToList();
        //    int cont = 0;
        //    foreach (var cambio in cambios)
        //    {
        //        OperarCuentaResponse ocr = new OperarCuentaResponse();
        //        var res1 = wsBroxel.ActivacionDeCuenta(cambio.Cuenta, "opsbroxel");
        //        Tarjeta t = Helper.ArmaTarjeta(cambio.Cuenta);
        //        var res2 = wsBroxel.CambiarNip(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2, cambio.PIN, 3);
        //        if (res1.CodigoRespuesta == 0)
        //            ocr = wsBroxel.BloqueoDeCuenta(cambio.Cuenta, "opsBroxel");

        //        cambio.Activacion = res1.CodigoRespuesta.ToString();
        //        cambio.CodActiva = res1.NumeroAutorizacion;
        //        cambio.Bloqueo = ocr.CodigoRespuesta.ToString();
        //        cambio.CodBloqueo = ocr.NumeroAutorizacion;
        //        cambio.NIP = res2.CodigoRespuesta.ToString();
        //        cambio.CodNIP = res2.NumeroAutorizacion;
        //        cambio.Resultado = cambio.Activacion + ":" + cambio.NIP + ":" + cambio.CodNIP;
        //        cont++;
        //        if(cont%5==0){
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
        //    return p+"OK";
        //}
    }
}