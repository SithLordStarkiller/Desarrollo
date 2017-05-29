using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace wsBroxel.BussinessLogic
{
    /// <summary>
    /// Tiene las operaciones que se usan para  broxel family.
    /// </summary>
    public class BroxelFamily
    {
        /// <summary>
        /// valida si la cuenta pertenece a la cuenta padre de BroxelFamily.
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="numCuentaHija"></param>
        /// <returns></returns>
        public bool EsHijoDeLaCuentaPadreFamily(int idUsuarioOnline, string numCuentaHija)
        {
            bool esHijo = false;
            try
            {
                using (var broxelCommon = new BroxelCommonEntities())
                {
                    var cuentaPadre =
                        broxelCommon.CuentaPadreFamily.FirstOrDefault(p => p.IdUsuarioOnlineBroxel == idUsuarioOnline);

                    if (cuentaPadre != null)
                    {
                        var cuentaHija =
                            broxelCommon.CuentaAsociadaFamily.FirstOrDefault(h => h.IdCuentaPadre == cuentaPadre.Id && h.NumeroCuenta == numCuentaHija);

                        if (cuentaHija != null)
                        {
                            esHijo = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                esHijo = false;
            }

            return esHijo;
        }

        /// <summary>
        /// agregar cuenta padre de BroxelFamily.
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="numCuenta"></param>
        /// <param name="claveCliente"></param>
        /// <returns></returns>
        public bool AgregarCuentaPadre(int idUsuarioOnline, string numCuenta, string claveCliente)
        {
            var res = false;
            try
            {
                using (var broxelCommon = new BroxelCommonEntities())
                {
                    CuentaPadreFamily cta = new CuentaPadreFamily
                    {
                        ClaveCliente = claveCliente,
                        IdUsuarioOnlineBroxel = idUsuarioOnline,
                        NumeroCuenta = numCuenta
                    };
                    broxelCommon.CuentaPadreFamily.Add(cta);
                    broxelCommon.SaveChanges();
                    res = true;
                }
            }
            catch (Exception)
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// agregar cuenta hija de BroxelFamily.
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="numCuentaPadre"></param>
        /// <param name="numCuentaHija"></param>
        /// <returns></returns>
        public bool AgregarCuentaHija(int idUsuarioOnline, string numCuentaPadre, string numCuentaHija)
        {
            var res = false;
            try
            {
                using (var broxelCommon = new BroxelCommonEntities())
                {
                    var cuentaPadre =
                        broxelCommon.CuentaPadreFamily.FirstOrDefault(
                            s => s.IdUsuarioOnlineBroxel == idUsuarioOnline && s.NumeroCuenta == numCuentaPadre);

                    if (cuentaPadre != null)
                    {
                        CuentaAsociadaFamily cta = new CuentaAsociadaFamily
                        {
                            NumeroCuenta = numCuentaHija,
                            IdCuentaPadre = cuentaPadre.Id,
                            IdEstatusCuenta = 2, 
                            EstaConfirmada = true
                        };
                        broxelCommon.CuentaAsociadaFamily.Add(cta);
                        broxelCommon.SaveChanges();
                        res = true;
                    }
                }
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }

        /// <summary>
        /// agregar accion cuenta hija de BroxelFamily.
        /// </summary>
        /// <param name="statusAccion"></param>
        /// <param name="idAccion"></param>
        /// <param name="numCuentaHija"></param>
        /// <returns></returns>
        public bool AgregarAccionCuentaHija(string numCuentaHija, int idAccion, bool statusAccion)
        {
            var respuesta = false;
            try
            {
                using (var repositorioCommon = new BroxelCommonEntities())
                {
                    var cuenta = repositorioCommon.CuentaAsociadaFamily.FirstOrDefault(s=> s.NumeroCuenta == numCuentaHija);

                    if (cuenta != null)
                    {
                        var configuraciones = repositorioCommon.ConfigTarjetaAsociada.Where(s => s.IdCuentaAsociada == cuenta.Id && s.IdAccionTarjeta == idAccion);
                        
                        if (configuraciones.Any())
                        {
                            var configTarjetaAsociada = configuraciones.FirstOrDefault();
                            if (configTarjetaAsociada != null)
                            {
                                configTarjetaAsociada.Estatus = statusAccion;
                                repositorioCommon.SaveChanges();
                                respuesta = true;
                            }
                        }
                        else
                        {
                            //insertar en config
                            var config = new ConfigTarjetaAsociada()
                            {
                                Estatus = statusAccion,
                                IdAccionTarjeta = idAccion,
                                IdCuentaAsociada = cuenta.Id
                            };

                            repositorioCommon.ConfigTarjetaAsociada.Add(config);
                            repositorioCommon.SaveChanges();
                            respuesta = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Ocurrio un error al insertar/actualizar la configuracion de la cuenta " + numCuentaHija + " de la accion "  + idAccion + " -el error es: " +ex.Message);
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// agregar accion cuenta hija de BroxelFamily.
        /// </summary>
        /// <param name="idUsuarioOnline"></param>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool EsPadre(string numCuenta, int idUsuarioOnline)
        {
            var respuesta = false;
            try
            {
                using (var repositorioCommon = new BroxelCommonEntities())
                {
                    var cuenta = repositorioCommon.CuentaPadreFamily.FirstOrDefault(s => s.IdUsuarioOnlineBroxel == idUsuarioOnline);

                    if (cuenta != null)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception)
            {
                respuesta = false;
            }
            return respuesta;
        }


        /// <summary>
        /// Confirmar Cuenta BroxelFamily.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool ConfirmarCuentaFamily(string numCuenta)
        {
            var respuesta = false;
            try
            {
                using (var repositorioCommon = new BroxelCommonEntities())
                {
                    var cuenta = repositorioCommon.CuentaAsociadaFamily.FirstOrDefault(s => s.NumeroCuenta == numCuenta);

                    if (cuenta != null)
                    {
                        cuenta.EstaConfirmada = true;
                        repositorioCommon.SaveChanges();
                        respuesta = true;
                    }
                }
            }
            catch (Exception)
            {
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// es la cuenta padre de BroxelFamily.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool EsCuentaPadre(string numCuenta)
        {
            var respuesta = false;
            try
            {
                using (var repositorioCommon = new BroxelCommonEntities())
                {
                    var cuenta = repositorioCommon.CuentaPadreFamily.FirstOrDefault(s => s.NumeroCuenta == numCuenta);

                    if (cuenta != null)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception)
            {
                respuesta = false;
            }
            return respuesta;
        }

        /// <summary>
        /// es la cuenta padre de BroxelFamily.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public CuentaPadreFamily ObtenerCuentaPadre(string numCuenta)
        {
            CuentaPadreFamily cta = null;
            try
            {
                using (var repositorioCommon = new BroxelCommonEntities())
                {
                    var cuenta = repositorioCommon.CuentaPadreFamily.FirstOrDefault(s => s.NumeroCuenta == numCuenta);

                    if (cuenta != null)
                    {
                        cta = cuenta;
                    }
                }
            }
            catch (Exception)
            {
                cta = null;
            }
            return cta;
        }

        /// <summary>
        /// agregar accion cuenta hija de BroxelFamily.
        /// </summary>
        /// <param name="numCuenta"></param>
        /// <returns></returns>
        public bool EsHija(string numCuenta)
        {
            var respuesta = false;
            try
            {
                using (var repositorioCommon = new BroxelCommonEntities())
                {
                    var cuenta = repositorioCommon.CuentaAsociadaFamily.FirstOrDefault(s => s.NumeroCuenta == numCuenta);

                    if (cuenta != null)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}