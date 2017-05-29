using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{

    /// <summary>
    /// Valida la lista de cuentas
    /// </summary>
    public class ValidaCuentas : IOperacion
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
                // Validacion de monto

                if (oper.Monto < 0)
                {
                    step = 14;
                    throw new Exception("Amount can't be negative, please retry");
                }

                foreach (var cuenta in oper.Cuentas)
                {
                    var id = mySql.ValidaCuentaNoClabe(cuenta);
                    if (id != null)
                    {
                        if (oper.Folio > 0)
                        {
                            if (mySql.InsertDetalleDispersionesWs(oper.Folio, cuenta) == null)
                            {
                                step = 12;
                                throw new Exception("An exception ocurred while inserting  detail for account number  " + cuenta + " , please retry");
                            }
                            oper.TipoOper = TiposOperacion.Dispersion;
                        }
                        else
                        {
                            step = 13;
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