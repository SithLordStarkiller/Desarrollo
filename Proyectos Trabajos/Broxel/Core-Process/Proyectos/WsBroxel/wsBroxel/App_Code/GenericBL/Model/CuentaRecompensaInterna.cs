using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.GenericBL.Model
{
    public class CuentaRecompensaInterna:CuentasRecompensa
    {
        public Tarjeta Tarjeta { set; get; }
        public maquila maquila { set; get; }
    }
}