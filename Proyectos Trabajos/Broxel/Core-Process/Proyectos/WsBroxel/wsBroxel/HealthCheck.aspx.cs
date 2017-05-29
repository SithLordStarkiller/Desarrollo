using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsBroxel.App_Code;

namespace wsBroxel
{
    public partial class HealthCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BroxelService wsBroxel = new BroxelService();
                var resp = wsBroxel.GetSaldosPorCuenta("511538490", "2", "HealthCheck");
                if (resp != null)
                {
                    if (resp.Saldos.DisponibleAdelantos > 0.0m)
                        lblResultado.Text = "OK";
                    else
                        lblResultado.Text = "ERROR";
                }
                else
                    lblResultado.Text = "ERROR";

                //var resp2 = wsBroxel.GetSaldosPorCuenta("5115", 2);
                //if (resp != null)
                //{
                //    if (resp.Saldos.DisponibleAdelantos > 0.0)
                //        lblResultado.Text = "OK";
                //    else
                //        lblResultado.Text = "ERROR";
                //}
                //else
                //    lblResultado.Text = "ERROR";
            }
            catch
            {
                lblResultado.Text = "ERROR";
            }
        }
    }
}

//broxelco_rdgEntities dbHelper = new broxelco_rdgEntities();
            //Tarjeta t;
            //int POS = 0, ATM = 0;
            //BroxelService webService = new BroxelService();
            //List<String> cuentasActivadas = new List<string>();
            //List<String> cuentasBloqueadas = new List<string>();
            //int count = 0;
            //string folio = "1404D000011";

            //var dispersionSolicitud = dbHelper.dispersionesSolicitudes.Where(x => x.folio == folio).ToList();
            //var dispersiones =
            //    dbHelper.dispersionesInternas.Where(
            //        x =>
            //            x.idSolicitud == folio &&
            //            ((x.codigoRespuestaPOS != "-1" && x.incrementoPOS > 0.0) ||
            //             (x.codigoRespuestaATM != "-1" && x.incrementoATM > 0.0))).OrderBy(x => x.id).ToList();
            //foreach (var dispersion in dispersiones)
            //{

            //    var maq2 = (from r in dbHelper.maquila
            //        join rb in dbHelper.vw_registri_broxel on r.num_cuenta equals rb.NrucO
            //        where r.num_cuenta == dispersion.cuenta && rb.tipo == "00"
            //        select new
            //        {
            //            NumeroTarjeta =
            //                r.nro_tarjeta.Substring(0, 6) + rb.folio_de_registro.Substring(5) +
            //                r.nro_tarjeta.Substring(13),
            //            NombreTarjetaHabiente = r.nombre_tarjethabiente,
            //            Id = rb.id
            //        }).OrderByDescending(x => x.Id);
            //    var maq = maq2.ToList();
            //        if (maq.Count > 1)
            //        {
            //            int max = maq.Max(x => x.Id);
            //            maq = maq.Where(x => x.Id == max).ToList();
            //        }
            //        t = new Tarjeta(maq[0].NombreTarjetaHabiente, maq[0].NumeroTarjeta,
            //                            dispersion.fechaExpiracion, dispersion.cvc);
            //string cuenta = String.Empty;

            //var cuentas = (from n in db.correr_disponibles
            //    join n2 in db.ind_movimientos on n.nruco equals n2.NroRuc
            //    select new {Cuenta = n2.NroRuc, Producto = n2.CodPtoCuota, Disponible = n.disponible})
            //    .Where(x => x.Producto == "K151" && x.Disponible <= 70).ToList();
            
            //Response.Write("<br><br>");
            //for (int i = 0; i < 100; i++)
            //{
            //    int num = Helper.GetRandomNumber(1, cuentas.Count);
            //    string numCuenta = cuentas.ElementAt(num).Cuenta;
            //    Response.Write("Cuenta;"+numCuenta+";Disponibles;"+ cuentas.ElementAt(num).Disponible + ";SaldoOnLine;"+ webService.GetSaldosPorCuenta(numCuenta,2).Saldos.DisponibleCompras + ";<br>");
            //}
            //Response.Write("<br><br>");


            //Helper.ActualizaYMandaMail("monserrat.martinez@broxel.com", "1403D000002", new List<String>(), "Incremento");

            //var dispersiones = db.dispersionesInternas.Where(x => x.idSolicitud == "1402D000030" && x.id >= 28428
            //    && x.codigoRespuestaATM != "-1").ToList();
            //int cont = 0;
            //var renominaciones = db.renom.Where(x=>x.Renominada!="1").OrderBy(x => x.id).ToList();
            //foreach (var re in renominaciones)
            //{
            //    cont++;
            //    var resp = webService.Nominacion(re.CUENTA, "SANTA FE", "AV MARIO PANI", "05348", "CUAJIMALPA",
            //        "400 PISO 1", "OFICINA", "H", "A" + re.CUENTA, re.TARJETA, "IFE", "", "44330303", "20130327",
            //        "SOLTERO", "BROXEL SAPI", "MASCULINO", "BROXEL SAPI", "TITULAR");
            //    re.Renominada = "1";
            //    re.CodigoResp = resp.CodigoRespuesta.ToString();
            //    re.Respuesta = resp.UserResponse.Substring(0, 16);
            //    if (cont < 50) continue;
            //    try
            //    {
            //        db.SaveChanges();
            //        cont = 0;
            //    }
            //    catch(DbEntityValidationException ex)
            //    {
            //        foreach (var eve in ex.EntityValidationErrors)
            //        {
            //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //            foreach (var ve in eve.ValidationErrors)
            //            {
            //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                    ve.PropertyName, ve.ErrorMessage);
            //            }
            //        }
            //    }
            //}

            //try
            //{
            //    foreach (var dispersion in dispersiones)
            //    {
            //        var maq = (from m in db.maquila
            //            join r in db.registri_broxel on m.num_cuenta equals r.NrucO
            //            where m.num_cuenta == dispersion.cuenta && r.tipo == "00"
            //            select new
            //            {
            //                NumeroTarjeta =
            //                    m.nro_tarjeta.Substring(0, 6) + r.folio_de_registro.Substring(5) +
            //                    m.nro_tarjeta.Substring(13),
            //                NombreTarjetaHabiente = m.nombre_tarjethabiente,
            //                Id = r.id
            //            }).OrderByDescending(x => x.Id).ToList();
            //        if (maq.Count > 1)
            //        {
            //            int max = maq.Max(x => x.Id);
            //            maq = maq.Where(x => x.Id == max).ToList();
            //        }
            //        var t = new Tarjeta(maq[0].NombreTarjetaHabiente, maq[0].NumeroTarjeta,
            //            dispersion.fechaExpiracion, dispersion.cvc);
            //        var resp =
            //            webService.ReversoLimite(t.NombreTarjeta, t.NumeroTarjeta, t.FechaExpira, t.Cvc2,
            //                Convert.ToInt32(dispersion.idmovPOS), dispersion.codigoAutorizacionPOS, 2);
            //        dispersion.codigoRespuestaATM = resp.CodigoRespuesta.ToString();
            //        dispersion.codigoAutorizacionATM = resp.NumeroAutorizacion;
            //        dispersion.idmovATM = resp.IdMovimiento.ToString();
            //        var reeesp = webService.GetSaldosPorTarjeta(t.NumeroTarjeta, 2); 
            //        if(reeesp.Saldos != null){
            //            var sobra = reeesp.Saldos.DisponibleCompras;
            //            if (Math.Abs(sobra) > 0.01)
            //                dispersion.despuesATM = (float) sobra;
            //        }
            //        db.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    bool DoNothing = true;
            //}