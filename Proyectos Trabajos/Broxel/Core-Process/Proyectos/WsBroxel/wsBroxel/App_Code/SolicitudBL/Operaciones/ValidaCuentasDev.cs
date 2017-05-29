using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    /// <summary>
    /// Operacion para validación de cuentas devoluciones
    /// </summary>
    public class ValidaCuentasDev:IOperacion
    {
        /// <summary>
        /// Método principal de ejecución de la implementadora
        /// </summary>
        /// <param name="oper">Argumentos</param>
        /// <param name="mySql">Objeto de acceso a bd MySql</param>
        /// <returns></returns>
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 10;
            try
            {
                foreach (var cuenta in oper.Cuentas)
                {
                    var id = mySql.ValidaCuentaNoClabe(cuenta);
                    if (id != null)
                    {
                        if (oper.Folio > 0)
                        {
                            decimal saldoRec = 0;
                            var saldo = new BroxelService().GetSaldosPorCuenta(cuenta, "wsConferma", "ValidaCuentasDev");
                            if (saldo.Success == 1)
                            {
                                saldoRec = saldo.Saldos.DisponibleCompras;
                            }
                            else
                            {
                                step = 12;
                                throw new Exception(
                                    "An exception ocurred while inserting  detail for account number  " + cuenta +
                                    " , it's impossible to check account balance at the time, please retry");
                            }
                            if (mySql.InsertDetalleDispersionesWs(oper.Folio, cuenta, saldoRec) == null)
                            {
                                step = 13;
                                throw new Exception(
                                    "An exception ocurred while inserting  detail for account number  " + cuenta +
                                    " , please retry");
                            }
                        }
                        else
                        {
                            step = 14;
                            throw new Exception(
                                "An exception ocurred while inserting  detail for account number  " + cuenta +
                                ", internal data is corrupted , please retry");
                        }
                    }
                    else
                    {
                        step = 11;
                        throw new Exception("Account number " + cuenta + " is invalid or doesn´t exists, remove and retry");
                    }

                }
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = "01|" + e.Message, numTransac = 0 };
            }
            return res;

        }
    }
}