using System;

namespace wsBroxel.App_Code
{
    [Serializable]
    public class Usuario
    {
        public Int32 IdUsuario { get; set; }
        public String NombreUsuario { get; set; }
        public Int32 IdComercio { get; set; }
    }


    [Serializable]
    public class UsuarioResponse : Response
    {
        public UsuarioResponse()
        {
        }
    }

}