using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.SqlServer.Server;
using wsBroxel.App_Code.RenominacionBL.Model;
using wsBroxel.App_Code.SolicitudBL;

namespace wsBroxel.App_Code.AsignacionLineaBL
{
    /// <summary>
    /// Logica de negocios de creacion de solicitudes de asignación de línea
    /// </summary>
    public class AsignaLineaBL
    {
        #region Metodos Publicos

        public string InsertaDispersionConOriginacion(List<OriginacionData> originaciones, string estado, bool sinAtm = false, bool ejecuta = true)
        {
            var folio = "";
            try
            {
                folio = InsertSolicitudDispersion(originaciones[0].ClaveCliente, originaciones[0].Producto, estado);
                if (folio == "")
                    return folio;
                var dispersion =
                    new Task(() => InsertaDispersionDetallesConOriginacion(originaciones, folio, sinAtm, ejecuta));
                dispersion.Start();
                return folio;
            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error de dispersion service", "La renominacion fallo :  " + e, "yMQ3E3ert6", "Broxel : Dispersión Service");
            }
            return folio;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="originaciones"></param>
        /// <param name="folio"></param>
        /// <param name="sinAtm"></param>
        /// <param name="ejecuta"></param>
        private void InsertaDispersionDetallesConOriginacion(List<OriginacionData> originaciones, string folio,
            bool sinAtm = false, bool ejecuta = true)
        {
            var webService = new BroxelService();

            try
            {
                foreach (var originacion in originaciones)
                {
                    InsertDispersionesInternas(originacion, folio);
                }
                ActualizaDispersion(folio);
                if (ejecuta)
                    webService.Incremento(folio, "webService");

            }
            catch (Exception e)
            {
                Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com", "Error de dispersion service", "La renominacion fallo :  " + e, "yMQ3E3ert6", "Broxel : Dispersión Service");
            }
        }

        /// <summary>
        /// Valida si el folio de dispersion es mejoravit
        /// </summary>
        /// <param name="folio">folio de dispersion</param>
        /// <returns></returns>
        public bool ValidaDispersionMejoravit(string folio)
        {
            try
            {
                var res = new MySqlDataAccess().ValidaDispersionMejoravit(folio);
                if (res)
                {
                    var task = new Task(() => ActualizaIndOriginacionDispersion(folio));
                    task.Start();
                }
                return res;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al validar dispersion mejoravit: " + e);
                return false;
            }
        }
        /// <summary>
        /// Actualiza el status de la dispersion validada
        /// </summary>
        /// <param name="folio">Folio de dispersion</param>
        public void ActualizaIndOriginacionDispersion(string folio)
        {
            try
            {
                new MySqlDataAccess().ActualizaIndOriginacionDispersion(folio);
                using (var ctx = new broxelco_rdgEntities())
                {
                    var proxy = new BroxelService();
                    var dispersionesInternas = ctx.dispersionesInternas.Where(x => x.idSolicitud == folio && (x.incrementoPOS > 0 || x.incrementoATM > 0) && x.codigoRespuestaPOS == "-1").ToList();

                    foreach (var dispersionInterna in dispersionesInternas)
                    {
                        try
                        {
                            proxy.ActivacionDeCuenta(dispersionInterna.cuenta, "FondeoMejoravit");
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine("Ocurrio un error al actualizar la cuenta " + dispersionInterna.cuenta + ": " + e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error al actualizar status de ind_Originacion para la dispersion: " + folio + ": " + e);
            }
        }

        #endregion
        #region Metodos Privados

        private string InsertSolicitudDispersion(string claveCliente, string producto, string estado)
        {
            return new MySqlDataAccess().InsertSolicitudDispersion(claveCliente, producto, estado);
        }
        private bool InsertDispersionesInternas(OriginacionData originacion, string folio)
        {
            var res = false;
            try
            {
                res = new MySqlDataAccess().InsertDispersionInterna(folio, originacion.NumCuenta, originacion.Producto, originacion.ClaveCliente, Convert.ToDecimal(originacion.MontoPrestamoNumero));
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error al insertar dispersion Solicitud: " + ex);
                res = false;
            }
            return res;
        }

        private bool ActualizaDispersion(string folio)
        {
            return new MySqlDataAccess().FinalizaDispersionSolicitud(folio);
        }


        #endregion
    }
}