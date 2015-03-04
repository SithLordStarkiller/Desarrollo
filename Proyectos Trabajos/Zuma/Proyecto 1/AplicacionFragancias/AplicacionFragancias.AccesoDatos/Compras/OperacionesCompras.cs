using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.AccesoDatos.Compras
{
    public class OperacionesCompras
    {
        public COM_ORDENCOMPRA InsertaOrdenCompra(COM_ORDENCOMPRA ordenCompra)
        {
            using (var r = new Repositorio<COM_ORDENCOMPRA>())
            {
                return r.Agregar(ordenCompra);
            }
        }

        public COM_PRODUCTOS InsertaProductoOrdencompra(COM_PRODUCTOS ordenCompra)
        {
            using (var r = new Repositorio<COM_PRODUCTOS>())
            {
                return r.Agregar(ordenCompra);
            }
        }
    }
}
