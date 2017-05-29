using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    public class Favorito
    {
        public int IdFavorito { get; set; }
        public string NumCuenta { get; set; }
        public string Tarjeta { get; set; }
        public string Producto { get; set; }
        public string ProductoDesc { get; set; }
        public string Alias { get; set; }

    }
}