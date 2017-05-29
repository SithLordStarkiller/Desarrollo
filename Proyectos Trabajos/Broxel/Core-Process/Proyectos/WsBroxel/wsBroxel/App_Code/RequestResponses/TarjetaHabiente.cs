using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsBroxel.wsConsultas;

namespace wsBroxel.App_Code
{
    [Serializable]
    public class TarjetaHabiente
    {
        public String Nombre { get; set; }
        public TarjetaHabiente()
        {
        }
    }

    [Serializable]
    public class TarjetaHabienteRequest : Request
    {
        public Tarjeta Tarjeta;
        public String Cuenta;
        public TarjetaHabienteRequest()
        {
        }
    }

    [Serializable]
    public class TarjetaHabienteResponse : Response
    {
        TarjetaHabiente tarjetaHabiente;
        public TarjetaHabienteResponse()
        {
        }
    }
}