using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.AccesoDatos.Compras;

namespace AplicacionFragancias.LogicaNegocios.Compras
{
    public class OperaionesCompras
    {
        public COM_ORDENCOMPRA InsertaOrdenCompra(COM_ORDENCOMPRA ordenCompra,
            List<COM_PRODUCTOS_PEDIDOS> listaProductos)
        {
            var nuevaOrdenCompra = new OperacionesCompras().InsertaOrdenCompra(ordenCompra);

            Parallel.ForEach(listaProductos, item =>
            {
                item.NOORDENCOMPRA = nuevaOrdenCompra.NOORDENCOMPRA;
                new OperacionesCompras().InsertaProductoOrdencompra(item);
            });

            return nuevaOrdenCompra;
        }

        public COM_PROVEEDORES InsertaProveedor(COM_PROVEEDORES proveedor, COM_PROVEEDORES_CONTACTOS contactos)
        {
            proveedor = new OperacionesCompras().InsertaProveedor(proveedor);

            if (proveedor != null)
            {
                new OperacionesCompras().InsertaProveedoresContactos(contactos);
            }

            return proveedor;
        }

        public COM_PROVEEDORES ExisteProveedor(string cveProveedor)
        {
            return new OperacionesCompras().ExisteProveedor(cveProveedor);
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

        public List<COM_PRODUCTOS_PEDIDOS> ObtenListaProductos(COM_PRODUCTOS_PEDIDOS ordenCompra)
        {
            return new OperacionesCompras().ObtenListaProductos(ordenCompra);
        }

        public List<COM_PRODUCTOS_PEDIDOS> ObtenListaProductos(string odernCompra)
        {
            return new OperacionesCompras().ObtenListaProductos(odernCompra);
        }

        public List<COM_CAT_ESTATUS_COMPRA> ObtenEstatusCompras()
        {
            return new OperacionesCatalogosCompras().ObtenEstatusCompras();
        }

        public List<COM_PRODUCTOS> ObtenCatProductos()
        {
            return new OperacionesCompras().ObtenCatProductos();
        }

        public List<COM_CAT_UNIDADES_MEDIDA> ObteUnidadesMedidas()
        {
            return new OperacionesCatalogosCompras().ObtenUnidadesMedida();
        }

        public List<COM_CAT_PRESENTACION> ObtenPresentacion()
        {
            return new OperacionesCatalogosCompras().ObtenPresentacion();
        }

        public bool ActualizaProducto(COM_PRODUCTOS producto)
        {
            return new OperacionesCompras().ActualizaProducto(producto);
        }

        public bool EliminarOrdenCompra(string producto)
        {
            return new OperacionesCompras().EliminarOrdenCompra(producto);
        }
    }
}
