using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using wsBroxel.App_Code.VCBL.Models;

namespace wsBroxel
{
    [ServiceContract]
    public interface ITokenService
    {
        [OperationContract]
        string GetTokenSeed(string user, string appId, string pwd, string deviceId);

        [OperationContract]
        bool ValidateToken(string user, string token);

        [OperationContract]
        bool CalibrateToken(string user, string token1, string token2);

        [OperationContract]
        bool ValidateTokenIdApp(string user, string idApp, string token);

        [OperationContract]
        VcSeedData ValidateTokenIdAppOtp(string user, string idApp, string token);

        [OperationContract]
        bool CalibrateTokenIdApp(string user, string idApp, string token1, string token2);

    }
}
