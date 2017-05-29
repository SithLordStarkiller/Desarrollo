using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class ValidaCuenta:IOperacion
    {

        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 10;
            try
            {
                var infoCuenta = mySql.ValidaCuenta(oper.NumeroCuenta);
                if (infoCuenta!=null)
                {
                    if (infoCuenta.Producto == "K165")
                    {
                        var infoVirtual = mySql.ObtenCuentaVirtualWe(infoCuenta.ClaveCliente);
                        if (infoVirtual != null)
                        {
                            oper.Referencia = "Cuenta K165: " + oper.NumeroCuenta;
                            oper.NumeroCuenta = infoVirtual.Cuenta;
                            infoCuenta.Clabe = infoVirtual.Clabe;
                            if(oper.Folio > 0)
                                UpdateReferencia(oper.Folio,oper.Referencia,mySql);
                        }
                        else
                        {
                            throw new Exception("No se pudo obtener la información de la cuenta virtual WE");
                        }
                    }
                    if (!mySql.SetCuentaWsStatus(oper.Folio, oper.NumeroCuenta))
                    {
                        throw new Exception("No se pudo actualizar la cuenta en la solicitud");
                    }
                    oper.ClabeCliente = infoCuenta.Clabe;
                }
                else
                {
                    throw new Exception("No se pudo encontrar informacion para la cuenta: " + oper.NumeroCuenta);
                }
            }
            catch(Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse{msg="01",numTransac = 0};
            }
            return res;
        }

        private void UpdateReferencia(long folio, string referencia, MySqlDataAccess mySql)
        {
            try
            {
                mySql.SetReferenciaWs(folio, referencia);
            }
            catch (Exception e)
            {
                mySql.InsertDispersionErr(folio, "Error al actualizar referencia: " + e);
            }
        }
    }
}