using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using wsBroxel.App_Code;
using wsBroxel.App_Code.Online;

namespace wsBroxel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISolicitud" in both code and config file together.
    [ServiceContract]
    public interface ISolicitud
    {
        [OperationContract]
        DispResponse DispersionACuenta(string numeroCuenta, decimal monto, int idTransac);
        [OperationContract]
        DispResponse DispersionACuentaUsr(string numeroCuenta, decimal monto, int idTransac, string idUser);
        
        [OperationContract]
        DispResponse DispersionACuentaUsrRef(string numeroCuenta, decimal monto, int idTransac, string idUser, string referencia);

        //[OperationContract]
        //DispResponse DispersionAcuentaConCanal(string numeroCuenta, decimal monto, int idTransac, string idUser,
        //    string referencia, string canal);
   

        [OperationContract]
        DispResponse DispersionConferma(decimal monto, List<string> cuentas);
        [OperationContract]
        DispResponse DevolucionConferma(List<string> cuentas);
        [OperationContract]
        DispResponse DispersionCuentasGenerica(decimal monto, List<string> cuentas, string idUsuario);
        [OperationContract]
        string CrearSessionDash(int idUser, int idAplication);
    }
}
