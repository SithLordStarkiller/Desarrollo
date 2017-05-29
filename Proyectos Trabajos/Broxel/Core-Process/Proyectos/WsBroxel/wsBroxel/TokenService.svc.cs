using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using wsBroxel.App_Code.SolicitudBL;
using wsBroxel.App_Code.TokenBL;
using wsBroxel.App_Code.VCBL.Models;

namespace wsBroxel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TokenService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TokenService.svc or TokenService.svc.cs at the Solution Explorer and start debugging.
    public class TokenService : ITokenService
    {
        public string GetTokenSeed(string user, string appId, string pwd, string deviceId)
        {
            if (!ValidaUsuarioToken(appId, pwd))
                throw new Exception("usuario / password inválido");
            var mySql = new MySqlDataAccess();
            var tokenSeedExist = mySql.GetTokenUsuario(user, appId);
            if (tokenSeedExist != null)
                return tokenSeedExist.TokenSeed;
            while (true)
            {
                var tokenSeed = TokenBroxel.GeneraSemilla();
                var data = mySql.InsertToken(user, tokenSeed, appId, deviceId);
                if (data != null)
                    return tokenSeed;
            }

        }

        public bool ValidateToken(string user, string token)
        {
            var mySql = new MySqlDataAccess();
            var tokenData = mySql.GetTokenUsuario(user);
            return tokenData != null && TokenBroxel.ValidaToken(token, tokenData.TokenSeed, 5, tokenData.Offset);
        }

        public bool CalibrateToken(string user, string token1, string token2)
        {
            var mySql = new MySqlDataAccess();
            var tokenData = mySql.GetTokenUsuario(user);
            var newOffset = TokenBroxel.CalibraToken(token1, tokenData.TokenSeed, 1440);
            //if (newOffset == 0) return false;
            var valida = TokenBroxel.ValidaToken(token2, tokenData.TokenSeed, 5, newOffset);
            return valida && mySql.UpdOffsetToken(user, tokenData.TokenSeed, newOffset);
        }

        public bool ValidateTokenIdApp(string user, string idApp, string token)
        {
            var mySql = new MySqlDataAccess();
            var tokenData = mySql.GetTokenUsuario(user,idApp);
            var tolerance = mySql.GetTokenAppTolerance(idApp);
            return tokenData != null && TokenBroxel.ValidaToken(token, tokenData.TokenSeed, tolerance, tokenData.Offset);
        }

        public VcSeedData ValidateTokenIdAppOtp(string user, string idApp, string token)
        {
            try
            {
                var mySql = new MySqlDataAccess();
                var tokenData = mySql.GetTokenUsuario(user, idApp);
                var tolerance = mySql.GetTokenAppTolerance(idApp);
                if (tokenData != null)
                {
                    if (TokenBroxel.ValidaToken(token, tokenData.TokenSeed, tolerance, tokenData.Offset))
                    {
                        return new VcSeedData { Seed = tokenData.TokenSeed, Status = true, DescStatus = "Seed obtenida y validada con éxito." };
                    }
                    return new VcSeedData { Seed = string.Empty, Status = false, DescStatus = "Token: " + token + " Inválido." };
                }
                return new VcSeedData { Seed = string.Empty, Status = false, DescStatus = "No exite token para el usuario: " + user + " con Id App: " + idApp + "." };
            }
            catch
            {
                return new VcSeedData { Seed = string.Empty, Status = false, DescStatus = "Error al Validar el Token." };
            }
            
        }

        public bool CalibrateTokenIdApp(string user, string idApp, string token1, string token2)
        {
            var mySql = new MySqlDataAccess();
            var tokenData = mySql.GetTokenUsuario(user, idApp);
            var newOffset = TokenBroxel.CalibraToken(token1, tokenData.TokenSeed, 1440);
            //if (newOffset == 0) return false;
            var valida = TokenBroxel.ValidaToken(token2, tokenData.TokenSeed, 5, newOffset);
            return valida && mySql.UpdOffsetToken(user, tokenData.TokenSeed, newOffset);
        }

        private bool ValidaUsuarioToken(string appId, string pwd)
        {
            var result = false;
            try
            {
                var rawpwd = AesEncrypterToken.Decrypt(pwd);
                var datos = rawpwd.Split('|');
                var mySql = new MySqlDataAccess();
                // TODO: Meter despues validacion de fecha
                if (AesEncrypterToken.Decrypt(datos[0]) == AesEncrypterToken.Decrypt(mySql.GetTokenUsuarioApp(appId)))
                    result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
