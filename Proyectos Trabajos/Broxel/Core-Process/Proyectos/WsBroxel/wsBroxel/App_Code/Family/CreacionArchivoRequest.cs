using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code
{
    public class CreacionArchivoRequest
    {
        public List<ParametrosFamily> Parametros { set; get; }
        public DireccionEnvioTarjetaFisica DirEnvioTarjeta { set; get; }
        public DatosAdicionales Datos { set; get; }
    }
}