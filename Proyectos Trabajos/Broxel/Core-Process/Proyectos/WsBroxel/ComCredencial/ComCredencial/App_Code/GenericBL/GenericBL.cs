using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using ComCredencial.App_Code.Utils;
using ComCredencial.SolicitudBL;

namespace ComCredencial.GenericBL
{
    public class GenericBL
    {
        #region Persiste Mensaje

        /// <summary>
        /// Función para guardar los mensajes de persistencia de credenciales de BroxelService.
        /// </summary>
        /// <param name="fechaInicio">Fecha inicio de llamada al método.</param>
        /// <param name="fechaFin">Fecha fin de termino de proceso.</param>
        /// <param name="numCuenta">El número de cuenta del cliente.</param>
        /// <param name="idMetodo">Id del metodo llamado.</param>
        /// <param name="idServicio">Id del servicio utilizado.</param>
        /// <param name="request">El request de la llamada al método.</param>
        /// <param name="response">El response de respuesta de llamada.</param>
        /// <param name="idMovimiento">Id del movimiento registrado.</param>
        /// <param name="idAnulacion">Id de la anulacion registrada.</param>
        public void PersisteMensaje(DateTime fechaInicio, DateTime fechaFin, string numCuenta, int idMetodo,
            int idServicio, string request, string response, int idMovimiento = 0, int idAnulacion = 0)
        {
            try
            {
                using (var ctx = new BroxelEntities())
                {
                    var msg = new LogMensajes
                    {
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        IdMetodo = idMetodo,
                        IdServicio = idServicio,
                        numCuenta = numCuenta,
                    };
                    if (!string.IsNullOrEmpty(request))
                        msg.request = AesEncrypter.Encrypt(request,
                            string.IsNullOrEmpty(numCuenta) ? "sinCuenta" : numCuenta);
                    if (!string.IsNullOrEmpty(response))
                        msg.response = AesEncrypter.Encrypt(response,
                            string.IsNullOrEmpty(numCuenta) ? "sinCuenta" : numCuenta);
                    if (idMovimiento != 0)
                        msg.IdMovimiento = idMovimiento;
                    if (idAnulacion != 0)
                        msg.IdAnulacion = idAnulacion;
                    ctx.LogMensajes.Add(msg);
                    ctx.SaveChanges();
                }

            }
            catch (DbEntityValidationException ex)
            {
                var exception = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    exception.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exception.AppendFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, omar.vidal@broxel.com", "Error al persistir mensaje en base", "idMetodo:" + idMetodo.ToString(CultureInfo.InvariantCulture) + ", idServicio " + idServicio.ToString(CultureInfo.InvariantCulture) + "Error " + exception, "67896789", "Broxel Fintech");
            }
            catch (Exception e)
            {
                Trace.WriteLine("Ocurrio un error al persistir mensaje: " + e);
                Helper.SendMail("broxelonline@broxel.com", "mauricio.lopez@broxel.com, omar.vidal@broxel.com", "Error al persistir mensaje en base", "idMetodo:" + idMetodo.ToString(CultureInfo.InvariantCulture) + ", idServicio " + idServicio.ToString(CultureInfo.InvariantCulture) + " Detalle del error: " + e, "67896789", "Broxel Fintech");
            }
        }

        #endregion

        #region Niveles de Usuario

        /// <summary>
        /// Valida si una cuenta esta en cuarentena de niveles de servicio
        /// </summary>
        /// <param name="numCuenta">Numero de cuenta</param>
        /// <returns></returns>
        public bool ValidaCuentaEnCuarentena(string numCuenta)
        {
            return new MySqlDataAccess().ValidaCuentaEnCuarentena(numCuenta);
        }

        #endregion
    }
}
