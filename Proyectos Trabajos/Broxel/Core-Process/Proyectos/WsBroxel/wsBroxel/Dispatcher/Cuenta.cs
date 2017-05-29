using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.Dispatcher
{
    public class CuentaEntity
    {
        public string num_cuenta { get; set; }
        public Nullable<int> procesador { get; set; }
    }
    public class DispatcherCuenta
    {
        public string NumeroCuenta { get; set; }
        public int Procesador { get; set; }
    }
}