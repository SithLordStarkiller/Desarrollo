using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace srvBroxel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IsrvBroxel
    {

        [OperationContract]
        String AltaClienteBroxel(String pNombreCorto, String pNombre, String pApPaterno, String pApMaterno,
            String pRazonSocial, DateTime pFechaNacimientoConstitucion, String pRfc, String pNombreRepLegal,
            String pApPaternoRepLegal, String pApMaternoRepLegal, DateTime pFecNacRepLegal, String pRfcRepLegal,
            String pCalle, String pNumExterior, String pNumInterior, String pColonia, String pDelegacionMunicipio,
            String pCp, String pEstado, String pClavePais, String pTelefono, String pCorreoContacto, Boolean pReportaPld,
            Boolean pEmiteFactura, String pProducto, int pMaquilaId);

        [OperationContract]
        String GeneraOdt(String pClaveCliente, String pUsuarioCreacion, String pEstado, String pNombreSolicitante,
            String pEmailNotificacion, String pCodigoProducto, String pCodigoServicio, Decimal pPorcBonificacion,
            String pCodigoPeriodoBonificacion, String pCantidadCuotasBonificacion, DateTime pFechaDesde,
            String pMarcaTpp, String pGrupoDeLiquidacion, String pHabilitaCompra, String pUsuarioGenera,
            String pNombreTarjetahabiente, String pCalle, String pNumExterior, String pNumInterior, String pLocalidad,
            String pColonia, String pTelefono, String pEstadoDetalleCuentas, String pCodigoProvincia,
            String pCodigoPostal, String pTipoDeDocumento, String pNumeroDeDocumento, String pEmail,
            String pDenominacionTarjeta, DateTime pFechaDeNacimiento, String pSexo, String p4TaLinea);

        [OperationContract]
        String GeneraOdtAdicional(String pClaveCliente, String pUsuarioCreacion, String pEstado, String pNombreSolicitante,
          String pEmailNotificacion, String pCodigoProducto, String pCodigoServicio, Decimal pPorcBonificacion,
          String pCodigoPeriodoBonificacion, String pCantidadCuotasBonificacion, DateTime pFechaDesde,
          String pMarcaTpp, String pGrupoDeLiquidacion, String pHabilitaCompra, String pUsuarioGenera,
          String pNombreTarjetahabiente, String pCalle, String pNumExterior, String pNumInterior, String pLocalidad,
          String pColonia, String pTelefono, String pEstadoDetalleCuentas, String pCodigoProvincia,
          String pCodigoPostal, String pTipoDeDocumento, String pNumeroDeDocumento, String pEmail,
          String pDenominacionTarjeta, DateTime pFechaDeNacimiento, String pSexo, String p4TaLinea, String CuentaTitular, Int32 pidMaquila);

        [OperationContract]
        EstatusOdtResponse ValidaEstatusOdt(String pFolioOdt);

        [OperationContract]
        bool ArmaArchivo(string folioOdt);

        [OperationContract]
        Boolean ActualizaClienteMaquila(Int32 pIdMaquila, Int32 pIdCliente, String pClaveCliente, String pUsuario);

        [OperationContract]
        String ObtenerMaquilaInfo(Int32 pIdMaquila);

        [OperationContract]
        Int64 Login(Int64 idUser);

        [OperationContract]
        Int64 ValidateUser(Int64 idSecureId);

        [OperationContract]
        Int64 GetUsuarioOnline(int idClientesBroxel);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class EstatusOdtResponse
    {
        private string _numeroCuenta = "";
        private string _tarjeta = "";
        private string _clabe = "";

        [DataMember]
        public String NumeroCuenta {
            get { return _numeroCuenta; }
            set { _numeroCuenta = value; }
        }

        [DataMember]
        public String Tarjeta
        {
            get { return _tarjeta; }
            set { _tarjeta = value; }
        }

        [DataMember]
        public String Clabe
        {
            get { return _clabe; }
            set { _clabe = value; }
        }
    }
}

