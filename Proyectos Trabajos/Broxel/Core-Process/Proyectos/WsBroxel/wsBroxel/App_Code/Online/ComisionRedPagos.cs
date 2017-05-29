using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.Online
{
    [Serializable]
    public class ComisionRedPagos
    {
        public String Canal { get; set; }
        public String Leyenda { get; set; }
        public Decimal Monto { get; set; }
    }
    [Serializable]
    public class ComisionRedPagosResult : OnlineResponse
    {
        public List<ComisionRedPagos> ComisionesRedPagos { get; set; }
    }
}