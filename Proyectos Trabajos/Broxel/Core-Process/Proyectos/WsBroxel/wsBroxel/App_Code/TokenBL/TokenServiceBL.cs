using System;
using System.Diagnostics;
using wsBroxel.App_Code.SolicitudBL;

namespace wsBroxel.App_Code.TokenBL
{
    public class TokenServiceBL
    {
        /// <summary>
        ///     Actualiza el otp asociado al Token
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="idApp">Id de la Aplicación</param>
        /// <param name="otp">OTP</param>
        public bool ValidaOtpTemp(string user, string idApp, string otp)
        {
            try
            {
                var mySql = new MySqlDataAccess();
                if (mySql.ValidaOtpTokenBroxel(user, idApp, otp))
                    return mySql.UpdOtpToken("", user, idApp);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Hubo un error al Validar la Otp: " + e);
                return false;
            }
            return false;
        }
    }
}