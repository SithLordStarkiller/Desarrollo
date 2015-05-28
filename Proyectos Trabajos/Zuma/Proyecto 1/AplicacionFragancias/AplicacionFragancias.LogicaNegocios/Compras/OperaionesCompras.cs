using System;
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

        public COM_PROVEEDORES InsertaProveedor(COM_PROVEEDORES proveedor)
        {
            return new OperacionesCompras().InsertaProveedor(proveedor);
        }

        public List<COM_PROVEEDORES> ObtenListaProveedores()
        {
            return new OperacionesCompras().ObtenListaProveedores();
        }

        public List<COM_ORDENCOMPRA> ObtenListasOrdenesDeCompra(DateTime inicio, DateTime fin, List<int> status)
        {
            return new OperacionesCompras().ObtenListasOrdenesDeCompra(inicio, fin, status);
        }

        public COM_ORDENCOMPRA ObtenOrdencompra(string noOrdenCompra)
        {
            return new OperacionesCompras().ObtenOrdenCompra(noOrdenCompra);
        }

        public List<COM_PRODUCTOS> ObtenListaProductos(COM_ORDENCOMPRA ordenCompra)
        {
            return new OperacionesCompras().ObtenListaProductos(ordenCompra);
        }

        public List<COM_ESTATUS_COMPRA> ObtenEstatusCompras()
        {
            return new OperacionesCatalogosCompras().ObtenEstatusCompras();
        }

        public bool ActualizaProducto(COM_PRODUCTOS producto)
        {
            return new OperacionesCompras().ActualizaProducto(producto);
        }
    }
}
