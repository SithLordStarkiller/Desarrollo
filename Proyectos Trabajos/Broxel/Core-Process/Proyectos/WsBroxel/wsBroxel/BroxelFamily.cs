using System;
using System.Collections.Generic;
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
    }
}