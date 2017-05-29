using System;

namespace wsBroxel.App_Code.Online
{
    public abstract class OnlineResponse
    {
        public Boolean Success { get; set; }
        public String UserResponse { get; set; }
        public String Fecha { get; set; }

        public OnlineResponse()
        {
            UserResponse = "Error al procesar la transacción";
        }
    }
}