using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionFragancias.Entidades;

namespace AplicacionFragancias.LogicaNegocios.Compras
{
    public class OperaionesCompras
    {
        public COM_ORDENCOMPRA InsertaOrdenCompra(COM_ORDENCOMPRA ordenCompra, List<COM_PRODUCTOS> listaProductos)
        {
            var nuevaOrdenCompra = new AccesoDatos.Compras.OperacionesCompras().InsertaOrdenCompra(ordenCompra);

            Parallel.ForEach(listaProductos, item =>
            {
                item.IDORDENCOMPRA = nuevaOrdenCompra.IDORDENCOMPRA;
                var nuevoitem = new AccesoDatos.Compras.OperacionesCompras().InsertaProductoOrdencompra(item);
            });

            return nuevaOrdenCompra;
        }
    }
}
