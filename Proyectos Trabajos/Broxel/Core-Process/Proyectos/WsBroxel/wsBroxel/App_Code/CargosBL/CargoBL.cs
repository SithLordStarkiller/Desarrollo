using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using wsBroxel.App_Code.RequestResponses;
using wsBroxel.App_Code.SolicitudBL;

namespace wsBroxel.App_Code.CargosBL
{
    public class CargoBL
    {
        public RealizarCargoMontosResp RealizaCargosMejoravit(string claveCliente, int idComercio, string producto,
            List<DetalleMonto> detalleCuenta, bool realizarCargo)
        {
            var resp = new RealizarCargoMontosResp { Exito = false, MensajeError = "Sin errores." };
            var cargo = new CargoDetalleMonto();
            try
            {
                var mySql = new MySqlDataAccess();
                var idSolicitud = 0;
                var folio = "";
                using (var proxySql = new BroxelSQLEntities())
                {

                    // se genero el folio.
                    var folioParametro = new ObjectParameter("Folio", typeof(string));
                    var idSolicitudParametro = new ObjectParameter("IdSolicitud", typeof(int));

                    cargo.IdComercio = idComercio;
                    //generando folios.
                    var nombreComercio = mySql.ObtenerNombreComercio(idComercio);
                    //validando si el comercio existe.
                    if (string.IsNullOrEmpty(nombreComercio))
                    {
                        throw new Exception("No se existe comercio: " + idComercio);
                    }

                    //insertando el cargo para obtener el folio.
                    proxySql.SpInsertarCargoSinDetalle(claveCliente, producto,
                        idComercio,
                        nombreComercio, "WebService", "CARGO", "asignaciondelinea@broxel.com",
                        "asignaciondelinea@broxel.com", 0, true, folioParametro, true, idSolicitudParametro);
                    folio = folioParametro.Value.ToString();
                    idSolicitud = (int)idSolicitudParametro.Value;
                    //validando folio y id de solicitud.
                    if (string.IsNullOrEmpty(folio) || idSolicitud == 0)
                    {
                        throw new Exception("No se genero solicitud, ni folio.");
                    }
                    //insertando Detalle

                    cargo.Folio = folio;
                    cargo.IdComercio = idComercio;
                    //realizando cargos con la solicitud.

                    cargo.hizoCargoMasivo = true;
                }

                var taskDetalle =
                    new Task(() => AgregaCargosDetalles(folio, idSolicitud, claveCliente, producto, detalleCuenta,realizarCargo));
                taskDetalle.Start();

                resp.Exito = true;
                resp.DetalleCargoRealizado = cargo;
                resp.Mensaje = "Sin errores.";
                resp.MensajeError = "";
            }
            catch (Exception e)
            {
                resp.Exito = false;
                resp.DetalleCargoRealizado = new CargoDetalleMonto();
                resp.Mensaje = "No se realizo el cargo.";
                resp.MensajeError = "error WsDisposiciones : RealizarCargoMultiplesMontos=>" + (e.InnerException == null ? e.Message : e.InnerException.Message);
            }

            return resp;

        }

        private void AgregaCargosDetalles(string folio, int idSolicitud, string claveCliente, string producto, List<DetalleMonto> detalleCuenta, bool realizaCarga)
        {
            using (var proxySql = new BroxelSQLEntities())
            {
                foreach (var cuenta in detalleCuenta)
                {
                    try
                    {
                        proxySql.SpInsertarDetalleCargo(idSolicitud, claveCliente, cuenta.NumCuenta,producto, cuenta.Referencia, cuenta.Monto);
                    }
                    catch (Exception e)
                    {
                        Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com", "AgregarCargosDetalle", "Se intento agregar el cargo " + cuenta.Referencia + " a la solicitud " + folio + " con la excepción " + e, "67896789");
                    }
                }
            }
            if (realizaCarga)
                new BroxelService().CargoMasivo(folio);
        }

        /// <summary>
        /// Metodo que actualiza el detalle de cargos masivos mejoravit
        /// </summary>
        /// <param name="folio">Folio de la solicitud de cargo</param>
        public void ActualizaControlCuentasDetalle(string folio)
        {
            using (var sqlCtx = new BroxelSQLEntities())
            {
                var cargoSol =
                    sqlCtx.CargosSolicitudes.Where(c => c.Folio == folio).Include(c => c.CargosDetalle).FirstOrDefault();
                if (cargoSol == null)
                    return;
                if (cargoSol.CargosDetalle == null)
                    return;
                foreach (var detalle in cargoSol.CargosDetalle)
                {
                    try
                    {
                        using (var mySqlCtx = new broxelco_rdgEntities())
                        {
                            var idReferencia = Convert.ToInt32(detalle.Referencia);
                            var ccd =
                                mySqlCtx.ControlCuentasDetalle.FirstOrDefault(
                                    x => x.folioCargo == folio && x.id == idReferencia);
                            if (ccd == null)
                                continue;
                            ccd.IdEstatusCargo = detalle.CodigoRespuesta == -1 ? 3 : 4;
                            mySqlCtx.Entry(ccd).State = EntityState.Modified;
                            mySqlCtx.SaveChanges();
                            //Console.WriteLine("folio: " + folio + ", idReferencia : " + idReferencia.ToString(CultureInfo.InvariantCulture) + ", Resultado: " + (detalle.CodigoRespuesta == -1 ? 3 : 4).ToString(CultureInfo.InvariantCulture));
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.Write(DateTime.Now.ToString("O") + " Error en ActualizaControlCuentasDetalle folio " + folio + ": " + e);
                    }
                }
            }
        }

    }
}