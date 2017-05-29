using System;
using wsBroxel.App_Code.SolicitudBL.Model;

namespace wsBroxel.App_Code.SolicitudBL.Operaciones
{
    public class ValidaOperEnlínea:IOperacion
    {
        public DispResponse Execute(OperArguments oper, MySqlDataAccess mySql)
        {
            DispResponse res = null;
            var step = 11;
            try
            {
                if (!mySql.ValidaOperEnLinea(oper.NumeroCuenta))
                {
                    step = 12;
                    Helper.SendMail("dispersiones@broxel.com", "mauricio.lopez@broxel.com,  rodrigo.diazdeleon@broxel.com, eder.almeraz@broxel.com, joseraul.hernandez@broxel.com, carlos.reyes@broxel.com",
                    "Error en la dispersión wsSPEI", "Se intento realizar una dispersión/ pago a la cuenta " +
                    oper.NumeroCuenta + " con el folio " + oper.Folio + ", quedando en estatus 12"
                    + ", La cuenta no admite operacion SPEI en línea , favor de revisar.", "yMQ3E3ert6", "Broxel : Asignacion de Lineas");
                    throw new Exception("La cuenta no admite operacion SPEI en línea");
                }
            }
            catch (Exception e)
            {
                if (oper.Folio > 0)
                {
                    mySql.SetDispersionWsStatus(oper.Folio, step);
                    mySql.InsertDispersionErr(oper.Folio, e.Message);
                }
                res = new DispResponse { msg = "00", numTransac = 0 };
            }
            return res;
        }
    }
}