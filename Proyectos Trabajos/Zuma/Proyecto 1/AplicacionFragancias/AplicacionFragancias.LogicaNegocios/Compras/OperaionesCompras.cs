using System.Collections.Generic;
using System.Threading.Tasks;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.AccesoDatos.Compras;

namespace AplicacionFragancias.LogicaNegocios.Compras
{
    public class OperaionesCompras
    {
        public COM_ORDENCOMPRA InsertaOrdenCompra(COM_ORDENCOMPRA ordenCompra, List<COM_PRODUCTOS> listaProductos)
        {
            var nuevaOrdenCompra = new OperacionesCompras().InsertaOrdenCompra(ordenCompra);

            Parallel.ForEach(listaProductos, item =>
            {
                item.NOORDENCOMPRA = nuevaOrdenCompra.NOORDENCOMPRA;
                new OperacionesCompras().InsertaProductoOrdencompra(item);
            });

            return nuevaOrdenCompra;
        }

        public COM_ORDENCOMPRA ObtenOrdencompra(string noOrdenCompra)
        {
            return new OperacionesCompras().ObtenOrdenCompra(noOrdenCompra);
        }

        public List<COM_PRODUCTOS> ObtenListaProductos(COM_ORDENCOMPRA ordenCompra)
        {
            return new OperacionesCompras().ObtenListaProductos(ordenCompra);
        }

        public bool ActualizaProducto(COM_PRODUCTOS producto)
        {
            return new OperacionesCompras().ActualizaProducto(producto);
        }
    }
}
